using System.Collections.Generic;
using UnityEngine;

namespace Mechanix
{
    public class Engine : MonoBehaviour, IMechanicalPiece
    {
      /*  private double mass; //masse du moteur (kg)
        private double hp; //puissance du moteur (cheval vapeur)
        private int rpmMax; //Rotations Par Minutes(RPM) Maximum
        private int rpmMin; //Rotations Par Minutes(RPM) Minimum
        private double torque; //rotation de base à la sortie du moteur(vers la transmission)
        private double energyConsumption; //indice de consommation d'énergie(par seconde)
      */
        private string nameCylindre;
        
      

        public Engine(string name)
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


     
    }
}