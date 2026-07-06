import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { ApiService } from './api.service';
import { Router } from '@angular/router';
import {
  AuthResponse,
  LoginRequest,
  RegisterRequest,
  ResendEmailConfirmationRequest,
} from '../models';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly apiService: ApiService = inject(ApiService);
  private readonly router: Router = inject(Router);

  private readonly controllerName: string = 'auth';

  readonly isLoggedIn: WritableSignal<boolean> = signal<boolean>(this.hasToken());
  readonly currentUserEmail: WritableSignal<string | null> = signal<string | null>(
    this.getStoredEmail(),
  );

  register(registerRequest: RegisterRequest): Observable<{ message: string }> {
    return this.apiService.post<{ message: string }>(
      this.controllerName + '/register',
      registerRequest,
    );
  }

  login(loginRequest: LoginRequest): Observable<AuthResponse> {
    return this.apiService.post<AuthResponse>(this.controllerName + '/login', loginRequest);
  }

  confirmEmail(token: string, email: string): Observable<string> {
    return this.apiService.get<string>(this.controllerName + '/confirm-email', { token, email });
  }

  resendEmailConfirmation(
    resendEmailConfirmationRequest: ResendEmailConfirmationRequest,
  ): Observable<void> {
    return this.apiService.post<void>(
      this.controllerName + '/resend-email-confirmation',
      resendEmailConfirmationRequest,
    );
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
