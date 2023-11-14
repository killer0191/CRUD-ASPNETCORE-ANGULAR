import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, catchError, of } from 'rxjs';
import { Login } from '../Interfaces/login';
import { Tienda } from '../Interfaces/tienda';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private readonly apiUrl: string = environment.apiUrl;

  private apiC: string = this.apiUrl + 'Cliente';
  private apiT: string = this.apiUrl + 'Tiendum';

  constructor(private http: HttpClient) {}

  getList(): Observable<Login[]> {
    return this.http.get<Login[]>(`${this.apiC}/ObtenerTodo`);
  }

  getLogin(email: string, password: string): Observable<Login[]> {
    return this.http.get<Login[]>(
      `${this.apiC}/IniciarSesion/${email}/${password}`
    );
  }

  getIdClientePorEmail(email: string): Observable<number | null> {
    return this.http
      .get<number>(`${this.apiC}/ObtenerIdClientePorEmail/${email}`)
      .pipe(catchError(() => of(null)));
  }

  getListTienda(): Observable<Tienda[]> {
    return this.http.get<Tienda[]>(`${this.apiT}/ObtenerTodo`);
  }

  getLoginTienda(email: string, password: string): Observable<Tienda[]> {
    return this.http.get<Tienda[]>(
      `${this.apiT}/IniciarSesion/${email}/${password}`
    );
  }
  getIdTiendaPorEmail(email: string): Observable<number | null> {
    return this.http
      .get<number>(`${this.apiT}/ObtenerIdTiendaPorEmail/${email}`)
      .pipe(catchError(() => of(null)));
  }
  registrarCliente(clienteData: any): Observable<any> {
    return this.http.post<any>(`${this.apiC}/Registrar`, clienteData);
  }

  registrarTienda(tiendaData: any): Observable<any> {
    return this.http.post<any>(`${this.apiT}/Registrar`, tiendaData);
  }
}
