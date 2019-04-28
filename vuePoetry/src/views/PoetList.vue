<template>
  <div>
    <NavBar msg="诗人列表" url/>
    <!-- <Touch> -->
    <!-- <div slot="down"> -->
    <div v-show="isLoading" class="overlayer" @touchmove.prevent>
      <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
    </div>
    <van-pull-refresh v-model="isLoading" @refresh="onRefresh">
      <van-list
        v-show="!isLoading"
        v-model="loading"
        :loading="loading"
        :finished="finished"
        finished-text="没有更多了"
        @load="onLoad"
        :immediate-check="loading"
      >
        <!-- <ul class="mui-table-view">
          <li class="mui-table-view-cell mui-media" v-for="item in poetList" :key="item.name">
            <a href="javascript:;">
              <img class="mui-media-object mui-pull-left round" :src=item.image >
              <div class="mui-media-body">
                {{ item.name}}
                <p class='mui-ellipsis'>{{item.minIntroduce}}</p>
              </div>
            </a>
          </li>
        </ul>-->
        <div style="white-space: pre-line;" class="PoetryFont van-ellipsis">
          <van-cell
            class="animated fadeIn"
            style="animation-duration: 1000ms;height:6.2em"
            v-for="item in poetList"
            :key="item.name"
            :title="item.name"
            :label="item.minIntroduce"
            clickable
            size="large"
            @click="openPoet(item.name)"
          >
            <img
              slot="icon"
              style="float:right"
              class="round van-hairline--right"
              :src="item.image"
            >
          </van-cell>
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
import { List, Cell, PullRefresh } from 'vant'
import { SearchPoet } from '@/api/CommonFun'
import NavBar from '@/components/NavBar.vue'

Vue.use(List)
  .use(Cell)
  .use(PullRefresh)
export default {
  name: 'PoetList',
  components: {
    NavBar
    // Touch
  },
  props: {
    msg: String
  },
  data () {
    return {
      poetList: [],
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
    openPoet (index) {
      mui.openWindow({
        url: 'PoetDetails?poetName=' + index,
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
      _this.poetList = []
      SearchPoet(1, type).then(data => {
        for (var i in data.data) {
          if (data.data[i].image === null) {
            data.data[i].image = './img/avatar.png'
          }
          _this.poetList = _this.poetList.concat(data.data[i])
          _this.isLoading = false
        }
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
        SearchPoet(this.nowPage, type).then(data => {
          for (var i in data.data) {
            if (data.data[i].image === null) {
              data.data[i].image = './img/avatar.png'
            }
            _this.poetList = _this.poetList.concat(data.data[i])
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
  color: #676161 !important;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  -webkit-line-clamp: 2;
  /*超出2行显示省略号*/
  overflow: hidden;
}

.van-cell {
  padding: 1.5em;
}

.poetCenter {
  z-index: 1;
  left: 50%;
  transform: translateX(-50%);
}

.round {
  width: 50px;
  height: 50px;
  display: flex;
  border-radius: 50%;
  align-items: center;
  justify-content: center;
  overflow: hidden;
  position: relative;
  top: 60%;
  transform: translate(0, -50%);
  float: left;
  margin-right: 1em;
}
</style>
