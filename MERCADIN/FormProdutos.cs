using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MERCADIN
{
    public partial class FormProdutos : Form
    {
        private int _selectedProductId = -1;
        public FormProdutos()
        {
            InitializeComponent();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text) ||
                string.IsNullOrEmpty(txtPreco.Text) ||
                string.IsNullOrEmpty(txtQuantidade.Text))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            string name = txtNome.Text;
            decimal price;
            int stock;

            if (!decimal.TryParse(txtPreco.Text, out price) || !int.TryParse(txtQuantidade.Text, out stock))
            {
                MessageBox.Show("Preço ou quantidade precisam ser numeros.");
                return;
            }

            try
            {
                using (var connection = new SqliteConnection(DatabaseSetup.ConnectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)";

                    using (var command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Stock", stock);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Produto adicionado com sucesso!");

                txtNome.Clear();
                txtPreco.Clear();
                txtQuantidade.Clear();

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar produto: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                using (var connection = new SqliteConnection(DatabaseSetup.ConnectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Products;";

                    using (var command = new SqliteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            gridProdutos.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void gridProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridProdutos.Rows[e.RowIndex];

                _selectedProductId = Convert.ToInt32(row.Cells["Id"].Value);

                txtNome.Text = row.Cells["Name"].Value.ToString();
                txtPreco.Text = row.Cells["Price"].Value.ToString();
                txtQuantidade.Text = row.Cells["Stock"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_selectedProductId == -1)
            {
                MessageBox.Show("Nenhum produto selecionado.");
                return;
            }

            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtPreco.Text) || string.IsNullOrEmpty(txtQuantidade.Text))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos.");
                return;
            }

            decimal price;
            int stock;

            if (!decimal.TryParse(txtPreco.Text, out price) || !int.TryParse(txtQuantidade.Text, out stock))
            {
                MessageBox.Show("Preço ou quantidade precisam ser numeros.");
                return;
            }

            try
            {
                using (var connection = new SqliteConnection(DatabaseSetup.ConnectionString))
                {
                    connection.Open();

                    string sql = "UPDATE Products SET Name=@Name, Price=@Price, Stock=@Stock WHERE Id=@Id";

                    using (var command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", txtNome.Text);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Stock", stock);
                        command.Parameters.AddWithValue("@Id", _selectedProductId);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Produto atualizado com sucesso!");

                    LoadData();

                    txtNome.Clear();
                    txtPreco.Clear();
                    txtQuantidade.Clear();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar produto: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtQuantidade.Clear();
            txtPreco.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_selectedProductId == -1)
            {
                MessageBox.Show("Nenhum produto selecionado");
                return;
            }

            var result = MessageBox.Show("Tem certeza que quer deletar este produto?", "Sim", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqliteConnection(DatabaseSetup.ConnectionString))
                    {
                        connection.Open();
                        string sql = "DELETE FROM Products WHERE Id=@Id";

                        using (var command = new SqliteCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Id", _selectedProductId);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Produto deletado.");

                    LoadData();

                    txtNome.Clear();
                    txtPreco.Clear();
                    txtQuantidade.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar produto: " + ex.Message);
                }
            }
        }
    }
}