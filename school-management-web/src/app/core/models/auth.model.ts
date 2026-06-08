export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
  
}

export interface AuthResult {
  token: string;
  refreshToken: string;
  userId: number;
  role: string;
  expiresAt: string;
}

export type UserRole = 'Admin' | 'Teacher' | 'Student';

export interface AuthUser {
  userId: number;
  role: UserRole;
  token: string;
  refreshToken: string;
  expiresAt: string;
}
