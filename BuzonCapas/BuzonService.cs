public class BuzonService
{
    private readonly IMessageRepository repo;
    public BuzonService(IMessageRepository repo)
    {
        this.repo = repo;
    }
    

    public bool Alta(string texto)
    {
        texto = texto.Trim();
        if (texto.Length < 5 || texto.Length > 500) return false;
        var datos = repo.Leer();
        int siguienteId = datos.Count == 0 ? 1 : checked(datos.Max(m => m.Id) + 1);
        datos.Add(new Mensaje { Id = siguienteId, Texto = texto });
        repo.Guardar(datos);
        return true;
    }

    public List<Mensaje> Listar()
    {
        return repo.Leer();
    }
}