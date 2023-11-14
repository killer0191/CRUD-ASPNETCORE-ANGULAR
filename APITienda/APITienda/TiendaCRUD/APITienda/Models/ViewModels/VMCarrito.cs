namespace APITienda.Models.ViewModels
{
    public class VMCarrito
    {
        public int IdCarrito { get; set; }

        public DateTime? Fecha { get; set; }

        public int? IdCliente { get; set; }

        public int? IdArticulo { get; set; }

        public int? IdTienda { get; set; }
    }
}
