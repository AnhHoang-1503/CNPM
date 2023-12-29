<script setup>
import { useHomeStore } from "@/stores/homeStore";
import { ref, onMounted } from "vue";
import { House, Plus, User, Money, Delete } from "@element-plus/icons-vue";
import householdApi from "@/api/householdApi";
import AddHouseHold from "@/components/AddHouseHold.vue";

const tableColumn = [
    {
        name: "id",
        label: "Mã hộ",
        width: "100",
        fixed: "left",
    },
    {
        name: "house_number",
        label: "Số nhà",
        width: "209",
    },
    {
        name: "street",
        label: "Đường",
    },
    {
        name: "ward",
        label: "Phường",
    },
    {
        name: "district",
        label: "Huyện",
    },
    {
        name: "peopleCount",
        label: "Số thành viên",
    },
];
const dialogMessage = "Bạn có chắc chắn muốn xóa những hộ khẩu đã chọn?";

const homeStore = useHomeStore();
const selectedList = ref([]);
const tableData = ref([]);
const dialogVisible = ref(false);
const currentEdit = ref(null);

const addHouseHold = ref(null);
const editHouseHold = ref(null);

function handleSelectionChange(selected) {
    selectedList.value = selected;
}

async function reloadData() {
    tableData.value = await householdApi.getAllHouseholds();
}

async function createHouseHold(data) {
    await householdApi.createHouseHold(data);
    await reloadData();
}

async function updateHouseHold(data) {
    await householdApi.updateHouseHold(currentEdit.value, data);
    await reloadData();
}

async function deleteAllSelected() {
    await householdApi.deleteHouseholds(selectedList.value);
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
                            @click="addHouseHold.showdialog()"
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
                                                currentEdit = scope.row;
                                                editHouseHold.showdialog();
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
            <AddHouseHold
                ref="addHouseHold"
                @data-change="
                    async (data) => {
                        createHouseHold(data);
                    }
                "
            />
        </Teleport>
        <Teleport to="#app">
            <AddHouseHold
                ref="editHouseHold"
                title="Sửa hộ khẩu"
                :data="currentEdit"
                @data-change="
                    async (data) => {
                        updateHouseHold(data);
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
