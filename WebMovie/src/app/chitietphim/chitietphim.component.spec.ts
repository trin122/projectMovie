import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChitietphimComponent } from './chitietphim.component';

describe('ChitietphimComponent', () => {
  let component: ChitietphimComponent;
  let fixture: ComponentFixture<ChitietphimComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChitietphimComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChitietphimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
