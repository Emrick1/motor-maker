using System;
using UnityEngine;

namespace Mechanix
{
    public class Wheels : MonoBehaviour, IMechanicalPiece
    {
        private double mass;
        private double asphaltAdherence; //coefficient entre 0 et 1
        private double dirtAdherence; //coefficient entre 0 et 1
        private double sandAdherence; //coefficient entre 0 et 1
        private double generalAdherence; //coefficient entre 0 et 1
        private double pressure;
        private double contactArea;
        private double radialRigidity;
        private double radialTyreDeflexion;
        private double radius;
        private double width;
        private double frictionForce;
        private double normalForce;
        private double carLoad;

        public Wheels(double asphaltAdherence,
                      double dirtAdherence,
                      double sandAdherence,
                      double generalAdherence,
                      double radialRigidity,
                      double radius,
                      double width,
                      double mass,
                      double pressure)
        {
            this.radialRigidity = radialRigidity;
            this.radius = radius;
            this.width = width;
            this.mass = mass;
            this.pressure = pressure;
            carLoad = mass * 9.81;
            this.asphaltAdherence = asphaltAdherence;
            this.dirtAdherence = dirtAdherence;
            this.sandAdherence = sandAdherence;
            this.generalAdherence = generalAdherence;
            UpdateGeneralAdherence();
            CalculateTyreFriction();
        }

        public double Mass
        {
            get => mass;
            set => mass = value;
            }

        public double AsphaltAdherence
        {
            get => asphaltAdherence;
            set
            {
                asphaltAdherence = value; 
                UpdateGeneralAdherence();
            }
        }

        public double DirtAdherence
        {
            get => dirtAdherence;
            set
            {
                dirtAdherence = value; 
                UpdateGeneralAdherence();
            }
        }

        public double SandAdherence
        {
            get => sandAdherence;
            set
            {
                sandAdherence = value;
                UpdateGeneralAdherence();
            }
        }

        public double GeneralAdherence
        {
            get => generalAdherence;
            set => generalAdherence = value;
        }

        public void UpdateGeneralAdherence()
        {
            if (asphaltAdherence != null && dirtAdherence != null && sandAdherence != null)
            {
                this.generalAdherence = (asphaltAdherence + dirtAdherence + sandAdherence) / 3;
            } else
            {
                this.generalAdherence = 1;
            }
        }

        public void CalculateTyreFriction()
        {
            radialTyreDeflexion = (carLoad / 4) / radialRigidity;
            contactArea = width * (1.4 * Math.Sqrt(radialTyreDeflexion * ((2 * radius) - radialTyreDeflexion)));
            normalForce = pressure * contactArea;
            frictionForce = normalForce * generalAdherence; // TODO : general adherance? mu
        }

        void Start()
        {
            
        }
    }
}