using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BasicSynth
{
    public class Oscillator : GroupBox
    {
        public Oscillator()
        {
            this.Controls.Add(new Button()
            {
                Name = "Sine",
                Location = new Point(10, 15),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 122, 204),
                FlatStyle = FlatStyle.Flat,
                Text = "Sine"
            }) ;
            this.Controls.Add(new Button()
            {
                Name = "Square",
                Location = new Point(65, 15),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "Square"
            });
            this.Controls.Add(new Button()
            {
                Name = "Saw",
                Location = new Point(120, 15),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "Saw"
            });
            this.Controls.Add(new Button()
            {
                Name = "Triangle",
                Location = new Point(10, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "Triangle"
            });
            this.Controls.Add(new Button()
            {
                Name = "Noise",
                Location = new Point(65, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "Noise"
            });
            foreach (Control control in this.Controls)
            {
                control.Size = new Size(50, 30);
                control.Font = new Font("Microsoft Sans Serif", 6.75f);
                control.Click += WaveButton_Click;
            }
        }

        public WaveForm WaveForm { get; private set; }

        private void WaveButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            this.WaveForm = (WaveForm)Enum.Parse(typeof(WaveForm), button.Text);
            foreach (Button additionalButtons in this.Controls.OfType<Button>())
            {
                additionalButtons.BackColor = Color.FromArgb(37, 37, 38);
            }
            button.BackColor = Color.FromArgb(0, 122, 204);
        }
    }
}
