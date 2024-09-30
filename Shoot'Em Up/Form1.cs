namespace Shoot_Em_Up
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TestLevel form = new TestLevel();
            form.Show();
        }
    }
}
