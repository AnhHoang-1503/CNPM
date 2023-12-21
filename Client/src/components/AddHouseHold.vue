<script setup>
import { ref, onMounted, reactive, watch } from "vue";
import { useHomeStore } from "@/stores/homeStore";

const props = defineProps({
    isUpdate: {
        type: Boolean,
        default: false,
    },
    title: {
        type: String,
        default: "Thông tin hộ khẩu",
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

const homeStore = useHomeStore();
const dialogFormVisible = ref(false);
const form = reactive({
    house_number: "",
    street: "",
    ward: "",
    district: "",
});

const formLabelWidth = ref("120px");

const showdialog = () => {
    dialogFormVisible.value = true;
};

defineExpose({
    showdialog,
});

const emits = defineEmits(["dataChange"]);

watch(dialogFormVisible, (val) => {
    if (!val) {
        form.house_number = "";
        form.street = "";
        form.ward = "";
        form.district = "";
    }
});
</script>

<template>
    <div>
        <el-dialog
            v-model="dialogFormVisible"
            :title="title"
            style="width: 40%"
        >
            <el-form :model="form">
                <el-form-item label="Số nhà" :label-width="formLabelWidth">
                    <el-input v-model="form.house_number" autocomplete="off" />
                </el-form-item>
                <el-form-item label="Đường" :label-width="formLabelWidth">
                    <el-input v-model="form.street" autocomplete="off" />
                </el-form-item>
                <el-form-item label="Phường" :label-width="formLabelWidth">
                    <el-input v-model="form.ward" autocomplete="off" />
                </el-form-item>
                <el-form-item label="Huyện" :label-width="formLabelWidth">
                    <el-input v-model="form.district" autocomplete="off" />
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

<style scoped></style>
