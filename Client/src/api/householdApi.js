import client from "./apiClient";
import peopleApi from "./peopleApi";

export const getAllHouseholds = async () => {
    const res = await client.get("/Family");
    let listHousehold = res.data;
    listHousehold.forEach((household) => {
        household.peopleCount = 0;
    });

    const listPromise = listHousehold.map(async (household) => {
        let people = await getPeopleInHouseHold(household.id);
        household.peopleCount = people?.length;
        return household;
    });

    listHousehold = await Promise.all(listPromise);
    return listHousehold;
};

export const getPeopleInHouseHold = async (householdId) => {
    const res = await client.get(`/Person/family/${householdId}`);
    const data = res.data;
    return data;
};

export const createHouseHold = async (household) => {
    await client.post("/Family", household);
};

export const deleteHouseholds = async (list) => {
    var listIds = list.map((household) => household.id);
    await client.delete(`/Family`, {
        data: listIds,
    });
};

export const splitHouseHold = async (newHouseHold) => {
    await client.post("/Family/household_spilit", newHouseHold);
};

export const updateHouseHold = async (oldData, newData) => {
    await client.put(`/Family/${oldData.id}`, newData);
};

export default {
    getAllHouseholds,
    getPeopleInHouseHold,
    createHouseHold,
    deleteHouseholds,
    splitHouseHold,
    updateHouseHold,
};
