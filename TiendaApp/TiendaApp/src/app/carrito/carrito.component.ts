import { Component, OnInit } from '@angular/core';
import { CarritoService } from '../Services/carrito.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ClienteService } from '../Services/cliente.service';
import { AuthService } from '../auth.service';
import { ArticuloService } from '../Services/articulo.service';
import { TiendaService } from '../Services/tienda.service';
import { Observable, catchError, forkJoin, map, of, tap } from 'rxjs';
import { Compra } from '../Interfaces/compra';
import { CompraService } from '../Services/compra.service';

@Component({
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css'],
})
export class CarritoComponent implements OnInit {
  nombreCliente: string = '';
  carritoItems: any[] = [];
  idCliente: string | null = null;

  constructor(
    private carritoService: CarritoService,
    private router: Router,
    private route: ActivatedRoute,
    private clienteService: ClienteService,
    private authService: AuthService,
    private articuloService: ArticuloService,
    private tiendaService: TiendaService,
    private compraService: CompraService
  ) {}

  ngOnInit() {
    this.idCliente = this.route.snapshot.paramMap.get('idCliente');
    this.obtenerDatosCliente(+this.idCliente!);
    this.carritoService.obtenerCarritoPorIdCliente(+this.idCliente!).subscribe(
      (items: any[]) => {
        this.carritoItems = items;

        this.obtenerInformacionDetalladaArticulos();
      },
      (error: any) => {
        console.error('Error al obtener el carrito', error);
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
    if (Array.isArray(this.carritoItems)) {
      const observables = this.carritoItems.map((carritoItem) => {
        if (carritoItem.idArticulo !== null) {
          const nombreObs = this.obtenerNombreArticulo(carritoItem.idArticulo);
          const imagenObs = this.obtenerImagenArticulo(carritoItem.idArticulo);
          const precioObs = this.obtenerPrecioArticulo(carritoItem.idArticulo);
          const sucursalObs = this.obtenerNombreTienda(carritoItem.idTienda);
          return forkJoin([nombreObs, imagenObs, sucursalObs, precioObs]).pipe(
            tap(([nombre, imagen, sucursal, precio]) => {
              carritoItem.articuloInfo = { nombre, imagen, sucursal, precio };
            })
          );
        } else {
          console.error('Error: carritoItem.idArticulo es null.');
          return of(null);
        }
      });

      forkJoin(observables).subscribe(() => {});
    } else {
      console.log('Tipo de carritoItems:', this.carritoItems);
      console.error('Error: this.carritoItems no es un array.');
    }
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

  obtenerPrecioArticulo(idArticulo: number): Observable<string> {
    return this.articuloService.obtener(idArticulo).pipe(
      map((articulo: any) =>
        articulo.precio !== null ? articulo.precio : 'Precio no disponible'
      ),
      catchError((error: any) => {
        console.error('Error al obtener el precio del artículo', error);
        return of('Precio no disponible');
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

  comprarArticulo(carritoItem: any) {
    const compraData: Compra = {
      fecha: new Date(),
      total: carritoItem.articuloInfo.precio,
      idArticulo: carritoItem.idArticulo,
      idCliente: carritoItem.idCliente,
      idTienda: carritoItem.idTienda,
    };

    this.compraService.insertarCompra(compraData).subscribe(
      (response: any) => {
        console.log('Compra exitosa');
        this.eliminarArticulo(carritoItem.idCarrito);
      },
      (error: any) => {
        console.error('Error al realizar la compra', error);
      }
    );
  }

  eliminarArticulo(idCarrito: number) {
    this.carritoService.eliminarCarrito(idCarrito).subscribe(
      () => {
        this.obtenerCarrito();
      },
      (error: any) => {
        console.error('Error al eliminar el artículo del carrito', error);
      }
    );
  }

  obtenerCarrito() {
    this.carritoService.obtenerCarritoPorIdCliente(+this.idCliente!).subscribe(
      (items: any[]) => {
        this.carritoItems = items;
        this.obtenerInformacionDetalladaArticulos();
      },
      (error: any) => {
        console.error('Error al obtener el carrito', error);
      }
    );
  }
  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
