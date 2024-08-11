<template>
    <div>
    <ApexCharts>
    type="area"
    :width="'100%'"
    height="300"
    :options="options"
    :series="series"
    </ApexCharts>
    </div>
</template>


<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { IInventoryTimeLine } from "../../types/InventoryGraph";

 
//Vue.component("apexchart",);

@Component({
  name: "InventoryCharts",
  components: {}
})


export default class InventoryCharts extends Vue {
    
    get options() {
        return {
            datalabels: {enabled: false},
            fill: {
                type: 'gradient'
            },
            stroke: {
                curve: "Smooth"
            },
            // xaxis: {
            //     catagories: this.snapch
            // }
        }
    }
}

</script>