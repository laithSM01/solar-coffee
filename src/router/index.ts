import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Inventory from "@/views/Inventory.vue";
import Orders from "@/views/Orders.vue";
import Customers from "@/views/Customers.vue";
import CreateInvoice from "@/views/CreateInvoice.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
    {
        "path": "/",
        "name": "home",
        "component": Inventory
    },
    {
        "path": "/inventory",
        "name": "inventory",
        "component": Inventory
    },
    {
        "path": "/customers",
        "name": "customers",
        "component": Customers
    },
    {
        "path": "/orders",
        "name": "orders",
        "component": Orders
    },
    {
        "path": "/invoice/new",
        "name": "create-invoice",
        "component": CreateInvoice
    }
];

const router = new VueRouter({
    mode: "history",
    base: process.env.BASE_URL,
    routes
});

export default router;