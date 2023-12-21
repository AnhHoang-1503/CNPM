import client from "./apiClient";

export const getPeople = async () => {
    const res = await client.get("/Person");
    return res.data;
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

export default { getPeople, createNewborn, deletePeople, createPerson };
