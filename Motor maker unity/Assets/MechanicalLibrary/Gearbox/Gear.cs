namespace Mechanix
{
    public class Gear : IMechanicalPiece
    {
        private int nbDents;
        private double rayon;

        public Gear()
        {
            nbDents = 0;
            rayon = 0;
        }

        public Gear(int dents, double rayon)
        {
            this.nbDents = dents;
            this.rayon = rayon;
        }

        public int NbDents
        {
            get => nbDents;
            set => nbDents = value;
        }

        public double Rayon
        {
            get => rayon;
            set => rayon = value;
        }
    }
}