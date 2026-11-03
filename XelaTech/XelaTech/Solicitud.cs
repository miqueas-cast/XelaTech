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
        private string especialidadRequerida;
        private double costoManoObra;
        private double costoRepuestos;
        private double total;
        private bool diagnosticoRegistrado;
        private bool manoObraRegistrada;
        private Stack<string> historialEstados = new Stack<string>();
        private List<string> accionesRealizadas = new List<string>();
        private List<RepuestoUtilizado> repuestosUtilizados = new List<RepuestoUtilizado>();
        public const int LimiteTrabajosActivosTecnico = 3;

        public Persona Cliente { get; private set; }
        public Equipo Equipo { get; private set; }
        public Tecnico Tecnico { get; private set; }
        public DateTime FechaAsignacion { get; private set; }

        public string EspecialidadRequerida
        {
            get { return especialidadRequerida; }
            set
            {
                ValidarModificacion();
                if (estado != "Recibida" || value == null || value.Trim().Length < 3)
                {
                    throw new ArgumentException("La especialidad debe indicarse al recibir la solicitud.");
                }
                especialidadRequerida = value.Trim();
            }
        }

        public double Total
        {
            get { return total; }
            private set
            {
                ValidarMonto(value);
                total = value;
            }
        }

        public double CostoRepuestos
        {
            get { return costoRepuestos; }
            private set
            {
                ValidarMonto(value);
                costoRepuestos = value;
            }
        }

        public double CostoManoObra
        {
            get { return costoManoObra; }
            private set
            {
                ValidarMonto(value);
                costoManoObra = value;
            }
        }

        public string Diagnostico
        {
            get { return diagnostico; }
            private set
            {
                if (value == null || value.Trim().Length < 5)
                {
                    throw new ArgumentException("El diagnóstico debe tener al menos 5 caracteres.");
                }
                diagnostico = value.Trim();
            }
        }

        public string Estado
        {
            get { return estado; }
            set { CambiarEstado(value); }
        }

        public string Prioridad
        {
            get { return prioridad; }
            private set
            {
                if (value != "Normal" && value != "Urgente")
                {
                    throw new ArgumentException("La prioridad debe ser 'Normal' o 'Urgente'.");
                }
                prioridad = value;
            }
        }

        public DateTime FechaRecepcion
        {
            get { return fechaRecepcion; }
            private set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de recepción no puede ser futura.");
                }
                fechaRecepcion = value;
            }
        }

        public string DescripcionProblema
        {
            get { return descripcionProblema; }
            private set
            {
                if (value == null || value.Trim().Length < 10)
                {
                    throw new ArgumentException("La descripción debe tener al menos 10 caracteres.");
                }
                descripcionProblema = value.Trim();
            }
        }

        public int Codigo
        {
            get { return codigo; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El código debe ser un número positivo.");
                }
                codigo = value;
            }
        }

        public Solicitud(int codigo, string diagnostico, string estado, string prioridad, string descripcionProblema, double costoRepuestos, double costoManoObra, Cliente cliente, Equipo equipo, Tecnico tecnico)
        {
            if (cliente == null || equipo == null)
            {
                throw new ArgumentException("Debe seleccionar un cliente y un equipo existentes.");
            }
            if (estado != "Recibida" || tecnico != null || costoRepuestos != 0)
            {
                throw new ArgumentException("La solicitud debe comenzar recibida, sin técnico ni repuestos utilizados.");
            }
            Codigo = codigo;
            Diagnostico = diagnostico;
            Prioridad = prioridad;
            FechaRecepcion = DateTime.Now;
            DescripcionProblema = descripcionProblema;
            CostoRepuestos = costoRepuestos;
            CostoManoObra = costoManoObra;
            Cliente = cliente;
            Equipo = equipo;
            this.estado = estado;
            CalcularTotal();
            if (equipo.Propietario == null) equipo.Propietario = cliente;
        }

        private void ValidarModificacion()
        {
            if (estado == "Entregada" || estado == "Cancelada")
            {
                throw new ArgumentException("No se puede modificar una solicitud entregada o cancelada.");
            }
        }

        private void ValidarMonto(double valor)
        {
            if (valor < 0 || double.IsNaN(valor) || double.IsInfinity(valor))
            {
                throw new ArgumentException("El costo debe ser un número válido y no negativo.");
            }
        }

        public string ObtenerSiguienteEstado()
        {
            switch (estado)
            {
                case "Recibida": return "En espera";
                case "En espera": return "Asignada";
                case "Asignada": return "En diagnóstico";
                case "En diagnóstico": return "En reparación";
                case "En reparación": return "Lista para entregar";
                case "Lista para entregar": return "Entregada";
                default: return "";
            }
        }

        public void CambiarEstado(string nuevoEstado)
        {
            ValidarModificacion();
            if (nuevoEstado == "Cancelada")
            {
                Cancelar();
                return;
            }
            if (nuevoEstado == "Entregada")
            {
                RegistrarEntrega();
                return;
            }
            if (nuevoEstado != ObtenerSiguienteEstado() || nuevoEstado == "")
            {
                throw new ArgumentException("El cambio de estado no respeta el orden de atención.");
            }
            if (nuevoEstado == "En espera" && especialidadRequerida == null)
            {
                throw new ArgumentException("Debe indicar la especialidad requerida.");
            }
            if (nuevoEstado == "Asignada" && Tecnico == null)
            {
                throw new ArgumentException("Debe asignar un técnico desde la atención de colas.");
            }
            if (nuevoEstado == "En reparación" && !diagnosticoRegistrado)
            {
                throw new ArgumentException("Debe registrar el diagnóstico antes de reparar.");
            }
            if (nuevoEstado == "Lista para entregar" && !manoObraRegistrada)
            {
                throw new ArgumentException("Debe registrar el costo de mano de obra, incluso si es cero.");
            }
            historialEstados.Push(estado);
            estado = nuevoEstado;
        }

        public void AsignarTecnico(Tecnico tecnico)
        {
            ValidarModificacion();
            if (estado != "En espera" || tecnico == null)
            {
                throw new ArgumentException("Solo se puede asignar un técnico a una solicitud en espera.");
            }
            if (tecnico.Especialidad.Trim().ToLower() != especialidadRequerida.ToLower())
            {
                throw new ArgumentException("El técnico no tiene la especialidad requerida.");
            }
            if (tecnico.EstadoDisponibilidad != "Disponible" || tecnico.CantidadTrabajosActivos >= LimiteTrabajosActivosTecnico)
            {
                throw new ArgumentException("El técnico no está disponible o alcanzó su límite de trabajos.");
            }
            Tecnico = tecnico;
            CambiarEstado("Asignada");
            FechaAsignacion = DateTime.Now;
            tecnico.CantidadTrabajosActivos++;
        }

        public void RegistrarDiagnostico(string nuevoDiagnostico)
        {
            ValidarModificacion();
            if (estado != "En diagnóstico" && estado != "En reparación")
            {
                throw new ArgumentException("Debe estar en diagnóstico o en reparación para registrar el diagnóstico.");
            }
            Diagnostico = nuevoDiagnostico;
            diagnosticoRegistrado = true;
        }

        public void RegistrarAccion(string accion)
        {
            ValidarModificacion();
            if (estado != "En diagnóstico" && estado != "En reparación")
            {
                throw new ArgumentException("Las acciones se registran durante el diagnóstico o la reparación.");
            }
            if (accion == null || accion.Trim().Length < 3)
            {
                throw new ArgumentException("La acción debe tener al menos 3 caracteres.");
            }
            accionesRealizadas.Add(accion.Trim());
        }

        public void RegistrarManoObra(double costo)
        {
            ValidarModificacion();
            if (estado != "En reparación")
            {
                throw new ArgumentException("La mano de obra se registra durante la reparación.");
            }
            ValidarMonto(costo);
            ValidarMonto(costo + CostoRepuestos);
            CostoManoObra = costo;
            manoObraRegistrada = true;
            CalcularTotal();
        }

        internal void RegistrarRepuesto(Repuesto repuesto, int cantidad)
        {
            ValidarModificacion();
            if (estado != "En reparación" || repuesto == null)
            {
                throw new ArgumentException("Debe seleccionar un repuesto durante la reparación.");
            }
            ValidarMonto(repuesto.PrecioVenta);
            double nuevoCosto = CostoRepuestos + repuesto.PrecioVenta * cantidad;
            ValidarMonto(nuevoCosto);
            ValidarMonto(nuevoCosto + CostoManoObra);
            repuesto.UtilizarCantidad(cantidad);
            repuestosUtilizados.Add(new RepuestoUtilizado(repuesto, cantidad, repuesto.PrecioVenta));
            CostoRepuestos = nuevoCosto;
            CalcularTotal();
        }

        public void CalcularTotal()
        {
            ValidarModificacion();
            Total = CostoManoObra + CostoRepuestos;
        }

        public void DeshacerUltimoEstado()
        {
            ValidarModificacion();
            if (historialEstados.Count == 0)
            {
                throw new ArgumentException("El historial está vacío.");
            }
            string estadoAnterior = historialEstados.Peek();
            if ((estadoAnterior == "En espera" || estadoAnterior == "Recibida") && Tecnico != null)
            {
                if (Tecnico.CantidadTrabajosActivos <= 0)
                {
                    throw new ArgumentException("La carga del técnico no permite deshacer la asignación.");
                }
                Tecnico.CantidadTrabajosActivos--;
                Tecnico = null;
                FechaAsignacion = DateTime.MinValue;
            }
            estado = historialEstados.Pop();
        }

        public void Cancelar()
        {
            ValidarModificacion();
            if (Tecnico != null && Tecnico.CantidadTrabajosActivos <= 0)
            {
                throw new ArgumentException("La carga del técnico no permite cancelar la solicitud.");
            }
            for (int i = 0; i < repuestosUtilizados.Count; i++)
            {
                Repuesto repuesto = repuestosUtilizados[i].Repuesto;
                bool revisado = false;
                for (int j = 0; j < i; j++)
                {
                    if (repuestosUtilizados[j].Repuesto == repuesto)
                    {
                        revisado = true;
                    }
                }
                if (revisado) continue;
                int cantidad = 0;
                foreach (RepuestoUtilizado utilizado in repuestosUtilizados)
                {
                    if (utilizado.Repuesto == repuesto)
                    {
                        if (cantidad > int.MaxValue - utilizado.Cantidad)
                        {
                            throw new ArgumentException("La cantidad a devolver supera el límite permitido.");
                        }
                        cantidad += utilizado.Cantidad;
                    }
                }
                if (repuesto.Existencia > int.MaxValue - cantidad || repuesto.CantidadUtilizada < cantidad)
                {
                    throw new ArgumentException("No es posible devolver los repuestos con las existencias actuales.");
                }
            }
            foreach (RepuestoUtilizado utilizado in repuestosUtilizados)
            {
                utilizado.Repuesto.DevolverCantidad(utilizado.Cantidad);
            }
            if (Tecnico != null) Tecnico.CantidadTrabajosActivos--;
            historialEstados.Push(estado);
            estado = "Cancelada";
        }

        public void RegistrarEntrega()
        {
            ValidarModificacion();
            if (estado != "Lista para entregar")
            {
                throw new ArgumentException("Solo se puede entregar una solicitud lista para entregar.");
            }
            if (Tecnico == null || Tecnico.CantidadTrabajosActivos <= 0 || Tecnico.CantidadTrabajosFinalizados == int.MaxValue)
            {
                throw new ArgumentException("La carga del técnico no permite registrar la entrega.");
            }
            CalcularTotal();
            historialEstados.Push(estado);
            estado = "Entregada";
            Tecnico.CantidadTrabajosActivos--;
            Tecnico.CantidadTrabajosFinalizados++;
        }

        public void MostrarHistorial()
        {
            Console.WriteLine("Estado actual: " + estado);
            if (historialEstados.Count == 0) Console.WriteLine("No hay estados anteriores.");
            foreach (string estadoAnterior in historialEstados)
            {
                Console.WriteLine("Estado anterior: " + estadoAnterior);
            }
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("Código: SOL-" + Codigo);
            Console.WriteLine("Cliente: CLI-" + Cliente.Codigo + " - " + Cliente.NombreCompleto);
            Console.WriteLine("Equipo: " + Equipo.Marca + " " + Equipo.Modelo + " | Código: " + Equipo.Codigo + " | Serie: " + Equipo.NumeroSerie);
            Console.WriteLine("Técnico: " + (Tecnico == null ? "Sin asignar" : "TEC-" + Tecnico.Codigo + " - " + Tecnico.NombreCompleto));
            if (FechaAsignacion != DateTime.MinValue)
            {
                Console.WriteLine("Fecha de asignación: " + FechaAsignacion.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            Console.WriteLine("Especialidad requerida: " + EspecialidadRequerida);
            Console.WriteLine("Diagnóstico: " + Diagnostico);
            Console.WriteLine("Estado: " + Estado);
            Console.WriteLine("Prioridad: " + Prioridad);
            Console.WriteLine("Fecha de recepción: " + FechaRecepcion.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.WriteLine("Descripción del problema: " + DescripcionProblema);
            Console.WriteLine("Acciones realizadas:");
            if (accionesRealizadas.Count == 0) Console.WriteLine("Sin acciones registradas.");
            foreach (string accion in accionesRealizadas) Console.WriteLine("- " + accion);
            Console.WriteLine("Repuestos utilizados:");
            if (repuestosUtilizados.Count == 0) Console.WriteLine("Sin repuestos.");
            foreach (RepuestoUtilizado utilizado in repuestosUtilizados)
            {
                Console.WriteLine("REP-" + utilizado.Repuesto.Codigo + " " + utilizado.Repuesto.Nombre +
                    " | Cantidad: " + utilizado.Cantidad + " | Precio aplicado: " + utilizado.PrecioVenta);
            }
            if (estado == "Cancelada") Console.WriteLine("Los repuestos de esta solicitud fueron devueltos al inventario.");
            Console.WriteLine("Costo de repuestos: " + CostoRepuestos);
            Console.WriteLine("Costo de mano de obra: " + CostoManoObra);
            Console.WriteLine("Total: " + Total);
        }
    }

    internal class RepuestoUtilizado
    {
        public Repuesto Repuesto { get; private set; }
        public int Cantidad { get; private set; }
        public double PrecioVenta { get; private set; }

        public RepuestoUtilizado(Repuesto repuesto, int cantidad, double precioVenta)
        {
            Repuesto = repuesto;
            Cantidad = cantidad;
            PrecioVenta = precioVenta;
        }
    }
}
