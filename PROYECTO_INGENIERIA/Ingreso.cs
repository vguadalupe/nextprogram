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
    public partial class Ingreso : Form
    {
        
        Conexion conexion = new Conexion();
        public static String cedula = null;

        public Ingreso()
        {
            InitializeComponent();
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            string usuario = conexion.buscar_consulta_string("select USU_CEDULA,USU_PASSWORD from T_USUARIO where USU_CEDULA='" + tb_usuario.Text + "'", 0);
            string contraseña = conexion.buscar_consulta_string("select USU_CEDULA,USU_PASSWORD from T_USUARIO where USU_CEDULA='" + tb_usuario.Text + "'", 1);
           // MessageBox.Show(usuario + " " + contraseña);
            if (tb_usuario.Text.Equals(usuario) && tb_contraseñaa.Text.Equals(contraseña))
            {
                ///aca llamar a la ventana desepues de login
                cedula = tb_usuario.Text;
                Menu frm_menu = new Menu();
                frm_menu.Show();
                this.Hide();           

                tb_usuario.Text = "";
                tb_contraseñaa.Text = "";
                // MessageBox.Show("Genial logeo Perfecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Usuario no registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }




        private void Btn_salir_Click(object sender, EventArgs e)
        {
        }

        private void tb_usuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {






        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_salir_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btn_registrar_Click(object sender, EventArgs e)
        {
         
            Registrar_Usuarios frm_registrar = new Registrar_Usuarios();
            this.Hide();
            frm_registrar.Show();

        }
    }
}
