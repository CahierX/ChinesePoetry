<template>
  <div>
    <NavBar :msg="this.$route.query.state+' '+this.$route.query.dynasty+' '+this.$route.query.tags" url='back' />
    <!-- <Touch> -->
      <!-- <div slot="down"> -->
        <div v-show="isLoading" class="overlayer" @touchmove.prevent>
          <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
        </div>
        <div v-show="noData" style="margin-top:-50px" class="animated fadeIn">
          <NoData :word="this.$route.query.key" />
        </div>
        <van-pull-refresh v-model="loading" v-show="!isLoading" @refresh="onRefresh" pulling-text='小仙女' loosing-text='小可爱'
          style="text-align:center;">
          <div v-if="!noData">
            <van-list v-model="loading" :loading="loading" :finished="finished" finished-text="没有更多了" :immediate-check=false
              @load="onload">
              <div style="white-space: pre-line;" class="PoetryFont">
                <van-cell v-for="item in poetryList" :key="item.name" :title="item.name" :label="item.poetryContent"
                  clickable size="large" @click="openPoetry(item.poetryId)" />
              </div>
            </van-list>
          </div>
        </van-pull-refresh>
      <!-- </div> -->
    <!-- </Touch> -->
  </div>

</template>

<script>
import Vue from 'vue'
// import Touch from '@/components/Touch.vue'
import NavBar from '@/components/NavBar.vue'
import NoData from '@/components/NoData.vue'
import {
  Icon,
  PullRefresh,
  Cell,
  List
} from 'vant'
import {
  SearchPoetryFromTags
} from '@/api/CommonFun'
Vue.use(Icon).use(PullRefresh).use(Cell).use(List)
export default {
  name: 'FilterView',
  components: {
    NavBar,
    NoData
    // Touch
  },
  data () {
    return {
      // searchWord: '', // 搜索的关键字
      poetryList: [],
      isLoading: true,
      loading: false,
      finished: false,
      page: 1,
      noData: false, // 为找到数据
      isShow: false
    }
  },
  mounted () { // 开启了keep-alive 再次进入页面只触发activated
    this.onRefresh()
  },
  methods: {
    openPoetry (index) {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push('./PoetryDetails?poetryId=' + index)
      mui.openWindow({
        url: 'PoetryDetails?poetryId=' + index,
        waiting: {
          autoShow: false
        }
      })
    },
    onRefresh () {
      this.isShow = true
      var _this = this
      _this.page = 1
      _this.poetryList = []
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      SearchPoetryFromTags(this.page, this.$route.query.tags, this.$route.query.dynasty, this.$route.query.state,
        type).then((data) => {
        for (var i in data.data) {
          _this.poetryList = _this.poetryList.concat(data.data[i])
        }
        if (_this.poetryList.length !== 0) {
          _this.noData = false
        } else {
          _this.noData = true
        }
        _this.page++
        // 加载状态结束
        _this.loading = false
        _this.isLoading = false
        _this.$forceUpdate()
      })
    },
    onload () {
      // 异步更新数据
      setTimeout(() => {
        var _this = this
        var type = false
        if (window.localStorage.getItem('language') === 'tw') {
          type = true
        }
        SearchPoetryFromTags(this.page, this.$route.query.tags, this.$route.query.dynasty, this.$route.query.state,
          type).then((data) => {
          if (data.data.length === 0) {
            _this.finished = true
            _this.loading = false
          } else {
            //   _this.poetryList = _this.poetryList.concat(data.data[i])
            if (data.data.length === 0) {
              _this.finished = true
              _this.loading = false
            } else {
              for (var i in data.data) {
                _this.poetryList = _this.poetryList.concat(data.data[i])
              }
            }
          }
        })
        _this.page++
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
</style>
