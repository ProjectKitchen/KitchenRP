import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Reservation, NewReservation} from "../../types/reservation";
import {catchError, retry} from 'rxjs/operators';
import {Observable, throwError} from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ReservationService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) { }

    getBy(a: Partial<{id: number, startTime: string, endTime: string, userId: number, resourceId: number, status: string}>): Observable<Reservation[]> {
        let params = new HttpParams();
        if(a.id) params = params.set("id", String(a.id));
        if(a.startTime) params = params.set("startTime", a.startTime);
        if(a.endTime) params = params.set("endTime", a.endTime);
        if(a.userId) params = params.set("userId", String(a.userId));
        if(a.resourceId) params = params.set("resourceId", String(a.resourceId));
        if(a.status) params = params.set("status", a.status);

        const url = this.baseUrl + "/reservation";
        return this.http.get<Reservation[]>(url, { params: params });
        /*.pipe(
            retry(3),
            catchError(this.handeError)
        )*/
    }

    getById(id: number){
        let params = new HttpParams().set("id", String(id));
        const url = this.baseUrl + "reservation";
        return this.http.get<Reservation[]>(url, { params: params });
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
