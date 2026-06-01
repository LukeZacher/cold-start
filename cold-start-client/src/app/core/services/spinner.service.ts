import { inject, Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({ providedIn: 'root' })
export class SpinnerService {
  private readonly spinnerService = inject(NgxSpinnerService);

  show(): void {
    this.spinnerService.show();
  }

  hide(): void {
    this.spinnerService.hide();
  }
}
