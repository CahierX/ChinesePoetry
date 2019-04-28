/* 引入Vue框架 */
import Vue from 'vue'
/* 引入资源请求插件 */
import axios from 'axios'
/* 使用axios插件 */
// axios.defaults.withCredentials = true
// axios.defaults.timeout = 6000

// axios.defaults.headers.post['Content-Type'] = 'application/json'
Vue.prototype.$axios = axios
export default ({
})
