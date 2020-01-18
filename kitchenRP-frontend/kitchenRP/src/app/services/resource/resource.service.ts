import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Resource} from "../../types/resource";

@Injectable({
  providedIn: 'root'
})
export class ResourceService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) { }

    get(id: number) {
        return this.http.get<Resource>(this.baseUrl +"/resource/" + id);
    }

    getAll(type: string) {
        return this.http.get<Resource[]>(this.baseUrl + "/resource?resourceType=" + type);
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
