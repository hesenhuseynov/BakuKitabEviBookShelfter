import { OrderDetailsDTO } from "./orderDetailsDto";
import { OrderStatus } from "./orderstatus";

export interface OrderDTO {
    id: number;
    customerId: number;
    orderDate: Date;
    status:OrderStatus;
    orderNumber:string;
    deliveryAddress: string; 
    phoneNumber: string; 
    paymentMethod: string; 
    orderDetails: OrderDetailsDTO[];
}