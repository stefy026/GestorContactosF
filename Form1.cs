using System;
using System.Windows.Forms;

namespace GestorContactos
{
    public partial class FrmContactos : Form
    {
        private GestorContactos gestor;
        private int idSeleccionado;

        public FrmContactos()
        {
            InitializeComponent();

            gestor = new GestorContactos();
            idSeleccionado = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigurarTabla();
            CargarContactos();
        }

        private void ConfigurarTabla()
        {
            dgvContactos.AutoGenerateColumns = true;
            dgvContactos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContactos.MultiSelect = false;
            dgvContactos.ReadOnly = true;
            dgvContactos.AllowUserToAddRows = false;
            dgvContactos.AllowUserToDeleteRows = false;
        }

        private void CargarContactos()
        {
            dgvContactos.DataSource = null;
            dgvContactos.DataSource = gestor.ObtenerContactos();

            CambiarEncabezadosTabla();

            dgvContactos.ClearSelection();
        }

        private void CambiarEncabezadosTabla()
        {
            if (dgvContactos.Columns["Id"] != null)
            {
                dgvContactos.Columns["Id"].HeaderText = "ID";
            }

            if (dgvContactos.Columns["Nombre"] != null)
            {
                dgvContactos.Columns["Nombre"].HeaderText = "Nombre";
            }

            if (dgvContactos.Columns["NumeroTelefono"] != null)
            {
                dgvContactos.Columns["NumeroTelefono"].HeaderText = "Teléfono";
            }

            if (dgvContactos.Columns["CorreoElectronico"] != null)
            {
                dgvContactos.Columns["CorreoElectronico"].HeaderText = "Correo";
            }

            if (dgvContactos.Columns["Direccion"] != null)
            {
                dgvContactos.Columns["Direccion"].HeaderText = "Dirección";
            }

            dgvContactos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtBuscar.Clear();

            idSeleccionado = 0;
            dgvContactos.ClearSelection();

            txtNombre.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                gestor.AgregarContacto(
                    txtNombre.Text,
                    txtTelefono.Text,
                    txtCorreo.Text,
                    txtDireccion.Text
                );

                MessageBox.Show(
                    "Contacto agregado exitosamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CargarContactos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show(
                        "Primero selecciona un contacto de la tabla.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                gestor.EditarContacto(
                    idSeleccionado,
                    txtNombre.Text,
                    txtTelefono.Text,
                    txtCorreo.Text,
                    txtDireccion.Text
                );

                MessageBox.Show(
                    "Contacto actualizado exitosamente.",
                    "Actualización exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CargarContactos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show(
                        "Primero selecciona un contacto de la tabla.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Seguro que deseas eliminar este contacto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    gestor.EliminarContacto(idSeleccionado);

                    MessageBox.Show(
                        "Contacto eliminado exitosamente.",
                        "Eliminado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    CargarContactos();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvContactos.DataSource = null;
            dgvContactos.DataSource = gestor.BuscarContacto(txtBuscar.Text);

            CambiarEncabezadosTabla();

            dgvContactos.ClearSelection();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            CargarContactos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvContactos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvContactos.Rows[e.RowIndex];

                if (fila.Cells["Id"].Value != null)
                {
                    idSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);
                }

                if (fila.Cells["Nombre"].Value != null)
                {
                    txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                }
                else
                {
                    txtNombre.Text = "";
                }

                if (fila.Cells["NumeroTelefono"].Value != null)
                {
                    txtTelefono.Text = fila.Cells["NumeroTelefono"].Value.ToString();
                }
                else
                {
                    txtTelefono.Text = "";
                }

                if (fila.Cells["CorreoElectronico"].Value != null)
                {
                    txtCorreo.Text = fila.Cells["CorreoElectronico"].Value.ToString();
                }
                else
                {
                    txtCorreo.Text = "";
                }

                if (fila.Cells["Direccion"].Value != null)
                {
                    txtDireccion.Text = fila.Cells["Direccion"].Value.ToString();
                }
                else
                {
                    txtDireccion.Text = "";
                }
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            btnAgregar_Click(sender, e);
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            btnEliminar_Click(sender, e);
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void btnMostrarTodos_Click_1(object sender, EventArgs e)
        {
            btnMostrarTodos_Click(sender, e);
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            btnLimpiar_Click(sender, e);
        }

        private void dgvContactos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dgvContactos_CellClick(sender, e);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }
    }
}