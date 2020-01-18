import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Reservation} from "../../types/reservation";

@Injectable({
    providedIn: 'root'
})
export class ReservationService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) { }

    get(id: number) {

    }

    getAll() {
        return this.http.get<Reservation[]>(this.baseUrl + "/resources");
    }

    getMyReservations() {

    }

    create() {

    }

    update() {
        // set old reservation to aborted
        // new reservation created with data
    }

    delete() {

    }
}
