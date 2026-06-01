import { Routes } from '@angular/router';
import { Dashboard } from './features/dashboard/components/dashboard/dashboard';
import { Home } from './features/home/components/home/home';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: 'new-game',
    loadChildren: () => import('./features/new-game/new-game.routes').then((r) => r.newGameRoutes),
  },
  { path: 'dashboard', component: Dashboard },
  { path: '**', redirectTo: '' },
];
