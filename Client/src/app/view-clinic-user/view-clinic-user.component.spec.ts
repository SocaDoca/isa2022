import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewClinicUserComponent } from './view-clinic-user.component';

describe('ViewClinicUserComponent', () => {
  let component: ViewClinicUserComponent;
  let fixture: ComponentFixture<ViewClinicUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewClinicUserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewClinicUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
