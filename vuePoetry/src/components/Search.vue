<template>
  <div>
    <NavBar :msg="this.$route.query.key" url='back' />
        <div v-show="isLoading" class="overlayer" @touchmove.prevent>
          <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
        </div>
        <van-pull-refresh v-model="loading" v-show="!isLoading" @refresh="onRefresh" style="text-align:center;">
          <div v-if="!noData">
            <van-list v-model="loading" :loading="loading" :finished="finished" finished-text="没有更多了" :immediate-check=false
              @load="onload">
              <div style="white-space: pre-line;">
                <ul class="mui-table-view" v-for="item in poetryList" :key="item.name" @click="openPoetry(item.poetryId)">
                  <li class="mui-table-view-cell">
                      <div  v-html="item.onlyname" style="font-weight:500;font-size:18px;padding:5px" />
                      <div  v-html="item.dynasty +'·'+item.author" style="font-weight:320;font-size:14px;"/>
                      <div  v-html="item.poetryContent" style="padding:10px;font-size:16px;"/>
                  </li>
                </ul>
              </div>
            </van-list>
          </div>
          <div v-show="noData" style="margin-top:-50px" class="animated fadeIn">
            <NoData :word="this.$route.query.key" />
          </div>
        </van-pull-refresh>
  </div>

</template>

<script>
import Vue from 'vue'
import NavBar from '@/components/NavBar.vue'
import NoData from '@/components/NoData.vue'
import {
  Icon,
  PullRefresh,
  Cell,
  List
} from 'vant'
import {
  SearchPoetry
} from '@/api/CommonFun'
Vue.use(Icon).use(PullRefresh).use(Cell).use(List)
export default {
  components: {
    NavBar,
    NoData
  },
  props: {
    fun: [Boolean]
  },
  data () {
    return {
      // searchWord: '', // 搜索的关键字
      poetryList: [],
      isLoading: true,
      loading: false,
      finished: false,
      page: 1,
      noData: false // 为找到数据
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
      var _this = this
      _this.page = 1
      _this.poetryList = []
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      SearchPoetry(_this.$route.query.key, _this.page, type, this.fun).then((data) => {
        for (var i in data.data) {
          if (this.$route.query.key && this.$route.query.key.length > 0) {
            // 匹配关键字正则
            let replaceReg = new RegExp(this.$route.query.key, 'g')
            // 高亮替换v-html值
            let replaceString = '<span style="color:red">' + this.$route.query.key + '</span>'
            // 开始替换
            data.data[i].onlyname = data.data[i].onlyname.replace(replaceReg, replaceString)
            data.data[i].dynasty = data.data[i].dynasty.replace(replaceReg, replaceString)
            data.data[i].author = data.data[i].author.replace(replaceReg, replaceString)
            data.data[i].poetryContent = data.data[i].poetryContent.replace(replaceReg, replaceString)
          }
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
        SearchPoetry(_this.$route.query.key, _this.page, type, this.fun).then((data) => {
          if (data.data.length === 0) {
            _this.finished = true
            _this.loading = false
          } else {
            for (var i in data.data) {
              if (this.$route.query.key && this.$route.query.key.length > 0) {
                // 匹配关键字正则
                let replaceReg = new RegExp(this.$route.query.key, 'g')
                // 高亮替换v-html值
                let replaceString = '<span style="color:red">' + this.$route.query.key + '</span>'
                // 开始替换
                data.data[i].onlyname = data.data[i].onlyname.replace(replaceReg, replaceString)
                data.data[i].dynasty = data.data[i].dynasty.replace(replaceReg, replaceString)
                data.data[i].author = data.data[i].author.replace(replaceReg, replaceString)
                data.data[i].poetryContent = data.data[i].poetryContent.replace(replaceReg, replaceString)
              }
              _this.poetryList = _this.poetryList.concat(data.data[i])
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
