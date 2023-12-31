<script setup>
import { ref, onMounted, watch } from "vue";
import { useHomeStore } from "@/stores/homeStore";
import householdApi from "@/api/householdApi";
import { Plus, Delete } from "@element-plus/icons-vue";
import AddNewborn from "@/components/AddNewborn.vue";
import peopleApi from "@/api/peopleApi";
import AddHouseHold from "@/components/AddHouseHold.vue";

const tableColumn = [
    {
        name: "fee_id",
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
        name: "is_paid",
        label: "Tình trạng",
    },
    {
        name: "date_of_payment",
        label: "Ngày đóng",
    },
    {
        name: "amount",
        label: "Số tiền",
    },
];

onMounted(async () => {
    await reloadData();
});

const homeStore = useHomeStore();
const currentId = ref(homeStore.householdList[0]?.id);
const dialogVisible = ref(false);
const selectedList = ref([]);
const tableData = ref([]);

watch(currentId, async () => {
    await getFeeByHouseholdId();
});

async function getFeeByHouseholdId() {
    if (!currentId.value) {
        tableData.value = [];
        return;
    }

    tableData.value = await homeStore.getFeeByHouseholdId(currentId.value);
}

async function reloadData() {
    homeStore.householdList = await householdApi.getAllHouseholds();
    await getFeeByHouseholdId();
}

async function changeFeeStatus(fee) {
    await homeStore.changeFeeStatus(fee);
    await reloadData();
}
</script>

<template>
    <div style="width: 100%">
        <div class="container">
            <el-container>
                <el-header style="font-size: 20px">
                    <div class="toolbar">
                        <el-select
                            class="select_family_id"
                            v-model="currentId"
                            placeholder="Mã hộ khẩu"
                            size="default"
                        >
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
                    </div>
                </el-header>

                <el-main>
                    <el-scrollbar class="content">
                        <el-table
                            :data="tableData"
                            style="width: 100%"
                            @selection-change="
                                (selected) => {
                                    selectedList = selected;
                                }
                            "
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
                                        type="success"
                                        size="small"
                                        v-if="
                                            tableData[scope.$index].is_paid ==
                                            'Đã đóng'
                                        "
                                        @click="
                                            async () => {
                                                await changeFeeStatus(
                                                    tableData[scope.$index]
                                                );
                                            }
                                        "
                                    >
                                        Đã đóng
                                    </el-button>
                                    <el-button
                                        type="danger"
                                        size="small"
                                        v-if="
                                            tableData[scope.$index].is_paid ==
                                            'Chưa đóng'
                                        "
                                        @click="
                                            async () => {
                                                await changeFeeStatus(
                                                    tableData[scope.$index]
                                                );
                                            }
                                        "
                                    >
                                        Chưa đóng
                                    </el-button>
                                </template>
                            </el-table-column>
                        </el-table>
                    </el-scrollbar>
                </el-main>
            </el-container>
        </div>
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
