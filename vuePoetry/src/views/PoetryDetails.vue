<template>
  <div>
    <van-nav-bar fixed :left-text="poetry.name" left-arrow @click-left="back()" style="height:71px">
      <van-icon v-show="!noData && !collectState" name="star-o" size="1.6em" slot="right" @click="addCollect()" style="margin-top:-6px" />
      <van-icon v-show="!noData && collectState" name="star" color='#f44336' size="1.6em" slot="right" @click="delCollect()"
        style="margin-top:-6px" />
      <van-icon v-show="!noData" name="share" size="1.6em" slot="right" @click="share()" style="margin-left:25px;margin-top:-3px" />
    </van-nav-bar>
        <div v-show="isLoading" class="overlayer" @touchmove.prevent>
          <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
        </div>
        <van-pull-refresh v-model="loading" v-show="!isLoading" @refresh="onRefresh()" style="margin-top:71px;" class="animated fadeIn">
          <div v-show="!noData">
            <img :src="poetry.image" style="width:100%" @click="ImageShow()">
            <br> <br>
            <div class="animated fadeInUp"  style="text-align:center;animation-duration: 700ms">
              <span class="poetryName">{{poetry.name}}</span><br>
              <span class="poetryAuthor">{{poetry.dynasty}}·{{poetry.author}}</span>
              <div style="margin-top:0em;padding:10px;" class="pre"><van-cell :value="poetry.poetryContent" /></div>
              <div v-if="isShowTag">
                <van-tag color="#03a9f5" size="large" style="margin-top:10px" plain v-for="item in searchImageKey" :key="item" @click="searchTags(item)">{{item}}</van-tag>
              </div>
            </div>
            <br v-show="!noPoet"><br v-show="!noPoet">
            <div style="width:85%;height:auto;margin:auto;animation-duration: 800ms;margin-top:1.2em" class="animated fadeInUp" @click="openPoet()" v-show="!noPoet">
              <img slot="icon" style="position:absolute;margin-top:-2.6em" class="round poetCenter" :src="poetImg" />
              <van-cell class="poetIntroduce pre" clickable :value="poetIntroduce" />
            </div>
            <br>
            <van-tabs swipeable color="#03a9f5" title-active-color="#2196F3" v-show="isHaveBaidu || isHaveShangxi || isHaveAbout"  class="animated fadeInUp" style="animation-duration: 600ms;">
              <van-tab v-if="isHaveBaidu" title="百度百科">
                <div style="padding:15px">
                  <span style="color:#323233;line-height:2.5em;font-size:16px;text-indent:2em;list-sytle:none;" v-html="poetry.baiduPoetry"></span>
                </div>
              </van-tab>
              <van-tab v-if="isHaveShangxi" :title="$t('la.appreciate')">
                <van-cell class="pre" :value="poetry.shangxi"/>
              </van-tab>
              <van-tab v-if="isHaveAbout" :title="$t('la.aboutPoetry')">

                <van-cell class="pre"  :value="poetry.about" />
              </van-tab>
            </van-tabs>
          </div>
          <div v-show="noData" style="margin-top:-50px" class="animated fadeIn">
            <NoData word="我爱你" />
          </div>
        </van-pull-refresh>
  </div>
</template>

<script>
// import Touch from '@/components/Touch.vue'
import {
  PoetryDetails,
  SearchPoetImage,
  addCollect,
  delCollect,
  isCollect
} from '@/api/CommonFun'
import NoData from '@/components/NoData.vue'
import Vue from 'vue'
import {
  NavBar,
  Collapse,
  CollapseItem,
  Cell,
  CellGroup,
  Tag,
  ImagePreview,
  PullRefresh,
  Tab,
  Tabs,
  Toast
} from 'vant'
Vue.use(NavBar).use(Collapse).use(CollapseItem).use(Cell).use(CellGroup).use(Tag).use(ImagePreview).use(PullRefresh).use(
  Tab).use(Tabs).use(Toast)
export default {
  name: 'PoetryDetails',
  components: {
    NoData
    // Touch
  },
  data () {
    return {
      isHaveBaidu: true,
      isHaveShangxi: true,
      isHaveAbout: true,
      currentEncoding: true,
      targetEncoding: true,
      stopDOM: ['BR', 'TIME', 'IMG', 'CANVAS', 'SCRIPT'],

      noData: false, // 未找到数据
      refreshNum: -1, // 刷新的次数
      noPoet: false, // 找不到作者不显示框
      isShowTag: true,
      loading: false,
      isLoading: true,
      activeNames: ['0'],
      searchImageKey: [],
      poetName: '',
      poetImg: '',
      poetIntroduce: '',
      collectState: false,
      poetry: {
        name: '',
        dynasty: '',
        author: '',
        poetryContent: '',
        shangxi: '',
        about: '',
        tags: [],
        image: '',
        baiduPoetry: ''
      }
    }
  },
  mounted () {
    this.onRefresh()
    this.isCollect()
  },
  methods: {
    openPoet () {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push('/PoetDetails?poetName=' + this.poetName)
      mui.openWindow({
        url: 'PoetDetails?poetName=' + this.poetName,
        waiting: {
          autoShow: false
        }
      })
    },
    searchTags (word) {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push({
      //   path: 'FilterView',
      //   query: {
      //     tags: word,
      //     dynasty: '',
      //     state: ''
      //   }
      // })
      mui.openWindow({
        url: 'FilterView?tags=' + word + '&dynasty=&state=',
        waiting: {
          autoShow: false
        }
      })
    },
    isCollect () {
      var _this = this
      if (this.$route.query.poetryId === '0') {
        this.collectState = false
        this.noData = true
      } else {
        isCollect(this.$route.query.poetryId).then((data) => {
          _this.collectState = data.data
        })
      }
    },
    addCollect () {
      var _this = this
      this.isLogin = localStorage.getItem('isLogin')
      if (this.isLogin) {
        var data = {
          'poetryId': this.$route.query.poetryId
        }
        addCollect(data).then((data) => {
          if (data.data === 'success') {
            _this.collectState = true
            Toast({
              position: 'bottom',
              message: '收藏成功'
            })
          } else {
            Toast({
              position: 'bottom',
              message: '出现错误'
            })
          }
        })
      } else {
        this.$store.commit('setTransition', 'turn-on')
        // _this.$router.push('/Login')
        mui.openWindow({
          url: 'Login',
          id: 'Login'
        })
      }
    },
    delCollect () {
      var _this = this
      this.isLogin = localStorage.getItem('isLogin')
      if (this.isLogin) {
        var data = {
          'poetryId': this.$route.query.poetryId
        }
        delCollect(data).then((data) => {
          if (data.data === 'success') {
            _this.collectState = false
            Toast({
              position: 'bottom',
              message: '删除成功'
            })
          } else {
            Toast({
              position: 'bottom',
              message: '出现错误'
            })
          }
        })
      } else {
        this.$store.commit('setTransition', 'turn-on')
        // _this.$router.push('/Login')
        mui.openWindow({
          url: 'Login',
          id: 'Login',
          waiting: {
            autoShow: false
          }
        })
      }
    },
    share () {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push('/Share?poetryId=' + this.$route.query.poetryId)
      mui.openWindow({
        url: 'Share?poetryId=' + this.$route.query.poetryId,
        id: 'Share',
        waiting: {
          autoShow: false
        }
      })
      // 分享的代码
    },
    back () {
      // this.$store.commit('setTransition', 'turn-off')
      mui.back()
      // this.$router.go(-1)
    },
    ImageShow () {
      ImagePreview([
        this.poetry.image
      ])
    },
    onRefresh () {
      var _this = this
      _this.refreshNum++ // 刷新的次数
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      PoetryDetails(this.$route.query.poetryId, _this.refreshNum, type).then((data) => {
        if (data.data === 'error') {
          _this.noData = true
          _this.isLoading = false
          _this.loading = false
        } else {
          _this.searchImageKey = []
          data = data.data
          _this.poetry.name = data.name
          _this.poetry.poetryContent = data.poetryContent
          _this.poetry.shangxi = data.shangxi
          _this.poetry.about = data.about
          _this.poetry.author = data.author
          _this.poetry.dynasty = data.dynasty
          _this.poetName = data.author
          _this.poetry.image = data.image
          _this.poetry.baiduPoetry = data.baiduPoetry

          if (data.shangxi === null) {
            _this.isHaveShangxi = false
          }
          if (data.about === null) {
            _this.isHaveAbout = false
          }
          if (data.baiduPoetry === null) {
            _this.isHaveBaidu = false
          }
          _this.poetry.tags = data.tags.split(',')
          for (var i = 0; i < _this.poetry.tags.length; i++) {
            if (i === 0 && _this.poetry.tags.length === 1) {
              _this.searchImageKey = _this.searchImageKey.concat(_this.poetry.tags[i].substring(6, _this.poetry.tags[i].length - 4))
            } else if (i === 0) {
              _this.searchImageKey = _this.searchImageKey.concat(_this.poetry.tags[i].substring(6, _this.poetry.tags[i].length - 1))
            } else if (i === _this.poetry.tags.length - 1) {
              _this.searchImageKey = _this.searchImageKey.concat(_this.poetry.tags[i].substring(5, _this.poetry.tags[i].length - 4))
            } else {
              _this.searchImageKey = _this.searchImageKey.concat(_this.poetry.tags[i].substring(5, _this.poetry.tags[i].length - 1))
            }
          }
          if (_this.searchImageKey.length === 1) {
            _this.isShowTag = false
          }
          _this.isLoading = false
          _this.loading = false
          _this.SearchPoetImage()
        }
      }).catch((e) => {
        _this.isLoading = false
        _this.loading = false
      })
    },
    SearchPoetImage () {
      var _this = this
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      SearchPoetImage(_this.poetName, type).then((data) => {
        if (data.data === 'NoPoet') {
          _this.noPoet = true
        } else {
          _this.poetImg = data.data.image
          _this.poetIntroduce = data.data.minIntroduce
          if (_this.poetImg === null) {
            _this.poetImg = './img/avatar.png'
          }
        }
      })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
/* pre{
  font-family: '楷体'
} */
  .van-tag {
    margin-left: 1em
  }

  .VueStar__ground {
    width: 100% !important;
    height: 100% !important;
  }

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
    width: 200px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }

  .van-nav-bar {
    text-align: left;
  }

  .van-tab__pane {
    overflow: auto
  }

  .poetIntroduce {
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 3;
    /*超出3行显示省略号*/
    overflow: hidden;
    border: 1px solid #4b6b88;
    border-radius: 0.5em;
    height: 7em;
    padding: 15px;
    text-indent: 2em
  }

  .van-nav-bar .van-icon {
    color: #fff
  }

  .van-nav-bar {
    z-index: 2 !important;
    background-color: #03a9f5
  }

  .van-cell {
    font-size: 16px;
    line-height: 2em;
  }

  .van-cell__value--alone {
    text-align: center;
  }

  .poetCenter {
    z-index: 999;
    position: absolute;
    left: 50%;
    transform: translateX(-50%);
    /* 移动元素本身50% */
  }

  .round {
    width: 60px;
    height: 60px;
    display: flex;
    border-radius: 50%;
    align-items: center;
    justify-content: center;
    overflow: hidden;
  }

  .textCenter {
    text-align: center !important;
  }

  .poetryContent .van-cell__value--alone {
    text-align: center !important;
  }

  .poetryName {
    font-size: 1em;
  }

  .poetryAuthor {
    font-size: 0.8em;
    font-weight: lighter
  }

</style>
