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

const dialogFormVisible = ref(false);

const form = ref({
    name: "",
    amount: 0,
    type: null,
    mode: 0,
});

watch(
    () => props.data,
    (val) => {
        if (val) {
            form.value = { ...val };
        }
    }
);

const showdialog = () => {
    dialogFormVisible.value = true;
};
defineExpose({
    showdialog,
});
const emits = defineEmits(["dataChange"]);

watch(dialogFormVisible, (val) => {
    if (!val) {
        form.value = {
            name: "",
            amount: 0,
            type: null,
            mode: 0,
        };
    }
    if (val) {
        form.value = { ...props.data };
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
                        v-model="form.amount"
                        autocomplete="off"
                        :formatter="
                            (value) =>
                                `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
                        "
                        :parser="(value) => value.replace(/\$\s?|(,*)/g, '')"
                    />
                </el-form-item>
                <el-form-item label="Loại phí" label-width="100px">
                    <el-select v-model="form.type">
                        <el-option label="Bắt buộc" :value="0" />
                        <el-option label="Tự nguyện" :value="1" />
                    </el-select>
                </el-form-item>
                <el-form-item label="Hình Thức Phí" label-width="100px">
                    <el-select v-model="form.mode">
                        <el-option label="Cố định" :value="0" />
                        <el-option label="Theo đầu người" :value="1" />
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
