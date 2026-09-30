using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XelaTech
{
	internal class Repuesto
	{
		private int codigo;
		private string nombre;
		private string categoria;
		private int existencia;
		private int existenciaMinima;
		private double costoUnitario;
		private double precioVenta;
		private int cantidadUtilizada;

		public int CantidadUtilizada
		{
			get { return cantidadUtilizada; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("La cantidad utilizada no puede ser negativa.");
				}
				else
				{
					cantidadUtilizada = value;
				}
			}
		}

		public double PrecioVenta
		{
			get { return precioVenta; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("El precio de venta no puede ser negativo.");
				}
				else
				{
					precioVenta = value;
				}
			}
		}

		public double CostoUnitario
		{
			get { return costoUnitario; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("El costo unitario no puede ser negativo.");
				}
				else
				{
					costoUnitario = value;
				}
			}
		}

		public int ExistenciaMinima
		{
			get { return existenciaMinima; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("La existencia mínima no puede ser negativa.");
				}
				else
				{
					existenciaMinima = value;
				}
			}
		}

		public int Existencia
		{
			get { return existencia; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("La existencia no puede ser negativa.");
				}
				else
				{
					existencia = value;
				}
			}
		}

		public string Categoria
		{
			get { return categoria; }
			set
			{
				if (value == null || value.Length < 3)
				{
					throw new ArgumentException("La categoría debe tener al menos 3 caracteres.");
				}
				else
				{
					categoria = value;
				}
			}
		}

		public string Nombre
		{
			get { return nombre; }
			set 
			{
				if(value == null || value.Length < 3)
                {
                    throw new ArgumentException("El nombre debe tener al menos 3 caracteres.");
                }
                else
                {
                    nombre = value;
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

        public Repuesto(int codigo, string nombre, string categoria, int existencia, int existenciaMinima, double costoUnitario, double precioVenta, int cantidadUtilizada)
        {
            Codigo = codigo;
            Nombre = nombre;
            Categoria = categoria;
            Existencia = existencia;
            ExistenciaMinima = existenciaMinima;
            CostoUnitario = costoUnitario;
            PrecioVenta = precioVenta;
            CantidadUtilizada = cantidadUtilizada;
        }
		public void MostrarInformacion()
		{
			Console.WriteLine($"Código: {Codigo}");
			Console.WriteLine($"Nombre: {Nombre}");
			Console.WriteLine($"Categoría: {Categoria}");
			Console.WriteLine($"Existencia: {Existencia}");
			Console.WriteLine($"Existencia mínima: {ExistenciaMinima}");
			Console.WriteLine($"Costo unitario: {CostoUnitario}");
			Console.WriteLine($"Precio de venta: {PrecioVenta}");
			Console.WriteLine($"Cantidad utilizada: {CantidadUtilizada}");
		}

    }
}
