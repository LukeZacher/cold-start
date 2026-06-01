import { Component, inject } from '@angular/core';
import { NewGameService } from '../../services/new-game.service';
import { CreateCompany } from '../create-company/create-company';
import { CreateBrand } from '../../../../shared/components/create-brand/create-brand';
import { CreateModel } from '../../../../shared/components/create-model/create-model';

@Component({
  selector: 'app-new-game-shell',
  imports: [CreateCompany, CreateBrand, CreateModel],
  templateUrl: './new-game-shell.html',
  styleUrl: './new-game-shell.scss',
})
export class NewGameShell {
  readonly newGameService = inject(NewGameService);

  readonly steps = [
    { number: 1, label: 'Company' },
    { number: 2, label: 'Brand' },
    { number: 3, label: 'Model' },
  ];
}
