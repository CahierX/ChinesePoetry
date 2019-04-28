<template>
  <div>
    <NavBar msg="我的收藏" url='Person' />
    <!-- <Touch> -->
      <!-- <div slot="down"> -->
        <div v-show="isLoading" class="overlayer" @touchmove.prevent>
          <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
        </div>
        <van-pull-refresh v-model="isLoading" @refresh="onRefresh" slot="down" class="touchHeight">
          <van-list v-show="!isLoading" v-model="loading" :loading="loading" class="animated fadeIn"
            :finished="finished" finished-text="没有更多了" @load="onLoad" :immediate-check="loading">
            <div style="white-space: pre-line;text-align:center">
              <van-cell :title="item.name" v-for="item in poetryList" :key="item.name" :label="item.poetryContent"
                clickable size="large" @click="openPoetry(item.poetryId)" />
            </div>
          </van-list>
        </van-pull-refresh>
      <!-- </div>
    </Touch> -->
  </div>
</template>

<script>
// import Touch from '@/components/Touch.vue'
import Vue from 'vue'
import NavBar from '@/components/NavBar.vue'
import {
  collectList
} from '@/api/CommonFun'
import {
  List,
  Cell,
  Toast,
  PullRefresh
} from 'vant'
Vue.use(List).use(Cell).use(Toast).use(PullRefresh)
export default {
  name: 'Collect',
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
    openPoetry (index) {
      this.$store.commit('setTransition', 'turn-on')
      mui.openWindow({
        url: 'PoetryDetails?poetryId=' + index,
        waiting: {
          autoShow: false
        }
      })
      // this.$router.push('./PoetryDetails?poetryId=' + index)
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
      collectList(1, type).then((data) => {
        _this.poetryList = data.data
        _this.isLoading = false
      })
    },
    onLoad () {
      var _this = this
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      // 异步更新数据
      setTimeout(() => {
        collectList(this.nowPage, type).then((data) => {
          _this.poetryList = _this.poetryList.concat(data.data)
          _this.isLoading = false
          _this.nowPage++
          // 加载状态结束
          _this.loading = false
          console.log(data)
          if (data.data.length === 0) {
            _this.finished = true
          }
        })
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

  .delButton {
    display: flex;
    justify-content: center;
    align-items: Center;
  }
</style>
