<script setup>
import { useHomeStore } from "@/stores/homeStore";
import { ref, onMounted } from "vue";
import { House, Plus, User, Money, Delete } from "@element-plus/icons-vue";
import feeApi from "@/api/feeApi";
import AddFee from "@/components/AddFee.vue";

const tableColumn = [
    {
        name: "id",
        label: "Mã phí",
        width: "120",
    },
    {
        name: "name",
        label: "Tên phí",
    },
    {
        name: "type",
        label: "Loại phí",
    },
    {
        name: "totalAmount",
        label: "Tổng số tiền",
    },
    {
        name: "paidAmount",
        label: "Đã thu",
    },
];
const dialogMessage = "Bạn có chắc chắn muốn xóa những khoản phí đã chọn?";

const homeStore = useHomeStore();
const selectedList = ref([]);
const tableData = ref([]);
const dialogVisible = ref(false);
const currentEdit = ref(null);
const showWarning = ref(false);

const addFee = ref(null);
const editFee = ref(null);

function handleSelectionChange(selected) {
    selectedList.value = selected;
}

async function reloadData() {
    tableData.value = await feeApi.getAllFees();
}

async function deleteAllSelected() {
    selectedList.value.forEach((item) => {
        if (item.paidAmount != 0) {
            showWarning.value = true;
        }
    });
    if (showWarning.value) {
        return;
    }
    await feeApi.deleteFees(selectedList.value);
    await reloadData();
}

async function createFee(form) {
    await feeApi.createFee(form);
    await reloadData();
}

async function updateFee(form) {
    await feeApi.updateFee(currentEdit.value, form);
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
                            @click="addFee.showdialog()"
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
            <el-dialog v-model="showWarning" width="30%">
                <span style="font-size: 16px">
                    Khoản phí này đã được thu, không thể thay đổi
                </span>
                <template #footer>
                    <span class="dialog-footer">
                        <el-button
                            type="primary"
                            @click="
                                async () => {
                                    showWarning = false;
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
            <AddFee
                ref="addFee"
                @data-change="
                    (form) => {
                        createFee(form);
                    }
                "
            />
        </Teleport>
        <Teleport to="#app">
            <AddFee
                ref="editFee"
                title="Sửa khoản phí"
                :data="currentEdit"
                @data-change="
                    (form) => {
                        updateFee(form);
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
