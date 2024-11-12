import { BasketItem } from "./basketitem";

export interface Basket{
    id: string;
    userId: string;
    items: BasketItem[];
    total: number;
} 