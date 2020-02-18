import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationStatusListComponent } from './reservation-status-list.component';

describe('ReservationStatusListComponent', () => {
  let component: ReservationStatusListComponent;
  let fixture: ComponentFixture<ReservationStatusListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReservationStatusListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationStatusListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
