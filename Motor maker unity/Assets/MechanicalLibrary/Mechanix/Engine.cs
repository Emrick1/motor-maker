using System.Collections.Generic;
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
        private string nameCylindre;
        
      

        protected Engine(string name)
        {
            this.nameCylindre = name;
            /*
            this.mass = mass;
            this.hp = hp;
            this.rpmMax = rpmMax;
            this.rpmMin = rpmMin;
            this.torque = torque;
            this.energyConsumption = energyConsumption;
            */
        }
        void start()
        {

        }

        public static void addEngineToList()
        {
            enginesList.Add(gearMenante);
            enginesList.Add(gear1);
            enginesList.Add(gear2);
            enginesList.Add(gear3);
     

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