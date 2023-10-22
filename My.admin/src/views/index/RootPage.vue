<!-- 需要内嵌的 -->
<template>
    <el-container>
        <!-- 默认的宽度太长了 -->
        <el-aside style="width: inherit;">
            <!-- router：侧边栏关联上路由,
            加上unique-opened就只有一个打开 -->
            <el-menu :collapse="isCollapse" :unique-opened="true" router style="height: 100vh;
            background-color:blanchedalmond;" @select="handleSelect" :default-active="router.currentRoute.value.path">
                <el-sub-menu index="/desktop">
                    <template #title>
                        <IconCom icon="house"></IconCom>
                        <span>我的主页</span>
                    </template>
                    <el-menu-item index="/desktop">
                        <IconCom icon="wallet"></IconCom>
                        <span>工作台</span>
                    </el-menu-item>
                    <el-menu-item index="/person">
                        <IconCom icon="monitor"></IconCom>
                        <span>个人信息</span>
                    </el-menu-item>
                </el-sub-menu>
                <!-- 遍历每一个list -->
                <TreeView :obj="item" :key="item.Index" v-for="item in list"></TreeView>
            </el-menu>

        </el-aside>
        <el-container>
            <el-header>
                <HeaderCom></HeaderCom>

            </el-header>
            <el-main><router-view></router-view></el-main>
        </el-container>
    </el-container>
</template>

<script setup lang="ts">
import TreeView from '../../components/TreeMenu.vue'
import type TreeModel from '../../class/TreeModel'
import { computed, ref } from 'vue';
import HeaderCom from '../../components/HeaderCom.vue';
import useStore from '../../store/index'
import { handleSelect } from '../../tool/index'
import router from '../../router';
import IconCom from '../../components/IconCom.vue';
console.log(`折叠菜单全局状态局${useStore().isCollapse}`)
const list: Array<TreeModel> = [
    {
        "Name": "菜单管理",
        "Index": "/menu",
        "FilePath": "",
        "Children": [
            {
                "Name": "菜单列表",
                "Index": "/menu",
                "Children": [],
                "FilePath": "menu.vue"
            }
        ]
    },
    {
        "Name": "角色管理",
        "Index": "/role",
        "FilePath": "",
        "Children": [
            {
                "Name": "角色列表",
                "Index": "/role",
                "Children": [],
                "FilePath": "role.vue"
            }
        ]
    },
    {
        "Name": "用户管理",
        "Index": "/user",
        "FilePath": "",
        "Children": [
            {
                "Name": "用户列表",
                "Index": "/user",
                "Children": [],
                "FilePath": "user.vue"
            }
        ]
    }
]
const isCollapse = computed(() => useStore().isCollapse)
</script>