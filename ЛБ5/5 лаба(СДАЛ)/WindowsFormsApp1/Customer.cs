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
using ClassLibrary1;
namespace WindowsFormsApp1
{
    public partial class Customer : Form
    {
        Class1 dataBase = new Class1();

        int SelectRow;
        public Customer()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }



        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("name", "Название");
            dataGridView1.Columns.Add("cost", "Стоимость");
           
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string stringQuery = $"select id, name, cost from Товары";

            SqlCommand cmd = new SqlCommand(stringQuery, dataBase.getConnection());

            dataBase.openConnection();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRows(dgw, reader);
            }
            reader.Close();
        }

        private void ReadSingleRows(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), RowState.ModifiedNew);
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string queryString = $"select * from Товары" +
                $" where concat (id, name, cost) like '%" + textBox_Search.Text + "%'";

            SqlCommand cmd = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRows(dgw, reader);
            }
            reader.Close();

        }

        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }


        

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
                SelectRow = e.RowIndex;

                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[SelectRow];


                    textBox_ID.Text = row.Cells[0].Value.ToString();
                    textBox_Name.Text = row.Cells[1].Value.ToString();
                    textBox_Cost.Text = row.Cells[2].Value.ToString();



                }


            
        }
    }
}
