import { Routes } from '@angular/router';
import { Dashboard } from './features/dashboard/components/dashboard/dashboard';
import { Home } from './features/home/components/home/home';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: 'new-game',
    loadChildren: () => import('./features/new-game/new-game.routes').then((r) => r.newGameRoutes),
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then((r) => r.authRoutes),
  },
  { path: 'dashboard', component: Dashboard, canActivate: [authGuard] },
  { path: '**', redirectTo: '' },
];
