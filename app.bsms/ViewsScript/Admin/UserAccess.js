$(document).ready(function () {
    var cachedDepartmentOptions = null;
    var cachedGroupOptions = null;
    //For search deaprtment
    //var SearchDepartmentID = $("#SearchDepartmentID");
    //SearchDepartmentID.empty();
    //SearchDepartmentID.append($("<option></option").val("").html("Select Department"));
    //$.ajax({
    //    url: '/UserAccess/GetDepartment',
    //    type: 'POST',
    //    contentType: 'application/json; charset=utf-8',
    //    data: JSON.stringify(),
    //    dataType: 'json',
    //    success: function (data) {
    //        //Department
    //        $.each(data.DisplayText, function (i, val) {
    //            SearchDepartmentID.append(
    //                $("<option></option>").val(val.Value).html(val.DisplayText)
    //            );
    //        });

    //    },
    //    error: function (errorThrown) {
    //    }
    //});
    /// ends here
    $('#UserAccessContainer').jtable({
        title: 'User Role',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'RoleName ASC',
        actions: {
            listAction:
                function (postData, jtParams) {
                    console.log("Loading from custom function...");
                    return $.Deferred(function ($dfd) {
                        $.ajax({
                            url: '/UserAccess/UsersRoleAccess?jtStartIndex=' + jtParams.jtStartIndex + '&jtPageSize=' + jtParams.jtPageSize + '&jtSorting=' + jtParams.jtSorting,
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
                             url: '/UserAccess/CreateRoleAccess',
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
                          url: '/UserAccess/DeleteRoleAccess',
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
            updateAction:
             function (postData) {
                 return $.Deferred(function ($dfd) {
                     $.ajax({
                         url: '/UserAccess/UpdateRoleAccess',
                         type: 'POST',
                         dataType: 'json',
                         data: postData,
                         success: function (data) {
                             $dfd.resolve(data);
                             if (data.Result == 'OK') {
                                 alert(data.Message);
                                 return;
                             }
                             //alert(data.Message);
                         },
                         error: function () {
                             $dfd.reject();
                         }
                     });
                 });
             }

        },
        fields: {
            RoleID: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            RoleName: {
                title: 'Role Name',
                width: '23%',
                visibility: 'fixed',

            },

            RoleType: {
                title: 'Role Type',
                width: '13%',

            },
            DepartmentID: {
                title: 'Department',
                width: '15%',
                options: function () {
                    if (cachedDepartmentOptions) { //Check for cache
                        return cachedDepartmentOptions;
                    }
                    var options = [];
                    $.ajax({ //Not found in cache, get from server
                        url: '/UserAccess/GetDepartment',
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
                    return cachedDepartmentOptions = options; //Cache results and return options
                }

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
                        url: '/UserAccess/GetUserGroup',
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

        }
    });

    //Load list from server
    $('#UserAccessContainer').jtable('load');
});

$("#SearchDepartmentID").change(function () {
    $('#UserAccessContainer').jtable('load', {
        iDepartmentID: $('#SearchDepartmentID').val()
    });
});
