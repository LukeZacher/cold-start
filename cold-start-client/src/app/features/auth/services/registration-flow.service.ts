import { Injectable } from '@angular/core';

const STORAGE_KEY: string = 'registration_email';

@Injectable({ providedIn: 'root' })
export class RegistrationFlowService {
  setRegisteredEmail(email: string): void {
    sessionStorage.setItem(STORAGE_KEY, email);
  }

  getRegisteredEmail(): string | null {
    return sessionStorage.getItem(STORAGE_KEY);
  }

  clearRegisteredEmail(): void {
    sessionStorage.removeItem(STORAGE_KEY);
  }
}
