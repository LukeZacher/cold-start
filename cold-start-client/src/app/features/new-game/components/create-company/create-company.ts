import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { NewGameService } from '../../services/new-game.service';
import { CreateCompanyRequest } from '../../../../core/models/company.model';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { CountryService } from '../../../../core/services/country.service';
import { Country } from '../../../../core/models/country.model';
import { ToastService } from '../../../../core/services/toast.service';
import { SpinnerService } from '../../../../core/services/spinner.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-create-company',
  imports: [ReactiveFormsModule],
  templateUrl: './create-company.html',
  styleUrl: './create-company.scss',
})
export class CreateCompany implements OnInit {
  private readonly newGameService = inject(NewGameService);
  private readonly countryService = inject(CountryService);
  private readonly toastService = inject(ToastService);
  private readonly spinnerService = inject(SpinnerService);

  readonly minCharsCompanyName: number = 2;
  readonly maxCharsCompanyName: number = 50;

  readonly createCompanyForm = new FormGroup({
    name: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(this.minCharsCompanyName),
      Validators.maxLength(this.maxCharsCompanyName),
      Validators.pattern(/^[a-zA-Z0-9 ]*$/),
    ]),
    countryId: new FormControl<number | null>(null, [Validators.required]),
  });

  get nameControl() {
    return this.createCompanyForm.get('name');
  }
  get countryControl() {
    return this.createCompanyForm.get('countryId');
  }

  readonly countries = signal<Country[]>([]);

  readonly submitting = signal<boolean>(false);
  readonly loadingCountries = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  constructor() {
    effect(() => {
      if (this.loadingCountries() || this.submitting()) {
        this.spinnerService.show();
      } else {
        this.spinnerService.hide();
      }
    });
  }

  ngOnInit(): void {
    this.loadingCountries.set(true);
    this.countryService
      .getAll()
      .pipe(finalize(() => this.loadingCountries.set(false)))
      .subscribe({
        next: (countries) => {
          this.countries.set(countries);
        },
        error: () => {
          this.toastService.error('Error getting countries.');
        },
      });
  }

  submit(): void {
    if (this.createCompanyForm.invalid) return;

    this.submitting.set(true);
    this.error.set(null);

    const createCompanyRequest: CreateCompanyRequest = {
      name: this.createCompanyForm.value.name!,
      countryId: this.createCompanyForm.value.countryId!,
      userCreated: true,
    };

    this.newGameService.submitCompany(createCompanyRequest).subscribe({
      next: () => {
        this.submitting.set(false);
      },
      error: () => {
        this.error.set('Something went wrong when trying to create your company. Please try again');
        this.submitting.set(false);
      },
    });
  }
}
