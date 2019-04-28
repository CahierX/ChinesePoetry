<template>
  <div>
    <NavBar :msg="$t('la.person')" url="" />
    <van-cell :title="$t('la.user')" :border=false v-show="!isLogin" @click="openLogin()" is-link icon="manager-o" size='large'
      class="animated slideInUp" style="animation-duration: 700ms;" />
    <van-cell :title="$t('la.user')" :border=false v-show="isLogin" @click="openExit()" :value="username" icon="manager-o"
      size='large' class="animated slideInUp" style="animation-duration: 700ms" />
    <van-cell :title="$t('la.collect')" :border=false @click="collectList()" is-link icon="like" size='large' class="animated slideInUp"
      style="animation-duration: 700ms" />
    <van-cell :title="$t('la.zhOrTw')" :border=false @click="changeLanguage()" clickable size='large' icon="smile-comment-o"
      class="animated slideInUp" style="animation-duration: 1000ms" />
    <van-cell :title="$t('la.font')" :border=false @click="openFont()" clickable size='large' icon="smile-comment-o"
      class="animated slideInUp" style="animation-duration: 1000ms" />
    <van-cell :title="$t('la.money')" :border=false is-link icon="gold-coin-o" size='large' class="animated slideInUp"
      style="animation-duration: 800ms" @click="money()" />
    <van-cell :title="$t('la.aboutMe')" :border=false is-link icon="info-o" size='large' class="animated slideInUp"
      style="animation-duration: 900ms" @click="openAbout()" />
    <van-actionsheet v-model="show" :actions="actions" cancel-text="取消" @select="onSelect" @cancel="onCancel" />
    <van-popup v-model="fontShow" position="bottom" >
      <van-picker :columns="columns" @confirm="changeFont" @cancel="closeFont()" show-toolbar />
    </van-popup>
  </div>
</template>

<script>
// import Touch from '@/components/Touch.vue'
import NavBar from '@/components/NavBar.vue'
import Vue from 'vue'
import {
  CellGroup,
  Cell,
  SwitchCell,
  Actionsheet,
  Toast,
  Picker,
  Popup
} from 'vant'
Vue.use(Cell).use(CellGroup).use(SwitchCell).use(Actionsheet).use(Toast).use(Picker).use(Popup)
export default {
  name: 'Person',
  components: {
    NavBar
    // Touch
  },
  inject: ['reload'],
  data () {
    return {
      columns: ['云梦深深深几许', '楷体', '微软雅黑', '方正手迹-田歌硬笔行书', '方正字迹-徐杰行草', '方正龙开胜行书'],
      language: true,
      poetryList: [],
      username: '',
      isLogin: false,
      show: false, // 退出登录
      fontShow: false,
      actions: [{
        name: '确定退出登录嘛'
      }]
    }
  },
  created () {
    var page = mui.preload({
      url: 'Collect',
      id: 'Collect'
    })
    var page1 = mui.preload({
      url: 'Login',
      id: 'Login'
    })
    var page2 = mui.preload({
      url: 'About',
      id: 'About'
    })
  },
  mounted () {
    this.initUser()
  },
  methods: {
    changeFont (picker) {
      localStorage.setItem('font', picker)
      this.fontShow = false
      this.$router.go(0)
    },
    closeFont () {
      this.fontShow = false
    },
    openFont () {
      this.fontShow = !this.fontShow
    },
    money () {
      Toast({
        position: 'bottom',
        message: '不需要赞助(傲娇脸)'
      })
    },
    openAbout () {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push('/About')
      mui.openWindow({
        url: 'About',
        id: 'About',
        waiting: {
          autoShow: false
        }
      })
    },
    openExit () {
      this.show = true
    },
    onSelect (item) {
      // 点击选项时默认不会关闭菜单，可以手动关闭
      localStorage.removeItem('username')
      localStorage.removeItem('isLogin')
      this.isLogin = false
      this.show = false
      Toast({
        position: 'bottom',
        message: '注销成功'
      })
    },
    onCancel () {
      this.show = false
    },
    initUser () {
      this.isLogin = localStorage.getItem('isLogin')
      if (this.isLogin) {
        this.username = localStorage.getItem('username')
      }
    },
    collectList () {
      if (this.isLogin === false || this.isLogin === null) {
        // this.$store.commit('setTransition', 'turn-on')
        // this.$router.push('/login')
        mui.openWindow({
          url: 'login',
          id: 'login',
          waiting: {
            autoShow: false
          }
        })
      } else {
        // this.$store.commit('setTransition', 'turn-on')
        // this.$router.push('/Collect')
        mui.openWindow({
          url: 'Collect',
          id: 'Collect',
          waiting: {
            autoShow: false
          }
        })
      }
    },
    openLogin () {
      if (this.isLogin === false || this.isLogin === null) {
        // this.$store.commit('setTransition', 'turn-on')
        // this.$router.push('/login')
        mui.openWindow({
          url: 'login',
          id: 'login',
          waiting: {
            autoShow: false
          }
        })
      }
    },
    changeLanguage () {
      var type = 'tw'
      if (this.language) {
        type = 'zh'
      } else {
        type = 'tw'
      }
      this.$i18n.locale = type
      window.localStorage.setItem('language', type)
      this.language = !this.language
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .touchCss {
    height: 90vh;
    background-color: #fff;
  }
</style>
