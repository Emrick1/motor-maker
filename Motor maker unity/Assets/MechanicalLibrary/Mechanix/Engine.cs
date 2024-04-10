using System.Collections.Generic;
using UnityEngine;

namespace Mechanix
{
    /// <summary>
    /// <c>Classe contenant les paramètres d'un moteur.</c>
    /// </summary>
    public class Engine : MonoBehaviour
    {
        /// <summary>
        /// Nom du moteur.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Construit une instance de moteur avec un nom.
        /// </summary>
        /// <param name="name">Nom du moteur.</param>
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