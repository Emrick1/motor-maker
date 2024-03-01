namespace Mechanix
{
    public class Wheels : IMechanicalPiece
    {
        private double mass;
        private double asphaltAdherence; //coefficient entre 0 et 1
        private double dirtAdherence; //coefficient entre 0 et 1
        private double sandAdherence; //coefficient entre 0 et 1
        private double generalAdherence; //coefficient entre 0 et 1

        public Wheels(double mass, double asphaltAdherence, double dirtAdherence, double sandAdherence)
        {
            this.mass = mass;
            this.asphaltAdherence = asphaltAdherence;
            this.dirtAdherence = dirtAdherence;
            this.sandAdherence = sandAdherence;
            UpdateGeneralAdherence();
        }

        public Wheels()
        {
            this.mass = 0;
            this.asphaltAdherence = 1;
            this.dirtAdherence = 1;
            this.sandAdherence = 1;
            UpdateGeneralAdherence();
        }

        public double Mass
        {
            get => mass;
            set => mass = value;
            }

        public double AsphaltAdherence
        {
            get => asphaltAdherence;
            set
            {
                asphaltAdherence = value; 
                UpdateGeneralAdherence();
            }
        }

        public double DirtAdherence
        {
            get => dirtAdherence;
            set
            {
                dirtAdherence = value; 
                UpdateGeneralAdherence();
            }
        }

        public double SandAdherence
        {
            get => sandAdherence;
            set
            {
                sandAdherence = value;
                UpdateGeneralAdherence();
            }
        }

        public double GeneralAdherence
        {
            get => generalAdherence;
            set => generalAdherence = value;
        }

        public void UpdateGeneralAdherence()
        {
            if (asphaltAdherence != null && dirtAdherence != null && sandAdherence != null)
            {
                this.generalAdherence = (asphaltAdherence + dirtAdherence + sandAdherence) / 3;
            } else
            {
                this.generalAdherence = 1;
            }
        }
    }
}