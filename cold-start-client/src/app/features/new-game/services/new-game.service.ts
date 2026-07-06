import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { CompanyService } from '../../../core/services/company.service';
import { BrandService } from '../../../core/services/brand.service';
import { ModelService } from '../../../core/services/model.service';
import { Router } from '@angular/router';
import { Company, CreateCompanyRequest } from '../../../core/models/company.model';
import { Observable, tap } from 'rxjs';
import { Brand, CreateBrandRequest } from '../../../core/models/brand.model';
import { CreateModelRequest, Model } from '../../../core/models/model.model';

export interface NewGameState {
  step: number;
  companyId: number | null;
  brandId: number | null;
}

@Injectable({ providedIn: 'root' })
export class NewGameService {
  private readonly companyService: CompanyService = inject(CompanyService);
  private readonly brandService: BrandService = inject(BrandService);
  private readonly modelService: ModelService = inject(ModelService);
  private readonly router: Router = inject(Router);

  state: WritableSignal<NewGameState> = signal<NewGameState>({
    step: 1,
    companyId: null,
    brandId: null,
  });

  submitCompany(createCompanyRequest: CreateCompanyRequest): Observable<Company> {
    return this.companyService.create(createCompanyRequest).pipe(
      tap((company) => {
        this.state.update((s) => ({ ...s, companyId: company.id, step: 2 }));
      }),
    );
  }

  submitBrand(createBrandRequest: CreateBrandRequest): Observable<Brand> {
    return this.brandService.create(createBrandRequest).pipe(
      tap((brand) => {
        this.state.update((s) => ({ ...s, brandId: brand.id, step: 3 }));
      }),
    );
  }

  submitModel(createModelRequest: CreateModelRequest): Observable<Model> {
    return this.modelService.create(createModelRequest).pipe(
      tap(() => {
        this.router.navigate(['/dashboard']);
      }),
    );
  }

  reset(): void {
    this.state.set({
      step: 1,
      companyId: null,
      brandId: null,
    });
  }
}
