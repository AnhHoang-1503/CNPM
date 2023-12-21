<script setup>
import { ref, onMounted, reactive, watch } from "vue";
import vi from "element-plus/dist/locale/vi.mjs";

const props = defineProps({
    title: {
        type: String,
        default: "Thêm khoản phí mới",
    },
    data: {
        type: Object,
        default: () => ({}),
    },
});

onMounted(() => {
    if (props.data) {
        form.value = props.data;
    }
});

const dialogFormVisible = ref(false);

const form = reactive({
    name: "",
    amount: 0,
    type: null,
});

const showdialog = () => {
    dialogFormVisible.value = true;
};
defineExpose({
    showdialog,
});
const emits = defineEmits(["dataChange"]);

watch(dialogFormVisible, (val) => {
    if (!val) {
        form.name = "";
        form.amount = 0;
        form.type = null;
    }
});
</script>

<template>
    <div>
        <el-dialog
            v-model="dialogFormVisible"
            :title="title"
            style="width: 30%"
        >
            <el-form
                :model="form"
                :label-position="'top'"
                style="padding: 20px"
            >
                <el-form-item label="Tên phí" label-width="100px">
                    <el-input v-model="form.name" autocomplete="off" />
                </el-form-item>
                <el-form-item label="Phí phải đóng" label-width="100px">
                    <el-input
                        type="number"
                        v-model="form.amount"
                        autocomplete="off"
                    />
                </el-form-item>
                <el-form-item label="Loại phí" label-width="100px">
                    <el-select v-model="form.type">
                        <el-option label="Bắt buộc" :value="0" />
                        <el-option label="Tự nguyện" :value="1" />
                    </el-select>
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button
                        @click="
                            () => {
                                dialogFormVisible = false;
                            }
                        "
                    >
                        Huỷ
                    </el-button>
                    <el-button
                        type="primary"
                        @click="
                            async () => {
                                dialogFormVisible = false;
                                $emit('dataChange', form);
                            }
                        "
                    >
                        Xác nhận
                    </el-button>
                </span>
            </template>
        </el-dialog>
    </div>
</template>

<style scoped>
.el-select {
    width: 100%;
}
</style>

<style>
.el-date-editor {
    width: 100% !important;
}
</style>
