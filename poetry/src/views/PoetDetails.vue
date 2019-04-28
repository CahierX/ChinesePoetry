<template>
  <div>
    <!-- <NavBar :msg="poet" url='back' /> -->
    <van-nav-bar :left-text="poet" left-arrow @click-left="back()" fixed style="height:71px;">
      <van-icon name="search" size="1.8em" slot="right" @click="searchPoet()" style="margin-top:-5px;margin-right:15px" />
    </van-nav-bar>
    <!-- <Touch> -->
      <!-- <div slot="down" class="tochHeight"> -->
        <div v-show="isLoading" class="overlayer" @touchmove.prevent>
          <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
        </div>
        <div v-show="!isLoading" slot="down" style="margin-top:70px">
          <vue-star animate="animated jackInTheBox" style="animation-duration: 1000ms;z-index:1;position:absolute !important;z-index:0"
            color="#F05654" class="poetCenter">
            <img slot="icon" class="round" :src="poetImg" />
          </vue-star>
          <div style="padding:5px; ">
            <div style="margin-top:100px;animation-duration: 600ms" class="animated fadeInUp pre"><van-cell :value="poetMinIntroduce" /></div>
            <div style="animation-duration: 500ms" class="animated fadeInUp pre"><van-cell :value="poetIntroduce" /></div>
          </div>
        </div>
      <!-- </div> -->
    <!-- </Touch> -->
  </div>
</template>

<script>
// import Touch from '@/components/Touch.vue'
import {
  SearchPoetImage
} from '@/api/CommonFun'
import Vue from 'vue'
import {
  NavBar,
  Collapse,
  CollapseItem,
  Loading,
  Cell,
  Tag
} from 'vant'
import VueStar from 'vue-star'
Vue.use(NavBar).use(Collapse).use(CollapseItem).use(Loading).use(Cell).use(Tag)
export default {
  name: 'PoetryDetails',
  components: {
    VueStar
    // Touch
  },
  data () {
    return {
      searchImageKey: [],
      poet: '',
      poetImg: '',
      poetIntroduce: '',
      poetMinIntroduce: '',
      isLoading: true
    }
  },
  mounted () {
    this.initPoet()
  },
  methods: {
    searchPoet () {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push('/SearchPoetryForPoet?key=' + this.$route.query.poetName)
      mui.openWindow({
        url: 'SearchPoetryForPoet?key=' + this.$route.query.poetName,
        waiting: {
          autoShow: false
        }
      })
    },
    back () {
      // this.$store.commit('setTransition', 'turn-off')
      // this.$router.go(-1)
      mui.back()
    },
    initPoet () {
      // 联网请求
      var _this = this
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      SearchPoetImage(this.$route.query.poetName, type).then((data) => {
        _this.searchImageKey = []
        data = data.data
        _this.poet = data.name
        _this.poetIntroduce = data.introduce
        _this.poetMinIntroduce = data.minIntroduce
        _this.poetImg = data.image
        if (_this.poetImg === null) {
          _this.poetImg = './img/avatar.png'
        }
        _this.isLoading = false
      })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
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
    font-weight: 700
  }

  .van-nav-bar__title {
    color: #fff
  }

  .van-nav-bar .van-icon {
    color: #fff
  }

  .van-nav-bar {
    background-color: #03a9f5
  }

  .van-cell {
    font-size: 16px
  }

  .poetCenter {
    z-index: 1;
    left: 50%;
    transform: translateX(-50%);
  }

  .round {
    width: 60px;
    height: 60px;
    display: flex;
    border-radius: 50%;
    align-items: center;
    justify-content: center;
    overflow: hidden;
    ;
  }

  .textCenter {
    text-align: center
  }

  pre {
    white-space: pre-wrap;
    word-wrap: break-word;
  }

</style>
