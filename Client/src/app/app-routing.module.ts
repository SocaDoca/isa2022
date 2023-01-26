import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { NgImageSliderModule } from 'ng-image-slider';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { UserPersonalProfileComponent } from './user-personal-profile/user-personal-profile.component';
import { SearchClinicComponent } from './search-clinic/search-clinic.component';
import { UserPersonalProfileUpdateComponent } from './user-personal-profile-update/user-personal-profile-update.component';
import { EmployeePersonalProfileComponent } from './employee-personal-profile/employee-personal-profile.component';
import { EmployeePersonalProfileUpdateComponent } from './employee-personal-profile-update/employee-personal-profile-update.component';
import { ViewClinicComponent } from './view-clinic/view-clinic.component';
import { AdminAddTermComponent } from './admin-add-term/admin-add-term.component';
import { VisitationHistoryUserComponent } from './visitation-history-user/visitation-history-user.component';
import { TransfusionQuestionnaireComponent } from './transfusion-questionnaire/transfusion-questionnaire.component';
import { UserComplaintComponent } from './user-complaint/user-complaint.component';
import { UserScheduledAppointmentsComponent } from './user-scheduled-appointments/user-scheduled-appointments.component';
import { SearchUserComponent } from './search-user/search-user.component';
import { CalendarComponent } from './calendar/calendar.component';
import { VerifyUserComponent } from './verify-user/verify-user.component';
import { ChangePassComponent } from './change-pass/change-pass.component';
import { AddClinicComponent } from './add-clinic/add-clinic.component';
import { UpdateClinicComponent } from './update-clinic/update-clinic.component';
import { ViewClinicUserComponent } from './view-clinic-user/view-clinic-user.component';
import { StartAppointmentComponent } from './start-appointment/start-appointment.component';
import { AppointmentReportComponent } from './appointment-report/appointment-report.component';


const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: 'sign-in', component: SignInPageComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'profile/:id', component: UserPersonalProfileComponent },
  { path: 'profile/:id/visitationHistory', component: VisitationHistoryUserComponent },
  { path: 'profile/:id/viewClinicUser', component: ViewClinicUserComponent },
  { path: 'profile/:id/questionnaire', component: TransfusionQuestionnaireComponent },
  { path: 'profile/:id/complaint', component: UserComplaintComponent },
  { path: 'profile/:id/scheduledAppointments', component: UserScheduledAppointmentsComponent },
  { path: 'profileEmployee/:id', component: EmployeePersonalProfileComponent },
  { path: 'profileEmployee/:id/viewClinic', component: ViewClinicComponent },
  { path: 'profileEmployee/:id/viewClinic/update', component: UpdateClinicComponent },
  { path: 'profileEmployee/:id/addTerm', component: AdminAddTermComponent },
  { path: 'profileEmployee/:id/updateEmployeeProfile', component: EmployeePersonalProfileUpdateComponent },
  { path: 'profileEmployee/:id/add-clinic', component: AddClinicComponent },
  { path: 'profile/:id/updateProfile', component: UserPersonalProfileUpdateComponent },
  { path: 'profileEmployee/:id/searchUser', component: SearchUserComponent },
  { path: 'profileEmployee/:id/searchUser/start-appointment', component: StartAppointmentComponent },
  { path: 'profileEmployee/:id/searchUser/start-appointment/report', component: AppointmentReportComponent },
  { path: 'search', component: SearchClinicComponent },
  { path: 'change-pass', component: ChangePassComponent },
  { path: 'profileEmployee/:id/calendar', component: CalendarComponent },
  { path: 'Auth/verify-user', component: VerifyUserComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
