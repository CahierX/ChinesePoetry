import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from './axios'
import './libs/rem.js'
import animated from 'animate.css'
import vcolorpicker from 'vcolorpicker'
import VueLoading from 'vue-loading-template'
import VueI18n from 'vue-i18n'
import VueTouch from 'vue-touch'
import Mui from 'vue-awesome-mui'
import 'vue-awesome-mui/mui/dist/css/mui.css'
import '../public/fonts/index.less'
import zh from '../public/static/zhCN' // 简体中文语言包
import tw from '../public/static/zhTW' // 繁体
VueTouch.config.swipe = {
  direction: 'horizontal'
}
Vue.use(animated).use(vcolorpicker).use(VueLoading).use(VueI18n).use(VueTouch, { name: 'v-touch' }).use(Mui) // 繁体中文语言包
const i18n = new VueI18n({
  locale: window.localStorage.getItem('language') === null ? 'zh' : window.localStorage.getItem('language'), // 语言标识，设置默认语言
  messages: {
    'zh': zh, // 简体中文
    'tw': tw // 繁体中文
  }
})

Vue.config.productionTip = false

new Vue({
  router,
  store,
  i18n,
  axios,
  render: h => h(App)
}).$mount('#app')
