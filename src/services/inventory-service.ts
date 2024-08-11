import { IInventoryTimeLine } from '@/types/InventoryGraph';
import { IProductInventory } from '@/types/Product';
import { IShipment } from '@/types/Shipment';
import axios from 'axios';


export class InventoryService {
    API_URL = process.env.VUE_APP_API_URL;

    public async getInventory(): Promise<IProductInventory[]> {
        try {
            const result = await axios.get(`${this.API_URL}/inventory`);
            return result.data;
        } catch (error) {
            console.log('Error:', error);
            throw new Error('Failed to fetch inventory data');
        }
    }

    public async updateInventoryQuantity(shipment: IShipment) {
        let res = await axios.patch(`${this.API_URL}/inventory/`, shipment);
        return res.data;
    }
    public async getSnapShotHistory() :  Promise<IInventoryTimeLine[]>  {
        let res = await axios.get(`${this.API_URL}/inventory/snapshot`);
        return res.data;
    }




}