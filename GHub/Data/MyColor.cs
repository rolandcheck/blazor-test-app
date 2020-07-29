using System;
using System.Drawing;

namespace GHub.Data
{
    public class MyColor : IEntity
    {
        public Guid Id { get; set; }
        public MyColor()
        { }

        public MyColor(in Color color)
        {
            Color = color;
        }

        public int Argb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }

        public static implicit operator MyColor(Color color)
        {
            return new MyColor(color);
        }

        public static implicit operator Color(MyColor color)
        {
            return color.Color;
        }

        public Color Color { get; set; }
    }
}