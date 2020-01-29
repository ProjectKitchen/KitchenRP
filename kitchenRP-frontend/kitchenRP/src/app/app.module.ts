import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';

import {environment} from '../environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { AccountComponent } from './components/layout/navbar/account/account.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { ReservationControlComponent } from './components/calendar/reservation-control/reservation-control.component';
import { FullcalComponent } from './components/calendar/fullcal/fullcal.component';
import { ReservationStatusListComponent } from './components/reservations/reservation-status-list/reservation-status-list.component';
import { AdminComponent } from './components/admin/admin.component';
import { AdminTabsComponent } from './components/admin/admin-tabs/admin-tabs.component';
import { UserManagementComponent } from './components/admin/admin-tabs/user-management/user-management.component';
import { RestrictionManagementComponent } from './components/admin/admin-tabs/restriction-management/restriction-management.component';
import { ReservationModalComponent } from './components/reservations/reservation-status-list/reservation-modal/reservation-modal.component';
import { AllReservationsComponent } from './components/all-reservations/all-reservations.component';
import { AllReservationsStatusListComponent } from './components/all-reservations/all-reservations-status-list/all-reservations-status-list.component';
import { LoginComponent } from './components/login/login.component';

import { ModalReservationComponent } from './modals/modal-reservation/modal-reservation.component';
import { ModalUserComponent } from './modals/modal-user/modal-user.component';
import { ModalRestrictionComponent } from './modals/modal-restriction/modal-restriction.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { FullCalendarModule } from '@fullcalendar/angular';
import { ResourceManagementComponent } from './components/admin/admin-tabs/resource-management/resource-management.component';
import { ModalResourceComponent } from './modals/modal-resource/modal-resource.component';
import { ResourceInfoComponent } from './components/calendar/resource-info/resource-info.component';
import { TokenInterceptor } from "./token.interceptor";
import { AuthGuardUser } from "./services/auth/auth-guard-user.service";
import {AuthGuardAdmin} from "./services/auth/auth-guard-admin.service";
import {AuthGuardModerator} from "./services/auth/auth-guard-moderator.service";
import {AuthGuardLoggedIn} from "./services/auth/auth-guard-logged-in";
import { ResourceCalendarComponent } from './components/calendar/resource-calendar/resource-calendar.component';
@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        ReservationsComponent,
        CalendarComponent,
        AccountComponent,
        SidebarComponent,
        FooterComponent,
        ReservationControlComponent,
        FullcalComponent,
        ReservationStatusListComponent,
        AdminComponent,
        AdminTabsComponent,
        UserManagementComponent,
        ReservationModalComponent,
        AllReservationsComponent,
        AllReservationsStatusListComponent,
        LoginComponent,
        ModalReservationComponent,
        ModalUserComponent,
        RestrictionManagementComponent,
        ModalRestrictionComponent,
        ResourceManagementComponent,
        ModalResourceComponent,
        ResourceInfoComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        FullCalendarModule
    ],
    providers: [
        AuthGuardUser,
        AuthGuardModerator,
        AuthGuardAdmin,
        AuthGuardLoggedIn,
        {provide: 'API_BASE_URL', useValue: environment.baseUrl}, {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}
    ],
    bootstrap: [AppComponent],
    entryComponents: [
        ModalReservationComponent,
        ModalResourceComponent,
        ModalUserComponent,
        ModalRestrictionComponent
    ]
})
export class AppModule { }
