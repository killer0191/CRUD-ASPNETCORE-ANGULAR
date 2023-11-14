import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CompraService {
  private readonly apiUrl: string = environment.apiUrl;
  private apiC: string = this.apiUrl + 'Compra/';

  constructor(private http: HttpClient) {}

  obtenerTodo(): Observable<any> {
    return this.http.get(`${this.apiC}ObtenerTodo`);
  }

  obtener(id: number): Observable<any> {
    return this.http.get(`${this.apiC}Obtener/${id}`);
  }

  insertarCompra(compraData: any): Observable<any> {
    return this.http.post(`${this.apiC}Insertar`, compraData);
  }

  actualizarCompra(compraData: any): Observable<any> {
    return this.http.put(`${this.apiC}Actualizar`, compraData);
  }

  eliminarCompra(id: number): Observable<any> {
    return this.http.delete(`${this.apiC}Eliminar/${id}`);
  }

  obtenerPorFecha(fecha: string): Observable<any> {
    return this.http.get(`${this.apiC}ObtenerPorFecha/${fecha}`);
  }

  ordenarPorFecha(fecha: string): Observable<any> {
    return this.http.get(`${this.apiC}OrdenarPorFecha/${fecha}`);
  }
  obtenerPorIdCliente(idCliente: number): Observable<any> {
    return this.http.get(`${this.apiC}ObtenerPorIdCliente/${idCliente}`);
  }
  obtenerPorIdTienda(idTienda: number): Observable<any> {
    return this.http.get(`${this.apiC}ObtenerPorIdTienda/${idTienda}`);
  }
}
