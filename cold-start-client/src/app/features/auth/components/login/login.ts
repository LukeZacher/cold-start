import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { APP_NAME } from '../../../../core/constants';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private readonly titleService: Title = inject(Title);

  constructor() {
    this.titleService.setTitle(`Login - ${APP_NAME}`);
  }
}
