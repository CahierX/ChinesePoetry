<template>
  <div>
    <van-nav-bar title="" fixed :left-text="poetryName" left-arrow @click-left="back()" style="height:71px">
      <van-uploader :after-read="onRead" slot="right">
        <van-icon name="photograph" size="1.6em" style="margin-right:25px;margin-top:-5px" />
      </van-uploader>
      <colorPicker v-model="selectColor" slot="right" style="position:absolute;margin-top:16px;margin-top:14px" />
      <van-icon name="edit" size="1.6em" slot="right" @click="showHeight()" style="margin-left:35px;margin-top:-5px" />
    </van-nav-bar>
      <div id="imgDiv" v-show="poetryContent" class="animated fadeIn shareDiv" :style="{ backgroundImage: 'url('+ imgUrl+')'}" slot="down">
        <br>
        <van-slider v-show="isShowHeight" v-model="marginTop" @change="onChange" max='95' />
        <div style="width:90%; transform: translateX(5%);">
          <div class="wrap" :style="{position:'absolute',marginTop:marginTop*5.5+'px'}">
            <div style="margin-top:0em; " class="poetryContent">
              <span class="van-cell" contenteditable="true" :style="{color:selectColor}">{{poetryContent}}</span>
            </div>
            <span :style="{ float:'right', fontSize:'0.7em', marginTop:'-10px', color: selectColor}" contenteditable="true">{{poetryName}}——{{author}}</span>
          </div>
        </div>
      </div>
  </div>
</template>
<script>
import {
  PoetryDetails
} from '@/api/CommonFun'
import Vue from 'vue'
import {
  NavBar,
  Slider,
  Icon,
  Field,
  Cell,
  Uploader
} from 'vant'
Vue.use(NavBar).use(Icon).use(Field).use(Cell).use(Uploader).use(Slider)
export default {
  name: 'home',
  data () {
    return {
      marginTop: 20,
      poetryName: '',
      author: '',
      dynasty: '',
      poetryContent: '',
      selectColor: '#323233',
      imgUrl: '/img/bg.jpg',
      exportImgUrl: '',
      isShowHeight: false
    }
  },
  mounted () {
    this.PoetryDetails()
  },
  methods: {
    onRead (file) {
      this.imgUrl = file.content
    },
    showHeight () {
      this.isShowHeight = !this.isShowHeight
    },
    onChange (value) {
      console.log(value)
      this.marginTop = value
    },
    back () {
      // this.$store.commit('setTransition', 'turn-off')
      // this.$router.go(-1)
      mui.back()
    },
    PoetryDetails () {
      var _this = this
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      PoetryDetails(this.$route.query.poetryId, 1, type).then((data) => {
        _this.poetryName = data.data.name
        _this.author = data.data.author
        _this.poetryContent = data.data.poetryContent
        _this.dynasty = data.data.dynasty
      })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
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
    background-color: #03a9f5
  }

  .van-nav-bar__text {
    color: #fff;
    font-size: 16px;
    font-weight: 700
  }

  .shareDiv {
    height: 100vh;
    margin-top:71px;
    -moz-background-size: 100%;
    background-size: 100%;
  }

  .wrap {
    position: relative;
    margin: 0 auto;
    padding: 1em;
    height: auto;
    background: hsla(0, 0%, 100%, .25) border-box;
    /* overflow: hidden; */
    border-radius: .3em;
    box-shadow: 0 0 0 1px hsla(0, 0%, 100%, .3) inset, 0 .5em 1em rgba(0, 0, 0, 0.6);
    text-shadow: 0 1px 1px hsla(0, 0%, 100%, .3);
  }

  .wrap::before {
    content: '';
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: -1;
    -webkit-filter: blur(20px);
    filter: blur(20px);
  }

  .van-nav-bar .van-icon {
    color: #fff
  }

  .van-cell {
    background-color: rgba(255, 255, 255, 0);
    font-size: 0.75em;
    text-indent: 1em
  }

  .poetryContent .van-cell__value--alone {
    text-indent: 1em;
    line-height: 1.8em
  }
</style>
