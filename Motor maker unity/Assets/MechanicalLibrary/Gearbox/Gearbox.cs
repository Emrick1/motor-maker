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

        public double calculateRatio()
        {
            double ratio = 1;
            if (gears.Count >= 2)
            {
                Gear gear1;
                Gear gear2;
                for (int i = 0; i < gears.Count; i++)
                {
                    gear1 = gears[i - 1];
                    gear2 = gears[i];
                    ratio *= ratioOf(gear1.NbDents, gear2.NbDents);
                }
            }

            return ratio;
        }

        private double ratioOf(int driver, int follower)
        {
            return (double) follower / driver;
        } 
    }
}