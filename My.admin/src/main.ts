import { createApp } from 'vue'
import './assets/css/global.scss'
import App from './App.vue'
import router from './router'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import * as ElementPlusIconsVue from '@element-plus/icons-vue' 
import { createPinia } from 'pinia'
import piniaPluginPersist from 'pinia-plugin-persist'
import zhCn from 'element-plus/dist/locale/zh-cn.mjs'
const app = createApp(App)
app.use(router)
app.use(ElementPlus, {
  locale: zhCn,
})
app.use(createPinia().use(piniaPluginPersist))
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
  }
app.mount('#app')
