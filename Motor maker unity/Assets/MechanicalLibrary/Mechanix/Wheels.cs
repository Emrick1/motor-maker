using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanix
{
    /// <summary>
    /// <c>Classe représentant des paramètres de pneu.</c>
    /// </summary>
    public class Wheels : MonoBehaviour
    {
        /// <summary>
        /// Masse du pneu.
        /// </summary>
        private static double mass;
        /// <summary>
        /// Coefficent d'adhérence entre le pneu et de l'asphalte.
        /// </summary>
        private static double roadAdherence; //coefficient entre 0 et 1
        /// <summary>
        /// Coefficent d'adhérence entre le pneu et de la terre.
        /// </summary>
        private static double dirtAdherence; //coefficient entre 0 et 1
        /// <summary>
        /// Coefficent d'adhérence entre le pneu et de L'eau ou de la neige.
        /// </summary>
        private static double waterAndSnowAdherence; //coefficient entre 0 et 1
        /// <summary>
        /// Pression du pneu.
        /// </summary>
        private static double pressure = 70;
        /// <summary>
        /// Pression minimale du pneu.
        /// </summary>
        private static double minPressure = 69;
        /// <summary>
        /// Surface de contact du pneu.
        /// </summary>
        private static double contactArea;
        /// <summary>
        /// Rigidité radial du pneu.
        /// </summary>
        private static double radialRigidity;
        /// <summary>
        /// Déflexion radiale du pneu.
        /// </summary>
        private static double radialTyreDeflexion;
        /// <summary>
        /// Rayon du pneu.
        /// </summary>
        private static double radius;
        /// <summary>
        /// Largeur du pneu.
        /// </summary>
        private static double width;
        /// <summary>
        /// Force de friction du pneu avec le sol..
        /// </summary>
        private static double frictionForce;
        /// <summary>
        /// Force normale sur le pneu faite par le sol.
        /// </summary>
        private static double normalForce;
        /// <summary>
        /// Poids de la voiture (en N).
        /// </summary>
        private static double carLoad;
        /// <summary>
        /// Type de pneu selectionné.
        /// </summary>
        private static int selectedWheelType = 0;
        /// <summary>
        /// Sélecteur de pression du pneu.
        /// </summary>
        public Slider PressureSlider;
        /// <summary>
        /// Zone de texte contenat les statistiques du pneu.
        /// </summary>
        public TextMeshProUGUI WheelsStats;
        /// <summary>
        /// Boutton pour sélectionner le type de pneu numéro 1.
        /// </summary>
        public Button WheelType1;
        /// <summary>
        /// Boutton pour sélectionner le type de pneu numéro 2.
        /// </summary>
        public Button WheelType2;
        /// <summary>
        /// Boutton pour sélectionner le type de pneu numéro 3.
        /// </summary>
        public Button WheelType3;
        /// <summary>
        /// Boutton pour sélectionner le type de pneu numéro 4.
        /// </summary>
        public Button WheelType4;
        /// <summary>
        /// Boutton pour sélectionner le type de pneu numéro 5.
        /// </summary>
        public Button WheelType5;
        /// <summary>
        /// Affichage de la friction en fonction du pneu.
        /// </summary>
        public Slider FrictionSlider;
        /// <summary>
        /// Affichage de l'accélération en fonction du pneu.
        /// </summary>
        public Slider AccelerationSlider;
        /// /// <summary>
        /// Affichage de la vitesse maximale en fonction du pneu.
        /// </summary>
        public Slider VitesseMaxSlider;
        /// <summary>
        /// Zone de texte contenat les statistiques de la friction du pneu.
        /// </summary>
        public TextMeshProUGUI FrictionStats;
        /// <summary>
        /// Zone de texte contenat les statistiques de l'accélération du pneu.
        /// </summary>
        public TextMeshProUGUI AccelStats;
        /// <summary>
        /// Zone de texte contenat les statistiques de la vitesse maximale du pneu.
        /// </summary>
        public TextMeshProUGUI VitesseMaxStats;
        /// <summary>
        /// GameObject d'un pneu de type sport.
        /// </summary>
        public GameObject PneuSport;
        /// <summary>
        /// GameObject d'un pneu de type tout-tewrrain.
        /// </summary>
        public GameObject PneuToutTerrain;
        /// <summary>
        /// GameObject d'un pneu de type été..
        /// </summary>
        public GameObject PneuEte;
        /// <summary>
        /// GameObject d'un pneu de type course.
        /// </summary>
        public GameObject PneuCourse;
        /// <summary>
        /// GameObject d'un pneu de type professionnel.
        /// </summary>
        public GameObject PneuProffessionnel;
        /// <summary>
        /// GameObject d'un pneu du type sélectionné.
        /// </summary>
        private GameObject PneuChoisi;

        /// <summary>
        /// Construit un pneu avec les paramètres choisis.
        /// </summary>
        /// <param name="roadAdherenceSet">Adhérence à l'asphalte.</param>
        /// <param name="dirtAdherenceSet">Adhérence à la terre.</param>
        /// <param name="waterAndSnowAdherenceSet">Adhérence à l'eau et la neige.</param>
        /// <param name="generalAdherenceSet">Adhérence générale.</param>
        /// <param name="radialRigiditySet">Rigidité radiale.</param>
        /// <param name="radiusSet">Rayon.</param>
        /// <param name="widthSet">Largeu.r</param>
        /// <param name="massSet">Masse.</param>
        public static void WheelsSetValues(double roadAdherenceSet,
                      double dirtAdherenceSet,
                      double waterAndSnowAdherenceSet,
                      double generalAdherenceSet,
                      double radialRigiditySet,
                      double radiusSet,
                      double widthSet,
                      double massSet)
        {
            radialRigidity = radialRigiditySet;
            radius = radiusSet;
            width = widthSet;
            mass = massSet;
            carLoad = mass * 9.81;
            roadAdherence = roadAdherenceSet;
            dirtAdherence = dirtAdherenceSet;
            waterAndSnowAdherence = waterAndSnowAdherenceSet;
            CalculateTyreFriction();
        }

        public static double Mass
        {
            get => mass;
            set => mass = value;
        }

        public static double Pressure
        {
            get => pressure;
            set => pressure = value;
        }

        public static double FrictionForce
        {
            get => frictionForce;
            set => frictionForce = value;
        }

        public static int SelectedWheelType
        {
            get => selectedWheelType;
            set => selectedWheelType = value;
        }

        public static double Radius
        {
            get => radius;
            set => radius = value;
        }

        /// <summary>
        /// Retourne les statistiques du pneu dans un dictionary (Map).
        /// </summary>
        public static Dictionary<string, double> GetInfosWheels
        {
            get => new Dictionary<string, double> {
                { "Selected Wheel Type", (double) selectedWheelType},
                { "Pressure (psi)", pressure },
                { "Mass (kg)", mass },
                { "Car Load (N)", carLoad },
                { "Contact Area (cm^2)", contactArea },
                { "Radial Rigidity", radialRigidity },
                { "RadialTyreDeflexion (mm)", radialTyreDeflexion },
                { "Radius (mm)", radius },
                { "Width (mm)", width },
                { "Normal Force (N)", normalForce },
                { "Friction Force (N)", frictionForce }};
        }

        /// <summary>
        /// Calcule la friction du pneu avec le sol et gère l'acutalisation de celles-ci.
        /// </summary>
        public static void CalculateTyreFriction()
        {
            radialTyreDeflexion = ((carLoad / 4) / radialRigidity);
            contactArea = ((1 / Wheels.Pressure) + (1 / minPressure)) * (width * (1.4 * Math.Sqrt(radialTyreDeflexion * ((2 * radius) - radialTyreDeflexion)))) / 100000;
            normalForce = contactArea * carLoad;
            frictionForce = normalForce * roadAdherence; // TODO : general adherance? mu
        }

        /// <summary>
        /// Retire le pneu de l'affichage.
        /// </summary>
        public void desactiverModelPneus()
        {
            PneuSport.SetActive(false);
            PneuToutTerrain.SetActive(false);
            PneuEte.SetActive(false);
            PneuCourse.SetActive(false);
            PneuProffessionnel.SetActive(false);
        }

        void Start()
        {
            switch (selectedWheelType)
            {
                case 0:
                    minPressure = PressureSlider.minValue - 1;
                    Wheels.WheelsSetValues(0.65, 0.55, 0.5, 1, 140, 300, 200, PerfCalc.Mass);
                    selectedWheelType = 1;
                    PneuChoisi = PneuEte;
                    break;
                case 1:
                    PneuChoisi = PneuEte;
                    break;
                case 2:
                    PneuChoisi = PneuSport;
                    break;
                case 3:
                    PneuChoisi = PneuToutTerrain;
                    break;
                case 4:
                    PneuChoisi = PneuCourse;
                    break;
                case 5:
                    PneuChoisi = PneuProffessionnel;
                    break;
            }

            desactiverModelPneus();
            PneuChoisi.SetActive(true);
            PressureSlider.value = (float) pressure;

            WheelType1.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.65, 0.55, 0.5, 1, 140, 300, 200, 3000);
                selectedWheelType = 1;
                desactiverModelPneus();
                PneuChoisi = PneuEte;
                PneuEte.SetActive(true);
            });
            WheelType2.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.7, 0.6, 0.5, 1, 100, 310, 200, 3000 + 15);
                selectedWheelType = 2;
                desactiverModelPneus();
                PneuChoisi = PneuSport;
                PneuSport.SetActive(true);
            });
            WheelType3.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.8, 0.7, 0.6, 1, 80, 350, 220, 3000 + 30);
                selectedWheelType = 3;
                desactiverModelPneus();
                PneuChoisi = PneuToutTerrain;
                PneuToutTerrain.SetActive(true);
            });
            WheelType4.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.6, 0.5, 0.4, 1, 165, 250, 230, 3000 - 10);
                selectedWheelType = 4;
                desactiverModelPneus();
                PneuChoisi = PneuCourse;
                PneuCourse.SetActive(true); //todo changer type pneu
            });
            WheelType5.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.5, 0.4, 0.3, 1, 180, 220, 250, 3000 - 20);
                selectedWheelType = 5;
                desactiverModelPneus();
                PneuChoisi = PneuProffessionnel;
                PneuProffessionnel.SetActive(true);
            });
            
        }

        private void Update()
        {
            Wheels.Pressure = PressureSlider.value;
            Wheels.CalculateTyreFriction();
            WheelsStats.text = "Wheels' Stats:" +
                PerfCalc.DictionnaryToString(Wheels.GetInfosWheels)
                + getAdherenceString();

            foreach (GameObject pneu in FindObjectsOfType<GameObject>())
            {
                if (pneu != null && pneu.name.Length > 4 && pneu.name.Substring(0,4).Equals("Pneu") && pneu.activeSelf == true)
                {
                   pneu.transform.Rotate(new Vector3(0.25f, 1, 0.5f), 100 * Time.deltaTime);
                }
            }
            updateStatSliders();
        }

        /// <summary>
        /// Actualise l'état de l'affichage des barres de statistiques.
        /// </summary>
        private void updateStatSliders()
        {
            FrictionSlider.value = (float) (frictionForce);
            AccelerationSlider.value = (float) (frictionForce * 2);
            VitesseMaxSlider.value = (float) (350 - (frictionForce / 2));  //todo
            FrictionStats.text = $"{frictionForce:F3}" + " (N)";
            AccelStats.text = $"{((frictionForce * 120) / mass):F3}" + " (m/s^2)";
            VitesseMaxStats.text = $"{(350 - (frictionForce / 2)):F3}" + " (m/s)";

        }

        /// <summary>
        /// Retourne les statiques d'adhérence.
        /// </summary>
        /// <returns></returns>
        public static string getAdherenceString()
        {
            string adherance = "\nAdherence: Road " + roadAdherence + " Dirt " + dirtAdherence + " \nWet Road/Snow " + waterAndSnowAdherence;
            return adherance;
        }
    }
}