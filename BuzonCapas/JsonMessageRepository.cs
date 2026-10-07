using System.Text.Json;

public class JsonMessageRepository : IMessageRepository
{
    private readonly string ruta;
    public JsonMessageRepository(string ruta) => this.ruta = ruta;
    
    public List<Mensaje> Leer()
    {
        string json;
        try { json = File.ReadAllText(ruta); }
        catch (FileNotFoundException) { return new(); }
        var datos = JsonSerializer.Deserialize<List<Mensaje>>(json);
        Validar(datos);
        return datos!;
    }

    public void Guardar(List<Mensaje> mensajes)
    {
        Validar(mensajes);
        string json = JsonSerializer.Serialize(mensajes);
        string temporal = ruta + ".tmp";
        File.WriteAllText(temporal, json);
        File.Move(temporal, ruta, overwrite: true);
    }

    private void Validar(List<Mensaje>? mensajes)
    {
        if (mensajes is null) throw new InvalidOperationException("Datos nulos");
        foreach (var m in mensajes)
        {
            if (m.Id <= 0) throw new InvalidOperationException($"Id inválido: {m.Id}");
            if (string.IsNullOrWhiteSpace(m.Texto)) throw new InvalidOperationException($"Texto inválido: {m.Texto}");
        }
        var duplicats = mensajes.GroupBy(m => m.Id).Where(m => m.Count() > 1).Select(m => m.Key).ToList();
        if (duplicats.Count > 0) throw new InvalidOperationException($"Se han encontrado estos ids duplicados: {string.Join(", ", duplicats)}");
    }
}