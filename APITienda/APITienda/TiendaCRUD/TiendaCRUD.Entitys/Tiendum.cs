using System;
using System.Collections.Generic;

namespace TiendaCRUD.Entitys;

public partial class Tiendum
{
    public int IdTienda { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Direccion { get; set; }

    public string? Sucursal { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
