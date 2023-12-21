import { defineStore } from "pinia";
import { ref, watch } from "vue";
import householdApi from "@/api/householdApi";
import peopleApi from "@/api/peopleApi";
import feeApi from "@/api/feeApi";

const columnList = {
    "1-1": [
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
            width: "209",
        },
        {
            name: "ward",
            label: "Phường",
            width: "209",
        },
        {
            name: "district",
            label: "Huyện",
            width: "209",
        },
        {
            name: "peopleCount",
            label: "Số thành viên",
            width: "209",
        },
    ],
    "1-2": [
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
    ],
    "2-1": [
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
    ],
    "3-1": [
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
    ],
    "3-2": [
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
    ],
};

export const useHomeStore = defineStore("home", () => {
    const currentIndex = ref("1-1");
    const columns = ref(columnList[currentIndex.value]);
    const householdList = ref([]);

    async function getHouseHoldList() {
        householdList.value = await householdApi.getAllHouseholds();
        return householdList.value;
    }

    async function getPeopleInHouseHold(householdId) {
        return await householdApi.getPeopleInHouseHold(householdId);
    }

    async function getPeople() {
        return await peopleApi.getPeople();
    }

    async function createHouseHold(household) {
        await householdApi.createHouseHold(household);
    }

    async function createNewborn(newborn) {
        await peopleApi.createNewborn(newborn);
    }

    async function changeFeeStatus(fee) {
        await feeApi.changeFeeStatus(fee.bill_Id);
    }

    async function deleteAllSelected(list) {
        switch (currentIndex.value) {
            case "1-1":
                await householdApi.deleteHouseholds(list);
                break;
            case "1-2":
            case "2-1":
                await peopleApi.deletePeople(list);
                break;
            case "3-1":
                await feeApi.deleteFees(list);
                break;
            default:
                break;
        }
    }

    async function createPerson(person) {
        await peopleApi.createPerson(person);
    }

    async function splitHouseHold(newHouseHold) {
        await householdApi.splitHouseHold(newHouseHold);
    }

    async function getAllFees() {
        return await feeApi.getAllFees();
    }

    async function createFee(fee) {
        return await feeApi.createFee(fee);
    }

    async function getFeeByHouseholdId(householdId) {
        return await feeApi.getFeeByHouseholdId(householdId);
    }

    return {
        currentIndex,
        setCurrentIndex(index) {
            this.currentIndex = index;
            columns.value = columnList[index];
        },
        columns,
        getHouseHoldList,
        getPeopleInHouseHold,
        householdList,
        getPeople,
        createHouseHold,
        createNewborn,
        deleteAllSelected,
        createPerson,
        splitHouseHold,
        getAllFees,
        createFee,
        getFeeByHouseholdId,
        changeFeeStatus,
    };
});
