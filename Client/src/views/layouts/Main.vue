<script setup>
import { ref, onMounted, watch } from "vue";
import { House, Plus, User, Money, Delete } from "@element-plus/icons-vue";
import { useHomeStore } from "@/stores/homeStore.js";
import { useLoginStore } from "@/stores/loginStore.js";
import AddHouseHold from "@/components/AddHouseHold.vue";
import AddPerson from "@/components/AddPerson.vue";
import AddNewborn from "@/components/AddNewborn.vue";
import AddFee from "@/components/AddFee.vue";
import vi from "element-plus/dist/locale/vi.mjs";

const homeStore = useHomeStore();
const loginStore = useLoginStore();

const tableData = ref([]);
const tableColumn = ref([]);
const tableContent = ref(true);
const selectedList = ref([]);
const dialogVisible = ref(false);
const currentAction = ref("");
const dialogMessage = ref("");
const currentId = ref(1);
const addHouseHold = ref(null);
const addPerson = ref(null);
const addNewborn = ref(null);
const splitHouseHold = ref(null);
const addFee = ref(null);

onMounted(async () => {
    tableData.value = await homeStore.getHouseHoldList();
    tableColumn.value = homeStore.columns;
});

watch(currentId, async (newValue, oldValue) => {
    if (newValue != oldValue) {
        if (homeStore.currentIndex == "1-2") await getDetailHousehold();
        if (homeStore.currentIndex == "3-2") await getFeeByHouseholdId();
    }
});

async function getFeeByHouseholdId() {
    if (!currentId.value) {
        tableData.value = [];
        return;
    }

    tableData.value = await homeStore.getFeeByHouseholdId(currentId.value);
}

async function getDetailHousehold() {
    if (!currentId.value) {
        tableData.value = [];
        return;
    }

    tableData.value = await homeStore.getPeopleInHouseHold(currentId.value);
    tableData.value.forEach((item) => {
        item.gender = item.gender == 0 ? "Nam" : "Nữ";
        item.date_of_birth = new Date(item.date_of_birth).toLocaleDateString(
            "vi-VN"
        );
        item.date_of_permanent_registration = new Date(
            item.date_of_permanent_registration
        ).toLocaleDateString("vi-VN");
        item.date_of_issue = new Date(item.date_of_issue).toLocaleDateString(
            "vi-VN"
        );
    });
}

async function getPeople() {
    tableData.value = await homeStore.getPeople();
    tableData.value.forEach((item) => {
        item.gender = item.gender == 0 ? "Nam" : "Nữ";
        item.date_of_birth = new Date(item.date_of_birth).toLocaleDateString(
            "vi-VN"
        );
        item.date_of_permanent_registration = new Date(
            item.date_of_permanent_registration
        ).toLocaleDateString("vi-VN");
        item.date_of_issue = new Date(item.date_of_issue).toLocaleDateString(
            "vi-VN"
        );
    });
}

async function getFeeList() {
    tableData.value = await homeStore.getAllFees();
}

function handleSelectionChange(selected) {
    selectedList.value = selected;
}

async function selectMenu(index) {
    homeStore.setCurrentIndex(index);
    currentId.value = homeStore.householdList[0]?.id;
    selectedList.value = [];
    tableData.value = [];
    switch (index) {
        case "1-1":
            tableContent.value = true;
            tableColumn.value = homeStore.columns;
            tableData.value = await homeStore.getHouseHoldList();
            break;
        case "1-2":
            tableContent.value = true;
            tableColumn.value = homeStore.columns;
            await getDetailHousehold();
            break;
        case "2-1":
            tableContent.value = true;
            tableColumn.value = homeStore.columns;
            await getPeople();
            break;
        case "3-1":
            tableContent.value = true;
            tableColumn.value = homeStore.columns;
            await getFeeList();
            break;
        case "3-2":
            tableContent.value = true;
            tableColumn.value = homeStore.columns;
            await getFeeByHouseholdId();
            break;
        default:
            break;
    }
}

async function handleAction(data) {
    switch (currentAction.value) {
        case "deleteAllSelected":
            await homeStore.deleteAllSelected(selectedList.value);
            switch (homeStore.currentIndex) {
                case "1-1":
                    tableData.value = await homeStore.getHouseHoldList();
                    break;
                case "1-2":
                    await getDetailHousehold();
                    break;
                case "2-1":
                    await getPeople();
                    break;
                case "3-1":
                    await getFeeList();
                    break;
                default:
                    break;
            }
            break;
        case "addHouseHold":
            await homeStore.createHouseHold(data);
            tableData.value = await homeStore.getHouseHoldList();
            break;

        case "addNewborn":
            data.family_id = currentId.value;
            await homeStore.createNewborn(data);
            await getDetailHousehold();
            break;
        case "addPerson":
            await homeStore.createPerson(data);
            await getPeople();
            break;
        case "splitHouseHold":
            let newHouseHold = {};
            newHouseHold.id = currentId.value;
            newHouseHold.listMemberIds = selectedList.value.map(
                (item) => item.id
            );
            newHouseHold.familyCUDto = data;

            await homeStore.splitHouseHold(newHouseHold);
            await getDetailHousehold();
            await homeStore.getHouseHoldList();
            break;
        case "addFee":
            await homeStore.createFee(data);
            await getFeeList();
            break;
        case "changeFeeStatus":
            await homeStore.changeFeeStatus(data);
            await getFeeByHouseholdId();
            break;
        default:
            break;
    }
}

function handleAddAction() {
    switch (homeStore.currentIndex) {
        case "1-1":
            currentAction.value = "addHouseHold";
            addHouseHold.value.showdialog();
            break;
        case "1-2":
            currentAction.value = "addNewborn";
            addNewborn.value.showdialog();
            break;
        case "2-1":
            currentAction.value = "addPerson";
            addPerson.value.showdialog();
            break;
        case "3-1":
            currentAction.value = "addFee";
            addFee.value.showdialog();
            break;
        default:
            break;
    }
}
</script>

<template>
    <el-config-provider :locale="vi">
        <el-container class="layout-container-demo">
            <el-aside width="200px">
                <el-scrollbar class="aside">
                    <el-menu
                        :default-openeds="['1']"
                        default-active="/"
                        @select="selectMenu"
                        :router="true"
                    >
                        <el-sub-menu index="1">
                            <template #title>
                                <el-icon>
                                    <House />
                                </el-icon>
                                Quản lý hộ khẩu
                            </template>
                            <el-menu-item index="/">
                                Danh sách hộ khẩu
                            </el-menu-item>
                            <el-menu-item index="/detail-household">
                                Chi tiết hộ khẩu
                            </el-menu-item>
                        </el-sub-menu>
                        <el-sub-menu index="2">
                            <template #title>
                                <el-icon>
                                    <User />
                                </el-icon>
                                Quản lý nhân khẩu
                            </template>
                            <el-menu-item index="list-people">
                                Danh sách nhân khẩu
                            </el-menu-item>
                        </el-sub-menu>
                        <el-sub-menu index="3">
                            <template #title>
                                <el-icon>
                                    <Money />
                                </el-icon>
                                Quản lý thu phí
                            </template>
                            <el-menu-item index="list-fee">
                                Danh sách phí
                            </el-menu-item>
                            <el-menu-item index="detail-fee">
                                Phí của từng hộ
                            </el-menu-item>
                        </el-sub-menu>
                    </el-menu>
                </el-scrollbar>
                <div class="aside-logout" @click="loginStore.logout()">
                    <span>Đăng xuất</span>
                </div>
            </el-aside>

            <el-container>
                <RouterView></RouterView>
            </el-container>
            <!-- <el-container v-if="tableContent">
            <el-header style="font-size: 20px">
                <div class="toolbar">
                    <el-select
                        class="select_family_id"
                        v-model="currentId"
                        placeholder="Mã hộ khẩu"
                        size="default"
                        v-if="
                            homeStore.currentIndex == '1-2' ||
                            homeStore.currentIndex == '3-2'
                        "
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

                    <el-button
                        round
                        v-if="
                            selectedList.length > 0 &&
                            homeStore.currentIndex == '1-2'
                        "
                        @click="
                            () => {
                                currentAction = 'splitHouseHold';
                                splitHouseHold.showdialog();
                            }
                        "
                    >
                        Tách hộ khẩu
                    </el-button>
                    <el-button
                        :icon="Plus"
                        circle
                        @click="handleAddAction"
                        v-if="homeStore.currentIndex != '3-2'"
                    />
                    <el-button
                        type="danger"
                        :icon="Delete"
                        circle
                        v-if="selectedList.length > 0"
                        @click="
                            () => {
                                dialogVisible = true;
                                currentAction = 'deleteAllSelected';
                                if (homeStore.currentIndex == '1-1') {
                                    dialogMessage =
                                        'Bạn có chắc chắn muốn xóa những hộ khẩu đã chọn?';
                                }
                                if (
                                    homeStore.currentIndex == '2-1' ||
                                    homeStore.currentIndex == '1-2'
                                ) {
                                    dialogMessage =
                                        'Bạn có chắc chắn muốn xóa những nhân khẩu đã chọn?';
                                }
                                if (homeStore.currentIndex == '3-1') {
                                    dialogMessage =
                                        'Bạn có chắc chắn muốn xóa những khoản phí đã chọn?';
                                }
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
                        <el-table-column
                            fixed="right"
                            label=""
                            width="120"
                            v-if="homeStore.currentIndex == '3-2'"
                        >
                            <template #default="scope">
                                <el-button
                                    type="success"
                                    size="small"
                                    v-if="
                                        tableData[scope.$index].is_paid ==
                                        'Đã đóng'
                                    "
                                    @click="
                                        () => {
                                            currentAction = 'changeFeeStatus';
                                            handleAction(
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
                                        () => {
                                            currentAction = 'changeFeeStatus';
                                            handleAction(
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
        </el-container> -->
        </el-container>
    </el-config-provider>
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
