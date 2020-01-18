import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {flatMap} from "rxjs/operators";
import {ResourceService} from "../../../services/resource/resource.service";
import {Observable} from "rxjs";
import {Resource} from "../../../types/resource";

@Component({
    selector: 'app-resource-calendar',
    templateUrl: './resource-calendar.component.html',
    styleUrls: ['./resource-calendar.component.css']
})
export class ResourceCalendarComponent implements OnInit {

    private resource$: Observable<Resource>;

    constructor(private route: ActivatedRoute, private resourceService: ResourceService) {
        this.resource$ = route.params.pipe(
            flatMap(params =>  resourceService.getById(+params['id']))
        )
    }

    ngOnInit() {
    }

}
