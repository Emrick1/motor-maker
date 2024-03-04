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
        private int gearSelected = 0;//gearSelected
        public TextMeshProUGUI ValueText;
        private static double mass = 3000;
        private static int RPMmax = 9000;
        private static int RPMmin = 600;
        private static double RPM = 0;
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

        public PerfCalc(Car car)
        {
            this.car = car;
        }

        /*
        public double RPMOutput() {
         double RpmOutPut;
         RpmOutPut = RPM * calculateRatio(Gear 1, Gear 2, false);
            return RpmOutPut;
        }
        */

        void Start()
        {
            
        }

        void Update()
        {

            if (Input.GetKey(KeyCode.W))
            {
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
                if (Input.GetKey(KeyCode.S))
                {
                    RPM = (int)((RPM * 0.999) - 2);
                }
            }
            else if (RPM < RPMmin)
            {
                RPM = RPMmin;
            }

            CalculateSpeedAndForces();

            ValueText.text = "Stats :"
                + "\nRPM:" + RPM.ToString()
                + "\n\nWheels:"
                + DictionnaryToString(Wheels.GetInfosWheels)
                + Wheels.getAdherenceString();
        }

        private void CalculateSpeedAndForces()
        {
            windDensity = 101.3 / (8.395 * ambientTemperature);
            frictionForceWind = 0.5 * dragCoefficient * frontCarArea * windDensity * (speed * speed);
            acceleration = (engineForce - (frictionForceWheels + frictionForceWind) ) / mass;
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