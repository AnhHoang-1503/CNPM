<script setup>
import { RouterLink, RouterView } from "vue-router";
</script>

<template>
    <div>
        <div class="main">
            <div class="container">
                <RouterView />
            </div>
        </div>
    </div>
</template>

<style scoped>
.main {
    width: 100%;
    height: 100vh;
    background-color: #f5f7fa;
    display: grid;
    place-items: center;
}

.container {
    min-width: 1000px;
    min-height: 600px;
    border-radius: 4px;
}
</style>
