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
using System.Reflection;

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
        private static double frictionForceEngineReductionCoefficient = 0.7;
        private static double dragCoefficient = 0.3; //TODO CHANGER SELON CHAR
        private static double frontCarArea = 2.5; //TODO CHANGER SELON CHAR
        private static double windDensity = 0;
        private static double ambientTemperature = 30; //TODO CHANGER SELON Hiver (-6) ou ete (30)
        private static double engineForce = 0;
        private static double engineTorque = 0;
        private static double torqueOut = 0;
        private static double horsePower = 0;

        public GameObject gear10;
        public GameObject gear11;
        public GameObject gear12;
        public GameObject gear13;
        public GameObject gear14;
        public GameObject gear15;
        public GameObject gear16;
        public GameObject gear17;
        public GameObject gear18;
        public GameObject gear19;
        public GameObject gear20;
        public GameObject gear21;
        public GameObject gear22;
        public GameObject gear23;
        public GameObject gear24;
        public GameObject gear25;
        public GameObject gear26;
        public GameObject gear27;
        public GameObject gear28;
        public GameObject gear29;
        public GameObject gear30;

        public GameObject posMenant;
        public GameObject posMenantRetour;
        public GameObject posGear1;
        public GameObject posGear1Retour;
        public GameObject posGear2;
        public GameObject posGear2Retour;
        public GameObject posGear3;
        public GameObject posGear3Retour;
        public GameObject posGear4;
        public GameObject posGear4Retour;
        public GameObject posGear5;
        public GameObject posGear5Retour;
        public GameObject posReculon;
        public GameObject posReculonRetour;

        public Slider sliderEchelleTemporelle;
        public float facteurTemporel;

        public PerfCalc(Car car)
        {
            this.car = car;
        }

        public double getRPM()
        {
            return RPM;
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

        public void chargerGears(Gear g)
        {
            string fieldName = "gear" + g.NbDents;
            FieldInfo field = GetType().GetField(fieldName);
            GameObject gearDuplique = Instantiate((GameObject)field.GetValue(this));

            string fieldNameRetour = "gear" + (40 - g.NbDents);
            FieldInfo fieldRetour = GetType().GetField(fieldNameRetour);
            GameObject gearDupliqueRetour = Instantiate((GameObject)fieldRetour.GetValue(this));


            if (g.Name.StartsWith("M")) {
                gearDuplique.transform.SetParent(posMenant.transform, false);
                gearDupliqueRetour.transform.SetParent(posMenantRetour.transform, false);
            } 
            else if (g.Name.StartsWith("R")) {
                gearDuplique.transform.SetParent(posReculon.transform, false);
                gearDupliqueRetour.transform.SetParent(posReculonRetour.transform, false);
            }
            else {
                string fieldNameGear = "posGear" + g.Name.Substring(9);
                FieldInfo fieldGear = GetType().GetField(fieldNameGear);

                string fieldNameGearRetour = fieldNameGear + "Retour";
                FieldInfo fieldGearRetour = GetType().GetField(fieldNameGearRetour);

                gearDuplique.transform.SetParent(((GameObject)fieldGear.GetValue(this)).transform, false);
                gearDupliqueRetour.transform.SetParent(((GameObject)fieldGearRetour.GetValue(this)).transform, false);
            }

            gearDuplique.transform.localPosition = Vector3.zero;
            gearDuplique.transform.localRotation = Quaternion.identity;
            gearDuplique.transform.localScale = Vector3.one;

            gearDupliqueRetour.transform.localPosition = Vector3.zero; 
            gearDupliqueRetour.transform.localRotation = Quaternion.identity;
            gearDupliqueRetour.transform.localScale = Vector3.one;
        }

        void Start()
        {
            if (Wheels.SelectedWheelType == 0) 
            {
                Wheels.WheelsSetValues(0.65, 0.55, 0.5, 1, 140, 300, 200, PerfCalc.Mass);
                Wheels.SelectedWheelType = 1;
            };
            mass = Wheels.Mass;
            Wheels.CalculateTyreFriction();
            frictionForceWheels = Wheels.FrictionForce;
            Gearbox.addGearToList();
            List<Gear> gears = Gearbox.GearsList();
            gearSelected = gears[2];

            foreach (Gear gear in gears)
            {
                chargerGears(gear);
            }

            sliderEchelleTemporelle.onValueChanged.AddListener(delegate { facteurTemporel = sliderEchelleTemporelle.value; });

        }

        void Update()
        {
            CalculateSpeedAndForces();
            getTorqueSelonMoteur();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                
                speed += acceleration / 30;
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
                acceleration = (speed / -75) - 0.5;
                speed += acceleration / 60;
                if (speed < 0)
                {
                    speed = 0;
                }
            }

            if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            {
                acceleration = (speed / -180) - 0.1;
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
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (gearSelected != Gearbox.Gears(2))
                {
                    indexGearSelect -= 1;
                } 
                else {
                    indexGearSelect = 0;
                }
                gearSelected = Gearbox.Gears(indexGearSelect);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && gearSelected != Gearbox.Gears(Gearbox.GearsList().Count - 2))
            {
                if (gearSelected != Gearbox.Gears(0))
                {
                    indexGearSelect += 1;
                }
                else
                {
                    indexGearSelect = 2;
                }
                gearSelected = Gearbox.Gears(indexGearSelect);
            }
         

            RPMOut = (int)(((double)RPM) * (calculateRatio(Gearbox.Gears(1), gearSelected, (gearSelected == Gears(0)))));
            torqueOut = (horsePower * 5252) / RPMOut;
            engineForce = torqueOut * (Wheels.Radius / 100);

            rotateComponents();

            Wheels.CalculateTyreFriction();
            WriteStats();
        }

        private void WriteStats()
        {
            if (ValueText != null)
            {
                ValueText.text = "Stats: "
               + "\nRPM: " + RPM.ToString()
               + "\nRPM Sortie: " + RPMOut.ToString()
               + "\nTorque Moteur: " + $"{engineTorque:F4}"
               + "\nPuissance (Hp): " + $"{horsePower:F4}"
               + "\nTorque Output: " + $"{torqueOut:F4}"
               + "\nForce du moteur (N): " + $"{engineForce:F4}"
               + "\nEngrenage: " + gearSelected.Name.ToString()
               + "\nAcceleration (m/s^2): " + $"{acceleration:F4}"
               + "\nVitesse (m/s): " + $"{speed:F3}" + " Vitesse (km/h): " + $"{(speed*3.6):F3}"
               + "\nForce de friction pneus (N): " + $"{frictionForceWheels:F3}"
               + "\nForce de friction vent (N): " + $"{frictionForceWind:F3}"
               + "\n\nPneus: "
               + DictionnaryToString(Wheels.GetInfosWheels)
               + Wheels.getAdherenceString();
            }
        }

        private void rotateComponents()
        {
            double angleRotation = 0;
            foreach (GameObject pos in FindObjectsOfType<GameObject>())
            {
                if (pos != null && pos.name.Substring(0, 3).Equals("Pos"))
                {
                    if (!pos.name.EndsWith("Retour"))
                    {
                        for (int i = 0; i < Gearbox.GearsList().Count; i++)
                        {
                            if (pos.name[4..].Equals(Gearbox.Gears(i).Name))
                            {
                                angleRotation = (calculateRatio(Gearbox.Gears(1), Gearbox.Gears(i), false) * RPM * 360) / (60 * Time.deltaTime);// * facteurTemporel;
                            }
                        }
                    } 
                    else
                    {
                        angleRotation = (calculateRatio(Gearbox.Gears(1), new Gear(40 - Gearbox.Gears(1).NbDents, 1, "Retour"), true) * RPM * 360) / (60 * Time.deltaTime);// * facteurTemporel;
                    }

                    pos.transform.Rotate(Vector3.forward, (float)angleRotation);
                }
            }
        }

        private void CalculateSpeedAndForces()
        {
            windDensity = 101.3 / (8.395 * ambientTemperature);
            frictionForceWind = 0.5 * dragCoefficient * frontCarArea * windDensity * (speed * speed);
            acceleration = ((frictionForceEngineReductionCoefficient * engineForce) - (frictionForceWheels + (6.5 * frictionForceWind))) / (mass);
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
        public static double Speed
        {
            get => speed;
            set => speed = value;
        }
        public static double Acceleration
        {
            get => acceleration;
            set => acceleration = value;
        }
    }
}