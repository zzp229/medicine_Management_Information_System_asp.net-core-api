<template>
  <el-card class="box-card">
    <el-row>
      <el-col :span="5">
        <el-input v-model="searchVal" placeholder="Please input" @change="Search" />
      </el-col>
      <el-col :span="12">
        <el-button type="primary" @click="Search">查询</el-button>
        <el-button type="primary" @click="open">新增</el-button>
      </el-col>
    </el-row>
    <br>
    <el-row>
      <el-col>
        <el-table :data="tableData" :tree-props="{ children: 'Children' }" style="width: 100%;height: 65vh;" border
          row-key="Id">
          <el-table-column prop="Order" label="排序" width="80" />
          <el-table-column prop="Name" label="名称" width="180" />
          <el-table-column prop="Index" label="路径" width="80" />
          <el-table-column prop="Icon" label="图标" width="80">
            <template #default="scope">
              <IconCom :icon="scope.row.Icon"></IconCom>
            </template>
          </el-table-column>
          <el-table-column prop="FilePath" label="组件名" width="180" />
          <el-table-column prop="IsEnable" label="是否启用" width="100">
            <template #default="scope">
              <el-switch v-model="scope.row.IsEnable" disabled />
            </template>
          </el-table-column>
          <el-table-column prop="Description" label="描述" />
          <el-table-column label="Operations" align="center">
            <template #default="scope">
              <el-button size="small" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
              <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">Delete</el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-col>
    </el-row>
  </el-card>
  <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add>
</template>
  
<script lang="ts" setup>
import { ElMessage } from 'element-plus';
import { onMounted, Ref, ref } from 'vue';
import Menu from '../../../class/Menu';
import IconCom from '../../../components/IconCom.vue';
import { delMenu, getTreeMenu } from '../../../http';
import { SettingUserRouter } from '../../../tool';
import add from './add.vue';
let parms = {
  Name: "",
  Index: "",
  FilePath: "",
  Description: ""
}
const tableData: Ref<Array<Menu>> = ref<Array<Menu>>([])
const load = async () => {
  parms.Name = searchVal.value
  tableData.value = await getTreeMenu(parms) as any as Array<Menu>
}
const searchVal = ref("")
const Search = async () => {
  await load()
}
onMounted(load)


// -------------------- 新增、修改、删除逻辑 Start --------------------
const isShow = ref(false)
const open = () => {
  isShow.value = true
}
const closeAdd = () => {
  isShow.value = false
  info.value = new Menu()
}
const info: Ref<Menu> = ref<Menu>(new Menu());
const handleEdit = (index: number, row: Menu) => {
  info.value = row
  isShow.value = true
}
const success = async (message: string) => {
  isShow.value = false
  info.value = new Menu()
  ElMessage.success(message)
  await load()
  // 重载路由
  await SettingUserRouter()
}
const handleDelete = async (index: number, row: Menu) => {
  await delMenu(row.Id)
  await load()
}
// -------------------- 新增、修改、删除逻辑 End ----------------------

</script>
  