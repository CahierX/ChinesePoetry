<template>
  <div>
    <NavBar :msg="$t('la.filterPoetry')" url="" />
    <!-- <Touch style="background-color:#fff"> -->
      <div class="tagDiv animated fadeIn" style="animation-duration:500ms">
        <van-cell title="形式" :border='false' />
        <van-radio-group v-model="state">
            <van-radio name="" class="radio"> <van-tag type="primary" size='large' plain >不限</van-tag></van-radio>
            <van-radio name="诗" class="radio"> <van-tag type="primary" size='large' plain >诗</van-tag></van-radio>
            <van-radio name="词" class="radio"><van-tag type="primary" size='large' plain >词</van-tag></van-radio>
            <van-radio name="曲" class="radio"> <van-tag type="primary"  size='large' plain >曲</van-tag></van-radio>
            <van-radio name="文言文" class="radio"> <van-tag type="primary"  size='large' plain class="van-ellipsis">文言文</van-tag></van-radio>
        </van-radio-group>
         <van-cell title="朝代" :border='false' style="color:#323233" />
          <van-radio-group v-model="dynasty" >
        <van-radio name="" class="radio"> <van-tag type="primary" size='large' plain >不限</van-tag></van-radio>
        <van-radio name="先秦" class="radio"><van-tag type="primary" size='large' plain>先秦</van-tag></van-radio>
        <van-radio name="两汉" class="radio"><van-tag type="primary" size='large' plain>两汉</van-tag></van-radio>
        <van-radio name="魏晋" class="radio"><van-tag type="primary" size='large' plain>魏晋</van-tag></van-radio>
        <van-radio name="南北朝" class="radio"><van-tag type="primary" size='large' plain class="van-ellipsis">南北朝</van-tag></van-radio>
        <van-radio name="隋代" class="radio"><van-tag type="primary" size='large' plain>隋代</van-tag></van-radio>
        <van-radio name="唐代" class="radio"><van-tag type="primary" size='large' plain>唐代</van-tag></van-radio>
        <van-radio name="五代" class="radio"><van-tag type="primary" size='large' plain>五代</van-tag></van-radio>
        <van-radio name="宋代" class="radio"><van-tag type="primary" size='large' plain>宋代</van-tag></van-radio>
        <van-radio name="金朝" class="radio"><van-tag type="primary" size='large' plain>金朝</van-tag></van-radio>
        <van-radio name="元代" class="radio"><van-tag type="primary" size='large' plain>元代</van-tag></van-radio>
        <van-radio name="明代" class="radio"><van-tag type="primary" size='large' plain>明代</van-tag></van-radio>
        <van-radio name="明末清初" class="radio"><van-tag type="primary" size='large' plain>明末清</van-tag></van-radio>
        <van-radio name="清代" class="radio"><van-tag type="primary" size='large' plain>清代</van-tag></van-radio>
        <van-radio name="近现代" class="radio"><van-tag type="primary" size='large' plain>近现代</van-tag></van-radio>
        <van-radio name="民国末当代初" class="radio"><van-tag type="primary" size='large' plain>民国末</van-tag></van-radio>
        <van-radio name="当代" class="radio"><van-tag type="primary" size='large' plain>当代</van-tag></van-radio>
          </van-radio-group>
             <van-cell :title="$t('la.state')" :border='false' />
          <van-radio-group v-model="tags">
        <van-radio name="" class="radio"> <van-tag type="primary" size='large' plain >不限</van-tag></van-radio>
        <van-radio name="写人" class="radio"><van-tag type="primary" size='large' plain>写人</van-tag></van-radio>
        <van-radio name="写景" class="radio"><van-tag type="primary" size='large' plain>写景</van-tag></van-radio>
        <van-radio name="写山" class="radio"><van-tag type="primary" size='large' plain>写山</van-tag></van-radio>
        <van-radio name="写水" class="radio"><van-tag type="primary" size='large' plain>写水</van-tag></van-radio>
        <van-radio name="写风" class="radio"><van-tag type="primary" size='large' plain>写风</van-tag></van-radio>
        <van-radio name="写花" class="radio"><van-tag type="primary" size='large' plain>写花</van-tag></van-radio>
        <van-radio name="写雪" class="radio"><van-tag type="primary" size='large' plain>写雪</van-tag></van-radio>
        <van-radio name="写月" class="radio"><van-tag type="primary" size='large' plain>写月</van-tag></van-radio>
        <van-radio name="春天" class="radio"><van-tag type="primary" size='large' plain>春天</van-tag></van-radio>
        <van-radio name="夏天" class="radio"><van-tag type="primary" size='large' plain>夏天</van-tag></van-radio>
        <van-radio name="秋天" class="radio"><van-tag type="primary" size='large' plain>秋天</van-tag></van-radio>
        <van-radio name="冬天" class="radio"><van-tag type="primary" size='large' plain>冬天</van-tag></van-radio>
        <van-radio name="梅花" class="radio"><van-tag type="primary" size='large' plain>梅花</van-tag></van-radio>
        <van-radio name="菊花" class="radio"><van-tag type="primary" size='large' plain>菊花</van-tag></van-radio>
        <van-radio name="荷花" class="radio"><van-tag type="primary" size='large' plain>荷花</van-tag></van-radio>
        <van-radio name="柳树" class="radio"><van-tag type="primary" size='large' plain>柳树</van-tag></van-radio>
        <van-radio name="咏柳" class="radio"><van-tag type="primary" size='large' plain>咏柳</van-tag></van-radio>
        <van-radio name="抒情" class="radio"><van-tag type="primary" size='large' plain>抒情</van-tag></van-radio>
        <van-radio name="爱情" class="radio"><van-tag type="primary" size='large' plain>爱情</van-tag></van-radio>
        <van-radio name="友情" class="radio"><van-tag type="primary" size='large' plain>友情</van-tag></van-radio>
        <van-radio name="离别" class="radio"><van-tag type="primary" size='large' plain>离别</van-tag></van-radio>
        <van-radio name="送别" class="radio"><van-tag type="primary" size='large' plain>送别</van-tag></van-radio>
        <van-radio name="思念" class="radio"><van-tag type="primary" size='large' plain>思念</van-tag></van-radio>
        <van-radio name="思乡" class="radio"><van-tag type="primary" size='large' plain>思乡</van-tag></van-radio>
        <van-radio name="爱国" class="radio"><van-tag type="primary" size='large' plain>爱国</van-tag></van-radio>
        <van-radio name="边塞" class="radio"><van-tag type="primary" size='large' plain>边塞</van-tag></van-radio>
        <van-radio name="山水" class="radio"><van-tag type="primary" size='large' plain>山水</van-tag></van-radio>
        <van-radio name="田园" class="radio"><van-tag type="primary" size='large' plain>田园</van-tag></van-radio>
        <van-radio name="婉约" class="radio"><van-tag type="primary" size='large' plain>婉约</van-tag></van-radio>
        <van-radio name="豪放" class="radio"><van-tag type="primary" size='large' plain>豪放</van-tag></van-radio>
        <van-radio name="励志" class="radio"><van-tag type="primary" size='large' plain>励志</van-tag></van-radio>
        <van-radio name="哲理" class="radio"><van-tag type="primary" size='large' plain>哲理</van-tag></van-radio>
        <van-radio name="读书" class="radio"><van-tag type="primary" size='large' plain>读书</van-tag></van-radio>
        <van-radio name="惜时" class="radio"><van-tag type="primary" size='large' plain>惜时</van-tag></van-radio>
        <van-radio name="战争" class="radio"><van-tag type="primary" size='large' plain>战争</van-tag></van-radio>
        <van-radio name="民谣" class="radio"><van-tag type="primary" size='large' plain>民谣</van-tag></van-radio>
        <van-radio name="闺怨" class="radio"><van-tag type="primary" size='large' plain>闺怨</van-tag></van-radio>
        <van-radio name="悼亡" class="radio"><van-tag type="primary" size='large' plain>悼亡</van-tag></van-radio>
        <van-radio name="新年" class="radio"><van-tag type="primary" size='large' plain>新年</van-tag></van-radio>
        <van-radio name="女子" class="radio"><van-tag type="primary" size='large' plain>女子</van-tag></van-radio>
        <van-radio name="郊游" class="radio"><van-tag type="primary" size='large' plain>郊游</van-tag></van-radio>
        <van-radio name="赞美" class="radio"><van-tag type="primary" size='large' plain>赞美</van-tag></van-radio>
        <van-radio name="妇女" class="radio"><van-tag type="primary" size='large' plain>妇女</van-tag></van-radio>
        <van-radio name="侠义" class="radio"><van-tag type="primary" size='large' plain>侠义</van-tag></van-radio>
        <van-radio name="采莲" class="radio"><van-tag type="primary" size='large' plain>采莲</van-tag></van-radio>
        <van-radio name="将士" class="radio"><van-tag type="primary" size='large' plain>将士</van-tag></van-radio>
        <van-radio name="农民" class="radio"><van-tag type="primary" size='large' plain>农民</van-tag></van-radio>
        <van-radio name="游侠" class="radio"><van-tag type="primary" size='large' plain>游侠</van-tag></van-radio>
        <van-radio name="游子" class="radio"><van-tag type="primary" size='large' plain>游子</van-tag></van-radio>
        <van-radio name="桃花" class="radio"><van-tag type="primary" size='large' plain>桃花</van-tag></van-radio>
        <van-radio name="自由" class="radio"><van-tag type="primary" size='large' plain>自由</van-tag></van-radio>
        <van-radio name="人生" class="radio"><van-tag type="primary" size='large' plain>人生</van-tag></van-radio>
        <van-radio name="民歌" class="radio"><van-tag type="primary" size='large' plain>民歌</van-tag></van-radio>
        <van-radio name="节日" class="radio"><van-tag type="primary" size='large' plain>节日</van-tag></van-radio>
        <van-radio name="瀑布" class="radio"><van-tag type="primary" size='large' plain>瀑布</van-tag></van-radio>
        <van-radio name="言志" class="radio"><van-tag type="primary" size='large' plain>言志</van-tag></van-radio>
        <van-radio name="宴会" class="radio"><van-tag type="primary" size='large' plain>宴会</van-tag></van-radio>
        <van-radio name="羁旅" class="radio"><van-tag type="primary" size='large' plain>羁旅</van-tag></van-radio>
        <van-radio name="惆怅" class="radio"><van-tag type="primary" size='large' plain>惆怅</van-tag></van-radio>
        <van-radio name="闲适" class="radio"><van-tag type="primary" size='large' plain>闲适</van-tag></van-radio>
        <van-radio name="江南" class="radio"><van-tag type="primary" size='large' plain>江南</van-tag></van-radio>
        <van-radio name="情歌" class="radio"><van-tag type="primary" size='large' plain>情歌</van-tag></van-radio>
        <van-radio name="水乡" class="radio"><van-tag type="primary" size='large' plain>水乡</van-tag></van-radio>
        <van-radio name="友人" class="radio"><van-tag type="primary" size='large' plain>友人</van-tag></van-radio>
        <van-radio name="隐居" class="radio"><van-tag type="primary" size='large' plain>隐居</van-tag></van-radio>
        <van-radio name="秋夜" class="radio"><van-tag type="primary" size='large' plain>秋夜</van-tag></van-radio>
        <van-radio name="组诗" class="radio"><van-tag type="primary" size='large' plain>组诗</van-tag></van-radio>
        <van-radio name="讽刺" class="radio"><van-tag type="primary" size='large' plain>讽刺</van-tag></van-radio>
        <van-radio name="追忆" class="radio"><van-tag type="primary" size='large' plain>追忆</van-tag></van-radio>
        <van-radio name="登高" class="radio"><van-tag type="primary" size='large' plain>登高</van-tag></van-radio>
        <van-radio name="乐府" class="radio"><van-tag type="primary" size='large' plain>乐府</van-tag></van-radio>
        <van-radio name="生活" class="radio"><van-tag type="primary" size='large' plain>生活</van-tag></van-radio>
        <van-radio name="乡村" class="radio"><van-tag type="primary" size='large' plain>乡村</van-tag></van-radio>
        <van-radio name="归乡" class="radio"><van-tag type="primary" size='large' plain>归乡</van-tag></van-radio>
        <van-radio name="景点" class="radio"><van-tag type="primary" size='large' plain>景点</van-tag></van-radio>
        <van-radio name="孤独" class="radio"><van-tag type="primary" size='large' plain>孤独</van-tag></van-radio>
        <van-radio name="少女" class="radio"><van-tag type="primary" size='large' plain>少女</van-tag></van-radio>
        <van-radio name="相思" class="radio"><van-tag type="primary" size='large' plain>相思</van-tag></van-radio>
             <van-cell title="类型-节日" :border='false' />
        <van-radio name="元宵节" class="radio"><van-tag type="primary" size='large' plain class="van-ellipsis">元宵节</van-tag></van-radio>
        <van-radio name="寒食节" class="radio"><van-tag type="primary" size='large' plain class="van-ellipsis">寒食节</van-tag></van-radio>
        <van-radio name="清明节" class="radio"><van-tag type="primary" size='large' plain class="van-ellipsis">清明节</van-tag></van-radio>
        <van-radio name="端午节" class="radio"><van-tag type="primary" size='large' plain class="van-ellipsis">端午节</van-tag></van-radio>
        <van-radio name="七夕节" class="radio"><van-tag type="primary" size='large' plain class="van-ellipsis">七夕节</van-tag></van-radio>
        <van-radio name="中秋节" class="radio"><van-tag type="primary" size='large' plain class="van-ellipsis">中秋节</van-tag></van-radio>
        <van-radio name="重阳节" class="radio"><van-tag type="primary" size='large' plain class="van-ellipsis">重阳节</van-tag></van-radio>
     </van-radio-group>
     <br>
     <br>
     <div style="margin-top:20px" class="filterDiv">
      <span slot="title" class="filterCss" @click="initFilter()" >清空筛选</span>
          <van-button type="info" slot="right-icon" @click="showFilterView()" size="small" style="float:right;margin-top:4px">确定</van-button>
     </div>

      </div>
    <!-- </Touch> -->
  </div>
</template>

<script>
import NavBar from '@/components/NavBar.vue'
// import Touch from '@/components/Touch.vue'
import Vue from 'vue'
import {
  Tag,
  Button,
  Cell,
  RadioGroup,
  Radio

} from 'vant'
Vue.use(Button).use(Tag).use(Cell).use(RadioGroup).use(Radio)
export default {
  name: 'FilterPoetry',
  components: {
    NavBar
    // Touch
  },
  data () {
    return {
      tags: '',
      dynasty: '',
      state: ''
    }
  },
  mounted () {
    // this.selected()
  },
  methods: {
    initFilter () {
      this.state = ''
      this.tags = ''
      this.dynasty = ''
    },
    showFilterView () {
      // this.$store.commit('setTransition', 'turn-on')
      // this.$router.push({ path: 'FilterView', query: { tags: this.tags, dynasty: this.dynasty, state: this.state } })
      mui.openWindow({
        url: './FilterView?tags=' + this.tags + '&dynasty=' + this.dynasty + '&state=' + this.state,
        waiting: {
          autoShow: false
        }
      })
    }
  }
}
</script>

<style scoped>
.radio{
  width:25%;
  float:left
}
.tagDiv{
  padding:0.5em;
  line-height:2em
}
.filterDiv{
  padding:15px
}
.filterCss{
  color:rgb(25, 137, 250);
  font-size: 22px;
  top:50%;
  left:45px;
  width:100%;
  transform:translate(-50%,-50%);
  text-align: center;
}
</style>
