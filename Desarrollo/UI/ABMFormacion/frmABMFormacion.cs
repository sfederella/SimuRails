using SimuRails.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SimuRails.UI.ABMFormacion
{
    public partial class frmABMFormacion : Form
    {
        SimuRailsEntities context;
        private List<Formacion_X_Coche> auxCochesFormacion;

        public frmABMFormacion()
        {
            InitializeComponent();

            context = new SimuRailsEntities();
            auxCochesFormacion = new List<Formacion_X_Coche>();
            CargarListasEstaciones();
            CargarListasFormaciones();
            btnAgregarFormacion.Enabled = false;
            btnBorrarCocheFormacion.Enabled = false;
        }

        private void CargarListasEstaciones()
        {
            foreach (Coche c in context.Coche)
            {
                lbxCochesExistentes.Items.Add(c);
                lbxCochesExistentesMod.Items.Add(c);
            }
        }

        private void CargarListasFormaciones()
        {
            foreach(Formacion f in context.Formacion)
            {
                lbxFormacionesEliminar.Items.Add(f);
                lbxFormacionesModificar.Items.Add(f);
            }
        }
        private void RecalcularTotalesFormacion()
        {
            if (tabControl1.SelectedTab == tabCrearFormacion)
            {
                txtConsumoFormacionParado.Text = auxCochesFormacion.Select(x => x.Coche.ConsumoParado * x.VecesRepetido).Sum().ToString();
                txtConsumoFormacionMov.Text = auxCochesFormacion.Select(x => x.Coche.ConsumoMovimiento * x.VecesRepetido).Sum().ToString();
                txtTotalCoches.Text = auxCochesFormacion.Select(x => x.VecesRepetido).Sum().ToString();
                txtTotalAsientos.Text = auxCochesFormacion.Select(x => x.Coche.CantidadAsientos * x.VecesRepetido).Sum().ToString();
                txtMaxPasajerosLegal.Text = auxCochesFormacion.Select(x => x.Coche.MaximoLegalPasajeros * x.VecesRepetido).Sum().ToString();
                txtMaxRealPasajeros.Text = auxCochesFormacion.Select(x => x.Coche.CapacidadMaximaPasajeros * x.VecesRepetido).Sum().ToString();
            }
            else if(tabControl1.SelectedTab == tabModificarFormacion)
            {
                Formacion f = (Formacion)lbxFormacionesModificar.SelectedItem;
                if (f == null)
                {
                    txtConsumoFormacionMovMod.Text = "0";
                    txtConsumoFormacionParadoMod.Text = "0";
                    txtTotalCochesMod.Text = "0";
                    txtTotalAsientosMod.Text = "0";
                    txtMaximoPasajerlosLegalMod.Text = "0";
                    txtMaximoPasajerosRealMod.Text = "0";
                    return;
                }

                txtConsumoFormacionMovMod.Text = f.Formacion_X_Coche.Select(x => x.Coche.ConsumoMovimiento * x.VecesRepetido).Sum().ToString();
                txtConsumoFormacionParadoMod.Text = f.Formacion_X_Coche.Select(x => x.Coche.ConsumoParado * x.VecesRepetido).Sum().ToString();
                txtTotalCochesMod.Text = f.Formacion_X_Coche.Select(x => x.VecesRepetido).Sum().ToString();
                txtTotalAsientosMod.Text = f.Formacion_X_Coche.Select(x => x.Coche.CantidadAsientos * x.VecesRepetido).Sum().ToString();
                txtMaximoPasajerlosLegalMod.Text = f.Formacion_X_Coche.Select(x => x.Coche.MaximoLegalPasajeros * x.VecesRepetido).Sum().ToString();
                txtMaximoPasajerosRealMod.Text = f.Formacion_X_Coche.Select(x => x.Coche.CapacidadMaximaPasajeros * x.VecesRepetido).Sum().ToString();
            }
        }

        private void LimpiarTabCrearFormacion()
        {
            lbxFormacionesModificar.Items.Clear();
            lbxFormacionesEliminar.Items.Clear();
            CargarListasFormaciones();
            txtNombreFormacion.Text = "";
            txtCantidadCoches.Text = "";
            lbxCochesFormacion.Items.Clear();
            auxCochesFormacion = new List<Formacion_X_Coche>();
            lbxCochesExistentes.SelectedIndex = -1;
            txtCantidadCoches.Enabled = true;
            RecalcularTotalesFormacion();
        }

        private void LimpiarTabModificarFormacion(bool inhabilitarControles = false)
        {
            if (inhabilitarControles)
            {
                txtNombreFormacionMod.Enabled = false;
                lbxCochesExistentesMod.Enabled = false;
                txtCantidadCochesMod.Enabled = false;
                lbxCochesFormacionMod.Enabled = false;
            }

            lbxFormacionesModificar.Items.Clear();
            lbxFormacionesEliminar.Items.Clear();
            CargarListasFormaciones();
            txtNombreFormacionMod.Text = "";
            txtCantidadCochesMod.Text = "";
            lbxCochesFormacionMod.Items.Clear();
            lbxCochesExistentesMod.SelectedIndex = -1;
            lbxFormacionesModificar.SelectedIndex = -1;
            //txtCantidadCochesMod.Enabled = true;
            RecalcularTotalesFormacion();
        }

        private void btnAgregarFormacion_Click(object sender, EventArgs e)
        {
            Coche unCoche = (Coche)lbxCochesExistentes.SelectedItem;

            if (unCoche == null) return;

            string errorMsj = "";
            if (!Util.EsNumerico(txtCantidadCoches.Text))
                errorMsj += "Cantidad: Incompleto ó Incorrecto.\n";
            else if (int.Parse(txtCantidadCoches.Text) < 1)
                errorMsj += "Cantidad: El Valor debe ser Positivo.\n";
            if(lbxCochesExistentes.SelectedItem == null)
                errorMsj += "No se seleccionó ningun coche para agregar a la formación.\n";
            if (auxCochesFormacion.Where(x => x.Id_Coche == unCoche.Id).Count() != 0)
                errorMsj += "El coche ya existe dentro de esta formación.\n";

            if (String.IsNullOrEmpty(errorMsj))
            {
                Formacion_X_Coche fc = new Formacion_X_Coche();
                fc.Coche = unCoche;
                fc.Id_Coche = unCoche.Id;
                fc.VecesRepetido = Convert.ToInt32(txtCantidadCoches.Text);
                auxCochesFormacion.Add(fc);
                lbxCochesFormacion.Items.Add(unCoche);
                RecalcularTotalesFormacion();
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void btnBorrarCocheFormacion_Click(object sender, EventArgs e)
        {
            string errorMsj = "";
            if (lbxCochesFormacion.SelectedItem == null)
                errorMsj += "No se seleccionó ningun coche para sacar de la formación.\n";

            if (String.IsNullOrEmpty(errorMsj))
            {
                Coche unCoche = (Coche)lbxCochesFormacion.SelectedItem;
                auxCochesFormacion.Remove(auxCochesFormacion.Where(x => x.Id_Coche == unCoche.Id).First());
                lbxCochesFormacion.Items.Remove(unCoche);
                RecalcularTotalesFormacion();
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrearFormacion)
                CrearNuevaFormacion();

            if (tabControl1.SelectedTab == tabModificarFormacion)
                GuardarModificacionesFormacion();
        }

        private void CrearNuevaFormacion()
        {
            int cantidadLocomotoras = auxCochesFormacion.Where(x => x.Coche.EsLocomotora == true).Sum(x => x.VecesRepetido);
            string errorMsj = "";

            if (cantidadLocomotoras > 1)
            {
                if (MessageBox.Show("Atención: la formación tiene mas de una locomotora.¿Desea Continuar?", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            }

            if (!Util.EsAlfaNumerico(txtNombreFormacion.Text))
                errorMsj += "Nombre: Incompleto/Incorrecto.\n";
            else if (context.Formacion.Where(x => x.Nombre == txtNombreFormacion.Text).Count() > 0)
                errorMsj += "Nombre: ya existe una formación con el mismo nombre.\n";

            if(auxCochesFormacion.Count == 0)
                errorMsj += "La formación no tiene coches.\n";
            
            if (cantidadLocomotoras == 0)
                errorMsj += "No hay ninguna locomotora en la formación.\n";

            if (String.IsNullOrEmpty(errorMsj))
            {
                try
                {
                    Formacion nuevaFormacion = new Formacion();
                    nuevaFormacion.Nombre = txtNombreFormacion.Text;
                    nuevaFormacion.Formacion_X_Coche = auxCochesFormacion;
                    context.Formacion.Add(nuevaFormacion);
                    context.SaveChanges();
                    MessageBox.Show("La formación se ha creado exitosamente.\n");
                    LimpiarTabCrearFormacion();

                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se guardo la formación \n\n" + exc.ToString());
                }
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void GuardarModificacionesFormacion()
        {
            Formacion formacionSeleccionada = (Formacion)lbxFormacionesModificar.SelectedItem;
            string errorMsj = "";
            List<Formacion_X_Coche> auxFormacionCochesMod = new List<Formacion_X_Coche>();

            if (formacionSeleccionada == null)
            {
                MessageBox.Show("Se ha deseleccionado la formación que iba a ser modificada.\n");
                return;
            }

            List<string> nombresFormaciones = context.Formacion.Where(x => x.Id != formacionSeleccionada.Id).Select(x => x.Nombre).ToList<string>();

            foreach (Formacion_X_Coche fc in lbxCochesFormacionMod.Items)
                auxFormacionCochesMod.Add(fc);

            int cantidadLocomotoras = auxFormacionCochesMod.Where(x => x.Coche.EsLocomotora == true).Sum(x => x.VecesRepetido);

            if (cantidadLocomotoras > 1)
            {
                if (MessageBox.Show("Atención: la formación tiene mas de una locomotora.¿Desea Continuar?", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            }

            if (!Util.EsAlfaNumerico(txtNombreFormacionMod.Text))
                errorMsj += "Nombre: Incompleto/Incorrecto.\n";
            else if (nombresFormaciones.Contains(txtNombreFormacionMod.Text))
                errorMsj += "Nombre: ya existe una formación con el mismo nombre.\n";

            if (auxFormacionCochesMod.Count == 0)
                errorMsj += "La formación no tiene coches.\n";
            if (cantidadLocomotoras == 0)
                errorMsj += "La formación debe tener al menos una locomotora.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                try
                {
                    formacionSeleccionada.Nombre = txtNombreFormacionMod.Text;
                    /*Los coches se fueron guardando en la formacion en el momento que el coche se agrega a la lista dentro del metodo "btnAgregarCocheMod_Click()"
                     o en "btnEliminarCocheMod_Click()"*/

                    context.SaveChanges();
                    MessageBox.Show("La formación se ha guardado exitosamente.\n");
                    LimpiarTabModificarFormacion();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se guardo la formación \n\n" + exc.ToString());
                }
            }
            else
                MessageBox.Show(errorMsj);

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrearFormacion)
                LimpiarTabCrearFormacion();

            if (tabControl1.SelectedTab == tabModificarFormacion)
                LimpiarTabModificarFormacion(true);
        }

        private void lbxCochesFormacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Coche unCoche = (Coche)lbxCochesFormacion.SelectedItem;
            if (unCoche == null) return;
            txtCantidadCoches.Text = auxCochesFormacion.Where(x => x.Id_Coche == unCoche.Id).First().VecesRepetido.ToString();
            txtCantidadCoches.Enabled = false;
        }

        private void lbxCochesExistentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxCochesExistentes.SelectedIndex > -1)
            {
                txtCantidadCoches.Text = "";
                txtCantidadCoches.Enabled = true;
                lbxCochesFormacion.SelectedIndex = -1;
                btnAgregarFormacion.Enabled = true;
                btnBorrarCocheFormacion.Enabled = true;
            }
        }

        private void btnBorrarFormacion_Click(object sender, EventArgs e)
        {
            string errorMsj = "";
            Formacion unaFormacion = (Formacion)lbxFormacionesEliminar.SelectedItem;
            
            if (lbxFormacionesEliminar.SelectedItem == null)
                errorMsj += "No se ha seleccionado ninguna formación para eliminar.\n";
            else if (context.Servicio_X_Formacion.Where(x => x.Formacion.Id == unaFormacion.Id).Count() != 0)
                    errorMsj += "La formación no puede borrarse porque pertenece a un servicio.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                try
                {
                    if (MessageBox.Show("La formación se eliminará de manera permanente.¿Desea continuar?", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
                    Formacion f = (Formacion)lbxFormacionesEliminar.SelectedItem;

                    foreach (Formacion_X_Coche fc in context.Formacion_X_Coche.Where(x => x.Id_Formacion == f.Id))
                        context.Formacion_X_Coche.Remove(fc);
                    
                    context.Formacion.Remove(f);
                    context.SaveChanges();

                    lbxFormacionesEliminar.Items.Remove(f);
                    lbxFormacionesModificar.Items.Remove(f);
                    //para que limpie los otras pestañas y que no quede info inconsistente
                    LimpiarTabCrearFormacion();
                    LimpiarTabModificarFormacion();
                    MessageBox.Show("La formación se ha borrado exitosamente.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se borró la formación \n\n" + exc.ToString());
                }
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void lbxFormacionesModificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxFormacionesModificar.SelectedIndex > -1)
            {
                txtNombreFormacionMod.Enabled = true;
                lbxCochesExistentesMod.Enabled = true;
                txtCantidadCochesMod.Enabled = true;
                lbxCochesFormacionMod.Enabled = true;
                
                txtCantidadCochesMod.Text = "";

                Formacion unaFormacion = (Formacion)lbxFormacionesModificar.SelectedItem;
                txtNombreFormacionMod.Text = unaFormacion.Nombre;
                lbxCochesFormacionMod.Items.Clear();
                foreach (Formacion_X_Coche fc in unaFormacion.Formacion_X_Coche)
                    lbxCochesFormacionMod.Items.Add(fc);
                RecalcularTotalesFormacion();
            }
        }

        private void lbxCochesFormacionMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Formacion_X_Coche fc = (Formacion_X_Coche)lbxCochesFormacionMod.SelectedItem;
            if (fc != null)
            {
                txtCantidadCochesMod.Text = fc.VecesRepetido.ToString();
                txtCantidadCochesMod.Enabled = false;
            }
        }

        private void lbxCochesExistentesMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxCochesExistentesMod.SelectedIndex > -1)
            {
                lbxCochesFormacionMod.SelectedIndex = -1;
                txtCantidadCochesMod.Text = "";
                txtCantidadCochesMod.Enabled = true;
                btnAgregarCocheMod.Enabled = true;
                btnEliminarCocheMod.Enabled = true;
            }
        }

        private void btnAgregarCocheMod_Click(object sender, EventArgs e)
        {
            if (lbxCochesExistentesMod.SelectedIndex > -1)
            {
                string errorMsj = "";
                Coche cocheSeleccionado = (Coche)lbxCochesExistentesMod.SelectedItem;
                Formacion formacionSeleccionada = (Formacion)lbxFormacionesModificar.SelectedItem;

                if (formacionSeleccionada == null)
                    errorMsj += "No se seleccionó ninguna formación para ser modificada.\n";
                if (cocheSeleccionado == null)
                    errorMsj += "No se seleccionó ningún coche para ser agregado.\n";
                if (!Util.EsNumerico(txtCantidadCochesMod.Text))
                    errorMsj += "Cantidad de coches: Incompleto ó Incorrecto.\n";
                else if (Convert.ToInt32(txtCantidadCochesMod.Text) == 0)
                {
                    errorMsj += "Cantidad de coches: El valor debe ser positivo.\n";
                }
                    
                if (formacionSeleccionada.Formacion_X_Coche.Where(x => x.Coche.Modelo == cocheSeleccionado.Modelo).Count() != 0)
                    errorMsj += "El coche ya pertenece a la formación.\n";

                if (string.IsNullOrEmpty(errorMsj))
                {
                    Formacion_X_Coche fc = new Formacion_X_Coche();
                    fc.Coche = cocheSeleccionado;
                    fc.Id_Coche = cocheSeleccionado.Id;
                    fc.VecesRepetido = Convert.ToInt32(txtCantidadCochesMod.Text);
                    formacionSeleccionada.Formacion_X_Coche.Add(fc);
                    lbxCochesFormacionMod.Items.Add(fc);
                    txtCantidadCochesMod.Text = "";
                    RecalcularTotalesFormacion();
                }
                else
                    MessageBox.Show(errorMsj);
            }
        }

        private void btnEliminarCocheMod_Click(object sender, EventArgs e)
        {
            string errorMsj = "";
            Formacion_X_Coche fc = (Formacion_X_Coche)lbxCochesFormacionMod.SelectedItem;
            Formacion formacionSeleccionada = (Formacion)lbxFormacionesModificar.SelectedItem;

            if (fc == null)
                errorMsj += "No se seleccionó ningún coche para ser eliminado.\n";
            if (formacionSeleccionada == null)
                errorMsj += "No se seleccionó ninguna formación para ser modificada.\n";

            if (string.IsNullOrEmpty(errorMsj))
            {
                lbxCochesFormacionMod.SelectedIndex = -1;
                formacionSeleccionada.Formacion_X_Coche.Remove(fc);
                
                /*Esto se hace asi en caso de que el objeto todavia no haya sido agregado/guadado en el contexto de la bd*/
                try { context.Formacion_X_Coche.Remove(fc); }
                catch
                {
                    
                }

                lbxCochesFormacionMod.Items.Remove(fc);
                RecalcularTotalesFormacion();
            }
            else
                MessageBox.Show(errorMsj);
        }

        private void seleccionarPestaña(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab == tabCrearFormacion)
            {
                btnAceptar.Enabled = true;
                btnLimpiar.Enabled = true;
                btnAgregarFormacion.Enabled = false;
                btnBorrarCocheFormacion.Enabled = false;
                lbxCochesExistentes.SelectedIndex = -1;
                
            }
            else if (tabControl1.SelectedTab == tabModificarFormacion)
            {
                btnAceptar.Enabled = true;
                btnLimpiar.Enabled = true;
                btnAgregarCocheMod.Enabled = false;
                btnEliminarCocheMod.Enabled = false;
            }
            else
            {
                btnAceptar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlFormacion.Controls.Clear();
        }

        private void lbxFormacionesEliminar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Formacion f = (Formacion)lbxFormacionesEliminar.SelectedItem;
            
            if (f == null)
                return;

            lbxServiciosAsociadosEliminar.Items.Clear();
            List<Servicio_X_Formacion> serviciosFormacionesAsociados = context.Servicio_X_Formacion.Where(x => x.Id_Formacion == f.Id).ToList<Servicio_X_Formacion>();

            foreach (Servicio_X_Formacion sf in serviciosFormacionesAsociados)
                lbxServiciosAsociadosEliminar.Items.Add(sf.Servicio);
        }
    }
}
