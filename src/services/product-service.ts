import axios from "axios";
import { IProduct } from '@/types/Product';

export class ProductService {
    API_URL = process.env.VUE_APP_API_URL;

    async archive(productId: number) {
        try {
            const result = await axios.patch(`${this.API_URL}/product/${productId}`);
            return result.data;
        } catch (error) {
            console.error('Error while archiving product');
            return error;
        }
    }

    async addNewProduct(product: IProduct) {
        try {
            const result = await axios.post(`${this.API_URL}/product`, product);
            return result.data;
        } catch (error) {
            console.error('Error while adding new product');
            return error;
        }
    }

    async save(product: IProduct) {
        let res = await axios.post(`${this.API_URL}/product/`, product);
        return res.data;
    }
}