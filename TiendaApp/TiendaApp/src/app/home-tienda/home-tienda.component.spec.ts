import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeTiendaComponent } from './home-tienda.component';

describe('HomeTiendaComponent', () => {
  let component: HomeTiendaComponent;
  let fixture: ComponentFixture<HomeTiendaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HomeTiendaComponent]
    });
    fixture = TestBed.createComponent(HomeTiendaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
