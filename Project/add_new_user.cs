using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project.LogIn
{
    public partial class SignUp : Form
    {
        private string oldText;
        //SqlConnection con = new SqlConnection(@"Data Source=SQLSERVERDEV;Initial Catalog=Inventory;Integrated Security=True");
        // SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=Inventory;Integrated Security=True");
        SqlConnection con = new SqlConnection(@"Data Source=ROBOT\SQLEXPRESS;Initial Catalog=Inventory;Integrated Security=True");

        public SignUp()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        
    

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString());
        }

        private void SignUp_Load_1(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            display();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from registration where username='" + textBox3.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into registration values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                cmd1.ExecuteNonQuery();
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
                textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = "";
                display();
                MessageBox.Show("user records inserted successfully");

            }
            else
            {
                MessageBox.Show("this username already exist");
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
            textBox6.ResetText();

        }

        public void display()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from registration";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //Deleter function will work here
          
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from registration where id = "+ id +"";
            cmd.ExecuteNonQuery();

            display();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.All(chr => char.IsLetter(chr))) //It will checking if user enter is chr or not
                {
                    oldText = textBox1.Text;
                    textBox1.Text = oldText;

                    textBox1.BackColor = System.Drawing.Color.White;
                    textBox1.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    textBox1.Text = oldText;
                    textBox1.BackColor = System.Drawing.Color.Red;
                    textBox1.ForeColor = System.Drawing.Color.White;
                    MessageBox.Show("Error! Only Enter characters ");  //error message
                }
                textBox1.SelectionStart = textBox1.Text.Length;
            }



            catch (InvalidOperationException)
            {
                MessageBox.Show("Error! invalid operation");
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text.All(chr => char.IsLetter(chr))) //It will checking if user enter is chr or not
                {
                    oldText = textBox2.Text;
                    textBox2.Text = oldText;

                    textBox2.BackColor = System.Drawing.Color.White;
                    textBox2.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    textBox2.Text = oldText;
                    textBox2.BackColor = System.Drawing.Color.Red;
                    textBox2.ForeColor = System.Drawing.Color.White;
                    MessageBox.Show("Error! Only Enter characters ");  //error message
                }
                textBox2.SelectionStart = textBox2.Text.Length;
            }



            catch (InvalidOperationException)
            {
                MessageBox.Show("Error! invalid operation");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
