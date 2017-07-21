﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SimuRails.UI.ABMServicio;
using SimuRails.Model.Entities;

namespace SimuRails.UI.ABMTraza
{
    public partial class frmABMTraza : Form
    {
        SimuRailsEntities context;

        Trazas trazaSeleccionada;

        public frmABMTraza()
        {
            InitializeComponent();

            context = new SimuRailsEntities();

            clbTraCreServicios.DisplayMember = "Nombre";

            clbTraModServicios.DisplayMember = "Nombre";

            cargarServicios();

            cargarTrazas();
        }

        #region Acciones de los Botones
        /*
         * Accion al pulsar el boton 'Cancelar'
         */ 
        private void btnTraCancelar_Click(object sender, EventArgs e)
        {
            this.pnlTraza.Controls.Clear();
            this.Close();
        }

        /*
         * Accion al pulsar el boton 'Limpiar'
         */
        private void btnTraLimpiar_Click(object sender, EventArgs e)
        {
            if (tclTraza.SelectedTab == tabCrearTraza)
            {
                limpiarFormularioCrear();
            }
            else
            {
                limpiarFormularioModificar();
            }
        }

        /*
         * Accion al pulsar el boton 'Agregar Servicio'
         */
        private void btnTraCreAgregarServicio_Click(object sender, EventArgs e)
        {
            agregarNuevoServicio();
        }

        private void btnTraModAgregarServicio_Click(object sender, EventArgs e)
        {
            agregarNuevoServicio();
        }

        /*
         * Accion al pulsar 'Borrar Traza'
         */ 
        private void btnTraElBorrarTraza_Click(object sender, EventArgs e)
        {
            eliminarTraza();
        }

        /*
         * Accion al pulsar el boton 'Aceptar'
         */
        private void btnTraAceptar_Click(object sender, EventArgs e)
        {
            if(tclTraza.SelectedTab == tabCrearTraza)
            {
                agregarTraza();
            }
            else if (tclTraza.SelectedTab == tabModificarTraza)
            {
                modificarTraza();
            }
        }

        #endregion

        #region Casos de Uso

        /*
         * Caso de Uso: 'Agregar Traza'
         */ 
        private void agregarTraza()
        {
            string errorStr = "";

            if(!Util.EsAlfaNumerico(txtTraCreNombre.Text))
            {
                errorStr += "Nombre: Incompleto ó Incorrecto\n";
            }
            if (clbTraCreServicios.CheckedItems.Count == 0)
            {
                errorStr += "La traza no tiene servicios seleccionados\n";
            }

            if (errorStr.Length == 0)
            {
                try
                {
                    Trazas nuevaTraza = new Trazas();

                    nuevaTraza.Nombre = txtTraCreNombre.Text;

                    foreach (Servicios s in clbTraCreServicios.CheckedItems)
                    {
                        nuevaTraza.AgregarServicio(s);
                    }

                    context.Trazas.Add(nuevaTraza);

                    context.SaveChanges();

                    cargarTrazas();

                    limpiarFormularioCrear();

                    MessageBox.Show("Traza Guardada");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Traza No Guardada\n\nError\n\n" + exc.Message);
                }
            }
            else
            {
                MessageBox.Show(errorStr);
            }
        }

        /*
         * Caso de Uso: 'Modificar Traza'
         */
        private void modificarTraza()
        {
            string errorStr = "";

            if (!Util.EsAlfaNumerico(txtTraModNombre.Text))
            {
                errorStr += "Nombre: Incompleto/Incorrecto\n";
            }

            if (clbTraModServicios.CheckedItems.Count == 0)
            {
                errorStr += "La traza no tiene servicios seleccionados.\n";
            }

            if (errorStr.Length == 0)
            {
                try
                {
                    trazaSeleccionada = (Trazas)lstTraModTrazas.SelectedItem;

                    trazaSeleccionada.Nombre = txtTraModNombre.Text;

                    //borro las relaciones existentes en la base de datos
                    context.Trazas_X_Servicios.Where(x => x.Id_Traza == trazaSeleccionada.Id).ToList().ForEach(y => context.Trazas_X_Servicios.Remove(y));

                    //agrego la configuracion del checklistbox
                    foreach (Servicios s in clbTraModServicios.CheckedItems)
                    {
                        trazaSeleccionada.AgregarServicio(s);
                    }

                    context.SaveChanges();

                    txtTraModBuscar.Clear();    

                    MessageBox.Show("Las modificaciones han sido guardadas.");

                    cargarTrazas();

                    limpiarFormularioModificar();
                }
                catch(Exception exc)
                {
                    MessageBox.Show("Las modificaciones NO han sido guardadas.\n\nError\n\n" + exc.Message);
                }
            }
            else
            {
                MessageBox.Show(errorStr);
            }
        }
        
        /*
         * Caso de Uso: 'Eliminar Traza'
         */
        private void eliminarTraza()
        {
            string errorMsj = "";
            trazaSeleccionada = (Trazas)lstTraEliTrazas.SelectedItem;

            if (lstTraEliTrazas.SelectedItem == null)
            {
                errorMsj += "No se ha seleccionado ninguna traza para eliminar.\n";
            }
            else if(context.Simulaciones.Any(x => x.Id_Traza == trazaSeleccionada.Id))
            {
                errorMsj += "La traza no puede eliminarse porque pertenece a una simulación.\n";
            }

            if (string.IsNullOrEmpty(errorMsj))
            {
                try
                {
                    if (MessageBox.Show("La traza se eliminará de manera permanente. ¿Desea continuar?", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

                    trazaSeleccionada = (Trazas)lstTraEliTrazas.SelectedItem;

                    //borro todos los servicios asignados a la traza
                    context.Trazas_X_Servicios.Where(x => x.Id_Traza == trazaSeleccionada.Id).ToList().ForEach(y => context.Trazas_X_Servicios.Remove(y));

                    context.Trazas.Remove(trazaSeleccionada);

                    context.SaveChanges();

                    cargarTrazas();

                    limpiarFormularioCrear();

                    limpiarFormularioModificar();

                    txtTraEliBuscar.Clear();

                    txtTraCreBuscar.Clear();

                    txtTraModBuscar.Clear();

                    MessageBox.Show("Traza Eliminada");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("La traza No ha sido eliminada.\n\nError:\n\n" + exc.Message);
                }
            }
            else
            {
                MessageBox.Show(errorMsj);
            }
        }

        #endregion

        #region Cargar Listas de Servicios y Trazas
        private void cargarServicios()
        {
            clbTraCreServicios.Items.Clear();

            clbTraModServicios.Items.Clear();

            context.Servicios.ToList().ForEach(x => { clbTraCreServicios.Items.Add(x); clbTraModServicios.Items.Add(x); });
        }

        private void cargarTrazas()
        {
            lstTraEliTrazas.Items.Clear();

            lstTraModTrazas.Items.Clear();

            lstTraCreResultados.Items.Clear();

            context.Trazas.ToList().ForEach(x => { lstTraModTrazas.Items.Add(x); lstTraEliTrazas.Items.Add(x); lstTraCreResultados.Items.Add(x); });
        }

        #endregion

        #region Metodos Auxiliares

        /*
         * Evento que sucede cuando se selecciona una Traza de la lista para modificar                                                    
         */
        private void seleccionarTraza(object sender, EventArgs e)
        {
            if (lstTraModTrazas.SelectedIndex > -1)
            {
                habilitarModificar();

                trazaSeleccionada = (Trazas)lstTraModTrazas.SelectedItem;

                txtTraModNombre.Text = trazaSeleccionada.Nombre;

                for (int i = 0; i < clbTraModServicios.Items.Count; i++)
                {
                    clbTraModServicios.SetItemChecked(i, false);
                }

                foreach (Servicios s in trazaSeleccionada.ServiciosDisponibles)
                {
                    clbTraModServicios.SetItemChecked(clbTraModServicios.Items.IndexOf(s), true);
                }
            }
        }

        private void agregarNuevoServicio()
        {
            frmABMServicio frmServicio = new frmABMServicio();

            frmServicio.ShowDialog(this);

            cargarServicios();
        }

        private void limpiarFormularioCrear()
        {
            txtTraCreNombre.Clear();

            txtTraCreBuscar.Clear();

            for (int i = 0; i < clbTraCreServicios.Items.Count; i++)
            {
                clbTraCreServicios.SetItemChecked(i, false);
            }
        }

        private void limpiarFormularioModificar()
        {
            txtTraCreNombre.Clear();

            for (int i = 0; i < clbTraCreServicios.Items.Count; i++)
            {
                clbTraModServicios.SetItemChecked(i, false);
            }
        }

        private void seleccionarPestaña(object sender, TabControlEventArgs e)
        {
            if(tclTraza.SelectedTab == tabCrearTraza)
            {
                btnTraAceptar.Enabled = true;
                btnTraLimpiar.Enabled = true;
                limpiarFormularioCrear();
            }
            else if (tclTraza.SelectedTab == tabModificarTraza)
            {
                btnTraAceptar.Enabled = true;
                btnTraLimpiar.Enabled = true;
                deshabilitarModificar();
                limpiarFormularioModificar();
            }
            else
            {
                btnTraAceptar.Enabled = false;
                btnTraLimpiar.Enabled = false;
            }
        }

        private void actualizarTraza(TextBox buscarTraza, ListBox resultados)
        {
            if (!String.IsNullOrEmpty(buscarTraza.Text) && Util.EsAlfaNumerico(buscarTraza.Text))
            {
                resultados.Items.Clear();
                context.Trazas.Where(x => x.Nombre.Contains(buscarTraza.Text)).ToList().ForEach(y => resultados.Items.Add(y));
            }
            else
            {
                cargarTrazas();
            }
            deshabilitarModificar();
        }

        private void deshabilitarModificar()
        {
            txtTraModNombre.Enabled = false;
            clbTraModServicios.Enabled = false;
        }

        private void habilitarModificar()
        {
            txtTraModNombre.Enabled = true;
            clbTraModServicios.Enabled = true;
        }

        private void buscarTraza(object sender, EventArgs e)
        {
            if (tclTraza.SelectedTab == tabCrearTraza)
            {
                actualizarTraza(txtTraCreBuscar, lstTraCreResultados);
            }
            else if (tclTraza.SelectedTab == tabModificarTraza)
            {
                actualizarTraza(txtTraModBuscar, lstTraModTrazas);
            }
            else
            {
                actualizarTraza(txtTraEliBuscar, lstTraEliTrazas);
            }
        }

        #endregion

        private void simulacionesAsociadas(object sender, EventArgs e)
        {
            if (lstTraEliTrazas.SelectedIndex > -1)
            {
                lstTraEliSimulaciones.Items.Clear();
                context.Simulaciones.Where(x => x.Id_Traza == ((Trazas)lstTraEliTrazas.SelectedItem).Id).ToList().ForEach(y => lstTraEliSimulaciones.Items.Add(y));
            }
        }

        private void seleccionarServicios(object sender, EventArgs e)
        {
            if (tclTraza.SelectedTab == tabCrearTraza)
            {
                if (clbTraCreServicios.SelectedIndex > -1)
                {
                    checkServicios(clbTraCreServicios);
                }
            }
            else
            {
                if (clbTraModServicios.SelectedIndex > -1)
                {
                    checkServicios(clbTraModServicios);
                }
            }
        }

        private void checkServicios(CheckedListBox clb)
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
    }
}