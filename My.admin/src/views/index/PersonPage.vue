<template>
    <el-row>
        <el-col>
            <el-card>
                <el-form :model="form" label-width="80px">
                    <el-form-item label="用户名">
                        <el-input v-model="form.name" />
                    </el-form-item>
                    <el-form-item label="密码">
                        <el-input v-model="form.password" />
                    </el-form-item>
                    <el-form-item label="头像">
                        <el-upload class="avatar-uploader" :action="fromAction" :show-file-list="false"
                            :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload">
                            <img v-if="imageUrl" :src="imageUrl" class="avatar" />
                            <el-icon v-else class="avatar-uploader-icon">
                                <Plus />
                            </el-icon>
                        </el-upload>
                    </el-form-item>
                    <el-form-item label="存储方式">
                        <el-radio-group v-model="form.uploadMode">
                            <el-radio label="1" size="large">本地存储</el-radio>
                            <el-radio label="2" size="large">七牛云</el-radio>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="onSubmit">Create</el-button>
                        <el-button>Cancel</el-button>
                    </el-form-item>
                </el-form>
            </el-card>
            <el-card>
                <p style="color:red;">注意事项</p>
                <p>当前不支持短信验证相关功能，请谨慎修改密码</p>
                <p>单机部署可以选择本地存储，多服务器负载请上传到云</p>
                <p>目前内置七牛云，测试阶段使用</p>
                <p>......</p>
            </el-card>
        </el-col>
    </el-row>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import { ElMessage, UploadProps } from 'element-plus'
const form = reactive({
    name: "",
    password: "",
    imageUrl: "01.jpeg",
    uploadMode: "1"
})

const imageUrl = ref(form.imageUrl)
const fromAction = ref("/api/File/UploadFile?mode=2")

// 上传前的校验
const beforeAvatarUpload: UploadProps['beforeUpload'] = (rawFile) => {
    if (!(rawFile.type == 'image/png' || rawFile.type == 'image/jpeg')) {
        ElMessage.error('Avatar picture must be JPG format!')
        return false
    } else if (rawFile.size / 1024 / 1024 > 2) {
        ElMessage.error('Avatar picture size can not exceed 2MB!')
        return false
    }
    return true
}
// 上传
const handleAvatarSuccess: UploadProps['onSuccess'] = (
    response,
    uploadFile
) => {
    console.log(response.Result)
    imageUrl.value = URL.createObjectURL(uploadFile.raw!)
}

const onSubmit = () => {

}

</script>


<style scoped>
.avatar-uploader .avatar {
    width: 178px;
    height: 178px;
    display: block;
}

.avatar-uploader .el-upload {
    border: 1px dashed var(--el-border-color);
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: var(--el-transition-duration-fast);
}

.avatar-uploader .el-upload:hover {
    border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    text-align: center;
}

.el-card {
    float: left;
    width: 500px;
    margin-left: 20px;
}
</style>