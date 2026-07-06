import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { APP_NAME } from '../../../../core/constants';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  private readonly router: Router = inject(Router);
  private readonly titleService: Title = inject(Title);

  constructor() {
    this.titleService.setTitle(APP_NAME);
  }

  startNewGame(): void {
    this.router.navigate(['/auth/register']);
  }

  loadGame(): void {}

  goToAboutPage(): void {}
}
