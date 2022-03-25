using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGame
{
    public partial class ScoreScreen : UserControl
    {
        public ScoreScreen()
        {
            InitializeComponent();

            scoreLabel.Text = $"Your Score: {GameScreen.score}";
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new MainMenu());
        }
    }
}
