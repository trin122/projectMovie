import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HoathinhComponent } from './hoathinh.component';

describe('HoathinhComponent', () => {
  let component: HoathinhComponent;
  let fixture: ComponentFixture<HoathinhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HoathinhComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HoathinhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
