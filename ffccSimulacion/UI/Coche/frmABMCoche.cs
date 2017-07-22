﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SimuRails.Model.Entities;
using SimuRails.Model.Simulacion;

namespace SimuRails.UI.ABMCoche
{
    public partial class frmABMCoche : Form
    {
        SimuRailsEntities context;

        public frmABMCoche()
        {
            InitializeComponent();

            context = new SimuRailsEntities();
            
            rndEsLocomotoraNo.Checked = true;
            cbxTipoConsumo.Enabled = false;
            txtConsumoMov.Enabled = false;
            txtConsumoParado.Enabled = false;

            cargarCochesEnListas();

            deshabilitarModificar();
        }

        private void btnCocheNuevoCancelar_Click(object sender, EventArgs e)
        {
            this.pnlCoche.Controls.Clear();
        }

        private void btnCocheNuevoAceptar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrear)
                GuardarNuevoCoche();
            if (tabControl1.SelectedTab == tabModificar)
                GuardarModificacionesCoche();
        }
        
        private void GuardarNuevoCoche()
        {
            string errorMsj = "";

            if (!Util.EsAlfaNumerico(txtModelo.Text))
                errorMsj += "Modelo: Incompleto ó Incorrecto.\n";

            if(rndEsLocomotoraSi.Checked)
            {
                if(cbxTipoConsumo.SelectedItem.ToString() == "")
                    errorMsj += "Tipo Consumo: Incompleto ó Incorrecto.\n";

                if(!Util.EsNumerico(txtConsumoMov.Text))
                    errorMsj += "Consumo en movimiento: Incompleto ó Incorrecto.\n";

                if (!Util.EsNumerico(txtConsumoParado.Text))
                    errorMsj += "Consumo parado: Incompleto ó Incorrecto.\n";
            }

            if (!Util.EsNumerico(txtCantidadAsientos.Text))
                errorMsj += "Cantidad Asientos: Incompleto ó Incorrecto.\n";

            if (!Util.EsNumerico(txtMaxLegal.Text))
                errorMsj += "Capacidad Máxima Legal: Incompleto ó Incorrecto.\n";

            if (!Util.EsNumerico(txtMaxReal.Text))
                errorMsj += "Capacidad Máxima Real: Incompleto ó Incorrecto.\n";

            if (String.IsNullOrEmpty(errorMsj))
            {
                try
                {
                    Coche nuevoCoche = new Coche();
                    nuevoCoche.Modelo = txtModelo.Text;
                    if (rndEsLocomotoraSi.Checked)
                    {
                        nuevoCoche.EsLocomotora = true;
                        nuevoCoche.ConsumoMovimiento = Convert.ToInt32(txtConsumoMov.Text);
                        nuevoCoche.ConsumoParado = Convert.ToInt32(txtConsumoParado.Text);
                        if (cbxTipoConsumo.SelectedItem.ToString() == "Eléctrico")
                            nuevoCoche.TipoConsumo = (int)TipoConsumo.Electrico;
                        else
                            nuevoCoche.TipoConsumo = (int)TipoConsumo.Disel;
                    }
                    else
                    {
                        nuevoCoche.EsLocomotora = false;
                        nuevoCoche.ConsumoMovimiento = 0;
                        nuevoCoche.TipoConsumo = 0;
                        nuevoCoche.ConsumoParado = 0;
                    }
                    nuevoCoche.CantidadAsientos = Convert.ToInt32(txtCantidadAsientos.Text);
                    nuevoCoche.MaximoLegalPasajeros = Convert.ToInt32(txtMaxLegal.Text);
                    nuevoCoche.CapacidadMaximaPasajeros = Convert.ToInt32(txtMaxReal.Text);

                    context.Coche.Add(nuevoCoche);
                    context.SaveChanges();
                    LimpiarTabNuevoCoche();
                    cargarCochesEnListas();
                    MessageBox.Show("Coche Guardado.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se Guardo el Coche \n\n" + exc.ToString());
                }
            }
            else
            {
                MessageBox.Show(errorMsj);
            }
        }

        public void GuardarModificacionesCoche()
        {
            string errorMsj = "";

            if(!Util.EsAlfaNumerico(txtModeloMod.Text))
                errorMsj += "Modelo: Incompleto ó Incorrecto.\n";
            if(rdbLocomotoraSiMod.Checked)
            {
                if (cbxTipoConsumoMod.SelectedItem.ToString() == "")
                    errorMsj += "Tipo de consumo: Incompleto ó Incorrecto.\n";
                if(!Util.EsNumerico(txtConsumoMovimientoMod.Text))
                    errorMsj += "Consumo en movimiento: Incompleto ó Incorrecto.\n";
                if(!Util.EsNumerico(txtConsumoParadoMod.Text))
                    errorMsj += "Consumo parado: Incompleto ó Incorrecto.\n";
            }
            if(!Util.EsNumerico(txtCantidadAsientosMod.Text))
                errorMsj += "Cantidad de asientos: Incompleto ó Incorrecto.\n";
            if(!Util.EsNumerico(txtCapacidadLegalMod.Text))
                errorMsj += "Capacidad máxima legal: Incompleto ó Incorrecto.\n";
            if(!Util.EsNumerico(txtCapacidadRealMod.Text))
                errorMsj += "Capacidad máxima real: Incompleto ó Incorrecto.\n";
            if (string.IsNullOrEmpty(errorMsj))
            {
                try
                {
                    Coche cocheSeleccionado = (Coche)lbxCochesModificar.SelectedItem;
                    cocheSeleccionado.Modelo = txtModeloMod.Text;

                    if (rdbLocomotoraSiMod.Checked)
                    {
                        cocheSeleccionado.EsLocomotora = true;
                        if (cbxTipoConsumoMod.SelectedItem.ToString() == "Diesel")
                            cocheSeleccionado.TipoConsumo = (int)TipoConsumo.Disel;
                        else
                            cocheSeleccionado.TipoConsumo = (int)TipoConsumo.Electrico;
                        cocheSeleccionado.ConsumoMovimiento = Convert.ToInt32(txtConsumoMovimientoMod.Text);
                        cocheSeleccionado.ConsumoParado = Convert.ToInt32(txtConsumoParadoMod.Text);
                    }
                    else
                    {
                        cocheSeleccionado.EsLocomotora = false;
                        cocheSeleccionado.ConsumoMovimiento = 0;
                        cocheSeleccionado.ConsumoParado = 0;
                        cocheSeleccionado.TipoConsumo = 0;
                    }

                    cocheSeleccionado.CantidadAsientos = Convert.ToInt32(txtCantidadAsientosMod.Text);
                    cocheSeleccionado.MaximoLegalPasajeros = Convert.ToInt32(txtCapacidadLegalMod.Text);
                    cocheSeleccionado.CapacidadMaximaPasajeros = Convert.ToInt32(txtCapacidadRealMod.Text);

                    context.SaveChanges();

                    //lbxCochesModificar.Items.Clear();
                    //lbxCochesBorrar.Items.Clear();
                    txtModeloMod.Text = "";
                    rdbLocomotoraNoMod.Checked = true;
                    cbxTipoConsumoMod.SelectedIndex = -1;
                    txtConsumoMovimientoMod.Text = "";
                    txtConsumoParadoMod.Text = "";
                    txtCantidadAsientosMod.Text = "";
                    txtCapacidadLegalMod.Text = "";
                    txtCapacidadRealMod.Text = "";
                    cargarCochesEnListas();
                    deshabilitarModificar();
                    MessageBox.Show("Las modificaciones se han guardado exitosamente.\n");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se Guardo el Coche \n\n" + exc.ToString());
                }
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void rndEsLocomotoraSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rndEsLocomotoraSi.Checked)
            {
                cbxTipoConsumo.Enabled = true;
                txtConsumoMov.Enabled = true;
                txtConsumoParado.Enabled = true;
            }
            else
            {
                cbxTipoConsumo.Enabled = false;
                txtConsumoMov.Enabled = false;
                txtConsumoParado.Enabled = false;
            }

        }

        private void btnCocheLimpiar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrear)
                LimpiarTabNuevoCoche();

            if (tabControl1.SelectedTab == tabModificar)
                LimpiarTabModificarCoche();
        }

        private void LimpiarTabNuevoCoche()
        {
            txtModelo.Text = "";
            rndEsLocomotoraNo.Checked = true;
            cbxTipoConsumo.SelectedItem = "";
            txtConsumoMov.Text = "";
            txtConsumoParado.Text = "";
            txtCantidadAsientos.Text = "";
            txtMaxLegal.Text = "";
            txtMaxReal.Text = "";
            cbxTipoConsumo.Enabled = false;
            txtConsumoMov.Enabled = false;
            txtConsumoParado.Enabled = false;
            txtCocheCreBuscar.Clear();
        }

        private void LimpiarTabModificarCoche()
        {
            txtModeloMod.Text = "";
            rdbLocomotoraNoMod.Checked = true;
            cbxTipoConsumoMod.SelectedItem = "";
            txtConsumoMovimientoMod.Text = "";
            txtConsumoParadoMod.Text = "";
            txtCantidadAsientosMod.Text = "";
            txtCapacidadLegalMod.Text = "";
            txtCapacidadRealMod.Text = "";
            cbxTipoConsumoMod.Enabled = false;
            txtConsumoMovimientoMod.Enabled = false;
            txtConsumoParadoMod.Enabled = false;
        }

        private void cargarCochesEnListas()
        {
            lbxCochesBorrar.Items.Clear();
            lbxCochesModificar.Items.Clear();
            lstCocheCrear.Items.Clear();

            List<Coche> listaCoches = context.Coche.ToList<Coche>();
            foreach(Coche c in listaCoches)
            {
                lbxCochesBorrar.Items.Add(c);
                lbxCochesModificar.Items.Add(c);
                lstCocheCrear.Items.Add(c);
            }

            /*Se define cual propiedad de los objeto que estan dentro de las list box se van a mostrar*/
            //lbxCochesBorrar.DisplayMember = "Modelo";
            //lbxCochesModificar.DisplayMember = "Modelo";
        }

        private void btnBorrarCoche_Click(object sender, EventArgs e)
        {
            string errormsj = "";

            Coche unCoche = (Coche)lbxCochesBorrar.SelectedItem;
            if (lbxCochesBorrar.SelectedIndex < 0)
            {
                errormsj += "No se ha seleccionado ningun coche para eliminar\n";
            }
            else if (context.Formacion_X_Coche.Where(x => x.Id_Coche == unCoche.Id).Count() != 0)
            {
                errormsj += "El coche no puede borrarse porque pertenece a una o mas formaciones\n";
            }
            if (string.IsNullOrEmpty(errormsj))
            {
                try
                {
                    if (MessageBox.Show("El coche se eliminará de manera permanente.¿Desea continuar?", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

                    /*Se verifica que el coche no pertenezca a ninguna formacion antes de borrarlo*/
                    context.Coche.Remove(unCoche);
                    context.SaveChanges();
                    //lbxCochesBorrar.Items.Remove(unCoche);
                    //lbxCochesModificar.Items.Remove(unCoche);
                    cargarCochesEnListas();
                    MessageBox.Show("El Coche ha sido borrado");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se borro el Coche \n\n" + exc.ToString());
                }
            }
            else
            {
                MessageBox.Show(errormsj);
            }
        }

        private void lbxCochesModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxCochesModificar.SelectedIndex > -1)
            {
                habilitarModificar();
                Coche unCoche = (Coche)lbxCochesModificar.SelectedItem;
                txtModeloMod.Text = unCoche.Modelo;
                if (unCoche.EsLocomotora == true)
                {
                    rdbLocomotoraSiMod.Checked = true;

                    if (unCoche.TipoConsumo == (int)TipoConsumo.Disel)
                        cbxTipoConsumoMod.SelectedItem = "Diesel";
                    else
                        cbxTipoConsumoMod.SelectedItem = "Eléctrico";

                    txtConsumoMovimientoMod.Text = unCoche.ConsumoMovimiento.ToString();
                    txtConsumoParadoMod.Text = unCoche.ConsumoParado.ToString();

                }
                else
                {
                    rdbLocomotoraNoMod.Checked = true;
                    cbxTipoConsumoMod.SelectedIndex = -1;
                    cbxTipoConsumoMod.Enabled = false;
                    txtConsumoMovimientoMod.Enabled = false;
                    txtConsumoParadoMod.Enabled = false;
                    txtConsumoMovimientoMod.Text = "0";
                    txtConsumoParadoMod.Text = "0";
                }

                txtCantidadAsientosMod.Text = unCoche.CantidadAsientos.ToString();
                txtCapacidadLegalMod.Text = unCoche.MaximoLegalPasajeros.ToString();
                txtCapacidadRealMod.Text = unCoche.CapacidadMaximaPasajeros.ToString();
            }
        }

        private void rdbLocomotoraSiMod_CheckedChanged(object sender, EventArgs e)
        {
            cbxTipoConsumoMod.Enabled = true;
            txtConsumoMovimientoMod.Enabled = true;
            txtConsumoParadoMod.Enabled = true;
        }

        private void rdbLocomotoraNoMod_CheckedChanged(object sender, EventArgs e)
        {
            cbxTipoConsumoMod.Enabled = false;
            cbxTipoConsumoMod.SelectedIndex = -1;
            txtConsumoMovimientoMod.Enabled = false;
            txtConsumoMovimientoMod.Text = "";
            txtConsumoParadoMod.Enabled = false;
            txtConsumoParadoMod.Text = "";
        }

        private void cbxTipoConsumo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTipoConsumo.SelectedItem.ToString() == "Diesel")
            {
                lblConsumoMovCrear.Text = "L / Km";
                lblConsumoParadoCrear.Text = "L / Km";
            }
            else//Si no es diesel es electrico
            {
                lblConsumoMovCrear.Text = "KW / H";
                lblConsumoParadoCrear.Text = "KW / H";
            }
        }

        private void cbxTipoConsumoMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTipoConsumoMod.SelectedItem.ToString() == "Diesel")
            {
                lblConsumoMovMod.Text = "L / Km";
                lblConsumoParadoMod.Text = "L / Km";
            }
            else//Si no es diesel es electrico
            {
                lblConsumoMovMod.Text = "KW / H";
                lblConsumoParadoMod.Text = "KW / H";
            }
        }

        private void seleccionarPestaña(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrear)
            {
                btnCocheLimpiar.Enabled = true;
                btnCocheNuevoAceptar.Enabled = true;
                LimpiarTabNuevoCoche();
                LimpiarTabModificarCoche();
                deshabilitarModificar();
            }
            else if (tabControl1.SelectedTab == tabModificar)
            {
                btnCocheLimpiar.Enabled = true;
                btnCocheNuevoAceptar.Enabled = true;
            }
            else
            {
                btnCocheLimpiar.Enabled = false;
                btnCocheNuevoAceptar.Enabled = false;
                LimpiarTabNuevoCoche();
                LimpiarTabModificarCoche();
                deshabilitarModificar();
                lbxCochesModificar.SelectedIndex = -1;
            }
        }

        private void buscarCoche(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab == tabCrear)
            {
                actualizarCoche(txtCocheCreBuscar, lstCocheCrear);
            }
            else if (tabControl1.SelectedTab == tabModificar)
            {
                actualizarCoche(txtCocheModBuscar, lbxCochesModificar);
                LimpiarTabModificarCoche();
            }
            else
            {
                actualizarCoche(txtCocheEliBuscar, lbxCochesBorrar);
            }
        }

        private void actualizarCoche(TextBox buscarCoche, ListBox resultados)
        {
            if (!string.IsNullOrEmpty(buscarCoche.Text) && Util.EsAlfaNumerico(buscarCoche.Text))
            {
                resultados.Items.Clear();
                context.Coche.Where(x => x.Modelo.Contains(buscarCoche.Text)).ToList().ForEach(y => resultados.Items.Add(y));
            }
            else
            {
                cargarCochesEnListas();
            }
            deshabilitarModificar();
        }

        private void formacionesAsociadas(object sender, EventArgs e)
        {
            if(lbxCochesBorrar.SelectedIndex > -1)
            {
                Formacion f;
                lstCocheEliFormaciones.Items.Clear();
                List<Formacion_X_Coche> fc = context.Formacion_X_Coche.Where(x => x.Id_Coche == ((Coche)lbxCochesBorrar.SelectedItem).Id).ToList<Formacion_X_Coche>();
                foreach(var item in fc)
                {
                    f = context.Formacion.Where(x => x.Id == item.Id_Formacion).FirstOrDefault();
                    lstCocheEliFormaciones.Items.Add(f);
                }
            }
        }

        private void habilitarModificar()
        {
            txtCantidadAsientosMod.Enabled = true;
            txtCapacidadLegalMod.Enabled = true;
            txtCapacidadRealMod.Enabled = true;
            txtConsumoMovimientoMod.Enabled = true;
            txtConsumoParadoMod.Enabled = true;
            txtModeloMod.Enabled = true;
            cbxTipoConsumoMod.Enabled = true;
        }

        private void deshabilitarModificar()
        {
            txtCantidadAsientosMod.Enabled = false;
            txtCapacidadLegalMod.Enabled = false;
            txtCapacidadRealMod.Enabled = false;
            txtConsumoMovimientoMod.Enabled = false;
            txtConsumoParadoMod.Enabled = false;
            txtModeloMod.Enabled = false;
            cbxTipoConsumoMod.Enabled = false;
        }
    }
}
