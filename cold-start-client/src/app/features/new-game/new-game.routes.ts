import { Routes } from '@angular/router';
import { NewGameShell } from './components/new-game-shell/new-game-shell';

export const newGameRoutes: Routes = [
  {
    path: '',
    component: NewGameShell,
  },
];
