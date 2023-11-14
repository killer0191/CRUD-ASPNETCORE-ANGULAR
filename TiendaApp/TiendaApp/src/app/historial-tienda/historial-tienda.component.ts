import { Component, OnInit } from '@angular/core';
import { CompraService } from '../Services/compra.service';
import { Router, ActivatedRoute } from '@angular/router';
import { TiendaService } from '../Services/tienda.service';
import { AuthService } from '../auth.service';
import { ArticuloService } from '../Services/articulo.service';
import { Observable, catchError, forkJoin, map, of, tap } from 'rxjs';

@Component({
  selector: 'app-historial-tienda',
  templateUrl: './historial-tienda.component.html',
  styleUrls: ['./historial-tienda.component.css'],
})
export class HistorialTiendaComponent implements OnInit {
  nombreTienda: string = '';
  historialCompras: any[] = [];
  idTienda: string | null = null;

  constructor(
    private compraService: CompraService,
    private router: Router,
    private route: ActivatedRoute,
    private tiendaService: TiendaService,
    private authService: AuthService,
    private articuloService: ArticuloService
  ) {}

  ngOnInit() {
    this.idTienda = this.route.snapshot.paramMap.get('idTienda');
    this.obtenerDatosTienda(+this.idTienda!);
    this.compraService.obtenerPorIdTienda(+this.idTienda!).subscribe(
      (compras: any[]) => {
        this.historialCompras = compras;
        this.obtenerInformacionDetalladaArticulos();
      },
      (error: any) => {
        console.error('Error al obtener el historial de compras', error);
      }
    );
  }

  obtenerDatosTienda(idTienda: number) {
    this.tiendaService.obtenerPorId(idTienda).subscribe(
      (datosTienda) => {
        this.nombreTienda = datosTienda.sucursal;
      },
      (error) => {
        console.error('Error al obtener datos de la tienda', error);
      }
    );
  }

  obtenerInformacionDetalladaArticulos() {
    const observables = this.historialCompras.map((compra) => {
      const nombreObs = this.obtenerNombreArticulo(compra.idArticulo);
      const imagenObs = this.obtenerImagenArticulo(compra.idArticulo);

      return forkJoin([nombreObs, imagenObs]).pipe(
        tap(([nombre, imagen]) => {
          compra.articuloInfo = { nombre, imagen };
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

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
