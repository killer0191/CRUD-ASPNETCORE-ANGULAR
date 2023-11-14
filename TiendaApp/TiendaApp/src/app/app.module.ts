import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';

import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';
import { CarritoComponent } from './carrito/carrito.component';
import { HistorialClienteComponent } from './historial-cliente/historial-cliente.component';
import { HistorialTiendaComponent } from './historial-tienda/historial-tienda.component';
import { HomeTiendaComponent } from './home-tienda/home-tienda.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CarritoComponent,
    HistorialClienteComponent,
    HistorialTiendaComponent,
    HomeTiendaComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
