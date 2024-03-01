using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanix
{
    public class Wheels : MonoBehaviour, IMechanicalPiece
    {
        private static double mass;
        private static double asphaltAdherence; //coefficient entre 0 et 1
        private static double dirtAdherence; //coefficient entre 0 et 1
        private static double sandAdherence; //coefficient entre 0 et 1
        private static double generalAdherence; //coefficient entre 0 et 1
        private static double pressure;
        private static double contactArea;
        private static double radialRigidity;
        private static double radialTyreDeflexion;
        private static double radius;
        private static double width;
        private static double frictionForce;
        private static double normalForce;
        private static double carLoad;
        public Slider RpmSlider;

        public static void WheelsSetValues(double asphaltAdherenceSet,
                      double dirtAdherenceSet,
                      double sandAdherenceSet,
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
            asphaltAdherence = asphaltAdherenceSet;
            dirtAdherence = dirtAdherenceSet;
            sandAdherence = sandAdherenceSet;
            generalAdherence = generalAdherenceSet;
            UpdateGeneralAdherence();
            CalculateTyreFriction();
        }

        public static double Mass
        {
            get => mass;
            set => mass = value;
            }

        public static double AsphaltAdherence
        {
            get => asphaltAdherence;
            set
            {
                asphaltAdherence = value; 
                UpdateGeneralAdherence();
            }
        }

        public static double DirtAdherence
        {
            get => dirtAdherence;
            set
            {
                dirtAdherence = value; 
                UpdateGeneralAdherence();
            }
        }

        public static double SandAdherence
        {
            get => sandAdherence;
            set
            {
                sandAdherence = value;
                UpdateGeneralAdherence();
            }
        }

        public static double GeneralAdherence
        {
            get => generalAdherence;
            set => generalAdherence = value;
        }

        public static double Pressure
        {
            get => pressure;
            set => pressure = value;
        }

        public static Dictionary<string, double> GetInfosWheels
        {
            get => new Dictionary<string, double> { {"pressure", pressure}, { "pressure", pressure } };
        }

        public static void UpdateGeneralAdherence()
        {
            if (asphaltAdherence != null && dirtAdherence != null && sandAdherence != null)
            {
                generalAdherence = (asphaltAdherence + dirtAdherence + sandAdherence) / 3;
            } else
            {
                generalAdherence = 1;
            }
        }

        public static void CalculateTyreFriction()
        {
            radialTyreDeflexion = (carLoad / 4) / radialRigidity;
            contactArea = width * (1.4 * Math.Sqrt(radialTyreDeflexion * ((2 * radius) - radialTyreDeflexion)));
            normalForce = pressure * contactArea;
            frictionForce = normalForce * generalAdherence; // TODO : general adherance? mu
        }

        void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}