namespace APITienda.Models.ViewModels
{
    public class VMCompra
    {
        public int IdCompra { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal? Total { get; set; }

        public int? IdArticulo { get; set; }

        public int? IdCliente { get; set; }

        public int? IdTienda { get; set; }
    }
}
