
<template>
    <div class="inventory-container">
      <h1 id="inventory-title">Inventory Dashboard</h1>
      <hr />
      <div class="inventory-actions">
        <solar-button @button:click="showProductModal" id="addNewBtn">Add new item</solar-button>
        <solar-button @button:click="showShipmentmodal" id="recieveShipmentBtn">Recieve Shipment</solar-button>
      </div>
      <table id="inventory-table" class="table">
        <tr>
          <th>Item</th>
          <th>Quantity On-Hand</th>
          <th>Unit Price</th>
          <th>Taxable</th>
          <th>Delete</th>
        </tr>
        <tr v-for="item in inventory" :key="item.id">
          <td>{{item.product?.name}}</td>
          <td
            v-bind:class="`${applyColor(item.quantityOnHand, item.idealQuantity)}`"
          >{{item.quantityOnHand}}</td>
          <td>{{item.product?.price | price}}</td>
          <td>
            <span v-if="item.product?.isTaxable">Yes</span>
            <span v-else>No</span>
          </td>
          <td>
            <div
              class="lni lni-cross-circle product-archive"
              @click="archiveProduct(item.product.id)"
            ></div>
          </td>
        </tr>
      </table>
      <product-modal v-if="isNewProductVisible"
      @save:product="saveNewProduct"
      @close="closeModals"></product-modal>
  
      <shipment-modal
        v-if="isShipmentVisible"
        :inventory="inventory"
        @close="closeModals"
        @save:shipment="saveNewShipment"
      />
    </div>
  </template>
  
  <script lang="ts">
  import { Component, Vue } from "vue-property-decorator";
  import { IProduct, IProductInventory } from "../types/Product";
  import SolarButton from "@/components/SolarButton.vue";
import ShipmentModal from "@/components/modals/ShipmentModal.vue";
import ProductModal from "@/components/modals/ProductModal.vue";
import { InventoryService } from "@/services/inventory-service";
import { IShipment } from "@/types/Shipment";
import { ProductService } from "@/services/product-service";
  
  const inventoryService = new InventoryService();
  const productService = new ProductService();

  @Component({
    name: "Inventory",
    components: { SolarButton, ShipmentModal, ProductModal }
  })
  export default class Inventory extends Vue {
    inventory: IProductInventory[] = [];
    isNewProductVisible = false;
    isShipmentVisible = false;

    showProductModal() {
      this.isNewProductVisible = true;
    }

    showShipmentmodal() {
      this.isShipmentVisible = true;
    }
  
    closeModals() {
      this.isShipmentVisible = false;
      this.isNewProductVisible = false;
    }
  
    async saveNewProduct(product: IProduct) {
      await productService.save(product);
      await this.initialize();
    }
    async saveNewShipment(shipment: IShipment) {
      await inventoryService.updateInventoryQuantity(shipment);
      this.isShipmentVisible = false;
      await this.initialize();
    }

    async initialize() {
      this.inventory = await inventoryService.getInventory();
    }

    created() {
      this.initialize();
    }
  
    applyColor(currentValue: number, targetValue: number) {
      if (currentValue <= 0) {
        return "red";
      }
      if (Math.abs(targetValue - currentValue) > 8) {
        return "yellow";
      }
      return "green";
    }
    async archiveProduct(productId: number) {
    try {
      await productService.archive(productId);
      await this.initialize();
    } catch (error) {
      console.error("Error Occured");
    }
  }
  }
  </script>
  
  <style lang="scss">
  @import "@/scss/global.scss";
  .green {
    font-weight: bold;
    color: $solar-green;
  }
  
  .yellow {
    font-weight: bold;
    color: $solar-yellow;
  }
  
  .red {
    font-weight: bold;
    color: $solar-red;
  }
  
  .inventory-actions {
    display: flex;
    justify-content: flex-end;
    margin-bottom: 0.8rem;
  }
  
  .product-archive {
    cursor: pointer;
    font-weight: bold;
    font-size: 1.2rem;
    color: $solar-red;
  }
  </style>
  