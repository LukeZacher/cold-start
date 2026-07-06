import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { provideSpinnerConfig } from 'ngx-spinner';
import { authInterceptor, loadingInterceptor } from './core/interceptors';
import { provideIcons } from '@ng-icons/core';
import { appIcons } from './core/icons';
import {
  provideTippyLoader,
  provideTippyConfig,
  tooltipVariation,
  popperVariation,
} from '@ngneat/helipopper/config';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([loadingInterceptor, authInterceptor])),
    provideAnimations(),
    provideToastr(),
    provideSpinnerConfig({ type: 'ball-scale-multiple' }),
    provideIcons(appIcons),
    provideTippyLoader(() => import('tippy.js')),
    provideTippyConfig({
      defaultVariation: 'tooltip',
      variations: {
        tooltip: tooltipVariation,
        popper: popperVariation,
      },
    }),
  ],
};
