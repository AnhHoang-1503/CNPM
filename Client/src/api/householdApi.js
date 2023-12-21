import client from "./apiConfig";
import peopleApi from "./peopleApi";

export const getAllHouseholds = async () => {
    const res = await client.get("/Family");
    const listHousehold = res.data;
    listHousehold.forEach(async (household) => {
        let people = await getPeopleInHouseHold(household.id);
        household.peopleCount = people?.length;
    });
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

export default {
    getAllHouseholds,
    getPeopleInHouseHold,
    createHouseHold,
    deleteHouseholds,
    splitHouseHold,
};
