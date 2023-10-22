import axios from 'axios'
import store from '../store/index'
import ApiResult from '../class/ApiResult'
import { ElMessage } from 'element-plus'
import router from '../router/index'
const instance = axios.create({
    timeout: 3000,
    headers: {
        "Content-Type": "application/json",
        // 这这里无法使用pinia
        // "Authorization":"Bearer "+"你的token"
    }
})

// 拦截请求
instance.interceptors.request.use(
    config => {
        // 全局状态管理需要在这里获取，否则会提示没有初始化引用（需要安装什么的）
        config.headers.Authorization = "Bearer " + store().token
        return config
    }
)
// 拦截响应
instance.interceptors.response.use(
    response => {
        // 拿到请求结果后统一返回，并设置返回结果
        const res: ApiResult = response.data
        if (!res.IsSuccess) {
            ElMessage.error(res.Msg)
        }
        // 对于业务模块，只需关注结果，无需做验证提示
        return res.Result
    },
    // 请求异常走这里
    error => {
        // 请求的配置对象
        const originalRequest = error.config
        if (!originalRequest) return Promise.reject(error)

        // 根据不同状态妈啊，做出不同的响应
        // 1、401表示未授权=>跳到登录页
        if (error.response.status === 401) {
            router.push({
                path: "/login"
            })
        } else if (error.response.status === 500) {
            ElMessage.error("内部服务器错误，请检查服务是否启动！")
        } else if (error.response.status === 404) {
            ElMessage.error("接口地址不存在，请检查接口地址！")
        } else if ((JSON.stringify(error)).indexOf('time out') > 1) {
            ElMessage.error("接口超时！")
        }
        // ...

        // 请求失败就返回错误信息
        return Promise.reject(error)
    }
)

export const getToken = (obj: {}) => {
    console.log("getToken了")
    return instance.post(`/api/Login/GetToken`, obj)
}

// 获取菜单
export const getTreeMenu = (obj: {}) => {
    return instance.post(`/api/Menu/GetMenus`, obj)
}

