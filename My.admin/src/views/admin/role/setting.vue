<template>
    <el-dialog v-model="dialogVisible" title="设置菜单" style="width:30vw;height: 50vh;" draggable
        @close="$emit('closeSettingAdd')">
        <div>
            <el-tree-select ref="tree" style="width: 90%;" v-model="value" :data="store().UserMenus" :props="{
                children: 'Children',
                label: 'Name',
                value: 'Id'
            }" :default-checked-keys="[14]" node-key="Id" :render-after-expand="false" show-checkbox multiple />
        </div>
        <template #footer>
            <span class="dialog-footer" style="position: absolute;bottom: 20px;left: 0px;">
                <el-button @click="close()">取消</el-button>
                <el-button type="primary" @click="save()">确定</el-button>
            </span>
        </template>
    </el-dialog>
</template>
<script setup lang="ts">
import { computed, ref } from 'vue';
import store from '../../../store/index'
import { RecursiveRoutes } from '../../../tool';
import { settingMenu } from '../../../http';
const props = defineProps({
    isShow: Boolean,
    rids: String
})
const dialogVisible = computed(() => props.isShow)
const emits = defineEmits(["closeSettingAdd", "settingSuccess"])

const value = ref()

const close = () => {
    emits("closeSettingAdd")
}

const tree = ref()

console.log(store().UserMenus)
const save = async () => {
    // 获取当前选择的树节点，该函数会返回子节点和父节点
    console.log(tree.value.getCheckedNodes())
    // 展开一棵树变为list结构
    console.log(RecursiveRoutes(tree.value.getCheckedNodes()))
    // 通过ES6的map方法，序列化数组中的每一项为JSON字符串，再通过Set集合来排重，最后map返回反序列化后的结果
    const uniqueArr = Array.from(new Set(RecursiveRoutes(tree.value.getCheckedNodes()).map(item => JSON.stringify(item))))
        .map(str => JSON.parse(str));
    // 通过filter过滤掉父节点，返回被选中的子节点
    console.log(uniqueArr.filter(s => s.Children == null).map(item => item.Id))
    // 组合参数 
    let mids = uniqueArr.map(item => item.Id).join(',')
    console.log(props.rids, mids)
    let res = (await settingMenu(props.rids!, mids) as any) as boolean
    if (res) {
        emits("settingSuccess")
    }
}
</script>