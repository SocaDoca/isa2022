import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeePersonalProfileUpdateComponent } from './employee-personal-profile-update.component';

describe('EmployeePersonalProfileUpdateComponent', () => {
  let component: EmployeePersonalProfileUpdateComponent;
  let fixture: ComponentFixture<EmployeePersonalProfileUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeePersonalProfileUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeePersonalProfileUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
