<template>
  <div style="margin-top:71px;">
    <van-nav-bar :title="homeTitie" fixed :left-text="msg" left-arrow @click-left="back()" style="height:71px">
      <!-- <van-icon name="setting-o" size="1.6em" slot="right" @click="open()" style="margin-top:-5px"/> -->
    </van-nav-bar>
  </div>

</template>

<script>
import Vue from 'vue'
import {
  NavBar,
  Icon
} from 'vant'
Vue.use(NavBar).use(Icon)
export default {
  name: 'NavBar',
  data () {
    return {
      show: false
    }
  },
  props: {
    msg: String,
    homeTitie: String,
    url: String
  },
  methods: {
    back () {
      mui.back()
    }
    // open () {
    //   this.$router.push('./Person')
    // }
  }
}
</script>

<style scoped>
  .van-nav-bar__title {
    color: #fff;
    margin-top: 26px;
    font-size: 16px;
    font-weight: 700
  }

  .van-nav-bar__text {
    color: #fff;
    font-size: 16px;
    font-weight: 700;
  }

  .van-nav-bar .van-icon {
    color: #fff
  }

  .van-nav-bar {
    z-index: 2 !important;
    background-color: #03a9f5
  }
</style>
