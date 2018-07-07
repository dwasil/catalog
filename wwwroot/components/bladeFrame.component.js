angular.
  module('bladeApp', ['treeApp'])
  .component('bladeframe', {      
      templateUrl: 'templates/bladeFrame.tmpl.html',
      controller: function BladeFrameController($rootScope, $timeout) {  
        var self = this;       
        this.dataReady = false;

        $rootScope.$on('onTreeDataReady', function(event) {          
          $timeout(function(){            
            self.dataReady = true;
          }, 1000);
        });
       }
  });