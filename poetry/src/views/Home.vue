<template>
  <div>
    <van-nav-bar fixed style="height:71px">
      <van-icon name="smile-o" size="1.7em" slot="left" @click="back()" style="margin-top:-5px"/>
      <van-icon name="setting-o" size="1.6em" slot="right" @click="open()" style="margin-top:-5px"/>
    </van-nav-bar>
       <van-pull-refresh v-model="isLoading" @refresh="onRefresh" pulling-text=' ' loosing-text=" " loading-text=" " style="height:100vh">
    <vue-star
      animate="animated jackInTheBox"
      style="animation-duration: 1000ms;"
      color="#F05654"
      class="poetCenter"
    >
      <img src="/img/pika.png" slot="icon" alt="刘亦菲" class="round">
    </vue-star>
    <div style="margin-top:20em" class="animated fadeIn">
      <van-field
        style="width:95%;margin-left:7px"
        v-model="searchKey"
        class="input center"
        ref="focus"
        @keyup.enter.9="showSearch()"
        :placeholder="$t('la.love')"
      >
      <button type="button" slot="button" @click="showSearch()" class="mui-btn mui-btn-outlined" style="margin-top:-15px;margin-left:-62px;height:40px;border:0px;border-left:1px solid rgba(0, 0, 0, .2)">搜索</button>
      </van-field>
      <br>
      <van-cell class="animated fadeInUp"  style="animation-duration: 900ms" :title="$t('la.fil')" icon="contact" :border="false">
        <span @click="openPoetList()" style="color:#03a9f5">{{$t('la.more')}}</span>
      </van-cell>
      <div class="sentence animated fadeInUp" style="text-align:center;width:100%">
        <span
          class="poetry"
          style="color: rgb(3, 169, 245);padding: 8px;"
          @click="openPoet('白居易')"
        >白居易</span>
        <span
          class="poetry"
          style="color: rgb(3, 169, 245);padding: 8px;"
          @click="openPoet('苏轼')"
        >{{$t('la.sushi')}}</span>
        <span
          class="poetry"
          style="color: rgb(3, 169, 245);padding: 8px;"
          @click="openPoet('杜甫')"
        >杜甫</span>
      </div>
      <van-cell class="animated fadeInUp"  style="animation-duration: 1000ms" :title="$t('la.filterPoetry')" icon="filter-o" :border="false">
        <span @click="openFilterPoetry()" style="color:#03a9f5">{{$t('la.more')}}</span>
      </van-cell>
      <div class="sentence animated fadeInUp" style="text-align:center;width:100%">
        <span
          class="poetry"
          style="color: rgb(3, 169, 245);padding: 8px;"
          @click="searchDynasty('民国末当代初')"
        >{{$t('la.minguomo')}}</span>
        <span
          class="poetry"
          style="color: rgb(3, 169, 245);padding: 8px;"
          @click="searchTags('七夕节')"
        >{{$t('la.qixi')}}</span>
        <span
          class="poetry"
          style="color: rgb(3, 169, 245);padding: 8px;"
          @click="searchDynasty('近现代')"
        >{{$t('la.jinxiandai')}}</span>
        <span
          class="poetry"
          style="color: rgb(3, 169, 245);padding: 8px;"
          @click="searchDynasty('当代')"
        >{{$t('la.dangdai')}}</span>
      </div>
      <van-cell class="animated fadeInUp"  style="animation-duration: 1100ms" :title="$t('la.select')" icon="bulb-o" :border="false">
        <span @click="openPoetrySentenceList()" style="color:#03a9f5;">{{$t('la.more')}}</span>
      </van-cell>
      <div class="sentence animated fadeInUp" style="text-align:center;">
        <span
          class="poetry"
          style="color: rgb(3, 169, 245);padding: 8px ;width:80%;"
          @click="openSingleSentence(poetrySentenceList.poetryId)"
        >{{poetrySentenceList.poetry}}</span>
      </div>
    </div>
      </van-pull-refresh>
  </div>
</template>
<script>
// Toast 这些都是在网上粘的别人的。但是找不到出处了，大佬见谅。
import { SearchRandomSentence } from '@/api/CommonFun'
import VueStar from 'vue-star'
import Vue from 'vue'
import { NavBar, Icon, Field, Cell, PullRefresh } from 'vant'
Vue.use(NavBar)
  .use(Icon)
  .use(Field)
  .use(Cell)
  .use(PullRefresh)
export default {
  name: 'home',
  data () {
    return {
      show: false,
      searchKey: '', // 搜索的关键字
      poetrySentenceList: [],
      username: '',
      isLoading: true
    }
  },
  components: {
    VueStar
  },
  updated () {
    var page = mui.preload({
      url: 'FilterPoetry',
      id: 'FilterPoetry'
    })
    var page1 = mui.preload({
      url: 'Person',
      id: 'Person'
    })
    var page2 = mui.preload({
      url: 'PoetrySentence',
      id: 'PoetrySentence'
    })
    var page3 = mui.preload({
      url: 'PoetrySentence',
      id: 'PoetrySentence'
    })
    var page4 = mui.preload({
      url: 'PoetList',
      id: 'PoetList'
    })
    var page5 = mui.preload({
      url: 'PoetryStarTop',
      id: 'PoetryStarTop'
    })
  },
  mounted () {
    this.SearchRandomSentence()
  },
  methods: {
    onRefresh () {
      setTimeout(() => {
        this.SearchRandomSentence()
        this.isLoading = false
      }, 500)
    },
    openPoet (index) {
      mui.openWindow({
        url: 'PoetDetails?poetName=' + index,
        waiting: {
          autoShow: false
        }
      })
    },
    searchDynasty (word) {
      mui.openWindow({
        url: 'FilterView?tags=&dynasty=' + word + '&state=',
        waiting: {
          autoShow: false
        }
      })
    },
    searchTags (word) {
      mui.openWindow({
        url: 'FilterView?tags=' + word + '&dynasty=&state=',
        waiting: {
          autoShow: false
        }
      })
    },
    openFilterPoetry () {
      mui.openWindow({
        url: 'FilterPoetry',
        id: 'FilterPoetry',
        waiting: {
          autoShow: false
        }
      })
    },
    back () {
      mui.openWindow({
        url: 'PoetryStarTop',
        id: 'PoetryStarTop',
        show: {
          aniShow: 'slide-in-left' // 页面显示动画，默认为”slide-in-right“；
        },
        waiting: {
          autoShow: false
        }
      })
    },
    open () {
      mui.openWindow({
        url: 'Person',
        id: 'Person',
        waiting: {
          autoShow: false
        }
      })
    },
    showSearch () {
      mui.openWindow({
        url: 'SearchForKey?key=' + this.searchKey,
        waiting: {
          autoShow: false
        }
      })
      this.$refs.focus.blur()
      this.searchKey = ''
    },
    openPoetList () {
      this.$store.commit('setTransition', 'turn-on')
      mui.openWindow({
        url: 'PoetList',
        id: 'PoetList',
        waiting: {
          autoShow: false
        }
      })
    },
    openPoetrySentenceList () {
      mui.openWindow({
        url: 'PoetrySentence',
        id: 'PoetrySentence',
        waiting: {
          autoShow: false
        }
      })
    },
    openSingleSentence (poetryId) {
      mui.openWindow({
        url: 'PoetryDetails?poetryId=' + poetryId,
        waiting: {
          autoShow: false
        }
      })
    },
    SearchRandomSentence () {
      var _this = this
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      SearchRandomSentence(type).then(data => {
        _this.poetrySentenceList = data.data
      })
    }
    // changeInput () {
    //   this.$refs.focus.$el.style = 'width:90%;border-color: #03a9f5;box-shadow: 0 0 5px #03a9f5;'
    // },
    // blurInput () {
    //   this.$refs.focus.$el.style = 'width:90%;'
    // }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.van-nav-bar__title {
  color: #fff;
  margin-top: 26px;
  font-size: 16px;
  font-weight: 700;
}

.van-nav-bar__text {
  color: #fff;
  font-size: 16px;
  font-weight: 700;
}

.van-nav-bar__title {
  color: #fff;
}

.van-nav-bar .van-icon {
  color: #fff;
}

.van-nav-bar {
  background-color: #03a9f5;
}

.poetry {
  color: #323233;
  font-size: 0.8em;
}

.poetCenter {
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  margin-top: -10em;
}

.round {
  width: 14em;
  /* height: 12em; */
  display: flex;
  /* border-radius: 50%; */
  align-items: center;
  justify-content: center;
  /* overflow: hidden; */
}

.center {
  z-index: 1;
  left: 50%;
  transform: translateX(-50%);
}

.sentence {
  display: flex;
  justify-content: center;
  align-items: center;
}

</style>
