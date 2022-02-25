using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace YaHeardMe.Forms
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        public void TimerInterval()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            this.Show();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();

        }

        public void timer_tick(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
