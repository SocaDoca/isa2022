import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPersonalProfileUpdateComponent } from './user-personal-profile-update.component';

describe('UserPersonalProfileUpdateComponent', () => {
  let component: UserPersonalProfileUpdateComponent;
  let fixture: ComponentFixture<UserPersonalProfileUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPersonalProfileUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserPersonalProfileUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
