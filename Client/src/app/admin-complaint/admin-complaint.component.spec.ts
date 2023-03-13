import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminComplaintComponent } from './admin-complaint.component';

describe('AdminComplaintComponent', () => {
  let component: AdminComplaintComponent;
  let fixture: ComponentFixture<AdminComplaintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminComplaintComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminComplaintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
