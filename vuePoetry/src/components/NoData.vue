<template>
  <div style="text-align:center;font-size:17px;color:rgb(123, 119, 119);margin-top:100px" @click="searchBaidu()">
    <span>{{$t('la.noData')}}</span>
    <img src="/img/noData.png" class="andimated fadeIn" style="width:100%;" alt="亲爱的我没能找到数据">
  </div>
</template>

<script>
export default {
  name: 'NoData',
  props: {
    word: String
  },
  methods: {
    searchBaidu () {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push('/SearchBaidu?key=' + this.word)
      mui.openWindow({
        url: 'SearchBaidu?key=' + this.word,
        waiting: {
          autoShow: false
        }
      })
    }
  }
}
</script>

<style scoped>

</style>
