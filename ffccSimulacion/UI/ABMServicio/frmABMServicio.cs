using SimuRails.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuRails.UI.ABMServicio
{
    public partial class frmABMServicio : Form
    {
        SimuRailsEntities context;
        List<Tramo> auxRelaciones;
        string programacionCrear;
        string programacionModificar;

        public frmABMServicio()
        {
            InitializeComponent();

            context = new SimuRailsEntities();
            auxRelaciones = new List<Tramo>();

            /*Por un error en el framework estas propiedades no aparecen en el diseñador de las vistas ni sugeridas cuando se escriben.
             Pero igualmente te deja usarlas*/
            clbxFormacionesCrear.DisplayMember = "Nombre";
            clbxFormacionesMod.DisplayMember = "Nombre";
            cbxEsParadaCrear.Checked = true;
            cbxEsParadaMod.Checked = true;

            CargarListas();
        }

        private void CargarListas()
        {
            foreach (Estacion e in context.Estacion)
            {
                lbxEstacionesOrigenCrear.Items.Add(e);
                lbxEstacionesDestinoCrear.Items.Add(e);
                lbxEstacionesDestinoMod.Items.Add(e);
                lbxEstacionesOrigenMod.Items.Add(e);
            }

            foreach (Formacion f in context.Formacion)
            {
                clbxFormacionesCrear.Items.Add(f);
                clbxFormacionesMod.Items.Add(f);
            }

            foreach(Servicio s in context.Servicio)
            {
                lbxServiciosModificar.Items.Add(s);
                lbxServiciosEliminar.Items.Add(s);
            }
        }

        private List<Tramo> OrdenarRelaciones(List<Tramo> listaDesaordenada)
        {
            List<Tramo> listaOrdenada = new List<Tramo>();
            if (listaDesaordenada.Count == 0)
                return listaOrdenada;

            Tramo relacionActual = listaDesaordenada.First();
            Estacion estacionOrigen = new Estacion();

            //Primero busco cual es la estacion de inicio
            while (relacionActual != null)
            {
                estacionOrigen = relacionActual.Estacion;
                relacionActual = listaDesaordenada.Where(x => x.Id_Estacion_Siguiente == relacionActual.Id_Estacion_Anterior).FirstOrDefault();
            }

            //Busco la primer relacion. Sera aquella que tenga como estacion anterior a la estacion origen
            relacionActual = listaDesaordenada.Where(x => x.Id_Estacion_Anterior == estacionOrigen.Id).FirstOrDefault();

            while(relacionActual!=null)
            {
                listaOrdenada.Add(relacionActual);
                relacionActual = listaDesaordenada.Where(x => x.Id_Estacion_Anterior == relacionActual.Id_Estacion_Siguiente).FirstOrDefault();
            }

            return listaOrdenada;
        }

        private void btnAgregarRelacionTabCrear_Click(object sender, EventArgs e)
        {
            string errorMsj = "";
            Estacion estacionOrigen = (Estacion)lbxEstacionesOrigenCrear.SelectedItem;
            Estacion estacionDestino = (Estacion)lbxEstacionesDestinoCrear.SelectedItem;

            if (estacionOrigen == null || estacionDestino==null)
            {
                MessageBox.Show("La estación de origen y/o la estación de destino no han sido seleccionadas.\n");
                return;
            }

            if (!Util.EsNumerico(txtDistanciaRelacionCrear.Text))
                errorMsj += "Distancia entre estaciones: Incompleto ó Incorrecto.\n";
            else if (int.Parse(txtDistanciaRelacionCrear.Text) == 0)
                errorMsj += "Distancia: El valor debe ser positivo.\n";
            if(!Util.EsNumerico(txtTiempoViajeRelacionCrear.Text))
                errorMsj += "Tiempo de Viaje: Incompleto ó Incorrecto.\n";
            else if (int.Parse(txtTiempoViajeRelacionCrear.Text) == 0)
                errorMsj += "Tiempo de Viaje: El valor debe ser positivo.\n";
            if (BuscarRelacion(estacionOrigen.Id, estacionDestino.Id) != null)
                errorMsj += "Esa relación ya existe en el servicio.\n";
            if (estacionDestino.Id == estacionOrigen.Id)
                errorMsj += "Una estación no puede ser origen y destino al mismo tiempo.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                Tramo nuevaRelacion = new Tramo();
                nuevaRelacion.Estacion = estacionOrigen;
                nuevaRelacion.Id_Estacion_Anterior = estacionOrigen.Id;
                nuevaRelacion.Estacion1 = estacionDestino;
                nuevaRelacion.Id_Estacion_Siguiente = estacionDestino.Id;
                nuevaRelacion.TiempoViaje = Convert.ToInt32(txtTiempoViajeRelacionCrear.Text); ;
                nuevaRelacion.Distancia = Convert.ToInt32(txtDistanciaRelacionCrear.Text);
                nuevaRelacion.EstSigEsParada = true;
               
                auxRelaciones.Add(nuevaRelacion);
                dgvRelacionesCrear.Rows.Add(new string[] { estacionOrigen.Nombre, estacionDestino.Nombre, estacionOrigen.Id.ToString(), estacionDestino.Id.ToString() });
                txtDistanciaRelacionCrear.Text = "";
                txtTiempoViajeRelacionCrear.Text = "";
                //lbxEstacionesOrigenCrear.SelectedIndex = -1;
                lbxEstacionesDestinoCrear.SelectedIndex = -1;
                lbxEstacionesOrigenCrear.SelectedItem = estacionDestino;
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void btnBorrarRelacionTabCrear_Click(object sender, EventArgs e)
        {
            string errorMsj = "";

            if (dgvRelacionesCrear.SelectedRows.Count != 1)
                errorMsj += "Debe seleccionar un fila para poder ser borrada.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                int id_estacionOrigen = Convert.ToInt32(dgvRelacionesCrear.SelectedRows[0].Cells["txtId_EstacionOrigen"].Value);
                int id_estacionDestino = Convert.ToInt32(dgvRelacionesCrear.SelectedRows[0].Cells["txtId_EstacionDestino"].Value);

                dgvRelacionesCrear.Rows.Remove(dgvRelacionesCrear.SelectedRows[0]);
                auxRelaciones.Remove(BuscarRelacion(id_estacionOrigen, id_estacionDestino));
            }
            else
                MessageBox.Show(errorMsj);
        }

        private Tramo BuscarRelacion(int id_estacionOrigen,int id_estacionDentino)
        {
            if (auxRelaciones.Count == 0)
                return null;
            Tramo r = auxRelaciones.Where(x => x.Id_Estacion_Anterior == id_estacionOrigen && x.Id_Estacion_Siguiente == id_estacionDentino).FirstOrDefault();

            return r;
        }

        private Tramo BuscarRelacion2(int id_estacionOrigen, int id_estacionDentino, Servicio s)
        {
            Tramo r = s.Tramo.Where(x => x.Id_Estacion_Anterior == id_estacionOrigen && x.Id_Estacion_Siguiente == id_estacionDentino).FirstOrDefault();

            return r;
        }

        private void dgvRelacionesCrear_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id_estacionOrigen = Convert.ToInt32(dgvRelacionesCrear.SelectedRows[0].Cells["txtId_EstacionOrigen"].Value);
            int id_estacionDestino = Convert.ToInt32(dgvRelacionesCrear.SelectedRows[0].Cells["txtId_EstacionDestino"].Value);
            Tramo r = BuscarRelacion(id_estacionOrigen, id_estacionDestino);

            lbxEstacionesOrigenCrear.SelectedItem = r.Estacion;
            lbxEstacionesDestinoCrear.SelectedItem = r.Estacion1;
            txtDistanciaRelacionCrear.Text = r.Distancia.ToString();
            txtTiempoViajeRelacionCrear.Text = r.TiempoViaje.ToString();
            txtDistanciaRelacionCrear.Enabled = false;
            txtTiempoViajeRelacionCrear.Enabled = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrearServicio)
                CrearNuevoServicio();

            if (tabControl1.SelectedTab == tabModificarServicio)
                GuardarCambiosServicio();
        }

        private void CrearNuevoServicio()
        {
            string errorMsj = "";
            Servicio nuevoServicio = new Servicio();

            if (!Util.EsAlfaNumerico(txtNombreServicio.Text))
                errorMsj += "Nombre de servicio: Incompleto/Incorrecto.\n";
            else if (context.Servicio.Where(x => x.Nombre == txtNombreServicio.Text).Count() > 0)
                errorMsj += "Nombre de servicio: ya existe un servicio con el mismo nombre.\n";

            if (auxRelaciones.Count == 0)
                errorMsj += "El servicio no tiene estaciones relacionadas.\n";

            if (!nuevoServicio.ServicioEsValido(auxRelaciones))
                errorMsj += "Las relaciones del servicio tienen errores.\n";

            if (clbxFormacionesCrear.CheckedItems.Count == 0)
                errorMsj += "El servicio no tiene formaciones asignadas.\n";

            if (string.IsNullOrEmpty(programacionCrear))
                errorMsj += "El servicio no tiene programacion asignada.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                nuevoServicio.Nombre = txtNombreServicio.Text;
                nuevoServicio.Tramo = auxRelaciones;
                nuevoServicio.ProgramacionStr = programacionCrear;

                for (int pos = 0; pos <= clbxFormacionesCrear.CheckedItems.Count - 1; pos++)
                {
                    Formacion f = (Formacion)clbxFormacionesCrear.CheckedItems[pos];
                    Servicio_X_Formacion sf = new Servicio_X_Formacion();
                    sf.Id_Formacion = f.Id;
                    sf.Formacion = f;
                    //sf.Servicios = nuevoServicio;
                    nuevoServicio.Servicio_X_Formacion.Add(sf);
                }
                try
                {
                    context.Servicio.Add(nuevoServicio);
                    context.SaveChanges();
                    lbxServiciosModificar.Items.Add(nuevoServicio);
                    lbxServiciosEliminar.Items.Add(nuevoServicio);
                    LimpiarTabCrear();
                    MessageBox.Show("El servicio se ha creado exitosamente.\n");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se guardo el servicio \n\n" + exc.ToString());
                }
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void LimpiarTabCrear()
        {
            auxRelaciones = new List<Tramo>();
            txtNombreServicio.Text = "";
            txtDistanciaRelacionCrear.Text = "";
            txtTiempoViajeRelacionCrear.Text = "";
            programacionCrear = "";
            cbxEsParadaCrear.Checked = true;
            lbxEstacionesOrigenCrear.SelectedIndex = -1;
            lbxEstacionesDestinoCrear.SelectedIndex = -1;
            dgvRelacionesCrear.Rows.Clear();
            clbxFormacionesCrear.Items.Clear();
            foreach (Formacion f in context.Formacion)
                clbxFormacionesCrear.Items.Add(f);
        }

        private void btnBorrarServicio_Click(object sender, EventArgs e)
        {
            string errorMsj = "";
            Servicio s = (Servicio)lbxServiciosEliminar.SelectedItem;

            if (s == null)
                errorMsj += "No se ha seleccionado ningun servicio para ser borrado.\n";
            else if (context.Traza_X_Servicio.Where(x => x.Id_Servicio == s.Id).Count() != 0)
                errorMsj += "El servicio no puede ser borrado porque pertenece a una o mas trazas.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                if (MessageBox.Show("El servicio se eliminará de manera permanente.¿Desea continuar?", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

                try
                {
                    foreach (Servicio_X_Formacion sf in context.Servicio_X_Formacion.Where(x => x.Id_Servicio == s.Id))
                        context.Servicio_X_Formacion.Remove(sf);

                    foreach (Tramo r in context.Tramo.Where(x => x.Id_Servicio == s.Id))
                        context.Tramo.Remove(r);

                    context.Servicio.Remove(s);
                    context.SaveChanges();
                    lbxServiciosEliminar.Items.Remove(s);
                    lbxServiciosModificar.Items.Remove(s);
                    MessageBox.Show("El servicio se ha eliminado exitosamente.\n");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se elimino el servicio \n\n" + exc.ToString());
                }
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrearServicio)
                LimpiarTabCrear();

            if (tabControl1.SelectedTab == tabModificarServicio)
                LimpiarTabModificar(true);
        }

        private void seleccionarPestaña(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrearServicio)
            {
                btnAceptar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            if (tabControl1.SelectedTab == tabModificarServicio)
            {
                btnAceptar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            if (tabControl1.SelectedTab == tabEliminarServicio)
            {
                btnAceptar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
        }

        private void LimpiarTabModificar(bool inhabilitarControles = false)
        {
            if (inhabilitarControles)
            {
                txtNombreServicioMod.Enabled = false;
                lbxEstacionesOrigenMod.Enabled = false;
                lbxEstacionesDestinoMod.Enabled = false;
                dgvRelacionesMod.Enabled = false;
                txtDistanciaRelacionMod.Enabled = false;
                txtTiempoViajeRelacionMod.Enabled = false;
                clbxFormacionesMod.Enabled = false;
                lbxServiciosModificar.SelectedIndex = -1;
            }

            cbxEsParadaMod.Checked = true;
            lbxEstacionesOrigenMod.SelectedIndex = -1;
            lbxEstacionesDestinoMod.SelectedIndex = -1;
            txtNombreServicioMod.Text = "";
            dgvRelacionesMod.Rows.Clear();
            for (int i = 0; i < clbxFormacionesMod.Items.Count; i++)
                clbxFormacionesMod.SetItemChecked(i, false);
        }

        private void lbxServiciosModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Servicio servicioSeleccionado = (Servicio)lbxServiciosModificar.SelectedItem;
            if (servicioSeleccionado == null)
                return;

            txtNombreServicioMod.Enabled = true;
            lbxEstacionesOrigenMod.Enabled = true;
            lbxEstacionesDestinoMod.Enabled = true;
            dgvRelacionesMod.Enabled = true;
            txtDistanciaRelacionMod.Enabled = true;
            txtTiempoViajeRelacionMod.Enabled = true;
            clbxFormacionesMod.Enabled = true;

            LimpiarTabModificar();

            List<Tramo> listaOrdenadaRelaciones = this.OrdenarRelaciones(servicioSeleccionado.Tramo.ToList<Tramo>());

            //foreach (Relaciones r in servicioSeleccionado.Relaciones)
            foreach (Tramo r in listaOrdenadaRelaciones)
                dgvRelacionesMod.Rows.Add(r.Estacion.Nombre, r.Estacion1.Nombre, r.Estacion.Id, r.Estacion1.Id);

            //clbxFormacionesMod.CheckedItems = servicioSeleccionado.Servicios_X_Formaciones.Select(x => x.Formaciones);
            foreach (Formacion f in servicioSeleccionado.Servicio_X_Formacion.Select(x => x.Formacion))
                clbxFormacionesMod.SetItemChecked(clbxFormacionesMod.Items.IndexOf(f), true);

            txtNombreServicioMod.Text = servicioSeleccionado.Nombre;
            programacionModificar = servicioSeleccionado.ProgramacionStr;
        }

        private void btnBorrarRelacionMod_Click(object sender, EventArgs e)
        {
            Servicio servicioSeleccionado = (Servicio)lbxServiciosModificar.SelectedItem;
            string errorMsj = "";

            if (servicioSeleccionado == null)
                errorMsj += "No se ha seleccionado ningún servicio para ser modificado.\n";

            if (dgvRelacionesMod.SelectedRows.Count != 1)
                errorMsj += "Debe seleccionar un fila para poder ser borrada.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                int id_estacionOrigen = Convert.ToInt32(dgvRelacionesMod.SelectedRows[0].Cells["Id_EstacionOrigen"].Value);
                int id_estacionDestino = Convert.ToInt32(dgvRelacionesMod.SelectedRows[0].Cells["Id_EstacionDestino"].Value);

                Tramo r = servicioSeleccionado.Tramo.Where(x => x.Estacion.Id == id_estacionOrigen && x.Estacion1.Id == id_estacionDestino).FirstOrDefault();
                if (r != null)
                {
                    servicioSeleccionado.Tramo.Remove(r);

                    /*Esto lo hago con try catch xq si la relacion fue agregada durante la modificacion del servicio la misma
                     todavia no guardada en la base y por lo tanto tampoco pertenece al contexto. Recien va a aparecer en el contexto cuando se guarden los cambios en la bd*/
                    try
                    { context.Tramo.Remove(r); }
                    catch
                    { }
                }

                dgvRelacionesMod.Rows.Remove(dgvRelacionesMod.SelectedRows[0]);
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void btnAgregarRelacionMod_Click(object sender, EventArgs e)
        {
            string errorMsj = "";
            Servicio servicioSeleccionado = (Servicio)lbxServiciosModificar.SelectedItem;
            Estacion estacionOrigen = (Estacion)lbxEstacionesOrigenMod.SelectedItem;
            Estacion estacionDestino = (Estacion)lbxEstacionesDestinoMod.SelectedItem;

            if (estacionOrigen == null || estacionDestino==null)
            {
                MessageBox.Show("La estación de origen y/o la estación de destino no han sido seleccionadas.\n");
                return;
            }

            if (servicioSeleccionado == null)
            {
                MessageBox.Show("No se ha seleccionado ningún servicio para ser modificado.\n");
                return;
            }

            if (servicioSeleccionado == null)
                errorMsj += "No se ha seleccionado ningún servicio para ser modificado.\n";
            if (!Util.EsNumerico(txtDistanciaRelacionMod.Text))
                errorMsj += "Distancia entre estaciones: Incompleto ó Incorrecto.\n";
            else if (int.Parse(txtDistanciaRelacionMod.Text) == 0)
                errorMsj += "Distancia: El valor debe ser positivo.\n";
            if (!Util.EsNumerico(txtTiempoViajeRelacionMod.Text))
                errorMsj += "Tiempo de Viaje: Incompleto ó Incorrecto.\n";
            else if (int.Parse(txtTiempoViajeRelacionMod.Text) == 0)
                errorMsj += "Tiempo de Viaje: El valor debe ser positivo.\n";
            if (estacionOrigen == null)
                errorMsj += "Debe seleccionar una estación de origen.\n";
            if (estacionDestino == null)
                errorMsj += "Debe seleccionar una estación de destino.\n";
            if (BuscarRelacion(estacionOrigen.Id, estacionDestino.Id) != null)
                errorMsj += "Esa relación ya existe en el servicio.\n";
            if (estacionDestino.Id == estacionOrigen.Id)
                errorMsj += "Una estación no puede ser origen y destino al mismo tiempo.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                Tramo nuevaRelacion = new Tramo();
                nuevaRelacion.EstSigEsParada = true;
                nuevaRelacion.Distancia = Convert.ToInt32(txtDistanciaRelacionMod.Text);
                nuevaRelacion.TiempoViaje = Convert.ToInt32(txtTiempoViajeRelacionMod.Text);
                nuevaRelacion.Id_Estacion_Anterior = estacionOrigen.Id;
                nuevaRelacion.Id_Estacion_Siguiente = estacionDestino.Id;
                nuevaRelacion.Estacion = estacionOrigen;
                nuevaRelacion.Estacion1 = estacionDestino;

                servicioSeleccionado.Tramo.Add(nuevaRelacion);
                var rowIndex = dgvRelacionesMod.SelectedRows[0].Index;
                dgvRelacionesMod.Rows.Insert(rowIndex, estacionOrigen.Nombre, estacionDestino.Nombre, estacionOrigen.Id, estacionDestino.Id);
                txtDistanciaRelacionMod.Text = "";
                txtTiempoViajeRelacionMod.Text = "";
                lbxEstacionesDestinoMod.SelectedIndex = -1;
                lbxEstacionesOrigenMod.SelectedItem = estacionDestino;
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void GuardarCambiosServicio()
        {
            string errorMsj = "";
            Servicio servicioSeleccionado = (Servicio)lbxServiciosModificar.SelectedItem;

            if (servicioSeleccionado == null)
            {
                MessageBox.Show("Se ha deseleccionado la formación que iba a ser modificada.\n");
                return;
            }
                

            List<string> nombreDeServicios = context.Servicio.Where(x => x.Id != servicioSeleccionado.Id).Select(x => x.Nombre).ToList<string>();

            if (!Util.EsAlfaNumerico(txtNombreServicioMod.Text))
                errorMsj += "Nombre de servicio: Incompleto ó Incorrecto.\n";
            else if (nombreDeServicios.Contains(txtNombreServicioMod.Text))
                errorMsj += "Nombre de servicio: ya existe un servicio con el mismo nombre.\n";

            if (servicioSeleccionado.Tramo.Count == 0)
                errorMsj += "El servicio no tiene estaciones relacionadas.\n";

            if (!servicioSeleccionado.ServicioEsValido(servicioSeleccionado.Tramo.ToList<Tramo>()))
                errorMsj += "Las relaciones del servicio tienen errores.\n";

            if (clbxFormacionesMod.CheckedItems.Count == 0)
                errorMsj += "El servicio no tiene formaciones asignadas.\n";

            if (string.IsNullOrEmpty(programacionModificar))
                errorMsj += "El servicio no tiene programacion asignada.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                try
                {
                    List<Servicio_X_Formacion> auxFormacionesServicio = servicioSeleccionado.Servicio_X_Formacion.ToList<Servicio_X_Formacion>();
                    servicioSeleccionado.Nombre = txtNombreServicioMod.Text;
                    servicioSeleccionado.ProgramacionStr = programacionModificar;

                    /*Se quitan aquellas formaciones que ya no pertenezcan al servicio*/
                    foreach (Servicio_X_Formacion sf in auxFormacionesServicio)
                    {
                        if (!clbxFormacionesMod.CheckedItems.Contains(sf.Formacion))
                        {
                            servicioSeleccionado.Servicio_X_Formacion.Remove(sf);
                            context.Servicio_X_Formacion.Remove(sf);
                        }
                    }

                    /*Se agregan las nuevas formaciones al servicio*/
                    foreach (Formacion f in clbxFormacionesMod.CheckedItems)
                    {
                        if (servicioSeleccionado.Servicio_X_Formacion.Where(x => x.Id_Formacion == f.Id).Count() == 0)
                        {
                            Servicio_X_Formacion servform = new Servicio_X_Formacion();
                            servform.Id_Servicio = servicioSeleccionado.Id;
                            servform.Id_Formacion = f.Id;
                            servform.Formacion = f;

                            servicioSeleccionado.Servicio_X_Formacion.Add(servform);
                        }
                    }

                    /*Las modificaciones a la lista de relaciones se van guardando a medida que se agregan o sacan formaciones*/

                    context.SaveChanges();

                    lbxServiciosModificar.Items.Clear();
                    lbxServiciosEliminar.Items.Clear();
                    foreach (Servicio s in context.Servicio)
                    {
                        lbxServiciosModificar.Items.Add(s);
                        lbxServiciosEliminar.Items.Add(s);
                    }

                    MessageBox.Show("Los cambios han sido guardados exitosamente.\n");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se guardo el servicio \n\n" + exc.ToString());
                }
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.pnlServicio.Controls.Clear();
        }

        private void dgvRelacionesMod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Servicio servicioSeleccionado = (Servicio)lbxServiciosModificar.SelectedItem;
            int id_estacionOrigen = Convert.ToInt32(dgvRelacionesMod.SelectedRows[0].Cells["Id_EstacionOrigen"].Value);
            int id_estacionDestino = Convert.ToInt32(dgvRelacionesMod.SelectedRows[0].Cells["Id_EstacionDestino"].Value);
            
            if(servicioSeleccionado==null)
            {
                MessageBox.Show("Se ha desseleccionado el servicio ha modificar.\n");
                return;
            }

            Tramo r = BuscarRelacion2(id_estacionOrigen, id_estacionDestino, servicioSeleccionado);

            if (r == null) return;
            

            lbxEstacionesOrigenMod.SelectedItem = r.Estacion;
            lbxEstacionesDestinoMod.SelectedItem = r.Estacion1;
            txtDistanciaRelacionMod.Text = r.Distancia.ToString();
            txtTiempoViajeRelacionMod.Text = r.TiempoViaje.ToString();

        }

        private void lbxServiciosEliminar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Servicio s = (Servicio)lbxServiciosEliminar.SelectedItem;

            if (s == null) return;

            lbxTrazasAsociadasEliminar.Items.Clear();
            List<Traza_X_Servicio> trazaServiciosAsociados = context.Traza_X_Servicio.Where(x => x.Id_Servicio == s.Id).ToList<Traza_X_Servicio>();

            foreach (Traza_X_Servicio ts in trazaServiciosAsociados)
                lbxTrazasAsociadasEliminar.Items.Add(ts.Traza);
        }

        private void dgvRelacionesCrear_Leave(object sender, EventArgs e)
        {
            txtDistanciaRelacionCrear.Enabled = true;
            txtTiempoViajeRelacionCrear.Enabled = true;
            txtDistanciaRelacionCrear.Text = "";
            txtTiempoViajeRelacionCrear.Text = "";
            if (dgvRelacionesCrear.Rows.Count != 0)
                dgvRelacionesCrear.CurrentCell = dgvRelacionesCrear[0, 0];
        }

        private void seleccionarFormaciones(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrearServicio)
            {
                if (clbxFormacionesCrear.SelectedIndex > -1)
                {
                    checkFormaciones(clbxFormacionesCrear);
                }
            }
            else
            {
                if (clbxFormacionesMod.SelectedIndex > -1)
                {
                    checkFormaciones(clbxFormacionesMod);
                }
            }
        }

        private void checkFormaciones(CheckedListBox clb)
        {
            if (clb.GetItemChecked(clb.SelectedIndex))
            {
                clb.SetItemChecked(clb.SelectedIndex, false);
            }
            else
            {
                clb.SetItemChecked(clb.SelectedIndex, true);
            }
        }

        private void btnProgramacionCrear_Click(object sender, EventArgs e)
        {
            using (frmProgramacionServicio frmProg = new frmProgramacionServicio(programacionCrear))
            {
                if (frmProg.ShowDialog() == DialogResult.OK)
                {
                    programacionCrear = frmProg.ProgramacionStr;
                }
            }
        }

        private void btnProgramacionModificar_Click(object sender, EventArgs e)
        {
            using (frmProgramacionServicio frmProg = new frmProgramacionServicio(programacionModificar))
            {
                if (frmProg.ShowDialog() == DialogResult.OK)
                {
                    programacionModificar = frmProg.ProgramacionStr;
                }
            }
        }

        private void btnModificarRelacionTabCrear_Click(object sender, EventArgs e)
        {

        }

        private void btnModificarRelacionMod_Click(object sender, EventArgs e)
        {
            Servicio servicioSeleccionado = (Servicio)lbxServiciosModificar.SelectedItem;
            string errorMsj = "";
            Estacion estacionOrigen = (Estacion)lbxEstacionesOrigenMod.SelectedItem;
            Estacion estacionDestino = (Estacion)lbxEstacionesDestinoMod.SelectedItem;

            if (servicioSeleccionado == null)
                errorMsj += "No se ha seleccionado ningún servicio para ser modificado.\n";

            if (dgvRelacionesMod.SelectedRows.Count != 1)
                errorMsj += "Debe seleccionar un fila para poder ser borrada.\n";

            if (estacionOrigen == null)
            {
                MessageBox.Show("Debe seleccionar una estación de origen.\n");
                return;
            }
            if (estacionDestino == null)
            {
                MessageBox.Show("Debe seleccionar una estación de destino.\n");
                return;
            }
            if (!Util.EsNumerico(txtDistanciaRelacionMod.Text))
                errorMsj += "Distancia entre estaciones: Incompleto ó Incorrecto.\n";
            else if (int.Parse(txtDistanciaRelacionMod.Text) == 0)
                errorMsj += "Distancia: El valor debe ser positivo.\n";
            if (!Util.EsNumerico(txtTiempoViajeRelacionMod.Text))
                errorMsj += "Tiempo de Viaje: Incompleto ó Incorrecto.\n";
            else if (int.Parse(txtTiempoViajeRelacionMod.Text) == 0)
                errorMsj += "Tiempo de Viaje: El valor debe ser positivo.\n";
            if (BuscarRelacion(estacionOrigen.Id, estacionDestino.Id) != null)
                errorMsj += "Esa relación ya existe en el servicio.\n";
            if (estacionDestino.Id == estacionOrigen.Id)
                errorMsj += "Una estación no puede ser origen y destino al mismo tiempo.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                var rowIndex = dgvRelacionesMod.SelectedRows[0].Index;
                int id_estacionOrigen = Convert.ToInt32(dgvRelacionesMod.SelectedRows[0].Cells["Id_EstacionOrigen"].Value);
                int id_estacionDestino = Convert.ToInt32(dgvRelacionesMod.SelectedRows[0].Cells["Id_EstacionDestino"].Value);

                Tramo r = servicioSeleccionado.Tramo.Where(x => x.Estacion.Id == id_estacionOrigen && x.Estacion1.Id == id_estacionDestino).FirstOrDefault();
                if (r != null)
                {
                    servicioSeleccionado.Tramo.Remove(r);
                    /*Esto lo hago con try catch xq si la relacion fue agregada durante la modificacion del servicio la misma
                     todavia no guardada en la base y por lo tanto tampoco pertenece al contexto. Recien va a aparecer en el contexto cuando se guarden los cambios en la bd*/
                    try
                    { context.Tramo.Remove(r); }
                    catch
                    { }
                }
                dgvRelacionesMod.Rows.Remove(dgvRelacionesMod.SelectedRows[0]);

                Tramo nuevaRelacion = new Tramo();
                nuevaRelacion.EstSigEsParada = true;
                nuevaRelacion.Distancia = Convert.ToInt32(txtDistanciaRelacionMod.Text);
                nuevaRelacion.TiempoViaje = Convert.ToInt32(txtTiempoViajeRelacionMod.Text);
                nuevaRelacion.Id_Estacion_Anterior = estacionOrigen.Id;
                nuevaRelacion.Id_Estacion_Siguiente = estacionDestino.Id;
                nuevaRelacion.Estacion = estacionOrigen;
                nuevaRelacion.Estacion1 = estacionDestino;

                servicioSeleccionado.Tramo.Add(nuevaRelacion);
                dgvRelacionesMod.Rows.Insert(rowIndex, estacionOrigen.Nombre, estacionDestino.Nombre, estacionOrigen.Id, estacionDestino.Id);
                dgvRelacionesMod.CurrentCell = dgvRelacionesMod[0,rowIndex];

            }
            else
                MessageBox.Show(errorMsj);

         

            if (string.IsNullOrEmpty(errorMsj))
            {
                
            }
            else
                MessageBox.Show(errorMsj);
        }
    }
}
