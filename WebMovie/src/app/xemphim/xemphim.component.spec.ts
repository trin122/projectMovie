import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XemphimComponent } from './xemphim.component';

describe('XemphimComponent', () => {
  let component: XemphimComponent;
  let fixture: ComponentFixture<XemphimComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ XemphimComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(XemphimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
