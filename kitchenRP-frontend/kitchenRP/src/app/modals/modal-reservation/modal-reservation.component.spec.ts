import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalReservationComponent } from './modal-reservation.component';

describe('ModalReservationComponent', () => {
  let component: ModalReservationComponent;
  let fixture: ComponentFixture<ModalReservationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalReservationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
