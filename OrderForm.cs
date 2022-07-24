using CargoProqram;
using System.Data;
using System.Data.SqlClient;

namespace CargoProgram
{
    public partial class OrderForm : Form
    {
        public OrderForm()
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
        private void OrderForm_Load(object sender, EventArgs e)
        {
          
            ShowData("select * from Order");
            dataGridView1.Columns["Orderİd"].Visible = false;
            dataGridView1.AllowUserToAddRows = false;

        }



        private void AddOrder_Click(object sender, EventArgs e)
        {
        try
        { 
                double a = Convert.ToDouble(PurchasePrice.Text);
                double b = Convert.ToDouble(SalePrice.Text);
                double d = Convert.ToDouble(PaymentStatus.Text);
                double c = b - d;
                myConnection.Open();
                string sql = @"insert into Order(NameSurname,ProductName,StockCode,Link,Size,DateofOrder,Firm,PurchasePrice,SalePrice,PaymentStatus,QalıqBorc,OrderStatus,PlaceofDelivery,Note,PhoneNumber) values (@NameSurname,@ProductName,@StockCode,@Link,@Size,@DateofOrder,@Firm,@PurchasePrice,@SalePrice,@PaymentStatus,@OrderStatus,@PlaceofDelivery,@Note,@PhoneNumber)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);

                cmd.Parameters.AddWithValue("@NameSurname", NameSurname.Text);
                cmd.Parameters.AddWithValue("@ProductName", ProductName.Text);
               cmd.Parameters.AddWithValue("@StockCode", StockCode.Text);
                if (StockCode.Text == "")
                {
                    MessageBox.Show("Enter Stock Code:");

                }
                cmd.Parameters.AddWithValue("@Link", Link.Text);
                cmd.Parameters.AddWithValue("@Size", Size.Text);
                cmd.Parameters.AddWithValue("@DateofOrder", Date.Text);
                cmd.Parameters.AddWithValue("@Firm", FirmName.Text);
                cmd.Parameters.AddWithValue("@PurchasePrice", a);
                cmd.Parameters.AddWithValue("@@SalePrice", Convert.ToDouble(SalePrice.Text));
                cmd.Parameters.AddWithValue("@PaymentStatus", Convert.ToDouble(PaymentStatus.Text));
                cmd.Parameters.AddWithValue("@QalıqBorcuu", c);
                cmd.Parameters.AddWithValue("@OrderStatus", OrderStatus.Text);
                cmd.Parameters.AddWithValue("PlaceofDeliver", PlaceofDelivery.Text);
                cmd.Parameters.AddWithValue("@Note", Note.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber.Text);
                if (StockCode.Text != "")
                {
                    cmd.ExecuteNonQuery();

                }

            ShowData("select * from Order");
                NameSurname.Clear();
                ProductName.Clear();
                StockCode.Clear();
                Link.Clear();
                Size.Clear();
                FirmName.Clear();
                PurchasePrice.Clear();
                SalePrice.Clear();
                PaymentStatus.Clear();
                PlaceofDelivery.Clear();
                Note.Clear();
                PhoneNumber.Clear();
                OrderStatus.Text = "";
                myConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Delete_Order_Click(object sender, EventArgs e)
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from Order where StockCode=@StockCode", myConnection);
            cmd.Parameters.AddWithValue("@StockCode", Stock_Code.Text);
            if (Stock_Code.Text.Trim() != "")
            {
                cmd.ExecuteNonQuery();
            }
            ShowData("select * from Order ");
            Stock_Code.Clear();
            myConnection.Close();
        }

        private void GoNextForm_Click(object sender, EventArgs e)
        {
            DeliveredOrdersForm frm = new DeliveredOrdersForm();
            this.Hide();
            frm.Show();
        }
    }
}