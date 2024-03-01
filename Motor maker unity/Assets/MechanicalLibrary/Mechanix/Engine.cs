using UnityEngine;

namespace Mechanix
{
    public abstract class Engine : MonoBehaviour, IMechanicalPiece
    {
        private double mass; //masse du moteur (kg)
        private double hp; //puissance du moteur (cheval vapeur)
        private int rpmMax; //Rotations Par Minutes(RPM) Maximum
        private int rpmMin; //Rotations Par Minutes(RPM) Minimum
        private double torque; //rotation de base à la sortie du moteur(vers la transmission)
        private double energyConsumption; //indice de consommation d'énergie(par seconde)

        public Engine()
        {
            mass = 0;
            hp = 0;
            rpmMax = 0;
            rpmMin = 0;
            torque = 0;
            energyConsumption = 0;
        }

        protected Engine(double mass, double hp, int rpmMax, int rpmMin, double torque, double energyConsumption)
        {
            this.mass = mass;
            this.hp = hp;
            this.rpmMax = rpmMax;
            this.rpmMin = rpmMin;
            this.torque = torque;
            this.energyConsumption = energyConsumption;
        }

        public double Mass
        {
            get => mass;
            set => mass = value;
        }

        public double Hp
        {
            get => hp;
            set => hp = value;
        }

        public int RpmMax
        {
            get => rpmMax;
            set => rpmMax = value;
        }

        public int RpmMin
        {
            get => rpmMin;
            set => rpmMin = value;
        }

        public double Torque
        {
            get => torque;
            set => torque = value;
        }
        
        public double EnergyConsumption
        {
            get => energyConsumption;
            set => energyConsumption = value;
        }

    }
}