// Make sure to include the `ui.router` module as a dependency
angular.module('Wizcation', [
  'ui.router',
  'oc.lazyLoad'
])

.run(
  ['$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {

        // It's very handy to add references to $state and $stateParams to the $rootScope
        // so that you can access them from any scope within your applications.For example,
        // <li ng-class="{ active: $state.includes('contacts.list') }"> will set the <li>
        // to active whenever 'contacts.list' or one of its decendents is active.
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
  ]
)

.config(
  ['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider',
    function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {

     
        $urlRouterProvider.otherwise("/")

        //////////////////////////
        // State Configurations //
        //////////////////////////

        // Use $stateProvider to configure your states.
        $stateProvider
          .state('Home', {
              url: "/",
              templateUrl: '\Home/Content',
              resolve: {
                  lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                      return $ocLazyLoad.load([
                           {
                               name: 'Salereport',
                               files: ['scripts/app/Hotel.js']
                           },
                      ])
                  }]
              }
          })
               .state('Hotel', {
                   url: "/hotel",
                   templateUrl: '\Hotel/Index',
                   resolve: {
                       lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                           return $ocLazyLoad.load([
                                {
                                    name: 'Salereport',
                                    files: ['scripts/app/Hotel.js']
                                },
                           ])
                       }]
                   }
               })
        .state('CompanyOverview', {
            url: "/rr",
            templateUrl: '\Home/About',
            resolve: {
                lazyLoad: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                         {
                             name: 'Salereport',
                             files: ['scripts/app/Hotel.js']
                         },
                    ])
                }]
            }
        })
    }
  ]
);
