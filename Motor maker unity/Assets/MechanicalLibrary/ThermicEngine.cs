namespace Mechanix
{
    public class ThermicEngine : Engine
    {
        private int nbCylindre; //nombre de cylindre
        private double volumeCylindre; //volume par cylindre (L)
        private double volumeCylindres; //volume des cylindres

       public ThermicEngine() : base()
        {
            nbCylindre = 0;
            volumeCylindre = 0;
            volumeCylindres = 0;
        }

       public ThermicEngine(double mass, double hp, int rpmMax, int rpmMin, double torque, double energyConsumption, int nbCylindre, double volumeCylindre, double volumeCylindres) : 
           base(mass, hp, rpmMax, rpmMin, torque, energyConsumption)
       {
           this.nbCylindre = nbCylindre;
           this.volumeCylindre = volumeCylindre;
           this.volumeCylindres = volumeCylindres;
       }
    }
}