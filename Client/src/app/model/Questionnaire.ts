export interface QuestionnaireInterface {
  id?: any;
  patient_RefID?: string;
  question1?: any;
  question2?: any;
  question3?: any;
  question4?: any;
  question5?: any;
  question6?: any;
  question7?: any;
  question8?: any;
  question9?: any;
  question10?: any;
  question11?: any;
  question12?: any;
  isValid?: any;
  isQuestionnaireValid?: any;
}

export class Questionnaire implements QuestionnaireInterface {
  id?: any;
  patient_RefID?: string;
  question1?: any;
  question2?: any;
  question3?: any;
  question4?: any;
  question5?: any;
  question6?: any;
  question7?: any;
  question8?: any;
  question9?: any;
  question10?: any;
  question11?: any;
  question12?: any;
  isValid?: any;
  isQuestionnaireValid?: any;

  constructor(obj: QuestionnaireInterface) {
    this.id = obj.id;
    this.patient_RefID = obj.patient_RefID;
    this.question1 = obj.question1;
    this.question2 = obj.question2;
    this.question3 = obj.question3;
    this.question4 = obj.question4;
    this.question5 = obj.question5;
    this.question6= obj.question6;
    this.question7 = obj.question7;
    this.question8 = obj.question8;
    this.question9 = obj.question9;
    this.question10 = obj.question10;
    this.question11 = obj.question11;
    this.question12 = obj.question12;
    this.isValid = obj.isValid;
    this.isQuestionnaireValid = obj.isQuestionnaireValid;
  }
}
