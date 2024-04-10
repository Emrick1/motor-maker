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
using UnityEngine.SceneManagement;

namespace Mechanix
{
    /// <summary>
    /// <c>Classe servant à calculer les performances d'une voiture.</c>
    /// </summary>
    public class PerfCalc : MonoBehaviour
    {
        /// <summary>
        /// Instance de la voiture utilisée par la classe.
        /// </summary>
        private Car car;
        /// <summary>
        /// Engrenage selectionné.
        /// </summary>
        public static Gear gearSelected;//gearSelected
        /// <summary>
        /// Zone de texte utilisée pour afficher les informations relatives au performances.
        /// </summary>
        public TextMeshProUGUI ValueText;

        public TextMeshProUGUI echelleTText;
        /// <summary>
        /// Masse de la voiture actuelle.
        /// </summary>
        private static double mass = 3000;
        /// <summary>
        /// Rotations par minute (RPM) maximales du moteur actuelle.
        /// </summary>
        private static int RPMmax = 7100;
        /// <summary>
        /// Rotations par minute (RPM) minimales du moteur actuelle.
        /// </summary>
        private static int RPMmin = 600;
        /// <summary>
        /// Rotations par minute (RPM) actuelles.
        /// </summary>
        private static double RPM = 0;
        /// <summary>
        /// Rotations par minutes (RPM) à la sortie du moteur.
        /// </summary>
        private static double RPMOut = 0;
        /// <summary>
        /// Vélocité de la voiture actuelle.
        /// </summary>
        private static double speed = 0;
        /// <summary>
        /// Accélération de la voiture actuelle.
        /// </summary>
        private static double acceleration = 0;
        /// <summary>
        /// Facteur d'augmentation.
        /// </summary>
        private static float facteurAugmentation;
        /// <summary>
        /// Variable de temps utilisée pour les calculs.
        /// </summary>
        private static float t;
        /// <summary>
        /// Friction produite sur les roues.
        /// </summary>
        private static double frictionForceWheels = 0;
        /// <summary>
        /// Friction produite par le vent.
        /// </summary>
        private static double frictionForceWind = 0;
        /// <summary>
        /// Coefficent de réduction de frottement du moteur.
        /// </summary>
        private static double frictionForceEngineReductionCoefficient = 0.7;
        /// <summary>
        /// Coefficient de le trainée de la voiture.
        /// </summary>
        private static double dragCoefficient = 0.3; //TODO CHANGER SELON CHAR
        /// <summary>
        /// Aire du devant de la voiture.
        /// </summary>
        private static double frontCarArea = 2.5; //TODO CHANGER SELON CHAR
        /// <summary>
        /// Densité du vent.
        /// </summary>
        private static double windDensity = 0;
        /// <summary>
        /// Température ambiante.
        /// </summary>
        private static double ambientTemperature = 30; //TODO CHANGER SELON Hiver (-6) ou ete (30)
        /// <summary>
        /// Force du moteur.
        /// </summary>
        private static double engineForce = 0;
        /// <summary>
        /// Torque du moteur.
        /// </summary>
        private static double engineTorque = 0;
        /// <summary>
        /// Torque produit par le moteur.
        /// </summary>
        private static double torqueOut = 0;
        /// <summary>
        /// Puissance du moteur en chevaux-vapeur (HP).
        /// </summary>
        private static double horsePower = 0;
        /// <summary>
        /// Échelle temporelle utilisée par les calculs de performances.
        /// </summary>
        private static float echelleTemporelle = 0.1f;

        /// <summary>
        /// GameObject pour un engrenage de 10 dents.
        /// </summary>
        public GameObject gear10;
        /// <summary>
        /// GameObject pour un engrenage de 11 dents.
        /// </summary>
        public GameObject gear11;
        /// <summary>
        /// GameObject pour un engrenage de 12 dents.
        /// </summary>
        public GameObject gear12;
        /// <summary>
        /// GameObject pour un engrenage de 13 dents.
        /// </summary>
        public GameObject gear13;
        /// <summary>
        /// GameObject pour un engrenage de 14 dents.
        /// </summary>
        public GameObject gear14;
        /// <summary>
        /// GameObject pour un engrenage de 15 dents.
        /// </summary>
        public GameObject gear15;
        /// <summary>
        /// GameObject pour un engrenage de 16 dents.
        /// </summary>
        public GameObject gear16;
        /// <summary>
        /// GameObject pour un engrenage de 17 dents.
        /// </summary>
        public GameObject gear17;
        /// <summary>
        /// GameObject pour un engrenage de 18 dents.
        /// </summary>
        public GameObject gear18;
        /// <summary>
        /// GameObject pour un engrenage de 19 dents.
        /// </summary>
        public GameObject gear19;
        /// <summary>
        /// GameObject pour un engrenage de 20 dents.
        /// </summary>
        public GameObject gear20;
        /// <summary>
        /// GameObject pour un engrenage de 21 dents.
        /// </summary>
        public GameObject gear21;
        /// <summary>
        /// GameObject pour un engrenage de 22 dents.
        /// </summary>
        public GameObject gear22;
        /// <summary>
        /// GameObject pour un engrenage de 23 dents.
        /// </summary>
        public GameObject gear23;
        /// <summary>
        /// GameObject pour un engrenage de 24 dents.
        /// </summary>
        public GameObject gear24;
        /// <summary>
        /// GameObject pour un engrenage de 25 dents.
        /// </summary>
        public GameObject gear25;
        /// <summary>
        /// GameObject pour un engrenage de 26 dents.
        /// </summary>
        public GameObject gear26;
        /// <summary>
        /// GameObject pour un engrenage de 27 dents.
        /// </summary>
        public GameObject gear27;
        /// <summary>
        /// GameObject pour un engrenage de 28 dents.
        /// </summary>
        public GameObject gear28;
        /// <summary>
        /// GameObject pour un engrenage de 29 dents.
        /// </summary>
        public GameObject gear29;
        /// <summary>
        /// GameObject pour un engrenage de 30 dents.
        /// </summary>
        public GameObject gear30;
        /// <summary>
        /// GameObject de l'engrenage menant dans la scène actuelle.
        /// </summary>
        public GameObject posMenant;
        /// <summary>
        /// GameObject de l'engrenage menant dans les autres scène.
        /// </summary>
        public GameObject posMenantRetour;
        /// <summary>
        /// GameObject de l'engrenage numéro 1 dans la scène actuelle.
        /// </summary>
        public GameObject posGear1;
        /// <summary>
        /// GameObject de l'engrenage numéro 1 dans les autres scène.
        /// </summary>
        public GameObject posGear1Retour;
        /// <summary>
        /// GameObject de l'engrenage numéro 2 dans la scène actuelle.
        /// </summary>
        public GameObject posGear2;
        /// <summary>
        /// GameObject de l'engrenage numéro 2 dans les autres scène.
        /// </summary>
        public GameObject posGear2Retour;
        /// <summary>
        /// GameObject de l'engrenage numéro 3 dans la scène actuelle.
        /// </summary>
        public GameObject posGear3;
        /// <summary>
        /// GameObject de l'engrenage numéro 3 dans les autres scène.
        /// </summary>
        public GameObject posGear3Retour;
        /// <summary>
        /// GameObject de l'engrenage numéro 4 dans la scène actuelle.
        /// </summary>
        public GameObject posGear4;
        /// <summary>
        /// GameObject de l'engrenage numéro 4 dans les autres scène.
        /// </summary>
        public GameObject posGear4Retour;
        /// <summary>
        /// GameObject de l'engrenage numéro 5 dans la scène actuelle.
        /// </summary>
        public GameObject posGear5;
        /// <summary>
        /// GameObject de l'engrenage numéro 5 dans les autres scène.
        /// </summary>
        public GameObject posGear5Retour;
        /// <summary>
        /// GameObject de l'engrenage pour le reculons dans la scène actuelle.
        /// </summary>
        public GameObject posReculon;
        /// <summary>
        /// GameObject de l'engrenage pour le reculons dans les autres scène.
        /// </summary>
        public GameObject posReculonRetour;
        /// <summary>
        /// Échelle temporelle de la rotation de la boîte de vitesse.
        /// </summary>
        public Slider sliderEchelleTemporelle;

        /// <summary>
        /// Construit une instance du calculateur de performance.
        /// </summary>
        /// <param name="car">Instance de voiture voulue.</param>
        public PerfCalc(Car car)
        {
            this.car = car;
        }

        public double getRPM()
        {
            return RPM;
        }

        /// <summary>
        /// Calcule la torque produit par un moteur.
        /// </summary>
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

        /// <summary>
        /// Charge un engrenage dans l'affichage du moteur graphique.
        /// </summary>
        /// <param name="g">Engrenage voulu.</param>
        public void chargerGears(Gear g)
        {
            string fieldName = "gear" + g.NbDents;
            FieldInfo field = GetType().GetField(fieldName);
            string fieldNameRetour = "gear" + (40 - g.NbDents);
            FieldInfo fieldRetour = GetType().GetField(fieldNameRetour);
            if (field != null && g.NbDents <= 30)
            {
                GameObject gearDuplique = Instantiate((GameObject)field.GetValue(this));
                GameObject gearDupliqueRetour = Instantiate((GameObject)fieldRetour.GetValue(this));


                if (g.Name.StartsWith("M"))
                {
                    gearDuplique.transform.SetParent(posMenant.transform, false);
                    gearDupliqueRetour.transform.SetParent(posMenantRetour.transform, false);
                }
                else if (g.Name.StartsWith("R"))
                {
                    gearDuplique.transform.SetParent(posReculon.transform, false);
                    gearDupliqueRetour.transform.SetParent(posReculonRetour.transform, false);
                }
                else
                {
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

            sliderEchelleTemporelle.onValueChanged.AddListener(delegate { echelleTemporelle = sliderEchelleTemporelle.value; });

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
         

            RPMOut = (int)(((double)RPM) * (calculateRatio(gearSelected, Gearbox.Gears(1), (gearSelected == Gears(0)))));
            torqueOut = (horsePower * 5252) / RPMOut;
            engineForce = torqueOut * (Wheels.Radius / 100);

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                rotateComponents();
            }

            Wheels.CalculateTyreFriction();
            WriteStats();
        }

    /// <summary>
    /// Gère l'affichage de test des diférentes données calculées.
    /// </summary>
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
            if (echelleTText != null)
            {
                echelleTText.text = echelleTemporelle.ToString().Substring(0,3);
            }
        }

        /// <summary>
        /// Gère la rotation visuelle des composants mécaniques dans le menu.
        /// </summary>
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
                                angleRotation = (calculateRatio(Gearbox.Gears(1), Gearbox.Gears(i), false) * RPM * 360) / (60);
                            }
                        }
                    } 
                    else
                    {
                        angleRotation = (calculateRatio(Gearbox.Gears(1), new Gear(40 - Gearbox.Gears(1).NbDents, 1, "Retour"), false) * RPM * 360) / (60);
                    }

                    Debug.Log("echelle " + echelleTemporelle);

                    pos.transform.Rotate(Vector3.forward, (float)angleRotation * Time.deltaTime * echelleTemporelle);
                }
            }
        }

        /// <summary>
        /// Fait le calcul de vitesse et de force selon les pièces choisies.
        /// </summary>
        private void CalculateSpeedAndForces()
        {
            windDensity = 101.3 / (8.395 * ambientTemperature);
            frictionForceWind = 0.5 * dragCoefficient * frontCarArea * windDensity * (speed * speed);
            acceleration = ((frictionForceEngineReductionCoefficient * engineForce) - (frictionForceWheels + (2 * frictionForceWind))) / (mass);
        }

        /// <summary>
        /// Transfère le contenu d'un dictionary (Map) en chaîne de caratère (String).
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>Chaîne de caratère du contenu du dictionary.</returns>
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