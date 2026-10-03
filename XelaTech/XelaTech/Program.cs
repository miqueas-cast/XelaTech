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


            // Datos de prueba
            Clientes.Add(new Cliente(1, "Juan Perez", "12345678", "juan.perez@example.com", "Estudiante"));
            Clientes.Add(new Cliente(2, "Maria Lopez", "87654321", "maria.lopez@example.com", "Institucional"));
            Tecnicos.Add(new Tecnico(1, "Luis Ramirez", "Redes", "Disponible"));
            Tecnicos.Add(new Tecnico(2, "Ana Torres", "Soporte", "Disponible"));
            ComputadorasPortatiles.Add(new ComputadoraPortatil(1, "HP.", "Victus", "Con daños leves", "Cargador", "154145546"));
            ComputadorasPortatiles.Add(new ComputadoraPortatil(2, "Dell", "Prisma", "Con daños leves", "Cargador", "15465666"));
            ComputadorasDeEscritorio.Add(new ComputadoraDeEscritorio(1, "Lenovo", "ThinkCentre", "Con daños leves", "Cargador", "154145546"));
            ComputadorasDeEscritorio.Add(new ComputadoraDeEscritorio(2, "Acer", "Aspire", "Con daños leves", "Cargador", "15465666"));
            Impresoras.Add(new Impresora(1, "Canon", "Pixma", "Con daños leves", "Cargador", "154145546"));
            Impresoras.Add(new Impresora(2, "Epson", "EcoTank", "Con daños leves", "Cargador", "15465666"));





            int opcionMenu;
            do
            {
                Console.Clear();
                opcionMenu = ValidarEntradasInt("============================================\r\n XELATECH SOPORTE\r\n============================================\r\n1. Gestión de clientes\r\n2. Gestión de técnicos\r\n3. Gestión de equipos\r\n4. Gestión de solicitudes\r\n5. Gestión de repuestos\r\n6. Atención de colas\r\n7. Ordenamientos\r\n8. Búsquedas\r\n9. Reportes\r\n10. Generar datos de prueba\r\n11. Salir\n\nSelecciona una opción: ", 1, 11, "Error: Opción invlida");
                switch(opcionMenu)
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
                                    Console.WriteLine("");
                                    int codigoCliente = Clientes.Count + 1;
                                    Console.Write("Ingrese nombre: ");
                                    string nombre = Console.ReadLine();

                                    Console.Clear();
                                    string numeroTelefono;
                                    while (true)
                                    {
                                        Console.Write("Ingrese número de telefono: ");
                                        numeroTelefono = Console.ReadLine();
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
                                        correoElectronico = Console.ReadLine();
                                        if (correoElectronico.Contains('@'))
                                        {
                                            break;
                                        }
                                    }
                                    Console.Clear();
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Seleccione tipo de cliente: ");
                                        int tipoCliente = ValidarEntradasInt("Seleccione tipo cliente: \n\n1. Estudiantes\n2. Profesionales\n3. Pequeños negocios\n4. Instituciones\n\nSeleccione una opción: ", 1, 3, "Error: Opción invalida");
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
                                                    int confirmacionCli = ValidarEntradasInt($"Codigo: CLI-{codigoCliente}\nNombre: {nombre}\nNumero de telefono: {numeroTelefono}\nCorreo electronico: {correoElectronico}\nTipo de cliente: Instituciones\n\n¿Esta seguro que desea registrar CLI-{codigoCliente}?\n\n1. SI\n2. NO\n\nSeleccione una opción: ", 1, 2, "Error: opción no valida");
                                                    if (confirmacionCli == 1)
                                                    {
                                                        Clientes.Add(new Cliente(codigoCliente, nombre, numeroTelefono, correoElectronico, "Pequeño negocio"));
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
                                        switch(opcionActualizar)
                                        {
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine("Actualizar nombre");
                                                Console.Write("Ingrese el nuevo nombre: ");
                                                string nuevoNombre = Console.ReadLine();
                                                Console.Clear();
                                                Clientes[indiceCodigoClienteCase3].ActualizarNombre(nuevoNombre);
                                                Console.WriteLine("Nombre actualizado correctamente.");
                                                Console.ReadKey();
                                                break;
                                            case 2:
                                                Console.Clear();
                                                string numeroTelefonoNuevo;
                                                while (true)
                                                {
                                                    Console.Write("Ingrese número de telefono: ");
                                                    numeroTelefonoNuevo = Console.ReadLine();
                                                    if (ValidarTelefono(numeroTelefonoNuevo))
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        MostrarErrorAnimado("Error: el número de telefono debe contener solo número y una longitud de 8 caracteres");
                                                    }
                                                }
                                                Clientes[indiceCodigoClienteCase3].ActualizarNumeroTelefono(numeroTelefonoNuevo);
                                                Console.WriteLine("Número de teléfono actualizado correctamente.");
                                                Console.ReadKey();
                                                break;
                                            case 3:
                                                Console.Clear();
                                                string correoElectronicoNuevo;
                                                while (true)
                                                {
                                                    Console.Write("Ingrese dirección de correo electronico: ");
                                                    correoElectronicoNuevo = Console.ReadLine();
                                                    if (correoElectronicoNuevo.Contains('@'))
                                                    {
                                                        break;
                                                    }
                                                }

                                                Clientes[indiceCodigoClienteCase3].ActualizarCorreoElectronico(correoElectronicoNuevo);
                                                Console.WriteLine("Dirección de correo electrónico actualizada correctamente.");
                                                Console.ReadKey();
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
                                                            Clientes[indiceCodigoClienteCase3].ActualizarTipoCliente("Estudiante");
                                                            Console.WriteLine("Tipo de cliente actualizado correctamente.");
                                                            Console.ReadKey();
                                                            break;
                                                        case 2:
                                                            Clientes[indiceCodigoClienteCase3].ActualizarTipoCliente("Profesional");
                                                            Console.WriteLine("Tipo de cliente actualizado correctamente.");
                                                            Console.ReadKey();
                                                            break;
                                                        case 3:
                                                            Clientes[indiceCodigoClienteCase3].ActualizarTipoCliente("Pequeños negocios");
                                                            Console.WriteLine("Tipo de cliente actualizado correctamente.");
                                                            Console.ReadKey();
                                                            break;
                                                        case 4:
                                                            Clientes[indiceCodigoClienteCase3].ActualizarTipoCliente("Institución");
                                                            Console.WriteLine("Tipo de cliente actualizado correctamente.");
                                                            Console.ReadKey();
                                                            break;
                                                    }
                                                    break;
                                                }

                                                break;
                                            case 5:
                                                Console.Clear();
                                                break;
                                        }

                                    }while(opcionActualizar != 5);
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
                                    int codigoTecnicos = Tecnicos.Count + 1;
                                    Console.Write("Ingrese nombre: ");
                                    string nombreTecnico = Console.ReadLine();
                                    Console.Clear();
                                    string especialidad;

                                    while (true)
                                    {
                                        int opcionEspecialidad = ValidarEntradasInt("Escoga tipo de especialidad: \n\n1. Uno\n2. dos\n3. tres", 1, 2, "Error: Opción no valida");
                                        if (opcionEspecialidad == 1) 
                                        {
                                            especialidad = "Uno";
                                            break;
                                        }
                                        else if(opcionEspecialidad == 2)
                                        {
                                            especialidad = "Dos";
                                            break;
                                        }
                                        else
                                        {
                                            especialidad = "Tres";
                                            break;
                                        }
                                    }
                                    Console.Clear();
                                    string estadoDisponibilidad;
                                    while (true)
                                    {
                                        int opcionDisponibilidad = ValidarEntradasInt("Escoga disponibilidad: \n\n1. Disponible\n2. No disponible\n\nIngrese una opción: ", 1, 2, "Error: Opción no valida");
                                        if(opcionDisponibilidad == 1)
                                        {
                                            estadoDisponibilidad = "Disponible";
                                            break;
                                        }
                                        else
                                        {
                                            estadoDisponibilidad = "No disponible";
                                            break;
                                        }

                                    }
                                    while (true)
                                    {
                                        int confirmacionTec = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: TEC-{codigoTecnicos}\nNombre: {nombreTecnico}\nEspecialidad: {especialidad}\nEstado disponibildad: {estadoDisponibilidad}\n\n¿Esta seguro de registrar REP-{codigoTecnicos}?\n\n1. SI\n2. NO", 1, 2, "Error: Opción no valida");
                                        if(confirmacionTec == 1)
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
                                        opcionCase2Tec = ValidarEntradasInt("1. Consultar repartidor por codigo\n2. Ver todos los tecnicos registrados\n3. Volver atras\n\nEliga una opción: ", 1, 3, "Error: opción no valida");
                                        switch (opcionCase2Tec)
                                        {
                                            case 1:
                                                Console.Clear();
                                                int codigoBuscarTec = ValidarEntradasInt("Ingrese codigo de tecnico a buscar: TEC-", 1, int.MaxValue, "Cliente no encontrado");
                                                int indiceCodigoTec = -1;
                                                for(int i = 0; i < Tecnicos.Count; i++)
                                                {
                                                    if (Tecnicos[i].Codigo == codigoBuscarTec)
                                                    {
                                                        indiceCodigoTec = i;
                                                    }
                                                }
                                                if (indiceCodigoTec == -1)
                                                {
                                                    Console.WriteLine("Cliente no encontrado");
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Tecnicos[indiceCodigoTec].MostrarInformacion();
                                                    Console.ReadKey();
                                                }

                                                break;
                                            case 2:
                                                if(Tecnicos.Count == 0)
                                                {
                                                    MostrarErrorAnimado("Sin tecnicos registrados");
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Clientes regisrados");
                                                    Console.WriteLine();
                                                    foreach(Tecnico tecnico in Tecnicos)
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
                                    int codigoBuscarTecCase3 = ValidarEntradasInt("Ingrese codigo de cliente a buscar: CLI-", 1, int.MaxValue, "Error: codigo no valido!");
                                    int indiceBuscarTecCase3 = -1;
                                    for (int i = 0; i < Clientes.Count; i++)
                                    {
                                        if (Clientes[i].Codigo == codigoBuscarTecCase3)
                                        {
                                            indiceBuscarTecCase3 = i;
                                            break;
                                        }
                                    }
                                    if (indiceBuscarTecCase3 == -1)
                                    {
                                        MostrarErrorAnimado("Tecnico no encontrado no encontrado");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Tecnico encontrado");
                                        Console.WriteLine();
                                        Clientes[indiceBuscarTecCase3].MostrarInformacion();
                                        Console.WriteLine();
                                        Console.WriteLine("Presione una tecla para continuar...");
                                        Console.ReadKey();
                                    }
                                    int opcionActualizarTec;
                                    do
                                    {
                                        opcionActualizarTec = ValidarEntradasInt("1. Actualizar nombre\n2. Actualizar especialidad\n3. Regresar al menú anterior\n\nSeleccione una opción: ", 1, 3, "Error: opción no valida");
                                        switch (opcionActualizarTec)
                                        {
                                            case 1:
                                                Console.Clear();
                                                Console.Write("Ingrese nuevo nombre");
                                                string nuevoNombreTec = Console.ReadLine();
                                                Tecnicos[indiceBuscarTecCase3].ActualizarNombre(nuevoNombreTec);
                                                Console.Clear();
                                                Console.WriteLine("Nombre actualizado correctamente.");
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Console.Write("Ingrese nueva especialidad");
                                                string nuevaEspecialidadTec = Console.ReadLine();
                                                Tecnicos[indiceBuscarTecCase3].ActualizarEspecialidad(nuevaEspecialidadTec);
                                                Console.Clear();
                                                Console.WriteLine("Especialidad actualizada correctamente.");
                                                Console.ReadKey();
                                                break;
                                            case 3:
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
                        int codigoComputadoraPortatil = ComputadorasPortatiles.Count + 1;
                        int codigoComputadoraDeEscritorio = ComputadorasDeEscritorio.Count + 1;
                        int codigoImpresora = Impresoras.Count + 1;

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
                                                    Console.Clear();
                                                    Console.Write("Ingrese numero de serie: ");
                                                    string numeroSerie = Console.ReadLine();
                                                    Console.Clear();
                                                    Console.WriteLine("Ingrese marca: ");
                                                    string marca = Console.ReadLine();
                                                    Console.Clear();
                                                    Console.WriteLine("Ingrese modelo: ");
                                                    string modelo = Console.ReadLine();
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
                                                    string accesoriosEntregados = Console.ReadLine();
                                                    Console.Clear();
                                                    while (true)
                                                    {
                                                        int resumenCportatil = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: {codigoComputadoraPortatil}\nNumero de serie: {numeroSerie}\nMarca: {marca}\nModelo: {modelo}\nEstado Fisico: {estadoFisico}\nAccesorios: {accesoriosEntregados}\n\n¿Esta seguro de registrar este equipo?\n1. SI\n2. NO\n\nIngrese una opción:", 1, 2, "Error: opción no valida");
                                                        if(resumenCportatil == 1)
                                                        {
                                                            Console.Clear();
                                                            ComputadorasPortatiles.Add(new ComputadoraPortatil(codigoComputadoraPortatil, marca, modelo, estadoFisico, accesoriosEntregados, numeroSerie));
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
                                                    Console.Clear();
                                                    Console.Write("Ingrese numero de serie: ");
                                                    string numeroSerie = Console.ReadLine();
                                                    Console.Clear();
                                                    Console.WriteLine("Ingrese marca: ");
                                                    string marca = Console.ReadLine();
                                                    Console.Clear();
                                                    Console.WriteLine("Ingrese modelo: ");
                                                    string modelo = Console.ReadLine();
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
                                                    string accesoriosEntregados = Console.ReadLine();
                                                    Console.Clear();
                                                    while (true)
                                                    {
                                                        int resumenCEscritorio = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: {codigoComputadoraDeEscritorio}\nNumero de serie: {numeroSerie}\nMarca: {marca}\nModelo: {modelo}\nEstado Fisico: {estadoFisico}\nAccesorios: {accesoriosEntregados}\n\n¿Esta seguro de registrar este equipo?\n1. SI\n2. NO\n\nIngrese una opción:", 1, 2, "Error: opción no valida");
                                                        if (resumenCEscritorio == 1)
                                                        {
                                                            Console.Clear();
                                                            ComputadorasDeEscritorio.Add(new ComputadoraDeEscritorio(codigoComputadoraDeEscritorio, marca, modelo, estadoFisico, accesoriosEntregados, numeroSerie));
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
                                                    Console.Clear();
                                                    Console.Write("Ingrese numero de serie: ");
                                                    string numeroSerie = Console.ReadLine();
                                                    Console.Clear();
                                                    Console.WriteLine("Ingrese marca: ");
                                                    string marca = Console.ReadLine();
                                                    Console.Clear();
                                                    Console.WriteLine("Ingrese modelo: ");
                                                    string modelo = Console.ReadLine();
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
                                                    string accesoriosEntregados = Console.ReadLine();
                                                    Console.Clear();
                                                    while (true)
                                                    {
                                                        int resumenImpresora = ValidarEntradasInt($"Resumen de datos: \n\nCodigo: {codigoImpresora}\nNumero de serie: {numeroSerie}\nMarca: {marca}\nModelo: {modelo}\nEstado Fisico: {estadoFisico}\nAccesorios: {accesoriosEntregados}\n\n¿Esta seguro de registrar este equipo?\n1. SI\n2. NO\n\nIngrese una opción:", 1, 2, "Error: opción no valida");
                                                        if (resumenImpresora == 1)
                                                        {
                                                            Console.Clear();
                                                            Impresoras.Add(new Impresora(codigoImpresora, marca, modelo, estadoFisico, accesoriosEntregados, numeroSerie));
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
                                                    if(opcionBusquedaPortatil == 1)
                                                    {
                                                        int codigoPortatilBuscar = ValidarEntradasInt("Ingrese codigo de portatil a buscar: PORT-", 1, int.MaxValue, "Codigo no valido");
                                                        int indicePortatilBuscar = -1;
                                                        for(int i = 0; i < ComputadorasPortatiles.Count; i++)
                                                        {
                                                            if (ComputadorasPortatiles[i].Codigo == codigoPortatilBuscar)
                                                            {
                                                                indicePortatilBuscar = i;
                                                            }
                                                        }
                                                        if(indicePortatilBuscar == -1)
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
                                                    else if(opcionBusquedaPortatil == 2)
                                                    {
                                                        if(ComputadorasPortatiles.Count != 0)
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Computadoras portatiles ingresadas: ");
                                                            foreach(ComputadoraPortatil portatil in ComputadorasPortatiles)
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
                                                ActualizarInformacionEquipo(ComputadorasPortatiles, "computadora portatil", "PORT");
                                                break;
                                            case 2:
                                                Console.Clear();
                                                ActualizarInformacionEquipo(ComputadorasDeEscritorio, "computadora de escritorio", "DESK");
                                                break;
                                            case 3:
                                                Console.Clear();
                                                ActualizarInformacionEquipo(Impresoras, "impresora", "IMP");
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
                        Console.Clear();

                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();

                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();

                        Console.ReadKey();
                        break;
                    case 7:
                        Console.Clear();

                        Console.ReadKey();
                        break;
                    case 8:
                        Console.Clear();

                        Console.ReadKey();
                        break;
                    case 9:
                        Console.Clear();

                        Console.ReadKey();
                        break;
                    case 10:
                        Console.Clear();

                        Console.ReadKey();
                        break;
                    case 11:
                        Console.Clear();

                        Console.ReadKey();
                        break;
                    default:
                        break;
                }


            } while( opcionMenu !=11);
            
        }
        static void ActualizarInformacionEquipo<T>(List<T> tipoEquipo, string tipoEquipoMostrar, string abreviatura) where T : Equipo
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
                        string nuevaMarca = Console.ReadLine();
                        tipoEquipo[indiceEquipo].ActualizarMarca(nuevaMarca);
                        Console.Clear();
                        Console.WriteLine("Marca actualizada con exito!");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Ingrese nuevo modelo: ");
                        string nuevoModelo = Console.ReadLine();
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
                        string nuevoNumeroSerie = Console.ReadLine();
                        tipoEquipo[indiceEquipo].ActualizarNumeroSerie(nuevoNumeroSerie);
                        Console.Clear();
                        Console.WriteLine("Número de serie actualizado con exito!");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Write("Ingrese nuevos accesorios: ");
                        string nuevosAccesorios = Console.ReadLine();
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
        static int ValidarEntradasInt(string mensaje, int min, int max, string mensajeError)
        {
            int valor;
            bool esValido;
            do
            {
                Console.Clear();
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                esValido = int.TryParse(entrada, out valor);
                if (!esValido)
                {

                    MostrarErrorAnimado("Por favor ingrese un caracter valido!");
                    Console.Write(mensaje);
                }
                else if (valor < min || valor >max)
                {
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
        static bool ValidarTelefono(string numeroTelefono)
        {
            if (numeroTelefono.Length != 8)
            {
                return false;
            }
            foreach (char c in numeroTelefono)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
