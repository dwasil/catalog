angular.
  module('catalogApp')
  .component('productDetail', {            
      templateUrl: 'templates/product-detail.tmpl.html',
      controller: function ProductDetailController($rootScope, $http, $timeout, $scope, $document) {  
        this.restrict = 'E';
        var self = this;
        this.data = {};        
        this.isHiddenClass = ' hidden';            
        this.changed = false;
        this.valid = false;
        this.dataReady = false;        
        this.isQuantityInteger = true;

        this.isFormValid = function()
        {
            return this.isImageValid() && this.isNameValid() && this.isPriceValid() && this.isQuantityValid();
        }

        this.isImageValid = function()
        {
            return this.data.picture && this.data.picture.length && this.data.picture.length > 0;
        }

        this.isNameValid = function()
        {
            return this.data.name && this.data.name.length && this.data.name.length >= 3;
        }

        this.isPriceValid = function()
        {
            return this.data.price && this.data.price > 0;
        }        

        this.isQuantityValid = function()
        {            
            return this.data.quantity && this.data.quantity > 0 && (this.data.quantity^0) == this.data.quantity;
        }        

        this.hide = function()
        {
            this.saveIfNeedChangedData();
            this.isHiddenClass = ' hidden';            
        }

        this.show = function()
        {
            this.isHiddenClass = '';
        }

        this.update = function()
        {
            $http.put('api/product/'+this.data.id, this.data)
                .then(
                    function (response) {                        
                        if(response.status == '204'){
                            self.changed = false;
                            $rootScope.$emit('onProductUpdated');
                        }
                        else{
                            alert('Ошибка сохранения');
                        }
                    },
                    function () {
                        alert('Ошибка сохранения');
                    });            
        }

        this.create = function(){            
            $http.post('api/product/', this.data)
                .then(
                    function (response) {                        
                        if(response.status == '201'){                            
                            self.changed = false;
                            self.data = response.data;
                            $rootScope.$emit('onProductAdded');
                        }
                        else{
                            alert('Ошибка сохранения');
                        }
                    },
                    function () {
                        alert('Ошибка сохранения');
                    });            
        }

        this.save = function(){
            if(!this.changed)
                return;           

            if(this.data.id > 0){
                this.update();
            } 
            else{
                this.create();
            }
        }        

        this.cancel = function(){
            if(!this.changed)
                return;

            if(!confirm('Вы уверены, что хотите отменить сделанные изменения?')){                
               return;                 
            }

            if(this.data.id > 0)
            {
                this.getData(this.data.id);
            }
            else
            {
                this.setEmptyData(this.data.categoryId);
            }

            this.changed = false;
        }
        
        this.getData = function(productId){   
            this.changed = false;
            $http.get('api/product/'+productId)
            .then(function(response) {              
                
                if(response.status == 200){
                    response = angular.fromJson(response);
                    self.data = response.data;                    
                    self.resetImageInput();
                    $timeout(function(){ self.dataReady = true; }, 1000); 
                }
                else{
                    alert('Ошибка получения данных о товаре');
                    self.hide();   
                }

            },
            function(){
                alert('Ошибка получения данных о товаре');
                self.hide();   
            });  
        }

        this.setEmptyData = function(categoryId)
        {
            this.data = {
                "id": 0, 
                "picture":"", 
                "name":"", 
                "price": 0, 
                "quantity":0,
                "categoryId": categoryId
            };             

            this.resetImageInput();
        }

        this.resetImageInput = function()
        {            
            $document[0].getElementById('product-image-detail').value = '';
        }

        this.onChange = function()
        {
            this.changed = true;            
            this.valid = this.isFormValid();
        }
 
        this.isChanged = function()
        {
            return this.changed;            
        }
 
        this.saveIfNeedChangedData = function()
        {
            if(this.changed){
                if(confirm('Сохранить измененные данные?')){
                    this.save();                                   
                }
            }

            this.changed = false;     
        }        

        $scope.setImageFiles = function(files)
        {
            if(!files){
                return;
            }

            var fd = new FormData();               
            fd.append("image", files[0]);             

            $http.post('api/product/UploadImage/', fd, {                
                headers: {'Content-Type': undefined },
                transformRequest: angular.identity
            })
            .then(
                function(response){
                    if(response.status == 200){
                        response = angular.fromJson(response);
                        self.data.picture = response.data.path;    
                        self.onChange()                   
                    }   
                    else
                    {
                        alert('Ошибка загрузки изображения');
                    }
                },
                function(){
                    alert('Ошибка загрузки изображения');
            });            
        }

        //event listeners
        $rootScope.$on('onProductEdit', function(event, productId) {
            self.saveIfNeedChangedData();   
            self.dataReady = false;
            self.show();
            self.getData(productId);            
        });

        $rootScope.$on('onProductAdd', function(event, categoryId) {            
            self.saveIfNeedChangedData();             
            self.setEmptyData(categoryId);   
            self.show();    
            self.dataReady = true;
            $rootScope.$emit('onProductAddOpened');
        });

        $rootScope.$on('onProductListHide', function(event) {              
            self.hide();                       
        });

        $rootScope.$on('onProductDeleted', function(event, id) {
            if(id == self.data.id){
                self.changed = false;
                self.hide(); 
            }      
        });        

        $rootScope.$on('onTreeNodeClick', function(event, data) {              
            self.hide();
        });
      }
  });