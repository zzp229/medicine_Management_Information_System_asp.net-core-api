<template>
    <!-- 需要内嵌的页面都放在这里 -->
    <el-container>
        <el-aside style="width:inherit;">
            <el-menu :collapse="isCollapse" router :unique-opened="true"
                style="height: 100vh;background-color:blanchedalmond;" @select="handleSelect"
                :default-active="router.currentRoute.value.path">
                <el-sub-menu index="/desktop">
                    <template #title>
                        <IconCom icon="house"></IconCom>
                        <span>我的主页</span>
                    </template>
                    <el-menu-item index="/">
                        <IconCom icon="wallet"></IconCom>
                        <span>工作台</span>
                    </el-menu-item>
                    <el-menu-item index="/person">
                        <IconCom icon="monitor"></IconCom>
                        <span>个人信息</span>
                    </el-menu-item>
                </el-sub-menu>
                <TreeMenu :list="list"></TreeMenu>
            </el-menu>
        </el-aside>
        <el-container>
            <el-header>
                <HeaderCom></HeaderCom>
                <!-- <IconCom icon="expand"></IconCom> -->
            </el-header>
            <el-main> <router-view></router-view></el-main>
        </el-container>
    </el-container>
</template>
<script setup lang="ts">
import TreeMenu from '../../components/TreeMenu.vue'
import TreeModel from '../../class/TreeModel'
import { computed, ref } from 'vue';
import HeaderCom from '../../components/HeaderCom.vue';
import useStore from '../../store/index';
import { handleSelect } from '../../tool/index'
import router from '../../router';
import IconCom from '../../components/IconCom.vue'
import store from '../../store/index';
const list = computed(() => store().UserMenus)
const isCollapse = computed(() => useStore().isCollapse)
</script>