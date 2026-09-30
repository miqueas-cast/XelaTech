using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XelaTech
{
    public class Equipo
    {
		private int codigo;
		private string numeroSerie;
		private string marca;
		private string modelo;
		private string tipoEquipo;
		private string estadoFisico;
		private string accesorios;

		public string Accesorios
		{
			get { return accesorios; }
			set 
			{
				if(value == null || value.Length < 3)
                {
                    throw new ArgumentException("Los accesorios deben tener al menos 3 caracteres.");
                }
                else
                {
                    accesorios = value;
                }
            }
		}

		public string EstadoFisico
		{
			get { return estadoFisico; }
			set 
			{
				if(value == null || value.Length < 3)
                {
                    throw new ArgumentException("El estado físico debe tener al menos 3 caracteres.");
                }
                else
                {
                    estadoFisico = value;
                }
            }
		}

		public string TipoEquipo
		{
			get { return tipoEquipo; }
			set 
			{ 
				if(value == null || value.Length < 3)
                {
                    throw new ArgumentException("El tipo de equipo debe tener al menos 3 caracteres.");
                }
                else
                {
                    tipoEquipo = value;
                }
            }
		}

		public string Modelo
		{
			get { return modelo; }
			set 
			{
				if(value == null || value.Length < 3)
                {
                    throw new ArgumentException("El modelo debe tener al menos 3 caracteres.");
                }
                else
                {
                    modelo = value;
                }
            }
		}

		public string Marca
		{
			get { return marca; }
			set 
			{
				if(value == null || value.Length < 3)
                {
                    throw new ArgumentException("La marca debe tener al menos 3 caracteres.");
                }
                else
                {
                    marca = value;
                }
            }
		}

		public string NumeroSerie
		{
			get { return numeroSerie; }
			set 
			{
				if(value == null || value.Length < 3)
                {
                    throw new ArgumentException("El número de serie debe tener al menos 3 caracteres.");
                }
                else
                {
                    numeroSerie = value;
                }
            }
		}

		public int Codigo
		{
			get { return codigo; }
			set 
			{
				if(value <= 0)
                {
                    throw new ArgumentException("El código debe ser un número positivo.");
                }
                else
                {
                    codigo = value;
                }
            }
		}
        public Equipo(int codigo, string marca, string modelo, string tipoEquipo, string estadoFisico, string accesorios, string numeroSerie)
        {
            Codigo = codigo;
            Marca = marca;
            Modelo = modelo;
            TipoEquipo = tipoEquipo;
            EstadoFisico = estadoFisico;
            Accesorios = accesorios;
            NumeroSerie = numeroSerie;
        }
        public virtual void MostrarInformacion()
        {
            Console.WriteLine("Código: " + Codigo);
            Console.WriteLine("Marca: " + Marca);
            Console.WriteLine("Modelo: " + Modelo);
            Console.WriteLine("Tipo de equipo: " + TipoEquipo);
            Console.WriteLine("Estado físico: " + EstadoFisico);
            Console.WriteLine("Accesorios: " + Accesorios);
            Console.WriteLine("Número de serie: " + NumeroSerie);
        }

    }
	public class ComputadoraPortatil: Equipo
	{
        public ComputadoraPortatil(int codigo, string marca, string modelo, string tipoEquipo, string estadoFisico, string accesorios, string numeroSerie) 
            : base(codigo, marca, modelo, tipoEquipo, estadoFisico, accesorios, numeroSerie)
        {
            
        }
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
        }
    }
	public class ComputadoraDeEscritorio : Equipo
    {
        public ComputadoraDeEscritorio(int codigo, string marca, string modelo, string tipoEquipo, string estadoFisico, string accesorios, string numeroSerie)
            : base(codigo, marca, modelo, tipoEquipo, estadoFisico, accesorios, numeroSerie)
        {

        }
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
        }

    }
	public class Impresora : Equipo
    {
        public Impresora(int codigo, string marca, string modelo, string tipoEquipo, string estadoFisico, string accesorios, string numeroSerie)
            : base(codigo, marca, modelo, tipoEquipo, estadoFisico, accesorios, numeroSerie)
        {
        }
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
        }

    }
}
