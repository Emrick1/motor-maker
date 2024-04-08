namespace Mechanix
{
    public class Gear 
    {
        private int nbDents;
        private double rayon;
        private string name;


        public Gear()
        {
            nbDents = 0;
            rayon = 0;
        }

        public Gear(int dents, double rayon, string name)
        {
            this.nbDents = dents;
            this.rayon = rayon;
            this.name = name;
        }

        public int NbDents
        {
            get => nbDents;
            set => nbDents = value;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }

        public double Rayon
        {
            get => rayon;
            set => rayon = value;
        }
    }
}