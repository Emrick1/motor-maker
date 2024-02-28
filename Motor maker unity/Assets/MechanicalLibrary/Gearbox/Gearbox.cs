using System;
using System.Collections.Generic;

namespace Mechanix
{
    public class Gearbox : IMechanicalPiece
    {
        private List<Gear> gears;

        public Gearbox(List<Gear> gears)
        {
            this.gears = gears;
        }

        public Gearbox()
        {
            gears = new List<Gear>();
        }

        public void addGear(Gear gear)
        {
            gears.Add(gear);
        }

        public void removeGear(int index)
        {
            gears.RemoveAt(index);
        }

        public void removeGear(Gear gear)
        {
            gears.Remove(gear);
        }

        public void clearGearbox()
        {
            gears = new List<Gear>();
        }

        public double calculateRatio(Gear menant, Gear menee, Boolean recule)
        {
            double ratio;

            ratio = (menant.NbDents/menee.NbDents);

            if(recule) {
                ratio *= (-1) ;}

            return ratio;
        }

            
        

        private double ratioOf(int driver, int follower)
        {
            return (double) follower / driver;
        } 
    }
}