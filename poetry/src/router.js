import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'App',
      component: () => import('./App.vue'),
      children: [
        {
          path: '/',
          name: 'home',
          component: Home
        },
        {
          path: '/PoetryDetails',
          name: 'PoetryDetails',
          component: () => import('./views/PoetryDetails.vue'),
          meta: {
            keepAlive: false
          }
        },
        {
          path: '/PoetDetails',
          name: 'PoetDetails',
          component: () => import('./views/PoetDetails.vue'),
          meta: {
            keepAlive: false
          }
        },
        {
          path: '/Person',
          name: 'Person',
          component: () => import('./views/Person.vue'),
          meta: {
            keepAlive: false
          }
        },
        {
          path: '/PoetryStarTop',
          name: 'PoetryStarTop',
          component: () => import('./views/PoetryStarTop.vue'),
          meta: {
            keepAlive: true // 需要被缓存
          }
        },
        {
          path: '/SearchForKey',
          name: 'SearchForKey',
          component: () => import('./views/SearchForKey.vue'),
          meta: {
            keepAlive: true
          }
        },
        {
          path: '/PoetList',
          name: 'PoetList',
          component: () => import('./views/PoetList.vue'),
          meta: {
            keepAlive: true
          }
        },
        {
          path: '/PoetrySentence',
          name: 'PoetrySentence',
          component: () => import('./views/PoetrySentence.vue'),
          meta: {
            keepAlive: true
          }
        },
        {
          path: '/SearchBaidu',
          name: 'SearchBaidu',
          component: () => import('./views/SearchBaidu.vue'),
          meta: {
            keepAlive: false
          }
        },
        {
          path: '/Share',
          name: 'Share',
          component: () => import('./views/Share.vue'),
          meta: {
            keepAlive: false
          }
        },
        {
          path: '/Login',
          name: 'Login',
          component: () => import('./views/Login.vue'),
          meta: {
            keepAlive: false
          }
        },
        {
          path: '/Collect',
          name: 'Collect',
          component: () => import('./views/Collect.vue'),
          meta: {
            keepAlive: false
          }
        },
        {
          path: '/FilterPoetry',
          name: 'FilterPoetry',
          component: () => import('./views/FilterPoetry.vue'),
          meta: {
            keepAlive: false
          }
        },
        {
          path: '/FilterView',
          name: 'FilterView',
          component: () => import('./views/FilterView.vue'),
          meta: {
            keepAlive: true
          }
        },
        {
          path: '/SearchPoetryForPoet',
          name: 'SearchPoetryForPoet',
          component: () => import('./views/SearchPoetryForPoet.vue'),
          meta: {
            keepAlive: true
          }
        },
        {
          path: '/About',
          name: 'About',
          component: () => import('./views/About.vue'),
          meta: {
            keepAlive: true
          }
        }
      ]
    }
  ]
})
