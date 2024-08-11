<template>
    <div>
      <h1 class="ordersTitle">Sales Orders</h1>
      <hr />
      <table id="sales-order-table" class="table" v-if="orders.length">
        <tr>
          <th>Customer Id</th>
          <th>Order Id</th>
          <th>Order Total</th>
          <th>Order Status</th>
          <th>Mark Complete</th>
        </tr>
        <tr v-for="order in orders" :key="order.id">
          <td>{{order.customer.id}}</td>
          <td>{{order.id}}</td>
          <td>{{ getTotal(order) | price }}</td>
          <td :class="{ green: order.isPaid }">{{ getStatus(order.isPaid)}}</td>
          <td>
            <div
              v-if="!order.isPaid"
              class="lni lni-checkmark-circle order-complete green"
              @click="markComplete(order.id)"
            ></div>
          </td>
        </tr>
      </table>
    </div>
  </template>
  
  <script lang="ts">
  import { Component, Vue } from "vue-property-decorator";
  import { ISalesOrder } from "../types/SalesOrder";
  import OrdersService from "../services/order-service";
  
  const ordersService = new OrdersService();
  
  @Component({
    name: "Orders",
    components: {}
  })
  export default class Orders extends Vue {
    orders: ISalesOrder[] = [];
  
    getTotal(order: ISalesOrder) {
      return order.salesOrderItems.reduce(
        (a, b) => a + b.product.price * b.quantity,
        0
      );
    }
  
    getStatus(isPaid: boolean) {
      return isPaid ? "Paid in Full" : "Order Upaid";
    }
  
    async initialize() {
      this.orders = await ordersService.getOrders();
    }
  
    async created() {
      await this.initialize();
    }
  
    async markComplete(orderId: number) {
      await ordersService.markOrdersComplete(orderId);
      await this.initialize();
    }
  }
  </script>
  
  <style lang="scss">
  @import "@/scss/global.scss";
  .green {
    font-weight: bold;
    color: $solar-green;
  }
  
  .order-complete {
    cursor: pointer;
    text-align: center;
  }
  </style>