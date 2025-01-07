import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XemtatcaComponent } from './xemtatca.component';

describe('XemtatcaComponent', () => {
  let component: XemtatcaComponent;
  let fixture: ComponentFixture<XemtatcaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ XemtatcaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(XemtatcaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
