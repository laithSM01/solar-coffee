import axios from 'axios';

export default class OrdersService {
    API_URL = process.env.VUE_APP_API_URL;

    public async getOrders(): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/orders`);
        return result.data;
    }

    public async markOrdersComplete(id: number): Promise<any> {
        const result: any = await axios.patch(`${this.API_URL}/order/complete/${id}`);
        return result.data;
    }
}