import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    states: 'trun-on' // 进栈
  },
  mutations: {
    setTransition (state, states) {
      state.states = states
    }
  }
})

export default store
