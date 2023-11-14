using System;
using System.Collections.Generic;

namespace TiendaCRUD.Entitys;

public partial class Compra
{
    public int IdCompra { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public int? IdArticulo { get; set; }

    public int? IdCliente { get; set; }

    public int? IdTienda { get; set; }

    public virtual Articulo? IdArticuloNavigation { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Tiendum? IdTiendaNavigation { get; set; }
}
