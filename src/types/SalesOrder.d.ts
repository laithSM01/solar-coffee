import { ICustomer } from "./Customer";
import { ILineItems } from "./Invoice";

export interface ISalesOrder {
    id: number;
    createdOn: Date;
    updatedOn?: Date;
    customer: ICustomer;
    isPaid: Boolean;
    salesOrderItems: ILineItems[];
}