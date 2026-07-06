import { BodyStyle, FuelType, SizeSegment } from './enums/vehicle.enum';

export interface Model {
  id: number;
  name: string;
  bodyStyle: BodyStyle;
  size: SizeSegment;
  fuelType: FuelType;
  baseMsrpNew?: number;
  maxMsrpNew?: number;
  warrantyMiles?: number;
  safetyRating?: number;
  brandId: number;
}

export interface CreateModelRequest {
  name: string;
  brandId: number;
  bodyStyle: BodyStyle;
  size: SizeSegment;
  fuelType: FuelType;
}
