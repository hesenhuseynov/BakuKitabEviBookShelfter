import { OrderDetailsDTO } from "./orderDetailsDto";

export interface OrderWithDetailsDTO {
    orderId: number;
    orderNumber: string;
    orderDate: Date;
    deliveryAddress: string;
    customerId: number;
    customerPhoneNumber:string;
    paymentMethod:string,
    orderDetails: OrderDetailsDTO[];
  }