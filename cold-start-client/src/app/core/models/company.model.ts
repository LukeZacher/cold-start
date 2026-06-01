export interface Company {
  id: number;
  name: string;
  description?: string;
  countryId: number;
  userCreated: boolean;
}

export interface CreateCompanyRequest {
  name: string;
  countryId: number;
  userCreated: boolean;
}
