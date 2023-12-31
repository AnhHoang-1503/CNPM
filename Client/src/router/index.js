import { createRouter, createWebHistory } from "vue-router";
import Main from "@/views/layouts/Main.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "home",
            component: Main,
            children: [
                {
                    path: "/",
                    name: "home",
                    component: () => import("@/views/ListHouseHoldView.vue"),
                },
                {
                    path: "/detail-household",
                    name: "detail-household",
                    component: () => import("@/views/DetailHouseHoldView.vue"),
                },
                {
                    path: "/list-people",
                    name: "list-people",
                    component: () => import("@/views/ListPeopleView.vue"),
                },
                {
                    path: "/list-fee",
                    name: "list-fee",
                    component: () => import("@/views/ListFeeView.vue"),
                },
                {
                    path: "/detail-fee",
                    name: "detail-fee",
                    component: () => import("@/views/DetailFeeView.vue"),
                },
            ],
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
