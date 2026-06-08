import { Injectable, signal, computed, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  AuthResult,
  AuthUser,
  LoginRequest,
  RegisterRequest,
  UserRole
} from '../models/auth.model';

const STORAGE_KEY = 'sms_auth';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly apiUrl = `${environment.apiUrl}/auth`;

  private readonly authState = signal<AuthUser | null>(this.loadFromStorage());

  readonly user = computed(() => this.authState());
  readonly isLoggedIn = computed(() => !!this.authState());
  readonly role = computed(() => this.authState()?.role ?? null);
  readonly userId = computed(() => this.authState()?.userId ?? null);

  login(credentials: LoginRequest): Observable<AuthResult> {
    return this.http.post<AuthResult>(`${this.apiUrl}/login`, credentials).pipe(
      tap(result => this.setSession(result))
    );
  }

  register(data: RegisterRequest): Observable<AuthResult> {
    return this.http.post<AuthResult>(`${this.apiUrl}/register`, data).pipe(
      tap(result => this.setSession(result))
    );
  }

  refreshToken(): Observable<AuthResult> {
    const refreshToken = this.authState()?.refreshToken;
    return this.http.post<AuthResult>(`${this.apiUrl}/refresh-token`, JSON.stringify(refreshToken), {
      headers: { 'Content-Type': 'application/json' }
    }).pipe(
      tap(result => this.setSession(result))
    );
  }

  logout(): void {
    localStorage.removeItem(STORAGE_KEY);
    this.authState.set(null);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return this.authState()?.token ?? null;
  }

  hasRole(...roles: UserRole[]): boolean {
    const current = this.authState()?.role;
    return !!current && roles.includes(current);
  }

  redirectByRole(): void {
    const role = this.authState()?.role;
    const routes: Record<UserRole, string> = {
      Admin: '/admin',
      Teacher: '/teacher',
      Student: '/student'
    };
    if (role && routes[role]) {
      this.router.navigate([routes[role]]);
    }
  }

  private setSession(result: AuthResult): void {
    const user: AuthUser = {
      userId: result.userId,
      role: result.role as UserRole,
      token: result.token,
      refreshToken: result.refreshToken,
      expiresAt: result.expiresAt
    };
    localStorage.setItem(STORAGE_KEY, JSON.stringify(user));
    this.authState.set(user);
  }

  private loadFromStorage(): AuthUser | null {
    try {
      const raw = localStorage.getItem(STORAGE_KEY);
      if (!raw) return null;
      const user = JSON.parse(raw) as AuthUser;
      if (new Date(user.expiresAt) < new Date()) {
        localStorage.removeItem(STORAGE_KEY);
        return null;
      }
      return user;
    } catch {
      return null;
    }
  }
}
