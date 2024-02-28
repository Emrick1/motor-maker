using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.Windows;


namespace Mechanix
{
    [Serializable]
    public class Car
    {
        private Engine engine;
        private Gearbox gearbox;
        //private Transmission transmission;
        private Wheels wheels;
        private Skins skins;
        private string filePath = "";

        public Car()
        {
            engine = new ThermicEngine();
            gearbox = new Gearbox(new List<Gear>());
            wheels = new Wheels();
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

        public void LoadCarParts(string path)
        {
            Car car = Enregistreur.LoadSettingsV2(path);
            if (car != null)
            {
                this.engine = car.engine;
                this.gearbox = car.gearbox;
                this.wheels = car.wheels;
                this.skins = car.skins;
            }
        }
        
        public void LoadCarParts()
        {
            Car car = Enregistreur.LoadSettingsV2(filePath);
            if (car != null)
            {
                this.engine = car.engine;
                this.gearbox = car.gearbox;
                this.wheels = car.wheels;
                this.skins = car.skins;
            }
        }

        public void SaveCar(string path)
        {
            Enregistreur.SaveCar(path, this);
        }

        public void SaveCar()
        {
            Enregistreur.SaveCar(filePath, this);
        }
    }
}