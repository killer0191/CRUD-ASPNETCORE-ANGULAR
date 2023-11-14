import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Carrito } from '../Interfaces/carrito';
import { Compra } from '../Interfaces/compra';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CarritoService {
  private readonly apiUrl: string = environment.apiUrl;
  private apiC: string = this.apiUrl + 'Carrito/';

  constructor(private http: HttpClient) {}

  obtenerCarrito(): Observable<Carrito[]> {
    return this.http.get<Carrito[]>(`${this.apiC}ObtenerTodo`);
  }

  obtenerCarritoPorId(id: number): Observable<Carrito> {
    return this.http.get<Carrito>(`${this.apiC}Obtener/${id}`);
  }

  insertarCarrito(carritoData: any): Observable<any> {
    return this.http.post<any>(`${this.apiC}Insertar`, carritoData);
  }

  actualizarCarrito(carritoData: any): Observable<any> {
    return this.http.put<any>(`${this.apiC}Actualizar`, carritoData);
  }

  eliminarCarrito(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiC}Eliminar/${id}`);
  }

  obtenerCarritoPorIdCliente(idCliente: number): Observable<Carrito[]> {
    return this.http.get<Carrito[]>(
      `${this.apiC}ObtenerPorIdCliente/${idCliente}`
    );
  }

  comprar(idCarrito: number, compraData: Compra): Observable<any> {
    return this.http.post<any>(
      `${this.apiC}Comprar?idCarrito=${idCarrito}`,
      compraData
    );
  }

  ordenarCarritoPorFecha(fecha: string): Observable<Carrito[]> {
    return this.http.get<Carrito[]>(`${this.apiC}OrdenarPorFecha/${fecha}`);
  }
}
