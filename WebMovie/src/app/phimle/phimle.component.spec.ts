import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhimleComponent } from './phimle.component';

describe('PhimleComponent', () => {
  let component: PhimleComponent;
  let fixture: ComponentFixture<PhimleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhimleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhimleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
