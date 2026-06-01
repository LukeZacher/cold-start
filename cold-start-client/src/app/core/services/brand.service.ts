import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Brand, CreateBrandRequest } from '../models/brand.model';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  private readonly api = inject(ApiService);

  private readonly controllerName: string = 'brand';

  getAll(): Observable<Brand[]> {
    return this.api.get<Brand[]>(`${this.controllerName}`);
  }

  getById(id: number): Observable<Brand> {
    return this.api.get<Brand>(`${this.controllerName}/${id}`);
  }

  create(createBrandRequest: CreateBrandRequest): Observable<Brand> {
    return this.api.post<Brand>(`${this.controllerName}`, createBrandRequest);
  }
}
