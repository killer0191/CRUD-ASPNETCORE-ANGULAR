namespace APITienda.Models.ViewModels
{
    public class VMArticulo
    {
        public int IdArticulo { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }

        public byte[]? Imagen { get; set; }

        public int? Stock { get; set; }

        public int? IdTienda { get; set; }
    }
}
