<script setup>
import { ref } from "vue";
import { User, Key } from "@element-plus/icons-vue";
import { useLoginStore } from "@/stores/loginStore";
import { useHomeStore } from "@/stores/homeStore";

const loginStore = useLoginStore();
const homeStore = useHomeStore();
const username = ref("");
const password = ref("");
const isLoading = ref(false);
const login = async () => {
    try {
        isLoading.value = true;
        await loginStore.login(username.value, password.value);
        await homeStore.setDataToDefault();
    } catch (error) {
        console.log("login ~ error:", error);
    } finally {
        isLoading.value = false;
    }
};
</script>

<template>
    <div>
        <div class="container">
            <el-row>
                <el-col :span="24">
                    <h1 class="text">LOGIN</h1>
                </el-col>
            </el-row>
            <el-row :gutter="20">
                <el-col :span="7"></el-col>
                <el-col :span="10">
                    <el-input
                        v-model="username"
                        placeholder="Username"
                        clearable
                        :prefix-icon="User"
                        autocomplete="off"
                    >
                    </el-input>
                </el-col>
            </el-row>
            <el-row :gutter="20">
                <el-col :span="7"></el-col>
                <el-col :span="10">
                    <el-input
                        v-model="password"
                        placeholder="Password"
                        show-password
                        autocomplete="off"
                        :prefix-icon="Key"
                    >
                    </el-input>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="10"></el-col>
                <el-col :span="4">
                    <el-button
                        type="primary"
                        round
                        size="large"
                        @click="login()"
                        :loading="isLoading"
                    >
                        Login
                    </el-button>
                </el-col>
                <el-col :span="10"></el-col>
            </el-row>
        </div>
    </div>
</template>

<style scoped>
.container {
    width: 1400px;
    height: 800px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    background-color: #fff;
    padding: 40px;
    border-radius: 4px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}
.el-button {
    width: 100%;
    height: 42px;
    font-size: 16px;
}
.text {
    font-size: 40px;
    font-weight: 700;
    color: #409eff;
    text-align: center;
}
.el-row {
    margin-bottom: 20px;
    width: 100%;
    height: 42px;
}

.el-row:last-child {
    margin-bottom: 0;
}

.el-row:first-child {
    margin-bottom: 40px;
}

.el-input {
    height: 42px;
}
</style>
