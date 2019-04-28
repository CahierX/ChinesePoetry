<template>
  <div class="allDiv">
    <NavBar :msg="$t('la.login')" url='Person' />
    <!-- <Touch> -->
      <div class="login animated fadeIn ">
        <div style="text-align:center">
          <br>
          <span class="titlePoet">{{$t('la.poetryTitle')}}</span>
        </div>
        <van-field style="margin-top:2em;width:90%" v-model="username" class="input center"  :placeholder="$t('la.username')">
        </van-field>
        <van-field style="margin-top:2em;width:90%" v-model="password" type="password" class="input center"  @keyup.enter="enter()" :placeholder="$t('la.password')">
        </van-field>
        <span v-show="!loginIsShow" style="float:right;font-size:14px;color:#969799;padding:15px" @click="loginShow()">已经有账号了?立即登录</span>
        <span v-show="loginIsShow" style="float:right;font-size:14px;color:#969799;padding:15px" @click="registerShow()">还没有账号?立即注册</span>
        <van-button v-show="!loginIsShow" plain type="info" style="width:90%;transform: translateX(5%);" ref="focus"  @click="register()">{{$t('la.register')}}</van-button>
        <van-button v-show="loginIsShow" plain type="info" style="width:90%;transform: translateX(5%);" ref="focus"  @click="login()">{{$t('la.login')}}</van-button>
        <br>
        <br>
      </div>
    <!-- </Touch> -->
  </div>
</template>

<script>
// import Touch from '@/components/Touch.vue'
import Vue from 'vue'
import {
  login,
  register
} from '@/api/CommonFun'
import NavBar from '@/components/NavBar.vue'
import {
  Icon,
  Field,
  Button,
  Toast
} from 'vant'
Vue.use(NavBar).use(Icon).use(Field).use(Button).use(Toast)
export default {
  name: 'Login',
  components: {
    NavBar
    // Touch
  },
  data () {
    return {
      username: '',
      password: '',
      isLogin: false,
      loginIsShow: false
    }
  },
  mounted () {
    this.outLogin()
  },
  methods: {
    enter () {
      if (this.loginIsShow) {
        this.login()
      } else {
        this.register()
      }
    },
    registerShow () {
      this.loginIsShow = false
    },
    loginShow () {
      this.loginIsShow = true
    },
    outLogin () {
      this.isLogin = localStorage.getItem('isLogin')
      if (this.isLogin) {
        // this.$store.commit('setTransition', 'turn-off')
        mui.openWindow({
          url: '/',
          id: 'home',
          waiting: {
            autoShow: false
          }
        })
        // this.$router.push('/')
      }
    },
    login () {
      var _this = this
      if (this.username === '' || this.password === '') {
        Toast({
          position: 'bottom',
          message: '账号密码不能为空哟'
        })
      } else {
        login(this.username, this.password).then((data) => {
          if (data.data === 'success') {
            localStorage.setItem('isLogin', true)
            localStorage.setItem('username', _this.username)
            // this.$store.commit('setTransition', 'turn-off')
            _this.$router.push('/Person')
            // mui.openWindow({
            //   url: 'Person',
            //   id: 'Person',
            //   waiting: {
            //     autoShow: false
            //   }
            // })
          } else {
            Toast({
              position: 'bottom',
              message: '亲爱的密码错了哟'
            })
          }
        })
      }
    },
    register () {
      var _this = this
      if (this.username === '' || this.password === '') {
        Toast({
          position: 'bottom',
          message: '账号密码不能为空哟'
        })
      } else {
        register(this.username, this.password).then((data) => {
          if (data.data === 'success') {
            localStorage.setItem('isLogin', true)
            localStorage.setItem('username', _this.username)
            // this.$store.commit('setTransition', 'turn-off')
            _this.$router.push('/Person')
            // mui.openWindow({
            //   url: 'Person',
            //   id: 'Person',
            //   waiting: {
            //     autoShow: false
            //   }
            // })
          } else {
            Toast({
              position: 'bottom',
              message: '用户名已经被别人用啦'
            })
          }
        })
      }
    }
    // changeInput1 () {
    //   console.log(this.$refs.focus)
    //   this.$refs.focus1.$el.style = 'width:90%;border-color: #03a9f5;box-shadow: 0 0 5px #03a9f5;margin-top:2em'
    //   this.$refs.focus1.$el.className = 'input center van-cell van-field'
    // },
    // blurInput1 () {
    //   this.$refs.focus1.$el.style = 'width:90%;margin-top:2em;margin-top:2em'
    //   this.$refs.focus1.$el.className = 'input center van-cell van-field'
    // },
    // changeInput2 () {
    //   this.$refs.focus2.$el.style = 'width:90%;border-color: #03a9f5;box-shadow: 0 0 5px #03a9f5;margin-top:2em'
    //   this.$refs.focus2.$el.className = 'input center van-cell van-field'
    // },
    // blurInput2 () {
    //   this.$refs.focus2.$el.style = 'width:90%;margin-top:2em;margin-top:2em'
    //   this.$refs.focus2.$el.className = 'input center van-cell van-field'
    // }
  }
}
</script>

<style scoped>
  .van-button--info {
    background: rgba(255, 255, 255, 0.7);
    border: 1px solid #c1cdda
  }

  .van-button--plain.van-button--info {
    color: #7a8b9c
  }

  .titlePoet {
    font-size: 16px;
    text-align: center;
    color: #7a8b9c;
    font-weight: bold;
    font-family: "微软雅黑";
  }

  .allDiv {
    background-image: url('/img/share.jpg');
    height: 100vh;
  }

  .login {
    margin-top: 200px;
    width: 90%;
    border-radius: 20px;
    transform: translateX(5%);
    background: rgba(255, 255, 255, 0.7);
  }

  .input {
    background: transparent;
    /* padding: 12px 5px 12px 15px;
    height: 47px; */
  }

  .van-field__label {
    width: 30px
  }

  .van-cell__title {
    flex: none
  }

  .center {
    z-index: 1;
    left: 50%;
    transform: translateX(-50%);
  }
</style>
