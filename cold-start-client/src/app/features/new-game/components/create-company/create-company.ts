import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { NewGameService } from '../../services/new-game.service';
import { CreateCompanyRequest } from '../../../../core/models/company.model';
import { ReactiveFormsModule, FormGroup, Validators, FormControl } from '@angular/forms';
import { CountryService } from '../../../../core/services/country.service';
import { Country } from '../../../../core/models/country.model';
import { ToastService } from '../../../../core/services/toast.service';
import { SpinnerService } from '../../../../core/services/spinner.service';
import { finalize } from 'rxjs';
import { FormBuilder } from '@angular/forms';

const MIN_COMPANY_NAME_LENGTH: number = 2;
const MAX_COMPANY_NAME_LENGTH: number = 50;

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
  private readonly formBuilder = inject(FormBuilder);

  readonly minCharsCompanyName: number = MIN_COMPANY_NAME_LENGTH;
  readonly maxCharsCompanyName: number = MAX_COMPANY_NAME_LENGTH;

  readonly createCompanyForm: FormGroup = this.formBuilder.group({
    name: [
      '',
      [
        Validators.required,
        Validators.minLength(MIN_COMPANY_NAME_LENGTH),
        Validators.maxLength(MAX_COMPANY_NAME_LENGTH),
        Validators.pattern(/^[a-zA-Z0-9 ]*$/),
      ],
    ],
    countryId: [null as number | null, [Validators.required]],
  });

  get nameControl(): FormControl {
    return this.createCompanyForm.get('name') as FormControl;
  }
  get countryControl(): FormControl {
    return this.createCompanyForm.get('countryId') as FormControl;
  }

  readonly countries = signal<Country[]>([]);

  readonly submitting = signal<boolean>(false);
  readonly loadingCountries = signal<boolean>(false);

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

    const createCompanyRequest: CreateCompanyRequest = {
      name: this.nameControl.value.trim(),
      countryId: this.countryControl.value,
      userCreated: true,
    };

    this.newGameService
      .submitCompany(createCompanyRequest)
      .pipe(
        finalize(() => {
          this.submitting.set(false);
        }),
      )
      .subscribe({
        error: () => {
          this.toastService.error("Couldn't create your company. Please try again.");
        },
      });
  }
}
