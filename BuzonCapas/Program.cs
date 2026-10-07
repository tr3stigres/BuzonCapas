class Program
{
    public static void Main()
    {
        try
        {


            string ruta = Path.Combine(AppContext.BaseDirectory, "buzon.json");
            Console.WriteLine($"Archivo de datos: {ruta}");
            var repositorio = new JsonMessageRepository(ruta);
            var servicio = new BuzonService(repositorio);


            do
            {
                Console.WriteLine("\n\nMenu\n");
                Console.WriteLine("1.- Dar de ALTA un mensaje");
                Console.WriteLine("2.- LISTAR los mensajes");
                Console.WriteLine("0.- SALIR");
                Console.Write("\n\nEscoge opción (1,2,0) > ");
                string opcion = Console.ReadLine() ?? "0";
                if (opcion == "0")
                {
                    break;
                }
                else if (opcion == "1")
                {
                    /* Recoger texto, llamar a Alta y responder. */
                    Console.Write("Texto: ");
                    string texto = Console.ReadLine() ?? "";
                    bool guardado = servicio.Alta(texto);
                    Console.WriteLine(guardado ? "Mensaje guardado" : "Texto de 5 a 500 caracteres");
                }
                else if (opcion == "2") 
                {
                    /* Llamar a Listar y mostrar mensajes. */
                    var mensajes = servicio.Listar();
                    if (mensajes.Count == 0)
                    {
                        Console.WriteLine("Todavía no hay mensajes");
                    }
                    else
                    {
                        foreach (var m in mensajes)
                        {
                            Console.WriteLine($"{m.Id}: {m.Texto}");
                            Console.WriteLine(m.Analisis is null ? "Sin análisis aceptado" : $"Análisis: {m.Analisis.Resumen}");
                        }
                    }
                    
                }
                else Console.WriteLine("La opción ha de ser 1, 2 o 0");
            } while (true);
        }
        catch (Exception error)
        {
            Console.WriteLine($"Se ha producido el siguiente error:\n{error.Message}");
        }
    }
}

