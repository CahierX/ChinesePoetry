<template>
  <div>
    <NavBar msg='诗词名句' url='' />

    <!-- <Touch> -->
      <!-- <div slot="down"> -->
        <div v-show="isLoading" class="overlayer" @touchmove.prevent>
          <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
        </div>
        <van-pull-refresh v-model="isLoading" @refresh="onRefresh" loading-text="" class="animated fadeInUp">
          <van-list v-show="!isLoading" v-model="loading" :loading="loading" :finished="finished" finished-text="没有更多了"
            @load="onLoad" :immediate-check="loading">
            <div class="mui-card" v-for="item in poetryList" :key="item.author" @click="openDetails(item.poetryId)">
              <div class="mui-card-header">
                <span style="font-size:17px;padding:15px">{{item.poetry}}</span>
              </div>
              <div class="mui-card-content">
                <img v-lazy="item.image" style="width:100%;height:200px;overflow:hidden;" />
              </div>
              <div class="mui-card-footer" style="text-align:center;float:none">
                <span style="font-size:15px;">{{item.author}}</span>
              </div>
            </div>
          </van-list>
        </van-pull-refresh>
      <!-- </div> -->
    <!-- </Touch> -->
  </div>
</template>

<script>
import Vue from 'vue'
// import Touch from '@/components/Touch.vue'
import {
  List,
  Cell,
  Lazyload,
  PullRefresh
} from 'vant'
import {
  SearchPoetrySentence
} from '@/api/CommonFun'
import NavBar from '@/components/NavBar.vue'
Vue.use(List).use(Cell).use(Lazyload, {
  error: '/img/error.jpg',
  loading: '/img/loading.gif'
}).use(PullRefresh)
export default {
  name: 'PoetrySentence',
  components: {
    NavBar
    // Touch
  },
  data () {
    return {
      poetryList: [],
      loading: false,
      finished: false,
      nowPage: 2,
      poetryName: [],
      poetryImage: [],
      isLoading: true
    }
  },
  mounted () {
    this.initPoetry()
  },
  methods: {
    onRefresh () {
      setTimeout(() => {
        this.initPoetry()
        this.poetryList = []
      }, 500)
    },
    openDetails (poetryId) {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push('./PoetryDetails?poetryId=' + poetryId)
      mui.openWindow({
        url: './PoetryDetails?poetryId=' + poetryId,
        waiting: {
          autoShow: false
        }
      })
    },
    initPoetry () {
      var _this = this
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      SearchPoetrySentence(1, type).then((data) => {
        for (var i in data.data) {
          _this.poetryList = _this.poetryList.concat(data.data[i])
        }
        _this.isLoading = false
      })
    },
    onLoad () {
      // 异步更新数据
      setTimeout(() => {
        var _this = this
        var type = false
        if (window.localStorage.getItem('language') === 'tw') {
          type = true
        }
        SearchPoetrySentence(this.nowPage, type).then((data) => {
          for (var i in data.data) {
            _this.poetryList = _this.poetryList.concat(data.data[i])
          }
        })
        _this.nowPage++
        // 加载状态结束
        _this.loading = false
      }, 500)
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  .van-cell__label {
    /* font-size:1em !important; */
    color: #323233 !important;
  }

  .van-cell--large .van-cell__title {
    font-size: 1em
  }

  .van-cell--large .van-cell__label {
    font-size: 1.3em
  }
</style>
