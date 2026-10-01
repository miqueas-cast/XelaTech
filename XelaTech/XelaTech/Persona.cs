using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XelaTech
{
	public class Persona
	{
		private int codigo;
		private string nombreCompleto;

		public string NombreCompleto
		{
			get { return nombreCompleto; }
			set
			{
				if (value == null || value.Length < 3)
				{
					throw new ArgumentException("El nombre completo debe tener al menos 3 caracteres.");
				}
				else
				{
					nombreCompleto = value;

				}
			}
		}

		public int Codigo
		{
			get { return codigo; }
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("El código debe ser un número positivo.");
				}
				else
				{
					codigo = value;
				}
			}
		}
		public Persona(int codigo, string nombre)
		{
			Codigo = codigo;
			nombreCompleto = nombre;
		}
		public virtual void MostrarInformacion()
		{
			
		}

	}
	public class Cliente : Persona
	{
		private string numeroTelefono;
		private string correoElectronico;
		private string tipoCliente;
		private int cantidadSolicitudes;

		public int CantidadSolicitudes
		{
			get { return cantidadSolicitudes; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("La cantidad de solicitudes no puede ser negativa.");
				}
				else
				{
					cantidadSolicitudes = value;
				}
			}
		}

		public string TipoCliente
		{
			get { return tipoCliente; }
			set
			{
				if (value == null || value.Length < 3)
				{
					throw new ArgumentException("El tipo de cliente debe tener al menos 3 caracteres.");
				}
				else
				{
					tipoCliente = value;
				}
			}
		}

		public string CorreoElectronicio
		{
			get { return correoElectronico; }
			set
			{
				if (value == null || !value.Contains("@"))
				{
					throw new ArgumentException("El correo electrónico no es válido.");
				}
				else
				{
					correoElectronico = value;
				}
			}
		}

		public string NumeroTelefono
		{
			get { return numeroTelefono; }
			set
			{
				if (value == null || value.Length < 8)
				{
					throw new ArgumentException("El número de teléfono debe tener al menos 8 dígitos.");
				}
				else
				{
					numeroTelefono = value;
				}
			}
		}
		public Cliente(int codigo, string nombre, string numeroTelefono, string correoElectronico, string tipoCliente)
			: base(codigo, nombre)
		{
			NumeroTelefono = numeroTelefono;
			CorreoElectronicio = correoElectronico;
			TipoCliente = tipoCliente;
			CantidadSolicitudes = 0;
		}
		public virtual void MostrarInformacion()
		{
            Console.WriteLine("Codigo: CLI-" + Codigo);
            Console.WriteLine("Nombre: " + NombreCompleto);
			Console.WriteLine("Número de teléfono: " + NumeroTelefono);
			Console.WriteLine("Correo electrónico: " + CorreoElectronicio);
			Console.WriteLine("Tipo de cliente: " + TipoCliente);
		}
		public void ActualizarNombre(string nuevoNombre)
		{
			NombreCompleto = nuevoNombre;
		}
		public void ActualizarNumeroTelefono(string nuevoNumeroTelefono)
		{
			NumeroTelefono = nuevoNumeroTelefono;
		}
		public void ActualizarCorreoElectronico(string nuevoCorreoElectronico)
		{
			CorreoElectronicio = nuevoCorreoElectronico;

		}
		public void ActualizarTipoCliente(string nuevoTipoCliente)
		{
			TipoCliente = nuevoTipoCliente;
		}
		public class Tecnico : Persona
		{
			private string especialidad;
			private string estadoDisponibilidad;
			private int cantidadTrabajosActivos;
			private int cantidadTrabajosFinalizados;
			private double calificacionPromedio;

			public double CalificacionPromedio
			{
				get { return calificacionPromedio; }
				set
				{
					if (value < 0 || value > 5)
					{
						throw new ArgumentException("La calificación promedio debe estar entre 0 y 5.");
					}
					else
					{
						calificacionPromedio = value;
					}
				}
			}

			public int CantidadTrabajosFinalizados
			{
				get { return cantidadTrabajosFinalizados; }
				set
				{
					if (value < 0)
					{
						throw new ArgumentException("La cantidad de trabajos finalizados no puede ser negativa.");
					}
					else
					{
						cantidadTrabajosFinalizados = value;
					}
				}
			}

			public int CantidadTrabajosActivos
			{
				get { return cantidadTrabajosActivos; }
				set
				{
					if (value < 0)
					{
						throw new ArgumentException("La cantidad de trabajos activos no puede ser negativa.");
					}
					else
					{
						cantidadTrabajosActivos = value;
					}
				}
			}

			public string EstadoDisponibilidad
			{
				get { return estadoDisponibilidad; }
				set
				{
					if (value == null || (value != "Disponible" && value != "No Disponible"))
					{
						throw new ArgumentException("El estado de disponibilidad debe ser 'Disponible' o 'No Disponible'.");
					}
					else
					{
						estadoDisponibilidad = value;
					}
				}
			}

			public string Especialidad
			{
				get { return especialidad; }
				set
				{
					if (value == null || value.Length < 3)
					{
						throw new ArgumentException("La especialidad debe tener al menos 3 caracteres.");
					}
					else
					{
						especialidad = value;
					}
				}
			}
			public Tecnico(int codigo, string nombre, string especialidad, string estadoDisponibilidad)
				: base(codigo, nombre)
			{
				Especialidad = especialidad;
				EstadoDisponibilidad = estadoDisponibilidad;
				CantidadTrabajosActivos = 0;
				CantidadTrabajosFinalizados = 0;
				CalificacionPromedio = 0;
			}
			public virtual void MostrarInformacion()
			{
                Console.WriteLine("Codigo: TEC-" + Codigo);
                Console.WriteLine("Nombre: " + NombreCompleto);
				Console.WriteLine("Especialidad: " + Especialidad);
				Console.WriteLine("Estado de disponibilidad: " + EstadoDisponibilidad);
				Console.WriteLine("Cantidad de trabajos activos: " + CantidadTrabajosActivos);
				Console.WriteLine("Cantidad de trabajos finalizados: " + CantidadTrabajosFinalizados);
				Console.WriteLine("Calificación promedio: " + CalificacionPromedio);
			}
		}

	}
}
