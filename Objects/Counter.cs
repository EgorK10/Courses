﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Courses
{
    public class Counter : IImpactPoint
    {
        public Action<ParticleColorful>? DestroyParticle;

        public float Diameter = 60;
        public Color color = Color.Red;
        public ushort Count = 0;
        public override void ImpactParticle(Particle particle)
        {
            if (Overlaps(particle))
            {
                if (particle is ParticleColorful particleColorful)
                {
                    DestroyParticle?.Invoke(particleColorful);
                }
            }
        }

        public override bool Overlaps(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double distance = Math.Sqrt(gX * gX + gY * gY);

            return distance <= Diameter / 2;
        }
        public override void Render(Graphics g)
        {
            var brush = new SolidBrush(Color.FromArgb(Convert.ToInt32(Count * 0.1f), 0, 0));
            g.FillEllipse(brush, X - Diameter / 2, Y - Diameter / 2, Diameter, Diameter);
            g.DrawEllipse(new Pen(color, 2), X - Diameter / 2, Y - Diameter / 2, Diameter, Diameter);

            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;


            if (Count > 0)
            {
                g.DrawString(
                    $"{Count}",
                    new Font("Comic Sans MS", 10), // шрифт и размер
                    new SolidBrush(Color.White), // цвет шрифта
                    X, Y, stringFormat);
            }            
        }
    }
}