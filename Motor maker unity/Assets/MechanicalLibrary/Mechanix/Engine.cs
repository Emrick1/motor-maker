using System.Collections.Generic;
using UnityEngine;

namespace Mechanix
{
    public class Engine : MonoBehaviour
    {
        /*  private double mass; //masse du moteur (kg)
          private double hp; //puissance du moteur (cheval vapeur)
          private int rpmMax; //Rotations Par Minutes(RPM) Maximum
          private int rpmMin; //Rotations Par Minutes(RPM) Minimum
          private double torque; //rotation de base à la sortie du moteur(vers la transmission)
          private double energyConsumption; //indice de consommation d'énergie(par seconde)
        */
        private string name;
        
        
      

        public Engine(string name)
        {
            this.name = name;
            /*
            this.mass = mass;
            this.hp = hp;
            this.rpmMax = rpmMax;
            this.rpmMin = rpmMin;
            this.torque = torque;
            this.energyConsumption = energyConsumption;
            */
        }
        public string Name
        {
            get => name;
            set => name = value;
        }
        void start()
        {

        }


     
    }
}