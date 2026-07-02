import { Routes } from '@angular/router';

export const authRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./components/auth-shell/auth-shell').then((c) => c.AuthShell),
    children: [
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
        path: 'registration-success',
        loadComponent: () =>
          import('./components/registration-success/registration-success').then(
            (c) => c.RegistrationSuccess,
          ),
      },
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full',
      },
    ],
  },
];
