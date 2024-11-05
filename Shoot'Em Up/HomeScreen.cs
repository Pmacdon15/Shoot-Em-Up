namespace Shoot_Em_Up
{
    public partial class HomeScreen : Form
    {
        public HomeScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_start_game_Click(object sender, EventArgs e)
        {
            //TestLevel form = new TestLevel();
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
