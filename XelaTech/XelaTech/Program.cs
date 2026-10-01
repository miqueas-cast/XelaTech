using System;
using System.Collections.Generic;
using System.Linq;
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

                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();

                        Console.ReadKey();
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
