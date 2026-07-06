import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ActiveRequestsService {
  private numActiveRequests: number = 0;

  increment(): void {
    this.numActiveRequests++;
  }

  decrement(): number {
    this.numActiveRequests--;
    return this.numActiveRequests;
  }
}
