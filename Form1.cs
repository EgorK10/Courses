using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Courses.Particle;



namespace Courses
{
    public partial class Form : System.Windows.Forms.Form
    {
        Emitter emitter;
        public Collector collector = new();

        public Form()
        {
            InitializeComponent();
            picDisplay.MouseWheel += picDisplay_MouseWheel;

            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            emitter = new Emitter 
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Red,
                ColorTo = Color.FromArgb(0, Color.Purple),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            collector.PaintParticle += (particle) =>
            {
                particle.FromColor = collector.color;
                particle.ToColor = collector.color;
            };

            collector.ReturnColor += (particle) =>
            {
                particle.FromColor = emitter.ColorFrom;
                particle.ToColor = emitter.ColorTo;
            };

            emitter.impactPoints.Add(collector);
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
                collector.Render(g);
            }

            picDisplay.Invalidate();
        }
                
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            collector.X = e.X;
            collector.Y = e.Y;
           
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void tbSpreading_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = tbSpreading.Value;
        }

        private void btfromColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            emitter.ColorFrom = colorDialog1.Color;
        }

        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            collector.Diameter += e.Delta * 0.1f;
            collector.Diameter = MathF.Abs(collector.Diameter);
        }

        private void bttoColor_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            emitter.ColorTo = colorDialog2.Color;
        }

        private void btcolletorColor_Click(object sender, EventArgs e)
        {
            colorDialog3.ShowDialog();
            collector.color = colorDialog3.Color;
        }
    }
}
