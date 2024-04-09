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
    /// Class <c>Car</c> Repr�sente une voiture contenant diff�rentes configurations de pi�ces m�caniques
    /// </summary>
    [Serializable]
    public class Car : MonoBehaviour
    {
        /// <summary>
        /// Sp�cifie les param�tres du motewur utilis� par la voiture instanci�e.
        /// </summary>
        private Engine engine;
        /// <summary>
        /// Sp�cifie les param�tres de la bo�te de transmission utilis�e par la voiture instanci�e.
        /// </summary>
        private Gearbox gearbox;
        /// <summary>
        /// Sp�cifie les param�tres concernant les roues de la voiture instanci�e.
        /// </summary>
        private Wheels wheels;
        /// <summary>
        /// Sp�cifie l'aspect esth�tique utilis�e par la voiture instanci�e.
        /// </summary>
        private Skins skins;
        /// <summary>
        /// Nom du fichier utilis� pour la sauvegarde de cette instance.
        /// </summary>
        private string filePath = "";

        /// <summary>
        /// Construit une instance de voiture avec des param�tres par d�faut.
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
        /// Charge une instance de voiture sauvegard�e sur un fichier sp�cifi� et utilise ses param�tres pour cette instance.
        /// </summary>
        /// <param name="path">Nom du fichier � sauvegarder.</param>
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
        /// Charge une instance de voiture sauvegard�e sur un fichier de provenance pr�dertermin�e et utilise ses param�tres pour cette instance.
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
        /// Sauvegarde une instance de voiture dans un fichier sp�cifi�.
        /// </summary>
        /// <param name="path">Nom du ficher � sauvegarder.</param>
        public void SaveCar(string path)
        {
            Enregistreur.SaveCar(path, this);
        }

        /// <summary>
        /// Sauvegarde une instance de voiture dans un fichier de provenance pr�determin�.
        /// </summary>
        public void SaveCar()
        {
            Enregistreur.SaveCar(filePath, this);
        }
    }
}