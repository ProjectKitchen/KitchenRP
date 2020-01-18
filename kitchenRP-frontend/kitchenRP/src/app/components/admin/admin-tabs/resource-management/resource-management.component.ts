import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalResourceComponent} from "../../../../modals/modal-resource/modal-resource.component";

import {Resource} from "../../../../types/resource";
import {ResourceService} from "../../../../services/resource/resource.service";

// test data
const table: Resource [] = [
    {
        "id": 1,
        "displayName":"Printer1",
        "metaData": "PLA, 20x20",
        "description": "123456",
        "resourceType": "3D-Printer"
    }
];

function search(text: string, pipe: PipeTransform): Resource[] {
    return table.filter(resource => {
        const term = text.toLowerCase();
        return resource.displayName.toLowerCase().includes(term)
            || resource.metaData.toLowerCase().includes(term)
            || resource.resourceType.toLowerCase().includes(term);
        //|| pipe.transform(resource.id).includes(term); // ID search?
    });
}

@Component({
    selector: 'app-resource-management',
    templateUrl: './resource-management.component.html',
    styleUrls: ['./resource-management.component.css'],
    providers: [DecimalPipe]
})
export class ResourceManagementComponent implements OnInit {

    resources$: Observable<Resource[]>;
    filter = new FormControl('');

    constructor(private resourceService: ResourceService, private modalService: NgbModal, pipe: DecimalPipe) {
        this.resources$ = this.filter.valueChanges.pipe(
            startWith(''),
            map(text => search(text, pipe))
        );
        this.loadData();
    }

    ngOnInit() {
    }

    loadData() {
        console.log("Load Resources here:");
        var resources = this.resourceService.getAll("3D_PRINTER");

        resources.subscribe(val => {
            console.log(val);
        });
        //this.resources$ = resources;
    }

    openModal(tableRow) {
        const modalRef = this.modalService.open(ModalResourceComponent, { windowClass : "modal-size-lg"});
        modalRef.componentInstance.Data = tableRow;
    }
}
