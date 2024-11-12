import { OrderDetails } from "./orderdetails";

export interface Order {
     orderNumber:string,
      id:number, 
     // customerId:number;
     orderDate:Date;
     status:string ;
     address:string;
     orderDetails: OrderDetails[];
      description: string | null;
      orderCode: string | null;
}