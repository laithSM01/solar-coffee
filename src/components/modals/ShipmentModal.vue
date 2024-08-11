<template>
  <solar-modal>
    <template v-slot:header>Recieve Shipment</template>
    <template v-slot:body>
      <label for="product">Product Recieved</label>
      <select v-model="selectedProduct" class="shipmentItems" id="product">
        <option value disabled>Please select one shipment</option>
        <option v-for="item in inventory" :value="item" :key="item.product?.id">{{item.product?.name}}</option>
      </select>
      <label for="qtyRecieved">Quantity Recieved</label>
      <input type="number" id="qtyRecieved" v-model="qtyRecieved" />
    </template>
    <template v-slot:footer>
      <solar-button type="button" @button:click="save">Save Recieved Shipment</solar-button>
      <solar-button type="button" @button:click="close">Close</solar-button>
    </template>
  </solar-modal>
</template>
  
  <script lang="ts">
  import { Component, Vue, Prop } from "vue-property-decorator";
  import SolarButton from "@/components/SolarButton.vue";
  import SolarModal from "@/components/modals/SolarModal.vue";
  import { IProductInventory, IProduct } from "../../types/Product";
  import { IShipment } from "@/types/Shipment";
  @Component({
    name: "ShipmentModal",
    components: { SolarButton, SolarModal }
  })
  export default class ShipmentModal extends Vue {
    @Prop({required: true, type: Array as () => IProductInventory[] })
    inventory!: IProductInventory[];
    selectedProduct: IProduct = {
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
    let shipment: IShipment = {
      productId: this.selectedProduct?.id,
      adjustment: this.qtyRecieved, // when we recieve some value , particular product id, this is what we need to make adjustment in DB
    };
    this.$emit('save:shipment', shipment)
  }
  
}
  </script>
  
  <style lang="scss">
  @import "@/scss/global.scss";
  </style>