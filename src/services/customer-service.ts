import axios from "axios";
import { ICustomer } from '@/types/Customer';
import IServiceResponse from '@/types/ServiceResponse';

export default class CustomerService {
    API_URL = process.env.VUE_APP_API_URL;

    public async getAllCustomers(): Promise<ICustomer[]> {
      
            const result = await axios.get(`${this.API_URL}/customers`);
            return result.data;
        
    }

    public async createCustomer(customer: ICustomer): Promise<IServiceResponse<ICustomer>> {
        
            const result = await axios.post(`${this.API_URL}/customer`, customer);
            return result.data;
       
    }

    public async deletCustomer(customerId: number): Promise<boolean> {
        
            const result = await axios.delete(`${this.API_URL}/customer/${customerId}`);
            return result.data;
        
    }

}