import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Articulo } from '../Interfaces/articulo';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ArticuloService {
  private readonly apiUrl: string = environment.apiUrl;

  private apiC: string = this.apiUrl + 'Articulo/';

  constructor(private http: HttpClient) {}

  obtenerTodo(): Observable<Articulo[]> {
    return this.http.get<Articulo[]>(`${this.apiC}ObtenerTodo`);
  }

  obtener(id: number): Observable<Articulo> {
    return this.http.get<Articulo>(`${this.apiC}Obtener/${id}`);
  }

  insertar(articulo: Articulo): Observable<Articulo> {
    return this.http.post<Articulo>(`${this.apiC}Insertar`, articulo);
  }

  actualizar(articulo: Articulo): Observable<Articulo> {
    return this.http.put<Articulo>(`${this.apiC}Actualizar`, articulo);
  }

  eliminar(id: number): Observable<any> {
    return this.http.delete(`${this.apiC}Eliminar/${id}`);
  }
  obtenerPorIdTienda(idTienda: number): Observable<any> {
    return this.http.get(`${this.apiC}ObtenerPorIdTienda/${idTienda}`);
  }
}
