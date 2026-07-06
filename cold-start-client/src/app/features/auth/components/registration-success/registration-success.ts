import { Component, computed, inject, Signal, signal, WritableSignal } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { APP_NAME } from '../../../../core/constants';
import { RouterLink } from '@angular/router';
import { RegistrationFlowService } from '../../services/registration-flow.service';
import { AuthService, ToastService } from '../../../../core/services';
import { ResendEmailConfirmationRequest } from '../../../../core/models';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-registration-success',
  imports: [RouterLink],
  templateUrl: './registration-success.html',
  styleUrl: './registration-success.scss',
})
export class RegistrationSuccess {
  private readonly titleService: Title = inject(Title);
  private readonly registrationFlowService: RegistrationFlowService =
    inject(RegistrationFlowService);
  private readonly authService: AuthService = inject(AuthService);
  private readonly toastService: ToastService = inject(ToastService);

  readonly email: string | null = this.registrationFlowService.getRegisteredEmail();

  readonly emailSent: WritableSignal<boolean> = signal<boolean>(false);
  readonly submitting: WritableSignal<boolean> = signal<boolean>(false);

  readonly isResendButtonDisabled: Signal<boolean> = computed<boolean>(() => {
    return this.submitting();
  });

  constructor() {
    this.titleService.setTitle(`Registration Successful - ${APP_NAME}`);
  }

  resendConfirmationEmail() {
    if (this.submitting()) {
      return;
    }

    if (!this.email) {
      // Prompt user to provide their email.
      return;
    }

    this.submitting.set(true);
    this.emailSent.set(false);

    const resendEmailConfirmationRequest: ResendEmailConfirmationRequest = {
      email: this.email,
    };

    this.authService
      .resendEmailConfirmation(resendEmailConfirmationRequest)
      .pipe(
        finalize(() => {
          this.submitting.set(false);
        }),
      )
      .subscribe({
        next: () => {
          this.emailSent.set(true);
          // Cooldown timer?
        },
        error: () => {
          this.toastService.error('Something went wrong. Please try again.');
        },
      });
  }
}
