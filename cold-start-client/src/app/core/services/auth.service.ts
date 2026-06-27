import { inject, Injectable, signal } from '@angular/core';
import { ApiService } from './api.service';
import { Router } from '@angular/router';
import { AuthResponse, LoginRequest, RegisterRequest } from '../models/auth.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly apiService = inject(ApiService);
  private readonly router = inject(Router);

  private readonly controllerName: string = 'auth';

  readonly isLoggedIn = signal<boolean>(this.hasToken());
  readonly currentUserEmail = signal<string | null>(this.getStoredEmail());

  register(registerRequest: RegisterRequest): Observable<string> {
    return this.apiService.post<string>(this.controllerName + '/register', registerRequest);
  }

  login(loginRequest: LoginRequest): Observable<AuthResponse> {
    return this.apiService.post<AuthResponse>(this.controllerName + '/login', loginRequest);
  }

  confirmEmail(token: string, email: string): Observable<string> {
    return this.apiService.get<string>(`${this.controllerName}/confirm-email`, { token, email });
  }

  handleLoginSuccess(authResponse: AuthResponse): void {
    localStorage.setItem('token', authResponse.token);
    localStorage.setItem('email', authResponse.email);
    this.isLoggedIn.set(true);
    this.currentUserEmail.set(authResponse.email);
    this.router.navigate(['/dashboard']);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    this.isLoggedIn.set(false);
    this.currentUserEmail.set(null);
    this.router.navigate(['/auth/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  private getStoredEmail(): string | null {
    return localStorage.getItem('email');
  }
}
