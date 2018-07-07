angular.
  module('bladeApp')
  .component('productList', {            
      templateUrl: 'templates/product-list.tmpl.html',
      controller: function ProductListController($rootScope, $http, $timeout, $scope) {  
        this.restrict = 'E';
        var self = this;
        this.data = [];        
        this.isHiddenClass = ' hidden';       
        this.selectedIndex = -1;
        this.hasChecked = false;
        this.dataReady = false;
        this.categoryId = 0;
        this.currentPage = 0;
        this.pageSize = 0;
        this.lastPage = 0;                
        

        this.getNextPage = function(){            
            return this.currentPage < this.lastPage ? this.currentPage + 1 : 0; 
        }

        this.getPrevPage = function(){            
            return this.currentPage > 1 ? this.currentPage - 1 : 0; 
        }

        this.getPagesList = function(){
            return '';
        }

        this.hide = function(){
            this.isHiddenClass = ' hidden';
            $rootScope.$emit('onProductListHide');
        }

        this.gotoPage = function(page){
            
        }

        this.show = function(){
            this.isHiddenClass = '';
        }

        this.addProduct = function(){
            $rootScope.$emit('onProductAdd', this.categoryId);
        }

        this.onProductSelect = function(index, productId){
            this.selectedIndex = index;
            this.editProduct(productId); 
        }

        this.editProduct = function(productId){
            $rootScope.$emit('onProductEdit', productId);
        }

        // todo: batch mode
        this.deleteProducts = function(){
            if(!this.hasChecked)
                return;   
            
            var counter = 0;

            for(var i = this.data.list.length - 1; i >= 0; i--){                             
                if(this.data.list[i].checked){                                     
                    (function(i){
                        counter++;
                        $http.delete('api/product/'+self.data.list[i].id)
                        .then(
                            function (response) {                        
                                if(response.status == '204'){

                                    $rootScope.$emit('onProductDeleted', self.data.list[i].id);
                                    counter--;

                                    if(counter == 0) {
                                        self.getData();
                                    }
                                }
                                else{
                                    alert('Ошибка удаления');
                                }
                            },
                            function () {
                                alert('Ошибка удаления');
                            });        
                    })(i);                       
                }
            }   
        }        

        this.onPageSelect = function(pageNumber)
        {
            $rootScope.$emit('onPageSelect', {
                categotyId: this.categoryId,
                pageNumber: pageNumber
            });
        }

        this.setHasChecked = function()
        {
            var result = false;

            for(var i = this.data.list.length - 1; i >= 0; i-- ){
                if(this.data.list[i].checked){
                    result = true;
                    break;
                }
            }

            this.hasChecked = result;


        }

        this.getData = function(page)
        {
            var url = 'api/product/bycategory2/'+this.categoryId;

            if(page)
            {
                url += '?page='+page;
            }

            $http.get(url).then(
                function(response) {
                    //check data
                    var data = angular.fromJson(response);
                    
                    data.list = data.data.data.products;

                    for(var i = data.list.length - 1; i >= 0; i--){
                        data.list[i].checked = false;
                        data.list[i].selected = false;
                    }

                    self.data = data;                                             
                },
                function(){
                    alert('Ошибка получения данных')
                }
            );
        }

        $rootScope.$on('onTreeNodeClick', function(event, categoryId) {
            self.categoryId = categoryId;
            self.dataReady = false;
            self.show();      
            self.getData();                      
            $timeout(function(){ self.dataReady = true; }, 1000);        
        });

        $rootScope.$on('onProductAdded', function(event) {
            self.getData();                     
        });

        $rootScope.$on('onProductUpdated', function(event) {
            self.getData();                     
        });

        $rootScope.$on('onProductAddOpened', function(event) {
            self.selectedIndex = -1;                     
        });
      }
  });