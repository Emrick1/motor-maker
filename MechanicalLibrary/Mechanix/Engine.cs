namespace Mechanix
{
    public class Engine
    {
        private double mass; //masse du moteur (kg)
        private int nbCylindre; //nombre de cylindre
        private double volume; //volume global (L)
        private double volumeCylindre; //volume par cylindre (L)
        private double hp; //puissance du moteur (cheval vapeur)
        private int rpmMax; //Rotations Par Minutes(RPM)
        private double torque; //rotation de base à la sortie du moteur(vers la transmission)

        public Engine(double mass, int nbCylindre, double volume, double volumeCylindre, double hp, int rpmMax, double torque)
        {
            this.mass = mass;
            this.nbCylindre = nbCylindre;
            this.volume = volume;
            this.volumeCylindre = volumeCylindre;
            this.hp = hp;
            this.rpmMax = rpmMax;
            this.torque = torque;
        }

        public Engine()
        {
            this.mass = 0;
            this.nbCylindre = 0;
            this.volume = 0;
            this.volumeCylindre = 0;
            this.hp = 0;
            this.rpmMax = 0;
            this.torque = 0;
        }
    }
}