<script setup></script>

<template>
    <div>
        <div class="container">
            <el-container v-if="tableContent">
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
                                                currentAction =
                                                    'changeFeeStatus';
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
                                                currentAction =
                                                    'changeFeeStatus';
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
            </el-container>
        </div>
    </div>
</template>

<style scoped></style>
