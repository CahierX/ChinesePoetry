<template>
  <div>
    <NavBar :msg="$t('la.recommend')" url='' />
    <!-- <Touch> -->
      <div style="text-align:center">
        <div v-show="isLoading" class="overlayer" @touchmove.prevent>
          <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
        </div>
        <van-pull-refresh v-model="isLoading" @refresh="onRefresh">
          <van-list v-show="!isLoading" v-model="loading" :loading="loading" :finished="finished" finished-text="没有更多了"
            @load="onLoad" :immediate-check="loading">
            <div style="white-space: pre-line;" class="PoetryFont">
              <van-cell class="animated fadeInUp" v-for="item in poetryList" :key="item.name"
                :title="item.name" :label="item.poetryContent" clickable size="large" @click="openPoetryDetails(item.poetryId)" />
            </div>
          </van-list>
        </van-pull-refresh>
      </div>
  <!-- </Touch> -->
  </div>
</template>

<script>
import NavBar from '@/components/NavBar.vue'
// import Touch from '@/components/Touch.vue'
import Vue from 'vue'
import {
  List,
  Cell,
  Toast,
  PullRefresh
} from 'vant'
import {
  StarTop
} from '@/api/CommonFun'
Vue.use(List).use(Cell).use(Toast).use(PullRefresh)
export default {
  name: 'PoetryStarTop',
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
      isLoading: true
    }
  },
  mounted () {
    this.initPoetry()
  },
  methods: {
    openPoetryDetails (index) {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push(`./PoetryDetails?poetryId=` + index)
      mui.openWindow({
        url: 'PoetryDetails?poetryId=' + index,
        waiting: {
          autoShow: false
        }
      })
    },
    onRefresh () {
      setTimeout(() => {
        this.initPoetry()
        this.isLoading = false
      }, 500)
    },
    initPoetry () {
      var _this = this
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      _this.poetryList = []
      StarTop(1, type).then((data) => {
        for (var i in data.data) {
          _this.poetryList = _this.poetryList.concat(data.data[i])
        }
        _this.isLoading = false
      })
    },
    onLoad () {
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      // 异步更新数据
      setTimeout(() => {
        var _this = this
        StarTop(this.nowPage, type).then((data) => {
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
    color: #323233;
  }
.van-cell:not(:last-child)::after{
    border-bottom:2px solid #c6d7f1
  }
</style>
