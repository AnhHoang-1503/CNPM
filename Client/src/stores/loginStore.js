import { defineStore } from "pinia";
import { ref } from "vue";
import userApi from "@/api/userApi";
import router from "@/router";

export const useLoginStore = defineStore("login", () => {
    async function login(username, password) {
        const token = await userApi.login(username, password);
        localStorage.setItem("token", token);
        router.push("/");
    }

    async function logout() {
        localStorage.removeItem("token");
        router.push("/login");
    }

    return {
        login,
        logout,
    };
});
