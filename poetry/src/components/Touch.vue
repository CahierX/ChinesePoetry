<template>
  <v-touch @swiperight="onSwipeRight" class="swiperTouch">
    <slot name="down"></slot>
  </v-touch>
</template>
<script>
export default {
  methods: {
    onSwipeRight () {
      this.$store.commit('setTransition', 'turn-off')
      mui.back()
    }
  }
}
</script>
<style>
</style>
