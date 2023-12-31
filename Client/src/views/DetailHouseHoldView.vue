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
        name: "note",
        label: "Ghi chú",
        width: "180",
    },
];

const dialogMessage = "Bạn có chắc chắn muốn xóa những nhân khẩu đã chọn?";

onMounted(async () => {
    homeStore.householdList = await householdApi.getAllHouseholds();
    await reloadData();
});

const homeStore = useHomeStore();
const currentId = ref(homeStore.householdList[0]?.id);
const dialogVisible = ref(false);
const selectedList = ref([]);
const tableData = ref([]);
const splitHouseHold = ref(null);

const addNewborn = ref(null);

watch(currentId, async () => {
    await getDetailHousehold();
});

async function getDetailHousehold() {
    if (!currentId.value) {
        tableData.value = [];
        return;
    }

    tableData.value = await householdApi.getPeopleInHouseHold(currentId.value);
}

async function reloadData() {
    homeStore.householdList = await householdApi.getAllHouseholds();
    await getDetailHousehold();
}

async function createNewborn(form) {
    await peopleApi.createNewborn(form);
    await reloadData();
}

async function splitHouseHoldAction(data) {
    let newHouseHold = {};
    newHouseHold.id = currentId.value;
    newHouseHold.listMemberIds = selectedList.value.map((item) => item.id);
    newHouseHold.familyCUDto = data;

    await householdApi.splitHouseHold(newHouseHold);

    await reloadData();
    currentId.value =
        homeStore.householdList[homeStore.householdList.length - 1]?.id;
}

async function deleteAllSelected() {
    await peopleApi.deletePeople(selectedList.value);
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

                        <el-button
                            round
                            v-if="selectedList.length > 0"
                            @click="
                                () => {
                                    splitHouseHold.showdialog();
                                }
                            "
                        >
                            Tách hộ khẩu
                        </el-button>
                        <el-button
                            :icon="Plus"
                            circle
                            @click="
                                () => {
                                    addNewborn.showdialog();
                                }
                            "
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
                        </el-table>
                    </el-scrollbar>
                </el-main>
            </el-container>
        </div>
        <Teleport to="#app">
            <AddNewborn
                ref="addNewborn"
                @data-change="
                    async (form) => {
                        createNewborn(form);
                    }
                "
            />
        </Teleport>
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
                ref="splitHouseHold"
                :title="'Thông tin hộ khẩu mới'"
                @data-change="
                    async (form) => {
                        await splitHouseHoldAction(form);
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
