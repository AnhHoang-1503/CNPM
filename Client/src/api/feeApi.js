import client from "./apiClient";

export const getAllFees = async () => {
    const res = await client.get("/Fee");
    let list = res.data;
    list.forEach((fee) => {
        fee.type = fee.type == 0 ? "Bắt buộc" : "Tự nguyện";
        fee.is_paid = fee.is_paid ? "Đã đóng" : "Chưa đóng";

        fee.totalAmount = fee.totalAmount.toLocaleString("en-US");
        fee.paidAmount = fee.paidAmount.toLocaleString("en-US");
    });
    return list;
};

export const getFeeByHouseholdId = async (householdId) => {
    const res = await client.get(`/Fee/fee-list/${householdId}`);
    let list = res.data;
    list.forEach((fee) => {
        fee.type = fee.type == 0 ? "Bắt buộc" : "Tự nguyện";
        fee.is_paid = fee.is_paid ? "Đã đóng" : "Chưa đóng";
        if (fee.date_of_payment) {
            fee.date_of_payment = new Date(
                fee.date_of_payment
            ).toLocaleDateString();
        }
        fee.amount = fee.amount.toLocaleString("en-US");
    });

    return list;
};

export const deleteFees = async (list) => {
    var listIds = list.map((fee) => fee.id);
    await client.delete(`/Fee`, {
        data: listIds,
    });
};

export const createFee = async (fee) => {
    if (fee.mode == 0) {
        await client.post("/Fee", fee);
    }
    if (fee.mode == 1) {
        await client.post("/Fee/fee-per-person", fee);
    }
};

export const changeFeeStatus = async (feeId) => {
    await client.post(`/Fee/pay-fee/${feeId}`);
};

export const updateFee = async (oldFee, newFee) => {
    await client.put(`/Fee/${oldFee.id}`, newFee);
};

export default {
    getAllFees,
    deleteFees,
    createFee,
    getFeeByHouseholdId,
    changeFeeStatus,
    updateFee,
};
