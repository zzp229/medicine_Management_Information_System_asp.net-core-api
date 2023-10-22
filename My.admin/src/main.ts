import { createApp } from 'vue'
import './assets/css/global.scss'
import App from './App.vue'
import router from './router'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import { createPinia } from 'pinia'
import piniaPluginPersist from 'pinia-plugin-persist'   //持久化管理的插件
import zhCn from 'element-plus/dist/locale/zh-cn.mjs'
const app = createApp(App)
app.use(router)
app.use(ElementPlus, {
    locale: zhCn,
})
app.use( createPinia().use(piniaPluginPersist))    //注意这里需要括号，因为创建Pinia Store实例注册到应用中，其它的在vue上下文中工作
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}
app.mount('#app')
