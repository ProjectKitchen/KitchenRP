import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Restriction, NewRestriction} from "../../types/restriction";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RestrictionService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) { }

    getByResourceId(rId: number) {
        const url = this.baseUrl +"/restriction/" + rId;
        return this.http.get<Restriction>(url);
    }

    getAll(){
        const url = this.baseUrl + "/restriction";
        return this.http.get<Restriction[]>(url);
    }

    create(restriction: NewRestriction): Observable<Restriction> {
        const url = this.baseUrl + "/restriction";
        return this.http.post<Restriction>(url, restriction);
    }

    update(restriction): Observable<Restriction> {
        const url = this.baseUrl + "/restriction";
        return this.http.put<Restriction>(url, restriction);
    }

    delete(id: number): Observable<{}> {
        const url = this.baseUrl + "/restriction/" + id;
        return this.http.delete(url);
    }
}
