using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {

            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_Rol().Listar();
                        
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;


            //Mostrar todos los clientes registrados
            List<Cliente> listaCliente = new CN_Cliente().Listar();

            foreach (Cliente item in listaCliente)
            {
                dgvdata.Rows.Add(new object[] {"", item.IdCliente, item.Documento,
                item.NombreCompleto, item.Correo, item.Telefono,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "No Activo"
            });
            }

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {

            string mensaje = string.Empty;

            Cliente objCliente = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombrecompleto.Text,
                Correo = txtcorreo.Text,
                Telefono = txttelefono.Text,
                
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objCliente.IdCliente == 0)
            {
                int idClientegenerado = new CN_Cliente().Registrar(objCliente, out mensaje);

                if (idClientegenerado != 0)
                {
                    dgvdata.Rows.Add(new object[] {"", txtid.Text, txtdocumento.Text,
                            txtnombrecompleto.Text, txtcorreo.Text, txtclave.Text,
                            ((OpcionCombo)cborol.SelectedItem).Valor.ToString(),
                            ((OpcionCombo)cborol.SelectedItem).Texto.ToString(),
                            ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                            ((OpcionCombo)cboestado.SelectedItem).Texto.ToString()
                        });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }

            }
    }
}
