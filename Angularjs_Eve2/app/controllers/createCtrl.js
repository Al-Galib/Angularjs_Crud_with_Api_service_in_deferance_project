angular.module("bookApp")
    .controller("createCtrl", ($scope, apiSvc, createUrl) => {
        $scope.booknew = {};
        $scope.modalErr = "";
        $scope.insert = f => {
            console.log($scope.booknew.Picture);
            apiSvc.insert(createUrl, {
                BookName: $scope.booknew.BookName,
                AuthorName: $scope.booknew.AuthorName,
                PublishDate: $scope.booknew.PublishDate,
                Picture: $scope.booknew.Picture.base64,
                ImageType: $scope.booknew.Picture.filetype
            })
                .then(r => {
                    console.log(r);
                    $scope.model.books.push(r.data);
                    $scope.booknew = {};
                    f.$setPristine();
                    f.$setUntouched();
                    $scope.modalErr = "A new data added";
                }, err => {
                    $scope.modalErr = "Fail to insert data";
                    console.log(err);
                })
        }
    });