import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Login } from '../Interfaces/login';

@Injectable({
  providedIn: 'root',
})
export class ClienteService {
  private readonly apiUrl: string = environment.apiUrl;
  private apiC: string = this.apiUrl + 'Cliente';

  constructor(private http: HttpClient) {}

  obtenerDatosCliente(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiC}/Obtener/${id}`);
  }
}
