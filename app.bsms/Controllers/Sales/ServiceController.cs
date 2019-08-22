using app.bsms;
using app.bsms.api;
using app.bsms.Common;
using app.bsms.Controllers;
using app.bsms.Helpers;
using app.bsms.Models.Account;
using app.bsms.Models.Catelogue;
using app.bsms.Models.Sales;
using app.bsms.Models.Sales.Post;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Sales
{
    [NoCache]
    public class ServiceController : BaseController
    {
        public ServiceController()
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(FormCollection collection)
        {
            ActionResult action;
            decimal num;
            DateTime? nullable;
            string str;
            string str1;
            string str2;
            string str3;
            try
            {
                if (base.ModelState.IsValid && collection["cart_details"] != null)
                {
                    List<Cart_Details> cartDetails = JsonConvert.DeserializeObject<List<Cart_Details>>(Convert.ToString(collection["cart_details"]));
                    if (cartDetails != null)
                    {
                        List<Cart_Details> list = (
                            from f in cartDetails
                            where f.lineType != "TD" 
                            //Yoonus
                            group f by new { lineStatus = f.lineStatus, lineType = f.lineType, itemCode = f.itemCode, itemName = f.itemName, referenceTreatmentCode = f.referenceTreatmentCode, isFOC = f.isFOC, referenceTransactionNumber = f.referenceTransactionNumber, topupBalance = f.topupBalance, topupOutstanding = f.topupOutstanding } into f
                            //group f by new { lineStatus = f.lineStatus, lineType = f.lineType, itemCode = f.itemCode, itemName = f.itemName, referenceTreatmentCode = f.referenceTreatmentCode, isFOC = f.isFOC } into f 
                            select new Cart_Details()
                            {
                                lineStatus = f.Key.lineStatus,
                                lineType = f.Key.lineType,
                                itemCode = f.Key.itemCode,
                                itemName = f.Key.itemName,
                                itemQty = f.Sum<Cart_Details>((Cart_Details s) => s.itemQty),
                                unitDiscount = f.Sum<Cart_Details>((Cart_Details s) => s.unitDiscount),
                                unitPrice = f.Sum<Cart_Details>((Cart_Details u) => u.unitPrice),
                                promoPrice = (f.Key.isFOC ? decimal.Zero : f.Sum<Cart_Details>((Cart_Details s) => s.itemQty * s.unitPrice) - f.Sum<Cart_Details>((Cart_Details s) => s.unitDiscount)),
                                itemAmount = (f.Key.isFOC ? decimal.Zero : f.Sum<Cart_Details>((Cart_Details s) => s.itemQty * s.unitPrice) - f.Sum<Cart_Details>((Cart_Details s) => s.unitDiscount)),
                                salesAmount = (f.Key.isFOC ? decimal.Zero : f.Sum<Cart_Details>((Cart_Details s) => s.itemQty * s.unitPrice) - f.Sum<Cart_Details>((Cart_Details s) => s.unitDiscount)),
                                referenceTreatmentCode = f.Key.referenceTreatmentCode,
                                //Yoonus
                                referenceTransactionNumber = f.Key.referenceTransactionNumber,
                                topupBalance = f.Key.topupBalance,
                                topupOutstanding = f.Key.topupOutstanding,
                                //Yoonus
                                isFOC = f.Key.isFOC,
                                courseDetails = new List<Course_Details>(),
                                discountDetails = new List<Discounts>(),
                                salesStaffDetails = new List<SalesStaff>(),
                                prepaidAccountDetails = new List<Prepaid>()
                            }).ToList<Cart_Details>();
                        List<Settings> item = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).settings;
                        bool flag = ((
                            from s in item
                            where s.settingName == "GST SETTING"
                            select s).FirstOrDefault<Settings>().settingValue == "INCLUSIVE" ? true : false);
                        decimal one = Convert.ToDecimal((
                            from s in item
                            where s.settingName == "GST PERCENTAGE"
                            select s).FirstOrDefault<Settings>().settingValue);
                        one = decimal.One + (one / new decimal(100));
                        string str4 = (
                            from s in item
                            where s.settingName == "GST APPLY BY"
                            select s).FirstOrDefault<Settings>().settingValue;
                        foreach (Cart_Details zero in list)
                        {
                            app.bsms.api.Service.Parameters.Clear();
                            app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                            app.bsms.api.Service.Parameters.Add("itemCode", zero.itemCode);
                            //Item item1 = app.bsms.api.Service.Get<Item>("getItemDetails");
                            //Yoonus Changing to getItemDetail
                            Item item1 = app.bsms.api.Service.Get<Item>("getItemDetail");
                            Cart_Details courseDetails = (
                                from f in cartDetails
                                where f.itemCode == zero.itemCode
                                select f).FirstOrDefault<Cart_Details>();
                            zero.salesStaffDetails = courseDetails.salesStaffDetails;
                            if (courseDetails.salesStaffDetails == null)
                            {
                                zero.strStaffs = null;
                            }
                            else
                            {
                                zero.strStaffs = JsonConvert.SerializeObject(courseDetails.salesStaffDetails);
                                Cart_Details cartDetail = zero;
                                if (courseDetails.strStaffs == "[]")
                                {
                                    str3 = null;
                                }
                                else
                                {
                                    str3 = courseDetails.strStaffs;
                                }
                                cartDetail.strStaffs = str3;
                            }
                            if (zero.lineType == "TD")
                            {
                                zero.discountDetails = null;
                                zero.strDiscounts = null;
                            }
                            else
                            {
                                zero.discountDetails = courseDetails.discountDetails;
                                if (courseDetails.discountDetails == null)
                                {
                                    zero.strDiscounts = null;
                                }
                                else
                                {
                                    zero.strDiscounts = JsonConvert.SerializeObject(courseDetails.discountDetails);
                                    Cart_Details cartDetail1 = zero;
                                    if (courseDetails.strDiscounts == "[]")
                                    {
                                        str2 = null;
                                    }
                                    else
                                    {
                                        str2 = courseDetails.strDiscounts;
                                    }
                                    cartDetail1.strDiscounts = str2;
                                    zero.unitDiscount = zero.discountDetails.Sum<Discounts>((Discounts d) => d.discountAmount);
                                    zero.discountPercent = zero.discountDetails.Sum<Discounts>((Discounts p) => p.discountPercentage);
                                    zero.discountUser = courseDetails.discountUser;
                                }
                            }
                            if (zero.lineType == "COURSE")
                            {
                                if (courseDetails.courseDetails == null)
                                {
                                    zero.promoPrice = (zero.unitPrice * zero.itemQty) - zero.unitDiscount;
                                }
                                else if (courseDetails.courseDetails.FirstOrDefault<Course_Details>().courseType != "N")
                                {
                                    zero.promoPrice = (zero.unitPrice * zero.itemQty) - zero.unitDiscount;
                                }
                                else
                                {
                                    zero.totalTreatments = courseDetails.totalTreatments;
                                    Cart_Details value = zero;
                                    int? nullable1 = zero.totalTreatments;
                                    value.promoPrice = (nullable1.Value * zero.unitPrice) - zero.unitDiscount;
                                }
                                zero.itemAmount = zero.promoPrice;
                                zero.salesAmount = zero.promoPrice;
                                zero.depositAmount = (zero.depositAmount.IsNullOrZero() ? new decimal?(zero.promoPrice) : zero.depositAmount);
                            }
                            else if (!(zero.lineType == "VOUCHER") || !(item1.brandName == "NON SALES VOUCHER"))
                            {
                                zero.promoPrice = (zero.isFOC ? decimal.Zero : (zero.unitPrice * zero.itemQty) - zero.unitDiscount);
                                zero.itemAmount = (zero.isFOC ? decimal.Zero : zero.promoPrice);
                                if (zero.lineType != "TD")
                                {
                                    zero.balanceAmount = decimal.Zero;
                                    zero.salesAmount = (zero.isFOC ? decimal.Zero : zero.promoPrice);
                                    zero.depositAmount = (zero.depositAmount.IsNullOrZero() ? new decimal?(zero.promoPrice) : zero.depositAmount);
                                }
                                else
                                {
                                    zero.balanceAmount = courseDetails.balanceAmount;
                                    zero.treatmentCode = courseDetails.treatmentCode;
                                    zero.isFirstTreatmentDone = courseDetails.isFirstTreatmentDone;
                                    zero.salesAmount = decimal.Zero;
                                    Cart_Details nullable2 = zero;
                                    num = new decimal();
                                    nullable2.depositAmount = new decimal?(num);
                                }
                            }
                            else
                            {
                                zero.promoPrice = decimal.Zero;
                                zero.itemAmount = decimal.Zero;
                                zero.salesAmount = decimal.Zero;
                                Cart_Details cartDetail2 = zero;
                                num = new decimal();
                                cartDetail2.depositAmount = new decimal?(num);
                            }
                            if (zero.lineType != "COURSE")
                            {
                                if (courseDetails.courseDetails == null)
                                {
                                    courseDetails.courseDetails = new List<Course_Details>();
                                }
                                if (zero.lineType != "PACKAGE")
                                {
                                    Course_Details courseDetail = new Course_Details()
                                    {
                                        treatmentNumber = 1,
                                        itemCode = zero.itemCode,
                                        itemDescription = zero.itemName,
                                        treatmentUnitPrice = zero.itemAmount,
                                        courseType = "N",
                                        treatmentStatus = (courseDetails.isFirstTreatmentDone ? "Done" : "Open"),
                                        treatmentDate = DateTime.Now
                                    };
                                    nullable = null;
                                    courseDetail.nextAppt = nullable;
                                    courseDetail.isFOC = new bool?(zero.isFOC);
                                    courseDetails.courseDetails.Add(courseDetail);
                                }
                                else
                                {
                                    courseDetails.courseDetails = new List<Course_Details>();
                                    app.bsms.api.Service.Parameters.Clear();
                                    app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                                    app.bsms.api.Service.Parameters.Add("itemCode", zero.itemCode);
                                    List<PackageDetails> packageDetails = new List<PackageDetails>();
                                    packageDetails.AddRange(app.bsms.api.Service.GetList<PackageDetails>("packageContent"));
                                    foreach (PackageDetails packageDetail in packageDetails)
                                    {
                                        for (int i = 0; i < packageDetail.quantity; i++)
                                        {
                                            Course_Details courseDetail1 = new Course_Details()
                                            {
                                                treatmentNumber = packageDetail.lineNumber,
                                                itemCode = packageDetail.contentItemCode,
                                                itemDescription = packageDetail.contentItemName,
                                                treatmentUnitPrice = packageDetail.price,
                                                courseType = "N",
                                                treatmentStatus = (courseDetails.isFirstTreatmentDone ? "Done" : "Open"),
                                                treatmentDate = DateTime.Now
                                            };
                                            nullable = null;
                                            courseDetail1.nextAppt = nullable;
                                            courseDetail1.isFOC = new bool?(zero.isFOC);
                                            courseDetails.courseDetails.Add(courseDetail1);
                                        }
                                    }
                                }
                            }
                            zero.courseDetails = courseDetails.courseDetails;
                            if (zero.lineType == "PREPAID")
                            {
                                if (courseDetails.prepaidAccountDetails != null)
                                {
                                    zero.strPrepaid = JsonConvert.SerializeObject(courseDetails.prepaidAccountDetails);
                                    Cart_Details cartDetail3 = zero;
                                    if (zero.strStaffs == "[]")
                                    {
                                        str1 = null;
                                    }
                                    else
                                    {
                                        str1 = zero.strStaffs;
                                    }
                                    cartDetail3.strPrepaid = str1;
                                }
                                else if (!item1.openPrepaid)
                                {
                                    courseDetails.prepaidAccountDetails = new List<Prepaid>();
                                    app.bsms.api.Service.Parameters.Clear();
                                    app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                                    app.bsms.api.Service.Parameters.Add("rangeCode", item1.rangeCode);
                                    ItemType itemType = app.bsms.api.Service.GetList<ItemType>("prepaid").FirstOrDefault<ItemType>((ItemType f) => f.stockCode == zero.itemCode);
                                    if (itemType != null)
                                    {
                                        foreach (Prepaid prepaid in itemType.prepaidCondition)
                                        {
                                            List<Prepaid> prepaids = courseDetails.prepaidAccountDetails;
                                            Prepaid prepaid1 = new Prepaid()
                                            {
                                                prepaidLineNumber = 1,
                                                prepaidType = zero.itemCode
                                            };
                                            decimal? nullable3 = prepaid.prepaidValue;
                                            prepaid1.prepaidBonus = nullable3.Value - zero.unitPrice;
                                            decimal num1 = zero.unitPrice;
                                            nullable3 = prepaid.prepaidValue;
                                            prepaid1.prepaidTotal = num1 + (nullable3.Value - zero.unitPrice);
                                            prepaid1.prepaidAmount = new decimal?(zero.unitPrice);
                                            prepaid1.prepaidValue = prepaid.prepaidValue;
                                            prepaid1.prepaidItemType = prepaid.prepaidItemType;
                                            prepaid1.prepaidCondition1 = (prepaid.prepaidCondition1 == null ? "All" : prepaid.prepaidCondition1);
                                            prepaid1.prepaidCondition2 = (prepaid.prepaidCondition2 == null ? "All" : prepaid.prepaidCondition2);
                                            nullable = null;
                                            prepaid1.prepaidExpiryDate = nullable;
                                            prepaids.Add(prepaid1);
                                        }
                                    }
                                    zero.strPrepaid = JsonConvert.SerializeObject(courseDetails.prepaidAccountDetails);
                                    Cart_Details cartDetail4 = zero;
                                    if (zero.strStaffs == "[]")
                                    {
                                        str = null;
                                    }
                                    else
                                    {
                                        str = zero.strStaffs;
                                    }
                                    cartDetail4.strPrepaid = str;
                                }
                            }
                            zero.prepaidAccountDetails = courseDetails.prepaidAccountDetails;
                            zero.holdItemQty = (courseDetails.holdItemQty.IsNullOrZero() ? new int?(0) : courseDetails.holdItemQty);
                            if (str4 != "ALL")
                            {
                                if (!item1.taxable)
                                {
                                    continue;
                                }
                                zero.gstAmountCollected = Utility.GetGST(flag, zero.itemAmount, one);
                            }
                            else
                            {
                                zero.gstAmountCollected = Utility.GetGST(flag, zero.itemAmount, one);
                            }
                        }
                        list.AddRange(
                            from f in cartDetails
                            where f.lineType == "TD"
                            select f);
                        app.bsms.api.Service.Parameters.Clear();
                        app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                        app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
                        Temp_Cart tempCart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                        if (tempCart.userID == null)
                        {
                            tempCart = new Temp_Cart()
                            {
                                userID = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID,
                                siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode,
                                staffCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).staffCode,
                                macAddress = base.Request.ServerVariables["REMOTE_ADDR"]
                            };
                        }
                        tempCart.transactionDetails = list;
                        tempCart.totalQuantity = tempCart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.itemQty);
                        tempCart.totalAmount = tempCart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.itemAmount);
                        tempCart.depositAmount = tempCart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => ((!f.depositAmount.HasValue ? new decimal?(new decimal()) : f.depositAmount)).Value);
                        tempCart.payDetails = new List<app.bsms.Models.Sales.Post.Payment_Details>();
                        app.bsms.api.Service.Parameters.Clear();
                        JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
                        {
                            ContractResolver = new NullToEmptyStringResolver()
                        };
                        app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject(tempCart, jsonSerializerSetting));
                    }
                    else
                    {
                        action = base.RedirectToAction("Empty", "Cart");
                        return action;
                    }
                }
                if (Convert.ToString(collection["callBy"]) == "COURSE" || Convert.ToString(collection["callBy"]) == "PREPAID")
                {
                    action = base.Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    action = base.RedirectToAction("Index", "Cart");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return action;
        }

        public ActionResult GetProductItemType(string type, string code)
        {
            Catelogue catelogue = new Catelogue()
            {
                type = type
            };
            app.bsms.api.Service.Parameters.Clear();
            app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
            app.bsms.api.Service.Parameters.Add("rangeCode", code);
            if (type == "product")
            {
                catelogue.lstItemTypes = app.bsms.api.Service.GetList<ItemType>("retailProduct");
            }
            else if (type == "voucher")
            {
                catelogue.lstItemTypes = app.bsms.api.Service.GetList<ItemType>("voucher");
            }
            else if (type == "prepaid")
            {
                catelogue.lstItemTypes = app.bsms.api.Service.GetList<ItemType>("prepaid");
            }
            
            return this.PartialView("_PartialItemType", catelogue);
        }

        public ActionResult GetRange(string type, string code)
        {
            Catelogue catelogue = new Catelogue();
            app.bsms.api.Service.Parameters.Clear();
            app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
            app.bsms.api.Service.Parameters.Add("brandCode", code);
            catelogue.type = type;
            if (type == "product")
            {
                catelogue.lstRanges = app.bsms.api.Service.GetList<Range>("range");
            }
            else if (type == "voucher")
            {
                catelogue.lstRanges = app.bsms.api.Service.GetList<Range>("voucherRange");
            }
            else if (type == "prepaid")
            {
                catelogue.lstRanges = app.bsms.api.Service.GetList<Range>("prepaidRange");
            }
            return this.PartialView("_PartialType", catelogue);
        }

        public ActionResult GetServiceType(string serviceID)
        {
            Catelogue catelogue = new Catelogue();
            app.bsms.api.Service.Parameters.Clear();
            app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
            app.bsms.api.Service.Parameters.Add("departmentCode", serviceID);
            catelogue.lstServiceTypes = app.bsms.api.Service.GetList<ServiceType>("serviceType");
            return this.PartialView("_PartialServiceType", catelogue);
        }

        //public ActionResult GetServiceType()
        //{
        //    Catelogue catelogue = new Catelogue();
        //    app.bsms.api.Service.Parameters.Clear();
        //    app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
        //    catelogue.lstServiceTypes = app.bsms.api.Service.GetList<ServiceType>("serviceType");
        //    return this.PartialView("_PartialServiceType", catelogue);
        //}

        public ActionResult GetServiceSearch(string text)
        {
            Catelogue catelogue = new Catelogue();
            app.bsms.api.Service.Parameters.Clear();
            app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
            app.bsms.api.Service.Parameters.Add("itemName", text);
            List<ServiceTypeItem> list = (
                from f in app.bsms.api.Service.GetList<ServiceTypeItem>("searchItem")
                orderby f.stockName
                select f).ToList<ServiceTypeItem>();
            catelogue.lstServiceTypeItems = (
                from f in list
                where f.itemType != "COURSE"
                select new ServiceTypeItem()
                {
                    stockCode = f.stockCode,
                    stockName = f.stockName,
                    itemPrice = f.itemPrice,
                    itemType = f.itemType,
                    workCommPoints = f.workCommPoints,
                    salesCommPoints = f.salesCommPoints,
                    itemSinglePrice = f.itemPrice,
                    itemSingleCode = f.stockCode,
                    itemBoth = false
                }).ToList<ServiceTypeItem>();
            list = (
                from f in list
                where f.itemType == "COURSE"
                select f).ToList<ServiceTypeItem>();
            foreach (ServiceTypeItem serviceTypeItem in list)
            {
                if (!catelogue.lstServiceTypeItems.Exists((ServiceTypeItem f) => f.stockName == serviceTypeItem.stockName))
                {
                    catelogue.lstServiceTypeItems.Add(new ServiceTypeItem()
                    {
                        stockName = serviceTypeItem.stockName,
                        itemCourseCode = serviceTypeItem.stockCode,
                        itemCoursePrice = serviceTypeItem.itemPrice,
                        itemType = "COURSE",
                        itemBoth = false
                    });
                }
                else
                {
                    ServiceTypeItem serviceTypeItem1 = catelogue.lstServiceTypeItems.SingleOrDefault<ServiceTypeItem>((ServiceTypeItem f) => f.stockName == serviceTypeItem.stockName);
                    serviceTypeItem1.itemSingleCode = serviceTypeItem1.stockCode;
                    serviceTypeItem1.itemSinglePrice = serviceTypeItem1.itemPrice;
                    serviceTypeItem1.itemCourseCode = serviceTypeItem.stockCode;
                    serviceTypeItem1.itemCoursePrice = serviceTypeItem.itemPrice;
                    serviceTypeItem1.itemType = "BOTH";
                    serviceTypeItem1.itemBoth = true;
                }
            }
            return this.PartialView("_PartialServiceTypeItem", catelogue);
        }

        public ActionResult GetServiceTypeItem(string typeID)
        {
            Catelogue catelogue = new Catelogue();
            app.bsms.api.Service.Parameters.Clear();
            app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
            app.bsms.api.Service.Parameters.Add("rangeCode", typeID);
            List<ServiceTypeItem> list = (
                from f in app.bsms.api.Service.GetList<ServiceTypeItem>("serviceItem")
                orderby f.stockName
                select f).ToList<ServiceTypeItem>();
            catelogue.lstServiceTypeItems = (
                from f in list
                where f.itemType != "COURSE"
                select new ServiceTypeItem()
                {
                    stockCode = f.stockCode,
                    stockName = f.stockName,
                    itemPrice = f.itemPrice,
                    itemType = f.itemType,
                    workCommPoints = f.workCommPoints,
                    salesCommPoints = f.salesCommPoints,
                    itemSinglePrice = f.itemPrice,
                    itemSingleCode = f.stockCode,
                    itemBoth = false
                }).ToList<ServiceTypeItem>();
            list = (
                from f in list
                where f.itemType == "COURSE"
                select f).ToList<ServiceTypeItem>();
            foreach (ServiceTypeItem serviceTypeItem in list)
            {
                if (!catelogue.lstServiceTypeItems.Exists((ServiceTypeItem f) => f.stockName == serviceTypeItem.stockName))
                {
                    catelogue.lstServiceTypeItems.Add(new ServiceTypeItem()
                    {
                        stockName = serviceTypeItem.stockName,
                        itemCourseCode = serviceTypeItem.stockCode,
                        itemCoursePrice = serviceTypeItem.itemPrice,
                        itemType = "COURSE",
                        itemBoth = false
                    });
                }
                else
                {
                    ServiceTypeItem serviceTypeItem1 = catelogue.lstServiceTypeItems.SingleOrDefault<ServiceTypeItem>((ServiceTypeItem f) => f.stockName == serviceTypeItem.stockName);
                    serviceTypeItem1.itemSingleCode = serviceTypeItem1.stockCode;
                    serviceTypeItem1.itemSinglePrice = serviceTypeItem1.itemPrice;
                    serviceTypeItem1.itemCourseCode = serviceTypeItem.stockCode;
                    serviceTypeItem1.itemCoursePrice = serviceTypeItem.itemPrice;
                    serviceTypeItem1.itemType = "BOTH";
                    serviceTypeItem1.itemBoth = true;
                }
            }
            return this.PartialView("_PartialServiceTypeItem", catelogue);
        }

        //[HttpPost]
       

        //public ActionResult GetServiceType(string serviceID)
        //{
        //    Catelogue catelogue = new Catelogue();
        //    app.bsms.api.Service.Parameters.Clear();
        //    app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
        //    app.bsms.api.Service.Parameters.Add("departmentCode", serviceID);
        //    catelogue.lstServiceTypes = app.bsms.api.Service.GetList<ServiceType>("serviceType");
        //    return this.PartialView("_PartialServiceType", catelogue);
        //}



        //public ActionResult GetServiceType1()
        //{
        //    Catelogue catelogue = new Catelogue();
        //    try
        //    {
        //        app.bsms.api.Service.Parameters.Clear();
        //        app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
        //        catelogue.lstServiceTypes1 = app.bsms.api.Service.GetList<ServiceType>("serviceType");
        //        //catelogue.lstBrands = app.bsms.api.Service.GetList<Brand>("Brand");
        //        //catelogue.lstVouchers = app.bsms.api.Service.GetList<Brand>("voucherBrand");
        //        //catelogue.lstPrepaids = app.bsms.api.Service.GetList<Brand>("prepaidBrand");
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //    return base.View(catelogue);
        //}

        public ActionResult Index()
        {
            Catelogue catelogue = new Catelogue();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                
                catelogue.lstServices = app.bsms.api.Service.GetList<app.bsms.Models.Catelogue.Service>("department");              
                catelogue.lstBrands = app.bsms.api.Service.GetList<Brand>("Brand");
                
                catelogue.lstVouchers = app.bsms.api.Service.GetList<Brand>("voucherBrand");
                catelogue.lstPrepaids = app.bsms.api.Service.GetList<Brand>("prepaidBrand");
               


            }
            catch (Exception exception)
            {
                throw exception;
            }
            return base.View(catelogue);
        }
    }
}