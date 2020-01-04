import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';

import {environment} from '../environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { AccountComponent } from './components/layout/navbar/account/account.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { DeviceInfoComponent } from './components/calendar/device-info/device-info.component';
import { ReservationControlComponent } from './components/calendar/reservation-control/reservation-control.component';
import { FullcalComponent } from './components/calendar/fullcal/fullcal.component';
import { ReservationStatusListComponent } from './components/reservations/reservation-status-list/reservation-status-list.component';
import { AdminComponent } from './components/admin/admin.component';
import { AdminTabsComponent } from './components/admin/admin-tabs/admin-tabs.component';
import { UserManagementComponent } from './components/admin/admin-tabs/user-management/user-management.component';
import { DeviceManagementComponent } from './components/admin/admin-tabs/device-management/device-management.component';
import { RestrictionManagementComponent } from './components/admin/admin-tabs/restriction-management/restriction-management.component';
import { ReservationModalComponent } from './components/reservations/reservation-status-list/reservation-modal/reservation-modal.component';
import { AllReservationsComponent } from './components/all-reservations/all-reservations.component';
import { AllReservationsStatusListComponent } from './components/all-reservations/all-reservations-status-list/all-reservations-status-list.component';
import { LoginComponent } from './components/login/login.component';

import { ModalReservationComponent } from './modals/modal-reservation/modal-reservation.component';
import { ModalDeviceComponent } from './modals/modal-device/modal-device.component';
import { ModalUserComponent } from './modals/modal-user/modal-user.component';
import { ModalRestrictionComponent } from './modals/modal-restriction/modal-restriction.component';
import {FormsModule} from "@angular/forms";



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ReservationsComponent,
    CalendarComponent,
    AccountComponent,
    SidebarComponent,
    FooterComponent,
    DeviceInfoComponent,
    ReservationControlComponent,
    FullcalComponent,
    ReservationStatusListComponent,
    AdminComponent,
    AdminTabsComponent,
    UserManagementComponent,
    DeviceManagementComponent,
    ReservationModalComponent,
    AllReservationsComponent,
    AllReservationsStatusListComponent,
    LoginComponent,
    ModalReservationComponent,
    ModalDeviceComponent,
    ModalUserComponent,
    RestrictionManagementComponent,
    ModalRestrictionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule
  ],
  providers: [{provide: 'API_BASE_URL', useValue: environment.baseUrl}],
  bootstrap: [AppComponent],
  entryComponents: [
    ModalReservationComponent,
    ModalDeviceComponent,
    ModalUserComponent,
    ModalRestrictionComponent
  ]
})
export class AppModule { }
