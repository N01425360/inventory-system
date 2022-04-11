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

namespace Project
{
    public partial class sales : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=Inventory;Integrated Security=True");
        SqlConnection con = new SqlConnection(@"Data Source=ROBOT\SQLEXPRESS;Initial Catalog=Inventory;Integrated Security=True");


        DataTable dt = new DataTable();
        int total = 0;
        public sales()
        {
            InitializeComponent();
        }

        private void sales_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt.Clear();
            dt.Columns.Add("Product");
            dt.Columns.Add("Price");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Total");
             total = 0;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox3_KeyUp(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Visible = true;
            listBox1.Items.Clear();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Stock where product_name like('"+textBox3.Text+"%')";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);


            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["product_name"].ToString());
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex + 1;

                }
                if (e.KeyCode == Keys.Up)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex - 1;

                }
                if (e.KeyCode == Keys.Enter)
                {
                    textBox3.Text = this.listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    textBox4.Focus();

                }
            }
            catch { }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select top 1 * from purchase_master where product_name='" + textBox3.Text + "' order by id desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox4.Text = dr["product_price"].ToString();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            try {
                textBox6.Text = Convert.ToString(Convert.ToInt32(textBox4.Text) * Convert.ToInt32(textBox5.Text));
            }
            catch(Exception ex){ 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int stock = 0;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "Select * from stock where product_name='"+textBox3.Text+"'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                stock = Convert.ToInt32(dr1["product_qty"].ToString());
            }
            if (Convert.ToInt32(textBox5.Text) > stock)
            {
                MessageBox.Show("Stock insufficient");
            }
            else {
                DataRow dr = dt.NewRow();
                dr["Product"] = textBox3.Text;
                dr["Price"] = textBox4.Text;
                dr["Quantity"] = textBox5.Text;
                dr["Total"] = textBox6.Text;
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                total += Convert.ToInt32(dr["Total"].ToString());
                label10.Text = Convert.ToString(total);
                
            }
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                total = 0;
                dt.Rows.RemoveAt(Convert.ToInt32(dataGridView1.CurrentCell.RowIndex.ToString()));
                foreach (DataRow dr in dt.Rows)
                {
                    total += Convert.ToInt32(dr["Total"].ToString());
                    label10.Text = Convert.ToString(total);
                }            }
            catch(Exception ex){ 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string orderid = "";
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            //Insert order details
            cmd1.CommandText = "insert into order_user values('"+ textBox1.Text +"','" + textBox2.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Value.ToString("dd/mm/yyyy") + "')";
            cmd1.ExecuteNonQuery();


            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            //Select the last order id generated n use it to insert order items
            cmd2.CommandText = "select top 1 * from  order_user order by id desc";
            cmd2.ExecuteNonQuery();

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
               orderid =dr2["id"].ToString();
            }

            foreach (DataRow dr in dt.Rows)
            {
                int qty = 0;
                string pname = "";

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "insert into order_item values('" + orderid.ToString() + "','" + dr["Product"].ToString() + "','" + dr["Price"].ToString() + "','" + dr["Quantity"].ToString() + "','" + dr["Total"].ToString() + "')";
               
                cmd3.ExecuteNonQuery();

                //Update the stock after insertinng thte items in to order item tabel

                qty = Convert.ToInt32(dr["Quantity"].ToString());
                pname = dr["Product"].ToString();
                SqlCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                cmd4.CommandText = "update stock set product_qty=product_qty-" + qty + " where product_name='" + pname.ToString() + "'";
                cmd4.ExecuteNonQuery();


            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            label10.Text = "";
            dt.Clear();
            dataGridView1.DataSource = dt;
            MessageBox.Show("Record Inserted Successfully");

        }
    }
}
