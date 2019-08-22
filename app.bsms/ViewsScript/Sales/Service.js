$(document).ready(function () {
    var cachedProjectOptions = null;
    var cachedGroupOptions = null;
    $('#ServiceContainer').jtable({
        title: 'Sales Service',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'ServiceId ASC',
        actions: {
            listAction:
                function (postData, jtParams) {
                    console.log("Loading from custom function...");
                    return $.Deferred(function ($dfd) {
                        $.ajax({
                            url: '/Service/ServiceList?jtStartIndex=' + jtParams.jtStartIndex + '&jtPageSize=' + jtParams.jtPageSize + '&jtSorting=' + jtParams.jtSorting,
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
                             url: '/Service/CreateService',
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
                          url: '/Service/DeleteService',
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
                         url: '/Service/UpdateService',
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
            ServiceId: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            ServiceName: {
                title: 'Service Name',
                width: '23%',
                visibility: 'fixed'

            },

            ServiceDesc: {
                title: 'Description',
                width: '13%'

            },
            ProjectID: {
                title: 'Project Name',
                width: '13%',
                options: function () {
                    if (cachedProjectOptions) { //Check for cache
                        return cachedProjectOptions;
                    }
                    var options = [];
                    $.ajax({ //Not found in cache, get from server
                        url: '/Service/GetProjectList',
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
                    return cachedProjectOptions = options; //Cache results and return options
                }


            },
        }
    });

    //Load list from server
    $('#ServiceContainer').jtable('load');
});

