import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../../service/user.service';
import { Questionnaire } from '../model/Questionnaire';

@Component({
  selector: 'app-transfusion-questionnaire',
  templateUrl: './transfusion-questionnaire.component.html',
  styleUrls: ['./transfusion-questionnaire.component.css']
})
export class TransfusionQuestionnaireComponent implements OnInit {

  questionnaire: Questionnaire;
  patientId: any;
  submitted: boolean = false;
  //isValid: any;

  form: FormGroup = new FormGroup({
    question1: new FormControl(''),
    question2: new FormControl(''),
    question3: new FormControl(''),
    question4: new FormControl(''),
    question5: new FormControl(''),
    question6: new FormControl(''),
    question7: new FormControl(''),
    question8: new FormControl(''),
    question9: new FormControl(''),
    question10: new FormControl(''),
    question11: new FormControl(''),
    question12: new FormControl(''),


  });



  constructor(private userService: UserService, private modalService: NgbModal, private route: ActivatedRoute, private router: Router) {
    this.questionnaire = new Questionnaire({
      question1: undefined,
      question2: undefined,
      question3: undefined,
      question4: undefined,
      question5: undefined,
      question6: undefined,
      question7: undefined,
      question8: undefined,
      question9: undefined,
      question10: undefined,
      question11: undefined,
      question12: undefined,
      isValid: undefined

    });

  }

  ngOnInit() {

  }

  onSubmit(): void {
    this.submitted = true;


    if (this.form.invalid) {
      return;
    } else {
      this.submitQuestionnaire();
      //console.log(JSON.stringify(this.form.value, null, 2));

    }
  }

  isSubmitted = false;
  submitForm(form: NgForm) {
    this.isSubmitted = true;
    if (!form.valid) {
      return false;
    } else {
      return alert(JSON.stringify(form.value))
    }
  }

  submitQuestionnaire() {
    this.patientId = this.route.snapshot.params['id'];
    this.userService.saveQuestionnaire(this.questionnaire, this.patientId).subscribe(res => {
      this.questionnaire = res;

      console.log(res);
    })

  }
}
