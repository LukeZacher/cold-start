import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
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
import { ToastService } from '../../../../core/services/toast.service';
import { SpinnerService } from '../../../../core/services/spinner.service';
import { AuthService } from '../../../../core/services/auth.service';
import { RegisterRequest } from '../../../../core/models/auth.model';

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
  private readonly authService = inject(AuthService);
  private readonly toastService = inject(ToastService);
  private readonly spinnerService = inject(SpinnerService);
  private readonly formBuilder = inject(FormBuilder);

  readonly minPasswordLength: number = MIN_PASSWORD_LENGTH;
  readonly maxPasswordLength: number = MAX_PASSWORD_LENGTH;
  readonly maxEmailLength: number = MAX_EMAIL_LENGTH;

  readonly submitting = signal<boolean>(false);

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

  register() {
    this.submitting.set(true);
    // const registerRequest: RegisterRequest =;
    // this.authService.register(registerRequest).subscribe()
  }
}
