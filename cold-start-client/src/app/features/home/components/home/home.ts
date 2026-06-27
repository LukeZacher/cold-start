import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  private readonly router = inject(Router);

  startNewGame(): void {
    this.router.navigate(['/auth/register']);
  }

  loadGame(): void {}

  goToAboutPage(): void {}
}
