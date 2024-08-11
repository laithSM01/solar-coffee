<template>
    <div>
        <h1 id="customersTitle">Manage Customers</h1>
        <hr />
        <div class="customer-actions">
      <solar-button @button:click="showNewCustomerModal">Add Customer</solar-button>
    </div>
    <table id="customers" class="table">
      <tr>
        <th>Customer</th>
        <th>Address</th>
        <th>State</th>
        <th>Since</th>
        <th>Delete</th>
      </tr>
      <tr v-for="customer in customers" :key="customer.id">
        <td>{{ customer.firstName + " " + customer.lastName }}</td>
        <td>{{ customer.primaryAddress.addressLine1 + " " + customer.primaryAddress.addressLine2 }}</td>
        <td>{{ customer.primaryAddress.state }}</td>
        <td>{{ customer.createdOn  }}</td>
        <td>
          <div class="lni lni-cross-circle product-archive" @click="deleteCustomer(customer.id)"></div>
        </td>
      </tr>
    </table>

    <customer-modal
      v-if="isCustomerModalVisible"
      @save:customer="saveNewCustomer"
      @close="closeModals"
    />
    </div>
</template>


<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SolarButton from "@/components/SolarButton.vue";
import { ICustomer } from "@/types/Customer";
import CustomerService from "@/services/customer-service";
import CustomerModal from "@/components/modals/NewCustomerModal.vue";


@Component({
  name: "Customers",
  components: { SolarButton, CustomerModal }
})
export default class Customers extends Vue {
    customerService = new CustomerService()
    isCustomerModalVisible = false;
    customers: ICustomer[] = [];

    showNewCustomerModal() {
    this.isCustomerModalVisible = true;
  }
  async initialize() {
    this.customers = await this.customerService.getAllCustomers();
  }

  async created() {
    await this.initialize();
  }
  async deleteCustomer(customerId: number) {
    try {
      await this.customerService.deletCustomer(customerId);
      this.initialize();
    } catch (error) {
      console.error("Error Occured while deleting customer", error);
    }
  }
  async saveNewCustomer(customer: ICustomer) {
    try {
      await this.customerService.createCustomer(customer);
      this.initialize();
      this.isCustomerModalVisible = false;
    } catch (error) {
      console.error("Error Occured while creating new customer", error);
    }
  }

  closeModals() {
    this.isCustomerModalVisible = false;
  }
 }
</script>

<style lang="scss">
@import "@/scss/global.scss";
.customer-actions {
  display: flex;
  margin-bottom: 0.8rem;
  justify-content: flex-end;

  div {
    margin-right: 0.8rem;
  }
}
</style>