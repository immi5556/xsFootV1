var personalDetailsPnl = $("#PersonalDetailsPnl")
var accountDetailsPnl = $("#AccountDetailsPnl")
var securityDetailsPnl = $("#SecurityDetailsPnl")
var confirmationPnl = $("#ConfirmationPnl")
var stage1 = $("#stage1")
var stage1cur = $("#stage1cur")
var stage1suc = $("#stage1suc")
var stage2 = $("#stage2")
var stage2cur = $("#stage2cur")
var stage2suc = $("#stage2suc")
var stage3 = $("#stage3")
var stage3cur = $("#stage3cur")
var stage3suc = $("#stage3suc")
var stage4 = $("#stage4")
var stage4cur = $("#stage4cur")
var stage4suc = $("#stage4suc")
var form = $('form');
var boolDOB = "";

$(document).ready(function () {
    //accountDetailsPnl.hide();
    //securityDetailsPnl.hide();
    //confirmationPnl.hide();
    document.getElementById("PersonalDetailsPnl").style.display = "block";
    document.getElementById("AccountDetailsPnl").style.display = "none";
    document.getElementById("SecurityDetailsPnl").style.display = "none";
    document.getElementById("ConfirmationPnl").style.display = "none";

    $("#icno").hide();
    $("#passport").hide();

    $("#ICNo").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            $("#errmsg").html("Numbers Only").show().fadeOut("slow");
            return false;
        }
    });
    $("#Postcode").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            $("#errmsg").html("Numbers Only").show().fadeOut("slow");
            return false;
        }
    });
    $("#Mobile_No").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            $("#errmsg").html("Numbers Only").show().fadeOut("slow");
            return false;
        }
    });

    $('#DOB').datepicker(
    {
        changeMonth: true,
        yearRange: "-100:+0",
        changeYear: true,
        autoclose: true
    });
});

$('#ic').change(function () {
    $("#icno").show();
    $("#passport").hide();

    $('#Passport').val("");
});
$('#psport').change(function () {
    $("#icno").hide();
    $("#passport").show();
    $('#ICNo').val("");
    //$('#Passport').val("");
});

$('#ICNo').change(function () {
    var str = $('#ICNo').val();
    var year = str.substring(0, 2);
    var month = str.substring(2, 4);
    var day = str.substring(4, 6);

    var date2 = year + "/" + month + "/" + day;

    $('#DOB').datepicker('setDate', new Date(19 + year, month - 1, day));
});

var checkic = "";
var checkuname = "";
$('#ICNo').blur(function () {
    var url = "/Register/CheckICNo";
    var name = $('#ICNo').val();

    $.get(url, { input: name }, function (data) {
        if (data == "NotAvailable") {
            $("#result").html("<span style='color:red'>IC NO already exist</span>");
            checkic = data;
        }
        else {
            $("#result").html("<span style='color:green'></span>");
            checkic = data;
        }
    });
})

$('#UserName').blur(function () {
    var url = "";//"/Register/CheckUserName";
    var usname = $('#UserName').val();

    $.get(url, { input: usname }, function (udata) {
        if (udata == "NotAvailable") {
            $("#uname").html("<span style='color:red'>Username already exist</span>");
            checkuname = udata;
        }
        else {
            $("#uname").html("<span style='color:green'></span>");
            checkuname = udata;
        }
    });
})

//Next - PersonalDetails
$('#validatePersonalDetails').click(function () {

    var selectedVal = "";
    var selected = $("input[type='radio'][name='IDType']:checked");
    if (selected.length > 0) {
        selectedVal = selected.val();
        if (selectedVal == "IC") {
            if ($('#ICNo').val() == "") {
                //alert("Please fill in IC No");
                //return false;
            }
        }
        if (selectedVal == "PP") {
            if ($('#Passport').val() == "") {
                //alert("Please fill in Passport No");
                //return false;
            }
        }
    }

    var dob = $('#DOB').val();
    var newDOB = getAge(dob);

    if (newDOB < 18) {
        alert("You're not allowed to register since your age is below 18 years")
        return false;
    }

    //var isValid = (form.validate().element($('#Name'))
    //    & form.validate().element($('#IDType'))
    //    & form.validate().element($('#DOB'))
    //    & form.validate().element($('#Sex'))
    //    & form.validate().element($('#Address'))
    //    & form.validate().element($('#City'))
    //    //& form.validate().element($('#State'))
    //    & form.validate().element($('#Postcode'))
    //    & form.validate().element($('#Email'))
    //    & form.validate().element($('#Mobile_No')));

    //if (isValid && checkic != "NotAvailable") {
        accountDetailsPnl.show();
        personalDetailsPnl.hide();
        confirmationPnl.hide();
        stage1suc.show();
        stage1cur.hide();
        stage2cur.show();
        stage2.hide();
    //}
    return true;
});

function getAge(birthDateString) {
    var today = new Date();
    var birthDate = new Date(birthDateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}

//Prev - AccountDetails
function showPersonalDetailsPnl() {

    //var isValid = form.validate().element($('#Name'))
    //    & form.validate().element($('#Address'));
    accountDetailsPnl.hide();
    confirmationPnl.hide();
    personalDetailsPnl.show();
    stage1suc.hide();
    stage1cur.show();
    stage2.show();
    stage2cur.hide();
}
//Next - AccountDetails
function validateAccountDetails() {

    //var isValid2 = (form.validate().element($('#UserName')) 
    //    & form.validate().element($('#Password')));

    
    //if (isValid2 && checkuname != "NotAvailable") {
        accountDetailsPnl.hide();
        personalDetailsPnl.hide();
        securityDetailsPnl.show();
        stage2cur.hide();
        stage2suc.show();
        stage3.hide();
        stage3cur.show();
    //}
    return true;
}
//Prev - SecurityDetails
function showAccountDetailsPnl() {

    //var isValid = form.validate().element($('#Name'))
    //    & form.validate().element($('#Address'));
    personalDetailsPnl.hide();
    securityDetailsPnl.hide();
    accountDetailsPnl.show();
    stage2suc.hide();
    stage2cur.show();
    stage3.show();
    stage3cur.hide();
}

//Next - SecurityDetails
function validateSecurityDetails() {

    //var selectedVal = "";
    //var selected = $("input[type='radio'][name='SecurityImg']:checked");
    //if (selected.length > 0) {
    //    selectedVal = selected.val();
    //}

    //if (selectedVal == "") {
    //    alert("Please select Security Image");
    //    return false;
    //}

    //var isValid = form.validate().element($('#SecurityImg'))
    //            & form.validate().element($('#SecurityPhrase'));

    //if (isValid) {

    //    //ID Type Name
    //    var IDType = "";
    //    var IDNo1 = $('#ICNo').val();
    //    var IDNo2 = $('#Passport').val();

    //    var s = $("input[type='radio'][name='IDType']:checked");
    //    if (s.length > 0) {
    //        IDType = s.val();
    //    }

    //    var Name = $('#Name').val();
    //    $("#DisplayName").html(Name);

    //    if (IDType == "PP") {
    //        $("#DisplayIDType").html("Passport");
    //        $("#DisplayICNo").html(IDNo2);
    //    }
    //    else {
    //        $("#DisplayIDType").html("IC");
    //        $("#DisplayICNo").html(IDNo1);
    //    }

    //    var DOB = $('#DOB').val();
    //    $("#DisplayDOB").html(DOB);

    //    var vsex = $("input[type='radio'][name='Sex']:checked");
    //    var valSex = "";
    //    if (vsex.length > 0) {
    //        valSex = vsex.val();
    //    }
    //    var Sex = valSex;
    //    if (Sex == "M") {
    //        $("#DisplayGender").html("Male");
    //    }
    //    if (Sex == "F") {
    //        $("#DisplayGender").html("Female");
    //    }
    //    var Address = $('#Address').val();
    //    $("#DisplayAddress").html(Address);
    //    var City = $('#City').val();
    //    $("#DisplayCity").html(City);
    //    var Postcode = $('#Postcode').val();
    //    $("#DisplayPostcode").html(Postcode);

    //    //var Country = $('#Country').val();
    //    //$("#DisplayCountry").html(Country);
    //    var Email = $('#Email').val();
    //    $("#DisplayEmail").html(Email);
    //    var Mobile_No = $('#Mobile_No').val();
    //    $("#DisplayMobileNo").html(Mobile_No);
    //    var UserName = $('#UserName').val();
    //    $("#DisplayUserName").html(UserName);
    //    var SecurityPhrase = $('#SecurityPhrase').val();
    //    $("#DisplaySecurity").html(SecurityPhrase);

    //    var State = $('#State').val();
    //    var Country = $('#Country').val();

    //    var statecountry = {
    //        State: State,
    //        Country: Country
    //    };

       
        accountDetailsPnl.hide();
        securityDetailsPnl.hide();
        confirmationPnl.show();
        stage3suc.show();
        stage3cur.hide();
        stage4.hide();
        stage4cur.show();
    //}
}

//Prev - Confirmation
function showSecurityDetailsPnl() {

    //var isValid = form.validate().element($('#Name'))
    //    & form.validate().element($('#Address'));
    accountDetailsPnl.hide();
    confirmationPnl.hide();
    securityDetailsPnl.show();
    stage4cur.show();
    stage4.hide();
    stage3suc.hide();
    stage3cur.show();
    stage4.show();
    stage4cur.hide();
}

$("#Country").change(function () {
    // this will call when Country Dropdown select change
    var CountryID = parseInt($("#Country").val());
    if (!isNaN(CountryID)) {
        var ddState = $("#State");
        ddState.empty(); // this line is for clear all items from State dropdown
        ddState.append($("<option></option").val("").html("Select State"));

        // Here call Controller Action via Jquery to load State for selected Country
        //$.ajax({
        //    url: '../Register/GetStates/',
        //    type: "GET",
        //    data: { CountryID: CountryID },
        //    dataType: "json",
        //    success: function (data) {
        //        $.each(data, function (i, val) {
        //            ddState.append(
        //                $("<option></option>").val(val.StateID).html(val.StateName)
        //            );
        //        });
        //    },
        //    error: function () {
        //        alert("Error!");
        //    }
        //});
    }
});