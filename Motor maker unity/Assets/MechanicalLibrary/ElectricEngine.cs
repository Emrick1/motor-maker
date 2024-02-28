using UnityEngine;

namespace Mechanix
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine() : base()
        {
            
        }

        public ElectricEngine(double mass, double hp, int rpmMax, int rpmMin, double torque, double energyConsumption) : 
            base(mass, hp, rpmMax, rpmMin, torque, energyConsumption)
        {
            
        }
    }
}