<template>
  <solar-modal>
    <template v-slot:header>Add New Product</template>
    <template v-slot:body>
      <ul class="newProduct">
        <li>
          <label for="isTaxable">Is this product taxable?</label>
          <input type="checkbox" id="isTaxable" v-model="newProduct.isTaxable" />
        </li>
        <li>
          <label for="name">Name</label>
          <input type="text" id="name" v-model="newProduct.name" />
        </li>
        <li>
          <label for="description">Description</label>
          <input type="text" id="description" v-model="newProduct.description" />
        </li>
        <li>
          <label for="price">Price (USD)</label>
          <input type="text" id="price" v-model="newProduct.price" />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <solar-button type="button" aria-label="Save Product" @button:click="save">Save Product</solar-button>
      <solar-button type="button" aria-label="Close Modal" @button:click="close">Close</solar-button>
    </template>
  </solar-modal>
</template>
  
  <script lang="ts">
  import { Component, Vue, Prop } from "vue-property-decorator";
  import SolarButton from "@/components/SolarButton.vue";
  import SolarModal from "@/components/modals/SolarModal.vue";
  import { IProductInventory, IProduct } from "../../types/Product";
  @Component({
    name: "ProductModal",
    components: { SolarButton, SolarModal }
  })
  export default class ProductModal extends Vue {
    newProduct: IProduct = {
    id: 0,
    createdOn: new Date(),
    updatedOn: new Date(),
    name: "",
    description: "",
    price: 0,
    isTaxable: false,
    isArchived: false
  };
  
    qtyRecieved = 0;

    close() {
    this.$emit("close");
  }

  save() {
    this.$emit('save:product', this.newProduct)
  }
  
}
  </script>
  
  <style lang="scss">
  @import "@/scss/global.scss";
  .newProduct {
  list-style: none;
  padding: 0;
  margin: 0;
}
  </style>