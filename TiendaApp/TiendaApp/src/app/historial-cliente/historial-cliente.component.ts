import { Component, OnInit } from '@angular/core';
import { CompraService } from '../Services/compra.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ClienteService } from '../Services/cliente.service';
import { AuthService } from '../auth.service';
import { ArticuloService } from '../Services/articulo.service';
import { TiendaService } from '../Services/tienda.service';
import { Observable, catchError, forkJoin, map, of, tap } from 'rxjs';

@Component({
  selector: 'app-historial-cliente',
  templateUrl: './historial-cliente.component.html',
  styleUrls: ['./historial-cliente.component.css'],
})
export class HistorialClienteComponent implements OnInit {
  nombreCliente: string = '';
  historialCompras: any[] = [];
  idCliente: string | null = null;

  constructor(
    private compraService: CompraService,
    private router: Router,
    private route: ActivatedRoute,
    private clienteService: ClienteService,
    private authService: AuthService,
    private articuloService: ArticuloService,
    private tiendaService: TiendaService
  ) {}

  ngOnInit() {
    this.idCliente = this.route.snapshot.paramMap.get('idCliente');
    this.obtenerDatosCliente(+this.idCliente!);
    this.compraService.obtenerPorIdCliente(+this.idCliente!).subscribe(
      (compras: any[]) => {
        this.historialCompras = compras;

        this.obtenerInformacionDetalladaArticulos();
      },
      (error: any) => {
        console.error('Error al obtener el historial de compras', error);
      }
    );
  }
  obtenerDatosCliente(idCliente: number) {
    this.clienteService.obtenerDatosCliente(idCliente).subscribe(
      (datosCliente) => {
        this.nombreCliente = datosCliente.nombre;
      },
      (error) => {
        console.error('Error al obtener datos del cliente', error);
      }
    );
  }

  obtenerInformacionDetalladaArticulos() {
    const observables = this.historialCompras.map((compra) => {
      const nombreObs = this.obtenerNombreArticulo(compra.idArticulo);
      const imagenObs = this.obtenerImagenArticulo(compra.idArticulo);
      const sucursalObs = this.obtenerNombreTienda(compra.idTienda);
      return forkJoin([nombreObs, imagenObs, sucursalObs]).pipe(
        tap(([nombre, imagen, sucursal]) => {
          compra.articuloInfo = { nombre, imagen, sucursal };
        })
      );
    });

    forkJoin(observables).subscribe(() => {});
  }

  obtenerNombreArticulo(idArticulo: number): Observable<string> {
    return this.articuloService.obtener(idArticulo).pipe(
      map((articulo: any) => articulo.nombre),
      catchError((error: any) => {
        console.error('Error al obtener el nombre del artículo', error);
        return of('Nombre no disponible');
      })
    );
  }

  obtenerImagenArticulo(idArticulo: number): Observable<string> {
    return this.articuloService.obtener(idArticulo).pipe(
      map((articulo: any) => articulo.imagen),
      catchError((error: any) => {
        console.error('Error al obtener la imagen del artículo', error);
        return of('Imagen no disponible');
      })
    );
  }

  obtenerNombreTienda(idTienda: number): Observable<string> {
    return this.tiendaService.obtenerPorId(idTienda).pipe(
      map((tienda: any) => tienda.sucursal),
      catchError((error: any) => {
        console.error('Error al obtener el nombre de la tienda', error);
        return of('Nombre no disponible');
      })
    );
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
