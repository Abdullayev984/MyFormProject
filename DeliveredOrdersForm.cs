using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CargoProgram;

namespace CargoProqram
{
    public partial class DeliveredOrdersForm : Form
    {
        public DeliveredOrdersForm()
        {
            InitializeComponent();
        }
        SqlConnection myConnection = new SqlConnection("Data Source=DESKTOP-1T63NNN;Initial Catalog=Orders;Integrated Security=true");
        private void ShowData(string data)
        {
            SqlDataAdapter da = new SqlDataAdapter(data, myConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void DeliveredOrdersForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            ShowData("select * from DeliveredOrders");
            dataGridView1.AllowUserToAddRows = false;
        }
  

        private void GoNextForm_Click(object sender, EventArgs e)
        {
            OrderForm fr = new OrderForm();
            this.Hide();
            fr.Show();
        }

       

        private void AddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                SqlCommand cmd = new SqlCommand("insert into DeliveredOrders(NameSurname,StockCode,DeliveryDate,AmountPaid) values (@NameSurname,@StockCode,@DeliveryDate,@AmountPaid)", myConnection);

                cmd.Parameters.AddWithValue("@NameSurname", NameSurname.Text);
                cmd.Parameters.AddWithValue("@StockCode", StockCode.Text);
                cmd.Parameters.AddWithValue("@DeliveryDate", DeliveryDate.Text);
                cmd.Parameters.AddWithValue("@AmountPaid", Convert.ToDouble(AmountPaid.Text));
                if (StockCode.Text == "")
                {
                    MessageBox.Show("Enter Stock Code:");
                    NameSurname.Clear();
                    StockCode.Clear();
                    AmountPaid.Clear();
                }
                if (StockCode.Text != "")
                {
                    cmd.ExecuteNonQuery();

                }

                ShowData("select * from DeliveredOrders");
                NameSurname.Clear();
                StockCode.Clear();
                DeliveryDate.Clear();
                AmountPaid.Clear();

                myConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteForDate_Click(object sender, EventArgs e)
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from DeliveredOrders where DeliveryDate=@DeliveryDate", myConnection);
            cmd.Parameters.AddWithValue("@DeliveryDate", DeletedForDate.Text.Trim());
            if (DeletedForDate.Text.Trim() != "")
            {
                cmd.ExecuteNonQuery();
            }
            ShowData("select * from DeliveredOrders");
            DeletedForDate.Clear();
            myConnection.Close();
        }

        private void DeleteForStock_Click(object sender, EventArgs e)
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from DeliveredOrders where StockCode=@StockCode", myConnection);
            cmd.Parameters.AddWithValue("@StockCode", DeletedStockCode.Text);
            if (DeletedStockCode.Text.Trim() != "")
            {

                cmd.ExecuteNonQuery();
            }
            ShowData("select * from DeliveredOrders");
            DeletedStockCode.Clear();
            myConnection.Close();
        }

        private void FinalAmount_Click(object sender, EventArgs e)
        {
            double cem = 0;

            int b = dataGridView1.RowCount;
            for (int i = 0; i < b; i++)
            {

                cem = cem + Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString());


            }
            Final_Amount.Text = cem.ToString() + " AZN";
        }
    }
}
