using System;
using System.Collections.Generic;

namespace TiendaCRUD.Entitys;

public partial class Articulo
{
    public int IdArticulo { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public byte[]? Imagen { get; set; }

    public int? Stock { get; set; }

    public int? IdTienda { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Tiendum? IdTiendaNavigation { get; set; }
}
