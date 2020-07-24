using System.Drawing;

namespace GHub.Data
{
    public class MyColor : EntityBase
    {
        public MyColor()
        { }

        private MyColor(in Color color)
        {
            Color = color;
        }

        public int Argb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }

        public KnownColor KnownColor => Color.ToKnownColor();

        public static implicit operator MyColor(Color color)
        {
            return new MyColor(color);
        }



        public Color Color { get; set; }
    }
}