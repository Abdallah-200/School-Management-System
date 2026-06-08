import { UserRole } from './auth.model';

export interface User {
  id: number;
  fullName: string;
  email: string;
  role: UserRole;
  isActive?: boolean;
  createdDate?: string;
}

export interface CreateUserRequest {
  name: string;
  email: string;
  password: string;
  role?: UserRole;
}

export interface UpdateUserRequest {
  id: number;
  fullName?: string;
  email?: string;
  role?: UserRole;
}
