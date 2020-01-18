import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalRestrictionComponent } from './modal-restriction.component';

describe('ModalRestrictionComponent', () => {
  let component: ModalRestrictionComponent;
  let fixture: ComponentFixture<ModalRestrictionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalRestrictionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalRestrictionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
