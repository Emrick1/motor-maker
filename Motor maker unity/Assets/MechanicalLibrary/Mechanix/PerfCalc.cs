using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
using static Mechanix.Wheels;
using static Mechanix.Gearbox;
using static Mechanix.Gear;
using System.Collections.Generic;
using System;

namespace Mechanix
{
    public class PerfCalc : MonoBehaviour
    {
        private Car car;
        public static Gear gearSelected;//gearSelected
        public TextMeshProUGUI ValueText;
        private static double mass = 3000;
        private static int RPMmax = 7100;
        private static int RPMmin = 600;
        private static double RPM = 0;
        private static double RPMOut = 0;
        private static double speed = 0;
        private static double acceleration = 0;
        private static float facteurAugmentation;
        private static float t;
        private static double frictionForceWheels = 0;
        private static double frictionForceWind = 0;
        private static double frictionForceEngineReductionCoefficient = 0.85;
        private static double dragCoefficient = 0.3; //TODO CHANGER SELON CHAR
        private static double frontCarArea = 2.5; //TODO CHANGER SELON CHAR
        private static double windDensity = 0;
        private static double ambientTemperature = 30; //TODO CHANGER SELON Hiver (-6) ou ete (30)
        private static double engineForce = 0;
        private static double engineTorque = 0;
        private static double torqueOut = 0;
        private static double horsePower = 0;

        public PerfCalc(Car car)
        {
            this.car = car;
        }

        public double getRPM()
        {
            return RPM;
        }

        public double getAcceleration()
        {
            return 0;
        }

        public void getTorqueSelonMoteur()
        {
            //V6
            if (RPM <= 4000)
            {
                engineTorque = (0.000016375 * (Math.Pow((RPM), 2))) + 173;
            }
            else
            {
                engineTorque = (-0.000001625 * (Math.Pow(((RPM) - 6000), 2))) + 500;
            }
            horsePower = (engineTorque * RPM) / 5252;
        }

        void Start()
        {
            if (Wheels.SelectedWheelType == 0) 
            {
                Wheels.WheelsSetValues(0.65, 0.55, 0.5, 1, 140, 300, 200, PerfCalc.Mass);
                Wheels.SelectedWheelType = 1;
            };
            Wheels.CalculateTyreFriction();
            frictionForceWheels = Wheels.FrictionForce;
            Gearbox.addGearToList();
            List<Gear> gears = Gearbox.GearsList();
            gearSelected = gears[1];
            
        }

        void Update()
        {
            CalculateSpeedAndForces();
            getTorqueSelonMoteur();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                
                speed += acceleration / 20;
                if (RPM < RPMmax)
                {
                    facteurAugmentation = (19 / (1 + (Mathf.Exp((-0.1f * t) + 5))) + 1);
                    RPM += (int)facteurAugmentation;
                    t += 0.05f;
                } 
                else
                {
                    RPM = RPMmax;
                }
            }
            else if (RPM > RPMmin)
            {
                t = 0;
                RPM -= 1;
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    RPM = (int)((RPM * 0.999) - 1);
                }
            }
            else if (RPM < RPMmin)
            {
                RPM = RPMmin;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                acceleration = -2;
                speed += acceleration / 60;
                if (speed < 0)
                {
                    speed = 0;
                }
            } 

            if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            {
                acceleration = -0.5;
                speed += acceleration / 60;
                if (speed < 0)
                {
                    speed = 0;
                }
            }

            for (int i = 1; i <= Gearbox.GearsList().Count - 1; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    gearSelected = Gearbox.Gears(i);
                }
            }

            int indexGearSelect = Gearbox.GearsList().IndexOf(gearSelected);
            if (Input.GetKeyDown(KeyCode.LeftArrow) && gearSelected != Gearbox.Gears(1))
            {
                indexGearSelect -= 1;
                gearSelected = Gearbox.Gears(indexGearSelect);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && gearSelected != Gearbox.Gears(Gearbox.GearsList().Count - 2))
            {
                indexGearSelect += 1;
                gearSelected = Gearbox.Gears(indexGearSelect);
            }
         

            RPMOut = (int)(((double)RPM) * (calculateRatio(Gearbox.Gears(0), gearSelected, false)));
            torqueOut = (horsePower * 5252) / RPMOut;
            engineForce = torqueOut * (Wheels.Radius / 100);

            Wheels.CalculateTyreFriction();
            WriteStats();
        }

        private void WriteStats()
        {
            ValueText.text = "Stats: "
               + "\nRPM: " + RPM.ToString()
               + "\nRPM Output: " + RPMOut.ToString()
               + "\nTorque Moteur: " + $"{engineTorque:F4}"
               + "\nHorse Power: " + $"{horsePower:F4}"
               + "\nTorque Output: " + $"{torqueOut:F4}"
               + "\nEngine Force (N): " + $"{engineForce:F4}"
               + "\nGear: " + gearSelected.Name.ToString()
               + "\nAcceleration (m/s^2): " + $"{acceleration:F4}"
               + "\nSpeed (m/s): " + $"{speed:F3}" + " Speed (km/h): " + $"{(speed*3.6):F3}"
               + "\nFriction Force Wheels (N): " + $"{frictionForceWheels:F3}"
               + "\nFriction Force Wind (N): " + $"{frictionForceWind:F3}"
               + "\n\nWheels: "
               + DictionnaryToString(Wheels.GetInfosWheels)
               + Wheels.getAdherenceString();
        }

        private void CalculateSpeedAndForces()
        {
            windDensity = 101.3 / (8.395 * ambientTemperature);
            frictionForceWind = 0.5 * dragCoefficient * frontCarArea * windDensity * (speed * speed);
            acceleration = ((frictionForceEngineReductionCoefficient * engineForce) - (frictionForceWheels + frictionForceWind)) / (mass);
            
        }

        public static string DictionnaryToString(Dictionary<string, double> dictionary)
        {
            string returnString = "";
            foreach (KeyValuePair<string, double> pair in dictionary)
            {
                returnString += "\n" + pair.Key + " : " + $"{pair.Value:F3}";
            }
            return returnString;
        }
        public static double Mass
        {
            get => mass;
            set => mass = value;
        }
    }
}