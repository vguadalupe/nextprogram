using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_INGENIERIA
{
    public partial class Menu : Form
    {
        public static String cedula;
        Boolean estado_modificar = false;
        public string codigo_producto;
        Conexion Conexion = new Conexion();
        String comerciante;
        public Menu()
        {
            cedula = Ingreso.cedula;
            InitializeComponent();
            llamar_datos_usuario();
            actualizar_tabla_productos();
            actualizar_tabla_mis_pedidos();
            actualizar_mis_reputaciones();
            bloquear_tx();
            bt_nueva_orden.Enabled = false;
            bt_añadir_orden.Enabled = false;
            bt_cancelar_orden.Enabled = false;
            tb_cantidad_items.Enabled = false;
            bt_completar_orden.Enabled = false;
        }


        private void Menu_Load(object sender, EventArgs e)
        {
            cedula = Ingreso.cedula;
        }

        private void llenar_combobox_comerciante()
        {
            cb_comerciantes.Items.Clear();
            Conexion.llenar_combo(cb_comerciantes, "select USU_NOMBRE from T_USUARIO where not  USU_CEDULA ='"+cedula+"'",0);
        }
        private void llamar_datos_usuario()
        {
            String Consulta = "select * from T_USUARIO where USU_CEDULA= '" + cedula + "' ";
            Conexion.buscar_usuario(Consulta);
            tb_cedula.Text = Conexion.USU_CEDULA;
            tb_password.Text = Conexion.USU_PASSWORD;
            tb_nombre.Text = Conexion.USU_NOMBRE;
            tb_correo.Text = Conexion.USU_CORREO;
            tb_sexo.Text = Conexion.USU_SEXO;
            tb_telefono.Text = Conexion.USU_TELEFONO;
            tb_direccion.Text = Conexion.USU_DIRECCION;
            dtp_fecha_nacimiento.Text = Conexion.USU_FECHA_NAC;

        }

        private void bloquear_tx()
        {
            tb_cedula.Enabled = false;
            tb_correo.Enabled = false;
            tb_direccion.Enabled = false;
            tb_nombre.Enabled = false;
            tb_password.Enabled = false;
            tb_telefono.Enabled = false;
            tb_sexo.Enabled = false;
            dtp_fecha_nacimiento.Enabled = false;
        }
        private void desbloquear_txt()
        {
            tb_correo.Enabled = true;
            tb_direccion.Enabled = true;
            tb_nombre.Enabled = true;
            tb_password.Enabled = true;
            tb_telefono.Enabled = true;
        }

        private void actualizar_tabla_productos()
        {
            Conexion.Actualizar_DGV(dgv_productos, "select PRO_ID as Codigo,PRO_NOMBRE as Producto, PRO_PRECIO as Precio, PRO_STOCK as Stock, PRO_CATEGORIA as Categoria, PRO_DESCRIPCION as Descripcion from T_PRODUCTO where USU_CEDULA ='" + cedula+"' ");

        }
        private void actualizar_tabla_mis_pedidos()
        {
            Conexion.Actualizar_DGV(dgv_mis_pedidos, "select TRA_ID as CODIGO, TRA_COD_COMERCIANTE as COMERCIANTE, TRA_ESTADO as ESTADO, TRA_FORMAPAGO as FORMA, TRA_FECHAGTRANSACCION as FECHA from T_ORDEN  where USU_CEDULA='" + cedula + "' ");

        }
        private void actualizar_tabla_pedidos()
        {
            Conexion.Actualizar_DGV(dgv_tabla_pedidos, "select P.PRO_ID as Codigo, P.PRO_NOMBRE as Producto, P.PRO_DESCRIPCION as Descripcion, P.PRO_PRECIO as Precio, P.PRO_STOCK as Stock, P.PRO_CATEGORIA from T_USUARIO U join T_PRODUCTO P on U.USU_CEDULA= P.USU_CEDULA where U.USU_NOMBRE='" + cb_comerciantes.Text+"'");

        }
        private void actualizar_mis_reputaciones()
        {
            Conexion.Actualizar_DGV(dgv_mi_reputacion, "select TRA_ID as ORDEN_Nº, C_PUNTOS as PUNTOS, C_COMENTARIO as DETALLES from T_CALIFICACION  where USU_CEDULA ='" + cedula + "'");

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (estado_modificar == false)
            {
                estado_modificar = true;
                desbloquear_txt();

            }
            else
            {
                estado_modificar = false;
                String Actualizar = ("Update T_USUARIO set USU_TELEFONO='" + tb_telefono.Text + "', USU_CORREO='" + tb_correo.Text + "', USU_DIRECCION='" + tb_direccion.Text + "', USU_PASSWORD='" + tb_password.Text + "', USU_NOMBRE='" + tb_nombre.Text + "' where USU_CEDULA='" + cedula + "'");
                Conexion.Ejecutar(Actualizar, "Usuario Modificado Con Èxito", "Error al Actualizar");
                llamar_datos_usuario();
                bloquear_tx();
            }


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var pregunta = MessageBox.Show("¿ESTÁ SEGURO DE QUERER BORRAR SU CUENTA?\n Se perderá toda la información relacionada con ella.", "Aviso de seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pregunta == DialogResult.Yes)
            {
                string Eliminar = ("delete from T_USUARIO where USU_CEDULA ='" + cedula + "'");
                Conexion.Ejecutar(Eliminar, "Cuenta Borrada Completamente", "Error al Actualizar");

            }

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void tb_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            String Guadar = "insert into T_PRODUCTO values('" + cedula + "','" + tb_pro_nombre.Text + "'," + tb_pro_precio.Text + "," + tb_stock.Text + ",'" + tb_descripcion.Text + "','null','" + cb_categoria.Text + "')";
            Conexion.Ejecutar(Guadar, "Producto Guardado Con Èxito", "Error al Guardar");
            actualizar_tabla_productos();
            limpiar_productos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (estado_modificar == false)
            {
                estado_modificar = true;
                codigo_producto = dgv_productos.CurrentRow.Cells[0].Value.ToString();
                tb_pro_nombre.Text = dgv_productos.CurrentRow.Cells[1].Value.ToString();
                tb_pro_precio.Text = dgv_productos.CurrentRow.Cells[2].Value.ToString();
                tb_stock.Text = dgv_productos.CurrentRow.Cells[3].Value.ToString();
                cb_categoria.Text = dgv_productos.CurrentRow.Cells[4].Value.ToString();
                tb_descripcion.Text = dgv_productos.CurrentRow.Cells[5].Value.ToString();
            }
            else
            {
                estado_modificar = false;
                String Actualizar = ("Update T_PRODUCTO set PRO_NOMBRE='" + tb_pro_nombre.Text + "', PRO_PRECIO='" + tb_pro_precio.Text + "', PRO_STOCK='" + tb_stock.Text + "', PRO_DESCRIPCION='" + tb_descripcion.Text + "', PRO_CATEGORIA='" + cb_categoria.Text + "' where PRO_ID='" + codigo_producto + "'");
                Conexion.Ejecutar(Actualizar, "Usuario Modificado Con Èxito", "Error al Actualizar");
                limpiar_productos();
                actualizar_tabla_productos();
            }

        }
        private void limpiar_productos()
        {
            tb_pro_nombre.Text = "";
            tb_pro_precio.Text = "";
            tb_stock.Text = "";
            cb_categoria.Text = "";
            tb_descripcion.Text = "";
        }
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            estado_modificar = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pregunta = MessageBox.Show("¿ESTÁ SEGURO DE QUERER BORRAR ESTE PRODUCTO?\n Se perderá toda la información relacionada con el.", "Aviso de seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pregunta == DialogResult.Yes)
            {
                string Eliminar = ("delete from T_PRODUCTO where PRO_ID ='" + dgv_productos.CurrentRow.Cells[0].Value.ToString()+ "'");
                Conexion.Ejecutar(Eliminar, "Producto eliminado correctamente", "Error al Actualizar");
                actualizar_tabla_productos();

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
             }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
           }

        private void cb_comerciantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizar_tabla_pedidos();
        }

        private void bt_cancelar_orden_Click(object sender, EventArgs e)
        {
            

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void bt_añadir_orden_Click(object sender, EventArgs e)
        {
            
        }

        private void bt_completar_orden_Click(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tb_buscar_comerciante_Click(object sender, EventArgs e)
        {
            llenar_combobox_comerciante();
            bt_nueva_orden.Enabled = true;
        }

        private void bt_nueva_orden_Click(object sender, EventArgs e)
        {
            tb_cantidad_items.Enabled = true;
            bt_completar_orden.Enabled = true;
            tb_cantidad_items.Text = "1";
            bt_nueva_orden.Enabled = false;
            bt_cancelar_orden.Enabled = true;
            bt_añadir_orden.Enabled = true;
            tb_buscar_comerciante.Enabled = false;
            cb_comerciantes.Enabled = false;
            string Eliminar = ("insert into T_ORDEN (USU_CEDULA) values('" + cedula + "')");
            Conexion.Ejecutar(Eliminar, "1) Seleccione Items a comprar \n2) Precione el Boton Agregar\n3)Seleccione el numero de Items", "Paso 2");

        }

        private void bt_cancelar_orden_Click_1(object sender, EventArgs e)
        {
            //int numero_pedido = Conexion.buscar_si_exite("SELECT  TOP 1 TRA_ID FROM T_ORDEN where USU_CEDULA = '" + cedula + "'  ORDER BY TRA_ID DESC");
            string Eliminar = ("Delete from T_ORDEN where TRA_ID = (SELECT  TOP 1 TRA_ID FROM T_ORDEN where USU_CEDULA = '" + cedula + "'  ORDER BY TRA_ID DESC)");
            Conexion.Ejecutar(Eliminar, "Se canceló la Compra", "Paso 2");
            bt_nueva_orden.Enabled = true;
            bt_añadir_orden.Enabled = false;
            bt_cancelar_orden.Enabled = false;
            tb_buscar_comerciante.Enabled = true;
            cb_comerciantes.Enabled = true;
            tb_cantidad_items.Enabled = false;
            bt_completar_orden.Enabled = false;
            tb_cantidad_items.Text = "";
        }

        private void bt_añadir_orden_Click_1(object sender, EventArgs e)
        {
            var pregunta = MessageBox.Show("¿ESTÁ SEGURO DE AÑADIR " + tb_cantidad_items.Text + " items de " + dgv_tabla_pedidos.CurrentRow.Cells[1].Value.ToString() + " a su pedido?", "Aviso de seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pregunta == DialogResult.Yes)

            {
                int numero_pedido = Conexion.buscar_si_exite("SELECT  TOP 1 TRA_ID FROM T_ORDEN where USU_CEDULA = '" + cedula + "'  ORDER BY TRA_ID DESC");
                string Eliminar = ("insert into T_DETALLE_TRANSACCION values (" + dgv_tabla_pedidos.CurrentRow.Cells[0].Value.ToString() + "," + numero_pedido + "," + tb_cantidad_items.Text + " )");
                Conexion.Ejecutar(Eliminar, "Pedido Ingresado Correctamente", "Error al Actualizar");


            }
        }

        private void cb_comerciantes_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            actualizar_tabla_pedidos();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {

          
                Ingreso frm_ingreso = new Ingreso();
                this.Dispose();
                
                frm_ingreso.Show();
        
          
               
            
                         
        }

        private void cb_forma_pago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void guardar()
        {
            bt_nueva_orden.Enabled = true;
            bt_añadir_orden.Enabled = false;
            bt_cancelar_orden.Enabled = false;
            tb_buscar_comerciante.Enabled = true;
            cb_comerciantes.Enabled = true;
            tb_cantidad_items.Enabled = false;
            bt_completar_orden.Enabled = false;
            tb_cantidad_items.Text = "";
            tb_direccion_entrega.Text = "";
            

        }

        private void bt_completar_orden_Click_1(object sender, EventArgs e)
        {
            var pregunta = MessageBox.Show("¿Está seguro de terminar su pedido.pedido?", "Aviso de seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pregunta == DialogResult.Yes)

            {
                String ced_comerciante = Conexion.buscar_consulta_string("select USU_CEDULA from T_USUARIO where USU_NOMBRE='"+cb_comerciantes.Text+"'", 0);
                int numero_pedido = Conexion.buscar_si_exite("SELECT  TOP 1 TRA_ID FROM T_ORDEN where USU_CEDULA = '" + cedula + "'  ORDER BY TRA_ID DESC");
                string Actualizar_Pedido = ("Update T_ORDEN set USU_CEDULA='" + cedula + "',TRA_ESTADO='Pendiente',TRA_FORMAPAGO='" + cb_forma_pago.Text + "',TRA_FECHAGTRANSACCION='" + DateTime.Now.ToString() + "',TRA_FORMAENTREGA='',TRA_DIRECCIONENTREGA='" + tb_direccion_entrega.Text + "',TRA_COD_COMERCIANTE='"+ ced_comerciante + "' where TRA_ID=" + numero_pedido + "");
                Conexion.Ejecutar(Actualizar_Pedido, "Pedido Ingresado Correctamente", "Error al Actualizar");
                actualizar_tabla_mis_pedidos();
                comerciante = cb_comerciantes.Text;
                guardar();
            
            }

        }
       
        
        private void button7_Click_1(object sender, EventArgs e)
        {
            var pregunta = MessageBox.Show("¿Está seguro de eliminar esta Orden de Pedido?", "Aviso de seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pregunta == DialogResult.Yes)

            {
                string borrar_orden = ("delete from T_ORDEN where TRA_ID='"+ dgv_mis_pedidos.CurrentRow.Cells[0].Value.ToString() + "'");
                Conexion.Ejecutar(borrar_orden, "Orden Borrada Exitosamente", "Error al Actualizar");
                actualizar_tabla_mis_pedidos();
                
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string calificar = ("insert into T_CALIFICACION values('" + dgv_mis_pedidos.CurrentRow.Cells[1].Value.ToString() + "',"+cb_calificación.Text+",'"+cb_comentario.Text+"','"+ dgv_mis_pedidos.CurrentRow.Cells[0].Value.ToString() + "')");
            Conexion.Ejecutar(calificar, "Comerciante Calificado", "Error al Calificar");
            actualizar_mis_reputaciones();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }


        private void button9_Click(object sender, EventArgs e)
        {
            int contador = 0;
            //creamos una variable DATATIME
            DateTime fecha_pedido;
            //RECORREMOS TODA LA TABLA
            foreach (DataGridViewRow dgvRenglon in dgv_mis_pedidos.Rows)
            {
                //GUARDAMOS EL DATO DE LA COLUMNA FECHA EN LA VARIABLE DATATIME
              //  MessageBox.Show(DateTime.Now.AddDays(1).ToString());
                fecha_pedido = DateTime.Parse(dgvRenglon.Cells[4].Value.ToString());
                // COMPARAMOS SI LA FECHA GUARDADA ES MENOR A LA FECHA ACTUAL
                if ((fecha_pedido.AddDays(1)) < DateTime.Now)
                {
                    String Eliminar_expirado = "delete from T_ORDEN where TRA_ID='" + (dgvRenglon.Cells[0].Value.ToString()) + "';";
                    Conexion.Ejecutar_sin_mensaje(Eliminar_expirado);
                    contador++;
                }
                
            }
            MessageBox.Show("Se eliminaron " + contador + " Ordenes de Pedido", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            actualizar_tabla_mis_pedidos();
        }

        private void bt_cancelar_orden_Click_2(object sender, EventArgs e)
        {
            string Eliminar = ("Delete from T_ORDEN where TRA_ID = (SELECT  TOP 1 TRA_ID FROM T_ORDEN where USU_CEDULA = '" + cedula + "'  ORDER BY TRA_ID DESC)");
            Conexion.Ejecutar(Eliminar, "Se canceló la Compra", "Paso 2");
            bt_nueva_orden.Enabled = true;
            bt_añadir_orden.Enabled = false;
            bt_cancelar_orden.Enabled = false;
            tb_buscar_comerciante.Enabled = true;
            cb_comerciantes.Enabled = true;
            tb_cantidad_items.Enabled = false;
            bt_completar_orden.Enabled = false;
            tb_cantidad_items.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }
    }
}
