angular.module("bookApp")
    .factory("apiSvc", ($http) => {
        return {
            get: (url) => {
                return $http({
                    method: "GET",
                    url: url
                });
            },
            insert: (url, data) => {
                return $http({
                    method: "POST",
                    url: url,
                    data: data
                });
            },
            update: (url, data) => {
                return $http({
                    method: "PUT",
                    url: url,
                    data: data
                });
            },
            delete: (url) => {
                return $http({
                    method: "DELETE",
                    url: url
                   
                });
            }
        }
    });