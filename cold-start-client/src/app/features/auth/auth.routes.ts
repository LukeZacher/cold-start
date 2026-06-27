import { Routes } from '@angular/router';

export const authRoutes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./components/login/login').then((c) => c.Login),
  },
  {
    path: 'register',
    loadComponent: () => import('./components/register/register').then((c) => c.Register),
  },
  {
    path: 'confirm-email',
    loadComponent: () =>
      import('./components/confirm-email/confirm-email').then((c) => c.ConfirmEmail),
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
];
