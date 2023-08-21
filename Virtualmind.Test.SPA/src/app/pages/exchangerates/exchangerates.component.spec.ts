import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExchangeratesComponent } from './exchangerates.component';

describe('ExchangeratesComponent', () => {
  let component: ExchangeratesComponent;
  let fixture: ComponentFixture<ExchangeratesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExchangeratesComponent]
    });
    fixture = TestBed.createComponent(ExchangeratesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
