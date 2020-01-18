import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Resource, NewResource, ResourceType} from "../../types/resource";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ResourceService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) { }

    // -- Resource --
    getById(id: number) {
        const url = this.baseUrl +"/resource/" + id;
        return this.http.get<Resource>(url);
    }

    getAll(){
        const url = this.baseUrl + "/resource";
        return this.http.get<Resource[]>(url);
    }

    getByType(type: string) {
        const url = this.baseUrl + "/resource?requestType=" + type;
        return this.http.get<Resource[]>(url);
    }

    create(resource: NewResource): Observable<Resource> {
        const url = this.baseUrl + "/resource";
        return this.http.post<Resource>(url, resource);
    }

    update(resource: Resource): Observable<Resource> {
        const url = this.baseUrl + "/resource";
        return this.http.put<Resource>(url, resource);
    }

    delete(id: number): Observable<{}> {
        const url = this.baseUrl + "/resource/" + id;
        return this.http.delete(url);
    }


    // -- ResourceTypes --
    getAllTypes(){
        const url = this.baseUrl + "/resource/type";
        return this.http.get<ResourceType[]>(url)
    }

    getType(type: string){
        const url = this.baseUrl + "/resource/type/" + type;
        return this.http.get<ResourceType>(url);
    }

    createType(type: ResourceType): Observable<ResourceType>{
        const url = this.baseUrl + "/resource/type";
        return this.http.post<ResourceType>(url, type);
    }

    deleteType(type: string): Observable<{}>{
        const url = this.baseUrl + "/resource/type/" + type;
        return this.http.delete(url);
    }
}
