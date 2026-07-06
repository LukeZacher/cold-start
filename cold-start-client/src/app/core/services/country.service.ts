import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Country } from '../models/country.model';

@Injectable({ providedIn: 'root' })
export class CountryService {
  private readonly apiService: ApiService = inject(ApiService);

  private readonly controllerName: string = 'country';

  getAll(): Observable<Country[]> {
    return this.apiService.get<Country[]>(`${this.controllerName}`);
  }
}
