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
        "name":"Printer1",
        "meta": "PLA, 20x20",
        "type": "3D-Printer",
        "description": "123456"
    },{
        "id": 2,
        "name":"Printer2",
        "meta": "123",
        "type": "3D-Printer",
        "description": "aoewifaoewf"
    },{
        "id": 3,
        "name":"Printer3",
        "meta": "PLA, 20x20",
        "type": "3D-Printer",
        "description": "aoweifaowef"
    },{
        "id": 4,
        "name":"Printer4",
        "meta": "PLA, 20x20",
        "type": "3D-Printer",
        "description": "öoawnfeoawnef"
    },{
        "id": 5,
        "name":"Printer5",
        "meta": "PLA, 20x20",
        "type": "3D-Printer",
        "description": "eäsrbmserb"
    },{
        "id": 6,
        "name":"Printer6",
        "meta": "PLA, 20x20",
        "type": "3D-Printer",
        "description": "aüoaiefüoaef"
    }
];

function search(text: string, pipe: PipeTransform): Resource[] {
    return table.filter(resource => {
        const term = text.toLowerCase();
        return resource.name.toLowerCase().includes(term)
            || resource.meta.toLowerCase().includes(term)
            || resource.type.toLowerCase().includes(term);
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
        table[0] = {
            "id": 1,
            "name":"Kein Printer",
            "meta": "PLA, 20x20",
            "type": "Test Typ",
            "description": "987654321"
        };
    }

    openModal(tableRow) {
        const modalRef = this.modalService.open(ModalResourceComponent, { windowClass : "modal-size-lg"});
        modalRef.componentInstance.Data = tableRow;
    }
}
