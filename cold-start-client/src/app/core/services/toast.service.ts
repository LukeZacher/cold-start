import { inject, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class ToastService {
  private readonly toastService: ToastrService = inject(ToastrService);

  success(message: string): void {
    this.toastService.success(message);
  }

  error(message: string): void {
    this.toastService.error(message);
  }

  info(message: string): void {
    this.toastService.info(message);
  }

  warning(message: string): void {
    this.toastService.warning(message);
  }
}
