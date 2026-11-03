using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        private Cliente propietario;

        public Cliente Propietario
        {
            get { return propietario; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Seleccione un propietario registrado.");
                }
                propietario = value;
            }
        }

        public string Accesorios
        {
            get { return accesorios; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 3)
                {
                    throw new ArgumentException("Los accesorios deben tener al menos 3 caracteres.");
                }
                else
                {
                    accesorios = value.Trim();
                }
            }
        }

        public string EstadoFisico
        {
            get { return estadoFisico; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 3)
                {
                    throw new ArgumentException("El estado físico debe tener al menos 3 caracteres.");
                }
                else
                {
                    estadoFisico = value.Trim();
                }
            }
        }

        public string TipoEquipo
        {
            get { return tipoEquipo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 3)
                {
                    throw new ArgumentException("El tipo de equipo debe tener al menos 3 caracteres.");
                }
                else
                {
                    tipoEquipo = value.Trim();
                }
            }
        }

        public string Modelo
        {
            get { return modelo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 3)
                {
                    throw new ArgumentException("El modelo debe tener al menos 3 caracteres.");
                }
                else
                {
                    modelo = value.Trim();
                }
            }
        }

        public string Marca
        {
            get { return marca; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 2)
                {
                    throw new ArgumentException("La marca debe tener al menos 2 caracteres.");
                }
                else
                {
                    marca = value.Trim();
                }
            }
        }

        public string NumeroSerie
        {
            get { return numeroSerie; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 3)
                {
                    throw new ArgumentException("El número de serie debe tener al menos 3 caracteres.");
                }
                else
                {
                    numeroSerie = value.Trim();
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
        public Equipo(int codigo, string marca, string modelo, string estadoFisico, string accesorios, string numeroSerie)
        {
            Codigo = codigo;
            Marca = marca;
            Modelo = modelo;
            EstadoFisico = estadoFisico;
            Accesorios = accesorios;
            NumeroSerie = numeroSerie;
            TipoEquipo = "Equipo";
        }

        public Equipo(int codigo, string marca, string modelo, string estadoFisico, string accesorios, string numeroSerie, Cliente propietario)
            : this(codigo, marca, modelo, estadoFisico, accesorios, numeroSerie)
        {
            Propietario = propietario;
        }
        public virtual void MostrarInformacion(string abreviatura)
        {
            Console.WriteLine($"Código: {abreviatura}-{Codigo}");
            Console.WriteLine("Marca: " + Marca);
            Console.WriteLine("Modelo: " + Modelo);
            Console.WriteLine("Tipo de equipo: " + TipoEquipo);
            Console.WriteLine("Estado físico: " + EstadoFisico);
            Console.WriteLine("Accesorios: " + Accesorios);
            Console.WriteLine("Número de serie: " + NumeroSerie);
            Console.WriteLine("Propietario: " + (Propietario == null ? "Sin registrar" : "CLI-" + Propietario.Codigo + " " + Propietario.NombreCompleto));
        }
        public virtual void ActualizarMarca(string marca)
        {
            Marca = marca;
        }
        public virtual void ActualiziarModelo(string modelo)
        {
            Modelo = modelo;
        }
        public virtual void ActualziarTipoEquipo(string tipoEquipo)
        {
            TipoEquipo = tipoEquipo;
        }
        public virtual void ActualizarEstadoFisico(string estadoEquipo)
        {
            EstadoFisico = estadoEquipo;
        }
        public virtual void ActualizarNumeroSerie(string numeroSerie)
        {
            NumeroSerie = numeroSerie;
        }
        public virtual void ActualizarAccesorios(string accesorios)
        {
            Accesorios = accesorios;
        }

    }
    public class ComputadoraPortatil : Equipo
    {
        public ComputadoraPortatil(int codigo, string marca, string modelo, string estadoFisico, string accesorios, string numeroSerie)
            : base(codigo, marca, modelo, estadoFisico, accesorios, numeroSerie)
        {
            TipoEquipo = "Computadora portátil";
        }

        public ComputadoraPortatil(int codigo, string marca, string modelo, string estadoFisico, string accesorios, string numeroSerie, Cliente propietario)
            : this(codigo, marca, modelo, estadoFisico, accesorios, numeroSerie)
        {
            Propietario = propietario;
        }
        public override void MostrarInformacion(string abreviatura)
        {
            Console.WriteLine("=== COMPUTADORA PORTÁTIL ===");
            base.MostrarInformacion(abreviatura);
        }


    }
    public class ComputadoraDeEscritorio : Equipo
    {
        public ComputadoraDeEscritorio(int codigo, string marca, string modelo, string estadoFisico, string accesorios, string numeroSerie)
            : base(codigo, marca, modelo, estadoFisico, accesorios, numeroSerie)
        {
            TipoEquipo = "Computadora de escritorio";
        }

        public ComputadoraDeEscritorio(int codigo, string marca, string modelo, string estadoFisico, string accesorios, string numeroSerie, Cliente propietario)
            : this(codigo, marca, modelo, estadoFisico, accesorios, numeroSerie)
        {
            Propietario = propietario;
        }
        public override void MostrarInformacion(string abreviatura)
        {
            Console.WriteLine("=== COMPUTADORA DE ESCRITORIO ===");
            base.MostrarInformacion(abreviatura);
        }

    }
    public class Impresora : Equipo
    {
        public Impresora(int codigo, string marca, string modelo, string estadoFisico, string accesorios, string numeroSerie)
            : base(codigo, marca, modelo, estadoFisico, accesorios, numeroSerie)
        {
            TipoEquipo = "Impresora";
        }

        public Impresora(int codigo, string marca, string modelo, string estadoFisico, string accesorios, string numeroSerie, Cliente propietario)
            : this(codigo, marca, modelo, estadoFisico, accesorios, numeroSerie)
        {
            Propietario = propietario;
        }
        public override void MostrarInformacion(string abreviatura)
        {
            Console.WriteLine("=== IMPRESORA ===");
            base.MostrarInformacion(abreviatura);
        }

    }
}
