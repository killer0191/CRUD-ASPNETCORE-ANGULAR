import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { TiendaService } from '../Services/tienda.service';
import { ArticuloService } from '../Services/articulo.service';
import { CarritoService } from '../Services/carrito.service';
import { Router, ActivatedRoute } from '@angular/router';
import { CompraService } from '../Services/compra.service';

@Component({
  selector: 'app-home-tienda',
  templateUrl: './home-tienda.component.html',
  styleUrls: ['./home-tienda.component.css'],
})
export class HomeTiendaComponent implements OnInit {
  nombreTienda: string = '';
  listaArticulos: any[] = [];
  idTienda: string | null = null;
  mostrarFormulario: boolean = false;
  mostrarFormularioEdicion: boolean = false;
  nuevoArticulo: any = {};
  selectedFile: File | undefined;
  articuloSeleccionado: any = {};

  constructor(
    private authService: AuthService,
    private tiendaService: TiendaService,
    private articuloService: ArticuloService,
    private carritoService: CarritoService,
    private router: Router,
    private route: ActivatedRoute,
    private compraService: CompraService
  ) {
    const idTienda = this.route.snapshot.paramMap.get('idTienda');
    this.obtenerDatosTienda(+idTienda!);
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.idTienda = params['idTienda'];
    });

    this.actualizarListaArticulos();
  }

  obtenerDatosTienda(idTienda: number) {
    console.log('id de la tienda,', idTienda);
    this.tiendaService.obtenerPorId(idTienda).subscribe(
      (datosTienda) => {
        this.nombreTienda = datosTienda.sucursal;
      },
      (error) => {
        console.error('Error al obtener datos de la tienda', error);
      }
    );
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  abrirFormularioIngresoArticulo() {
    this.mostrarFormulario = true;
  }

  cerrarFormularioIngresoArticulo() {
    this.mostrarFormulario = false;
  }

  abrirFormularioEdiciónArticulo() {
    this.mostrarFormularioEdicion = true;
  }

  cerrarFormularioEdicionArticulo() {
    this.mostrarFormularioEdicion = false;
  }

  cargarImagen(event: any) {
    this.selectedFile = event.target.files[0];
  }

  ingresarArticulo() {
    this.nuevoArticulo.idTienda = +this.idTienda!;
    //this.nuevoArticulo.imagen = this.selectedFile;
    this.articuloService.insertar(this.nuevoArticulo).subscribe(
      (articuloInsertado) => {
        console.log('Articulo insertado correctamente: ', this.nuevoArticulo);
        this.actualizarListaArticulos();
        this.cerrarFormularioIngresoArticulo();
      },
      (error) => {
        console.error('Error al insertar el artículo', error);
      }
    );
  }

  actualizarListaArticulos() {
    this.articuloService.obtenerPorIdTienda(+this.idTienda!).subscribe(
      (articulos: any[]) => {
        this.listaArticulos = articulos;
      },
      (error: any) => {
        console.error('Error al obtener la lista de artículos', error);
      }
    );
  }
  actualizarArticulo() {
    this.articuloService.actualizar(this.articuloSeleccionado).subscribe(
      () => {
        console.log(
          `Artículo con ID ${this.articuloSeleccionado.idArticulo} actualizado correctamente`
        );
        this.actualizarListaArticulos();
        this.cerrarFormularioIngresoArticulo();
        this.mostrarFormularioEdicion = false;
      },
      (error) => {
        console.error('Error al actualizar el artículo', error);
      }
    );
  }

  editarArticulo(idArticulo: number) {
    const articulo = this.listaArticulos.find(
      (a) => a.idArticulo === idArticulo
    );

    if (articulo) {
      this.articuloSeleccionado = { ...articulo };
      this.mostrarFormularioEdicion = true;
    }
  }

  eliminarArticulo(idArticulo: number) {
    const confirmacion = confirm(
      '¿Estás seguro de que deseas eliminar este artículo?'
    );

    if (confirmacion) {
      this.articuloService.eliminar(idArticulo).subscribe(
        () => {
          console.log(`Artículo con ID ${idArticulo} eliminado correctamente`);
          this.actualizarListaArticulos();
        },
        (error) => {
          console.error('Error al eliminar el artículo', error);
        }
      );
    }
  }
}
