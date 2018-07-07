angular.
  module('treeApp').
  component('tree', {
    templateUrl: 'modules/tree/tree.html',    
    controller: function TreeController($http, $rootScope) {  
        var selectedNode = null,
            data = [];             

        this.setData = function (newData){ 
            data = newData;
            $rootScope.$emit('onTreeDataReady');
        }

        this.getData = function(){
            return data;
        }        
        
        this.onToggleNode = function($event, nodeId){
            var element = $event.currentTarget || $event.srcElement;
            toggleClass(element);    
            toggleChildren(nodeId);            
        }

        toggleClass = function(element)
        {                
            angular.element(element).toggleClass('plus');
            angular.element(element).toggleClass('minus');
        }

        toggleChildren = function(nodeId)
        {                
            var childrenEl = document.getElementById('children-'+nodeId);

            if(childrenEl)
            {
                angular.element(childrenEl).toggleClass('children-visible');
                angular.element(childrenEl).toggleClass('children-hidden');
            }
        }

        this.onClickNode = function($event, nodeId){
            var element = $event.currentTarget || $event.srcElement;                      

            angular.element(element).addClass('selected-node');

            if(selectedNode)
            {
                angular.element(selectedNode).removeClass('selected-node'); 
            }         
            
            selectedNode = element;
            $rootScope.$emit('onTreeNodeClick', nodeId);            
        }        

        var self = this;           
       
        $http.get('api/category/tree').then(
            function(response){    

                if(response.status == 200){
                    response = angular.fromJson(response);
                    self.setData(response.data);                                      
                }   
                else
                {
                    alert('Ошибка загрузки категорий');
                }
            },
            function(){
                alert('Ошибка загрузки категорий');
        });                 
    }
  });