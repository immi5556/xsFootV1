var vMethod = 1;

$(document).ready(function () {

    $('#StudentTableContainer').jtable({
        title: 'Add Cart List',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'ServiceID asc',
        actions: {
            listAction: '../AddToCart/AddCartList'
        },
        fields: {
            ServiceID: {
                width: '5%',
                key: true,
                create: false,
                edit: false,
                del: false,
                list: true
            },
            Treatment: {
                title: 'Treatment',
                width: '12%',
                list: true
            },
            Single: {
                title: 'Single',
                width: '12%',
                list: true
            },
            IsCourse: {
                title: 'IsCourse',
                width: '23%'
            },
            Deposit: {
                title: 'Deposit',
                list: true
            },
            Discount: {
                title: 'Discount ',
                width: '13%',
                display: function (data) {
                    if (data.record) {
                        return '<img title="Add Discount" style="cursor:pointer;width:25px" onclick=\"getDiscount(' + data.record.ServiceID + ')\" src="~/img/MenuButton/otc2/Octagon-Staff.png" /">';
                    }
                }
            },
            UnitPrice: {
                title: 'UnitPrice (RM)',
                width: '8%'
            },
            FOC: {
                title: 'FOC (RM)',
                width: '9%'
            },
            TotalAmount: {
                title: 'TotalAmount (RM)',
                width: '9%'
            },
            IsActive: {
                title: 'Status',
                width: '10%',
                type: 'checkbox',
                values: { '0': 'Inactive', '1': 'Active', '2': 'Pending Procedure' },
                defaultValue: '1'
            },
            CustomAction: {
                title: '',
                width: '1%',
                sorting: false,
                create: false,
                edit: false,
                list: true,
                display: function (data) {
                    if (data.record) {
                        return '<img title="MutliStaff" style="cursor:pointer;width:25px" onclick=\"getUpdate(' + data.record.ServiceID + ')\" src="~/img/MenuButton/oct2/Octagon-Staff.png" /">';
                    }
                }
            },
            CustomAction1: {
                title: '',
                width: '1%',
                sorting: false,
                create: false,
                edit: false,
                list: true,
                display: function (data) {
                    if (data.record) {
                        return '<img title="MutliSalesStaff" style="cursor:pointer;width:25px" onclick=\"getSalesUpdate(' + data.record.ServiceID + ')\" src="~/img/MenuButton/oct2/Octagon-Staff.png" /">';
                    }
                }
            },
            CustomAction2: {
                title: '',
                width: '1%',
                sorting: false,
                create: false,
                edit: false,
                list: true,
                display: function (data) {
                    if (data.record) {
                        return '<img title="Remove" style="cursor:pointer;width:25px" onclick=\"getdelete(' + data.record.ServiceID + ')\" src="~/img/MenuButton/oct2/delete.png" /">';
                    }
                }
            }
        }
    });

    //Load student list from server
    $('#StudentTableContainer').jtable('load');


    $('#StudentTableContainer1').jtable({
        title: 'Catalogue List',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'ServiceID asc',
        actions: {
            listAction: '../AddToCart/AddCartList'
        },
        fields: {
            ServiceID: {
                width: '5%',
                key: true,
                create: false,
                edit: false,
                del: false,
                list: true
            },
            Treatment: {
                title: 'Item Name',
                width: '12%',
                list: true
            },
            Single: {
                title: 'Single',
                width: '12%',
                list: false
            },
            IsCourse: {
                title: 'IsCourse',
                width: '23%',
                list: false
            },
            Deposit: {
                title: 'Deposit',
                list: false
            },
            TotalAmount: {
                title: 'Price (RM / SGD)',
                width: '9%'
            },
            Discount: {
                title: 'Single ',
                width: '13%',
                display: function (data) {
                    if (data.record) {
                        return '<button type="button" title="Add to cart" style="border: 1px solid #B0A478;background-color: #E3D595;color:black" class="btn btn-primary btn-embossed" id="btnSales">Single</button>';
                    }
                }
            },
            UnitPrice: {
                title: 'Course ',
                width: '13%',
                display: function (data) {
                    if (data.record) {
                        return '<button type="button" title="Course" onclick=\"getCourseUpdate(' + data.record.ServiceID + ')\" style="border: 1px solid #B0A478;background-color: #E3D595;color:black" class="btn btn-primary btn-embossed" id="btnSales">Course</button>';
                    }
                }
            },
            FOC: {
                title: 'FOC (RM)',
                width: '9%',
                list: false
            },

            IsActive: {
                title: 'Status',
                width: '10%',
                type: 'checkbox',
                values: { '0': 'Inactive', '1': 'Active', '2': 'Pending Procedure' },
                defaultValue: '1',
                list: false
            },
            CustomAction: {
                title: '',
                width: '1%',
                sorting: false,
                create: false,
                edit: false,
                list: false,
                display: function (data) {
                    if (data.record) {
                        return '<button type="button" title="Searcg Items" style="border: 1px solid #B0A478;background-color: #E3D595;color:black" class="btn btn-primary btn-embossed" id="btnSales"><i class="fa fa-plus"></i>Single</button>';
                    }
                }
            },
            CustomAction1: {
                title: '',
                width: '1%',
                sorting: false,
                create: false,
                edit: false,
                list: false,
                display: function (data) {
                    if (data.record) {
                        return '<img title="MutliSalesStaff" style="cursor:pointer;width:25px" onclick=\"getSalesUpdate(' + data.record.ServiceID + ')\" src="~/img/MenuButton/oct2/Octagon-Staff.png" /">';
                    }
                }
            },
            CustomAction2: {
                title: '',
                width: '1%',
                sorting: false,
                create: false,
                edit: false,
                list: false,
                display: function (data) {
                    if (data.record) {
                        return '<img title="Remove" style="cursor:pointer;width:25px" onclick=\"getdelete(' + data.record.ServiceID + ')\" src="~/img/MenuButton/oct2/delete.png" /">';
                    }
                }
            }
        }
    });

    //Load student list from server
    $('#StudentTableContainer1').jtable('load');
});

//Select Sales
$('#btnSales').click(function () {
    //('#formProject')[0].reset();

    $('#SalesModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
});

//Select Customers
$('#btnCustomer').click(function () {
    //('#formProject')[0].reset();

    $('#createCustomerModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
});

//UPDATE 
function getUpdate(ServiceID) {

    $('#SalesModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
}

//UPDATE 
function getSalesUpdate(ServiceID) {

    $('#SalesModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
}

//UPDATE 
function getdelete(ServiceID) {

    alert("Are You Sure to delete?");
}
//getDiscount

function getDiscount(ServiceID) {

    $('#DiscountModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
}

function getTotalDiscountModal(ServiceID) {

    $('#TotalDiscountModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
}

function getCourseUpdate(ServiceID) {

    $('#CourseUpdateModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
}

$('#Ttotaldiscount').on('input', function () {

    $('#TotalDiscountModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
});
jQuery('#Ttotaldiscount').on('input', function () {
    // do your stuff
    $('#TotalDiscountModal').modal({
        show: true,
        backdrop: 'static',
        keyboard: true
    });
});

function getsearchresult() {
    var divrowfour = document.getElementById('rowfour');

    //// hide
    //divrowfour.style.visibility = 'hidden';
    //// OR
    //divrowfour.style.display = 'none';

    // show
    divrowfour.style.visibility = 'visible';
    // OR
    divrowfour.style.display = 'block';

    var divrowone = document.getElementById('rowone');
    var divrowtwo = document.getElementById('rowtwo');
    var divrowthree = document.getElementById('rowthree');
    divrowone.style.display = 'none';
    divrowtwo.style.display = 'none';
    divrowthree.style.display = 'none';
}

function getServiceresult() {
    var divrowfour = document.getElementById('rowfour');
    var divrowone = document.getElementById('rowone');
    var divrowtwo = document.getElementById('rowtwo');
    var divrowthree = document.getElementById('rowthree');

    divrowone.style.display = 'block';
    divrowone.style.visibility = 'visible';
    divrowfour.style.display = 'none';
    divrowfour.style.visibility = 'hidden';
    divrowtwo.style.display = 'none';
    divrowtwo.style.visibility = 'hidden';
    divrowthree.style.display = 'none';
    divrowthree.style.visibility = 'hidden';
    // ProductCompleteHidden();
}


function getServicetyperesult(serviceid) {
    var divrowfour = document.getElementById('rowfour');
    var divrowone = document.getElementById('rowone');
    var divrowtwo = document.getElementById('rowtwo');
    var divrowthree = document.getElementById('rowthree');

    divrowone.style.display = 'block';
    divrowone.style.visibility = 'visible';
    divrowfour.style.display = 'none';
    divrowfour.style.visibility = 'hidden';
    divrowtwo.style.display = 'block';
    divrowtwo.style.visibility = 'visible';
    divrowthree.style.display = 'none';
    divrowthree.style.visibility = 'hidden';
    ProductCompleteHidden();
    //alert(serviceid);

    $.ajax({ //Not found in cache, get from server
        url: '/Service/ServiceType?sServiceID=' + serviceid,
        type: 'POST',
        async: false,
        success: function (data) {
            //debugger;

            $('#divPartialViewContainer').html(data);
            return;


        }
    });
    //call partial view
    //$.ajax({ //Not found in cache, get from server
    //    url: '/Service/ServiceType?sServiceID=' + serviceid,
    //    type: 'POST',
    //    dataType: 'json',
    //    async: false,
    //    success: function (data) {
    //        if (data.Result != 'OK') {

    //            return;
    //        }
    //        options = data.Options;
    //    }
    //});


}
function getServicetyperesult1(serviceid) {
    var divrowfour = document.getElementById('rowfour');
    var divrowone = document.getElementById('rowone');
    var divrowtwo = document.getElementById('rowtwo');
    var divrowthree = document.getElementById('rowthree');

    divrowone.style.display = 'block';
    divrowone.style.visibility = 'visible';
    divrowfour.style.display = 'none';
    divrowfour.style.visibility = 'hidden';
    divrowtwo.style.display = 'block';
    divrowtwo.style.visibility = 'visible';
    divrowthree.style.display = 'block';
    divrowthree.style.visibility = 'visible';
    ProductCompleteHidden();
    $.ajax({ //Not found in cache, get from server
        url: '/Service/ServiceTypeItem?sServiceID=' + serviceid,
        type: 'POST',
        async: false,
        success: function (data) {
            //debugger;

            $('#divPartialViewItemContainer').html(data);
            return;


        }
    });
}

function AddPreCart(stockName, stockCode, itemPrice) {
    //alert('si');

    $(this).css('background-color', 'red');


    $.ajax({ //Not found in cache, get from server
        url: '/Service/PreAddCartItem?sStockCode=' + stockCode + '&sStockName=' + stockName + '&sitemPrice=' + itemPrice,
        type: 'POST',
        async: false,
        success: function (data) {
            // debugger;

            $('#divPartialViewPreTransactionContainer').html(data);
            return;


        }
    });
}


function ServiceCompleteHidden() {

    var divrowone = document.getElementById('rowone');
    var divrowtwo = document.getElementById('rowtwo');
    var divrowthree = document.getElementById('rowthree');
    var divrowfour = document.getElementById('rowfour');

    divrowone.style.display = 'none';
    divrowone.style.visibility = 'hidden';
    divrowfour.style.display = 'none';
    divrowfour.style.visibility = 'hidden';
    divrowtwo.style.display = 'none';
    divrowtwo.style.visibility = 'hidden';
    divrowthree.style.display = 'none';
    divrowthree.style.visibility = 'hidden';
}

function ProductCompleteHidden() {

    var divrowone = document.getElementById('rowPone');
    var divrowtwo = document.getElementById('rowPtwo');
    var divrowthree = document.getElementById('rowPthree');

    divrowone.style.display = 'none';
    divrowone.style.visibility = 'hidden';

    divrowtwo.style.display = 'none';
    divrowtwo.style.visibility = 'hidden';
    divrowthree.style.display = 'none';
    divrowthree.style.visibility = 'hidden';
}

function getProductresult() {
    var divrowone = document.getElementById('rowPone');
    //divrowone.style.display = 'block';
    divrowone.style.visibility = 'visible';
    ServiceCompleteHidden();
}

function getProducttyperesult(brandCode) {
    var divrowfour = document.getElementById('rowfour');
    var divrowone = document.getElementById('rowPone');
    var divrowtwo = document.getElementById('rowPtwo');
    var divrowthree = document.getElementById('rowPthree');

    divrowone.style.display = 'block';
    divrowone.style.visibility = 'visible';
    divrowfour.style.display = 'none';
    divrowfour.style.visibility = 'hidden';
    divrowtwo.style.display = 'block';
    divrowtwo.style.visibility = 'visible';
    divrowthree.style.display = 'none';
    divrowthree.style.visibility = 'hidden';
    ServiceCompleteHidden();
    $.ajax({ //Not found in cache, get from server
        url: '/Service/ProductType?sbrandCode=' + brandCode,
        type: 'POST',
        async: false,
        success: function (data) {
            //debugger;

            $('#divPartialProductTypeViewContainer').html(data);
            return;


        }
    });

}
function getProducttyperesult1(srangeCode) {
    var divrowfour = document.getElementById('rowfour');
    var divrowone = document.getElementById('rowPone');
    var divrowtwo = document.getElementById('rowPtwo');
    var divrowthree = document.getElementById('rowPthree');

    divrowone.style.display = 'block';
    divrowone.style.visibility = 'visible';
    divrowfour.style.display = 'none';
    divrowfour.style.visibility = 'hidden';
    divrowtwo.style.display = 'block';
    divrowtwo.style.visibility = 'visible';
    divrowthree.style.display = 'block';
    divrowthree.style.visibility = 'visible';
    ServiceCompleteHidden();
    $.ajax({ //Not found in cache, get from server
        url: '/Service/ProductItemType?srangeCode=' + srangeCode,
        type: 'POST',
        async: false,
        success: function (data) {
            //debugger;

            $('#divPartialTypeItemsViewContainer').html(data);
            return;


        }
    });
}

function PayCash(sPaymentType) {

    alert('dfdfdfdfd');
    
    
    var txtAmount = document.getElementById("txtAmount").value;
    $.ajax({ //Not found in cache, get from server
        url: '/Payment/PrePayment?sPaymentType=' + sPaymentType + '&txtAmount=' + txtAmount,
        type: 'POST',
        async: false,
        success: function (data) {
            debugger;

            $('#divPartialPaymentViewContainer').html(data);
            return;


        }
    });
}

function DeletePreTransaction(iItemCode) {
    var user_choice = window.confirm('Would you like to continue?');


    if (user_choice == true) {


        $.ajax({ //Not found in cache, get from server
            url: '/Service/PreAddCartDeleteItem?siItemCode=' + iItemCode,
            type: 'POST',
            async: false,
            success: function (data) {
                //debugger;

                $('#divPartialViewPreTransactionContainer').html(data);
                return;


            }
        });


    } else {


        return false;


    }


}


$("#checkedAll").change(function () {
    if (this.checked) {
        $(".checkSingle").each(function () {
            this.checked = true;
        });
    } else {
        $(".checkSingle").each(function () {
            this.checked = false;
        });
    }
});

$(".checkSingle").click(function () {
    if ($(this).is(":checked")) {
        var isAllChecked = 0;

        $(".checkSingle").each(function () {
            if (!this.checked)
                isAllChecked = 1;
        });

        if (isAllChecked == 0) {
            $("#checkedAll").prop("checked", true);
        }
    }
    else {
        $("#checkedAll").prop("checked", false);
    }
});

$('#bootstrap-data-tableCustomer').DataTable();

