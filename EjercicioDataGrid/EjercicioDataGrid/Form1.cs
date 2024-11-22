using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjercicioDataGrid
{
    public partial class Form1 : Form
    {
        private List<Ciudad> ciudades;
        private Ciudad ciudadSel = new Ciudad();
        public Form1()
        {
            InitializeComponent();
            ciudades = new List<Ciudad>();
        }
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            Ciudad ciudad = new Ciudad();
            ciudad.ID = int.Parse(txtCodigo.Text);
            ciudad.Nombre = txtNombre.Text;

            int index = ciudades.FindIndex(item => item.ID == ciudad.ID);

            if (index != -1)
            {
                ciudades[index] = ciudad;
            }
            else
            {
                ciudades.Add(ciudad);
            }

            MostrarDatos();
            LimpiarCodigo();
        }

        private void MostrarDatos()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = ciudades;
        }

        private void LimpiarCodigo()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtCodigo.Focus();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "Archivos DAT (*.dat)|*.dat";
                saveFileDialog1.Title = "Guardar Archivo";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ciudadArchivo archivo = new ciudadArchivo();

                    archivo.GuardarArchivo(ciudades, saveFileDialog1.FileName);
                    MessageBox.Show("Se ha guardado el archivo", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Archivos DAT (*.dat)|*.dat|Todos los archivos (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            { 
                string ruta = openFileDialog1.FileName;

                ciudadArchivo archivo = new ciudadArchivo();
                ciudades = archivo.CargarCiudades(ruta);

                MostrarDatos();
            } else
            {
                MessageBox.Show("No se selecciono ningún archivo.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ciudadSel.ID == 0 || string.IsNullOrEmpty(ciudadSel.Nombre))
                {
                    MessageBox.Show("Por favor, seleccione una ciudad para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                ciudades.Remove(ciudadSel);
                MessageBox.Show("Ciudad eliminada...", "Ciudad", MessageBoxButtons.OK, MessageBoxIcon.Information);

  
                ciudadSel = new Ciudad(); 
                MostrarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow currentRow = dgvRegistros.CurrentRow;
            if (currentRow != null )
            {
                ciudadSel.ID = int.Parse(currentRow.Cells[0].Value.ToString());
                ciudadSel.Nombre = currentRow.Cells[1].Value.ToString();
                txtCodigo.Text = ciudadSel.ID.ToString();
                txtNombre.Text = ciudadSel.Nombre;
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "Archivos DAT (*.dat)|*.dat";
                saveFileDialog1.Title = "Guardar Archivo";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ciudadArchivo archivo = new ciudadArchivo();

                    archivo.GuardarArchivo(ciudades, saveFileDialog1.FileName);
                    MessageBox.Show("Se ha guardado el archivo", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
