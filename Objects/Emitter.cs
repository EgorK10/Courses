using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Courses.Particle;

namespace Courses
{
    public class Emitter
    {
        public List<Particle> particles = new List<Particle>();
        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();

        public int X;
        public int Y;
        public int Direction = 0;
        public int Spreading = 360;
        public int SpeedMin = 1;
        public int SpeedMax = 10;
        public int RadiusMin = 2;
        public int RadiusMax = 10;
        public int LifeMin = 20;
        public int LifeMax = 100;
        public int ParticlesCount = 500;

        public Color ColorFrom;
        public Color ColorTo;

        public int ParticlesPerTick = 1;

        public int MousePositionX;
        public int MousePositionY;

        public float GravitationX = 0;
        public float GravitationY = 1;

        public virtual void ResetParticle(Particle particle)
        {
            if (particle is ParticleColorful particleColor)
            {
                particleColor.FromColor = ColorFrom;
                particleColor.ToColor = ColorTo;
            }
            particle.Life = Particle.rand.Next(LifeMin, LifeMax);
           
            particle.X = X;
            particle.Y = Y;

            var direction = Direction
                + (double)Particle.rand.Next(Spreading)
                - Spreading / 2;


            var speed = Particle.rand.Next(SpeedMin, SpeedMax);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);            

            particle.Radius = Particle.rand.Next(RadiusMin, RadiusMax);
        }
      

        public void UpdateState()
        {
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < ParticlesCount)
                {
                    var particle = new ParticleColorful();
                    particle.FromColor = Color.White;
                    particle.ToColor = Color.FromArgb(0, Color.Black);

                    ResetParticle(particle); // добавили вызов ResetParticle

                    particles.Add(particle);

                }
                else
                {
                    break;
                }

            }

            foreach (var particle in particles.ToList())
            {
                particle.Life -= 1; // уменьшаю здоровье
                                    
                if (particle.Life < 0) // если здоровье кончилось
                {
                    ResetParticle(particle);

                }
                else
                {
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;

                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                    }


                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;                   
                }

            }
        }
        public void Render(Graphics g)
        {
            foreach (var point in impactPoints.ToList())
            {
                point.Render(g);
            }

            foreach (var particle in particles.ToList())
            {
                particle.Draw(g);
            }
        }
    }
}
