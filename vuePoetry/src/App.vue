<template>
  <div id="app" ref="app" :style="{fontFamily:font}">
    <Touch>
      <div style="height:100vh"  slot="down">
        <keep-alive>
          <router-view v-if="this.$route.meta.keepAlive" class="child-view"></router-view>
        </keep-alive>
        <router-view v-if="!this.$route.meta.keepAlive" class="child-view"></router-view>
      </div>

    </Touch>
  </div>
</template>

<script>
import Touch from '@/components/Touch.vue'
export default {
  data () {
    return {
      font: '微软雅黑 '
    }
  },
  components: {
    Touch
  },
  mounted () {
    this.changFont()
  },
  methods: {
    changFont () {
      this.font = localStorage.getItem('font')
      console.log(this.font)
    }
  }
}
</script>
<style>
h2{
  font-size:20px !important;
  }
input{
  font-family: '微软雅黑' !important
}
  .pre {
    white-space: pre-wrap;
    word-wrap: break-word;
  }

  .overlayer {
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 998;
    background-color: rgba(0, 0, 0, .3);
    display: flex;
    justify-content: center;
    align-items: Center;
  }

  .tochHeight {
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 10;
    height: 100vh
  }

  .child-view {
    position: absolute;
    background-color: #fff !important;
    width: 100%;
    height:100vh
  }

  /* .turn-on-enter {
    transform: translate3d(100%, 0, 0);
  } */

  /* .turn-on-leave-to { */
    /* transform: translate3d(-100%, 0, 0); */
  /* } */

  /*
  .turn-on-enter-active,
  .turn-on-leave-active {
     z-index: 20;
    transition: transform .3s ease;
  } */

  /* .turn-off-enter { */
    /* transform: translate3d(-100%, 0, 0); */
  /* } */

  /* .turn-off-leave-to {
    transform: translate3d(100%, 0, 0);
  }

  .turn-off-enter-active,
  .turn-off-leave-active {
    transition: transform .3s ease;
  }

  .turn-off-leave-active {
    z-index: 20;
  } */
</style>
