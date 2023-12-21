<script setup>
import { ref, onMounted, reactive, watch } from "vue";
import vi from "element-plus/dist/locale/vi.mjs";
import { useHomeStore } from "@/stores/homeStore";

const homeStore = useHomeStore();
const props = defineProps({
    title: {
        type: String,
        default: "Thêm nhân khẩu mới",
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
    alias: "",
    date_of_birth: new Date(),
    place_of_birth: "",
    place_of_origin: "",
    peoples: "Kinh",
    job: "",
    place_of_work: "",
    identify_id: null,
    date_of_issue: new Date(),
    place_of_issue: "",
    date_of_permanent_registration: new Date(),
    previous_permanent_residence: "",
    family_id: null,
    relationship: "",
    // moving_date: new Date(),
    // moving_to: "",
    // note: "",
    gender: 0,
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
        form.alias = "";
        form.date_of_birth = new Date();
        form.place_of_birth = "";
        form.place_of_origin = "";
        form.peoples = "Kinh";
        form.job = ""; //
        form.place_of_work = ""; //
        form.identify_id = null; //
        form.date_of_issue = new Date(); //
        form.place_of_issue = ""; //
        form.date_of_permanent_registration = new Date();
        form.previous_permanent_residence = ""; //
        form.family_id = null; //
        form.relationship = "";
        // form.moving_date = new Date(); //
        // form.moving_to = ""; //
        // form.note = ""; //
        form.gender = 0;
    }
});
</script>

<template>
    <div>
        <el-dialog
            v-model="dialogFormVisible"
            :title="title"
            style="width: 60%"
        >
            <el-form
                :model="form"
                :label-position="'top'"
                style="padding: 20px"
            >
                <el-row :gutter="20">
                    <el-col :span="12">
                        <el-form-item label="Tên" label-width="100px">
                            <el-input v-model="form.name" autocomplete="off" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="Biệt danh" label-width="100px">
                            <el-input v-model="form.alias" autocomplete="off" />
                        </el-form-item>
                    </el-col>
                </el-row>

                <el-row :gutter="20">
                    <el-col :span="8">
                        <el-form-item label="Giới tính" label-width="140px">
                            <el-select v-model="form.gender">
                                <el-option label="Nam" :value="0" />
                                <el-option label="Nữ" :value="1" />
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="Dân tộc" label-width="140px">
                            <el-input
                                v-model="form.peoples"
                                autocomplete="off"
                            />
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="Ngày sinh" label-width="140px">
                            <el-config-provider :locale="vi">
                                <el-date-picker
                                    v-model="form.date_of_birth"
                                    type="date"
                                    placeholder="Ngày sinh"
                                />
                            </el-config-provider>
                        </el-form-item>
                    </el-col>
                </el-row>

                <el-row :gutter="20">
                    <el-col :span="12">
                        <el-form-item label="Nghề nghiệp" label-width="140px">
                            <el-input v-model="form.job" autocomplete="off" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="Nơi làm việc" label-width="140px">
                            <el-input
                                v-model="form.place_of_work"
                                autocomplete="off"
                            />
                        </el-form-item>
                    </el-col>
                </el-row>

                <el-row :gutter="20">
                    <el-col :span="8">
                        <el-form-item label="Số CMND" label-width="140px">
                            <el-input
                                v-model="form.identify_id"
                                autocomplete="off"
                            />
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="Ngày cấp" label-width="140px">
                            <el-config-provider :locale="vi">
                                <el-date-picker
                                    v-model="form.date_of_issue"
                                    type="date"
                                    placeholder="Ngày sinh"
                                />
                            </el-config-provider>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="Nơi cấp" label-width="140px">
                            <el-input
                                v-model="form.place_of_issue"
                                autocomplete="off"
                            />
                        </el-form-item>
                    </el-col>
                </el-row>

                <el-row :gutter="20">
                    <el-col :span="12">
                        <el-form-item label="Nơi sinh" label-width="140px">
                            <el-input
                                v-model="form.place_of_birth"
                                autocomplete="off"
                            />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="Nguyên quán" label-width="140px">
                            <el-input
                                v-model="form.place_of_origin"
                                autocomplete="off"
                            />
                        </el-form-item>
                    </el-col>
                </el-row>

                <el-row :gutter="20">
                    <el-col :span="12">
                        <el-form-item
                            label="Ngày đăng ký HKTT"
                            label-width="140px"
                        >
                            <el-config-provider :locale="vi">
                                <el-date-picker
                                    v-model="
                                        form.date_of_permanent_registration
                                    "
                                    type="date"
                                    placeholder="Ngày sinh"
                                />
                            </el-config-provider>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="HKTT trước đó" label-width="140px">
                            <el-input
                                v-model="form.previous_permanent_residence"
                                autocomplete="off"
                            />
                        </el-form-item>
                    </el-col>
                </el-row>

                <el-row :gutter="20">
                    <el-col :span="12">
                        <el-form-item label="Mã hộ khẩu" label-width="140px">
                            <el-select v-model="form.family_id">
                                <el-option
                                    v-for="item in homeStore.householdList.map(
                                        (item) => ({
                                            label: `Hộ khẩu ${item.id} - ${item.house_number}`,
                                            value: item.id,
                                        })
                                    )"
                                    :label="item.label"
                                    :value="item.value"
                                />
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item
                            label="Quan hệ với chủ hộ"
                            label-width="140px"
                        >
                            <el-input
                                v-model="form.relationship"
                                autocomplete="off"
                            />
                        </el-form-item>
                    </el-col>
                </el-row>
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
                                if (form.family_id == null) {
                                    form.family_id =
                                        homeStore.householdList[0].id;
                                }
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
