import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { APP_NAME } from '../../../../core/constants';

@Component({
  selector: 'app-confirm-email',
  imports: [],
  templateUrl: './confirm-email.html',
  styleUrl: './confirm-email.scss',
})
export class ConfirmEmail {
  private readonly titleService: Title = inject(Title);

  constructor() {
    this.titleService.setTitle(`Confirm Email - ${APP_NAME}`);
  }
}
