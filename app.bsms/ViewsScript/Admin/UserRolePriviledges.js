
$(document).ready(function () {
    var cachedDepartmentOptions = null;
    var cachedGroupOptions = null;
    var cachedMenuOptions = null;
    //For search deaprtment
    var SearchDepartmentID = $("#SearchDepartmentID");
    SearchDepartmentID.empty();
    SearchDepartmentID.append($("<option></option").val("").html("Select Department"));
    //For Menu
    var SearchMenuID = $("#SearchMenuID");
    SearchMenuID.empty();
    SearchMenuID.append($("<option></option").val("").html("Select Menu"));
    //For Role Group
    var SearchRoleGrpID = $("#SearchRoleGrpID");
    SearchRoleGrpID.empty();
    SearchRoleGrpID.append($("<option></option").val("").html("Select Role Group"));

    $.ajax({
        url: '/UserRolePrivileges/GetListData',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(),
        dataType: 'json',
        success: function (data) {
            //Department
            $.each(data.Department, function (i, val) {
                SearchDepartmentID.append(
                    $("<option></option>").val(val.Department_ID).html(val.DepartName)
                );
            });
            //Menu
            $.each(data.Menu, function (i, val) {
                SearchMenuID.append(
                    $("<option></option>").val(val.Menu_ID).html(val.Menuname)
                );
            });
            //RoleGroup
            $.each(data.RoleGrp, function (i, val) {
                SearchRoleGrpID.append(
                    $("<option></option>").val(val.RoleGrpID).html(val.RoleGrpName)
                );
            });

        },
        error: function (errorThrown) {
        }
    });

    $('#UserRolePAccessContainer').jtable({
        title: 'Users Role Priviledges',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'DepartmentID ASC',
        actions: {
            listAction:
                function (postData, jtParams) {
                    console.log("Loading from custom function...");
                    return $.Deferred(function ($dfd) {
                        $.ajax({
                            url: '/UserRolePrivileges/UsersRolePrivilegesList?jtStartIndex=' + jtParams.jtStartIndex + '&jtPageSize=' + jtParams.jtPageSize + '&jtSorting=' + jtParams.jtSorting,
                            type: 'POST',
                            dataType: 'json',
                            data: postData,
                            success: function (data) {
                                $dfd.resolve(data);
                            },
                            error: function () {
                                $dfd.reject();
                            }
                        });
                    });
                },
            createAction:
                 function (postData) {
                     return $.Deferred(function ($dfd) {
                         $.ajax({
                             url: '/UserRolePrivileges/CreateRolePrivileges',
                             type: 'POST',
                             dataType: 'json',
                             data: postData,
                             success: function (data) {
                                 $dfd.resolve(data);
                                 if (data.Result == 'OK') {
                                     alert(data.Message);
                                     return;
                                 }
                             },
                             error: function () {
                                 $dfd.reject();
                             }
                         });
                     });
                 },
            deleteAction:
              function (postData) {
                  return $.Deferred(function ($dfd) {
                      $.ajax({
                          url: '/UserRolePrivileges/DeleteRolePrivileges',
                          type: 'POST',
                          dataType: 'json',
                          data: postData,
                          cache: false,
                          success: function (data) {
                              $dfd.resolve(data);
                              if (data.Result == 'OK') {
                                  alert(data.Message);
                                  return;
                              }
                          },
                          error: function () {
                              $dfd.reject();
                          }
                      });
                  });
              },
            updateAction:
             function (postData) {
                 return $.Deferred(function ($dfd) {
                     $.ajax({
                         url: '/UserRolePrivileges/UpdateRolePrivileges',
                         type: 'POST',
                         dataType: 'json',
                         data: postData,
                         success: function (data) {
                             $dfd.resolve(data);
                             if (data.Result == 'OK') {
                                 alert(data.Message);
                                 return;
                             }
                         },
                         error: function () {
                             $dfd.reject();
                         }
                     });
                 });
             }

        },
        fields: {
            RolePrivilegesID: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            RoleGrpID: {
                title: 'Role Group',
                width: '12%',
                options: function () {
                    if (cachedGroupOptions) { //Check for cache
                        return cachedGroupOptions;
                    }
                    var options = [];
                    $.ajax({ //Not found in cache, get from server
                        url: '/UserRolePrivileges/GetUserGroup',
                        type: 'POST',
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            if (data.Result != 'OK') {
                                alert(data.Message);
                                return;
                            }
                            options = data.Options;
                        }
                    });
                    return cachedGroupOptions = options; //Cache results and return options
                }
            },
            DepartmentID: {
                title: 'Menu',
                width: '15%',
                options: function () {
                    if (cachedDepartmentOptions) { 
                        return cachedDepartmentOptions;
                    }
                    var options = [];
                    $.ajax({ 
                        url: '/UserRolePrivileges/GetDepartment',
                        type: 'POST',
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            if (data.Result != 'OK') {
                                alert(data.Message);
                                return;
                            }
                            options = data.Options;
                        }
                    });
                    return cachedDepartmentOptions = options; 
                }

            },
            menu_id: {
                title: 'Sub Menu',
                dependsOn: 'DepartmentID',
                width: '22%',
                options: function (data) {
                    if (data.source == 'list') {
                        return '/UserRolePrivileges/GetUserMenu?DepartmentID=0';
                    }
                    return '/UserRolePrivileges/GetUserMenu?DepartmentID=' + data.dependedValues.DepartmentID;
                },
            },
            Visible: {
                title: 'Visible',
                width: '6%',
                type: 'radiobutton',
                options: { '0': 'No', '1': 'Yes' }
            },
            RAdd: {
                title: 'Add',
                width: '6%',
                type: 'radiobutton',
                options: { '0': 'No', '1': 'Yes' }
            },
            RModify: {
                title: 'Modify',
                width: '6%',
                type: 'radiobutton',
                options: { '0': 'No', '1': 'Yes' }
            },
            //CustomAction: {
            //    title: '',
            //    width: '2%',
            //    list: true,
            //    display: function (data) {
            //        if (data.record) {

            //            var edit = " ";
            //            edit = edit + "<div class='btn-group'>";
            //            //edit = edit + "  <button type='button' class='btn btn-primary  btn-xs dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><i class='fa fa-wrench lg'></i></button>";
            //            edit = edit + "  <button type='button' class='btn btn-primary  btn-xs dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><i class='fa fa-wrench lg'></i>Action";
            //            edit = edit + "    <span class='caret'></span>";
            //            edit = edit + "  </button>";
            //            edit = edit + "  <ul class='dropdown-menu' style='min-width:100px;font-size:11px;margin:0px;padding:0px'>";
            //            edit = edit + "    <li><a href='#'><i class='fa fa-search lg'></i> View Request</a></li>";
            //            edit = edit + "    <li class='divider' style='margin:0px'></li>";
            //            //if (rowData[i].MediaID == "3") {
            //            //    if (rowData[i].PFX != "") {
            //            //        edit = edit + "    <li><a href='#' onclick=\"getSoftCert('" + rowData[i].CertificateRequestID + "')\"><i class='fa fa-download lg'></i> Download Cert</a></li>";
            //            //        edit = edit + "    <li class='divider' style='margin:0px'></li>";
            //            //    }
            //            //} else if (rowData[i].MediaID == "5") {
            //            //    if (rowData[i].Cert_Status == "1") {
            //            //        edit = edit + "    <li><a style='cursor:pointer' href='/Certificate/getServerCertificate?CertificateRequestID=" + rowData[i].CertificateRequestID + "&RegistrationNo=" + rowData[i].RegistrationNo + "'><i class='fa fa-download lg'></i> Download Cert</a></li>";
            //            //        edit = edit + "    <li class='divider' style='margin:0px'></li>";
            //            //        //cert = "<a class='badge badge-success' style='cursor:pointer' href='/Certificate/getServerCertificate?CertificateRequestID=" + rowData[i].CertificateRequestID + "&RegistrationNo=" + rowData[i].RegistrationNo + "'>DOWNLOAD</a> ";
            //            //    }
            //            //}
            //            //if (rowData[i].Cert_Status == "1") {
            //            //    edit = edit + "    <li><a href='#' onclick=\"getData('" + rowData[i].CertificateRequestID + "')\"><i class='fa fa-delete lg'></i> Request Revoke</a></li>";
            //            //}
            //            edit = edit + "  </ul>";
            //            edit = edit + "</div>";

            //            //return '<button title="Edit Contract" class="jtable-command-button jtable-edit-command-button" onclick=\"getUpdate(' + data.record.ContractId + ')\; return false;"><span>Edit Record</span></button>';
            //            return edit;
            //        }
            //    }
            //}
        }
    });

    //Load  list from server
    $('#UserRolePAccessContainer').jtable('load');
});
$("#SearchDepartmentID").change(function () {
    $('#UserRolePAccessContainer').jtable('load', {
        iDepartmentID: $('#SearchDepartmentID').val()
    });
});
$("#SearchMenuID").change(function () {
    $('#UserRolePAccessContainer').jtable('load', {
        iMenuID: $('#SearchMenuID').val()
    });
});
$("#SearchRoleGrpID").change(function () {
    $('#UserRolePAccessContainer').jtable('load', {
        iRoleGrpID: $('#SearchRoleGrpID').val()
    });
});
$("#btnToFind").click(function () {
    $('#UserRolePAccessContainer').jtable('load', {
        iDepartmentID: $('#SearchDepartmentID').val(),
        iMenuID: $('#SearchMenuID').val(),
        iRoleGrpID: $('#SearchRoleGrpID').val()
    });
});
