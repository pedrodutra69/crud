using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace Aula_02_09_2020_CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        SqlConnection sqlCon = null;
        private string strCon = @"Password=Pedropedro123.;Persist Security Info=True;User ID=sa;Initial Catalog=BDCrud;Data Source=PEDRO\SQLEXPRESS";
        private string strSql = string.Empty;


        private void btnInserir_Click(object sender, EventArgs e)
        {

            strSql = "insert into Agenda (Nome, Telefone) values (@Nome, @Telefone)";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            try
            {
                sqlCon.Open();

                //Passagem de parametros

                comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
                comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = txtTelefone.Text;

                comando.ExecuteNonQuery();
                MessageBox.Show("Registro adicionado com sucesso.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {

                sqlCon.Close();
                txtNome.Clear();
                txtTelefone.Clear();
                txtNome.Focus();

            }


        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {


            if (txtID.Text == string.Empty)
            {

                strSql = "select * from agenda order by id ";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(strSql, sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                sqlCon.Open();
                dgAgenda.DataSource = dt;
                sqlCon.Close();


            }
            else
            {


                //---------------------------------------------------------------
                strSql = "select * from agenda where id=@id";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);

                comando.Parameters.Add("@id", SqlDbType.Int).Value = txtID.Text;


                try
                {


                    if (txtID.Text == String.Empty)
                    {

                        throw new Exception("Error, digite um ID");

                    }

                    sqlCon.Open();

                    SqlDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {

                        throw new Exception("ID não cadastrado");

                    }

                    if (dr.Read())
                    {

                        txtID.Text = Convert.ToString(dr["id"]);
                        txtNome.Text = Convert.ToString(dr["Nome"]);
                        txtTelefone.Text = Convert.ToString(dr["Telefone"]);

                    }


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }
                finally
                {

                    sqlCon.Close();
                    txtNome.Focus();

                }
            }
            //---------------------------------------------------------------

        }

        //Programa de inserir dados
        private void btnAlterar_Click(object sender, EventArgs e)
        {

            strSql = "update agenda set Nome =@Nome, Telefone=@Telefone where id=@id";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@id", SqlDbType.Int).Value = txtID.Text;
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = txtTelefone.Text;


            try
            {

                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro atualizado com sucesso");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {

                sqlCon.Close();
                txtID.Clear();
                txtNome.Clear();
                txtTelefone.Clear();
                txtNome.Focus();


                strSql = "select * from agenda order by id ";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comm = new SqlCommand(strSql, sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(strSql, sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                sqlCon.Open();
                dgAgenda.DataSource = dt;
                sqlCon.Close();

            }


        }


        //arrumar -  paramos aqui aula 
        private void btnPesquisaNome_Click(object sender, EventArgs e)
        {

            strSql = "select * from agenda where Nome like @Nome";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;

            try
            {

                if (txtNome.Text == String.Empty)
                {

                    throw new Exception("Digite um nome");

                }

                sqlCon.Open();

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {

                    throw new Exception("Nome não encontrado");

                }

                if (dr.Read())
                {
                    txtID.Text = Convert.ToString(dr["id"]);
                    txtNome.Text = Convert.ToString(dr["Nome"]);
                    txtTelefone.Text = Convert.ToString(dr["Telefone"]);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {

                sqlCon.Close();

                txtNome.Focus();
            }
        }

        //programa para deletar registros
        private void btnExcluir_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Desseja Excluir o registro ?", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {

                MessageBox.Show("Operação cancelada");

            }
            else
            {

                strSql = "delete from agenda where id = @id";
                sqlCon = new SqlConnection(strCon);

                SqlCommand comando = new SqlCommand(strSql, sqlCon);
                comando.Parameters.Add("@id", SqlDbType.Int).Value = txtID.Text;

                try
                {

                    sqlCon.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro apagado com sucesso");

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }
                finally
                {

                    sqlCon.Close();
                    txtID.Text = "";
                    txtNome.Clear();
                    txtTelefone.Clear();
                    txtNome.Focus();
                    
                }
            }
        }

        private void dgAgendaClick(object sender, DataGridViewCellEventArgs e)
        {

            txtID.Text = dgAgenda.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgAgenda.CurrentRow.Cells[1].Value.ToString();
            txtTelefone.Text = dgAgenda.CurrentRow.Cells[2].Value.ToString();

        }
    }
}
