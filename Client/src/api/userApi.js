import client from "./apiConfig";

export const login = async (username, password) => {
    const res = await client.post("/Login", {
        username,
        password,
    });
    return res.data;
};

export default {
    login,
};
