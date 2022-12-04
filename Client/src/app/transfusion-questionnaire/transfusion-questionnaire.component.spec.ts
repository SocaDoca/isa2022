import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransfusionQuestionnaireComponent } from './transfusion-questionnaire.component';

describe('TransfusionQuestionnaireComponent', () => {
  let component: TransfusionQuestionnaireComponent;
  let fixture: ComponentFixture<TransfusionQuestionnaireComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransfusionQuestionnaireComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransfusionQuestionnaireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
