import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPersonalProfileComponent } from './user-personal-profile.component';

describe('UserPersonalProfileComponent', () => {
  let component: UserPersonalProfileComponent;
  let fixture: ComponentFixture<UserPersonalProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPersonalProfileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserPersonalProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
