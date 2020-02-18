import {Component, Input, OnInit} from '@angular/core';
import {Resource} from "../../../types/resource";

@Component({
    selector: 'app-resource-info',
    templateUrl: './resource-info.component.html',
    styleUrls: ['./resource-info.component.css']
})
export class ResourceInfoComponent implements OnInit {

    @Input('resource')
    private resource: Resource;

    constructor() {
    }

    ngOnInit() {
    }

}
