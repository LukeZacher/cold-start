import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { CreateModelRequest, Model } from '../models/model.model';

@Injectable({
  providedIn: 'root',
})
export class ModelService {
  private readonly api = inject(ApiService);

  private readonly controllerName: string = 'model';

  getAll(): Observable<Model[]> {
    return this.api.get<Model[]>(`${this.controllerName}`);
  }

  getById(id: number): Observable<Model> {
    return this.api.get<Model>(`${this.controllerName}/${id}`);
  }

  create(createModelRequest: CreateModelRequest): Observable<Model> {
    return this.api.post<Model>(`${this.controllerName}`, createModelRequest);
  }
}
