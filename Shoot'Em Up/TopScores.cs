using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shoot_Em_Up
{
    class PlayerScore
    {
        public string Name;
        public int Score;

        public PlayerScore(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }
    }


    public partial class TopScores : Form
    {
        //string currentDirectory = "C:\\Users\\wagne\\WFORMS\\Shoot-Em-Up\\Shoot'Em Up";
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public TopScores()
        {
            InitializeComponent();
        }

        private void TopScores_Load(object sender, EventArgs e)
        {

            List<PlayerScore> list = new List<PlayerScore>();


            foreach (string line in File.ReadLines(Path.Combine(currentDirectory, "topscores.txt")))
            {
                if(!line.Contains(","))
                {
                    continue;
                }
                string[] playerandscore = line.Split(",");
                PlayerScore newPlayerScore = new PlayerScore(playerandscore[0], Int32.Parse(playerandscore[1]));
                list.Add(newPlayerScore);
            }

            list.Sort((a, b) => b.Score - a.Score);

            Label labelTitleName = new Label();
            labelTitleName.Location = new Point(100, 50);
            labelTitleName.Font = new Font("Arial", 12, FontStyle.Bold);
            labelTitleName.Text = "Player";

            Label labelTitleScore = new Label();
            labelTitleScore.Location = new Point(300, 50);
            labelTitleScore.Font = new Font("Arial", 12, FontStyle.Bold);
            labelTitleScore.Text = "Best Scores";

            this.Controls.Add(labelTitleName);
            this.Controls.Add(labelTitleScore);


            for (int i = 0; i < list.Count; i++)
            {

                Label newLabelPlayer = new Label();
                newLabelPlayer.Location = new Point(100, (int)(this.Height * 0.10) * i + 100);
                newLabelPlayer.Font = new Font("Arial", 12, FontStyle.Bold);
                newLabelPlayer.Text = list[i].Name.ToString().ToUpper();

                Label newLabelScore = new Label();
                newLabelScore.Location = new Point(300, (int)(this.Height * 0.10) * i + 100);
                newLabelScore.Font = new Font("Arial", 12, FontStyle.Bold);
                newLabelScore.Text = list[i].Score.ToString();

                this.Controls.Add(newLabelPlayer);
                this.Controls.Add(newLabelScore);

                if (i== 4) break;
            }

        }
    }
}
