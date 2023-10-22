<template>
  <el-card class="box-card">
    <el-row>
      <el-col :span="5">
        <el-input v-model="searchVal" placeholder="Please input" @change="Search" />
      </el-col>
      <el-col :span="12">
        <el-button type="primary" @click="Search">查询</el-button><el-button type="primary">新增</el-button>
      </el-col>
    </el-row>
    <br>
    <el-row>
      <el-col>
        <el-table :data="tableData" style="width: 100%;height: 65vh;" border row-key="id">
          <el-table-column type="index" label="index" width="70" />
          <el-table-column prop="date" label="Date" width="180" />
          <el-table-column prop="name" label="Name" width="180" />
          <el-table-column prop="address" label="Address" />
          <el-table-column label="Operations" align="center">
            <template #default="scope">
              <el-button size="small" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
              <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">Delete</el-button>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination style="margin-top: 10px;" background layout="prev, pager, next" :total="1000" />
      </el-col>
    </el-row>
  </el-card>
</template>
  
<script lang="ts" setup>
import { onMounted, ref } from 'vue';
const searchVal = ref("")
const Search = () => {
    if (searchVal.value != '') {
        tableData.value = orilist.filter(p => p.name.indexOf(searchVal.value) > -1)
    } else {
        tableData.value = orilist
    }
}
const orilist = [
    {
        id: "2",
        date: '2016-05-02',
        name: 'Tom2',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "3",
        date: '2016-05-04',
        name: 'Tom3',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "2",
        date: '2016-05-02',
        name: 'Tom2',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "3",
        date: '2016-05-04',
        name: 'Tom3',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "2",
        date: '2016-05-02',
        name: 'Tom2',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "3",
        date: '2016-05-04',
        name: 'Tom3',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "2",
        date: '2016-05-02',
        name: 'Tom2',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "3",
        date: '2016-05-04',
        name: 'Tom3',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "2",
        date: '2016-05-02',
        name: 'Tom2',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        id: "3",
        date: '2016-05-04',
        name: 'Tom3',
        icon: "edit",
        address: 'No. 189, Grove St, Los Angeles',
    },
]
const tableData = ref(orilist)
const handleEdit = (index: number, row: {}) => { }
const handleDelete = (index: number, row: {}) => { }

</script>
  