using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_INGENIERIA
{
    public partial class Registrar_Usuarios : Form
    {
        Conexion conexion = new Conexion();
        public Registrar_Usuarios()
        {
            InitializeComponent();
        }

        private void tb_guardar_Click(object sender, EventArgs e)
        {
            int usuario = conexion.buscar_si_exite("select count(USU_CEDULA) from T_USUARIO where USU_CEDULA='" + tb_cedula.Text + "'");
            if (usuario ==0)
            {
                String Guadar = "insert into T_USUARIO values('" + tb_cedula.Text + "','" + tb_password.Text + "','" + tb_nombre.Text + "','" + tb_telefono.Text + "','" + tb_correo.Text + "','" + tb_direccion.Text + "','" + cb_sexo.Text + "','" + dtp_fecha_nacimiento.Text + "') ;";
                conexion.Ejecutar(Guadar, "Usuario Guardado Con Èxito", "Error al Guardar");
                this.Dispose();
                Ingreso frm_ingreso = new Ingreso();
                frm_ingreso.Show();

            }
            else
                {
                MessageBox.Show("Error, el usuario ya existe", "Cedula Repetida", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void Registrar_Usuarios_Load(object sender, EventArgs e)
        {
 
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bt_modificar_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ingreso frm_ingreso = new Ingreso();
            frm_ingreso.Show();
            this.Hide();
        }
    }
}
