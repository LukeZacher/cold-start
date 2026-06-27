import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { CreateModelRequest, Model } from '../models/model.model';
import { BaseEntityService } from './base-entity.service';

@Injectable({
  providedIn: 'root',
})
export class ModelService extends BaseEntityService<Model> {
  protected readonly controllerName: string = 'model';
}
