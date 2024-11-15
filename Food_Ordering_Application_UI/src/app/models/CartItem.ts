import { Menu } from "./Menu";

export interface CartItem {
    cartId: string;
    customerId: string;
    menuId: string;
    menu:Menu;
    quantity: number;
  }