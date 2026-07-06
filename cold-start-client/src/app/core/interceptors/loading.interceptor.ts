import { HttpContextToken, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { SpinnerService, ActiveRequestsService } from '../services';
import { finalize } from 'rxjs';

export const SKIP_LOADING_SPINNER: HttpContextToken<boolean> = new HttpContextToken<boolean>(
  () => false,
);

export const loadingInterceptor: HttpInterceptorFn = (request, next) => {
  if (request.context.get(SKIP_LOADING_SPINNER)) {
    return next(request);
  }

  const spinnerService: SpinnerService = inject(SpinnerService);
  const activeRequestsService: ActiveRequestsService = inject(ActiveRequestsService);

  activeRequestsService.increment();
  spinnerService.show();

  return next(request).pipe(
    finalize(() => {
      const remainingActiveRequests: number = activeRequestsService.decrement();
      if (remainingActiveRequests === 0) {
        spinnerService.hide();
      }
    }),
  );
};
