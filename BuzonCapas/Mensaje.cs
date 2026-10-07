using static System.Net.Mime.MediaTypeNames;

public class Mensaje
{
    public int Id { get; set; }
    public string Texto { get; set; } = "";
    public AnalisisMensaje? Analisis { get; set; }
}