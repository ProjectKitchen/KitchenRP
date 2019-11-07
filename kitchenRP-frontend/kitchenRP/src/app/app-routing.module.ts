import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReservationsComponent } from "./components/reservations/reservations.component";
import {CalendarComponent} from "./components/calendar/calendar.component";


const routes: Routes = [
    {path: 'calendar', component: CalendarComponent},
    {path: 'reservations', component: ReservationsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
