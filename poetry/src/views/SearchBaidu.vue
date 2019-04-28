<template>
  <div>
    <NavBar :msg="$route.query.key" url='back' />

    <!-- <Touch> -->
      <!-- <div slot="down"> -->
        <div v-show="isLoading" class="overlayer" @touchmove.prevent>
          <vue-loading type="bars" color="#03a9f5" :size="{ width: '50px', height: '50px' }"></vue-loading>
        </div>
        <div style="padding:15px;" v-show="!isLoading" slot="down">
          <!-- <span style="color:#323233;line-height:2.5em;font-size:1px;text-indent:2em;list-sytle:none;" v-html="baiduPoetry"></span> -->
          <van-tabs class="animated fadeIn" style="animation-duration: 1000ms" animated color="#03a9f5"
            title-active-color="#2196F3">
            <van-tab :title="$route.query.key">
              <div style="padding:15px">
                <span style="color:#323233;line-height:2.5em;font-size:16px;text-indent:2em;list-sytle:none;" v-html="baiduPoetry"></span>
              </div>
            </van-tab>
          </van-tabs>
        </div>
      <!-- </div> -->
    <!-- </Touch> -->
  </div>
</template>

<script>
// import Touch from '@/components/Touch.vue'
import {
  getPoetryBaidu
} from '@/api/CommonFun'
import NavBar from '@/components/NavBar.vue'
import Vue from 'vue'
import {
  Tab,
  Tabs
} from 'vant'
Vue.use(Tab).use(Tabs)
export default {
  name: 'SearchBaidu',
  components: {
    NavBar
    // Touch
  },
  data () {
    return {
      baiduPoetry: '',
      isLoading: true
    }
  },
  mounted () {
    this.onRefresh()
  },
  methods: {
    onRefresh () {
      var _this = this
      var type = false
      if (window.localStorage.getItem('language') === 'tw') {
        type = true
      }
      getPoetryBaidu(this.$route.query.key, type).then((data) => {
        _this.baiduPoetry = data.data
        _this.isLoading = false
      })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
