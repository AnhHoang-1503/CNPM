import client from "./apiClient";

export const getPeople = async () => {
    const res = await client.get("/Person");
    const data = res.data.map((item) => {
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
        return item;
    });
    return data;
};

export const createNewborn = async (newborn) => {
    await client.post("/Person/newborn", newborn);
};

export const deletePeople = async (list) => {
    var listIds = list.map((person) => person.id);
    await client.delete(`/Person`, {
        data: listIds,
    });
};

export const createPerson = async (person) => {
    await client.post("/Person", person);
};

export const updatePerson = async (oldData, newData) => {
    await client.put(`/Person/${oldData.id}`, newData);
};

export default {
    getPeople,
    createNewborn,
    deletePeople,
    createPerson,
    updatePerson,
};
