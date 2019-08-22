if (!isIE()) {
    //Yoonus Block
    //var zoom = $(window).width() / $("body").width() * 75;
    //document.body.style.zoom = zoom + "%"
}

function ajaxSuccess(data) {
    window.location.href = data;
}

function ajaxError(data) {
    //window.location.href = data;
}

function showCustomerModal(customer) {
    $.ajax({
        url: "/Cart/GetCustomerModal",
        data: { customer: customer },
        type: "GET",
        async: false,
        dataType: "html",
        contentType: "application/json",
        success: function (data) {
            $("#cust-srch-contain").html(data);
            //$(window).scrollTop($('#divServiceType').offset().top);
            

        }
    });
    //contentType: "application/x-www-form-urlencoded",
}

function showServiceTypes(id) {
    $.ajax({
        url: "/Service/GetServiceType",
        data: { serviceID: id },
        type: "GET",
        async: false,
        dataType: "html",
        contentType: "application/json",
        success: function (data) {
            $("#divServiceType").html(data);
            $(window).scrollTop($('#divServiceType').offset().top);
        }
    });
    //contentType: "application/x-www-form-urlencoded",
}

function searchServiceTypes() {
    $.ajax({
        url: "/Service/GetServiceSearch",
        data: { text: $("#txtsearchitems").val() },
        type: "GET",
        async: false,
        dataType: "html",
        contentType: "application/json",
        success: function (data) {
            $("#divServiceItemType").html(data);
            //$(window).scrollTop($('#divServiceItemType').offset().top);
        }
    });
    //contentType: "application/x-www-form-urlencoded",
}

function showServiceItemTypes(id) {
    $.ajax({
        url: "/Service/GetServiceTypeItem",
        data: { typeID: id },
        type: "GET",
        async: false,
        dataType: "html",
        contentType: "application/json",
        success: function (data) {
            $("#divServiceItemType").html(data);
            $(window).scrollTop($('#divServiceItemType').offset().top);
        }
    });
    //contentType: "application/x-www-form-urlencoded",
}

function stringifyCart() {
    $.ajax({
        url: document.location.protocol + "//" + document.location.host + "/Cart/RefreshCartDetails",
        data: {},
        type: "GET",
        async: false,
        contentType: "application/json",
        success: function (data) {
            $('#cart_details').val(data);
        }
    });

}

function stringifyRecallCart(transactionNumber) {
    $.ajax({
        url: document.location.protocol + "//" + document.location.host + "/Cart/RecallCartDetails",
        data: { transactionNumber: transactionNumber },
        type: "GET",
        async: false,
        contentType: "application/json",
        success: function (data) {
            $('#cart_details').val(data);
        }
    });

}

function isIE() {
    ua = navigator.userAgent;
    /* MSIE used to detect old browsers and Trident used to newer ones*/
    var is_ie = ua.indexOf("MSIE ") > -1 || ua.indexOf("Trident/") > -1;

    return is_ie;
}

$(function () {
    $(window).on('resize', function () {
        //Yoonus Block
        //if (!isIE()) {
        //    var zoom = $(window).width() / $("body").width() * 75;
        //    document.body.style.zoom = zoom + "%"
        // }
    });

    var container = $('.body-row form').length > 0 ? $('.body-row form').parent() : "body";

    $('.datepicker').datepicker({
        format: 'yyyy-mm-dd',
        container: container,
        todayHighlight: true,
        autoclose: true
    });

    $('[data-toggle=print]').click(function () {
        var myStyle = '<link href="/css/bootstrap/bootstrap.css" rel="stylesheet" />' +
            '<link href="/css/font-awesome/font-awesome.css" rel="stylesheet" />' +
            '<link href="/css/site.css" rel="stylesheet" />';

        var mywindow = window.open('', 'new div', 'height=auto,width=600');
        mywindow.document.write('<html><head><title></title>');
        mywindow.document.write(myStyle + '</head><body>');
        mywindow.document.write($('#' + $(this).attr("data-target")).html());
        mywindow.document.write('</body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () { mywindow.print(); mywindow.close(); }, 1000);
        return true;
    });

    $('[data-toggle=email]').click(function () {
        var content = '<html><head><title></title><link href="/css/bootstrap/bootstrap.css" rel="stylesheet" />' +
            '<link href="/css/font-awesome/font-awesome.css" rel="stylesheet" />' +
            '<link href="/css/site.css" rel="stylesheet" /></head><body>' + $('#' + $(this).attr("data-content")).html() + '</body></html>';

        $.ajax({
            url: $(this).attr("data-url"),
            data: JSON.stringify({ email: $(this).attr("data-target"), sub: $(this).attr("data-subject"), msg: content }),
            type: "POST",
            //async: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            cache: false,
            success: function (data) {
                alert(data)
            }
        });
    });
    // Collapse click
    $('[data-toggle=sidebar-collapse]').click(function () {
        $('.menu').toggleClass('d-none menu-collapsed', 200);
        $('.submenu').toggleClass('d-none sidebar-submenu', 200);
        $('.submenu').toggleClass('submenu-icon', 200);
        $('.sidebar').toggleClass('sidebar-collapsed sidebar-expanded', 200);

        // Treating d-flex/d-none on separators with title
        var SeparatorTitle = $('.sidebar-separator-title');
        if (SeparatorTitle.hasClass('d-flex')) {
            SeparatorTitle.removeClass('d-flex');
        } else {
            SeparatorTitle.addClass('d-flex');
        }

        // Collapse/Expand icon
        $('#collapse-icon').toggleClass('fa-angle-double-left fa-angle-double-right');

        if ($('#collapse-icon').attr('class').indexOf('fa-angle-double-right') != -1) {
            $('#sidebar').toggleClass('', 200);
            $('#main').toggleClass('col-10 ml-auto col-11 ml-6', 200)
        }
        else {
            $('#sidebar').toggleClass('col-2', 200);
            $('#main').toggleClass('col-11 ml-6 col-10 ml-auto', 200)
        }
    });

    //$('[data-toggle=sidebar-collapse]').click();

    $(document).ready(function () {
        if (document.location.pathname.indexOf("/Home/Login/") == -1)
            stringifyCart();

        var $cartBadge = $(".icon-cart-badge");

        if ($('#cart_details').val() == "" || $('#cart_details').val() == undefined || $('#cart_details').val() == "null")
            $cartBadge.text(0);
        else
            $cartBadge.text(JSON.parse($('#cart_details').val()).length);

        $('.movable-image').cropper({
            viewMode: 1,
            dragMode: 'move',
            autoCropArea: 0.65,
            restore: false,
            cropBoxMovable: false,
            cropBoxResizable: false,
            zoomOnWheel: false
        });

        $(".live-search-list li").each(function () {
            $(this).attr("data-search-term", $(this).text().toLowerCase());
        });

        $(document).on("input propertychange paste", "#referenceCode", function () {
            $("#customerCode").val($(this).val());
        });

        $("#divCourseExpiryLimit").hide();
        $("#divFlexiItems").hide();

        if ($('input[name=courseType]:checked').val() != undefined) {
            $("input[name='cartDetails.parent.courseType']").val($('input[name=courseType]:checked').val());

            if ($('input[name=courseType]:checked').val() == "N") {
                $("#divCourseExpiryLimit").hide();
                $("#divFlexiItems").hide();
                $("input[name='totalTreatments']").removeAttr('disabled');
            }
            else if ($('input[name=courseType]:checked').val() == "FFi") {
                $("#divCourseExpiryLimit").show();
                $("#divFlexiItems").show();
                $("input[name='totalTreatments']").attr("disabled", "disabled");
            }
            else {
                $("#divCourseExpiryLimit").show();
                $("#divCourseExpiryLimit").hide();
                $("input[name='totalTreatments']").attr("disabled", "disabled");
            }
        }

        if ($('#cartDetails_isFOC').is(':checked'))
            $("#divFOC").show();
        else
            $("#divFOC").hide();

        $("#show_hide_clientcode span").on("click", function (event) {
            event.preventDefault();
            if ($("#show_hide_clientcode input").attr("type") == "text") {
                $("#show_hide_clientcode input").attr("type", "password");
                $("#show_hide_clientcode #clishowhide").addClass("fa-eye-slash");
                $("#show_hide_clientcode #clishowhide").removeClass("fa-eye");
            } else if ($("#show_hide_clientcode input").attr("type") == "password") {
                $("#show_hide_clientcode input").attr("type", "text");
                $("#show_hide_clientcode #clishowhide").removeClass("fa-eye-slash");
                $("#show_hide_clientcode #clishowhide").addClass("fa-eye");
            }
        });

        $("#show_hide_password span").on("click", function (event) {
            event.preventDefault();
            if ($("#show_hide_password input").attr("type") == "text") {
                $("#show_hide_password input").attr("type", "password");
                $("#show_hide_password #pishowhide").addClass("fa-eye-slash");
                $("#show_hide_password #pishowhide").removeClass("fa-eye");
            } else if ($("#show_hide_password input").attr("type") == "password") {
                $("#show_hide_password input").attr("type", "text");
                $("#show_hide_password #pishowhide").removeClass("fa-eye-slash");
                $("#show_hide_password #pishowhide").addClass("fa-eye");
            }
        });

        $(document).on("click", "#btnCapture", function () {
            $('#mdlCapture').modal('toggle')
            LayoutApp.init();
        });

        //$(document).on("click", "#btnCapture", function () {
        //    $('#mdlCapture').modal('toggle')
        //    LayoutApp.init();
        //});

        $(document).on("click", "#btnCaptureClose", function () {
            $('#mdlCapture').modal('toggle')
            LayoutApp.close();
        });

        //btn-top-up
        $(document).on("click", ".btn-top-up", function () {
            var referenceTreatmentCode = '';
            var arr;

            $('#callBy').val('');

            if ($(this).attr("data-line-type") == "TP SERVICE") {
                var item = {
                    //staffcode: $("input[name='redeem.customerCode']").val(),
                    //unitPrice: parseFloat($(this).attr("data-price")),
                    staffcode: $(this).attr("data-customer"),
                    lineStatus: $(this).attr("data-line-status"),
                    lineType: $(this).attr("data-line-type"),
                    itemCode: $(this).attr("data-code"),
                    itemName: $(this).attr("data-name"),
                    itemQty: parseInt($(this).attr("data-quantity")),
                    unitPrice: parseFloat($("input[name='topupAmount']").val()),
                    topupBalance: parseFloat($(this).attr("data-price")) + parseFloat($("input[name='topupAmount']").val()),
                    topupOutstanding: parseFloat($(this).attr("data-outstanding")),
                    //topupOutstanding: parseFloat($(this).attr("data-price1")) - parseFloat($("input[name='topupAmount']").val()),
                    referenceTransactionNumber: $(this).attr("data-transactionNumber"),
                    treatmentCode: $(this).attr("data-treatmentCode"),
                    referenceTreatmentCode: $(this).attr("data-treatmentParentCode"),
                    subItemCode: "asd"
                };

                $.ajax({
                    url: "/Redemption/TopupItem",
                    type: "POST",
                    data: item,
                    success: function (res) {
                        res = JSON.parse(res);

                        if (res.status == 0) {
                            $("#mdlAlerts").modal({ backdrop: 'static', keyboard: false, show: true });
                            $("#alert-message").html(res.message);

                            return false;
                        }
                        else {
                            stringifyCart();
                            window.location.href = document.location.href;
                        }
                        return true;
                    }
                });
            }
            else {
                $cartBadge.text(parseInt($cartBadge.text()) + 1);

                if ($('#cart_details').val() == "" || $('#cart_details').val() == undefined || $('#cart_details').val() == "null")
                    arr = [];
                else
                    arr = JSON.parse($('#cart_details').val());

                var data = {
                    lineStatus: $(this).attr("data-line-status"),
                    lineType: $(this).attr("data-line-type"),
                    itemCode: $(this).attr("data-code"),
                    itemName: $(this).attr("data-name"),
                    itemQty: parseInt($(this).attr("data-quantity")),
                    unitPrice: parseFloat($(this).attr("data-price")),
                    referenceTreatmentCode: ''
                };

                arr.push(data);

                $('#cart_details').val(JSON.stringify(arr));
            }

        });

        $(document).on("click", ".btn-add-cart", function () {

            var referenceTreatmentCode = '';
            var arr;

            $('#callBy').val('');

            if ($(this).attr("data-line-type") == "TD") {

                var item = {
                    staffcode: $("input[name='redeem.customerCode']").val(),
                    lineStatus: $(this).attr("data-line-status"),
                    lineType: $(this).attr("data-line-type"),
                    itemCode: $(this).attr("data-code"),
                    itemName: $(this).attr("data-name"),
                    itemQty: parseInt($(this).attr("data-quantity")),
                    unitPrice: parseFloat($(this).attr("data-price")),
                    treatmentCode: $(this).attr("data-treatmentCode"),
                    referenceTreatmentCode: $(this).attr("data-treatmentParentCode"),
                    subItemCode: "asd"
                };

                $.ajax({
                    url: "/Redemption/RedeemItem",
                    type: "POST",
                    data: item,
                    success: function (res) {
                        res = JSON.parse(res);

                        if (res.status == 0) {
                            $("#mdlAlerts").modal({ backdrop: 'static', keyboard: false, show: true });
                            $("#alert-message").html(res.message);

                            return false;
                        }
                        else {
                            stringifyCart();
                            window.location.href = document.location.href;
                        }
                        return true;
                    }
                });
            }
            else {

                //$cartBadge.text(parseInt($cartBadge.text()) + 1);

                if ($('#cart_details').val() == "" || $('#cart_details').val() == undefined || $('#cart_details').val() == "null")
                    arr = [];
                else
                    arr = JSON.parse($('#cart_details').val());

                var data = { 
                    lineStatus: $(this).attr("data-line-status"),
                    lineType: $(this).attr("data-line-type"),
                    itemCode: $(this).attr("data-code"),
                    itemName: $(this).attr("data-name"),
                    itemQty: parseInt($(this).attr("data-quantity")),
                    unitPrice: parseFloat($(this).attr("data-price")),
                    referenceTreatmentCode: ''
                };
                
                var iflg;
                iflg = 0;
                for (var i = 0; i < arr.length; i++)
                    {   
                        if (data.itemCode == arr[i].itemCode) {
                            iflg = 1;
                            break;}
                    
                    }
                    if (iflg == 0)
                    {
                        arr.push(data);
                        $('#cart_details').val(JSON.stringify(arr));
                        $cartBadge.text(parseInt($cartBadge.text()) + 1);
                    }

                    
                
            }

            if ($(this).attr("data-line-type") == "COURSE" || $(this).attr("data-line-type") == "PREPAID") {
                $('#callBy').val($(this).attr("data-line-type"));

                var itemCode = $(this).attr("data-code");
                var lineType = $(this).attr("data-line-type");

                var openPrepaid = "false";

                if ($(this).attr("data-open-prepaid") != undefined)
                    openPrepaid = $(this).attr("data-open-prepaid").toLowerCase();

                $.ajax({
                    data: $("#frmAddToCart").serialize(),
                    type: "POST",
                    dataType: "json",
                    url: "/Service/AddToCart/",
                    success: function (data) {
                        if (lineType == "PREPAID")
                            if (openPrepaid == "true")
                                window.location.href = "/Cart/Preset" + lineType.toLowerCase() + "/" + itemCode;
                            else
                                window.location.href = "/Cart/Index";
                        else
                            window.location.href = "/Cart/Preset" + lineType.toLowerCase() + "/" + itemCode;
                    }
                });
            }
        });

        //$(document).on("click", "#btnvoidYes", function () {
        //    var referenceTreatmentCode = '';
        //    var arr;

        //    $('#callBy').val('');
        //    var $this = $(".btn-void");

        //    if ($this.attr("data-line-status") == "VT") {
        //        var item = {
        //            staffcode: $this.attr("data-customer"),
        //            lineStatus: $this.attr("data-line-status"),
        //            lineType: $this.attr("data-line-type"),
        //            itemCode: $this.attr("data-code"),
        //            itemName: $this.attr("data-name"),
        //            itemQty: parseInt($this.attr("data-quantity")),
        //            unitPrice: parseFloat($this.attr("data-price")),
        //            treatmentCode: $this.attr("data-treatmentCode"),
        //            referenceTreatmentCode: $this.attr("data-treatmentParentCode"),
        //            transactionNumber: $this.attr("data-transaction"),
        //            invoiceNumber: $this.attr("data-invoice"),
        //            subItemCode: "asd",
        //            username: $("input[name='username']").val(),
        //            password: $("input[name='password']").val(),
        //            logdet: "void"

        //        };

        //        $.ajax({
        //                                    url: "/Redemption/RecallTransaction",
        //                        type: "POST",
        //                            data: item,
        //                                success: function (res) {
        //                                    if (res.status == 0) {
        //                                        $("#mdlAlerts").modal({ backdrop: 'static', keyboard: false, show: true });
        //                                        $("#alert-message").html('Unable to void invoice ' + $this.attr("data-invoice"));
        //                                        return false;
        //                                    }
        //                                    else {
        //                                        window.location.href = "/BillOps/VoidInvoice/" + $this.attr("data-transaction") + "/" + $this.attr("data-invoice");
        //                                    }
        //                                    return true;
        //                                }
                           
        //        });
        //    }
        //});


        $(document).on("click", "#btnvoidYes", function () {
            var referenceTreatmentCode = '';
            var arr;
            $('#callBy').val('');
            var $this = $(".btn-void");


            var username = $("input[name='txtusername']").val();
            var password = $("input[name='txtpassword']").val();
            var logdet = "void";

            $.ajax({
                url: '/BillOps/CheckSecurity',
                data: { un: username, ps: password, logdet:logdet },
                success: function (data) {                   
                    Myvoidtrans();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    
                    alert(xhr.responseText);
                }
            });

            
        });

       
    

        function Myvoidtrans() {
            var referenceTreatmentCode = '';
            var arr;

            $('#callBy').val('');
            var $this = $(".btn-void");

            if ($this.attr("data-line-status") == "VT") {
                var item = {
                    staffcode: $this.attr("data-customer"),
                    lineStatus: $this.attr("data-line-status"),
                    lineType: $this.attr("data-line-type"),
                    itemCode: $this.attr("data-code"),
                    itemName: $this.attr("data-name"),
                    itemQty: parseInt($this.attr("data-quantity")),
                    unitPrice: parseFloat($this.attr("data-price")),
                    treatmentCode: $this.attr("data-treatmentCode"),
                    referenceTreatmentCode: $this.attr("data-treatmentParentCode"),
                    transactionNumber: $this.attr("data-transaction"),
                    invoiceNumber: $this.attr("data-invoice"),
                    subItemCode: "asd"

                };

                $.ajax({
                    url: "/Redemption/RecallTransaction",
                    type: "POST",
                    data: item,
                    success: function (res) {
                        if (res.status == 0) {
                            $("#mdlAlerts").modal({ backdrop: 'static', keyboard: false, show: true });
                            $("#alert-message").html('Unable to void invoice ' + $this.attr("data-invoice"));
                            return false;
                        }
                        else {
                            window.location.href = "/BillOps/VoidInvoice/" + $this.attr("data-transaction") + "/" + $this.attr("data-invoice");
                        }
                        return true;
                    }
                });
            }
        }

        $(document).on("click", ".btn-void", function () {
            $(".live-search-box").val('');
            $("#mdlvoidtrans").modal({ backdrop: 'static', keyboard: false, show: true });
        });

        $('[data-toggle=modal]').click(function () {
            //$(document).on("click", "#btnMdlCustomer", function () {
            $.ajax({
                url: $(this).attr("data-url"),
                data: { code: $(this).attr("data-code"), type: $(this).attr("data-value") },
                type: "GET",
                async: false,
                dataType: "html",
                contentType: "application/x-www-form-urlencoded",
                success: function (data) {
                    $("#divContent").html(data);

                    var form = $($(this).attr("data-target")).closest("form");
                    form.removeData('validator');
                    form.removeData('unobtrusiveValidation');
                    $.validator.unobtrusive.parse(form);
                    $(window).scrollTop($('#divContent').offset().top);
                }
            });
        });

        $('[data-toggle=clear]').click(function () {
            $.ajax({
                url: $(this).attr("data-url"),
                data: { id: $(this).attr("data-code"), id2: $(this).attr("data-value") },
                type: "GET",
                contentType: false,
                success: function (data) {
                    window.location.href = data;
                }
            });
        });

        $(document).on("click", "[data-fun=decrease]", function () {
            var $number = $('input[name="' + $(this).attr("data-target") + '"]');
            var value = parseInt($number.val(), 10);
            value = isNaN(value) ? 0 : value;

            var minVal;
            if ($(this).attr("data-zero") == "true")
                minVal = 0;
            else
                minVal = 1;

            value < minVal ? value = minVal : '';

            if (value == minVal) {
                $(this).addClass("disabled");
            }
            else
                value--;

            $number.val(value);

            if ($(this).attr("data-param") != '') {
                var $source = $('input[name="' + $(this).attr("data-param").split(',')[0].trim() + '"]');
                var $result = $('input[name="' + $(this).attr("data-param").split(',')[1].trim() + '"]');

                $result.val(value * parseInt(isNaN($source.val()) ? 0 : $source.val(), 10));
            }
        });

        $(document).on("click", "[data-fun=increase]", function () {
            var $number = $('input[name="' + $(this).attr("data-target") + '"]');
            var value = parseInt($number.val(), 10);
            value = isNaN(value) ? 0 : value;
            value < 1 ? value = 0 : '';
            value++;

            if (value > 0) {
                $('[data-fun=decrease]').removeClass("disabled");
            }

            $number.val(value);

            if ($(this).attr("data-param") != '') {
                var $source = $('input[name="' + $(this).attr("data-param").split(',')[0].trim() + '"]');
                var $result = $('input[name="' + $(this).attr("data-param").split(',')[1].trim() + '"]');

                $result.val(value * parseFloat(isNaN($source.val()) ? 0 : $source.val()));
            }
        });

        $(document).on("click", "[data-fun=range]", function () {
            var typ = $(this).attr("data-type");

            $.ajax({
                url: "/Service/GetRange",
                data: { type: $(this).attr("data-type"), code: $(this).attr("data-code") },
                type: "GET",
                async: false,
                dataType: "html",
                contentType: "application/x-www-form-urlencoded",
                success: function (data) {
                    
                    $("#div" + typ + "Range").html(data);
                    $(window).scrollTop($("#div" + typ + "Range").offset().top);
                }
            });
        });

        $(document).on("click", "[data-fun=itemtype]", function () {
            var typ = $(this).attr("data-type");

            $.ajax({
                url: "/Service/GetProductItemType",
                data: { type: $(this).attr("data-type"), code: $(this).attr("data-code") },
                type: "GET",
                async: false,
                dataType: "html",
                contentType: "application/x-www-form-urlencoded",
                success: function (data) {
                    
                    $("#div" + typ + "ItemType").html(data);
                    $(window).scrollTop($("#div" + typ + "ItemType").offset().top);
                }
            });
        });

        $('input[type=radio][name=courseType]').change(function () {
            $("input[name='cartDetails.parent.courseType']").val(this.value);

            if (this.value == "N") {
                $("#divCourseExpiryLimit").hide();
                $("#divFlexiItems").hide();
                $("input[name='totalTreatments']").val('');
                $("input[name='totalTreatments']").removeAttr('disabled');
            }
            else if (this.value == "FFi") {
                $("#divCourseExpiryLimit").show();
                if ($("input[name='cartDetails.isFirstTreatmentDone']").is(':checked'))
                    $("#divFlexiItems").show();
                else
                    $("#divFlexiItems").hide();
                $("input[name='totalTreatments']").val('');
                $("input[name='totalTreatments']").attr("disabled", "disabled");
            }
            else {
                $("#divCourseExpiryLimit").show();
                $("#divFlexiItems").hide();
                $("input[name='totalTreatments']").val('');
                $("input[name='totalTreatments']").attr("disabled", "disabled");
            }
        });

        $('#cartDetails_isFirstTreatmentDone').change(function () {
            if ($(this).is(":checked") && $("input[name='cartDetails.parent.courseType']").val() == "FFi")
                $("#divFlexiItems").show();
            else
                $("#divFlexiItems").hide();
        });

        $('#cartDetails_isFOC').change(function () {
            $("input[name='cartDetails.FOCReason']").val('');
            $("input[name='cartDetails.FOCQuantity']").val('0');

            if ($(this).is(":checked"))
                $("#divFOC").show();
            else
                $("#divFOC").hide();
        });

        $(document).on("click", "#btnAddTreatmentStaff", function () {
            var message = '';
            var per = 0;
            var amt = 0;

            if ($("select[name='cartDetails.firstTreatmentStaffCode'] option:selected").val().trim() == '')
                message += '<p>Valid Treatment StaffCode is required</p>';

            if (message.trim() != '') {
                $("#alert-message").html("<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">"
                    + message.trim() +
                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
                    "<span aria-hidden=\"true\">&times;</span>" +
                    "</button>" +
                    "</div>");

                return false;
            }

            var len = $('#tbltreatmentStaffs tr > td:contains(' + $("select[name='cartDetails.firstTreatmentStaffCode'] option:selected").val() + ') + td:contains(' + $("select[name='cartDetails.firstTreatmentStaffCode'] option:selected").text() + ')').length

            if (len == 0) {
                $('#tbltreatmentStaffs tbody').append(
                    "<tr class=\"row\">" +
                    "<td class=\"col-4\">" + $("select[name='cartDetails.firstTreatmentStaffCode'] option:selected").val() + "</td>" +
                    "<td class=\"col-6\">" + $("select[name='cartDetails.firstTreatmentStaffCode'] option:selected").text() + "</td>" +
                    "<td class=\"col-2\"><button type=\"button\" class=\"btn btn-flat\" data-mode=\"delete\"><i class=\"fa fa-trash fa-2x\"></i></button></td>" +
                    "</tr>");

                var itmArray = [];

                $('#tbltreatmentStaffs > tbody > tr').each(function () {
                    var data = {
                        staffCode: $(this).children(':first-child').text().trim(),
                        staffName: $(this).children(':nth-child(2)').text().trim()
                    };

                    itmArray.push(data);
                });

                $("select[name='cartDetails.firstTreatmentStaffCode']").val('');

                $("input[name='cartDetails.strTreatmentStaffs']").val(JSON.stringify(itmArray));
            }
        });

        $(document).on("click", "#btnAddStaff", function () {
            var message = '';
            var per = 0;
            var amt = 0;

            if ($("select[name='cartDetails.staffcode'] option:selected").val().trim() == '')
                message += '<p>Valid Staff is required</p>';
            /*
            if (parseFloat($("input[name='cartDetails.ratio']").val().trim()) <= 0 || isNaN(parseFloat($("input[name='cartDetails.ratio']").val().trim())))
                message += '<p>Valid distribution ratio is required</p>';
            */
            if (message.trim() != '') {
                $("#alert-message").html("<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">"
                    + message.trim() +
                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
                    "<span aria-hidden=\"true\">&times;</span>" +
                    "</button>" +
                    "</div>");

                return false;
            }

            var len = $('#tblStaffs tr > td:contains(' + $("select[name='cartDetails.staffcode'] option:selected").val() + ') + td:contains(' + $("select[name='cartDetails.staffcode'] option:selected").text() + ')').length;

            if (len == 0) {
                var ratio = parseFloat(100 / ($('#tblStaffs > tbody > tr').length + 1)).toFixed(2);

                $('#tblStaffs tbody').append(
                    "<tr class=\"row\">" +
                    "<td class=\"col-3\">" + $("select[name='cartDetails.staffcode'] option:selected").val() + "</td>" +
                    "<td class=\"col-5\">" + $("select[name='cartDetails.staffcode'] option:selected").text() + "</td>" +
                    "<td class=\"col-2\">" + ratio + "</td>" +
                    "<td class=\"col-2\"><button type=\"button\" class=\"btn btn-flat\" data-mode=\"delete\"><i class=\"fa fa-trash fa-2x\"></i></button></td>" +
                    "</tr>");

                $('#tblStaffs > tbody > tr').children(':nth-child(3)').text(ratio);

                var itmArray = [];

                $('#tblStaffs > tbody > tr').each(function () {
                    if (parseFloat($(this).children(':nth-child(3)').text().trim()) > 0) {
                        var data = {
                            staffCode: $(this).children(':first-child').text().trim(),
                            staffName: $(this).children(':nth-child(2)').text().trim(),
                            ratio: parseFloat($(this).children(':nth-child(3)').text().trim())
                        };

                        itmArray.push(data);
                    }
                });

                $("input[name='cartDetails.ratio']").val('0');
                $("select[name='cartDetails.staffcode']").val('');

                $("input[name='cartDetails.strStaffs']").val(JSON.stringify(itmArray));
            }
        });

        $('#discPercentage').change(function () {
            $('input[name=discType]').val('%');
        });

        $('#discPrice').change(function () {
            $('input[name=discType]').val('$');
        });

        $(document).on("click", "#btnAddDiscount", function () {
            var message = '';
            var per = 0;
            var amt = 0;

            if ($("input[name='discType']:checked").val().trim() == '')
                message += '<p>Valid Discount Type is required</p>';

            if (parseFloat($("input[name='discountPrice']").val().trim()) <= 0)
                message += '<p>Valid Discount Amount/Pecentage is required</p>';

            if ($("select[name='cartDetails.discountDescription'] option:selected").text().trim() == '')
                message += '<p>Valid Reason Code is required</p>';

            var itemAmount = 0;

            if ($("input[name='cartDetails.lineType']").val().trim() == "COURSE") {
                if ($('input[name=courseType]:checked').val() == "N") {
                    if (isNaN(parseInt($("input[name='totalTreatments']").val())))
                        message += '<p>Valid No. of Treatment is required</p>';
                    else if (parseInt($("input[name='totalTreatments']").val()) == 0)
                        message += '<p>Valid No. of Treatment is required</p>';
                    else
                        itemAmount = parseInt($("input[name='totalTreatments']").val()) * parseFloat($("input[name='cartDetails.unitPrice']").val());

                }
                else
                    itemAmount = parseFloat($('input[name="cartDetails.itemAmount"]').val());
            }
            else
                itemAmount = parseFloat($('input[name="cartDetails.itemAmount"]').val());

            if (message.trim() != '') {
                $("#alert-message").html("<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">"
                    + message.trim() +
                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
                    "<span aria-hidden=\"true\">&times;</span>" +
                    "</button>" +
                    "</div>");

                return false;
            }

            if ($("input[name='discType']:checked").val().trim() == "%") {
                per = parseFloat($("input[name='discountPrice']").val().trim());
                amt = parseFloat(itemAmount) * parseFloat($("input[name='discountPrice']").val().trim()) / 100;
            }
            else if ($("input[name='discType']:checked").val().trim() == "$") {
                per = 0;
                amt = parseFloat($("input[name='discountPrice']").val().trim());
            }

            var len = $('#tblDiscounts tr > td:contains(' + $("select[name='cartDetails.discountDescription'] option:selected").text() + ')').length

            if (len == 0) {
                $('#tblDiscounts tbody').append(
                    "<tr class=\"row\">" +
                    "<td class=\"col-1\">" + ($('#tblDiscounts > tbody > tr').length + 1) + "</td>" +
                    "<td class=\"col-2\">" + per + "</td>" +
                    "<td class=\"col-3\">" + amt + "</td>" +
                    "<td class=\"col-5\">" + $("select[name='cartDetails.discountDescription'] option:selected").text() + "</td>" +
                    "<td class=\"col-1\"><button type=\"button\" class=\"btn btn-flat\" data-mode=\"delete\"><i class=\"fa fa-trash fa-2x\"></i></button></td>" +
                    "</tr>");

                var itmArray = [];
                //var price = parseFloat($("input[name='cartDetails.unitPrice']").val().trim());
                //var qty = parseInt($("input[name='cartDetails.itemQty']").val().trim());
                var disc = 0;

                $('#tblDiscounts > tbody > tr').each(function () {
                    if (parseFloat($(this).children(':nth-child(3)').text().trim()) > 0) {
                        disc += parseFloat($(this).children(':nth-child(3)').text().trim());

                        var data = {
                            discountLineNumber: $(this).children(':first-child').text().trim(),
                            discountPercentage: parseInt($(this).children(':nth-child(2)').text().trim()),
                            discountAmount: parseFloat($(this).children(':nth-child(3)').text().trim()),
                            discountReason: $(this).children(':nth-child(4)').text().trim()
                        };

                        itmArray.push(data);
                    }
                });

                //$("input[name='discType']").val('%');
                $("input[name='discountPrice']").val('0');
                $("select[name='cartDetails.discountDescription']").val('');

                $("input[name='cartDetails.itemAmount']").val(itemAmount - disc);

                $("input[name='cartDetails.strDiscounts']").val(JSON.stringify(itmArray));
            }
        });

        $(document).on("click", "[data-mode=delete]", function () {
            var tblName = $(this).parent().parent().parent().parent()[0].id;

            $(this).parent().parent().remove();

            var itmArray = [];

            if (tblName == "tblStaffs") {
                $('#tblStaffs > tbody > tr').each(function () {
                    if (parseFloat($(this).children(':nth-child(3)').text().trim()) > 0) {
                        var data = {
                            staffCode: $(this).children(':first-child').text().trim(),
                            staffName: $(this).children(':nth-child(2)').text().trim(),
                            ratio: parseFloat($(this).children(':nth-child(3)').text().trim())
                        };

                        itmArray.push(data);
                    }
                });

                $("input[name='cartDetails.strStaffs']").val(JSON.stringify(itmArray));
            }
            else if (tblName == "tblDiscounts") {
                var price = parseFloat($("input[name='cartDetails.unitPrice']").val().trim());
                var qty = parseInt($("input[name='cartDetails.itemQty']").val().trim());
                var disc = 0;

                $('#tblDiscounts > tbody > tr').each(function () {
                    if (parseFloat($(this).children(':nth-child(3)').text().trim()) > 0) {
                        disc += parseFloat($(this).children(':nth-child(3)').text().trim());

                        var data = {
                            discountLineNumber: $(this).children(':first-child').text().trim(),
                            discountPercentage: parseInt($(this).children(':nth-child(2)').text().trim(), 10),
                            discountAmount: parseFloat($(this).children(':nth-child(3)').text().trim()),
                            discountReason: $(this).children(':nth-child(4)').text().trim()
                        };

                        itmArray.push(data);
                    }
                });

                var $itemAmount = $('input[name="cartDetails.itemAmount"]');
                $itemAmount.val((qty * price) - disc);

                $("input[name='cartDetails.strDiscounts']").val(JSON.stringify(itmArray));
            }
            else if (tblName == "tbltreatmentStaffs") {
                $('#tbltreatmentStaffs > tbody > tr').each(function () {
                    var data = {
                        staffCode: $(this).children(':first-child').text().trim(),
                        staffName: $(this).children(':nth-child(2)').text().trim()
                    };

                    itmArray.push(data);
                });

                $("input[name='cartDetails.strTreatmentStaffs']").val(JSON.stringify(itmArray));
            }
        });

        $(document).on("click", "[data-mode=save]", function () {
            var arr = [];

            var data = {
                lineStatus: $("#lineStatus").val().trim(),
                lineType: $("#lineType").val().trim(),
                itemCode: $("#itemCode").val().trim(),
                itemName: $("#itemName").val().trim(),
                itemQty: parseInt($('#itemQty').val()),
                unitPrice: parseFloat($('#unitPrice').val()),
                itemAmount: parseFloat($('#itemAmount').val()),
                strDiscounts: $('#strDiscounts').val()
            };

            arr.push(data);

            $('#strCarts').val(JSON.stringify(arr));
        });

        $(document).on("click", "[data-mode=cancel]", function () {
            $('input[name="cartDetails.strDiscounts"]').val('');
            $('input[name="cartDetails.strStaffs"]').val('');
            $('#strPayDetails').val('');
        });

        /*
        $(document).on("submit", "#frmCourse", function (e) {
            e.preventDefault();
            $(this).removeData("validator").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse($(this));
            if ($(this).valid()) {
                $.ajax({
                    processData: false,
                    contentType: false,
                    data: new FormData(this),
                    type: "POST",
                    url: "/Cart/Update",
                    success: function (data) {
                        window.location.href = data;
                    }
                });
            }
        });
        */

        $(document).on("submit", "#frmPayMode", function (e) {
            e.preventDefault();
            $(this).removeData("validator").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse($(this));

            if ($(this).valid()) {
                $.ajax({
                    processData: false,
                    contentType: false,
                    data: new FormData(this),
                    type: "POST",
                    url: "/Payment/Update",
                    success: function (data) {
                        window.location.href = data;
                    }
                });
            }
        });

        $(".live-search-box").focus(function () {
            $(this).val("");
        });
        
        //$(document).on("click", "#imgserv", function () {
        //    myj1 = 1;
        //    alert(myj1);
        //    //$("#mdlproddisp").modal({ backdrop: 'static', keyboard: false, show: false });
        //});

        //$(document).on("click", "#imgprod", function () {
        //    myj1 = 2;
        //    alert(myj1);
        //    $(".live-search-box").val('');
           
        //});
            //myj1 = 2;
            //alert(myj1);
        //});

        //$(document).on("click", "#btnSearchCustomer", function () {
        //var img = document.getElementById("imagecus");
        //img.addEventListener("mouseover", function () {
        //    if (img.src.match("images/image1.jpg")) {
        //        img.src = "images/image1_2.jpg";
        //    }
        //    else {
        //        img.src = "images/image1.jpg"
        //    }
        //})
        /*
function changeColor() {
    var image = document.getElementById("imagecus");
    if (image.getAttribute('src') == "Octagon - Customer.png") {
        image.src = "~/images/Octagon Customer Press.png";
    }
    else {
        image.src = "~/images/Octagon Customer.png";
    }
}
                $("#imagecus").mouseover(function () {
            $(this).attr('src', $(this).data("hover"));
        }).mouseout(function () {
            $(this).attr('src', $(this).data("src"));
        });
*/
        // modal popup is show,
        //$(document).on("click", "#btnSearchCustomer", function () {
        //    //$(".live-search-box").val('');
        //    $("#mdlCustomer").modal({ backdrop: 'static', keyboard: false, show: true });

        //});


        $(document).on("click", "#btnSearchCustomer", function () {
            //$(".live-search-box").val('');
            $("#mdlCustomer").modal({ backdrop: 'static', keyboard: false, show: true });
            //alert($("#txtsearchCust").val());
            //alert($("#Whatyouwant").val());
            showCustomerModal($("#txtsearchCust").val());            
        });

        //$(document).on("click", ".open-AddBookDialog", function () {
        //    var myBookId = $(this).data('id');
        //    $(".mdlinvoicenumber #bookId").val(myBookId);
        //    // As pointed out in comments, 
        //    // it is unnecessary to have to manually call the modal.
        //    // $('#addBookDialog').modal('show');
        //});
       

        $(document).on("click", "#btninvoicenumber", function () {

            var transnumber = $(this).data('transnumber');
            var invnumber = $(this).data('invnumber');
            var custname = $(this).data('custname');
            //var invamount = $(this).data('invamount');
            var phone = $(this).data('phone');
            //invnumber
            //custname
            //invamount
            //phone
            $("#mdlinvoicenumber").modal({ backdrop: 'static', keyboard: false, show: true });
            $(".modal-body #transnumber").val(transnumber);
            $(".modal-body #invnumber").val(invnumber);
            $(".modal-body #custname").val(custname);
            $(".modal-body #phone").val(phone);
            

            //showinvoicedetails($("#txtinvoicenumber").val());
        });

        //function showinvoicedetails(invoicenumber) {
        //    $.ajax({
        //        url: "/Redemption/Getinvoicedetail",
        //        data: { invoicenumber: invoicenumber },
        //        type: "GET",
        //        async: false,
        //        dataType: "html",
        //        contentType: "application/json",
        //        success: function (data) {
        //            $("#inv-srch-contain").html(data);
        //            //$(window).scrollTop($('#divServiceType').offset().top);


        //        }
        //    });


        $(document).on("click", "#btnemptycart", function () {
            $(".live-search-box").val('');
            $("#mdlemptycart").modal({ backdrop: 'static', keyboard: false, show: true });
        });
        $(document).on("click", "#btncheckout", function () {
            $(".live-search-box").val('');
            $("#mdlcheckout").modal({ backdrop: 'static', keyboard: false, show: true });
        });
        $(document).on("click", "#btnsupend", function () {
            $(".live-search-box").val('');
            $("#mdlsuspend").modal({ backdrop: 'static', keyboard: false, show: true });
        });


        //$(document).on("click", "#btnvoidtrans", function () {
        //    $(".live-search-box").val('');
        //    $("#mdlvoidtrans").modal({ backdrop: 'static', keyboard: false, show: true });
        //});

        //$(document).on("click", "#btnvoidtrans", function () {
        //    $("#buttonID").click(function (event) {
        //        event.preventDefault();
        //        $('<div title="Confirm Box"></div>').dialog({
        //            open: function (event, ui) {
        //                $(this).html("Yes or No question?");
        //            },
        //            close: function () {
        //                $(this).remove();
        //            },
        //            resizable: false,
        //            height: 140,
        //            modal: true,
        //            buttons: {
        //                'Yes': function () {
        //                    $(this).dialog('close');
        //                    $.post('url/theValueYouWantToPass');

        //                },
        //                'No': function () {
        //                    $(this).dialog('close');
        //                    $.post('url/theOtherValueYouWantToPAss');
        //                }
        //            }
        //        });
        //    });
        //});
        //$(document).on("click", "#btnvoidtrans", function () {
        //    $(".live-search-box").val('');
        //    $("#mdlvoidtrans").modal({ backdrop: 'static', keyboard: false, show: true });
        //});

        $("#btnSearchCust").on("click", function () {
            
            showCustomerModal($("#txtsearchCustomer").val());
        });

        $("#btnSearchCustomer1").on("click", function () {
            showCustomerModal($("#txtsearchCustomer1").val());
        });

        

        //$("#btnvoidtransaction").on("click", function () {
        //    showCustomerModal($("#btnvoidtransaction").val());
        //});
        $(document).on("click", "#btnvoidtransaction", function () {
            $(".live-search-box").val('');
            $("#mdlCustomer").modal({ backdrop: 'static', keyboard: false, show: true });
        });

        $(document).on("click", "#btnSearchStaff", function () {
            $(".live-search-box").val('');
            $("#mdlStaff").modal({ backdrop: 'static', keyboard: false, show: true });
        });

        $(document).on("click", "#btnSuspend", function () {
            $("#mdlToken").modal({ backdrop: 'static', keyboard: false, show: true });
        });

        $(document).on("keyup", ".live-search-box", function () {
            var searchTerm = $(this).val().toLowerCase();
            $(".live-search-list li").each(function () {
                if ($(this).filter("[data-search-term *= " + searchTerm + "]").length > 0 || searchTerm.length < 1) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
            //$.ajax({
            //    data: $(this).val().toLowerCase(),
            //    type: "POST",
            //    dataType: "json",
            //    url: "/Cart/Staff/",
            //    success: function (data) {
            //        window.location.href = data;
            //    }
            //});
            //$("#mdlCustomer").modal('toggle');

        });

        $(document).on("click", "#btnCustomer", function () {
            $('input[name="' + $(this).attr("data-value-target") + '"]').val($(this).attr("data-value"));
            $('input[name="' + $(this).attr("data-name-target") + '"]').val($(this).attr("data-name"));

            $.ajax({
                data: $("#frmStaff").serialize(),
                type: "POST",
                dataType: "json",
                url: "/Cart/Staff/",
                success: function (data) {
                    window.location.href = data;
                }
            });

            $("#mdlCustomer").modal('toggle');
        });

        $(document).on("click", "#btnStaff", function () {
            $('input[name="' + $(this).attr("data-value-target") + '"]').val($(this).attr("data-value"));
            $('input[name="' + $(this).attr("data-name-target") + '"]').val($(this).attr("data-name"));

            var name = $(this).attr("data-name");
            $('.card-body > .row').each(function () {
                if ($(this).children(":nth-child(7)").text().trim() == "")
                    $(this).children(":nth-child(7)").text(name);
            });

            $("#mdlStaff").modal('toggle');
        });

        //$('[data-mode=deleteCart]').click(function () {
        $(document).on("click", "[data-mode=deleteCart]", function () {
            $.ajax({
                url: $(this).attr("data-url"),
                data: { code: $(this).attr("data-code"), type: $(this).attr("data-value") },
                type: "GET",
                //contentType: false,
                success: function () {
                    window.location.href = "/Cart/Index" + $("input[name='cart.cartToken']").val();
                }
            });
        });

        $(document).on("click", "[data-mode=deletePayment]", function () {
            $.ajax({
                url: $(this).attr("data-url"),
                data: { code: $(this).attr("data-code"), type: $(this).attr("data-value") },
                type: "GET",
                contentType: false,
                success: function () {
                    window.location.href = "/Payment/Index/" + $("input[name='cart.cartToken']").val();
                }
            });
        });

        $("input[name='check-reverse-item']").change(function () {
            var itmArray = [];

            $('#tblReverse > div').each(function () {
                var chk = $(this).children(':first-child').find("input[name='check-reverse-item']:checkbox")[0];

                if (chk.checked) {
                    var data = {
                        treatmentNumber: $(this).children(':nth-child(3)').text().trim(),
                        treatmentName: $(this).children(':nth-child(4)').text().trim(),
                        treatmentUnitPrice: parseFloat($(this).children(':nth-child(6)').text().trim())
                    };

                    itmArray.push(data);
                }
            });

            $("input[name='strReversalItems']").val(JSON.stringify(itmArray));
        });

        $(document).on("click", "[data-mode=reverse]", function () {
            window.location.href = "/Redemption/ReverseTreatment/" + $(this).attr("data-ref") + "/" + $("input[name='check-item']:checked").attr("id").replace("chk-", "");
        });

        $(document).on("click", "[data-mode=details]", function () {
            window.location.href = "/Redemption/Details/" + $(this).attr("data-ref") + "/" + $("input[name='check-item']:checked").attr("id").replace("chk-", "");
        });

        $(document).on("click", "[data-mode=exchange]", function () {
            window.location.href = "/Service/Index/";
        });

        $(document).on("click", "[data-mode=appointment]", function () {
            window.location.href = "/Redemption/NextAppointment/" + $(this).attr("data-ref") + "/" + $("input[name='check-item']:checked").attr("id").replace("chk-", "");
        });

        $(document).on("click", "[data-mode=accounts]", function () {
            window.location.href = "/Accounts/Index/" + $(this).attr("data-ref") + "/" + ($("input[name='check-item']:checked").attr("id") == undefined ? "" : $("input[name='check-item']:checked").attr("id").replace("chk-", ""));
        });

        $(document).on("click", "[data-mode=account-details]", function () {
            window.location.href = "/Accounts/Details/" + $(this).attr("data-ref") + "/" + ($("input[name='check-item']:checked").attr("id") == undefined ? "" : $("input[name='check-item']:checked").attr("id").replace("chk-", ""));
        });

        $(document).on("click", "[data-mode=account-topup]", function () {
            
            window.location.href = "/Accounts/Topup/" + $(this).attr("data-ref") + "/" + ($("input[name='check-item']:checked").attr("id") == undefined ? "" : $("input[name='check-item']:checked").attr("id").replace("chk-", ""));
        });

        $(document).on("click", "[data-mode=product-details]", function () {
            window.location.href = "/Products/Details/" + $(this).attr("data-ref") + "/" + ($("input[name='check-item']:checked").attr("id") == undefined ? "" : $("input[name='check-item']:checked").attr("id").replace("chk-", ""));
        });

        $(document).on("change", "select[name='prepaid.prepaidCondition1']", function () {
            $.ajax({
                url: "/Cart/GetBrandServices",
                data: { condition: $(this).val() },
                type: "GET",
                success: function (data) {
                    var obj = data;

                    $("#prepaid_prepaidCondition2").empty();

                    for (var i = 0; i < obj.length; i++) {
                        $("#prepaid_prepaidCondition2").append('<option value="' + obj[i].itemCode + '">' + obj[i].itemDesc + '</option>');
                    }
                }
            });
        });
    });
});