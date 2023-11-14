import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { ClienteService } from '../Services/cliente.service';
import { ArticuloService } from '../Services/articulo.service';
import { CarritoService } from '../Services/carrito.service';
import { Router, ActivatedRoute } from '@angular/router';
import { CompraService } from '../Services/compra.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  nombreCliente: string = '';
  listaArticulos: any[] = [];
  idCliente: string | null = null;

  constructor(
    private authService: AuthService,
    private clienteService: ClienteService,
    private articuloService: ArticuloService,
    private carritoService: CarritoService,
    private router: Router,
    private route: ActivatedRoute,
    private compraService: CompraService
  ) {
    const idCliente = this.route.snapshot.paramMap.get('idCliente');
    this.obtenerDatosCliente(+idCliente!);
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.idCliente = params['idCliente'];
    });
    this.articuloService.obtenerTodo().subscribe(
      (articulos) => {
        this.listaArticulos = articulos;
      },
      (error) => {
        console.error('Error al obtener la lista de artículos', error);
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

    this.articuloService.obtenerTodo().subscribe(
      (articulos) => {
        this.listaArticulos = articulos;
      },
      (error) => {
        console.error('Error al obtener la lista de artículos', error);
      }
    );
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  comprarArticulo(articulo: any) {
    const compraData = {
      fecha: new Date(),
      total: articulo.precio,
      idArticulo: articulo.idArticulo,
      idCliente: +this.route.snapshot.paramMap.get('idCliente')!,
      idTienda: articulo.idTienda,
    };

    this.compraService.insertarCompra(compraData).subscribe(
      (respuesta) => {
        console.log('Compra realizada:', respuesta);
      },
      (error) => {
        console.error('Error al realizar la compra', error);
      }
    );
  }

  agregarAlCarrito(articulo: any) {
    const carritoData = {
      fecha: new Date(),
      idCliente: +this.route.snapshot.paramMap.get('idCliente')!,
      idArticulo: articulo.idArticulo,
      idTienda: articulo.idTienda,
    };

    this.carritoService.insertarCarrito(carritoData).subscribe(
      (respuesta) => {
        console.log('Artículo agregado al carrito:', respuesta);
      },
      (error) => {
        console.error('Error al agregar el artículo al carrito', error);
      }
    );
  }
  navegarHistorial() {
    const idCliente = this.route.snapshot.paramMap.get('idCliente');
    this.router.navigate(['/historial-cliente', idCliente]);
  }
}
