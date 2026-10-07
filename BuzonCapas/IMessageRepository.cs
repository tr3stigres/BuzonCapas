public interface IMessageRepository
{
    List<Mensaje> Leer();
    void Guardar(List<Mensaje> mensajes);
}
