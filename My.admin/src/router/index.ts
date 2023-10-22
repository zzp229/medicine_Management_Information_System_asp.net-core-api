import { createRouter, createWebHistory } from 'vue-router'
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            name: "login",
            path: "/login",
            component: () => import("../views/index/LoginPage.vue")
        },
        {
            name: "notfound",
            path: "/:pathMatch(.*)",
            component: () => import("../views/index/NotFound.vue")
        },
        {
            name: "admin",
            path: "/",
            component: () => import("../views/index/RootPage.vue"),
            children:[
                {
                    name: "desktop",
                    path: "/desktop",
                    component: () => import("../views/index/Desktop.vue")
                },
                {
                    name: "person",
                    path: "/person",
                    component: () => import("../views/index/PersonPage.vue")
                },
                {
                    name: "menu",
                    path: "/menu",
                    component: () => import("../views/admin/menu/menu.vue")
                },
                {
                    name: "role",
                    path: "/role",
                    component: () => import("../views/admin/role/role.vue")
                },
                {
                    name: "user",
                    path: "/user",
                    component: () => import("../views/admin/user/user.vue")
                },
            ]
        },
    ]
})
export default router