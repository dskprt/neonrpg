using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Utilities {

    class Color {

        private static readonly StringBuilder sb = new StringBuilder();

        public static readonly Color BLACK = new Color(0, 0, 0);
        public static readonly Color WHITE = new Color(255, 255, 255);
        public static readonly Color RED = new Color(255, 0, 0);
        public static readonly Color GREEN = new Color(0, 255, 0);
        public static readonly Color BLUE = new Color(0, 0, 255);

        public string Red { get; set; }
        public string Green { get; set; }
        public string Blue { get; set; }

        public Color(string r, string g, string b) {
            Red = r;
            Green = g;
            Blue = b;
        }

        public Color(int r, int g, int b) {
            Red = r.ToString();
            Green = g.ToString();
            Blue = b.ToString();
        }

        public string AsAnsiForeground() {
            sb.Clear();
            sb.Append("\u001b[38;2;");
            sb.Append(Red);
            sb.Append(";");
            sb.Append(Green);
            sb.Append(";");
            sb.Append(Blue);
            sb.Append("m");
            return sb.ToString();
        }

        public string AsAnsiBackground() {
            sb.Clear();
            sb.Append("\u001b[48;2;");
            sb.Append(Red);
            sb.Append(";");
            sb.Append(Green);
            sb.Append(";");
            sb.Append(Blue);
            sb.Append("m");
            return sb.ToString();
        }
    }
}
