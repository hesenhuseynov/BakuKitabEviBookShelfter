import { Token } from "./token";

export interface LoginResponse {
    success: boolean;
    message: string;
    token: Token;
  }