using UnityEngine;

namespace Mechanix
{
    public class Skins : IMechanicalPiece
    {
        private Color color;
        public Skins()
        {
            color = Color.grey;
        }

        public Skins(Color color)
        {
            this.color = color;
        }

        public Color Color
        {
            get => color;
            set => color = value;
        }
    }
}