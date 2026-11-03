using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XelaTech
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Cliente> Clientes = new List<Cliente>();
            List<Tecnico> Tecnicos = new List<Tecnico>();
            List<ComputadoraPortatil> ComputadorasPortatiles = new List<ComputadoraPortatil>();
            List<ComputadoraDeEscritorio> ComputadorasDeEscritorio = new List<ComputadoraDeEscritorio>();
            List<Impresora> Impresoras = new List<Impresora>();
            List<Repuesto> Repuestos = new List<Repuesto>();
            List<Solicitud> Solicitudes = new List<Solicitud>();
            Queue<Solicitud> PrioridadNormal = new Queue<Solicitud>();
            Queue<Solicitud> PrioridadUrgente = new Queue<Solicitud>();
            Random generador = new Random();

            // Datos de prueba preestablecidos
            Clientes.Add(new Cliente(1, "Juan Perez", "12345678", "juanperez@example.com", "Estudiante"));
            Clientes.Add(new Cliente(2, "Maria Lopez", "87654321", "marialopez@example.com", "Institución"));
            Tecnicos.Add(new Tecnico(1, "Luis Ramirez", "Redes", "Disponible"));
            Tecnicos.Add(new Tecnico(2, "Ana Torres", "Soporte", "Disponible"));
            ComputadorasPortatiles.Add(new ComputadoraPortatil(1, "HP.", "Victus", "Con daños leves", "Cargador", "154145546", Clientes[0]));
            ComputadorasPortatiles.Add(new ComputadoraPortatil(2, "Dell", "Prisma", "Con daños leves", "Cargador", "15465666", Clientes[1]));
            ComputadorasDeEscritorio.Add(new ComputadoraDeEscritorio(1, "Lenovo", "ThinkCentre", "Con daños leves", "Cargador", "154145547", Clientes[0]));
            ComputadorasDeEscritorio.Add(new ComputadoraDeEscritorio(2, "Acer", "Aspire", "Con daños leves", "Cargador", "15465667", Clientes[1]));
            Impresoras.Add(new Impresora(1, "Canon", "Pixma", "Con daños leves", "Cargador", "154145548", Clientes[0]));
            Impresoras.Add(new Impresora(2, "Epson", "EcoTank", "Con daños leves", "Cargador", "15465668", Clientes[1]));
            Repuestos.Add(new Repuesto(1, "Cargador", "Computador portatil", 5, 1, 45, 65, 0));
            Repuestos.Add(new Repuesto(2, "Cargador HP", "Computador portatil", 10, 1, 70, 100, 0));







            int opcionMenu = 0;
            do
            {
                try
                {
                    Console.Clear();
                    opcionMenu = ValidarEntradasInt("============================================\r\n XELATECH SOPORTE\r\n============================================\r\n1. Gestión de clientes\r\n2. Gestión de técnicos\r\n3. Gestión de equipos\r\n4. Gestión de solicitudes\r\n5. Gestión de repuestos\r\n6. Atención de colas\r\n7. Ordenamientos\r\n8. Búsquedas\r\n9. Reportes\r\n10. Generar datos de prueba\r\n11. Salir\n\nSelecciona una opción: ", 1, 11, "Error: Opción invlida");
                    switch (opcionMenu)
                    {
                        case 1:
                            Console.Clear();
                            int opcionMenuClientes;
                            do
                            {
                                opcionMenuClientes = ValidarEntradasInt("============================================\r\n GESTIÓN DE CLIENTES\r\n============================================\r\n1. Agregar cliente\r\n2. Consultar cliente\r\n3. Actualizar información\r\n4. Regresar el menú principal\n\nSelecciona una opción: ", 1, 4, "Error: Opción invlida");
                                switch (opcionMenuClientes)
                                {
                                    case 1:
                                        Console.Clear();
                                        Console.WriteLine("Agregar cliente");
                                        Console.WriteLine();
                                        int codigoCliente = ObtenerSiguienteCodigoPersona(Clientes, 1);
                                        Console.Write("Ingrese nombre: ");
                                        string nombre = LeerTextoSolicitud("", 3);

                                        Console.Clear();
                                        string numeroTelefono;
                                        while (true)
                                        {
                                            Console.Write("Ingrese número de telefono: ");
                                            numeroTelefono = LeerEntrada();
                                            if (ValidarTelefono(numeroTelefono))
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                MostrarErrorAnimado("Error: el número de telefono debe contener solo número y una longitud de 8 caracteres");
                                            }
                                        }
                                        Console.Clear();
                                        string correoElectronico;
                                        while (true)
                                        {
                                            Console.Write("Ingrese dirección de correo electronico: ");
                                            correoElectronico = LeerEntrada();
                                            if (ValidarCorreo(correoElectronico))
                                            {
                                                break;
                                            }
                                        }
                                        Console.Clear();
                                        while (true)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Seleccione tipo de cliente: ");
                                            int tipoCliente = ValidarEntradasInt("Seleccione tipo cliente: \n\n1. Estudiantes\n2. Profesionales\n3. Pequeños negocios\n4. Instituciones\n\nSeleccione una opción: ", 1, 4, "Error: Opción invalida");
                                            switch (tipoCliente)
                                            {
                                                case 1:
                                                    while (true)
                                                    {
                                                        int confirmacionCli = ValidarEntradasInt($"Codigo: CLI-{codigoCliente}\nNombre: {nombre}\nNumero de telefono: {numeroTelefono}\nCorreo electronico: {correoElectronico}\nTipo de cliente: Estudiante\n\n¿Esta seguro que desea registrar CLI-{codigoCliente}?\n\n1. SI\n2. NO\n\nSeleccione una opción: ", 1, 2, "Error: opción no valida");
                                                        if (confirmacionCli == 1)
                                                        {
                                                            Clientes.Add(new Cliente(codigoCliente, nombre, numeroTelefono, correoElectronico, "Estudiante"));
                                                            Console.WriteLine("Cliente agregado con exito!");
                                                            Console.ReadKey();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            MostrarErrorAnimado("Operación cancelada");
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    while (true)
                                                    {
                                                        int confirmacionCli = ValidarEntradasInt($"Codigo: CLI-{codigoCliente}\nNombre: {nombre}\nNumero de telefono: {numeroTelefono}\nCorreo electronico: {correoElectronico}\nTipo de cliente: Profesional\n\n¿Esta seguro que desea registrar CLI-{codigoCliente}?\n\n1. SI\n2. NO\n\nSeleccione una opción: ", 1, 2, "Error: opción no valida");
                                                        if (confirmacionCli == 1)
                                                        {
                                                            Clientes.Add(new Cliente(codigoCliente, nombre, numeroTelefono, correoElectronico, "Profesional"));
                                                            Console.WriteLine("Cliente agregado con exito!");
                                                            Console.ReadKey();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            MostrarErrorAnimado("Operación cancelada");
                                                            break;
                                                        }

                                                    }
                                                    break;
                                                case 3:
                                                    while (true)
                                                    {
                                                        int confirmacionCli = ValidarEntradasInt($"Codigo: CLI-{codigoCliente}\nNombre: {nombre}\nNumero de telefono: {numeroTelefono}\nCorreo electronico: {correoElectronico}\nTipo de cliente: Pequeños negocios\n\n¿Esta seguro que desea registrar CLI-{codigoCliente}?\n\n1. SI\n2. NO\n\nSeleccione una opción: ", 1, 2, "Error: opción no valida");
                                                        if (confirmacionCli == 1)
                                                        {
                                                            Clientes.Add(new Cliente(codigoCliente, nombre, numeroTelefono, correoElectronico, "Pequeños negocios"));
                                                            Console.WriteLine("Cliente agregado con exito!");
                                                            Console.ReadKey();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            MostrarErrorAnimado("Operación cancelada");
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case 4:
                                                    while (true)
                                                    {
                                                        int confirmacionCli = ValidarEntradasInt($"Codigo: CLI-{codigoCliente}\nNombre: {nombre}\nNumero de telefono: {numeroTelefono}\nCorreo electronico: {correoElectronico}\nTipo de cliente: Institucion\n\n¿Esta seguro que desea registrar CLI-{codigoCliente}?\n\n1. SI\n2. NO\n\nSeleccione una opción: ", 1, 2, "Error: opción no valida");
                                                        if (confirmacionCli == 1)
                                                        {
                                                            Clientes.Add(new Cliente(codigoCliente, nombre, numeroTelefono, correoElectronico, "Institución"));
                                                            Console.WriteLine("Cliente agregado con exito!");
                                                            Console.ReadKey();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            MostrarErrorAnimado("Operación cancelada");
                                                            break;
                                                        }
                                                    }
                                                    break;
                                            }
                                            break;
                                        }
                                        break;
                                    case 2:
                                        Console.Clear();
                                        int opcionConsulta;
                                        do
                                        {
                                            opcionConsulta = ValidarEntradasInt("Consultar cliente\n\n1. Consultar por codigo\n2. Ver todos los clientes\n3. Volver atras\n\nSeleccione una opción: ", 1, 3, "Error: opción no valida");
                                            switch (opcionConsulta)
                                            {
                                                case 1:
                                                    Console.Clear();
                                                    int codigoBuscar = ValidarEntradasInt("Ingrese codigo de cliente a buscar: CLI-", 1, int.MaxValue, "Error: codigo no valido!");
                                                    int indiceCodigoCliente = -1;
                                                    for (int i = 0; i < Clientes.Count; i++)
                                                    {
                                                        if (Clientes[i].Codigo == codigoBuscar)
                                                        {
                                                            indiceCodigoCliente = i;
                                                            break;
                                                        }
                                                    }
                                                    if (indiceCodigoCliente == -1)
                                                    {
                                                        MostrarErrorAnimado("Cliente no encontrado");
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Cliente encontrado");
                                                        Console.WriteLine();
                                                        Clientes[indiceCodigoCliente].MostrarInformacion();
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                case 2:
                                                    Console.Clear();
                                                    if (Clientes.Count == 0)
                                                    {
                                                        MostrarErrorAnimado("No hay clientes registrados");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Lista de clientes:");
                                                        Console.WriteLine();
                                                        foreach (var cliente in Clientes)
                                                        {
                                                            cliente.MostrarInformacion();
                                                            Console.WriteLine();
                                                        }
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                case 3:
                                                    break;
                                            }
                                        } while (opcionConsulta != 3);
                                        break;
                                    case 3:
                                        Console.Clear();
                                        int codigoBuscarCase3 = ValidarEntradasInt("Ingrese codigo de cliente a buscar: CLI-", 1, int.MaxValue, "Error: codigo no valido!");
                                        int indiceCodigoClienteCase3 = -1;
                                        for (int i = 0; i < Clientes.Count; i++)
                                        {
                                            if (Clientes[i].Codigo == codigoBuscarCase3)
                                            {
                                                indiceCodigoClienteCase3 = i;
                                                break;
                                            }
                                        }
                                        if (indiceCodigoClienteCase3 == -1)
                                        {
                                            MostrarErrorAnimado("Cliente no encontrado");
                                            break;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Cliente encontrado");
                                            Console.WriteLine();
                                            Clientes[indiceCodigoClienteCase3].MostrarInformacion();
                                            Console.WriteLine();
                                            Console.WriteLine("Presione una tecla para continuar...");
                                            Console.ReadKey();
                                        }
                                        Console.WriteLine("Actualizar información");
                                        int opcionActualizar;
                                        do
                                        {
                                            opcionActualizar = ValidarEntradasInt("1. Actualizar nombre\n2. Actualizar número de teléfono\n3. Actualizar correo electrónico\n4. Actualizar tipo de cliente\n5. Regresar al menú anterior\n\nSeleccione una opción: ", 1, 5, "Error: opción no valida");
                                            switch (opcionActualizar)
                                            {
                                                case 1:
                                                    Console.Clear();
                                                    Console.WriteLine("Actualizar nombre");
                                                    Console.WriteLine();
                                                    Console.Write("Ingrese el nuevo nombre: ");
                                                    string nuevoNombre = LeerTextoSolicitud("", 3);
                                                    Console.Clear();
                                                    if (nuevoNombre == Clientes[indiceCodigoClienteCase3].NombreCompleto)
                                                    {
                                                        MostrarErrorAnimado("Error: No se puede registrar el mismo nombre");
                                                    }
                                                    else
                                                    {
                                                        Clientes[indiceCodigoClienteCase3].ActualizarNombre(nuevoNombre);
                                                        Console.WriteLine("Nombre actualizado correctamente.");
                                                        Console.ReadKey();
                                                    }

                                                    break;
                                                case 2:
                                                    Console.Clear();
                                                    string numeroTelefonoNuevo;
                                                    while (true)
                                                    {
                                                        Console.Write("Ingrese número de telefono: ");
                                                        numeroTelefonoNuevo = LeerEntrada();
                                                        if (ValidarTelefono(numeroTelefonoNuevo))
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            MostrarErrorAnimado("Error: el número de telefono debe contener solo número y una longitud de 8 caracteres");
                                                        }
                                                    }

                                                    if (numeroTelefonoNuevo == Clientes[indiceCodigoClienteCase3].NumeroTelefono)
                                                    {
                                                        Console.Clear();
                                                        MostrarErrorAnimado("Error: no se puede asignar el mismo numero!");
                                                    }
                                                    else
                                                    {
                                                        Clientes[indiceCodigoClienteCase3].ActualizarNumeroTelefono(numeroTelefonoNuevo);
                                                        Console.WriteLine("Número de teléfono actualizado correctamente.");
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                case 3:
                                                    Console.Clear();
                                                    string correoElectronicoNuevo;
                                                    while (true)
                                                    {
                                                        Console.Write("Ingrese dirección de correo electronico: ");
                                                        correoElectronicoNuevo = LeerEntrada();
                                                        if (ValidarCorreo(correoElectronicoNuevo))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    Console.Clear();
                                                    if (correoElectronicoNuevo == Clientes[indiceCodigoClienteCase3].CorreoElectronicio)
                                                    {
                                                        MostrarErrorAnimado("Error: no se puede asignar el mismo correo eletronico!");
                                                    }
                                                    else
                                                    {
                                                        Clientes[indiceCodigoClienteCase3].ActualizarCorreoElectronico(correoElectronicoNuevo);
                                                        Console.WriteLine("Dirección de correo electrónico actualizada correctamente.");
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                case 4:
                                                    Console.Clear();
                                                    Console.WriteLine("Actualizar tipo de cliente");

                                                    while (true)
                                                    {
                                                        int tipoClienteNuevo = ValidarEntradasInt("Actualizar tipo de cliente\n\nSeleccione tipo cliente: \n\n1. Estudiantes\n2. Profesionales\n3. Pequeños negocios\n4. Instituciones\n\nSeleccione una opción: ", 1, 4, "Error: Opción invalida");
                                                        switch (tipoClienteNuevo)
                                                        {
                                                            case 1:
                                                                if (Clientes[indiceCodigoClienteCase3].TipoCliente == "Estudiante")
                                                                {
                                                                    MostrarErrorAnimado("Error: no se puede asignar el mismo tipo nuevamente!");
                                                                }
                                                                else
                                                                {
                                                                    Clientes[indiceCodigoClienteCase3].ActualizarTipoCliente("Estudiante");
                                                                    Console.WriteLine("Tipo de cliente actualizado correctamente.");
                                                                    Console.ReadKey();
                                                                }
                                                                break;
                                                            case 2:
                                                                if (Clientes[indiceCodigoClienteCase3].TipoCliente == "Profesional")
                                                                {
                                                                    MostrarErrorAnimado("Error: no se puede asignar el mismo tipo nuevamente!");
                                                                }
                                                                else
                                                                {
                                                                    Clientes[indiceCodigoClienteCase3].ActualizarTipoCliente("Profesional");
                                                                    Console.WriteLine("Tipo de cliente actualizado correctamente.");
                                                                    Console.ReadKey();
                                                                }
                                                                break;
                                                            case 3:
                                                                if (Clientes[indiceCodigoClienteCase3].TipoCliente == "Pequeños negocios")
                                                                {
                                                                    MostrarErrorAnimado("Error: no se puede asignar el mismo tipo nuevamente!");
                                                                }
                                                                else
                                                                {
                                                                    Clientes[indiceCodigoClienteCase3].ActualizarTipoCliente("Pequeños negocios");
                                                                    Console.WriteLine("Tipo de cliente actualizado correctamente.");
                                                                }
                                                                Console.ReadKey();
                                                                break;
                                                            case 4:
                                                                if (Clientes[indiceCodigoClienteCase3].TipoCliente == "Institución")
                                                                {
                                                                    MostrarErrorAnimado("Error: no se puede asignar el mismo tipo nuevamente!");
                                                                }
                                                                else
                                                                {
                                                                    Clientes[indiceCodigoClienteCase3].ActualizarTipoCliente("Institución");
                                                                    Console.WriteLine("Tipo de cliente actualizado correctamente.");
                                                                    Console.ReadKey();
                                                                }
                                                                break;
                                                        }
                                                        break;
                                                    }
                                                    break;
                                                case 5:
                                                    Console.Clear();
                                                    break;
                                            }
                                        } while (opcionActualizar != 5);
                                        break;
                                    case 4:
                                        Console.Clear();
                                        break;
                                }
                            } while (opcionMenuClientes != 4);
                            break;
                        case 2:
                            Console.Clear();
                            int opcionMenuTecnicos;
                            do
                            {
                                opcionMenuTecnicos = ValidarEntradasInt("============================================\r\n GESTIÓN DE TÉCNICOS\r\n============================================\r\n1. Agregar técnico\r\n2. Consultar técnico\r\n3. Actualizar información\r\n4. Regresar el menú principal\n\nSelecciona una opción: ", 1, 4, "Error: Opción invlida");
                                switch (opcionMenuTecnicos)
                                {
                                    case 1:
                                        Console.ReadKey();
                                        int codigoTecnicos = ObtenerSiguienteCodigoPersona(Tecnicos, 1);
                                        Console.Write("Ingrese nombre: ");
                                        string nombreTecnico = LeerTextoSolicitud("", 3);
                                        Console.Clear();
                                        string especialidad;

                                        while (true)
                                        {
                                            int opcionEspecialidad = ValidarEntradasInt("Escoga tipo de especialidad: \n\n1. Soporte\n2. Redes\n3. Impresión\n\nSeleccione una opción: ", 1, 3, "Error: Opción no valida");
                                            if (opcionEspecialidad == 1)
                                            {
                                                especialidad = "Soporte";
                                                break;
                                            }
                                            else if (opcionEspecialidad == 2)
                                            {
                                                especialidad = "Redes";
                                                break;
                                            }
                                            else
                                            {
                                                especialidad = "Impresión";
                                                break;
                                            }
                                        }
                                        Console.Clear();
                                        string estadoDisponibilidad;
                                        while (true)
                                        {
                                            int opcionDisponibilidad = ValidarEntradasInt("Escoga disponibilidad: \n\n1. Disponible\n2. No disponible\n\nIngrese una opción: ", 1, 2, "Error: Opción no valida");
                                            if (opcionDisponibilidad == 1)
                                            {
                                                estadoDisponibilidad = "Disponible";
                                                break;
                                            }
                                            else
                                            {
                                                estadoDisponibilidad = "No Disponible";
                                                break;
                                            }

                                        }
                                        while (true)
                                        {
                                            int confirmacionTec = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: TEC-{codigoTecnicos}\nNombre: {nombreTecnico}\nEspecialidad: {especialidad}\nEstado disponibildad: {estadoDisponibilidad}\n\n¿Esta seguro de registrar TEC-{codigoTecnicos}?\n\n1. SI\n2. NO", 1, 2, "Error: Opción no valida");
                                            if (confirmacionTec == 1)
                                            {
                                                Console.Clear();
                                                Tecnicos.Add(new Tecnico(codigoTecnicos, nombreTecnico, especialidad, estadoDisponibilidad));
                                                Console.WriteLine("Tecnico registrado exitosamente!");
                                                Console.ReadKey();
                                                break;
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                MostrarErrorAnimado("Operación cancelada");
                                                break;
                                            }
                                        }
                                        Console.Clear();
                                        break;
                                    case 2:
                                        Console.Clear();
                                        int opcionCase2Tec;
                                        do
                                        {
                                            opcionCase2Tec = ValidarEntradasInt("1. Consultar tecnico por codigo\n2. Ver todos los tecnicos registrados\n3. Volver atras\n\nEliga una opción: ", 1, 3, "Error: opción no valida");
                                            switch (opcionCase2Tec)
                                            {
                                                case 1:
                                                    Console.Clear();
                                                    int codigoBuscarTec = ValidarEntradasInt("Ingrese codigo de tecnico a buscar: TEC-", 1, int.MaxValue, "Cliente no encontrado");
                                                    int indiceCodigoTec = -1;
                                                    for (int i = 0; i < Tecnicos.Count; i++)
                                                    {
                                                        if (Tecnicos[i].Codigo == codigoBuscarTec)
                                                        {
                                                            indiceCodigoTec = i;
                                                        }
                                                    }
                                                    if (indiceCodigoTec == -1)
                                                    {
                                                        Console.WriteLine("Técnico no encontrado");
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Tecnicos[indiceCodigoTec].MostrarInformacion();
                                                        Console.ReadKey();
                                                    }

                                                    break;
                                                case 2:
                                                    if (Tecnicos.Count == 0)
                                                    {
                                                        MostrarErrorAnimado("Sin tecnicos registrados");
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Tecnico regisrados");
                                                        Console.WriteLine();
                                                        foreach (Tecnico tecnico in Tecnicos)
                                                        {
                                                            tecnico.MostrarInformacion();
                                                            Console.WriteLine();
                                                        }
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                case 3:
                                                    break;
                                            }
                                        } while (opcionCase2Tec != 3);
                                        Console.Clear();
                                        break;
                                    case 3:
                                        Console.Clear();
                                        int codigoBuscarTecCase3 = ValidarEntradasInt("Ingrese codigo de tecnico a buscar: TEC-", 1, int.MaxValue, "Error: codigo no valido!");
                                        int indiceBuscarTecCase3 = -1;
                                        for (int i = 0; i < Tecnicos.Count; i++)
                                        {
                                            if (Tecnicos[i].Codigo == codigoBuscarTecCase3)
                                            {
                                                indiceBuscarTecCase3 = i;
                                                break;
                                            }
                                        }
                                        if (indiceBuscarTecCase3 == -1)
                                        {
                                            MostrarErrorAnimado("Técnico no encontrado");
                                            break;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Tecnico encontrado");
                                            Console.WriteLine();
                                            Tecnicos[indiceBuscarTecCase3].MostrarInformacion();
                                            Console.WriteLine();
                                            Console.WriteLine("Presione una tecla para continuar...");
                                            Console.ReadKey();
                                        }
                                        int opcionActualizarTec;
                                        do
                                        {
                                            opcionActualizarTec = ValidarEntradasInt("1. Actualizar nombre\n2. Actualizar especialidad\n3. Regresar al menú anterior\n4. Actualizar disponibilidad\n5. Actualizar calificación promedio\n\nSeleccione una opción: ", 1, 5, "Error: opción no valida");
                                            switch (opcionActualizarTec)
                                            {
                                                case 1:
                                                    Console.Clear();
                                                    Console.Write("Ingrese nuevo nombre");
                                                    string nuevoNombreTec = LeerTextoSolicitud("", 3);
                                                    Console.Clear();
                                                    if (nuevoNombreTec == Tecnicos[indiceBuscarTecCase3].NombreCompleto)
                                                    {
                                                        MostrarErrorAnimado("Error: No se puede asignar el mismo nombre");
                                                    }
                                                    else
                                                    {
                                                        Tecnicos[indiceBuscarTecCase3].ActualizarNombre(nuevoNombreTec);
                                                        Console.Clear();
                                                        Console.WriteLine("Nombre actualizado correctamente.");
                                                    }
                                                    break;
                                                case 2:
                                                    Console.Clear();
                                                    Console.Write("Ingrese nueva especialidad: ");
                                                    string nuevaEspecialidadTec = LeerTextoSolicitud("", 3);
                                                    Tecnicos[indiceBuscarTecCase3].ActualizarEspecialidad(nuevaEspecialidadTec);
                                                    Console.Clear();
                                                    Console.WriteLine("Especialidad actualizada correctamente.");
                                                    Console.ReadKey();
                                                    break;
                                                case 3:
                                                    break;
                                                case 4:
                                                    int disponibilidadNueva = ValidarEntradasInt("1. Disponible\n2. No Disponible\nSeleccione una opción: ", 1, 2, "Opción no válida.");
                                                    Tecnicos[indiceBuscarTecCase3].ActualizarEstadoDisponibilidad(disponibilidadNueva == 1 ? "Disponible" : "No Disponible");
                                                    Console.WriteLine("Disponibilidad actualizada.");
                                                    Console.ReadKey();
                                                    break;
                                                case 5:
                                                    double promedioNuevo = ValidarEntradasDouble("Ingrese calificación promedio (0 a 5): ", 0, 5, "Calificación no válida.");
                                                    Tecnicos[indiceBuscarTecCase3].CalificacionPromedio = promedioNuevo;
                                                    Console.WriteLine("Calificación promedio actualizada.");
                                                    Console.ReadKey();
                                                    break;
                                            }

                                        } while (opcionActualizarTec != 3);
                                        break;
                                    case 4:
                                        break;
                                }
                            } while (opcionMenuTecnicos != 4);
                            break;
                        case 3:
                            Console.Clear();
                            int codigoComputadoraPortatil;
                            int codigoComputadoraDeEscritorio;
                            int codigoImpresora;

                            int opcionMenuEquipos;
                            do
                            {
                                opcionMenuEquipos = ValidarEntradasInt("============================================\r\n GESTIÓN DE EQUIPOS\r\n============================================\r\n1. Agregar equipo\r\n2. Consultar equipo\r\n3. Actualizar información\r\n4. Regresar el menú principal\n\nSelecciona una opción: ", 1, 4, "Error: Opción invlida");
                                switch (opcionMenuEquipos)
                                {
                                    case 1:
                                        int tipoEquipoRegistrar;
                                        do
                                        {
                                            Console.Clear();
                                            tipoEquipoRegistrar = ValidarEntradasInt("Seleccione tipo de equipo a registrar: \n\n1. Computadora portatil\n2. Computadora de escritorio\n3. Impresora\n4. Volver atras\n\nSeleccione una opción: ", 1, 4, "Error: opción no valida");
                                            switch (tipoEquipoRegistrar)
                                            {
                                                case 1:
                                                    {
                                                        codigoComputadoraPortatil = ObtenerSiguienteCodigoEquipo(ComputadorasPortatiles, 1);
                                                        Cliente propietarioEquipo = SeleccionarPropietarioEquipo(Clientes);
                                                        if (propietarioEquipo == null) break;
                                                        Console.Clear();
                                                        Console.Write("Ingrese numero de serie: ");
                                                        string numeroSerie = LeerNumeroSerieUnico(ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras, null);
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese marca: ");
                                                        string marca = LeerTextoSolicitud("", 2);
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese modelo: ");
                                                        string modelo = LeerTextoSolicitud("", 3);
                                                        Console.Clear();
                                                        string estadoFisico;
                                                        while (true)
                                                        {
                                                            int opcionEstadoFisico = ValidarEntradasInt("Seleccione estado fisico: \n\n1. Excelente\n2. Con daños leves\n3. Daños severos\n\nIngrese una opción: ", 1, 3, "Error: opción no valida");
                                                            if (opcionEstadoFisico == 1)
                                                            {
                                                                estadoFisico = "Excelente";
                                                                break;
                                                            }
                                                            else if (opcionEstadoFisico == 2)
                                                            {
                                                                estadoFisico = "Con daños leves";
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                estadoFisico = "Daños severos";
                                                                break;
                                                            }
                                                        }
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese accesorios entregados: ");
                                                        string accesoriosEntregados = LeerTextoSolicitud("", 3);
                                                        Console.Clear();
                                                        while (true)
                                                        {
                                                            int resumenCportatil = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: {codigoComputadoraPortatil}\nNumero de serie: {numeroSerie}\nMarca: {marca}\nModelo: {modelo}\nEstado Fisico: {estadoFisico}\nAccesorios: {accesoriosEntregados}\nPropietario: CLI-{propietarioEquipo.Codigo} {propietarioEquipo.NombreCompleto}\n\n¿Esta seguro de registrar este equipo?\n1. SI\n2. NO\n\nIngrese una opción:", 1, 2, "Error: opción no valida");
                                                            if (resumenCportatil == 1)
                                                            {
                                                                Console.Clear();
                                                                ComputadorasPortatiles.Add(new ComputadoraPortatil(codigoComputadoraPortatil, marca, modelo, estadoFisico, accesoriosEntregados, numeroSerie, propietarioEquipo));
                                                                Console.WriteLine("Equipo registrado correctamente");
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                MostrarErrorAnimado("Operación cancelada");
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    {
                                                        codigoComputadoraDeEscritorio = ObtenerSiguienteCodigoEquipo(ComputadorasDeEscritorio, 1);
                                                        Cliente propietarioEquipo = SeleccionarPropietarioEquipo(Clientes);
                                                        if (propietarioEquipo == null) break;
                                                        Console.Clear();
                                                        Console.Write("Ingrese numero de serie: ");
                                                        string numeroSerie = LeerNumeroSerieUnico(ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras, null);
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese marca: ");
                                                        string marca = LeerTextoSolicitud("", 2);
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese modelo: ");
                                                        string modelo = LeerTextoSolicitud("", 3);
                                                        Console.Clear();
                                                        string estadoFisico;
                                                        while (true)
                                                        {
                                                            int opcionEstadoFisico = ValidarEntradasInt("Seleccione estado fisico: \n\n1. Excelente\n2. Con daños leves\n3. Daños severos\n\nIngrese una opción: ", 1, 3, "Error: opción no valida");
                                                            if (opcionEstadoFisico == 1)
                                                            {
                                                                estadoFisico = "Excelente";
                                                                break;
                                                            }
                                                            else if (opcionEstadoFisico == 2)
                                                            {
                                                                estadoFisico = "Con daños leves";
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                estadoFisico = "Daños severos";
                                                                break;
                                                            }
                                                        }
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese accesorios entregados: ");
                                                        string accesoriosEntregados = LeerTextoSolicitud("", 3);
                                                        Console.Clear();
                                                        while (true)
                                                        {
                                                            int resumenCEscritorio = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: {codigoComputadoraDeEscritorio}\nNumero de serie: {numeroSerie}\nMarca: {marca}\nModelo: {modelo}\nEstado Fisico: {estadoFisico}\nAccesorios: {accesoriosEntregados}\nPropietario: CLI-{propietarioEquipo.Codigo} {propietarioEquipo.NombreCompleto}\n\n¿Esta seguro de registrar este equipo?\n1. SI\n2. NO\n\nIngrese una opción:", 1, 2, "Error: opción no valida");
                                                            if (resumenCEscritorio == 1)
                                                            {
                                                                Console.Clear();
                                                                ComputadorasDeEscritorio.Add(new ComputadoraDeEscritorio(codigoComputadoraDeEscritorio, marca, modelo, estadoFisico, accesoriosEntregados, numeroSerie, propietarioEquipo));
                                                                Console.WriteLine("Equipo registrado correctamente");
                                                                Console.ReadKey();
                                                                break;

                                                            }
                                                            else
                                                            {
                                                                MostrarErrorAnimado("Operación cancelada");
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    {
                                                        codigoImpresora = ObtenerSiguienteCodigoEquipo(Impresoras, 1);
                                                        Cliente propietarioEquipo = SeleccionarPropietarioEquipo(Clientes);
                                                        if (propietarioEquipo == null) break;
                                                        Console.Clear();
                                                        Console.Write("Ingrese numero de serie: ");
                                                        string numeroSerie = LeerNumeroSerieUnico(ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras, null);
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese marca: ");
                                                        string marca = LeerTextoSolicitud("", 2);
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese modelo: ");
                                                        string modelo = LeerTextoSolicitud("", 3);
                                                        Console.Clear();
                                                        string estadoFisico;
                                                        while (true)
                                                        {
                                                            int opcionEstadoFisico = ValidarEntradasInt("Seleccione estado fisico: \n\n1. Excelente\n2. Con daños leves\n3. Daños severos\n\nIngrese una opción: ", 1, 3, "Error: opción no valida");
                                                            if (opcionEstadoFisico == 1)
                                                            {
                                                                estadoFisico = "Excelente";
                                                                break;
                                                            }
                                                            else if (opcionEstadoFisico == 2)
                                                            {
                                                                estadoFisico = "Con daños leves";
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                estadoFisico = "Daños severos";
                                                                break;
                                                            }
                                                        }
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese accesorios entregados: ");
                                                        string accesoriosEntregados = LeerTextoSolicitud("", 3);
                                                        Console.Clear();
                                                        while (true)
                                                        {
                                                            int resumenImpresora = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: {codigoImpresora}\nNumero de serie: {numeroSerie}\nMarca: {marca}\nModelo: {modelo}\nEstado Fisico: {estadoFisico}\nAccesorios: {accesoriosEntregados}\nPropietario: CLI-{propietarioEquipo.Codigo} {propietarioEquipo.NombreCompleto}\n\n¿Esta seguro de registrar este equipo?\n1. SI\n2. NO\n\nIngrese una opción:", 1, 2, "Error: opción no valida");
                                                            if (resumenImpresora == 1)
                                                            {
                                                                Console.Clear();
                                                                Impresoras.Add(new Impresora(codigoImpresora, marca, modelo, estadoFisico, accesoriosEntregados, numeroSerie, propietarioEquipo));
                                                                Console.WriteLine("Equipo registrado correctamente");
                                                                Console.ReadKey();
                                                                break;

                                                            }
                                                            else
                                                            {
                                                                MostrarErrorAnimado("Operación cancelada");
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 4:
                                                    break;
                                            }
                                        } while (tipoEquipoRegistrar != 4);
                                        break;
                                    case 2:
                                        int opcionActualizarEquipo;
                                        do
                                        {
                                            opcionActualizarEquipo = ValidarEntradasInt("Consultar equipo\n\n1. Computadoras portatiles\n2. Computadoras de escritorio\n3. Impresoras\n4. Volver atras\n\nSelecciones una opción: ", 1, 4, "Error: opción invalida");
                                            switch (opcionActualizarEquipo)
                                            {
                                                case 1:
                                                    while (true)
                                                    {
                                                        int opcionBusquedaPortatil = ValidarEntradasInt("1. Buscar por codigo\n2. Mostrar todos los equipos\n3. Volver atras\n\nIngrese una opción: ", 1, 3, "Error: opción no valida");
                                                        if (opcionBusquedaPortatil == 1)
                                                        {
                                                            int codigoPortatilBuscar = ValidarEntradasInt("Ingrese codigo de portatil a buscar: PORT-", 1, int.MaxValue, "Codigo no valido");
                                                            int indicePortatilBuscar = -1;
                                                            for (int i = 0; i < ComputadorasPortatiles.Count; i++)
                                                            {
                                                                if (ComputadorasPortatiles[i].Codigo == codigoPortatilBuscar)
                                                                {
                                                                    indicePortatilBuscar = i;
                                                                }
                                                            }
                                                            if (indicePortatilBuscar == -1)
                                                            {
                                                                Console.Clear();
                                                                MostrarErrorAnimado("Codigo no encontrado");
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Equipo encontrado");
                                                                Console.WriteLine();
                                                                ComputadorasPortatiles[indicePortatilBuscar].MostrarInformacion("PORT");
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                        }
                                                        else if (opcionBusquedaPortatil == 2)
                                                        {
                                                            if (ComputadorasPortatiles.Count != 0)
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Computadoras portatiles ingresadas: ");
                                                                foreach (ComputadoraPortatil portatil in ComputadorasPortatiles)
                                                                {
                                                                    portatil.MostrarInformacion("PORT");
                                                                    Console.WriteLine();
                                                                }
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                MostrarErrorAnimado("Aun no se han registrado computadoras portatiles!");
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    while (true)
                                                    {
                                                        int opcionBusquedaEscritorio = ValidarEntradasInt("1. Buscar por codigo\n2. Mostrar todos los equipos\n3. Volver atras\n\nIngrese una opción: ", 1, 3, "Error: opción no valida");
                                                        if (opcionBusquedaEscritorio == 1)
                                                        {
                                                            int codigoEscritorioBuscar = ValidarEntradasInt("Ingrese codigo de computadora de escritorio a buscar: ESC-", 1, int.MaxValue, "Codigo no valido");
                                                            int indiceEscritorioBuscar = -1;
                                                            for (int i = 0; i < ComputadorasDeEscritorio.Count; i++)
                                                            {
                                                                if (ComputadorasDeEscritorio[i].Codigo == codigoEscritorioBuscar)
                                                                {
                                                                    indiceEscritorioBuscar = i;
                                                                }
                                                            }
                                                            if (indiceEscritorioBuscar == -1)
                                                            {
                                                                Console.Clear();
                                                                MostrarErrorAnimado("Codigo no encontrado");
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Equipo encontrado");
                                                                Console.WriteLine();
                                                                ComputadorasDeEscritorio[indiceEscritorioBuscar].MostrarInformacion("DESK");
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                        }
                                                        else if (opcionBusquedaEscritorio == 2)
                                                        {
                                                            if (ComputadorasDeEscritorio.Count != 0)
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Computadoras de escritorio ingresadas: ");
                                                                foreach (ComputadoraDeEscritorio escritorio in ComputadorasDeEscritorio)
                                                                {
                                                                    escritorio.MostrarInformacion("DESK");
                                                                    Console.WriteLine();
                                                                }
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                MostrarErrorAnimado("Aun no se han registrado computadoras de escritorio!");
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    while (true)
                                                    {
                                                        int opcionBusquedaImpresora = ValidarEntradasInt("1. Buscar por codigo\n2. Mostrar todos los equipos\n3. Volver atras\n\nIngrese una opción: ", 1, 3, "Error: opción no valida");
                                                        if (opcionBusquedaImpresora == 1)
                                                        {
                                                            int codigoImpresoraBuscar = ValidarEntradasInt("Ingrese codigo de impresora a buscar: IMP-", 1, int.MaxValue, "Codigo no valido");
                                                            int indiceImpresoraBuscar = -1;
                                                            for (int i = 0; i < Impresoras.Count; i++)
                                                            {
                                                                if (Impresoras[i].Codigo == codigoImpresoraBuscar)
                                                                {
                                                                    indiceImpresoraBuscar = i;
                                                                }
                                                            }
                                                            if (indiceImpresoraBuscar == -1)
                                                            {
                                                                Console.Clear();
                                                                MostrarErrorAnimado("Codigo no encontrado");
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Equipo encontrado");
                                                                Console.WriteLine();
                                                                Impresoras[indiceImpresoraBuscar].MostrarInformacion("IMP");
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                        }
                                                        else if (opcionBusquedaImpresora == 2)
                                                        {
                                                            if (Impresoras.Count != 0)
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Impresoras registradas: ");
                                                                foreach (Impresora impresora in Impresoras)
                                                                {
                                                                    impresora.MostrarInformacion("IMP");
                                                                    Console.WriteLine();
                                                                }
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                MostrarErrorAnimado("Aun no se han registrado impresoras");
                                                                Console.ReadKey();
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case 4:
                                                    break;
                                            }
                                        } while (opcionActualizarEquipo != 4);

                                        break;
                                    case 3:
                                        int opcionMenuActualizarEquipo;
                                        do
                                        {
                                            opcionMenuActualizarEquipo = ValidarEntradasInt("Tipo de equipo: \n\n1. Computadora portatil\n2. Computadora de escritorio\n3. Impresora\n4. Volver atras\n\nSeleccione una opción: ", 1, 4, "Error: opción no valida");
                                            switch (opcionMenuActualizarEquipo)
                                            {
                                                case 1:
                                                    Console.Clear();
                                                    ActualizarInformacionEquipo(ComputadorasPortatiles, "computadora portatil", "PORT", ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras);
                                                    break;
                                                case 2:
                                                    Console.Clear();
                                                    ActualizarInformacionEquipo(ComputadorasDeEscritorio, "computadora de escritorio", "DESK", ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras);
                                                    break;
                                                case 3:
                                                    Console.Clear();
                                                    ActualizarInformacionEquipo(Impresoras, "impresora", "IMP", ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras);
                                                    break;
                                                case 4:
                                                    Console.Clear();
                                                    break;
                                            }
                                        } while (opcionMenuActualizarEquipo != 4);

                                        break;
                                    case 4:
                                        break;
                                }
                            } while (opcionMenuEquipos != 4);

                            break;
                        case 4:
                            int SolicitudesSubMenu;
                            do
                            {
                                SolicitudesSubMenu = ValidarEntradasInt("============================================\n GESTIÓN DE SOLICITUDES\n============================================\n1. Crear nueva solicitud\n2. Consultar solicitud\n3. Registrar diagnóstico y acciones\n4. Actualizar estado\n5. Consultar historial\n6. Deshacer último cambio de estado\n7. Registrar entrega y calcular total\n8. Volver al menú principal\n9. Atender siguiente solicitud\n10. Registrar repuestos y costos\n11. Cancelar solicitud\n\nSeleccione una opción: ", 1, 11, "Opción no válida.");
                                try
                                {
                                    switch (SolicitudesSubMenu)
                                    {
                                        case 1:
                                            CrearNuevaSolicitud(Clientes, ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras, Tecnicos, Solicitudes, PrioridadNormal, PrioridadUrgente);
                                            break;
                                        case 2:
                                            ConsultarSolicitudes(Solicitudes);
                                            break;
                                        case 3:
                                            RegistrarDiagnosticoSolicitud(Solicitudes);
                                            break;
                                        case 4:
                                            CambiarEstadoSolicitud(Solicitudes);
                                            break;
                                        case 5:
                                            {
                                                Solicitud solicitud = SeleccionarSolicitud(Solicitudes);
                                                if (solicitud != null)
                                                {
                                                    solicitud.MostrarHistorial();
                                                    Console.ReadKey();
                                                }
                                                break;
                                            }
                                        case 6:
                                            {
                                                Solicitud solicitud = SeleccionarSolicitud(Solicitudes);
                                                if (solicitud != null)
                                                {
                                                    int confirmar = ValidarEntradasInt("¿Deshacer el último cambio de SOL-" + solicitud.Codigo + "?\n1. Sí\n2. No\nSeleccione una opción: ", 1, 2, "Opción no válida.");
                                                    if (confirmar == 1)
                                                    {
                                                        solicitud.DeshacerUltimoEstado();
                                                        Console.WriteLine("Estado restaurado: " + solicitud.Estado);
                                                        Console.ReadKey();
                                                    }
                                                }
                                                break;
                                            }
                                        case 7:
                                            {
                                                Solicitud solicitud = SeleccionarSolicitud(Solicitudes);
                                                if (solicitud != null)
                                                {
                                                    int confirmar = ValidarEntradasInt("Total: " + solicitud.Total + "\n¿Registrar entrega de SOL-" + solicitud.Codigo + "?\n1. Sí\n2. No\nSeleccione una opción: ", 1, 2, "Opción no válida.");
                                                    if (confirmar == 1)
                                                    {
                                                        solicitud.RegistrarEntrega();
                                                        Console.WriteLine("Equipo entregado. Total: " + solicitud.Total);
                                                        Console.ReadKey();
                                                    }
                                                }
                                                break;
                                            }
                                        case 8:
                                            Console.Clear();
                                            Console.WriteLine("Volviendo al menú principal...");
                                            break;
                                        case 9:
                                            AtenderSiguienteSolicitud(Solicitudes, Tecnicos, PrioridadNormal, PrioridadUrgente);
                                            break;
                                        case 10:
                                            RegistrarCostosSolicitud(Solicitudes, Repuestos);
                                            break;
                                        case 11:
                                            {
                                                Solicitud solicitud = SeleccionarSolicitud(Solicitudes);
                                                if (solicitud != null)
                                                {
                                                    int confirmar = ValidarEntradasInt("¿Cancelar SOL-" + solicitud.Codigo + " y devolver sus repuestos?\n1. Sí\n2. No\nSeleccione una opción: ", 1, 2, "Opción no válida.");
                                                    if (confirmar == 1)
                                                    {
                                                        solicitud.Cancelar();
                                                        Console.WriteLine("Solicitud cancelada. Repuestos devueltos y técnico liberado.");
                                                        Console.ReadKey();
                                                    }
                                                }
                                                break;
                                            }
                                    }
                                }
                                catch (ArgumentException error)
                                {
                                    MostrarErrorAnimado(error.Message);
                                    Console.ReadKey();
                                }
                                SincronizarColasSolicitudes(Solicitudes, PrioridadNormal, PrioridadUrgente);
                            } while (SolicitudesSubMenu != 8);
                            break;
                        case 5:
                            Console.Clear();
                            int opcionMenuRepuestos;
                            int codigoRepuesto;

                            do
                            {
                                Console.Clear();
                                opcionMenuRepuestos = ValidarEntradasInt("Menu de repuestos\n\n1. Registrar repuesto\n2. Consultar repuesto\n3. Actualizar información de repuesto\n4. Actualizar cantidad\n5. Volver atras\n\nIngrese una opción: ", 1, 5, "Error: opción no valida");
                                switch (opcionMenuRepuestos)
                                {
                                    case 1:
                                        Console.Clear();

                                        codigoRepuesto = ObtenerSiguienteCodigoRepuesto(Repuestos, 1);
                                        Console.Write("Ingrese nombre del repuesto: ");
                                        string nombreRepuesto = LeerTextoSolicitud("", 3);
                                        Console.Clear();
                                        Console.Write("Ingrese categoría de repuesto: ");
                                        string categoriaRepuesto = LeerTextoSolicitud("", 3);
                                        Console.Clear();
                                        int existenciaRepuesto = ValidarEntradasInt("Ingrese cantidad de repuestos: ", 0, int.MaxValue, "Error: cantidad no valida");
                                        Console.Clear();
                                        double costoUnitario = ValidarEntradasDouble("Ingrese costo unitario del repuesto: ", 0, double.MaxValue, "Error: costo no valido");
                                        Console.Clear();
                                        double precioVenta = ValidarEntradasDouble("Ingrese precio de venta del repuesto: ", 0, double.MaxValue, "Error: precio no valido");
                                        while (true)
                                        {
                                            int resumenRepuesto = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: REP-{codigoRepuesto}\nNombre: {nombreRepuesto}\nCategoria: {categoriaRepuesto}\nExistencia: {existenciaRepuesto}\nCosto unitario: {costoUnitario}\nPrecio de venta: {precioVenta}\n\n¿Esta seguro de registrar este repuesto?\n1. SI\n2. NO\n\nIngrese una opción:", 1, 2, "Error: opción no valida");
                                            if (resumenRepuesto == 1)
                                            {
                                                Console.Clear();
                                                Repuestos.Add(new Repuesto(codigoRepuesto, nombreRepuesto, categoriaRepuesto, existenciaRepuesto, 1, costoUnitario, precioVenta, 0));
                                                Console.WriteLine("Repuesto registrado correctamente");
                                                MostrarAvisoExistencia(Repuestos[Repuestos.Count - 1]);
                                                Console.ReadKey();
                                                break;
                                            }
                                            else
                                            {
                                                MostrarErrorAnimado("Operación cancelada");
                                                break;
                                            }
                                        }

                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        Console.Clear();
                                        int opcionConsultaRepuesto;
                                        do
                                        {
                                            opcionConsultaRepuesto = ValidarEntradasInt("Consultar repuesto\n\n1. Buscar por codigo\n2. Mostrar todos los repuestos\n3. Volver atras\n\nIngrese una opción: ", 1, 3, "Error: opción no valida");
                                            switch (opcionConsultaRepuesto)
                                            {
                                                case 1:
                                                    int codigoBuscarRepuesto = ValidarEntradasInt("Ingrese codigo de repuesto a buscar: REP-", 1, int.MaxValue, "Error: Codigo no valido!");
                                                    int indiceBuscarRepuesto = -1;
                                                    for (int i = 0; i < Repuestos.Count; i++)
                                                    {
                                                        if (Repuestos[i].Codigo == codigoBuscarRepuesto)
                                                        {
                                                            indiceBuscarRepuesto = i;
                                                        }
                                                    }
                                                    if (indiceBuscarRepuesto == -1)
                                                    {
                                                        Console.Clear();
                                                        MostrarErrorAnimado("Repuesto no encontrado");
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Repuesto encontrado: ");
                                                        Console.WriteLine();
                                                        Repuestos[indiceBuscarRepuesto].MostrarInformacion();
                                                        Console.WriteLine();
                                                        Console.WriteLine("Presione una tecla para volver...");
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                case 2:
                                                    Console.Clear();
                                                    if (Repuestos.Count == 0)
                                                    {
                                                        MostrarErrorAnimado("Sin repuestos registrados!");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Repuestos registrados: ");
                                                        Console.WriteLine();
                                                        foreach (Repuesto repuesto in Repuestos)
                                                        {
                                                            repuesto.MostrarInformacion();
                                                            Console.WriteLine();
                                                        }
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                case 3:
                                                    break;
                                            }
                                        } while (opcionConsultaRepuesto != 3);

                                        break;
                                    case 3:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Actualizar repuesto");
                                            int codigoActualizarRepuesto = ValidarEntradasInt("Ingrese codigo de repuesto: ", 1, int.MaxValue, "Error: codigo no encontrado!");
                                            int indiceActualizarRepuesto = -1;
                                            for (int i = 0; i < Repuestos.Count; i++)
                                            {
                                                if (Repuestos[i].Codigo == codigoActualizarRepuesto)
                                                {
                                                    indiceActualizarRepuesto = i;
                                                }
                                            }
                                            if (indiceActualizarRepuesto == -1)
                                            {
                                                MostrarErrorAnimado("Repuesto no encontrado");
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Repuesto encontrado: ");
                                                Console.WriteLine();
                                                Repuestos[indiceActualizarRepuesto].MostrarInformacion();
                                                Console.WriteLine();
                                                Console.WriteLine("Presione una tecla para volver....");
                                                Console.ReadKey();

                                                int opcionActualizarInforRepuesto = ValidarEntradasInt("Tipo de información a actualizar: \n\n1. Nombre\n2. Categoria\n3. Existencia minima\n4. Costo unitario\n5. Precio venta\n6. Volver atras\n\nSeleccione una opción: ", 1, 6, "Error: opción no valida");
                                                switch (opcionActualizarInforRepuesto)
                                                {
                                                    case 1:
                                                        Console.Clear();
                                                        Console.Write("Ingrese nuevo nombre: ");
                                                        Repuestos[indiceActualizarRepuesto].ActualizarNombre(LeerTextoSolicitud("", 3));
                                                        Console.WriteLine();
                                                        Console.Clear();
                                                        Console.ReadKey();
                                                        Console.WriteLine("Nuevo nombre de repuesto actualizado cone exito!");
                                                        break;
                                                    case 2:
                                                        Console.Clear();
                                                        Console.WriteLine("Ingrese nueva categoria: ");
                                                        Repuestos[indiceActualizarRepuesto].ActualizarCategoria(LeerTextoSolicitud("", 3));
                                                        Console.Clear();
                                                        Console.WriteLine("Nueva categoria de repuesto actualizada!");
                                                        Console.ReadKey();
                                                        break;
                                                    case 3:
                                                        Console.Clear();
                                                        int nuevaExistenciaMinima = ValidarEntradasInt("Ingrese nueva existencia minima: ", 0, int.MaxValue, "Error: nueva cantidad minima no permitida no permitido");
                                                        Repuestos[indiceActualizarRepuesto].ActualizarExitenciaMinima(nuevaExistenciaMinima);
                                                        Console.Clear();
                                                        Console.WriteLine("Nuevo limite de existencia actualizada con exito!");
                                                        MostrarAvisoExistencia(Repuestos[indiceActualizarRepuesto]);
                                                        Console.ReadKey();
                                                        break;
                                                    case 4:
                                                        Console.Clear();
                                                        double nuevoCostoUnitario = ValidarEntradasDouble("Ingrese nuevo costo unitario: ", 0, double.MaxValue, "Error: nuevo costo unitario invalido");
                                                        Repuestos[indiceActualizarRepuesto].ActualizarCostoUnitario(nuevoCostoUnitario);
                                                        Console.Clear();
                                                        Console.WriteLine("Nuevo costo unitario actaulizado con exito!");
                                                        Console.Clear();
                                                        Console.ReadKey();
                                                        break;
                                                    case 5:
                                                        Console.Clear();
                                                        double nuevoPrecioVenta = ValidarEntradasDouble("Ingrese nuevo precio de venta: ", 0, double.MaxValue, "Error: nuevo precio de venta actualizado");
                                                        Repuestos[indiceActualizarRepuesto].ActualizarPrecioVenta(nuevoPrecioVenta);
                                                        Console.Clear();
                                                        Console.WriteLine("Nuevo precio de venta actualizada con exito!");
                                                        Console.ReadKey();
                                                        break;
                                                    case 6:
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case 4:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Actualizar repuesto");
                                            int codigoActualizarRepuesto = ValidarEntradasInt("Ingrese codigo de repuesto: ", 1, int.MaxValue, "Error: codigo no encontrado!");
                                            int indiceActualizarRepuesto = -1;
                                            for (int i = 0; i < Repuestos.Count; i++)
                                            {
                                                if (Repuestos[i].Codigo == codigoActualizarRepuesto)
                                                {
                                                    indiceActualizarRepuesto = i;
                                                }
                                            }
                                            if (indiceActualizarRepuesto == -1)
                                            {
                                                MostrarErrorAnimado("Repuesto no encontrado");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Repuesto encontrado: ");
                                                Console.WriteLine($"Stock actual: {Repuestos[indiceActualizarRepuesto].Existencia}");
                                                Console.ReadKey();
                                                Console.WriteLine();
                                                int nuevoIngreso = ValidarEntradasInt("Ingrese nueva existencia: ", 1, 500, "Error: cantidad no permitida");
                                                Console.Clear();
                                                Repuestos[indiceActualizarRepuesto].ActualizarExistencia(nuevoIngreso);
                                                Console.WriteLine("Existencia actualizada con exito!");
                                                Console.ReadKey();
                                            }
                                        }
                                        break;
                                    case 5:
                                        break;

                                }
                            } while (opcionMenuRepuestos != 5);
                            break;
                        case 6:
                            GestionarColasSolicitudes(Solicitudes, Tecnicos, PrioridadNormal, PrioridadUrgente);
                            break;
                        case 7:
                            GestionarOrdenamientos(Solicitudes, Tecnicos, Repuestos);
                            break;
                        case 8:
                            GestionarBusquedas(Clientes, Solicitudes, ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras);
                            break;
                        case 9:
                            GestionarReportes(Clientes, Tecnicos, ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras, Repuestos, Solicitudes);
                            break;
                        case 10:
                            GestionarDatosPrueba(Clientes, Tecnicos, ComputadorasPortatiles, ComputadorasDeEscritorio, Impresoras, Repuestos, Solicitudes, PrioridadNormal, PrioridadUrgente, generador);
                            break;
                        case 11:
                            Console.Clear();

                            Console.ReadKey();
                            break;
                        default:
                            break;
                    }


                }
                catch (ArgumentException error)
                {
                    MostrarErrorAnimado(error.Message);
                    Console.WriteLine("Presione una tecla para volver al menú principal.");
                    Console.ReadKey();
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Entrada finalizada. Cerrando la aplicación.");
                    opcionMenu = 11;
                }
            } while (opcionMenu != 11);

        }
        static void GestionarReportes(List<Cliente> clientes, List<Tecnico> tecnicos, List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras, List<Repuesto> repuestos, List<Solicitud> solicitudes)
        {
            int opcionReporte;
            do
            {
                opcionReporte = ValidarEntradasInt("============================================\n REPORTES\n============================================\n1. Solicitudes en espera por prioridad\n2. Solicitudes activas, finalizadas y canceladas\n3. Técnicos disponibles y con carga completa\n4. Técnicos con más servicios finalizados\n5. Equipos atendidos por tipo\n6. Solicitudes ordenadas por costo total\n7. Solicitudes ordenadas por fecha\n8. Repuestos ordenados por existencia\n9. Repuestos mínimos o agotados\n10. Ingresos por servicios finalizados\n11. Resultados de búsquedas\n12. Matriz semanal de carga\n13. Regresar al menú principal\n\nSeleccione una opción: ", 1, 13, "Opción no válida.");
                try
                {
                    Console.Clear();
                    switch (opcionReporte)
                    {
                        case 1:
                            {
                                Queue<Solicitud> normales = new Queue<Solicitud>();
                                Queue<Solicitud> urgentes = new Queue<Solicitud>();
                                SincronizarColasSolicitudes(solicitudes, normales, urgentes);
                                MostrarColasSolicitudes(normales, urgentes);
                                break;
                            }
                        case 2:
                            MostrarResumenSolicitudes("ACTIVAS", FiltrarSolicitudesPorSituacion(solicitudes, 1));
                            MostrarResumenSolicitudes("FINALIZADAS - ENTREGADAS", FiltrarSolicitudesPorSituacion(solicitudes, 2));
                            MostrarResumenSolicitudes("CANCELADAS", FiltrarSolicitudesPorSituacion(solicitudes, 3));
                            break;
                        case 3:
                            MostrarGrupoTecnicos("DISPONIBLES", FiltrarTecnicosPorCarga(tecnicos, 1));
                            MostrarGrupoTecnicos("CARGA COMPLETA", FiltrarTecnicosPorCarga(tecnicos, 2));
                            MostrarGrupoTecnicos("NO DISPONIBLES POR ESTADO", FiltrarTecnicosPorCarga(tecnicos, 3));
                            break;
                        case 4:
                            {
                                List<Tecnico> mayores = BuscarTecnicosMasFinalizados(tecnicos);
                                if (mayores.Count == 0) Console.WriteLine("No hay servicios finalizados registrados.");
                                else MostrarGrupoTecnicos("MAYOR CANTIDAD DE SERVICIOS FINALIZADOS", mayores);
                                break;
                            }
                        case 5:
                            {
                                int[] cantidades = ContarEquiposAtendidosPorTipo(solicitudes);
                                Console.WriteLine("Equipos únicos con solicitudes entregadas:");
                                Console.WriteLine("Computadoras portátiles: " + cantidades[0]);
                                Console.WriteLine("Computadoras de escritorio: " + cantidades[1]);
                                Console.WriteLine("Impresoras: " + cantidades[2]);
                                Console.WriteLine("Total: " + (cantidades[0] + cantidades[1] + cantidades[2]));
                                break;
                            }
                        case 6:
                            MostrarReporteSolicitudesOrdenadas(solicitudes, true);
                            break;
                        case 7:
                            MostrarReporteSolicitudesOrdenadas(solicitudes, false);
                            break;
                        case 8:
                            {
                                List<Repuesto> ordenados = new List<Repuesto>(repuestos);
                                if (ordenados.Count >= 2) ordenados = ShellSortRepuestosPorExistencia(repuestos, true);
                                MostrarEncabezadoOrdenamiento("Shell Sort", "Existencia disponible", true, ordenados.Count);
                                MostrarAvisoCantidadOrdenamiento(ordenados.Count);
                                foreach (Repuesto repuesto in ordenados)
                                {
                                    repuesto.MostrarInformacion();
                                    Console.WriteLine();
                                }
                                break;
                            }
                        case 9:
                            {
                                List<Repuesto> bajos = BuscarRepuestosMinimos(repuestos);
                                Console.WriteLine("Repuestos al mínimo o agotados: " + bajos.Count);
                                if (bajos.Count == 0) Console.WriteLine("No hay repuestos al mínimo o agotados.");
                                foreach (Repuesto repuesto in bajos)
                                {
                                    Console.WriteLine("REP-" + repuesto.Codigo + " " + repuesto.Nombre +
                                        " | Existencia: " + repuesto.Existencia + " | Mínimo: " + repuesto.ExistenciaMinima);
                                }
                                break;
                            }
                        case 10:
                            Console.WriteLine("Se incluyen únicamente solicitudes entregadas.");
                            Console.WriteLine("Servicios finalizados: " + FiltrarSolicitudesPorSituacion(solicitudes, 2).Count);
                            Console.WriteLine("Ingresos totales: " + CalcularIngresosFinalizados(solicitudes).ToString("F2"));
                            break;
                        case 11:
                            GestionarBusquedas(clientes, solicitudes, portatiles, escritorios, impresoras);
                            continue;
                        case 12:
                            {
                                int opcionSemana = ValidarEntradasInt("Matriz semanal:\n1. Semana actual\n2. Consultar otra semana\n3. Regresar\nSeleccione una opción: ", 1, 3, "Opción no válida.");
                                if (opcionSemana == 3) continue;
                                DateTime fecha = DateTime.Today;
                                if (opcionSemana == 2)
                                {
                                    bool cancelar = false;
                                    while (true)
                                    {
                                        Console.Write("Fecha de la semana (dd/MM/yyyy, 0 para regresar): ");
                                        string entrada = LeerEntrada();
                                        if (entrada == null || entrada == "0")
                                        {
                                            cancelar = true;
                                            break;
                                        }
                                        if (DateTime.TryParseExact(entrada, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None, out fecha) && fecha <= DateTime.Today)
                                        {
                                            break;
                                        }
                                        MostrarErrorAnimado("Ingrese una fecha válida que no sea futura.");
                                        Console.WriteLine();
                                    }
                                    if (cancelar) continue;
                                }
                                Console.Clear();
                                MostrarMatrizCargaSemanal(tecnicos, solicitudes, fecha);
                                break;
                            }
                        case 13:
                            return;
                    }
                    Console.ReadKey();
                }
                catch (ArgumentException error)
                {
                    MostrarErrorAnimado(error.Message);
                    Console.ReadKey();
                }
            } while (opcionReporte != 13);
        }

        static List<Solicitud> FiltrarSolicitudesPorSituacion(List<Solicitud> solicitudes, int situacion)
        {
            List<Solicitud> resultado = new List<Solicitud>();
            foreach (Solicitud solicitud in solicitudes)
            {
                bool incluir = false;
                if (situacion == 1) incluir = solicitud.Estado != "Entregada" && solicitud.Estado != "Cancelada";
                else if (situacion == 2) incluir = solicitud.Estado == "Entregada";
                else if (situacion == 3) incluir = solicitud.Estado == "Cancelada";
                if (incluir) resultado.Add(solicitud);
            }
            return resultado;
        }

        static void MostrarResumenSolicitudes(string titulo, List<Solicitud> solicitudes)
        {
            Console.WriteLine(titulo + ": " + solicitudes.Count);
            if (solicitudes.Count == 0) Console.WriteLine("No hay solicitudes en este grupo.");
            foreach (Solicitud solicitud in solicitudes)
            {
                Console.WriteLine("SOL-" + solicitud.Codigo + " | Estado: " + solicitud.Estado + " | Prioridad: " + solicitud.Prioridad);
                Console.WriteLine("Cliente: " + solicitud.Cliente.NombreCompleto);
                Console.WriteLine("Recepción: " + solicitud.FechaRecepcion.ToString("dd/MM/yyyy HH:mm:ss") + " | Total: " + solicitud.Total.ToString("F2"));
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static List<Tecnico> FiltrarTecnicosPorCarga(List<Tecnico> tecnicos, int grupo)
        {
            List<Tecnico> resultado = new List<Tecnico>();
            foreach (Tecnico tecnico in tecnicos)
            {
                bool incluir = false;
                if (grupo == 1) incluir = tecnico.EstadoDisponibilidad == "Disponible" && tecnico.CantidadTrabajosActivos < Solicitud.LimiteTrabajosActivosTecnico;
                else if (grupo == 2) incluir = tecnico.CantidadTrabajosActivos >= Solicitud.LimiteTrabajosActivosTecnico;
                else if (grupo == 3) incluir = tecnico.EstadoDisponibilidad == "No Disponible" && tecnico.CantidadTrabajosActivos < Solicitud.LimiteTrabajosActivosTecnico;
                if (incluir) resultado.Add(tecnico);
            }
            return resultado;
        }

        static void MostrarGrupoTecnicos(string titulo, List<Tecnico> tecnicos)
        {
            Console.WriteLine(titulo + ": " + tecnicos.Count);
            if (tecnicos.Count == 0) Console.WriteLine("No hay técnicos en este grupo.");
            foreach (Tecnico tecnico in tecnicos)
            {
                Console.WriteLine("TEC-" + tecnico.Codigo + " " + tecnico.NombreCompleto);
                Console.WriteLine("Activos: " + tecnico.CantidadTrabajosActivos + "/" + Solicitud.LimiteTrabajosActivosTecnico +
                    " | Finalizados: " + tecnico.CantidadTrabajosFinalizados +
                    " | Estado: " + tecnico.EstadoDisponibilidad);
            }
            Console.WriteLine();
        }

        static List<Tecnico> BuscarTecnicosMasFinalizados(List<Tecnico> tecnicos)
        {
            int mayor = 0;
            foreach (Tecnico tecnico in tecnicos)
            {
                if (tecnico.CantidadTrabajosFinalizados > mayor) mayor = tecnico.CantidadTrabajosFinalizados;
            }
            List<Tecnico> resultado = new List<Tecnico>();
            if (mayor == 0) return resultado;
            foreach (Tecnico tecnico in tecnicos)
            {
                if (tecnico.CantidadTrabajosFinalizados == mayor) resultado.Add(tecnico);
            }
            return resultado;
        }

        static int[] ContarEquiposAtendidosPorTipo(List<Solicitud> solicitudes)
        {
            int[] cantidades = new int[3];
            List<Equipo> equiposContados = new List<Equipo>();
            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.Estado == "Entregada" && !equiposContados.Contains(solicitud.Equipo))
                {
                    equiposContados.Add(solicitud.Equipo);
                    if (solicitud.Equipo is ComputadoraPortatil) cantidades[0]++;
                    else if (solicitud.Equipo is ComputadoraDeEscritorio) cantidades[1]++;
                    else if (solicitud.Equipo is Impresora) cantidades[2]++;
                }
            }
            return cantidades;
        }

        static void MostrarAvisoCantidadOrdenamiento(int cantidad)
        {
            if (cantidad == 0) Console.WriteLine("No hay registros para mostrar; no se ejecutó el ordenamiento.");
            else if (cantidad == 1) Console.WriteLine("Único registro: no es necesario ejecutar el ordenamiento.");
        }

        static void MostrarReporteSolicitudesOrdenadas(List<Solicitud> solicitudes, bool porCosto)
        {
            List<Solicitud> ordenadas = new List<Solicitud>(solicitudes);
            if (porCosto)
            {
                if (solicitudes.Count >= 2) ordenadas = BubbleSortSolicitudesPorCosto(solicitudes, false);
                MostrarEncabezadoOrdenamiento("Bubble Sort", "Costo total", false, ordenadas.Count);
            }
            else
            {
                if (solicitudes.Count >= 2) ordenadas = OrdenarSolicitudesPorFecha(solicitudes, true);
                MostrarEncabezadoOrdenamiento("Quick Sort", "Fecha y hora de recepción", true, ordenadas.Count);
            }
            MostrarAvisoCantidadOrdenamiento(ordenadas.Count);
            MostrarResumenSolicitudes("SOLICITUDES", ordenadas);
        }

        static List<Repuesto> BuscarRepuestosMinimos(List<Repuesto> repuestos)
        {
            List<Repuesto> resultado = new List<Repuesto>();
            foreach (Repuesto repuesto in repuestos)
            {
                if (repuesto.Existencia <= repuesto.ExistenciaMinima) resultado.Add(repuesto);
            }
            return resultado;
        }

        static double CalcularIngresosFinalizados(List<Solicitud> solicitudes)
        {
            double ingresos = 0;
            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.Estado == "Entregada")
                {
                    double nuevoTotal = ingresos + solicitud.Total;
                    if (double.IsNaN(nuevoTotal) || double.IsInfinity(nuevoTotal))
                    {
                        throw new ArgumentException("El total de ingresos supera el límite numérico permitido.");
                    }
                    ingresos = nuevoTotal;
                }
            }
            return ingresos;
        }

        static DateTime ObtenerInicioSemana(DateTime fecha)
        {
            int diasDesdeLunes = ((int)fecha.DayOfWeek + 6) % 7;
            return fecha.Date.AddDays(-diasDesdeLunes);
        }

        static int[,] CrearMatrizCargaSemanal(List<Tecnico> tecnicos, List<Solicitud> solicitudes, DateTime fechaSemana)
        {
            DateTime inicio = ObtenerInicioSemana(fechaSemana);
            if (inicio > DateTime.MaxValue.Date.AddDays(-7))
            {
                throw new ArgumentException("La semana indicada está fuera del rango de fechas permitido.");
            }
            DateTime fin = inicio.AddDays(7);
            int[,] matriz = new int[tecnicos.Count, 7];
            for (int i = 0; i < tecnicos.Count; i++)
            {
                foreach (Solicitud solicitud in solicitudes)
                {
                    if (solicitud.Tecnico == tecnicos[i] && solicitud.FechaAsignacion != DateTime.MinValue &&
                        solicitud.FechaAsignacion >= inicio && solicitud.FechaAsignacion < fin)
                    {
                        int dia = (solicitud.FechaAsignacion.Date - inicio).Days;
                        matriz[i, dia]++;
                    }
                }
            }
            return matriz;
        }

        static int[] CalcularTotalesPorTecnico(int[,] matriz)
        {
            int[] totales = new int[matriz.GetLength(0)];
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++) totales[i] += matriz[i, j];
            }
            return totales;
        }

        static int[] CalcularTotalesPorDia(int[,] matriz)
        {
            int[] totales = new int[matriz.GetLength(1)];
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                for (int i = 0; i < matriz.GetLength(0); i++) totales[j] += matriz[i, j];
            }
            return totales;
        }

        static void MostrarMatrizCargaSemanal(List<Tecnico> tecnicos, List<Solicitud> solicitudes, DateTime fechaSemana)
        {
            DateTime inicio = ObtenerInicioSemana(fechaSemana);
            int[,] matriz = CrearMatrizCargaSemanal(tecnicos, solicitudes, fechaSemana);
            int[] porTecnico = CalcularTotalesPorTecnico(matriz);
            int[] porDia = CalcularTotalesPorDia(matriz);
            string[] dias = { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" };
            Console.WriteLine("Semana del " + inicio.ToString("dd/MM/yyyy") + " al " + inicio.AddDays(6).ToString("dd/MM/yyyy"));
            Console.WriteLine("Asignaciones registradas, incluidas las entregadas y canceladas.");
            Console.Write("Técnico".PadRight(16));
            foreach (string dia in dias) Console.Write(dia.PadLeft(6));
            Console.WriteLine("Total".PadLeft(7));
            int mayorTecnico = 0;
            for (int i = 0; i < tecnicos.Count; i++)
            {
                Console.Write(("TEC-" + tecnicos[i].Codigo).PadRight(16));
                for (int j = 0; j < 7; j++) Console.Write(matriz[i, j].ToString().PadLeft(6));
                Console.WriteLine(porTecnico[i].ToString().PadLeft(7));
                if (porTecnico[i] > mayorTecnico) mayorTecnico = porTecnico[i];
            }
            Console.Write("Total por día".PadRight(16));
            int mayorDia = 0;
            foreach (int cantidad in porDia)
            {
                Console.Write(cantidad.ToString().PadLeft(6));
                if (cantidad > mayorDia) mayorDia = cantidad;
            }
            Console.WriteLine();
            if (mayorTecnico == 0)
            {
                Console.WriteLine("No hay asignaciones registradas en esta semana.");
                return;
            }
            Console.WriteLine("Técnicos con mayor carga:");
            for (int i = 0; i < tecnicos.Count; i++)
            {
                if (porTecnico[i] == mayorTecnico) Console.WriteLine("TEC-" + tecnicos[i].Codigo + " " + tecnicos[i].NombreCompleto + ": " + mayorTecnico);
            }
            Console.WriteLine("Días con mayor carga:");
            for (int j = 0; j < 7; j++)
            {
                if (porDia[j] == mayorDia) Console.WriteLine(dias[j] + ": " + mayorDia);
            }
        }

        static void GestionarDatosPrueba(List<Cliente> clientes, List<Tecnico> tecnicos, List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras, List<Repuesto> repuestos, List<Solicitud> solicitudes, Queue<Solicitud> normales, Queue<Solicitud> urgentes, Random generador)
        {
            int opcion;
            do
            {
                opcion = ValidarEntradasInt("============================================\n DATOS DE PRUEBA\n============================================\n1. Agregar un lote de datos de prueba\n2. Regresar al menú principal\n\nSeleccione una opción: ", 1, 2, "Opción no válida.");
                if (opcion == 2) return;
                int cantidad = ValidarEntradasInt("Cantidad de solicitudes de prueba (1 a 100, 0 para regresar): ", 0, 100, "Ingrese una cantidad entre 0 y 100.");
                if (cantidad == 0) continue;
                string avisoTurnos = "";
                if (BuscarSolicitudesPorEstado(solicitudes, "En espera").Count > 0)
                {
                    avisoTurnos = "\nHay turnos pendientes: este lote solo creará solicitudes recibidas o en espera para respetarlos.";
                }
                int confirmar = ValidarEntradasInt("Se agregarán " + cantidad + " solicitudes, con clientes, equipos, técnicos y repuestos de prueba.\nLos datos actuales se conservarán." + avisoTurnos + "\n\n1. Generar\n2. Cancelar\nSeleccione una opción: ", 1, 2, "Opción no válida.");
                if (confirmar == 2) continue;
                try
                {
                    int clientesAntes = clientes.Count;
                    int tecnicosAntes = tecnicos.Count;
                    int equiposAntes = portatiles.Count + escritorios.Count + impresoras.Count;
                    int repuestosAntes = repuestos.Count;
                    int solicitudesAntes = solicitudes.Count;
                    GenerarDatosPrueba(clientes, tecnicos, portatiles, escritorios, impresoras, repuestos, solicitudes, normales, urgentes, cantidad, generador);
                    Console.WriteLine("Datos agregados correctamente:");
                    Console.WriteLine("Clientes: " + (clientes.Count - clientesAntes));
                    Console.WriteLine("Técnicos: " + (tecnicos.Count - tecnicosAntes));
                    Console.WriteLine("Equipos: " + (portatiles.Count + escritorios.Count + impresoras.Count - equiposAntes));
                    Console.WriteLine("Repuestos: " + (repuestos.Count - repuestosAntes));
                    Console.WriteLine("Solicitudes: " + (solicitudes.Count - solicitudesAntes));
                    Console.WriteLine("Solicitudes en espera: " + (normales.Count + urgentes.Count));
                    Console.WriteLine("Aviso: repuestos al mínimo o agotados: " + BuscarRepuestosMinimos(repuestos).Count);
                    Console.WriteLine("Puede consultar el detalle en Reportes.");
                    Console.ReadKey();
                }
                catch (ArgumentException error)
                {
                    MostrarErrorAnimado(error.Message);
                    Console.ReadKey();
                }
            } while (opcion != 2);
        }

        static int ObtenerSiguienteCodigoPersona<T>(List<T> personas, int cantidad) where T : Persona
        {
            int mayor = 0;
            foreach (T persona in personas)
            {
                if (persona.Codigo > int.MaxValue - cantidad)
                {
                    throw new ArgumentException("No hay suficientes códigos disponibles para generar el lote.");
                }
                if (persona.Codigo > mayor) mayor = persona.Codigo;
            }
            return mayor + 1;
        }

        static int ObtenerSiguienteCodigoEquipo<T>(List<T> equipos, int cantidad) where T : Equipo
        {
            int mayor = 0;
            foreach (T equipo in equipos)
            {
                if (equipo.Codigo > int.MaxValue - cantidad)
                {
                    throw new ArgumentException("No hay suficientes códigos de equipo disponibles para generar el lote.");
                }
                if (equipo.Codigo > mayor) mayor = equipo.Codigo;
            }
            return mayor + 1;
        }

        static int ObtenerSiguienteCodigoRepuesto(List<Repuesto> repuestos, int cantidad)
        {
            int mayor = 0;
            foreach (Repuesto repuesto in repuestos)
            {
                if (repuesto.Codigo > int.MaxValue - cantidad)
                {
                    throw new ArgumentException("No hay suficientes códigos de repuesto disponibles para generar el lote.");
                }
                if (repuesto.Codigo > mayor) mayor = repuesto.Codigo;
            }
            return mayor + 1;
        }

        static bool ExisteNumeroSerie(List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras, string numeroSerie)
        {
            string serie = numeroSerie.Trim().ToLower();
            foreach (ComputadoraPortatil equipo in portatiles)
            {
                if (equipo.NumeroSerie.Trim().ToLower() == serie) return true;
            }
            foreach (ComputadoraDeEscritorio equipo in escritorios)
            {
                if (equipo.NumeroSerie.Trim().ToLower() == serie) return true;
            }
            foreach (Impresora equipo in impresoras)
            {
                if (equipo.NumeroSerie.Trim().ToLower() == serie) return true;
            }
            return false;
        }

        static void GenerarDatosPrueba(List<Cliente> clientes, List<Tecnico> tecnicos, List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras, List<Repuesto> repuestos, List<Solicitud> solicitudes, Queue<Solicitud> normales, Queue<Solicitud> urgentes, int cantidad, Random generador)
        {
            if (cantidad < 1 || cantidad > 100 || generador == null)
            {
                throw new ArgumentException("Debe generar entre 1 y 100 solicitudes con un generador válido.");
            }
            int codigoCliente = ObtenerSiguienteCodigoPersona(clientes, cantidad);
            int codigoTecnico = ObtenerSiguienteCodigoPersona(tecnicos, cantidad);
            int codigoPortatil = ObtenerSiguienteCodigoEquipo(portatiles, cantidad);
            int codigoEscritorio = ObtenerSiguienteCodigoEquipo(escritorios, cantidad);
            int codigoImpresora = ObtenerSiguienteCodigoEquipo(impresoras, cantidad);
            int codigoRepuesto = ObtenerSiguienteCodigoRepuesto(repuestos, cantidad);
            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.Codigo > int.MaxValue - cantidad)
                {
                    throw new ArgumentException("No hay suficientes códigos de solicitud disponibles para generar el lote.");
                }
            }

            List<Cliente> clientesNuevos = new List<Cliente>();
            List<Tecnico> tecnicosNuevos = new List<Tecnico>();
            List<ComputadoraPortatil> portatilesNuevas = new List<ComputadoraPortatil>();
            List<ComputadoraDeEscritorio> escritoriosNuevos = new List<ComputadoraDeEscritorio>();
            List<Impresora> impresorasNuevas = new List<Impresora>();
            List<Repuesto> repuestosNuevos = new List<Repuesto>();
            List<Solicitud> solicitudesNuevas = new List<Solicitud>();
            List<Solicitud> solicitudesTrabajo = new List<Solicitud>(solicitudes);
            Queue<Solicitud> normalesTrabajo = new Queue<Solicitud>();
            Queue<Solicitud> urgentesTrabajo = new Queue<Solicitud>();
            string[] especialidades = { "Soporte", "Redes", "Impresión" };
            string[] estadosFisicos = { "Excelente", "Con daños leves", "Daños severos" };
            string[] marcasComputadora = { "HP", "Dell", "Lenovo" };
            bool hayTurnosAnteriores = BuscarSolicitudesPorEstado(solicitudes, "En espera").Count > 0;
            int[] destinos = new int[cantidad];
            int primerEstado = generador.Next(0, 8);
            int posicion = 0;
            if (hayTurnosAnteriores)
            {
                for (int i = 0; i < cantidad; i++) destinos[i] = generador.Next(0, 2);
            }
            else
            {
                for (int i = 0; i < cantidad; i++)
                {
                    int destino = (primerEstado + i) % 8;
                    if (destino >= 2) destinos[posicion++] = destino;
                }
                for (int i = 0; i < cantidad; i++)
                {
                    int destino = (primerEstado + i) % 8;
                    if (destino < 2) destinos[posicion++] = destino;
                }
            }

            for (int i = 0; i < cantidad; i++)
            {
                int codigoNuevoCliente = codigoCliente + i;
                Cliente cliente = new Cliente(codigoNuevoCliente, "Cliente de prueba " + codigoNuevoCliente,
                    generador.Next(20000000, 80000000).ToString(), "cliente" + codigoNuevoCliente + "@prueba.local", "Estudiante");
                clientesNuevos.Add(cliente);
                int tipo = generador.Next(0, 3);
                int codigoEquipo;
                string prefijo;
                if (tipo == 0) { codigoEquipo = codigoPortatil + portatilesNuevas.Count; prefijo = "PORT"; }
                else if (tipo == 1) { codigoEquipo = codigoEscritorio + escritoriosNuevos.Count; prefijo = "DESK"; }
                else { codigoEquipo = codigoImpresora + impresorasNuevas.Count; prefijo = "IMP"; }
                string serieBase = "PRUEBA-" + prefijo + "-" + codigoEquipo;
                string serie = serieBase;
                int intento = 1;
                while (ExisteNumeroSerie(portatiles, escritorios, impresoras, serie) ||
                    ExisteNumeroSerie(portatilesNuevas, escritoriosNuevos, impresorasNuevas, serie))
                {
                    serie = serieBase + "-" + intento;
                    intento++;
                }
                string marca = marcasComputadora[generador.Next(0, marcasComputadora.Length)];
                if (tipo == 2) marca = generador.Next(0, 2) == 0 ? "Canon" : "Epson";
                string modelo = "Modelo-" + generador.Next(100, 1000);
                string estadoFisico = estadosFisicos[generador.Next(0, estadosFisicos.Length)];
                Equipo equipo;
                if (tipo == 0)
                {
                    ComputadoraPortatil portatil = new ComputadoraPortatil(codigoEquipo, marca, modelo, estadoFisico, "Cargador", serie, cliente);
                    portatil.TipoEquipo = "Computadora portátil";
                    portatilesNuevas.Add(portatil);
                    equipo = portatil;
                }
                else if (tipo == 1)
                {
                    ComputadoraDeEscritorio escritorio = new ComputadoraDeEscritorio(codigoEquipo, marca, modelo, estadoFisico, "Cable de energía", serie, cliente);
                    escritorio.TipoEquipo = "Computadora de escritorio";
                    escritoriosNuevos.Add(escritorio);
                    equipo = escritorio;
                }
                else
                {
                    Impresora impresora = new Impresora(codigoEquipo, marca, modelo, estadoFisico, "Cable USB", serie, cliente);
                    impresora.TipoEquipo = "Impresora";
                    impresorasNuevas.Add(impresora);
                    equipo = impresora;
                }

                string especialidad = especialidades[generador.Next(0, especialidades.Length)];
                string prioridad = generador.Next(0, 2) == 0 ? "Normal" : "Urgente";
                Solicitud nueva = GuardarSolicitud(solicitudesTrabajo, cliente, equipo, prioridad,
                    "Problema de prueba en " + marca + " " + modelo, especialidad, normalesTrabajo, urgentesTrabajo);
                solicitudesNuevas.Add(nueva);
                Tecnico tecnico = BuscarTecnicoDisponible(tecnicosNuevos, nueva);
                if (tecnico == null)
                {
                    int codigoNuevoTecnico = codigoTecnico + tecnicosNuevos.Count;
                    tecnico = new Tecnico(codigoNuevoTecnico, "Técnico de prueba " + codigoNuevoTecnico, especialidad, "Disponible");
                    tecnicosNuevos.Add(tecnico);
                }
                int destino = destinos[i];
                int existencia = generador.Next(0, 4);
                if (destino >= 4) existencia = generador.Next(2, 6);
                double costo = generador.Next(10, 101);
                double precio = costo + generador.Next(5, 51);
                Repuesto repuesto = new Repuesto(codigoRepuesto + i, "Repuesto de prueba " + (codigoRepuesto + i),
                    equipo.TipoEquipo, existencia, 2, costo, precio, 0);
                repuestosNuevos.Add(repuesto);

                if (destino == 0)
                {
                    nueva.DeshacerUltimoEstado();
                }
                else if (destino >= 2)
                {
                    nueva.AsignarTecnico(tecnico);
                    if (destino >= 3)
                    {
                        nueva.CambiarEstado("En diagnóstico");
                        nueva.RegistrarDiagnostico("Diagnóstico de prueba: componente con falla.");
                        nueva.RegistrarAccion("Revisión y limpieza del equipo.");
                    }
                    if (destino >= 4)
                    {
                        nueva.CambiarEstado("En reparación");
                        int limite = 3;
                        if (repuesto.Existencia < limite) limite = repuesto.Existencia;
                        int utilizados = generador.Next(1, limite + 1);
                        nueva.RegistrarRepuesto(repuesto, utilizados);
                        nueva.RegistrarManoObra(generador.Next(25, 301));
                    }
                    if (destino == 5 || destino == 6)
                    {
                        nueva.CambiarEstado("Lista para entregar");
                    }
                    if (destino == 6) nueva.RegistrarEntrega();
                    else if (destino == 7) nueva.Cancelar();
                }
            }

            foreach (Cliente cliente in clientesNuevos) clientes.Add(cliente);
            foreach (Tecnico tecnico in tecnicosNuevos) tecnicos.Add(tecnico);
            foreach (ComputadoraPortatil equipo in portatilesNuevas) portatiles.Add(equipo);
            foreach (ComputadoraDeEscritorio equipo in escritoriosNuevos) escritorios.Add(equipo);
            foreach (Impresora equipo in impresorasNuevas) impresoras.Add(equipo);
            foreach (Repuesto repuesto in repuestosNuevos) repuestos.Add(repuesto);
            foreach (Solicitud solicitud in solicitudesNuevas) solicitudes.Add(solicitud);
            SincronizarColasSolicitudes(solicitudes, normales, urgentes);
        }

        static void GestionarOrdenamientos(List<Solicitud> solicitudes, List<Tecnico> tecnicos, List<Repuesto> repuestos)
        {
            int opcionOrdenamiento;
            do
            {
                opcionOrdenamiento = ValidarEntradasInt("============================================\n ORDENAMIENTOS\n============================================\n1. Solicitudes por costo total - Bubble Sort\n2. Técnicos por servicios finalizados - Selection Sort\n3. Repuestos por existencia - Shell Sort\n4. Solicitudes por fecha de recepción - Quick Sort\n5. Regresar al menú principal\n\nSeleccione una opción: ", 1, 5, "Opción no válida.");
                if (opcionOrdenamiento == 5) return;
                try
                {
                    int cantidad = solicitudes.Count;
                    if (opcionOrdenamiento == 2) cantidad = tecnicos.Count;
                    else if (opcionOrdenamiento == 3) cantidad = repuestos.Count;
                    if (cantidad < 2)
                    {
                        throw new ArgumentException("Se necesitan al menos dos registros para realizar el ordenamiento.");
                    }
                    int opcionSentido = ValidarEntradasInt("Sentido del ordenamiento:\n1. Ascendente\n2. Descendente\n3. Regresar\nSeleccione una opción: ", 1, 3, "Opción no válida.");
                    if (opcionSentido == 3) continue;
                    bool ascendente = opcionSentido == 1;
                    Console.Clear();
                    switch (opcionOrdenamiento)
                    {
                        case 1:
                            {
                                List<Solicitud> ordenadas = BubbleSortSolicitudesPorCosto(solicitudes, ascendente);
                                MostrarEncabezadoOrdenamiento("Bubble Sort", "Costo total de solicitudes", ascendente, ordenadas.Count);
                                foreach (Solicitud solicitud in ordenadas)
                                {
                                    solicitud.MostrarInformacion();
                                    Console.WriteLine();
                                }
                                break;
                            }
                        case 2:
                            {
                                List<Tecnico> ordenados = SelectionSortTecnicosPorFinalizados(tecnicos, ascendente);
                                MostrarEncabezadoOrdenamiento("Selection Sort", "Cantidad de servicios finalizados", ascendente, ordenados.Count);
                                foreach (Tecnico tecnico in ordenados)
                                {
                                    tecnico.MostrarInformacion();
                                    Console.WriteLine();
                                }
                                break;
                            }
                        case 3:
                            {
                                List<Repuesto> ordenados = ShellSortRepuestosPorExistencia(repuestos, ascendente);
                                MostrarEncabezadoOrdenamiento("Shell Sort", "Existencia disponible de repuestos", ascendente, ordenados.Count);
                                foreach (Repuesto repuesto in ordenados)
                                {
                                    repuesto.MostrarInformacion();
                                    Console.WriteLine();
                                }
                                break;
                            }
                        case 4:
                            {
                                List<Solicitud> ordenadas = OrdenarSolicitudesPorFecha(solicitudes, ascendente);
                                MostrarEncabezadoOrdenamiento("Quick Sort", "Fecha y hora de recepción", ascendente, ordenadas.Count);
                                foreach (Solicitud solicitud in ordenadas)
                                {
                                    solicitud.MostrarInformacion();
                                    Console.WriteLine();
                                }
                                break;
                            }
                    }
                    Console.ReadKey();
                }
                catch (ArgumentException error)
                {
                    MostrarErrorAnimado(error.Message);
                    Console.ReadKey();
                }
            } while (opcionOrdenamiento != 5);
        }

        static void MostrarEncabezadoOrdenamiento(string algoritmo, string criterio, bool ascendente, int cantidad)
        {
            Console.WriteLine("Algoritmo: " + algoritmo);
            Console.WriteLine("Criterio: " + criterio);
            if (ascendente) Console.WriteLine("Sentido: Ascendente");
            else Console.WriteLine("Sentido: Descendente");
            Console.WriteLine("Cantidad de registros: " + cantidad);
            Console.WriteLine();
        }

        static List<Solicitud> BubbleSortSolicitudesPorCosto(List<Solicitud> solicitudes, bool ascendente)
        {
            List<Solicitud> ordenadas = new List<Solicitud>(solicitudes);
            for (int i = 0; i < ordenadas.Count - 1; i++)
            {
                bool huboIntercambio = false;
                for (int j = 0; j < ordenadas.Count - 1 - i; j++)
                {
                    if ((ascendente && ordenadas[j].Total > ordenadas[j + 1].Total) ||
                        (!ascendente && ordenadas[j].Total < ordenadas[j + 1].Total))
                    {
                        Solicitud temporal = ordenadas[j];
                        ordenadas[j] = ordenadas[j + 1];
                        ordenadas[j + 1] = temporal;
                        huboIntercambio = true;
                    }
                }
                if (!huboIntercambio) break;
            }
            return ordenadas;
        }

        static List<Tecnico> SelectionSortTecnicosPorFinalizados(List<Tecnico> tecnicos, bool ascendente)
        {
            List<Tecnico> ordenados = new List<Tecnico>(tecnicos);
            for (int i = 0; i < ordenados.Count - 1; i++)
            {
                int indiceSeleccionado = i;
                for (int j = i + 1; j < ordenados.Count; j++)
                {
                    if ((ascendente && ordenados[j].CantidadTrabajosFinalizados < ordenados[indiceSeleccionado].CantidadTrabajosFinalizados) ||
                        (!ascendente && ordenados[j].CantidadTrabajosFinalizados > ordenados[indiceSeleccionado].CantidadTrabajosFinalizados))
                    {
                        indiceSeleccionado = j;
                    }
                }
                if (indiceSeleccionado != i)
                {
                    Tecnico temporal = ordenados[i];
                    ordenados[i] = ordenados[indiceSeleccionado];
                    ordenados[indiceSeleccionado] = temporal;
                }
            }
            return ordenados;
        }

        static List<Repuesto> ShellSortRepuestosPorExistencia(List<Repuesto> repuestos, bool ascendente)
        {
            List<Repuesto> ordenados = new List<Repuesto>(repuestos);
            for (int salto = ordenados.Count / 2; salto > 0; salto /= 2)
            {
                for (int i = salto; i < ordenados.Count; i++)
                {
                    Repuesto temporal = ordenados[i];
                    int j = i;
                    while (j >= salto &&
                        ((ascendente && ordenados[j - salto].Existencia > temporal.Existencia) ||
                        (!ascendente && ordenados[j - salto].Existencia < temporal.Existencia)))
                    {
                        ordenados[j] = ordenados[j - salto];
                        j -= salto;
                    }
                    ordenados[j] = temporal;
                }
            }
            return ordenados;
        }

        static List<Solicitud> OrdenarSolicitudesPorFecha(List<Solicitud> solicitudes, bool ascendente)
        {
            List<Solicitud> ordenadas = new List<Solicitud>(solicitudes);
            if (ordenadas.Count > 1)
            {
                QuickSortSolicitudesPorFecha(ordenadas, 0, ordenadas.Count - 1, ascendente);
            }
            return ordenadas;
        }

        static void QuickSortSolicitudesPorFecha(List<Solicitud> solicitudes, int inicio, int fin, bool ascendente)
        {
            if (inicio >= fin) return;
            int izquierda = inicio;
            int derecha = fin;
            DateTime pivote = solicitudes[inicio + (fin - inicio) / 2].FechaRecepcion;
            while (izquierda <= derecha)
            {
                while (izquierda <= fin &&
                    ((ascendente && solicitudes[izquierda].FechaRecepcion < pivote) ||
                    (!ascendente && solicitudes[izquierda].FechaRecepcion > pivote)))
                {
                    izquierda++;
                }
                while (derecha >= inicio &&
                    ((ascendente && solicitudes[derecha].FechaRecepcion > pivote) ||
                    (!ascendente && solicitudes[derecha].FechaRecepcion < pivote)))
                {
                    derecha--;
                }
                if (izquierda <= derecha)
                {
                    Solicitud temporal = solicitudes[izquierda];
                    solicitudes[izquierda] = solicitudes[derecha];
                    solicitudes[derecha] = temporal;
                    izquierda++;
                    derecha--;
                }
            }
            if (inicio < derecha) QuickSortSolicitudesPorFecha(solicitudes, inicio, derecha, ascendente);
            if (izquierda < fin) QuickSortSolicitudesPorFecha(solicitudes, izquierda, fin, ascendente);
        }

        static void GestionarBusquedas(List<Cliente> clientes, List<Solicitud> solicitudes, List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras)
        {
            int opcionBusqueda;
            do
            {
                opcionBusqueda = ValidarEntradasInt("============================================\n BÚSQUEDAS\n============================================\n1. Clientes por nombre parcial - Secuencial\n2. Solicitudes por estado - Secuencial\n3. Equipos por marca - Secuencial\n4. Solicitud por código - Binaria\n5. Regresar al menú principal\n\nSeleccione una opción: ", 1, 5, "Opción no válida.");
                try
                {
                    switch (opcionBusqueda)
                    {
                        case 1:
                            {
                                if (clientes.Count == 0) throw new ArgumentException("No hay clientes registrados.");
                                string nombre = LeerTextoSolicitud("Nombre parcial (0 para regresar): ", 1);
                                if (nombre == "0") break;
                                List<Cliente> encontrados = BuscarClientesPorNombre(clientes, nombre);
                                Console.Clear();
                                Console.WriteLine("Algoritmo: Búsqueda secuencial");
                                Console.WriteLine("Criterio: Nombre parcial - " + nombre);
                                Console.WriteLine("Coincidencias: " + encontrados.Count);
                                if (encontrados.Count == 0) Console.WriteLine("No se encontraron clientes.");
                                foreach (Cliente cliente in encontrados)
                                {
                                    cliente.MostrarInformacion();
                                    Console.WriteLine();
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {
                                if (solicitudes.Count == 0) throw new ArgumentException("No hay solicitudes registradas.");
                                string estado = SeleccionarEstadoBusqueda();
                                if (estado == null) break;
                                List<Solicitud> encontradas = BuscarSolicitudesPorEstado(solicitudes, estado);
                                Console.Clear();
                                Console.WriteLine("Algoritmo: Búsqueda secuencial");
                                Console.WriteLine("Criterio: Estado - " + estado);
                                Console.WriteLine("Coincidencias: " + encontradas.Count);
                                if (encontradas.Count == 0) Console.WriteLine("No se encontraron solicitudes.");
                                foreach (Solicitud solicitud in encontradas)
                                {
                                    solicitud.MostrarInformacion();
                                    Console.WriteLine();
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 3:
                            {
                                if (portatiles.Count + escritorios.Count + impresoras.Count == 0)
                                {
                                    throw new ArgumentException("No hay equipos registrados.");
                                }
                                string marca = LeerTextoSolicitud("Marca (0 para regresar): ", 1);
                                if (marca == "0") break;
                                List<Equipo> encontrados = BuscarEquiposPorMarca(portatiles, escritorios, impresoras, marca);
                                Console.Clear();
                                Console.WriteLine("Algoritmo: Búsqueda secuencial");
                                Console.WriteLine("Criterio: Marca - " + marca);
                                Console.WriteLine("Coincidencias: " + encontrados.Count);
                                if (encontrados.Count == 0) Console.WriteLine("No se encontraron equipos.");
                                foreach (Equipo equipo in encontrados)
                                {
                                    string abreviatura = "IMP";
                                    if (equipo is ComputadoraPortatil) abreviatura = "PORT";
                                    else if (equipo is ComputadoraDeEscritorio) abreviatura = "DESK";
                                    equipo.MostrarInformacion(abreviatura);
                                    Console.WriteLine();
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 4:
                            {
                                if (solicitudes.Count == 0) throw new ArgumentException("No hay solicitudes registradas.");
                                int codigo = ValidarEntradasInt("Código de solicitud: SOL- (0 para regresar): ", 0, int.MaxValue, "Código no válido.");
                                if (codigo == 0) break;
                                Solicitud encontrada = BuscarSolicitudBinaria(solicitudes, codigo);
                                Console.Clear();
                                Console.WriteLine("Algoritmo: Búsqueda binaria");
                                Console.WriteLine("Criterio: Código de solicitud - SOL-" + codigo);
                                Console.WriteLine("Orden previo: Bubble Sort por código, ascendente.");
                                if (encontrada == null) Console.WriteLine("Solicitud no encontrada.");
                                else encontrada.MostrarInformacion();
                                Console.ReadKey();
                                break;
                            }
                        case 5:
                            Console.Clear();
                            break;
                    }
                }
                catch (ArgumentException error)
                {
                    MostrarErrorAnimado(error.Message);
                    Console.ReadKey();
                }
            } while (opcionBusqueda != 5);
        }

        static string SeleccionarEstadoBusqueda()
        {
            int opcion = ValidarEntradasInt("Estado de la solicitud:\n1. Recibida\n2. En espera\n3. Asignada\n4. En diagnóstico\n5. En reparación\n6. Lista para entregar\n7. Entregada\n8. Cancelada\n9. Regresar\nSeleccione una opción: ", 1, 9, "Opción no válida.");
            switch (opcion)
            {
                case 1: return "Recibida";
                case 2: return "En espera";
                case 3: return "Asignada";
                case 4: return "En diagnóstico";
                case 5: return "En reparación";
                case 6: return "Lista para entregar";
                case 7: return "Entregada";
                case 8: return "Cancelada";
                default: return null;
            }
        }

        static List<Cliente> BuscarClientesPorNombre(List<Cliente> clientes, string nombre)
        {
            if (nombre == null || nombre.Trim().Length == 0)
            {
                throw new ArgumentException("Debe ingresar un nombre para buscar.");
            }
            string texto = nombre.Trim().ToLower();
            List<Cliente> encontrados = new List<Cliente>();
            foreach (Cliente cliente in clientes)
            {
                if (cliente.NombreCompleto != null && cliente.NombreCompleto.ToLower().Contains(texto))
                {
                    encontrados.Add(cliente);
                }
            }
            return encontrados;
        }

        static List<Solicitud> BuscarSolicitudesPorEstado(List<Solicitud> solicitudes, string estado)
        {
            if (estado == null || estado.Trim().Length == 0)
            {
                throw new ArgumentException("Debe indicar un estado para buscar.");
            }
            string texto = estado.Trim().ToLower();
            List<Solicitud> encontradas = new List<Solicitud>();
            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.Estado.ToLower() == texto)
                {
                    encontradas.Add(solicitud);
                }
            }
            return encontradas;
        }

        static List<Equipo> BuscarEquiposPorMarca(List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras, string marca)
        {
            if (marca == null || marca.Trim().Length == 0)
            {
                throw new ArgumentException("Debe indicar una marca para buscar.");
            }
            string texto = marca.Trim().ToLower();
            List<Equipo> encontrados = new List<Equipo>();
            foreach (ComputadoraPortatil equipo in portatiles)
            {
                if (equipo.Marca.ToLower().Contains(texto)) encontrados.Add(equipo);
            }
            foreach (ComputadoraDeEscritorio equipo in escritorios)
            {
                if (equipo.Marca.ToLower().Contains(texto)) encontrados.Add(equipo);
            }
            foreach (Impresora equipo in impresoras)
            {
                if (equipo.Marca.ToLower().Contains(texto)) encontrados.Add(equipo);
            }
            return encontrados;
        }

        static List<Solicitud> OrdenarSolicitudesPorCodigo(List<Solicitud> solicitudes)
        {
            List<Solicitud> ordenadas = new List<Solicitud>(solicitudes);
            for (int i = 0; i < ordenadas.Count - 1; i++)
            {
                bool huboIntercambio = false;
                for (int j = 0; j < ordenadas.Count - 1 - i; j++)
                {
                    if (ordenadas[j].Codigo > ordenadas[j + 1].Codigo)
                    {
                        Solicitud temporal = ordenadas[j];
                        ordenadas[j] = ordenadas[j + 1];
                        ordenadas[j + 1] = temporal;
                        huboIntercambio = true;
                    }
                }
                if (!huboIntercambio) break;
            }
            return ordenadas;
        }

        static Solicitud BuscarSolicitudBinaria(List<Solicitud> solicitudes, int codigo)
        {
            List<Solicitud> ordenadas = OrdenarSolicitudesPorCodigo(solicitudes);
            int izquierda = 0;
            int derecha = ordenadas.Count - 1;
            while (izquierda <= derecha)
            {
                int medio = izquierda + (derecha - izquierda) / 2;
                if (ordenadas[medio].Codigo == codigo)
                {
                    return ordenadas[medio];
                }
                else if (codigo < ordenadas[medio].Codigo)
                {
                    derecha = medio - 1;
                }
                else
                {
                    izquierda = medio + 1;
                }
            }
            return null;
        }

        static string LeerTextoSolicitud(string mensaje, int longitudMinima)
        {
            while (true)
            {
                Console.Write(mensaje);
                string texto = LeerEntrada();
                if (texto == null)
                {
                    throw new ArgumentException("No se recibió una entrada de texto.");
                }
                if (texto.Trim().Length >= longitudMinima)
                {
                    return texto.Trim();
                }
                MostrarErrorAnimado("Debe ingresar al menos " + longitudMinima + " caracteres.");
                Console.WriteLine();
            }
        }

        static bool ExisteSolicitudActiva(List<Solicitud> solicitudes, Equipo equipo)
        {
            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.Equipo == equipo && solicitud.Estado != "Entregada" && solicitud.Estado != "Cancelada")
                {
                    return true;
                }
            }
            return false;
        }

        static Solicitud GuardarSolicitud(List<Solicitud> solicitudes, Cliente cliente, Equipo equipo, string prioridad, string problema, string especialidad, Queue<Solicitud> normales, Queue<Solicitud> urgentes)
        {
            if (cliente == null || equipo == null)
            {
                throw new ArgumentException("Debe seleccionar un cliente y un equipo existentes.");
            }
            if (ExisteSolicitudActiva(solicitudes, equipo))
            {
                throw new ArgumentException("El equipo ya tiene una solicitud activa.");
            }
            if (cliente.CantidadSolicitudes == int.MaxValue)
            {
                throw new ArgumentException("El cliente alcanzó el límite de solicitudes registradas.");
            }
            int codigo = 1;
            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.Codigo == int.MaxValue)
                {
                    throw new ArgumentException("Se alcanzó el límite de códigos de solicitud.");
                }
                if (solicitud.Codigo >= codigo) codigo = solicitud.Codigo + 1;
            }
            Solicitud nuevaSolicitud = new Solicitud(codigo, "Sin diagnóstico inicial", "Recibida", prioridad, problema, 0, 0, cliente, equipo, null);
            nuevaSolicitud.EspecialidadRequerida = especialidad;
            nuevaSolicitud.CambiarEstado("En espera");
            solicitudes.Add(nuevaSolicitud);
            cliente.CantidadSolicitudes++;
            SincronizarColasSolicitudes(solicitudes, normales, urgentes);
            return nuevaSolicitud;
        }

        static void CrearNuevaSolicitud(List<Cliente> clientes, List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras, List<Tecnico> tecnicos, List<Solicitud> solicitudes, Queue<Solicitud> normales, Queue<Solicitud> urgentes)
        {
            if (clientes.Count == 0 || tecnicos.Count == 0 || portatiles.Count + escritorios.Count + impresoras.Count == 0)
            {
                throw new ArgumentException("Debe registrar clientes, equipos y al menos una especialidad de técnico.");
            }
            Cliente clienteSeleccionado = null;
            while (true)
            {
                int codigoCliente = ValidarEntradasInt("Ingrese código de cliente: CLI- (0 para cancelar): ", 0, int.MaxValue, "Código no válido.");
                if (codigoCliente == 0) return;
                for (int i = 0; i < clientes.Count; i++)
                {
                    if (clientes[i].Codigo == codigoCliente)
                    {
                        clienteSeleccionado = clientes[i];
                        break;
                    }
                }
                if (clienteSeleccionado != null) break;
                MostrarErrorAnimado("Cliente no encontrado.");
            }
            Equipo equipoSeleccionado = null;
            while (equipoSeleccionado == null)
            {
                int tipoEquipo = ValidarEntradasInt("Tipo de equipo:\n1. Computadora portátil\n2. Computadora de escritorio\n3. Impresora\n4. Cancelar\nSeleccione una opción: ", 1, 4, "Opción no válida.");
                if (tipoEquipo == 4) return;
                int codigoEquipo = ValidarEntradasInt("Ingrese código del equipo (0 para cancelar): ", 0, int.MaxValue, "Código no válido.");
                if (codigoEquipo == 0) return;
                switch (tipoEquipo)
                {
                    case 1:
                        foreach (ComputadoraPortatil portatil in portatiles)
                        {
                            if (portatil.Codigo == codigoEquipo) { equipoSeleccionado = portatil; break; }
                        }
                        break;
                    case 2:
                        foreach (ComputadoraDeEscritorio escritorio in escritorios)
                        {
                            if (escritorio.Codigo == codigoEquipo) { equipoSeleccionado = escritorio; break; }
                        }
                        break;
                    case 3:
                        foreach (Impresora impresora in impresoras)
                        {
                            if (impresora.Codigo == codigoEquipo) { equipoSeleccionado = impresora; break; }
                        }
                        break;
                }
                if (equipoSeleccionado == null) MostrarErrorAnimado("Equipo no encontrado.");
            }
            if (ExisteSolicitudActiva(solicitudes, equipoSeleccionado))
            {
                throw new ArgumentException("El equipo ya tiene una solicitud activa.");
            }
            Console.Clear();
            string descripcion = LeerTextoSolicitud("Ingrese descripción del problema: ", 10);
            int opcionPrioridad = ValidarEntradasInt("Prioridad:\n1. Normal\n2. Urgente\nSeleccione una opción: ", 1, 2, "Opción no válida.");
            string prioridad = "Normal";
            if (opcionPrioridad == 2) prioridad = "Urgente";

            List<string> especialidades = new List<string>();
            foreach (Tecnico tecnico in tecnicos)
            {
                if (!especialidades.Contains(tecnico.Especialidad))
                {
                    especialidades.Add(tecnico.Especialidad);
                }
            }
            string menuEspecialidades = "Especialidad requerida:\n";
            for (int i = 0; i < especialidades.Count; i++)
            {
                menuEspecialidades += (i + 1) + ". " + especialidades[i] + "\n";
            }
            menuEspecialidades += "0. Cancelar\nSeleccione una opción: ";
            int opcionEspecialidad = ValidarEntradasInt(menuEspecialidades, 0, especialidades.Count, "Especialidad no válida.");
            if (opcionEspecialidad == 0) return;
            string especialidad = especialidades[opcionEspecialidad - 1];
            int confirmar = ValidarEntradasInt("Resumen:\nCliente: " + clienteSeleccionado.NombreCompleto +
                "\nEquipo: " + equipoSeleccionado.Marca + " " + equipoSeleccionado.Modelo +
                "\nSerie: " + equipoSeleccionado.NumeroSerie + "\nProblema: " + descripcion +
                "\nPrioridad: " + prioridad + "\nEspecialidad: " + especialidad +
                "\n\n¿Registrar solicitud?\n1. Sí\n2. No\nSeleccione una opción: ", 1, 2, "Opción no válida.");
            if (confirmar == 2) return;
            Solicitud nueva = GuardarSolicitud(solicitudes, clienteSeleccionado, equipoSeleccionado, prioridad, descripcion, especialidad, normales, urgentes);
            Console.WriteLine("Solicitud SOL-" + nueva.Codigo + " registrada en la cola " + prioridad + ".");
            Console.ReadKey();
        }

        static Solicitud SeleccionarSolicitud(List<Solicitud> solicitudes)
        {
            if (solicitudes.Count == 0)
            {
                throw new ArgumentException("No hay solicitudes registradas.");
            }
            int codigo = ValidarEntradasInt("Ingrese código de solicitud: SOL- (0 para regresar): ", 0, int.MaxValue, "Código no válido.");
            if (codigo == 0) return null;
            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.Codigo == codigo) return solicitud;
            }
            throw new ArgumentException("Solicitud no encontrada.");
        }

        static void ConsultarSolicitudes(List<Solicitud> solicitudes)
        {
            int opcion = ValidarEntradasInt("Consultar solicitudes:\n1. Buscar por código\n2. Mostrar todas\n3. Regresar\nSeleccione una opción: ", 1, 3, "Opción no válida.");
            if (opcion == 3) return;
            Console.Clear();
            if (opcion == 1)
            {
                Solicitud solicitud = SeleccionarSolicitud(solicitudes);
                if (solicitud == null) return;
                solicitud.MostrarInformacion();
            }
            else
            {
                if (solicitudes.Count == 0) Console.WriteLine("No hay solicitudes registradas.");
                foreach (Solicitud solicitud in solicitudes)
                {
                    solicitud.MostrarInformacion();
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }

        static void RegistrarDiagnosticoSolicitud(List<Solicitud> solicitudes)
        {
            Solicitud solicitud = SeleccionarSolicitud(solicitudes);
            if (solicitud == null) return;
            if (solicitud.Estado != "En diagnóstico" && solicitud.Estado != "En reparación")
            {
                throw new ArgumentException("La solicitud debe estar en diagnóstico o en reparación.");
            }
            int opcion;
            do
            {
                opcion = ValidarEntradasInt("Diagnóstico y acciones:\n1. Registrar diagnóstico\n2. Registrar acción realizada\n3. Regresar\nSeleccione una opción: ", 1, 3, "Opción no válida.");
                if (opcion == 1)
                {
                    solicitud.RegistrarDiagnostico(LeerTextoSolicitud("Ingrese diagnóstico: ", 5));
                    Console.WriteLine("Diagnóstico registrado correctamente.");
                    Console.ReadKey();
                }
                else if (opcion == 2)
                {
                    solicitud.RegistrarAccion(LeerTextoSolicitud("Ingrese acción realizada: ", 3));
                    Console.WriteLine("Acción registrada correctamente.");
                    Console.ReadKey();
                }
            } while (opcion != 3);
        }

        static void CambiarEstadoSolicitud(List<Solicitud> solicitudes)
        {
            Solicitud solicitud = SeleccionarSolicitud(solicitudes);
            if (solicitud == null) return;
            string siguiente = solicitud.ObtenerSiguienteEstado();
            if (solicitud.Estado == "En espera")
            {
                throw new ArgumentException("Utilice Atención de colas para asignar la siguiente solicitud.");
            }
            if (siguiente == "") throw new ArgumentException("La solicitud ya está cerrada.");
            int confirmar = ValidarEntradasInt("Estado actual: " + solicitud.Estado + "\nSiguiente estado: " + siguiente +
                "\n1. Confirmar cambio\n2. Regresar\nSeleccione una opción: ", 1, 2, "Opción no válida.");
            if (confirmar == 2) return;
            solicitud.CambiarEstado(siguiente);
            Console.WriteLine("Estado actualizado: " + solicitud.Estado);
            Console.ReadKey();
        }

        static void RegistrarCostosSolicitud(List<Solicitud> solicitudes, List<Repuesto> repuestos)
        {
            Solicitud solicitud = SeleccionarSolicitud(solicitudes);
            if (solicitud == null) return;
            if (solicitud.Estado != "En reparación")
            {
                throw new ArgumentException("La solicitud debe estar en reparación para registrar repuestos y mano de obra.");
            }
            int opcion;
            do
            {
                opcion = ValidarEntradasInt("Repuestos y costos:\n1. Agregar repuesto utilizado\n2. Registrar costo de mano de obra\n3. Regresar\nSeleccione una opción: ", 1, 3, "Opción no válida.");
                try
                {
                    if (opcion == 1)
                    {
                        if (repuestos.Count == 0) throw new ArgumentException("No hay repuestos registrados.");
                        foreach (Repuesto repuesto in repuestos)
                        {
                            Console.WriteLine("REP-" + repuesto.Codigo + " " + repuesto.Nombre + " | Existencia: " + repuesto.Existencia + " | Precio: " + repuesto.PrecioVenta);
                        }
                        Console.WriteLine("Presione una tecla para ingresar el código.");
                        Console.ReadKey();
                        int codigo = ValidarEntradasInt("Código de repuesto (0 para regresar): ", 0, int.MaxValue, "Código no válido.");
                        if (codigo == 0) continue;
                        Repuesto seleccionado = null;
                        foreach (Repuesto repuesto in repuestos)
                        {
                            if (repuesto.Codigo == codigo) { seleccionado = repuesto; break; }
                        }
                        if (seleccionado == null) throw new ArgumentException("Repuesto no encontrado.");
                        if (seleccionado.Existencia == 0) throw new ArgumentException("El repuesto está agotado.");
                        int cantidad = ValidarEntradasInt("Cantidad utilizada (0 para regresar): ", 0, seleccionado.Existencia, "No hay existencia suficiente.");
                        if (cantidad == 0) continue;
                        solicitud.RegistrarRepuesto(seleccionado, cantidad);
                        Console.WriteLine("Repuesto registrado. Existencia restante: " + seleccionado.Existencia);
                        if (seleccionado.Existencia <= seleccionado.ExistenciaMinima)
                        {
                            Console.WriteLine("Aviso: el repuesto alcanzó o está por debajo de su existencia mínima.");
                        }
                    }
                    else if (opcion == 2)
                    {
                        double costo = ValidarEntradasDouble("Costo de mano de obra: ", 0, double.MaxValue, "El costo debe ser válido y no negativo.");
                        solicitud.RegistrarManoObra(costo);
                        Console.WriteLine("Mano de obra registrada. Total: " + solicitud.Total);
                    }
                    if (opcion != 3) Console.ReadKey();
                }
                catch (ArgumentException error)
                {
                    MostrarErrorAnimado(error.Message);
                    Console.ReadKey();
                }
            } while (opcion != 3);
        }

        static void GestionarColasSolicitudes(List<Solicitud> solicitudes, List<Tecnico> tecnicos, Queue<Solicitud> normales, Queue<Solicitud> urgentes)
        {
            int opcionColas;
            do
            {
                SincronizarColasSolicitudes(solicitudes, normales, urgentes);
                opcionColas = ValidarEntradasInt("============================================\n ATENCIÓN DE COLAS\n============================================\n1. Consultar colas por prioridad\n2. Consultar siguiente solicitud\n3. Atender siguiente solicitud\n4. Regresar al menú principal\n\nSeleccione una opción: ", 1, 4, "Opción no válida.");
                try
                {
                    switch (opcionColas)
                    {
                        case 1:
                            Console.Clear();
                            MostrarColasSolicitudes(normales, urgentes);
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Clear();
                            MostrarSiguienteSolicitud(normales, urgentes, tecnicos);
                            Console.ReadKey();
                            break;
                        case 3:
                            AtenderSiguienteSolicitud(solicitudes, tecnicos, normales, urgentes);
                            break;
                        case 4:
                            Console.Clear();
                            break;
                    }
                }
                catch (ArgumentException error)
                {
                    MostrarErrorAnimado(error.Message);
                    Console.ReadKey();
                }
            } while (opcionColas != 4);
        }

        static void MostrarColasSolicitudes(Queue<Solicitud> normales, Queue<Solicitud> urgentes)
        {
            Console.WriteLine("Solicitudes en espera: " + (urgentes.Count + normales.Count));
            Console.WriteLine("Se atienden primero las urgentes y después las normales.");
            Console.WriteLine("Dentro de cada cola se respeta el orden de llegada.");
            Console.WriteLine();
            MostrarColaSolicitudes(urgentes, "URGENTES");
            Console.WriteLine();
            MostrarColaSolicitudes(normales, "NORMALES");
        }

        static void MostrarColaSolicitudes(Queue<Solicitud> cola, string nombre)
        {
            Console.WriteLine(nombre + ": " + cola.Count);
            if (cola.Count == 0)
            {
                Console.WriteLine("No hay solicitudes en esta cola.");
            }
            int posicion = 1;
            foreach (Solicitud solicitud in cola)
            {
                Console.WriteLine(posicion + ". SOL-" + solicitud.Codigo +
                    " | Cliente: " + solicitud.Cliente.NombreCompleto +
                    " | Especialidad: " + solicitud.EspecialidadRequerida +
                    " | Recepción: " + solicitud.FechaRecepcion.ToString("dd/MM/yyyy HH:mm:ss"));
                posicion++;
            }
        }

        static void MostrarSiguienteSolicitud(Queue<Solicitud> normales, Queue<Solicitud> urgentes, List<Tecnico> tecnicos)
        {
            Solicitud solicitud = ObtenerSiguienteSolicitud(normales, urgentes);
            if (solicitud == null)
            {
                Console.WriteLine("No hay solicitudes en espera.");
                return;
            }
            Console.WriteLine("Siguiente solicitud: SOL-" + solicitud.Codigo);
            Console.WriteLine("Prioridad: " + solicitud.Prioridad);
            Console.WriteLine("Cliente: " + solicitud.Cliente.NombreCompleto);
            Console.WriteLine("Equipo: " + solicitud.Equipo.Marca + " " + solicitud.Equipo.Modelo);
            Console.WriteLine("Especialidad requerida: " + solicitud.EspecialidadRequerida);
            Console.WriteLine("Descripción: " + solicitud.DescripcionProblema);
            Tecnico tecnico = BuscarTecnicoDisponible(tecnicos, solicitud);
            if (tecnico == null)
            {
                Console.WriteLine("Permanece en espera: no hay un técnico compatible con capacidad disponible.");
                Console.WriteLine("Se conserva su turno y el orden de atención.");
            }
            else
            {
                Console.WriteLine("Técnico disponible: TEC-" + tecnico.Codigo + " - " + tecnico.NombreCompleto);
                Console.WriteLine("Trabajos activos: " + tecnico.CantidadTrabajosActivos + "/" + Solicitud.LimiteTrabajosActivosTecnico);
            }
        }

        static void SincronizarColasSolicitudes(List<Solicitud> solicitudes, Queue<Solicitud> normales, Queue<Solicitud> urgentes)
        {
            normales.Clear();
            urgentes.Clear();
            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.Estado == "En espera")
                {
                    if (solicitud.Prioridad == "Urgente") urgentes.Enqueue(solicitud);
                    else normales.Enqueue(solicitud);
                }
            }
        }

        static Solicitud ObtenerSiguienteSolicitud(Queue<Solicitud> normales, Queue<Solicitud> urgentes)
        {
            if (urgentes.Count > 0) return urgentes.Peek();
            if (normales.Count > 0) return normales.Peek();
            return null;
        }

        static Tecnico BuscarTecnicoDisponible(List<Tecnico> tecnicos, Solicitud solicitud)
        {
            Tecnico seleccionado = null;
            foreach (Tecnico tecnico in tecnicos)
            {
                if (tecnico.EstadoDisponibilidad == "Disponible" &&
                    tecnico.Especialidad.Trim().ToLower() == solicitud.EspecialidadRequerida.ToLower() &&
                    tecnico.CantidadTrabajosActivos < Solicitud.LimiteTrabajosActivosTecnico)
                {
                    if (seleccionado == null || tecnico.CantidadTrabajosActivos < seleccionado.CantidadTrabajosActivos)
                    {
                        seleccionado = tecnico;
                    }
                }
            }
            return seleccionado;
        }

        static void AtenderSiguienteSolicitud(List<Solicitud> solicitudes, List<Tecnico> tecnicos, Queue<Solicitud> normales, Queue<Solicitud> urgentes)
        {
            SincronizarColasSolicitudes(solicitudes, normales, urgentes);
            Solicitud solicitud = ObtenerSiguienteSolicitud(normales, urgentes);
            if (solicitud == null) throw new ArgumentException("No hay solicitudes en espera.");
            Tecnico tecnico = BuscarTecnicoDisponible(tecnicos, solicitud);
            if (tecnico == null)
            {
                throw new ArgumentException("SOL-" + solicitud.Codigo + " permanece en espera: no hay un técnico disponible de " + solicitud.EspecialidadRequerida + " con capacidad. Se conserva su turno.");
            }
            int confirmar = ValidarEntradasInt("Atender SOL-" + solicitud.Codigo + " (" + solicitud.Prioridad + ")" +
                "\nEspecialidad: " + solicitud.EspecialidadRequerida + "\nTécnico: " + tecnico.NombreCompleto +
                "\nTrabajos activos: " + tecnico.CantidadTrabajosActivos + "/" + Solicitud.LimiteTrabajosActivosTecnico +
                "\n1. Asignar\n2. Regresar\nSeleccione una opción: ", 1, 2, "Opción no válida.");
            if (confirmar == 2) return;
            solicitud.AsignarTecnico(tecnico);
            if (solicitud.Prioridad == "Urgente") urgentes.Dequeue();
            else normales.Dequeue();
            Console.WriteLine("Solicitud asignada correctamente.");
            Console.ReadKey();
        }

        static void ActualizarInformacionEquipo<T>(List<T> tipoEquipo, string tipoEquipoMostrar, string abreviatura, List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras) where T : Equipo
        {
            while (true)
            {
                Console.Clear();
                int codigoEquipoBuscar = ValidarEntradasInt($"Ingrese codigo de {tipoEquipoMostrar} a buscar: {abreviatura}-", 1, int.MaxValue, "Codigo no valido");
                int indiceEquipo = -1;
                for (int i = 0; i < tipoEquipo.Count; i++)
                {
                    if (tipoEquipo[i].Codigo == codigoEquipoBuscar)
                    {
                        indiceEquipo = i;
                    }
                }
                if (indiceEquipo == -1)
                {
                    Console.Clear();
                    MostrarErrorAnimado("Codigo no encontrado");
                    break;

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Equipo encontrado");
                    Console.WriteLine();
                    tipoEquipo[indiceEquipo].MostrarInformacion(abreviatura);
                    Console.ReadKey();

                }
                Console.Clear();

                int tipoDatoActualizar = ValidarEntradasInt($"¿Que dato desea actualizar?\n\n1. Marca\n2. Modelo\n3. Estado fisico\n4. Numero de serie\n5. Accesorios\n6. Volver atras\n\nIngrese una opción: ", 1, 6, "Error: opción no valida");
                switch (tipoDatoActualizar)
                {
                    case 1:
                        Console.Write("Ingrese nueva marca:");
                        string nuevaMarca = LeerTextoSolicitud("", 2);
                        tipoEquipo[indiceEquipo].ActualizarMarca(nuevaMarca);
                        Console.Clear();
                        Console.WriteLine("Marca actualizada con exito!");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Ingrese nuevo modelo: ");
                        string nuevoModelo = LeerTextoSolicitud("", 3);
                        Console.Clear();
                        tipoEquipo[indiceEquipo].ActualiziarModelo(nuevoModelo);
                        Console.WriteLine("Modelo actualizado con exito!");
                        Console.ReadKey();
                        break;
                    case 3:
                        string nuevoEstadoFisico;
                        while (true)
                        {
                            int intNuevoEstadoFisico = ValidarEntradasInt("Estado fisico nuevo: \n\n1. Excelente\n2. Con daños leves\n3. Daños serevos\n\nSeleccione una opcion: ", 1, 3, "Error: opción no valida");
                            if (intNuevoEstadoFisico == 1)
                            {
                                nuevoEstadoFisico = "Excelente";
                                break;
                            }
                            else if (intNuevoEstadoFisico == 2)
                            {
                                nuevoEstadoFisico = "Con daños leves";
                                break;
                            }
                            else
                            {
                                nuevoEstadoFisico = "Daños severos";
                                break;
                            }
                        }
                        tipoEquipo[indiceEquipo].ActualizarEstadoFisico(nuevoEstadoFisico);
                        Console.Clear();
                        Console.WriteLine("Estado fisico cambiado con exito");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Ingrese nuevo numero de serie: ");
                        string nuevoNumeroSerie = LeerNumeroSerieUnico(portatiles, escritorios, impresoras, tipoEquipo[indiceEquipo]);
                        tipoEquipo[indiceEquipo].ActualizarNumeroSerie(nuevoNumeroSerie);
                        Console.Clear();
                        Console.WriteLine("Número de serie actualizado con exito!");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Write("Ingrese nuevos accesorios: ");
                        string nuevosAccesorios = LeerTextoSolicitud("", 3);
                        Console.Clear();
                        tipoEquipo[indiceEquipo].ActualizarAccesorios(nuevosAccesorios);
                        Console.WriteLine("Nuevos accesorios agregados exitosamente!");
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        break;

                }
                break;
            }

        }

        static string LeerEntrada()
        {
            string entrada = Console.ReadLine();
            if (entrada == null)
            {
                throw new OperationCanceledException("La entrada se ha cerrado.");
            }
            return entrada;
        }

        static bool ValidarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;
            correo = correo.Trim();
            int arroba = correo.IndexOf('@');
            return arroba > 0 && arroba == correo.LastIndexOf('@') && arroba < correo.Length - 1 && !correo.Contains(" ");
        }

        static Cliente SeleccionarPropietarioEquipo(List<Cliente> clientes)
        {
            if (clientes.Count == 0)
            {
                throw new ArgumentException("Registre un cliente antes de registrar el equipo.");
            }
            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine("CLI-" + cliente.Codigo + " " + cliente.NombreCompleto);
            }
            while (true)
            {
                int codigo = ValidarEntradasInt("Código del propietario (0 para cancelar): CLI-", 0, int.MaxValue, "Código no válido.");
                if (codigo == 0) return null;
                foreach (Cliente cliente in clientes)
                {
                    if (cliente.Codigo == codigo) return cliente;
                }
                MostrarErrorAnimado("Propietario no encontrado.");
            }
        }

        static string LeerNumeroSerieUnico(List<ComputadoraPortatil> portatiles, List<ComputadoraDeEscritorio> escritorios, List<Impresora> impresoras, Equipo equipoActual)
        {
            while (true)
            {
                string serie = LeerTextoSolicitud("Número de serie: ", 3);
                bool repetida = false;
                foreach (ComputadoraPortatil equipo in portatiles)
                {
                    if (equipo != equipoActual && string.Equals(equipo.NumeroSerie, serie, StringComparison.OrdinalIgnoreCase)) repetida = true;
                }
                foreach (ComputadoraDeEscritorio equipo in escritorios)
                {
                    if (equipo != equipoActual && string.Equals(equipo.NumeroSerie, serie, StringComparison.OrdinalIgnoreCase)) repetida = true;
                }
                foreach (Impresora equipo in impresoras)
                {
                    if (equipo != equipoActual && string.Equals(equipo.NumeroSerie, serie, StringComparison.OrdinalIgnoreCase)) repetida = true;
                }
                if (!repetida) return serie;
                MostrarErrorAnimado("La serie ya pertenece a otro equipo.");
            }
        }

        static void MostrarAvisoExistencia(Repuesto repuesto)
        {
            if (repuesto.Existencia <= repuesto.ExistenciaMinima)
            {
                Console.WriteLine("AVISO: REP-" + repuesto.Codigo + " está en el mínimo o agotado. Existencia: " + repuesto.Existencia);
            }
        }

        static int ValidarEntradasInt(string mensaje, int min, int max, string mensajeError)
        {
            int valor;
            bool esValido;
            do
            {
                Console.Clear();
                Console.Write(mensaje);
                string entrada = LeerEntrada();
                esValido = int.TryParse(entrada, out valor);
                if (!esValido)
                {

                    MostrarErrorAnimado("Por favor ingrese un caracter valido!");
                    Console.Write(mensaje);
                }
                else if (valor < min || valor > max)
                {
                    esValido = false;
                    MostrarErrorAnimado(mensajeError);
                    Console.Write(mensaje);
                }
            } while (!esValido);
            return valor;
        }
        static double ValidarEntradasDouble(string mensaje, double min, double max, string mensajeError)
        {
            double valor;
            bool esValido;
            do
            {
                Console.Clear();
                Console.Write(mensaje);
                string entrada = LeerEntrada();
                esValido = double.TryParse(entrada, out valor);
                if (!esValido)
                {

                    MostrarErrorAnimado("Por favor ingrese un caracter valido!");
                    Console.Write(mensaje);
                }
                else if (valor < min || valor > max || double.IsNaN(valor))
                {
                    esValido = false;
                    MostrarErrorAnimado(mensajeError);
                    Console.Write(mensaje);
                }
            } while (!esValido);
            return valor;
        }
        static void MostrarErrorAnimado(string textoError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(textoError);
            Thread.Sleep(1400);
            Console.Write("\r" + new string(' ', textoError.Length) + "\r");
            Console.ResetColor();
        }
        static void MostrarExitoAnimado(string mensajeExito)
        {
            Console.ForegroundColor = ConsoleColor.Green;



            Console.Write($" {mensajeExito}");

            Thread.Sleep(2000);

            Console.Write("\r" + new string(' ', mensajeExito.Length + 5) + "\r");

            Console.ResetColor();
        }
        static void MostrarAnimacionPuntos()
        {
            Console.Write("Procesando y guardando datos");

            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                Thread.Sleep(400);
            }
            Console.Clear();
            MostrarExitoAnimado("Registro completado con éxito!");
        }
        static bool ValidarTelefono(string numeroTelefono)
        {
            if (numeroTelefono == null || numeroTelefono.Length != 8)
            {
                return false;
            }
            foreach (char c in numeroTelefono)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
