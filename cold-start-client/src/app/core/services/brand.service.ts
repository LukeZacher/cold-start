import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Brand, CreateBrandRequest } from '../models/brand.model';
import { ApiService } from './api.service';
import { BaseEntityService } from './base-entity.service';

@Injectable({
  providedIn: 'root',
})
export class BrandService extends BaseEntityService<Brand> {
  protected readonly controllerName: string = 'brand';
}
