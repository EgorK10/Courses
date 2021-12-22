using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Courses
{
    public class Collector : IImpactPoint
    {        
        public int Diameter = 100;
        private ushort _count = 0;

        public override void ImpactParticle(Particle particle)
        {
            if (Overlaps(particle))
            {
                if (particle is ParticleColorful particleColorful)
                {
                    particleColorful.FromColor = Color.Blue;
                    particleColorful.ToColor = Color.Blue;
                }

                _count++;
            }
        }

        public bool Overlaps(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double distance = Math.Sqrt(gX * gX + gY * gY);

            return distance <= Diameter / 2;
        }

        public override void Render(Graphics g)
        {
            g.DrawEllipse(new Pen(Color.Blue), X - Diameter / 2, Y - Diameter / 2, Diameter, Diameter);
            
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            if (_count > 0)
            {
                g.DrawString(
                    $"{_count}",
                    new Font("Comic Sans MS", 14), // шрифт и размер
                    new SolidBrush(Color.Red), // цвет шрифта
                    X,
                    Y,
                    stringFormat
                );
            }

            _count = 0;
        }
    }
}
