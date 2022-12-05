import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './main-page/main-page.component';
import { NavbarComponent } from './navbar/navbar.component';
import { NgImageSliderModule } from 'ng-image-slider';
import { MatIconModule } from '@angular/material/icon';
import { NavbarProfileComponent } from './navbar-profile/navbar-profile.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { UserPersonalProfileComponent } from './user-personal-profile/user-personal-profile.component';
import { SearchClinicComponent } from './search-clinic/search-clinic.component';
import { UserPersonalProfileUpdateComponent } from './user-personal-profile-update/user-personal-profile-update.component';
import { EmployeePersonalProfileComponent } from './employee-personal-profile/employee-personal-profile.component';
import { EmployeePersonalProfileUpdateComponent } from './employee-personal-profile-update/employee-personal-profile-update.component';
import { ViewClinicComponent } from './view-clinic/view-clinic.component';
import { AdminAddTermComponent } from './admin-add-term/admin-add-term.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { VisitationHistoryUserComponent } from './visitation-history-user/visitation-history-user.component';
import { TransfusionQuestionnaireComponent } from './transfusion-questionnaire/transfusion-questionnaire.component';
import { UserComplaintComponent } from './user-complaint/user-complaint.component';
import { CalendarComponent } from './calendar/calendar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';





@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    NavbarComponent,
    NavbarProfileComponent,
    SignInPageComponent,
    RegisterPageComponent,
    UserPersonalProfileComponent,
    SearchClinicComponent,
    UserPersonalProfileUpdateComponent,
    EmployeePersonalProfileComponent,
    EmployeePersonalProfileUpdateComponent,
    ViewClinicComponent,
    AdminAddTermComponent,
    VisitationHistoryUserComponent,
    TransfusionQuestionnaireComponent,
    UserComplaintComponent,
    CalendarComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatIconModule,
    NgImageSliderModule,
    GoogleMapsModule,
    NgbModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
