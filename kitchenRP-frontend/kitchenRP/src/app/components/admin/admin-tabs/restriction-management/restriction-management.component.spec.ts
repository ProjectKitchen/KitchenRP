import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RestrictionManagementComponent } from './restriction-management.component';

describe('RestrictionManagementComponent', () => {
  let component: RestrictionManagementComponent;
  let fixture: ComponentFixture<RestrictionManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RestrictionManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RestrictionManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
