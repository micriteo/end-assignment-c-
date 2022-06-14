using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows;
using System.Data.SqlClient;

namespace FinalAssignment
{
    public partial class CarRental : Form
    {
        public CarRental()
        {
            Thread t = new Thread(new ThreadStart(Splash));
            t.Start();
            Thread.Sleep(3000);
            InitializeComponent();
            t.Abort();
        }
        Company company = new Company();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public void Splash()   //Function that crates a spalsh screen or Loading Screen
        {
            Application.Run(new SplashScreen());
        }
        private void Form1_Load(object sender, EventArgs e) //Notification Bar Message
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\C#\FinalAssignment\FinalAssignment\CarRentalDataBase.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            //Connecting the app to the database 

            con.Open();
            //Opening the connection

            cmd = new SqlCommand("select carName from Car", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //Adding the items from the database in to the combobox
                comboBox1.Items.Add(dr[0].ToString());


                foreach(Car car in company.Cars)
                {
                    comboBox3.Items.Add(car.carName);
                }
            }
            dr.Close();

            notifyIcon1.BalloonTipTitle = "Zack is Sexy";
            notifyIcon1.BalloonTipText = "074 Do you think you can have his number LoL";
            notifyIcon1.Text = "Application Name";

        }
        private void Id_KeyPress(object sender, KeyPressEventArgs e)
        {       //We verifty with this event that the only thing available in this form are integers and not strings 
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void Name_KeyPress(object sender, KeyPressEventArgs e)
        {       //Allowing to enter only chars with no integers and also allowing to have white space in between 

            if (!char.IsLetter(e.KeyChar) && (!char.IsControl(e.KeyChar)) && (!char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }

        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)  //Icon visible in the systemTray 
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
        private void Form1_Resize(object sender, EventArgs e)  //Event that resizes the form in the systemTray
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
                notifyIcon1.ShowBalloonTip(1000);
            }
            else if (FormWindowState.Normal == this.WindowState)
            { notifyIcon1.Visible = false; }
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)  //About Box being displayed in the System Tray 
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        //Open command is shown in the systemTray
        {
            this.ShowInTaskbar = true;
        }
        private void page1ToolStripMenuItem_Click(object sender, EventArgs e)
        //Page1 is shown in the systemTray
        {
            this.ShowInTaskbar = true;
            tabControl1.SelectedIndex = 0;
        }
        private void page2ToolStripMenuItem_Click(object sender, EventArgs e)
        //Page2 is shown in the systemTray 
        {
            this.ShowInTaskbar = true;
            tabControl1.SelectedIndex = 1;
        }
        private void page3ToolStripMenuItem_Click(object sender, EventArgs e)
        //Page2 is shown in the systemTray 
        {
            this.ShowInTaskbar = true;
            tabControl1.SelectedIndex = 2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Event that will update the database witht he credintials made by user 

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\C#\FinalAssignment\FinalAssignment\CarRentalDataBase.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            //Preparing statement against SQL Injections and also inserting them in the Database
            cmd = new SqlCommand("UPDATE Car SET  startDate=@startDate, endDate=@endDate, day=@day where carName=@carName", con);
            cmd.Parameters.AddWithValue("@carName", comboBox1.Text);
            cmd.Parameters.AddWithValue("@startDate", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@endDate", dateTimePicker2.Text);
            cmd.Parameters.AddWithValue("day", comboBox2.Text);

            string carName = comboBox1.Text;
            DateTime startDate = DateTime.ParseExact(dateTimePicker1.Text, "dd-MMM-yy", null);
            DateTime endDate = DateTime.ParseExact(dateTimePicker2.Text, "dd-MMM-yy", null);
            string day = comboBox2.Text;
            Car car = new Car(carName, startDate, endDate, day);
            company.addCar(car);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void periodButton_Click(object sender, EventArgs e)
        {
            //When this button is pressed the difference between the periods will be calculated 

            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            TimeSpan difference = endDate - startDate;

            int days = difference.Days;

            lbl_Days.Text = days.ToString() + " Days";
        }
        //
        //
        //JOURNEY PAGE
        //
        //
        private void AddJourney_btn_Click(object sender, EventArgs e)
        {
            //Adding the values into the database 
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\C#\FinalAssignment\FinalAssignment\CarRentalDataBase.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("Insert into Journey values (/*@JourneyID,*/ @name ,@carName , @distance,  @day, @price, @period)", con);
            //cmd.Parameters.AddWithValue("JourneyID", textBox7.Text);
            cmd.Parameters.AddWithValue("name", textBox3.Text);
            cmd.Parameters.AddWithValue("carName", comboBox3.Text);
            cmd.Parameters.AddWithValue("distance", int.Parse(textBox4.Text));
            cmd.Parameters.AddWithValue("day", int.Parse(textBox6.Text));
            cmd.Parameters.AddWithValue("price", 0);
            cmd.Parameters.AddWithValue("period", 0);
            cmd.ExecuteNonQuery();
            con.Close();
            string name = textBox3.Text;
            int distance = int.Parse(textBox4.Text);
            int day = int.Parse(textBox6.Text);
            Car car = ((KeyValuePair<string, Car>)comboBox3.SelectedItem).Value;

            Journey journey = new Journey(name, car, distance,day);
            company.addJourney(journey);

            label20.Text = journey.getPrice().ToString() + " $ \n" + journey.distance.ToString() + " km \n" + journey.getPeriod() + " days\n";

            
        }

        private void getPrice_Click(object sender, EventArgs e)
        //Calculates the price of a journey 
        {
            //Calculating the total by multipyling 0.18 * how many killometers have been made 
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\C#\FinalAssignment\FinalAssignment\CarRentalDataBase.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("UPDATE Journey SET price=@price where JourneyID = @JourneyID ", con);
            //cmd.Parameters.AddWithValue("JourneyID", textBox7.Text);
            double total = 0;
            double distance = int.Parse(textBox4.Text);

            if (distance % 100 > 0)
            {
                total = 0.18 * distance;
            }
            total += total + int.Parse(textBox6.Text) * 50;
            cmd.Parameters.AddWithValue("price", double.Parse(total.ToString()));
            cmd.ExecuteNonQuery();
            //lbl_getPrice.Text = total.ToString() + " $";
            con.Close();
        }
         
        private void getDistance_Click(object sender, EventArgs e)
        {
            //label18.Text = textBox4.Text;
        }
        private void getPeriod_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;

            TimeSpan difference = date2 - date1;

            int days = difference.Days;

            //periodValue.Text = days.ToString() + " Days";

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\C#\FinalAssignment\FinalAssignment\CarRentalDataBase.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("UPDATE Journey SET period=@period where JourneyID = @JourneyID ", con);
            //cmd.Parameters.AddWithValue("JourneyID", textBox7.Text);
            cmd.Parameters.AddWithValue("period", int.Parse(days.ToString()));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //
        // Company Page
        //
        private void getTotal_Click_1(object sender, EventArgs e)
        {
            //Calculating the costs of all the journeys 
            double total;
            SqlDataReader reader = null;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\C#\FinalAssignment\FinalAssignment\CarRentalDataBase.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            string selectcmd = "SELECT SUM(price) FROM Journey";
            SqlCommand cmd = new SqlCommand(selectcmd, con);
            con.Open();
            total = Convert.ToInt32(cmd.ExecuteScalar());
            lblTotalIncome.Text = total.ToString() + " $";
            con.Close();

        }
        private void getAverageDistance_Click(object sender, EventArgs e)
        {
            //Calculating the Average Distance
            double avg;
            SqlDataReader reader = null;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\C#\FinalAssignment\FinalAssignment\CarRentalDataBase.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            string selectcmd = "SELECT AVG(distance) FROM Journey";
            SqlCommand cmd = new SqlCommand(selectcmd, con);
            con.Open();
            avg = Convert.ToInt32(cmd.ExecuteScalar());
            lblAvgDistance.Text = avg.ToString() + " km";
            con.Close();
        }

        private void getLongestPeriod_Click(object sender, EventArgs e)
        {
            //Getting the LongestPeriod from DataBase
            int max;
            SqlDataReader reader = null;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\C#\FinalAssignment\FinalAssignment\CarRentalDataBase.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            string selectcmd = "SELECT MAX(period) FROM Journey";
            SqlCommand cmd = new SqlCommand(selectcmd, con);
            con.Open();
            max = Convert.ToInt32(cmd.ExecuteScalar());
            lblLongestPeriod.Text = max.ToString() + " days";
            con.Close();
        }

        private void Stats_Click(object sender, EventArgs e)
        {

            double max;
            double avg;
            double total;
            max = company.longestPeriod();
            avg = company.averageDistance();
            total = company.totalIncome();
            
            lblLongestPeriod.Text = max.ToString() + " days";
            lblAvgDistance.Text = avg.ToString() + " km";
            lblTotalIncome.Text = total.ToString() + " $";
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {

            //Journey journey = new Journey(Name, car, distance, price);
            Dictionary<string, Car> test = new Dictionary<string, Car>();
            foreach (Car car in company.Cars)
            {
                if (!comboBox3.Items.Contains(car.carName)) { 
                test.Add(car.carName,car);
                }
            }
            comboBox3.DataSource = new BindingSource(test, null);
            comboBox3.DisplayMember = "Key";
            comboBox3.ValueMember = "Value";
        }
    }
}

