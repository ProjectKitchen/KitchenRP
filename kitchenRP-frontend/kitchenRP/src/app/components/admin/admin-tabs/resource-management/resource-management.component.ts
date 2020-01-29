import { Component, OnInit, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { tap, map, startWith, flatMap, repeatWhen } from 'rxjs/operators';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import {ModalResourceComponent} from "../../../../modals/modal-resource/modal-resource.component";

import {Resource, ResourceType} from "../../../../types/resource";
import {ResourceService} from "../../../../services/resource/resource.service";

@Component({
    selector: 'app-resource-management',
    templateUrl: './resource-management.component.html',
    styleUrls: ['./resource-management.component.css'],
    providers: [DecimalPipe]
})
export class ResourceManagementComponent implements OnInit {
    private refreshSubject = new Subject<any>();

    data: Resource [] = [];
    resources$: Observable<Resource[]>;
    filter = new FormControl('');

    constructor(private resourceService: ResourceService, private modalService: NgbModal, pipe: DecimalPipe) {
        this.resources$ = this.resourceService.getAll()
        .pipe(
            repeatWhen(_ => this.refreshSubject.asObservable()),
            tap(resources => this.data = resources),
            flatMap(r => this.filter.valueChanges
                .pipe(
                    startWith(''),
                    map(text => this.search(text, pipe))
                )
            )
        )
    }

    ngOnInit() {
    }

    openModal(tableRow) {
        const modalRef = this.modalService.open(ModalResourceComponent, { windowClass : "modal-size-lg"});
        modalRef.componentInstance.Data = tableRow;
        modalRef.componentInstance.refresh = () => this.refresh();
    }

    openModalAdd() {
        const modalRef = this.modalService.open(ModalResourceComponent, { windowClass : "modal-size-lg"});
        modalRef.componentInstance.Data = {id: "-", displayName: "", metaData: {}, description: ""};
        modalRef.componentInstance.Add = true;
        modalRef.componentInstance.refresh = () => this.refresh();
    }

    search(text: string, pipe: PipeTransform): Resource[] {
        return this.data.filter(resource => {
            const term = text.toLowerCase();
            return resource.displayName.toLowerCase().includes(term)
                || resource.resourceType['type'].toLowerCase().includes(term);
                //|| resource.metaData.toLowerCase().includes(term)
                //|| resource.resourceType.toLowerCase().includes(term);
            //|| pipe.transform(resource.id).includes(term); // ID search?
        });
    }

    refresh(): void{
        this.refreshSubject.next(null);
    }
}
