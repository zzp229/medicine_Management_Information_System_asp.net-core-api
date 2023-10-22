<template>
    <el-menu-item :index="item.Index" v-if="item.Children.length === 0">
        <el-icon><Notebook /></el-icon>
        <template #title><span>{{ item.Name }}</span></template>
    </el-menu-item>

    <el-sub-menu :index="item.Index" v-else>
        <template #title>
            <el-icon><Folder /></el-icon>
            <span>{{ item.Name }}</span>
        </template>
        <!-- key加上了会好点 -->
        <TreeMenu v-for="it in item.Children" :key="it.Index" :obj="it"></TreeMenu>
    </el-sub-menu>

    <!-- 配合不了收缩 -->
    <!-- <div v-for="item in list">
        <el-menu-item :index="item.Index" v-if="item.Children.length === 0">
            <el-icon><icon-menu /></el-icon>
            <span>{{ item.Name }}</span>
        </el-menu-item>

        <el-sub-menu :index="item.Index" v-else>
            <template #title>
                <el-icon>
                    <location />
                </el-icon>
                <span>{{ item.Name }}</span>
            </template>
            <TreeMenu :list="item.Children"></TreeMenu>
        </el-sub-menu>
    </div> -->
</template>

<script setup lang="ts">
import { PropType } from 'vue';
import type TreeModel from '../class/TreeModel';

const props = defineProps({
    // ?
    // list: Array<TreeModel>
    obj: Object as PropType<TreeModel> //接口作为参数的处理方法
})

const item: TreeModel = props.obj as TreeModel  //转换一下，要不然使用item会报空值警告
</script>