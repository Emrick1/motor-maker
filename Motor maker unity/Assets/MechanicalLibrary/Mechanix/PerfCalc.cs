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
        private static int RPMmax = 9000;
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
        private static double engineForce = 1000;

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

        void Start()
        {
            Wheels.WheelsSetValues(0.65, 0.55, 0.5, 1, 140, 300, 200, PerfCalc.Mass);
            Wheels.SelectedWheelType = 1;
            Wheels.CalculateTyreFriction();
            Gearbox.addGearToList();
            List<Gear> gears = Gearbox.GearsList();
            gearSelected = gears[1];
            
        }

        void Update()
        {
            CalculateSpeedAndForces();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                speed += acceleration / 80;
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
                    RPM = (int)((RPM * 0.999) - 2);
                }
            }
            else if (RPM < RPMmin)
            {
                RPM = RPMmin;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                speed -= acceleration / 20;
                if (speed < 0)
                {
                    speed = 0;
                }
            } 
            else if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)&& !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))))
            {
                speed -= acceleration / 40;
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && gearSelected != Gearbox.Gears(1))
                {
                    gearSelected = Gearbox.Gears((Gearbox.GearsList().IndexOf(gearSelected)) - 1);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) && gearSelected != Gearbox.Gears(Gearbox.GearsList().Count - 2));
                {
                    gearSelected = Gearbox.Gears((Gearbox.GearsList().IndexOf(gearSelected)) + 1);
                }
            }

            RPMOut = (int)(((double)RPM) * (calculateRatio(Gearbox.Gears(0), gearSelected, false)));
            

            ValueText.text = "Stats :"
                + "\nRPM:" + RPM.ToString()
                + "\nRPM Output:" + RPMOut.ToString()
                + "\nGear:" + gearSelected.Name.ToString()
                + "\nAcceleration:" + acceleration
                + "\nSpeed:" + speed
                + "\nFriction Force Wheels:" + frictionForceWheels
                + "\nFriction Force Wind:" + frictionForceWind
                + "\n\nWheels:"
                + DictionnaryToString(Wheels.GetInfosWheels)
                + Wheels.getAdherenceString();
        }

        private void CalculateSpeedAndForces()
        {
            windDensity = 101.3 / (8.395 * ambientTemperature);
            frictionForceWind = 0.5 * dragCoefficient * frontCarArea * windDensity * (speed * speed);
            acceleration = ((frictionForceEngineReductionCoefficient * engineForce) - (frictionForceWheels + frictionForceWind) ) / (mass/4);
            if (acceleration < 0)
            {
                acceleration = 0;
            }
        }

        public static string DictionnaryToString(Dictionary<string, double> dictionary)
        {
            string returnString = "";
            foreach (KeyValuePair<string, double> pair in dictionary)
            {
                returnString += "\n" + pair.Key + " : " + pair.Value;
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