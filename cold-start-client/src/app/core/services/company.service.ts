import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Company, CreateCompanyRequest } from '../models/company.model';

@Injectable({ providedIn: 'root' })
export class CompanyService {
  private readonly api = inject(ApiService);

  private readonly controllerName: string = 'company';

  getAll(): Observable<Company[]> {
    return this.api.get<Company[]>(`${this.controllerName}`);
  }

  getById(id: number): Observable<Company> {
    return this.api.get<Company>(`${this.controllerName}/${id}`);
  }

  create(createCompanyRequest: CreateCompanyRequest): Observable<Company> {
    return this.api.post<Company>(`${this.controllerName}`, createCompanyRequest);
  }
}
