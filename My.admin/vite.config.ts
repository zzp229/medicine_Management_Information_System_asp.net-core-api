import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    host: "127.0.0.1",
    port: 3000,
    open: true,
    // 设置本地代理
    proxy: {
      '/api': {
        // 转发地址
        target: "http://localhost:5189/api",
        // target: "http://localhost:5234/api",
        // 启用跨域访问
        changeOrigin: true,
        // 修改请求路径
        rewrite: path => path.replace(/^\/api/, '')
      },
    }
  },
  optimizeDeps: {
    include: ['TreeMenu']
  }
})
