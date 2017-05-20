// Make sure to include the `ui.router` module as a dependency
angular.module('Wizcation', [
  'ui.router',
  'oc.lazyLoad',
  'pascalprecht.translate',
  'ngAutocomplete',
  '720kb.datepicker'
])

.run(
  ['$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
     

        if (localStorage.getItem('lang') == null) {
          
            localStorage.setItem('lang', "th");
        } else {
           
        }
        // It's very handy to add references to $state and $stateParams to the $rootScope
        // so that you can access them from any scope within your applications.For example,
        // <li ng-class="{ active: $state.includes('contacts.list') }"> will set the <li>
        // to active whenever 'contacts.list' or one of its decendents is active.
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
  ]
)
     .config(['$locationProvider', function($locationProvider) {
         $locationProvider.hashPrefix('');
     }])
    
.config(
  ['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider',
    function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {

        $urlRouterProvider.otherwise("/")

        $stateProvider
          .state('Home', {
              url: "/",
              templateUrl: '\Home/Content',
              resolve: {
                  lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                      return $ocLazyLoad.load([
                           {
                               name: 'Home',
                               files: [
                                   'scripts/app/Hotel.js'
                               ]
                           },
                      ])
                  }]
              }
          })
              .state('Hotel', {
                   url: "/hotel",
                   templateUrl: '\Hotel/Index',
                   controller: 'Hotel',
                   resolve: {
                       lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                           return $ocLazyLoad.load([
                                {
                                    name: 'Hotel',
                                    files: ['scripts/app/Hotel.js']
                                },
                           ])
                       }]
                   }
              })
             .state('HotelDetail', {
                 url: "/hotel/hotel_detail/{ID:int}",
                 templateUrl: '\Hotel/Detail',
                 controller: 'HotelDetail',
                 resolve: {
                     lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                         return $ocLazyLoad.load([
                              {
                                  name: 'Hotel',
                                  files: ['scripts/app/Hotel.js']
                              },
                         ])
                     }]
                 }
             })
              .state('Activities', {
                  url: "/activities",
                  templateUrl: '\Activities/Index',
                  resolve: {
                      lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                          return $ocLazyLoad.load([
                               {
                                   name: 'Activities',
                                   files: ['scripts/app/Hotel.js']
                               },
                          ])
                      }]
                  }
              })
        .state('CompanyOverview', {
            url: "/CompanyOverview",
            templateUrl: '\Home/About',
     
        })
           .state('TermCondition', {
               url: "/TermCondition",
               templateUrl: '\Home/TermCondition',
               resolve: {
                   lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                       return $ocLazyLoad.load([
                            {
                                name: 'PrivatePolicy',
                                files: ['scripts/app/Hotel.js']
                            },
                       ])
                   }]
               }
           })
        .state('Policy', {
            url: "/PrivatePolicy",
            templateUrl: '\Home/PrivatePolicy',
           
        })
          .state('Enquiry', {
              url: "/Enquiry",
              templateUrl: '\Home/Enquiry',
              resolve: {
                  lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                      return $ocLazyLoad.load([
                           {
                               name: 'PrivatePolicy',
                               files: ['scripts/app/Hotel.js']
                           },
                      ])
                  }]
              }
          })
        
    }
  ]
)

.config(function ($translateProvider) {

    // translate menu
    $translateProvider.useStaticFilesLoader({
        prefix: '\Scripts/Lang/',
        suffix: '.json'
    })
    $translateProvider.preferredLanguage(localStorage.getItem('lang'))
    $translateProvider.forceAsyncReload(true);


})

.controller("translateController", ["$scope", "$translate", "$http", function ($scope, $translate, $http) {

    $translate.use(localStorage.getItem('lang'));
    $http.get("api/Setting/ListLanguageData").then(function (response) {
        $scope.lang = response.data;
    });
    $scope.langSelect =localStorage.getItem('lang');
    $scope.changeLanguage = function (lang) {
        $translate.use(lang);
        $scope.langSelect = lang;
    }
}]);
