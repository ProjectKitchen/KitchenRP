import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Reservation, NewReservation} from "../../types/reservation";
import {catchError, retry} from 'rxjs/operators';
import {Observable, throwError} from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ReservationService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) { }

    get(id: number) {
        const url = this.baseUrl + "/reservation/" + id;
        return this.http.get<Reservation[]>(url);
        /*.pipe(
            retry(3),
            catchError(this.handeError)
        )*/
    }

    getAll() {
        const url = this.baseUrl + "/reservation";
        return this.http.get<Reservation[]>(url);
    }

    create(reservation: NewReservation): Observable<Reservation> {
        const url = this.baseUrl + "/reservation";
        return this.http.post<Reservation>(url, reservation);
    }

    update() {
        // set old reservation to aborted
        // new reservation created with data
    }

    delete(id: number): Observable<{}> {
        const url = this.baseUrl + "/reservation/" + id;
        return this.http.delete(url);
    }

    /*private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
           console.error("An error occurred:", error.error.message);
        } else {
           console.error(
           "Backend returned code " + error.status + ", " +
           "body was: " + error.error);
        }
        return throwError("Something bad happened; please try again later.");
    };*/
}
