using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XelaTech
{
    public class Solicitud
    {
		private int codigo;
		private string descripcionProblema;
		private DateTime fechaRecepcion;
		private string prioridad;
		private string estado;
		private string diagnostico;
		private double costoManoObra;
		private double costoRepuestos;
		private double total;

		public double Total
		{
			get { return total; }
			set {
				if(value < 0)
                {
                    throw new ArgumentException("El total no puede ser negativo.");
                }
                else
                {
                    total = value;
                }
            }
		}

		public double CostoRepuestos
		{
			get { return costoRepuestos; }
			set 
			{ 
				if(value < 0)
                {
                    throw new ArgumentException("El costo de repuestos no puede ser negativo.");
                }
                else
                {
                    costoRepuestos = value;
                }
            }
		}

		public double CostoManoObra
		{
			get { return costoManoObra; }
			set 
			{ 
				if(value < 0)
                {
                    throw new ArgumentException("El costo de mano de obra no puede ser negativo.");
                }
                else
                {
                    costoManoObra = value;
                }
            }
		}

		public string Diagnostico
		{
			get { return diagnostico; }
			set 
			{
				if(value == null || value.Length < 10)
                {
                    throw new ArgumentException("El diagnóstico debe tener al menos 10 caracteres.");
                }
                else
                {
                    diagnostico = value;
                }
            }
		}

		public string Estado
		{
			get { return estado; }
			set
			{
				if (value == null || (value != "Pendiente" && value != "En Proceso" && value != "Finalizado"))
				{
					throw new ArgumentException("El estado debe ser 'Pendiente', 'En Proceso' o 'Finalizado'.");
				}
				else
				{
					estado = value;
				}
			}
		}

		public string Prioridad
		{
			get { return prioridad; }
			set 
			{
				if(value == null || (value != "Alta" && value != "Media" && value != "Baja"))
                {
                    throw new ArgumentException("La prioridad debe ser 'Alta', 'Media' o 'Baja'.");
                }
                else
                {
                    prioridad = value;
                }
            }
		}

		public DateTime FechaRecepcion
		{
			get { return fechaRecepcion; }
			set 
			{
				if(value > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de recepción no puede ser futura.");
                }
                else
                {
                    fechaRecepcion = value;
                }
            }
		}

		public string DescripcionProblema
		{
			get { return descripcionProblema; }
			set 
			{
				if(value == null || value.Length < 10)
                {
                    throw new ArgumentException("La descripción del problema debe tener al menos 10 caracteres.");
                }
                else
                {
                    descripcionProblema = value;
                }
            }
		}

		public int Codigo
		{
			get { return codigo; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("El código no puede ser negativo.");
				}
				else
				{
					codigo = value;
				}
			}
		}
        public Solicitud(int codigo, string diagnostico, string estado, string prioridad, DateTime fechaRecepcion, string descripcionProblema, double costoRepuestos, double costoManoObra)
        {
            Codigo = codigo;
            Diagnostico = diagnostico;
            Estado = estado;
            Prioridad = prioridad;
            FechaRecepcion = fechaRecepcion;
            DescripcionProblema = descripcionProblema;
            CostoRepuestos = costoRepuestos;
            CostoManoObra = costoManoObra;
        }
		public void MostrarInformacion()
		{
            Console.WriteLine("Código: SOL-" + Codigo);
            Console.WriteLine("Diagnóstico: " + Diagnostico);
            Console.WriteLine("Estado: " + Estado);
            Console.WriteLine("Prioridad: " + Prioridad);
            Console.WriteLine("Fecha de Recepción: " + FechaRecepcion.ToShortDateString());
            Console.WriteLine("Descripción del Problema: " + DescripcionProblema);
            Console.WriteLine("Costo de Repuestos: " + CostoRepuestos);
            Console.WriteLine("Costo de Mano de Obra: " + CostoManoObra);
            Console.WriteLine("Total: " + Total);
        }

    }
}
