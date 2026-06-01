import { BrandType } from './enums/brand.enums';

export interface Brand {
  id: number;
  name: string;
  type: string;
  description: string;
  country: string;
  foundedYear: number;
}

export interface CreateBrandRequest {
  name: string;
  type: BrandType;
  foundedYear: number;
  countryId: number;
  companyId: number;
}
