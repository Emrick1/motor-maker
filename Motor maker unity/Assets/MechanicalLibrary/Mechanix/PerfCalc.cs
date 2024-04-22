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
    /// <c>Classe servant � calculer les performances d'une voiture.</c>
    /// </summary>
    public class PerfCalc : MonoBehaviour
    {
        /// <summary>
        /// Engrenage selectionn�.
        /// </summary>
        public static Gear gearSelected;//gearSelected
        /// <summary>
        /// Zone de texte utilis�e pour afficher les informations relatives au performances.
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
        /// Rotations par minutes (RPM) � la sortie du moteur.
        /// </summary>
        private static double RPMOut = 0;
        /// <summary>
        /// V�locit� de la voiture actuelle.
        /// </summary>
        private static double speed = 0;
        /// <summary>
        /// Acc�l�ration de la voiture actuelle.
        /// </summary>
        private static double acceleration = 0;
        /// <summary>
        /// Friction produite sur les roues.
        /// </summary>
        private static double frictionForceWheels = 0;
        /// <summary>
        /// Friction produite par le vent.
        /// </summary>
        private static double frictionForceWind = 0;
        /// <summary>
        /// Coefficent de r�duction de frottement du moteur.
        /// </summary>
        private static double frictionForceEngineReductionCoefficient = 0.7;
        /// <summary>
        /// Coefficient de le train�e de la voiture.
        /// </summary>
        private static double dragCoefficient = 0.3; //TODO CHANGER SELON CHAR
        /// <summary>
        /// Aire du devant de la voiture.
        /// </summary>
        private static double frontCarArea = 2.5; //TODO CHANGER SELON CHAR
        /// <summary>
        /// Densit� du vent.
        /// </summary>
        private static double windDensity = 0;
        /// <summary>
        /// Temp�rature ambiante.
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
        /// �chelle temporelle utilis�e par les calculs de performances.
        /// </summary>
        private static float echelleTemporelle = 0.101f;

        private static double modificateurReculons = 0.1;

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
        /// GameObject de l'engrenage menant dans la sc�ne actuelle.
        /// </summary>
        public GameObject posMenant;
        /// <summary>
        /// GameObject de l'engrenage menant dans les autres sc�ne.
        /// </summary>
        public GameObject posMenantRetour;
        /// <summary>
        /// GameObject de l'engrenage num�ro 1 dans la sc�ne actuelle.
        /// </summary>
        public GameObject posGear1;
        /// <summary>
        /// GameObject de l'engrenage num�ro 1 dans les autres sc�ne.
        /// </summary>
        public GameObject posGear1Retour;
        /// <summary>
        /// GameObject de l'engrenage num�ro 2 dans la sc�ne actuelle.
        /// </summary>
        public GameObject posGear2;
        /// <summary>
        /// GameObject de l'engrenage num�ro 2 dans les autres sc�ne.
        /// </summary>
        public GameObject posGear2Retour;
        /// <summary>
        /// GameObject de l'engrenage num�ro 3 dans la sc�ne actuelle.
        /// </summary>
        public GameObject posGear3;
        /// <summary>
        /// GameObject de l'engrenage num�ro 3 dans les autres sc�ne.
        /// </summary>
        public GameObject posGear3Retour;
        /// <summary>
        /// GameObject de l'engrenage num�ro 4 dans la sc�ne actuelle.
        /// </summary>
        public GameObject posGear4;
        /// <summary>
        /// GameObject de l'engrenage num�ro 4 dans les autres sc�ne.
        /// </summary>
        public GameObject posGear4Retour;
        /// <summary>
        /// GameObject de l'engrenage num�ro 5 dans la sc�ne actuelle.
        /// </summary>
        public GameObject posGear5;
        /// <summary>
        /// GameObject de l'engrenage num�ro 5 dans les autres sc�ne.
        /// </summary>
        public GameObject posGear5Retour;
        /// <summary>
        /// GameObject de l'engrenage pour le reculons dans la sc�ne actuelle.
        /// </summary>
        public GameObject posReculon;
        /// <summary>
        /// GameObject de l'engrenage pour le reculons dans les autres sc�ne.
        /// </summary>
        public GameObject posReculonRetour;

        public GameObject cylMenant;
        public GameObject cylRetour;
        public GameObject cylChoisi;
        public GameObject cylBloque12;
        public Vector3 posCylBloque12;
        public GameObject cylBloque34;
        public Vector3 posCylBloque34;
        public GameObject cylBloque5R;
        public Vector3 posCylBloque5R;
        public static bool VolantToggleBool = false;
        /// <summary>
        /// �chelle temporelle de la rotation de la bo�te de vitesse.
        /// </summary>
        public Slider sliderEchelleTemporelle;

        public Toggle ToggleVolant;


        /// <summary>
        /// Calcule la torque produit par un moteur.
        /// </summary>
        public void getTorqueSelonMoteur()
        {

            //V6
            if (engineList.moteurSelected == 1 || engineList.moteurSelected == 0)
            {
                RPMmax = 7100;
                if (RPM <= 4000)
                {
                    engineTorque = (0.000016375 * (Math.Pow((RPM), 2))) + 173;
                }
                else
                {
                    engineTorque = (-0.000001625 * (Math.Pow(((RPM) - 6000), 2))) + 500;
                }
                
            //v8
            } else if (engineList.moteurSelected == 2) {
                RPMmax = 7500;
                if (RPM <= 4000)
                {
                    engineTorque = (0.000028 * (Math.Pow((RPM), 2))) + 380;
                }
                else
                {
                    engineTorque = (-0.000001625 * (Math.Pow(((RPM) - 6000), 2))) + 707;
                }

            } else
            //électrique
            {
                RPMmax = 13000;
                if (RPM <= 10000)
                {
                    engineTorque = (800);
                }
                else
                {
                    engineTorque = ((0.000042 * Math.Pow(RPM - 13000, 2) + 400));
                }

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

            Material gearMat = new Material(gear10.GetComponent<Renderer>().sharedMaterial);

            if (field != null && g.NbDents <= 30)
            {
                GameObject gearDuplique = Instantiate((GameObject)field.GetValue(this));
                GameObject gearDupliqueRetour = Instantiate((GameObject)fieldRetour.GetValue(this));


                if (g.Name.StartsWith("M"))
                {
                    gearDuplique.transform.SetParent(posMenant.transform, false);
                    gearDupliqueRetour.transform.SetParent(posMenantRetour.transform, false);

                    gearMat.color = Color.gray;
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

                    gearMat.color = Color.HSVToRGB((float.Parse(g.Name.Substring(9))-1f)/5f, 1, 0.5f + (((float)g.NbDents)-10f)*(1f-0.5f)/(30f-10f));

                    gearDuplique.transform.SetParent(((GameObject)fieldGear.GetValue(this)).transform, false);
                    gearDupliqueRetour.transform.SetParent(((GameObject)fieldGearRetour.GetValue(this)).transform, false);
                }

                gearDuplique.transform.localPosition = Vector3.zero;
                gearDuplique.transform.localRotation = Quaternion.identity;
                gearDuplique.transform.localScale = Vector3.one;
                gearDuplique.GetComponent<Renderer>().material = gearMat;

                gearDupliqueRetour.transform.localPosition = Vector3.zero;
                gearDupliqueRetour.transform.localRotation = Quaternion.identity;
                gearDupliqueRetour.transform.localScale = Vector3.one;

            }
           
        }

        void Start()
        {
            if (ToggleVolant != null) 
            {
            ToggleVolant.isOn = VolantToggleBool;
            ToggleVolant.onValueChanged.AddListener(delegate { VolantToggleBool = ToggleVolant.isOn; });
            }
            if (Wheels.SelectedWheelType == 0) 
            {
                Wheels.WheelsSetValues(0.65, 0.55, 0.5, 1, 140, 300, 200, PerfCalc.Mass);
                Wheels.SelectedWheelType = 1;
            }
            mass = Wheels.Mass;
            Wheels.CalculateTyreFriction();
            frictionForceWheels = Wheels.FrictionForce;
            if(Gearbox.GearsList().Count == 0)
            {
                Gearbox.addGearToList();
            }
            
            List<Gear> gears = Gearbox.GearsList();
            gearSelected = gears[2];

            posCylBloque12 = cylBloque12.transform.position;
            posCylBloque34 = cylBloque34.transform.position;
            posCylBloque5R = cylBloque5R.transform.position;
            translationCylindresBloque();
            
            foreach (Gear gear in gears)
            {
                chargerGears(gear);
            }

            sliderEchelleTemporelle.onValueChanged.AddListener(delegate { echelleTemporelle = sliderEchelleTemporelle.value; });

        }

        void Update()
        {
            float y = 32767;
            float z = 32767;
            if (VolantToggleBool)
            { 
                LogitechGSDK.DIJOYSTATE2ENGINES rec;
                rec = LogitechGSDK.LogiGetStateUnity(0);
                z = rec.lRz;
                y = rec.lY;
            }

            if (Input.GetKey(KeyCode.W))
            {
                y = -32767;
            }
            if (Input.GetKey(KeyCode.S))
            {
                z = -32767;
            }
            CalculateSpeedAndForces();
            getTorqueSelonMoteur();
            
            if (!gearSelected.Name.Equals("Reculons"))
            {
                modificateurReculons = 0.15;
                if (speed < 0)
                {
                    speed = (speed * 0.999) + 0.03;
                }
            }
            else
            {
                modificateurReculons = -0.15;
                if (speed > 0)
                {
                    speed = (speed * 0.999) - 0.03;
                } 
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || (y != 0 && y <= 32761))
            {
                
                speed += acceleration / 30;
                if (RPM < (((y - 32767) / 32767) * -0.5) * RPMmax)
                {
                    RPM += (((y - 32767) / 32767) * -0.5); 
                } 
                else
                {
                    RPM -= (((y - 60000) / 32767) * -0.5);
                }
            }
            else if (RPM > RPMmin)
            {
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

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || (z != 0 && z <= 32761))
            {
                acceleration = ((speed / -75) * (((z - 32767) / 32767) * -0.5)) - modificateurReculons * 5;
                speed += acceleration / 60;
            }

            if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !(z != 0 && z <= 32761) && !(y != 0 && y <= 32761))
            {

                acceleration = (speed / -180) - modificateurReculons;
                speed += acceleration / 60;
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

            translationCylindresBloque();


            RPMOut = (int)(((double)RPM) * (calculateRatio(Gearbox.Gears(1), gearSelected, (gearSelected == Gears(0)))));
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
    /// G�re l'affichage de test des dif�rentes donn�es calcul�es.
    /// </summary>
        private void WriteStats()
        {
            if (ValueText != null)
            {
                ValueText.text = "Stats: "
               + "\nRPM: " + $"{RPM:F3}"
               + "\nRPM Sortie: " + $"{RPMOut:F3}"
               + "\nTorque Moteur: " + $"{engineTorque:F3}"
               + "\nPuissance (Hp): " + $"{horsePower:F3}"
               + "\nTorque Output: " + $"{torqueOut:F3}"
               + "\nForce du moteur (N): " + $"{engineForce:F3}"
               + "\nEngrenage: " + gearSelected.Name.ToString()
               + "\nAcceleration (m/s^2): " + $"{(acceleration * 10):F3}"
               + "\nVitesse (m/s): " + $"{speed:F3}" + " Vitesse (km/h): " + $"{(speed*3.6):F3}"
               + "\nForce de friction pneus (N): " + $"{frictionForceWheels:F3}"
               + "\nForce de friction vent (N): " + $"{frictionForceWind:F3}"
               + "\n\nPneus: "
               + DictionnaryToString(Wheels.GetInfosWheels)
               + Wheels.getAdherenceString();
            }
            if (echelleTText != null)
            {
                echelleTText.text = echelleTemporelle.ToString()[..4];
            }
        }

        private void updateCylinders(string cylName, double angleRotation)
        {
            string fieldName = "cyl" + cylName;
            FieldInfo field = GetType().GetField(fieldName);
            GameObject cyl = (GameObject)field.GetValue(this);

            if (cylName.Equals("Choisi"))
            {
                Material cylMat = new Material(gear10.GetComponent<Renderer>().sharedMaterial);
                cylMat.color = Color.HSVToRGB((float.Parse(gearSelected.Name.Substring(9)) - 1f) / 5f, 1, 0.5f + (((float)gearSelected.NbDents) - 10f) * (1f - 0.5f) / (30f - 10f));
                cyl.GetComponent<Renderer>().material = cylMat;
            }

            cyl.transform.Rotate(Vector3.forward, (float)angleRotation * Time.deltaTime * echelleTemporelle);
        }

        /// <summary>
        /// G�re la rotation visuelle des composants m�caniques dans le menu.
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
                        updateCylinders("Retour", angleRotation);
                    }

                    if (pos.name[7..].Equals(gearSelected.Name[9..]))
                    {
                        updateCylinders("Choisi", angleRotation);
                    }
                    
                    if (pos.name[3..].Equals("Menant"))
                    {
                        updateCylinders("Menant", angleRotation);
                    }

                    pos.transform.Rotate(Vector3.forward, (float)angleRotation * Time.deltaTime * echelleTemporelle);
                }
            }
        }

        public void translationCylindresBloque()
        {
            Vector3 direcion = Vector3.right;
            String noGear = gearSelected.Name.Substring(9);

            if (gearSelected.Name.StartsWith("R"))
            {
                cylBloque5R.transform.position = Vector3.Lerp(cylBloque5R.transform.position, posCylBloque5R + direcion * 0.25f, 3 * Time.deltaTime);
            } 
            else
            {
                if (int.Parse(noGear) % 2 == 1)
                {
                    direcion = Vector3.left;
                }

                foreach (GameObject g in FindObjectsOfType<GameObject>())
                {
                    if(g.name.StartsWith("CylBloque"))
                    {
                        string fieldName = "pos" + g.name;
                        FieldInfo field = GetType().GetField(fieldName);

                        if (g.name.Contains(noGear))
                        {
                            g.transform.position = Vector3.Lerp(g.transform.position, (Vector3)field.GetValue(this) + direcion * 0.25f, 3*Time.deltaTime);
                        }
                        else
                        {
                            g.transform.position = Vector3.Lerp(g.transform.position, (Vector3)field.GetValue(this), 3*Time.deltaTime);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fait le calcul de vitesse et de force selon les pi�ces choisies.
        /// </summary>
        private void CalculateSpeedAndForces()
        {
            windDensity = 101.3 / (8.395 * ambientTemperature);
            frictionForceWind = 0.5 * dragCoefficient * frontCarArea * windDensity * (speed * speed);
            if (!gearSelected.Name.Equals("Reculons"))
            {
                acceleration = ((frictionForceEngineReductionCoefficient * engineForce) - frictionForceWheels - (6.5 * frictionForceWind)) / (mass);
                if (acceleration * 60 > (Wheels.FrictionForce * 120) / mass)
                {
                    acceleration = ((Wheels.FrictionForce * 120) / mass) / 60;
                }
            }
            else
            {
                acceleration = ((frictionForceEngineReductionCoefficient * engineForce * 0.5) + (0.5 * frictionForceWheels + (20 * frictionForceWind))) / (mass);
            }
        }

        /// <summary>
        /// Transf�re le contenu d'un dictionary (Map) en cha�ne de carat�re (String).
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>Cha�ne de carat�re du contenu du dictionary.</returns>
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
        public static double GetRPM
        {
            get => RPM;
            set => RPM = value;
        }
    }
}