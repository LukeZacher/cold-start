import { Component, inject, signal, WritableSignal } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { NgIcon } from '@ng-icons/core';
import { TippyDirective } from '@ngneat/helipopper';
import { ToastService, AuthService } from '../../../../core/services';
import { RegistrationFlowService } from '../../services/registration-flow.service';
import { RegisterRequest } from '../../../../core/models';
import { finalize } from 'rxjs';
import { Title } from '@angular/platform-browser';
import { APP_NAME } from '../../../../core/constants';

const MIN_PASSWORD_LENGTH: number = 8;
const MAX_PASSWORD_LENGTH: number = 64;
const MAX_EMAIL_LENGTH: number = 254;

const passwordMatchValidator: ValidatorFn = (group) => {
  const password = group.get('password')?.value;
  const confirmPassword = group.get('confirmPassword')?.value;
  return password === confirmPassword ? null : { passwordMismatch: true };
};

@Component({
  selector: 'app-register',
  imports: [RouterLink, ReactiveFormsModule, NgIcon, TippyDirective],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  private readonly authService: AuthService = inject(AuthService);
  private readonly toastService: ToastService = inject(ToastService);
  private readonly formBuilder: FormBuilder = inject(FormBuilder);
  private readonly router: Router = inject(Router);
  private readonly titleService: Title = inject(Title);
  private readonly registrationFlowService: RegistrationFlowService =
    inject(RegistrationFlowService);

  readonly minPasswordLength: number = MIN_PASSWORD_LENGTH;
  readonly maxPasswordLength: number = MAX_PASSWORD_LENGTH;
  readonly maxEmailLength: number = MAX_EMAIL_LENGTH;

  readonly showPassword: WritableSignal<boolean> = signal<boolean>(false);
  readonly showConfirmPassword: WritableSignal<boolean> = signal<boolean>(false);

  readonly submitting: WritableSignal<boolean> = signal<boolean>(false);

  readonly registerForm: FormGroup = this.formBuilder.group(
    {
      email: ['', [Validators.required, Validators.email, Validators.maxLength(MAX_EMAIL_LENGTH)]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(MIN_PASSWORD_LENGTH),
          Validators.maxLength(MAX_PASSWORD_LENGTH),
        ],
      ],
      confirmPassword: ['', [Validators.required]],
    },
    { validators: passwordMatchValidator },
  );

  get emailControl(): FormControl {
    return this.registerForm.get('email') as FormControl;
  }

  get passwordControl(): FormControl {
    return this.registerForm.get('password') as FormControl;
  }

  get confirmPasswordControl(): FormControl {
    return this.registerForm.get('confirmPassword') as FormControl;
  }

  constructor() {
    this.titleService.setTitle(`Register - ${APP_NAME}`);
  }

  register() {
    if (this.submitting()) {
      return;
    }

    if (this.registerForm.invalid) return;

    this.submitting.set(true);

    const registerRequest: RegisterRequest = {
      email: this.emailControl.value.trim(),
      password: this.passwordControl.value.trim(),
    };

    this.authService
      .register(registerRequest)
      .pipe(
        finalize(() => {
          this.submitting.set(false);
        }),
      )
      .subscribe({
        next: () => {
          this.registrationFlowService.setRegisteredEmail(registerRequest.email);
          this.router.navigate(['/auth/registration-success']);
        },
        error: () => {
          this.toastService.error('Registration error, please try again.');
        },
      });
  }
}
