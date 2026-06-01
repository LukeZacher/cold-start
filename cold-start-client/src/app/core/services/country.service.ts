import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Country } from '../models/country.model';

@Injectable({ providedIn: 'root' })
export class CountryService {
  private readonly api = inject(ApiService);

  private readonly controllerName: string = 'country';

  getAll(): Observable<Country[]> {
    return this.api.get<Country[]>(`${this.controllerName}`);
  }
}
