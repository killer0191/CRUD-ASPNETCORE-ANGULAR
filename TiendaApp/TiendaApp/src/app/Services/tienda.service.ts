import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tienda } from '../Interfaces/tienda';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TiendaService {
  private readonly apiUrl: string = environment.apiUrl;

  private apiC: string = this.apiUrl + 'Tiendum/';

  constructor(private http: HttpClient) {}

  obtenerTodo(): Observable<Tienda[]> {
    return this.http.get<Tienda[]>(`${this.apiC}ObtenerTodo`);
  }

  obtenerPorId(id: number): Observable<Tienda> {
    return this.http.get<Tienda>(`${this.apiC}Obtener/${id}`);
  }

  obtenerIdPorEmail(email: string): Observable<number> {
    return this.http.get<number>(
      `${this.apiC}ObtenerIdTiendaPorEmail/${email}`
    );
  }

  registrarTienda(tienda: Tienda): Observable<Tienda> {
    return this.http.post<Tienda>(`${this.apiC}Registrar`, tienda);
  }

  eliminarTienda(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiC}Eliminar/${id}`);
  }

  iniciarSesion(email: string, clave: string): Observable<any> {
    return this.http.get<any>(`${this.apiC}IniciarSesion/${email}/${clave}`);
  }
}
