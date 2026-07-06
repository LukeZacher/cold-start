import {
  Component,
  inject,
  OnInit,
  signal,
  ChangeDetectionStrategy,
  WritableSignal,
} from '@angular/core';
import { BrandService } from '../../../../core/services/brand.service';
import { Brand } from '../../../../core/models/brand.model';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Dashboard implements OnInit {
  private readonly brandService: BrandService = inject(BrandService);

  readonly brands: WritableSignal<Brand[]> = signal<Brand[]>([]);

  ngOnInit(): void {
    this.brandService.getAll().subscribe((data) => {
      this.brands.set(data);
      console.log('brands', this.brands);
    });
  }
}
