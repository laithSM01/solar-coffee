<template>
  <div class="side-menu-container">
    <router-link to="/">
      <img id="imgLogo" src="../assets/images/solar_coffee_logo.png" alt="Solar Coffee logo" />
    </router-link>
    <h1>Management Portal</h1>
    <solar-button id="menuInventory" @button:click="goTo('/inventory')" is-full-width>Inventory</solar-button>
    <solar-button id="menuCustomer" @button:click="goTo('/customers')" is-full-width>Customer</solar-button>
    <solar-button id="menuInvoice" @button:click="goTo('/invoice/new')" is-full-width>New Invoice</solar-button>
    <solar-button id="menuOrders" @button:click="goTo('/orders')" is-full-width>Orders</solar-button>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SolarButton from "@/components/SolarButton.vue";
@Component({
  name: "SideMenu",
  components: { SolarButton }
})
export default class SideMenu extends Vue {
  goTo(link: string) {
    this.$router.push(link);
  }
}
</script>
<style scoped lang="scss">
@import "@/scss/global.scss";
.side-menu-container {
  background-color: #fcfcfc;
  min-height: 100vh;
  width: $menu-width;
  display: flex;
  flex-direction: column;
  padding: 0.8rem;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.12), 0 1px 1px rgba(0, 0, 0, 0.24);
  box-sizing: border-box;
}

#imgLogo {
  width: 100%;
}

h1 {
  font-size: 1.2rem;
  margin: 1rem 0;
}
</style>