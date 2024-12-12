using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Configuration;

namespace Shoot_Em_Up
{
    class PlayerScore
    {
        public string Name;
        public int Score;
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;


        public PlayerScore(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }
    }


    public partial class TopScores : Form
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string connectionString;


        //ConfigurationManager.AppSettings["connectionString"];
            //"Server=tcp:bowvalleycollege.database.windows.net,1433;Initial Catalog=bvc;Persist Security Info=False;User ID=default;Password=wohKot-8vikne-diskax;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=DbTopScores;Connect Timeout=30";

        //string connectionString = "Data Source=(localdb)\\ProjectModels;;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public TopScores()
        {
            InitializeComponent();
        }

        private void TopScores_Load(object sender, EventArgs e)
        {
            connectionString = ConfigurationManager.ConnectionStrings["Shoot_Em_Up.Properties.Settings.connectionString"].ConnectionString;

            List<PlayerScore> list = new List<PlayerScore>();

            try
            {

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string Query = "SELECT TOP (5) * FROM TopScores ORDER BY Score DESC ";

                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {

                    Label newLabelPlayer = new Label();
                    newLabelPlayer.Location = new Point(100, (int)(this.Height * 0.10) * i + 100);
                    newLabelPlayer.Font = new Font("Arial", 10, FontStyle.Regular);
                    if (reader[1] != null)
                    {
                        newLabelPlayer.Text = reader[1].ToString();
                    }
                    Label newLabelScore = new Label();
                    newLabelScore.Location = new Point(300, (int)(this.Height * 0.10) * i + 100);
                    newLabelScore.Font = new Font("Arial", 10, FontStyle.Regular);
                    newLabelScore.TextAlign = ContentAlignment.MiddleRight;
                    newLabelScore.Text = reader[2].ToString();

                    this.Controls.Add(newLabelPlayer);
                    this.Controls.Add(newLabelScore);
                    i++;
                }

                con.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Label labelTitleName = new Label();
            labelTitleName.Location = new Point(100, 50);
            labelTitleName.Font = new Font("Arial", 10, FontStyle.Bold);
            labelTitleName.Text = "Player";

            Label labelTitleScore = new Label();
            labelTitleScore.Location = new Point(300, 50);
            labelTitleScore.Font = new Font("Arial", 10, FontStyle.Bold);
            labelTitleScore.Text = "Best Scores";
            labelTitleScore.TextAlign = ContentAlignment.MiddleRight;

            Button buttonClear = new Button();
            buttonClear.Location = new Point((int)(this.Width * 0.35), (int)(0.75* this.Height));
            buttonClear.Height = (int)(0.1 * this.Height);
            buttonClear.Width = (int)(0.3 * this.Width);
            buttonClear.Text = "Clear Score";
            buttonClear.Click += new EventHandler(this.ClearScores);


            this.Controls.Add(labelTitleName);
            this.Controls.Add(labelTitleScore);
            this.Controls.Add(buttonClear);

        }

        private void ClearScores(object? sender, EventArgs e)
        {

            try
            {

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string Query = "DELETE FROM TopScores";

                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Top Scores Clear", "Top Scores Clear", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
