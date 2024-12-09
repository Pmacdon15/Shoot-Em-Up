namespace Shoot_Em_Up
{
    public partial class HomeScreen : Form
    {
        public HomeScreen()
        {
            InitializeComponent();
        }


        private void button_start_game_Click(object sender, EventArgs e)
        {
            GamePlay form = new GamePlay(textBox_PlayerName.Text);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TopScores newTopScoresWindow = new TopScores();
            newTopScoresWindow.Show();
        }
    }
}
