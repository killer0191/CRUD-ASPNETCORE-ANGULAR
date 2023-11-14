export interface Articulo {
  idArticulo?: number;
  nombre: string;
  descripcion: string;
  precio: number;
  imagen: Uint8Array;
  stock: number;
  idTienda: number;
}
