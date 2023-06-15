export interface ClinicRatingInterface {
  clinicId?: any;
  patientId?: any;
  rating?: string;
}

export class ClinicRating implements ClinicRatingInterface{
  clinicId?: any;
  patientId?: any;
  rating?: string;


  constructor(obj: ClinicRatingInterface) {
    this.clinicId = obj.clinicId;
    this.patientId = obj.patientId;
    this.rating = obj.rating;
  }
}
