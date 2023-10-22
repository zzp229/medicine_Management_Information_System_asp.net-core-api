<template>
  <el-card class="box-card">
      <el-row>
          <el-col :span="5">
              <el-input v-model="searchVal" placeholder="Please input" @change="Search" />
          </el-col>
          <el-col :span="12">
              <el-button type="primary" @click="Search">查询</el-button>
              <el-button @click="open" type="primary">新增</el-button>
              <el-button type="primary" @click="SettingMenu">分配菜单</el-button>
          </el-col>
      </el-row>
      <br>
      <el-row>
          <el-col>
              <el-table :data="tableData" style="width: 100%;height: 65vh;" border ref="tb">
                  <el-table-column type="selection" width="55" />
                  <el-table-column prop="Order" label="排序" width="80" />
                  <el-table-column prop="Name" label="名称" width="120" />
                  <el-table-column prop="IsEnable" label="是否启用" width="100">
                      <template #default="scope">
                          <el-switch v-model="scope.row.IsEnable" disabled />
                      </template>
                  </el-table-column>
                  <el-table-column prop="Description" label="描述" />
                  <el-table-column prop="CreateUserName" label="创建人" width="80" />
                  <el-table-column prop="CreateDate" label="创建时间" />
                  <el-table-column prop="ModifyUserName" label="修改人" width="80" />
                  <el-table-column prop="ModifyDate" label="修改时间" />
                  <el-table-column prop="IsDelete" label="是否删除" width="90" />
                  <el-table-column label="操作" align="center">
                      <template #default="scope">
                          <el-button size="small" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
                          <el-button size="small" type="danger"
                              @click="handleDelete(scope.$index, scope.row)">Delete</el-button>
                      </template>
                  </el-table-column>
              </el-table>
              <el-pagination style="margin-top: 10px;" background layout="prev, pager, next" :total="total" />
          </el-col>
      </el-row>
  </el-card>
  <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add>
  <setting :isShow="isShow_Set" :rids="rids" @closeSettingAdd="closeSettingAdd" @settingSuccess="settingSuccess">
  </setting>
</template>

<script lang="ts" setup>
import { ElMessage, ElTable } from 'element-plus';
import { onMounted, reactive, Ref, ref } from 'vue';
import RoleRes from '../../../class/RoleRes';
import { delRole, getRoles } from '../../../http';
import add from './add.vue';
import setting from './setting.vue';
const total = ref(0)
const tableData = ref<Array<RoleRes>>([])
const parms = ref({
  Name: "",
  Description: "",
  PageIndex: 1,
  PageSize: 10
})
const load = async () => {
  let res = await getRoles(parms.value) as any
  tableData.value = res.Data
  total.value = res.Total
}
onMounted(async () => {
  await load()
})

const searchVal = ref("")
const Search = async () => {
  parms.value.Name = searchVal.value
  await load()
}


// -------------------- 新增、修改、删除逻辑 Start --------------------
const isShow = ref(false)
const open = () => {
  isShow.value = true
}
const closeAdd = () => {
  isShow.value = false
  info.value = new RoleRes()
}
const info: Ref<RoleRes> = ref<RoleRes>(new RoleRes());
const handleEdit = (index: number, row: RoleRes) => {
  info.value = row
  isShow.value = true
}
const success = async (message: string) => {
  isShow.value = false
  info.value = new RoleRes()
  ElMessage.success(message)
  await load()
}
const handleDelete = async (index: number, row: RoleRes) => {
  await delRole(row.Id)
  await load()
}
// -------------------- 新增、修改、删除逻辑 End ----------------------


// ------------------- 分配菜单逻辑 Start --------------------
const tb = ref<InstanceType<typeof ElTable>>()

const isShow_Set = ref(false)
const rids = ref("")
const SettingMenu = () => {
  // 判断是否选中了角色
  // 应该只支持单个设置，因为点开界面时存在反选逻辑，如果选择的多个角色它们的菜单不一致，则无法反选
  let rows = tb.value?.getSelectionRows()
  if (rows.length > 0) {
      rids.value = tb.value?.getSelectionRows().map((item: RoleRes) => item.Id).join(',')
      isShow_Set.value = true
  } else {
      ElMessage.warning("请选择需要分配菜单的角色")
  }
}
const closeSettingAdd = () => {
  isShow_Set.value = false
}
const settingSuccess = async () => {
  isShow_Set.value = false
}
// -------------------- 分配菜单逻辑 End ----------------------
</script>
<style lang="scss" scoped></style>  