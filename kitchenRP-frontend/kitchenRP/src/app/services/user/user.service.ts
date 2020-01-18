import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Reservation} from "../../types/reservation";
import {User} from "../../types/user";

@Injectable({
  providedIn: 'root'
})
export class UserService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) { }

    get(id: number) {
        return this.http.get<User>(this.baseUrl +"/user/" + id);
    }

    getAll() {
        return this.http.get<User[]>(this.baseUrl + "/user");
    }

    getMyReservations() {

    }

    create() {

    }

    update() {
    }

    delete() {
    }
}
