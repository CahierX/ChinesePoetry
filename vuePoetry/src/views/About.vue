<template>
  <div>
    <NavBar msg="肖肖" homeTitie="小可爱" url='Person' />
    <div style="background-color:#fff;padding:15px" class="touchHeight" slot="down">
      <span class="title">勿怪</span>
      <div class="font" style="text-indent:2em">
        <span>本软件部分页面参考诗鲸，因本人很喜欢诗鲸的风格，还请作者勿怪。</span>
      </div>
      <span class="title">诞生故事</span>
      <div class="font" style="text-indent:2em;font-weight:300">
        <span>我的小可爱是一位很喜欢诗词的姑娘，之前一直在使用诗鲸软件。有一天我看到了一位站长为他女朋友做了一个网站，里面放了她写的所有诗词还有他们的故事。我就萌发了做个软件给小可爱的念头，但是暂时不知道做什么就搁置了。一次偶然的机会在Github上发现了古诗词的数据包，因此我就做了这款软件。献给我的小可爱。
        </span>
      </div>
      <span class="title">感谢</span>
      <div class="font" style="text-indent:2em">
        <span>古诗词数据来源于古诗词网</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px">
        <span>图片数据来源于百度</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px">
        <span>部分数据来源于百度百科</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px">
        <span>部分图片来源于互联网</span>
      </div>
      <span class="title">开源框架</span>
      <div class="font" style="text-indent:2em;font-weight:300">
        <span>Vant</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px;font-weight:300">
        <span>Vant</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px;font-weight:300">
        <span>Vue-Star</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px;font-weight:300">
        <span>Vue-Touch</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px;font-weight:300">
        <span>Vue-loading-template</span>
      </div>
      <span class="title">未来开发计划</span>
      <div class="font" style="text-indent:2em;">
        <span>诗词学习功能，给出半句回答后半句</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px;">
        <span>中国传统节日诗词专属页面</span>
      </div>
      <div class="font" style="text-indent:2em;margin-top:-20px;">
        <span>更换字体</span>
      </div>
      <div style="white-space: pre-line;" class="PoetryFont animated fadeIn">
        <div style="height:300px;padding:25px;text-align:center; box-shadow: 0 0 0 1px hsla(0, 0%, 100%, .9) inset, 0 .2em .2em rgba(0, 0, 0, 0.1);">
          <img src="/img/about.jpg" style="width:100%;overflow:hidden;" />
          <span class="font">人生太短，而你太美</span><br>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import NavBar from '@/components/NavBar.vue'
export default {
  name: 'About',
  components: {
    NavBar
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .title {
    color: #232323;
    font-weight: 300;
    margin-left: 20px;
  }

  .font {
    font-size: 17px;
    padding: 15px;
    font-weight: 200;
    color: #232323
  }
</style>
