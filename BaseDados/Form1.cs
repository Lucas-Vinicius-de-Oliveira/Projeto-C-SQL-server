using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//sql serve ce
using System.Data.SqlServerCe;
using System.IO;

namespace BaseDados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            #region SQLSERVE CE
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sdf"; // esta definindo o caminho da base
            string strConection = @"DataSource = " + baseDados + "; Password = '123' "; //string conection definida

            SqlCeEngine db = new SqlCeEngine(strConection); //objeto definindo a conexao

            if (!File.Exists(baseDados)) // aqui verifico se a base existe ou nao
            {
                db.CreateDatabase();
            }

            db.Dispose();

            SqlCeConnection conexao = new SqlCeConnection(strConection); //criei a conexao agora vou abrir ela

            try
            {
                conexao.Open();
                labelResultado.Text = "conectado SQL SERVER";

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                comando.CommandText = "create table pessoas (id int not null primary key, nome nvarchar(50), email nvarchar(50))";
                comando.ExecuteNonQuery();

                labelResultado.Text = "tabela criada";
                comando.Dispose();
            }
            catch (Exception ex)
            {
                labelResultado.Text = "Erro ao conectar \n" + ex;
            }
            finally
            {
                conexao.Close();
            }
            #endregion

        }

        private void btnCriarTabela_Click(object sender, EventArgs e)
        {
             #region criar tabelas
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sdf"; // esta definindo o caminho da base
            string strConection = @"DataSource = " + baseDados + "; Password = '123' "; //string conection definida

            SqlCeConnection conexao = new SqlCeConnection(strConection); // acabei de criar a conexao agora vou abrir ela

            try
            {
                conexao.Open();


                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                comando.CommandText = "create table pessoas (id int not null primary key, nome nvarchar(50), email nvarchar(50))";
                comando.ExecuteNonQuery();

                labelResultado.Text = "tabela criada";
                comando.Dispose();
            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }
        #endregion

             #region inserir dados
        private void btnInserir_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sdf"; // esta definindo o caminho da base
            string strConection = @"DataSource = " + baseDados + "; Password = '123' "; //string conection definida

            SqlCeConnection conexao = new SqlCeConnection(strConection); // acabei de criar a conexao agora vou abrir ela

            try
            {
                conexao.Open();


                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                int id = new Random(DateTime.Now.Millisecond).Next(0, 1000);
                string nome = textNome.Text;
                string email = textEmail.Text;

                comando.CommandText = "insert into pessoas values(" + id + ",'" + nome + "','" + email + "')";

                comando.ExecuteNonQuery();

                labelResultado.Text = "Registro inserido no sql server";
                comando.Dispose();
            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }
        #endregion

        private void btnProcurar_Click(object sender, EventArgs e)
        {
             #region procurar
            labelResultado.Text = "";
            lista.Rows.Clear();

            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sdf"; // esta definindo o caminho da base
            string strConection = @"DataSource = " + baseDados + "; Password = '123' "; //string conection definida

            SqlCeConnection conexao = new SqlCeConnection(strConection); // acabei de criar a conexao agora vou abrir ela

            try
            {
                string query = "SELECT * FROM pessoas";

                if (textNome.Text != "")
                {
                    query = "SELECT * FROM pessoas where nome like " + textNome.Text;
                }

                DataTable dados = new DataTable();

                SqlCeDataAdapter adaptador = new SqlCeDataAdapter(query, strConection);

                conexao.Open();

                adaptador.Fill(dados);

                foreach (DataRow linha in dados.Rows)
                {
                    lista.Rows.Add(linha.ItemArray);
                }

            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }
        #endregion

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            #region deletar
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sdf"; // esta definindo o caminho da base
            string strConection = @"DataSource = " + baseDados + "; Password = '123' "; //string conection definida

            SqlCeConnection conexao = new SqlCeConnection(strConection); // acabei de criar a conexao agora vou abrir ela

            try
            {
                conexao.Open();


                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                int id = (int)lista.SelectedRows[0].Cells[0].Value;

                comando.CommandText = "delete from pessoas where id ='" + id + "'";

                comando.ExecuteNonQuery();

                labelResultado.Text = "Registro excluido no sql server";
                comando.Dispose();
            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
            #endregion
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sdf"; // esta definindo o caminho da base
            string strConection = @"DataSource = " + baseDados + "; Password = '123' "; //string conection definida

            SqlCeConnection conexao = new SqlCeConnection(strConection); // acabei de criar a conexao agora vou abrir ela

            try
            {
                conexao.Open();


                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                int id = (int)lista.SelectedRows[0].Cells[0].Value;
                string nome = textNome.Text;
                string email = textEmail.Text;
                    
                string query = "update pessoas set nome = '" + nome + "', email = '" + email + "' where id like '" + id + "'";

                comando.CommandText = query;

                comando.ExecuteNonQuery();

                labelResultado.Text = "Registro alterado no sql server";
                comando.Dispose();
            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }
    }

}
