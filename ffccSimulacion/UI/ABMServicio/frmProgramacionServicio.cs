using System;
using System.Globalization;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ffccSimulacion.UI.ABMServicio
{
    public partial class frmProgramacionServicio : Form
    {
        string _programacionStr;
        DataGridViewCell cellWithError;

        public string ProgramacionStr
        {
            get
            {
                return _programacionStr;
            }
        }
       
        public frmProgramacionServicio(string programacionInicial)
        {
            _programacionStr = programacionInicial;
            InitializeComponent();
        }

        private void frmProgramacionServicio_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_programacionStr))
            {
                var programacionArr = _programacionStr.Split(';');
                for(var i=0; i<programacionArr.Length; i++)
                {
                    horariosGridView.Rows.Add(programacionArr[i]);
                }
            }
        }

        private void aceptarButton_Click(object sender, EventArgs e)
        {
            _programacionStr = "";
            foreach (DataGridViewRow horarioRow in horariosGridView.Rows)
            {
                if (horarioRow.Cells[0].Value != null)
                {
                    _programacionStr += ";" + horarioRow.Cells[0].Value.ToString();
                }
            }

            if (!string.IsNullOrEmpty(_programacionStr))
            {
                _programacionStr = _programacionStr.Substring(1);
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void importarExcel_Click(object sender, EventArgs e)
        {
            var result  = openExcelDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                horariosGridView.Rows.Clear();
                horariosGridView.Rows.Add("Cargando...");

                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Open(openExcelDialog.FileName);
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                int rcount = worksheet.UsedRange.Rows.Count;
                TimeSpan horarioCell;
                horariosGridView.Rows.Clear();
                for (int i = 0; i < rcount; i++)
                {
                    try
                    {
                        horarioCell = TimeSpan.FromDays(worksheet.Cells[i + 1, 1].Value);
                        horariosGridView.Rows.Add(horarioCell.ToString(@"hh\:mm"));
                    }
                    catch
                    {
                        MessageBox.Show("La fila nro " + i + " del excel tiene un formato inválido.");
                        horariosGridView.Rows.Clear();
                        break;
                    }
                }
            }
        }

        private void horariosGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string value = (string) horariosGridView.CurrentCell.Value;
            if (string.IsNullOrEmpty(value))
            {
                if (e.RowIndex != horariosGridView.Rows.Count-1)
                {
                    horariosGridView.Rows.RemoveAt(e.RowIndex);
                }
            }
            else if (!esHorarioValido(value))
            {
                cellWithError = horariosGridView.CurrentCell;
                MessageBox.Show("Formato inválido.");
                horariosGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
            }
        }

        private bool esHorarioValido(string horario)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DateTime outDate;
            return DateTime.TryParseExact(horario, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out outDate);
        }

        private void horariosGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (cellWithError != null)
            {
                horariosGridView.CurrentCell = cellWithError;
                horariosGridView.BeginEdit(true);
                cellWithError = null;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            horariosGridView.Rows.Clear();
        }

        private void btnInsertRow_Click(object sender, EventArgs e)
        {
            var rowIndex = horariosGridView.CurrentCell.RowIndex;
            var colIndex = horariosGridView.CurrentCell.ColumnIndex;
            horariosGridView.Rows.Insert(rowIndex, 1);
            horariosGridView.CurrentCell = horariosGridView.Rows[rowIndex].Cells[colIndex];
            horariosGridView.BeginEdit(true);
        }

    }
}
