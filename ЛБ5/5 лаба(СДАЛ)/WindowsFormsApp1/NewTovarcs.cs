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
    public partial class NewTovarcs : Form
    {
        Class1 dataBase = new Class1();
        public NewTovarcs()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            var name = textBox_Name.Text;
            var count = textBox_Count.Text;
            int cost;
            if (int.TryParse(textBox_Cost.Text, out cost))
            {
                var addQuery = $"insert into Товары (name, cost, count) " +
                    $"values ('{name}','{cost}','{count}')";

                var command = new SqlCommand(addQuery, dataBase.getConnection());
                command.ExecuteNonQuery();



                MessageBox.Show("Запись создана успешно!!!", "Успех!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Цена введена не правльно");
            }
            dataBase.closeConnection();



        }

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
