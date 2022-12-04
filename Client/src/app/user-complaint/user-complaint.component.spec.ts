import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserComplaintComponent } from './user-complaint.component';

describe('UserComplaintComponent', () => {
  let component: UserComplaintComponent;
  let fixture: ComponentFixture<UserComplaintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserComplaintComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserComplaintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
