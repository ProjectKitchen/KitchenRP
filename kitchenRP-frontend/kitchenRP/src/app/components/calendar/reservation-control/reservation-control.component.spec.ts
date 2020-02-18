import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationControlComponent } from './reservation-control.component';

describe('ReservationControlComponent', () => {
  let component: ReservationControlComponent;
  let fixture: ComponentFixture<ReservationControlComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReservationControlComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
