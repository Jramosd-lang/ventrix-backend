namespace api_bentrix.Models
{
    public enum TipoDocumento
    {
        Cedula_de_ciudadania,
        Tarjeta_de_identidad,
        Cedula_de_extranjeria
    }

    public enum TipoPago
    {
        Efectivo,
        Tarjeta,
        Transferencia,
        Otros
    }

    public enum TipoDescuento
    {
        Porcentaje,
        Valor_fijo
    }

    public enum TipoReporte
    {
        Diario,
        Semanal,
        Mensual,
        Anual
    }
}
