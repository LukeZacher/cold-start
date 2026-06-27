import { inject } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

export abstract class BaseEntityService<T> {
  protected readonly apiService = inject(ApiService);
  protected abstract readonly controllerName: string;

  getAll(): Observable<T[]> {
    return this.apiService.get<T[]>(this.controllerName);
  }

  getById(id: number): Observable<T> {
    return this.apiService.get<T>(`${this.controllerName}/${id}`);
  }

  create<TRequest>(request: TRequest): Observable<T> {
    return this.apiService.post<T>(this.controllerName, request);
  }
}
