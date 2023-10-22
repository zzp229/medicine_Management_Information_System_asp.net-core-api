<template>
    <el-menu-item :index="item.Index" v-if="item.Children.length == 0">
        <el-icon>
            <Notebook />
        </el-icon>
        <template #title><span>{{ item.Name }}</span></template>
    </el-menu-item>
    <el-sub-menu :index="item.Index" v-else>
        <template #title>
            <el-icon>
                <Folder />
            </el-icon>
            <span>{{ item.Name }}</span>
        </template>
        <TreeMenu v-for="it in item.Children" :obj="it" :key="it.Index"></TreeMenu>
    </el-sub-menu>
</template>
<script setup lang="ts">
import { PropType } from 'vue';
import TreeModel from '../class/TreeModel';
const props = defineProps({
    obj: Object as PropType<TreeModel>
})

// 处理和修饰变量
const item: TreeModel = props.obj as TreeModel
</script>