import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllReservationsStatusListComponent } from './all-reservations-status-list.component';

describe('AllReservationsStatusListComponent', () => {
  let component: AllReservationsStatusListComponent;
  let fixture: ComponentFixture<AllReservationsStatusListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllReservationsStatusListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllReservationsStatusListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
