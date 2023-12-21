import { createRouter, createWebHistory } from "vue-router";
import Main from "@/views/layouts/Main.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "home",
            component: Main,
        },
        {
            path: "/login",
            name: "login",
            component: () => import("@/views/LoginView.vue"),
        },
    ],
});

router.beforeEach((to, from, next) => {
    const token = localStorage.getItem("token");
    if (to.name !== "login" && !token) {
        next({ name: "login" });
    } else {
        next();
    }
});

router.beforeEach((to, from, next) => {
    const token = localStorage.getItem("token");
    if (to.name === "login" && token) {
        next({ name: "home" });
    } else {
        next();
    }
});

export default router;
