import axios from "axios";

const instance = axios.create({
    baseURL: "https://localhost:153/api",
    headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token") || "",
    },
});

axios.interceptors.request.use(
    function (config) {
        return config;
    },
    function (error) {
        return Promise.reject(error);
    }
);

axios.interceptors.response.use(
    function (response) {
        return response;
    },
    function (error) {
        return Promise.reject(error);
    }
);

export default instance;
