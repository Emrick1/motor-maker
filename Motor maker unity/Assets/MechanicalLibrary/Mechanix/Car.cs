using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Windows;


namespace Mechanix
{
    /// <summary>
    /// Class <c>Car</c> Représente une voiture contenant différentes configurations de pièces mécaniques
    /// </summary>
    [Serializable]
    public class Car : MonoBehaviour
    {
        /// <summary>
        /// Spécifie les paramètres du motewur utilisé par la voiture instanciée.
        /// </summary>
        private Engine engine;
        /// <summary>
        /// Spécifie les paramètres de la boîte de transmission utilisée par la voiture instanciée.
        /// </summary>
        private Gearbox gearbox;
        /// <summary>
        /// Spécifie les paramètres concernant les roues de la voiture instanciée.
        /// </summary>
        private Wheels wheels;
        /// <summary>
        /// Spécifie l'aspect esthétique utilisée par la voiture instanciée.
        /// </summary>
        private Skins skins;
        /// <summary>
        /// Nom du fichier utilisé pour la sauvegarde de cette instance.
        /// </summary>
        private string filePath = "";

        /// <summary>
        /// Construit une instance de voiture avec des paramètres par défaut.
        /// </summary>
        public Car()
        {
            gearbox = new Gearbox(new List<Gear>());
            //wheels = new Wheels();
            skins = new Skins();
        }

        public Engine Engine
        {
            get => engine;
            set => engine = value;
        }

        public Gearbox Gearbox
        {
            get => gearbox;
            set => gearbox = value;
        }

        public Wheels Wheels
        {
            get => wheels;
            set => wheels = value;
        }

        public Skins Skins
        {
            get => skins;
            set => skins = value;
        }

        /// <summary>
        /// Charge une instance de voiture sauvegardée sur un fichier spécifié et utilise ses paramètres pour cette instance.
        /// </summary>
        /// <param name="path">Nom du fichier à sauvegarder.</param>
        public void LoadCarParts(string path)
        {
            Car car = Enregistreur.LoadSettings(path);
            if (car != null)
            {
                this.engine = car.engine;
                this.gearbox = car.gearbox;
                this.wheels = car.wheels;
                this.skins = car.skins;
            }
        }
        
        /// <summary>
        /// Charge une instance de voiture sauvegardée sur un fichier de provenance préderterminée et utilise ses paramètres pour cette instance.
        /// </summary>
        public void LoadCarParts()
        {
            Car car = Enregistreur.LoadSettings(filePath);
            if (car != null)
            {
                this.engine = car.engine;
                this.gearbox = car.gearbox;
                this.wheels = car.wheels;
                this.skins = car.skins;
            }
        }

        /// <summary>
        /// Sauvegarde une instance de voiture dans un fichier spécifié.
        /// </summary>
        /// <param name="path">Nom du ficher à sauvegarder.</param>
        public void SaveCar(string path)
        {
            Enregistreur.SaveCar(path, this);
        }

        /// <summary>
        /// Sauvegarde une instance de voiture dans un fichier de provenance prédeterminé.
        /// </summary>
        public void SaveCar()
        {
            Enregistreur.SaveCar(filePath, this);
        }
    }
}