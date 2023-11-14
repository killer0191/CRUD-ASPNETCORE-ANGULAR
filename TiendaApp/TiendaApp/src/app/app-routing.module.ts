import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HomeTiendaComponent } from './home-tienda/home-tienda.component';
import { CarritoComponent } from './carrito/carrito.component';
import { HistorialClienteComponent } from './historial-cliente/historial-cliente.component';
import { HistorialTiendaComponent } from './historial-tienda/historial-tienda.component';

const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'home/:idCliente', component: HomeComponent },
  { path: 'home-tienda/:idTienda', component: HomeTiendaComponent },
  { path: 'carrito/:idCliente', component: CarritoComponent },

  {
    path: 'historial-cliente/:idCliente',
    component: HistorialClienteComponent,
  },
  { path: 'historial-tienda/:idTienda', component: HistorialTiendaComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
