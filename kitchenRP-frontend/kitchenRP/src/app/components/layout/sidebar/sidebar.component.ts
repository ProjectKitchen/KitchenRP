import {Component, OnInit} from '@angular/core';
import {ResourceService} from "../../../services/resource/resource.service";
import {Observable} from "rxjs";
import {Resource} from "../../../types/resource";
import {groupBy} from "../../../util";
import {map} from "rxjs/operators";

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

    private resources$: Observable<Resource[][]>;

    constructor(private resourceService: ResourceService) {
        this.resources$ = this.resourceService.getAll().pipe(
            map(resources => groupBy(resources, (r) => r.resourceType.type))
        )
    }

    ngOnInit() {
    }

}
