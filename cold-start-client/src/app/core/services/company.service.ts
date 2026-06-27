import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Company, CreateCompanyRequest } from '../models/company.model';
import { BaseEntityService } from './base-entity.service';

@Injectable({ providedIn: 'root' })
export class CompanyService extends BaseEntityService<Company> {
  protected readonly controllerName: string = 'company';
}
