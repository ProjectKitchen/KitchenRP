import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {User, NewUser} from "../../types/user";
import {catchError, retry} from 'rxjs/operators';
import {Observable, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) { }

    getById(id: number) {
        const url = this.baseUrl + "/user/" + id;
        return this.http.get<User>(url);
        /*.pipe(
            retry(3),
            catchError(this.handeError)
        )*/
    }

    getAll() {
        const url = this.baseUrl + "/user";
        return this.http.get<User[]>(url);
    }

    create(user: NewUser): Observable<User> {
        const url = this.baseUrl + "/user";
        return this.http.post<User>(url, user);
    }

    update(user: User): Observable<User> {
        const url = this.baseUrl + "/user";
        return this.http.put<User>(url, user);
    }

    promote(id: string): Observable<User> {
        const url = this.baseUrl + "/" + id + "/promote";
        return this.http.put<User>(url, {});
    }

    demote(id: string): Observable<User> {
        const url = this.baseUrl + "/" + id + "/demote";
        return this.http.put<User>(url, {});
    }

    delete(id: number): Observable<{}> {
        const url = this.baseUrl + "/user/" + id;
        return this.http.delete(url);
    }

    getByName(userName: string) {
        const url = this.baseUrl + "/user?username=" + userName;
        return this.http.get(url);
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
