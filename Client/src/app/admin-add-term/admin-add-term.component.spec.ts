import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAddTermComponent } from './admin-add-term.component';

describe('AdminAddTermComponent', () => {
  let component: AdminAddTermComponent;
  let fixture: ComponentFixture<AdminAddTermComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminAddTermComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminAddTermComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
