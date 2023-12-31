<script setup>
import { useHomeStore } from "@/stores/homeStore";
import { ref, onMounted } from "vue";
import { House, Plus, User, Money, Delete } from "@element-plus/icons-vue";
import peopleApi from "@/api/peopleApi";
import AddPerson from "@/components/AddPerson.vue";
import householdApi from "@/api/householdApi";
import { toISOString } from "@/utils/helpers.js";

const tableColumn = [
    {
        name: "id",
        label: "Mã nhân khẩu",
        width: "120",
        fixed: "left",
    },

    {
        name: "name",
        label: "Tên",
        width: "180",
        fixed: "left",
    },
    {
        name: "gender",
        label: "Giới tính",
        width: "180",
    },
    {
        name: "alias",
        label: "Biệt danh",
        width: "180",
    },
    {
        name: "date_of_birth",
        label: "Ngày sinh",
        width: "180",
    },
    {
        name: "place_of_birth",
        label: "Nơi sinh",
        width: "180",
    },
    {
        name: "place_of_origin",
        label: "Nguyên quán",
        width: "180",
    },
    {
        name: "peoples",
        label: "Dân tộc",
        width: "180",
    },
    {
        name: "job",
        label: "Nghề nghiệp",
        width: "180",
    },
    {
        name: "place_of_work",
        label: "Nơi làm việc",
        width: "180",
    },
    {
        name: "identify_id",
        label: "Số CMND",
        width: "180",
    },
    {
        name: "date_of_issue",
        label: "Ngày cấp",
        width: "180",
    },
    {
        name: "place_of_issue",
        label: "Nơi cấp",
        width: "180",
    },
    {
        name: "date_of_permanent_registration",
        label: "Ngày đăng ký HKTT",
        width: "180",
    },
    {
        name: "previous_permanent_residence",
        label: "HKTT trước đó",
        width: "180",
    },
    {
        name: "relationship",
        label: "Quan hệ với chủ hộ",
        width: "180",
    },
    {
        name: "family_id",
        label: "Mã hộ",
        width: "180",
    },
    {
        name: "note",
        label: "Ghi chú",
        width: "180",
    },
];
const dialogMessage = "Bạn có chắc chắn muốn xóa những nhân khẩu đã chọn?";

const homeStore = useHomeStore();
const selectedList = ref([]);
const tableData = ref([]);
const dialogVisible = ref(false);
const currentEdit = ref(null);

const addPerson = ref(null);
const editPerson = ref(null);

function handleSelectionChange(selected) {
    selectedList.value = selected;
}

async function reloadData() {
    tableData.value = await peopleApi.getPeople();
    homeStore.householdList = await householdApi.getAllHouseholds();
}

async function deleteAllSelected() {
    await peopleApi.deletePeople(selectedList.value);
    await reloadData();
}

async function createPerson(form) {
    await peopleApi.createPerson(form);
    await reloadData();
}

async function updatePerson(data) {
    await peopleApi.updatePerson(currentEdit.value, data);
    await reloadData();
}

onMounted(async () => {
    await reloadData();
});
</script>

<template>
    <div style="width: 100%">
        <div class="container">
            <el-container>
                <el-header style="font-size: 20px">
                    <div class="toolbar">
                        <el-button
                            :icon="Plus"
                            circle
                            @click="addPerson.showdialog()"
                        />
                        <el-button
                            type="danger"
                            :icon="Delete"
                            circle
                            v-if="selectedList.length > 0"
                            @click="
                                () => {
                                    dialogVisible = true;
                                }
                            "
                        />
                    </div>
                </el-header>

                <el-main>
                    <el-scrollbar class="content">
                        <el-table
                            :data="tableData"
                            style="width: 100%"
                            @selection-change="handleSelectionChange"
                        >
                            <el-table-column type="selection" width="55" />
                            <el-table-column
                                v-for="column in tableColumn"
                                :prop="column.name"
                                :label="column.label"
                                :width="column.width"
                                :fixed="column.fixed"
                                show-overflow-tooltip
                            />
                            <el-table-column fixed="right" label="" width="120">
                                <template #default="scope">
                                    <el-button
                                        type="primary"
                                        link
                                        @click="
                                            () => {
                                                currentEdit = { ...scope.row };
                                                currentEdit.date_of_birth =
                                                    toISOString(
                                                        currentEdit.date_of_birth
                                                    );
                                                currentEdit.date_of_issue =
                                                    toISOString(
                                                        currentEdit.date_of_issue
                                                    );
                                                currentEdit.date_of_permanent_registration =
                                                    toISOString(
                                                        currentEdit.date_of_permanent_registration
                                                    );
                                                currentEdit.gender =
                                                    currentEdit.gender == 'Nam'
                                                        ? 0
                                                        : 1;
                                                editPerson.showdialog();
                                            }
                                        "
                                    >
                                        Sửa
                                    </el-button>
                                </template></el-table-column
                            >
                        </el-table>
                    </el-scrollbar>
                </el-main>
            </el-container>
        </div>
        <Teleport to="#app">
            <el-dialog v-model="dialogVisible" width="30%">
                <span style="font-size: 16px">{{ dialogMessage }}</span>
                <template #footer>
                    <span class="dialog-footer">
                        <el-button @click="dialogVisible = false">
                            Huỷ
                        </el-button>
                        <el-button
                            type="primary"
                            @click="
                                async () => {
                                    dialogVisible = false;
                                    deleteAllSelected();
                                }
                            "
                        >
                            Xác nhận
                        </el-button>
                    </span>
                </template>
            </el-dialog>
        </Teleport>
        <Teleport to="#app">
            <AddPerson
                ref="addPerson"
                @data-change="
                    async (form) => {
                        createPerson(form);
                    }
                "
            />
        </Teleport>
        <Teleport to="#app">
            <AddPerson
                ref="editPerson"
                title="Sửa thông tin nhân khẩu"
                :data="currentEdit"
                @data-change="
                    async (form) => {
                        updatePerson(form);
                    }
                "
            />
        </Teleport>
    </div>
</template>

<style scoped>
.aside {
    height: calc(100% - 40px);
}

.delete_button {
    cursor: pointer;
    font-size: 20px;
    color: #f56c6c;
    font-weight: bold;
}

.aside-logout {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 40px;
    width: 100%;
    border-top: 1px solid #fff;
    cursor: pointer;
}

.menu_dropdown {
    cursor: pointer;
    font-size: 20px;
}

.layout-container-demo {
    width: 100vw;
    height: 100vh;
    border-radius: 4px;
    background-color: #fff;
}

.layout-container-demo .el-header {
    position: relative;
    background-color: var(--el-color-primary-light-7);
    color: var(--el-text-color-primary);
    display: flex;
    align-items: center;
    justify-content: start;
}

.layout-container-demo .el-aside {
    color: var(--el-text-color-primary);
    background: var(--el-color-primary-light-8);
}

.layout-container-demo .el-menu {
    border-right: none;
}

.layout-container-demo .el-main {
    padding: 0;
}

.layout-container-demo .toolbar {
    display: flex;
    align-items: center;
    justify-content: start;
    height: 100%;
    width: 100%;
}

.select_family_id {
    margin-right: 12px;
}
</style>
