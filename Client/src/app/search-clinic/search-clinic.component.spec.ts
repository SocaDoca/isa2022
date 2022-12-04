import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchClinicComponent } from './search-clinic.component';

describe('SearchClinicComponent', () => {
  let component: SearchClinicComponent;
  let fixture: ComponentFixture<SearchClinicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchClinicComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchClinicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
