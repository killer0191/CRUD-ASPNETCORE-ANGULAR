import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistorialTiendaComponent } from './historial-tienda.component';

describe('HistorialTiendaComponent', () => {
  let component: HistorialTiendaComponent;
  let fixture: ComponentFixture<HistorialTiendaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HistorialTiendaComponent]
    });
    fixture = TestBed.createComponent(HistorialTiendaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
