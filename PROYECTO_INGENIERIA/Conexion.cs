using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace PROYECTO_INGENIERIA

{
    class Conexion
    {
        // campos usuario
        public string USU_CEDULA;
        public string USU_PASSWORD;
        public string USU_NOMBRE;
        public string USU_TELEFONO;
        public string USU_CORREO;
        public string USU_DIRECCION;
        public string USU_SEXO;
        public string USU_FECHA_NAC;


        SqlConnection conn;
        public void Conectar()
        {
            //Creamos la conexión
            conn = new SqlConnection("Data Source=DACH1996;Initial Catalog=COMERCIANTES;Integrated Security=True");
            conn.Open();
        }
        public void desconectar()
        {
            conn.Close();
        }

        public void buscar_usuario (String consulta)
        {
            Conectar();
            SqlDataReader leer;
            SqlCommand con = new SqlCommand(consulta, conn);
            leer = con.ExecuteReader();
            if (leer.Read())
            {
                USU_CEDULA =    leer[0].ToString();
                USU_PASSWORD =  leer[1].ToString();
                USU_NOMBRE =    leer[2].ToString();
                USU_TELEFONO =  leer[3].ToString();
                USU_CORREO =    leer[4].ToString();
                USU_DIRECCION = leer[5].ToString();
                USU_SEXO =      leer[6].ToString();
                USU_FECHA_NAC = leer[7].ToString();
            }
                               
            leer.Close();
            desconectar();
        }
        public void Ejecutar(String consulta, String ok, String Error)
        {
            try
            {


                //abrimos conexion
                Conectar();
                SqlCommand con = new SqlCommand(consulta, conn);

                //ejecutamos el còdigo sql
                int filasAfectadas = con.ExecuteNonQuery();
                //verificamos si hay cambios
                if (filasAfectadas > 0)
                    MessageBox.Show(ok, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                desconectar();
            }
            catch (Exception ex) //bloque catch para captura de error
            {
                MessageBox.Show(ex.ToString());
                //acción para manejar el error
            }
        }
        public void Ejecutar_sin_mensaje(String consulta)
        {
            try
            {


                //abrimos conexion
                Conectar();
                SqlCommand con = new SqlCommand(consulta, conn);

                //ejecutamos el còdigo sql
                int filasAfectadas = con.ExecuteNonQuery();
                //verificamos si hay cambios
                if (filasAfectadas > 0)
                 desconectar();
            }
            catch (Exception ex) //bloque catch para captura de error
            {
                MessageBox.Show(ex.ToString());
                //acción para manejar el error
            }
        }



        public void Actualizar_DGV(DataGridView DGV, String consulta)
        {
            Conectar();
            System.Data.DataSet DS = new System.Data.DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(consulta, conn);
            DA.Fill(DS, "T_PRODUCTO");
            DGV.DataSource = DS;
            DGV.DataMember = "T_PRODUCTO";
            desconectar();
        }




        public void Ingresar_Votos(string usuario, string evento, string candidata)
        {
            SqlCommand comando = new SqlCommand();
            SqlCommand comando2 = new SqlCommand();
            try //protejemos la consulta
            {


                conn = new SqlConnection("Data Source=DACH1996;Initial Catalog=Proyecto_Eleccion;Integrated Security=True");
                comando.Connection = conn;
                comando2.Connection = conn;
                //el simbolo @ nos indica los parametros que le vamos a pasar
                //este codigo es tambien util para update y delete
                comando.CommandText = "insert into Votos values(@usuario,@evento,@candidata,@fecha);";

                String fecha = DateTime.Now.ToString("dd/MM/yyyy");
                conn.Open();    //Abrimos la conexion a nuestra base de datos
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@evento", evento);
                comando.Parameters.AddWithValue("@candidata", candidata);
                comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(fecha));
                int NFilas = comando.ExecuteNonQuery();

                comando2.CommandText = "Update Evento_Usuario SET estado_voto='Realizado' where cod_evento=@evento and cod_usuario=@usuario;";
                comando2.Parameters.Clear();
                comando2.Parameters.AddWithValue("@evento", evento);
                comando2.Parameters.AddWithValue("@usuario", usuario);
                comando2.ExecuteNonQuery();


                //comando.ExecuteNonQuery regresa el numero de filas afectadas



                if (NFilas > 0)
                {
                    MessageBox.Show("Voto Registrado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //.NET  captura las excepciones que ocurran al realizar nuestras consultas en SqlException
            catch (SqlException ex)
            {
                MessageBox.Show("Existiò fallo al registrar el voto: " + ex);
            }
            conn.Close();    //se cierra la conexion para liberar espacio en memoria
            comando.Dispose();       //se limpian los comandos para poder hacer otra consulta
        }



        public void llenar_combo(ComboBox cb, String consulta, int columna)
        {
            Conectar();
            SqlDataReader leer;
            SqlCommand con = new SqlCommand(consulta, conn);
            leer = con.ExecuteReader();
            while (leer.Read())
            {
                //Columna es el numerode columna de los resultados de la consulta
                cb.Items.Add(leer[columna].ToString());
            }
            leer.Close();
            cb.SelectedIndex = 0;
            desconectar();
        }



        public int buscar_si_exite(String consulta)
        {

            int codigo = 0;
            Conectar();
            SqlDataReader leer;
            SqlCommand con = new SqlCommand(consulta, conn);
            leer = con.ExecuteReader();
            while (leer.Read())
            {
                codigo = ((int)leer[0]);

            }
            leer.Close();
            desconectar();
            return codigo;
        }
        public string buscar_consulta_string(String consulta, int columna)
        {

            string codigo = null;
            Conectar();
            SqlDataReader leer;
            SqlCommand con = new SqlCommand(consulta, conn);
            leer = con.ExecuteReader();
            while (leer.Read())
            {
                codigo = (leer[columna].ToString());

            }
            leer.Close();
            desconectar();
            return codigo;
            
        }

            public DateTime buscar_si_fecha(String consulta)
        {
            DateTime codigo;
            Conectar();
            SqlDataReader leer;
            SqlCommand con = new SqlCommand(consulta, conn);
            leer = con.ExecuteReader();
            leer.Read();
            codigo = ((DateTime)leer[0]);
            leer.Close();
            desconectar();
            return codigo;
        }

        public void Modificar_Candidata(string cedula, string nombre, string apellido, string fecha, string intereses, string video, string estado, PictureBox foto, string cod_evento, string cod_candidata)
        {
            SqlCommand comando = new SqlCommand();
            SqlCommand comando2 = new SqlCommand();
            try
            {
                conn = new SqlConnection("Data Source=DACH1996;Initial Catalog=Proyecto_Eleccion;Integrated Security=True");
                comando.Connection = conn;
                comando.CommandText = "update Candidata set nombre_candidata=@nombre, apellido_candidata=@apellido, fecha_nacimiento_candidata=@fecha, foto_candidata=@foto, intereses_candidata=@intereses, video_candidata=@video, estado_inscripcion_candidata=@estado where ced_candidata=@cedula;";
                conn.Open();    //Abrimos la conexion a nuestra base de datos
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@apellido", apellido);
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@intereses", intereses);
                comando.Parameters.AddWithValue("@video", video);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@cedula", cedula);
                comando.Parameters.Add("@foto", SqlDbType.Image);
                MemoryStream ms = new MemoryStream();
                foto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                comando.Parameters["@foto"].Value = ms.GetBuffer();
                int NFilas = comando.ExecuteNonQuery();

                comando2.Connection = conn;
                comando.Parameters.Clear();
                comando2.CommandText = "Insert into Evento_Candidata values (@codigo_evento,@cod_candidata);";
                comando2.Parameters.AddWithValue("@codigo_evento", cod_evento);
                comando2.Parameters.AddWithValue("@cod_candidata", cod_candidata);
                comando2.ExecuteNonQuery();

                //comando.ExecuteNonQuery regresa el numero de filas afectadas

                if (NFilas > 0)
                {
                    MessageBox.Show("Candidata Registrada con Exito", "Aprovada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Existió un error el Registrar Candidata" + ex);
            }
            conn.Close();    //se cierra la conexion para liberar espacio en memoria
            comando.Dispose();       //se limpian los comandos para poder hacer otra consulta
        }

    }
}
