import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhimboComponent } from './phimbo.component';

describe('PhimboComponent', () => {
  let component: PhimboComponent;
  let fixture: ComponentFixture<PhimboComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhimboComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhimboComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
