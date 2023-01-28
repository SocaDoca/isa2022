import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleAppComponent } from './schedule-app.component';

describe('ScheduleAppComponent', () => {
  let component: ScheduleAppComponent;
  let fixture: ComponentFixture<ScheduleAppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ScheduleAppComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
