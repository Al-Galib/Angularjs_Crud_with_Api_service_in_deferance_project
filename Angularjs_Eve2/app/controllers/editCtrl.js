angular.module("bookApp")
    .controller("editCtrl", ($scope, apiSvc, editUrl, $routeParams) => {
        var id = $routeParams.id;
        $scope.modalErr = "";
        $scope.bookEdit = {};
        var existing = $scope.model.books.find(a => a.Id == id);
        angular.copy(existing, $scope.bookEdit);
        $scope.update = f => {
           
            var obj = { Id: $scope.bookEdit.Id, BookName: $scope.bookEdit.BookName, AuthorName: $scope.bookEdit.AuthorName, PublishDate: $scope.bookEdit.PublishDate, Picture:"" }
            if ($scope.bookEdit.Picture.filesize && $scope.bookEdit.Picture.filesize >= 0) {
                obj.Picture = $scope.bookEdit.Picture.base64;
                obj.ImageType = $scope.bookEdit.Picture.filetype;
            }
            apiSvc.update(editUrl + "/" + $scope.bookEdit.Id, obj)
                .then(r => {
                    console.log(r);
                    var i = $scope.model.books.findIndex(a => a.Id == id);
                    $scope.model.books[i].BookName = r.data.BookName;
                    $scope.model.books[i].AuthorName = r.data.AuthorName;
                    $scope.model.books[i].PublishDate = r.data.PublishDate;
                    $scope.model.books[i].Picture = r.data.Picture;
                    f.$setPristine();
                    f.$setUntouched();
                    $scope.modalErr = "Data updated";
                }, err => {
                    $scope.modalErr = "updated faild";
                    console.log(err);
                })
            
        }
    });