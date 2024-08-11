<template>
    <solar-modal>
      <template v-slot:header>Add New Customer</template>
      <template v-slot:body>
        <ul class="newCustomer">
          <li>
            <label for="firstName">First Name</label>
            <input type="text" id="firstName" v-model="customer.firstName" />
          </li>
          <li>
            <label for="lastName">Last Name</label>
            <input type="text" id="lastName" v-model="customer.lastName" />
          </li>
          <li>
            <label for="addressLine1">Address Line 1</label>
            <input type="text" id="addressLine1" v-model="customer.primaryAddress.addressLine1" />
          </li>
          <li>
            <label for="addressLine2">Address Line 2</label>
            <input type="text" id="addressLine2" v-model="customer.primaryAddress.addressLine2" />
          </li>
          <li>
            <label for="country">Country</label>
            <input type="text" id="country" v-model="customer.primaryAddress.country" />
          </li>
          <li>
            <label for="state">State</label>
            <input type="text" id="state" v-model="customer.primaryAddress.state" />
          </li>
          <li>
            <label for="city">City</label>
            <input type="text" id="city" v-model="customer.primaryAddress.city" />
          </li>
          <li>
            <label for="postalCode">Postal Code</label>
            <input type="text" id="postalCode" v-model="customer.primaryAddress.postalCode" />
          </li>
        </ul>
      </template>
      <template v-slot:footer>
        <solar-button type="button" @button:click="save">Create New Customer</solar-button>
        <solar-button type="button" @button:click="close">Close</solar-button>
      </template>
    </solar-modal>
  </template>
  
  <script lang="ts">
  import { Component, Vue } from "vue-property-decorator";
  import SolarButton from "@/components/SolarButton.vue";
  import SolarModal from "@/components/modals/SolarModal.vue";
  import { ICustomer, IAddress } from "../../types/Customer";
  @Component({
    name: "CustomerModal",
    components: { SolarButton, SolarModal }
  })
  export default class CustomerModal extends Vue {
    address: IAddress = {
      id: 0,
      createdOn: new Date(),
      addressLine1: "",
      addressLine2: "",
      city: "",
      state: "",
      postalCode: "",
      country: ""
    };
    customer: ICustomer = {
      id: 0,
      createdOn: new Date(),
      firstName: "",
      lastName: "",
      primaryAddress: this.address
    };
    close() {
      this.$emit("close");
    }
  
    save() {
      this.$emit("save:customer", this.customer);
    }
  }
  </script>
  
  <style lang="scss">
  @import "@/scss/global.scss";
  .newCustomer {
    display: flex;
    flex-wrap: wrap;
    list-style: none;
    padding: 0;
    margin: 0;
    input {
      width: 80%;
      height: 1.8rem;
      margin: 0.8rem 2rem 0.8rem 0.8rem;
      font-size: 1.1rem;
      line-height: 1.3rem;
      padding: 0.2rem;
      color: #555;
    }
    label {
      font-weight: bold;
      margin: 0.8rem;
      display: block;
    }
  }
  </style>
  