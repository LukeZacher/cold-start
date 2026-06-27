import { Routes } from '@angular/router';

export const newGameRoutes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/new-game-shell/new-game-shell').then((c) => c.NewGameShell),
  },
];
