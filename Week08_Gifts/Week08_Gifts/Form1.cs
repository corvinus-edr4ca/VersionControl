using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Week08_Gifts.Entities;

namespace Week08_Gifts
{
    public partial class Form1 : Form
    {
        private List<Ball> _balls = new List<Ball>();

        private Ballfactory _factory;
        public Ballfactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }
        
        public Form1()
        {
            InitializeComponent();
            Factory = new Ballfactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var ball in _balls)
            {
                ball.MoveBall();
                if (ball.Left<maxPosition)
                {
                    maxPosition = ball.Left;
                }

                if (maxPosition > 1000)
                {
                    var oldestBall = _balls[0];
                    mainPanel.Controls.Remove(oldestBall);
                    _balls.Remove(oldestBall);
                }
            }
        }
    }
}
