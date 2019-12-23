using System.Linq;
using System.Text;

namespace Voca.BLL.Helpers
{
    public class NounHelper
    {
        public string GetGenitive(string name)
        {
            StringBuilder sb = new StringBuilder(name);

            if (name.EndsWith("a"))
            {
                sb.Length--;
                sb.Append("e");
            }
            else if (name.EndsWith("e"))
            {
                sb.Length--;
                sb.Append("a");
            }
            else if (name.EndsWith("i"))
            {
                sb.Append("ja");
            }
            else if (name.EndsWith("o"))
            {
                sb.Length--;
                sb.Append("a");
            }
            else if (name.EndsWith("ar"))//petar mitar, ali drugacije za igor
            {
                sb.Length -= 2;
                sb.Append("ra");
            }
            else if (name.EndsWith("ađ"))//đurađ - đurđa
            {
                sb.Length -= 2;
                sb.Append("đa");
            }
            else if (name.EndsWith("m") ||
                name.EndsWith("n") ||
                name.EndsWith("p") ||
                name.EndsWith("b") ||
                name.EndsWith("v") ||
                name.EndsWith("t") ||
                name.EndsWith("d") ||
                name.EndsWith("s") ||
                name.EndsWith("l") ||
                name.EndsWith("r") ||
                name.EndsWith("j") ||
                name.EndsWith("k") ||
                name.EndsWith("z") ||
                name.EndsWith("g") ||
                name.EndsWith("š"))
            {
                sb.Append("a");
            }

            return sb.ToString();
        }

        public string GetDativeOrLocative(string name)
        {
            StringBuilder sb = new StringBuilder(name);

            if (name.EndsWith("a"))
            {
                sb.Length--;
                sb.Append("i");
            }
            else if (name.EndsWith("e"))
            {
                sb.Length--;
                sb.Append("u");
            }
            else if (name.EndsWith("i"))
            {
                sb.Append("ju");
            }
            else if (name.EndsWith("o"))
            {
                sb.Length--;

                if (name.ElementAt(name.Length - 2) == 'l' ||
                    name.ElementAt(name.Length - 2) == 'k')
                {
                    sb.Append("u");
                }
                else
                {
                    sb.Append("i");
                }
            }
            else if (name.EndsWith("ar"))//petar mitar, ali drugacije za igor
            {
                sb.Length -= 2;
                sb.Append("ru");
            }
            else if (name.EndsWith("ađ"))//đurađ
            {
                sb.Length -= 2;
                sb.Append("đu");
            }
            else if (name.EndsWith("m") ||
                name.EndsWith("n") ||
                name.EndsWith("p") ||
                name.EndsWith("b") ||
                name.EndsWith("v") ||
                name.EndsWith("t") ||
                name.EndsWith("d") ||
                name.EndsWith("s") ||
                name.EndsWith("l") ||
                name.EndsWith("r") ||
                name.EndsWith("j") ||
                name.EndsWith("k") ||
                name.EndsWith("z") ||
                name.EndsWith("g") ||
                name.EndsWith("š"))
            {
                sb.Append("u");
            }

            return sb.ToString();
        }

        public string GetAccussative(string name)
        {
            StringBuilder sb = new StringBuilder(name);

            if (name.EndsWith("a"))
            {
                sb.Length--;
                sb.Append("u");
            }
            else if (name.EndsWith("e"))
            {
                sb.Length--;
                sb.Append("a");
            }
            else if (name.EndsWith("i"))
            {
                sb.Append("ja");
            }
            else if (name.EndsWith("o"))
            {
                sb.Length--;

                if (name.ElementAt(name.Length - 2) == 'l' ||
                    name.ElementAt(name.Length - 2) == 'k')
                {
                    sb.Append("a");
                }
                else
                {
                    sb.Append("u");
                }
            }
            else if (name.EndsWith("ar"))//petar mitar, ali drugacije za igor
            {
                sb.Length -= 2;
                sb.Append("ra");
            }
            else if (name.EndsWith("ađ"))//đurađ - đurđa
            {
                sb.Length -= 2;
                sb.Append("đa");
            }
            else if (name.EndsWith("m") ||
                name.EndsWith("n") ||
                name.EndsWith("p") ||
                name.EndsWith("b") ||
                name.EndsWith("v") ||
                name.EndsWith("t") ||
                name.EndsWith("d") ||
                name.EndsWith("s") ||
                name.EndsWith("l") ||
                name.EndsWith("r") ||
                name.EndsWith("j") ||
                name.EndsWith("k") ||
                name.EndsWith("z") ||
                name.EndsWith("g") ||
                name.EndsWith("š"))
            {
                sb.Append("a");
            }

            return sb.ToString();
        }

        public string GetVocative(string name)
        {
            StringBuilder sb = new StringBuilder(name);

            ///TODO za A kad je dugouzlazni akcenat nekako

            if (name.EndsWith("ar"))//petar mitar, ali drugacije za igor
            {
                sb.Length -= 2;
                sb.Append("re");
            }
            else if (name.EndsWith("đ"))//đurađ
            {
                sb.Append("e");
            }
            else if (name.EndsWith("m") ||
                name.EndsWith("n") ||
                name.EndsWith("p") ||
                name.EndsWith("b") ||
                name.EndsWith("v") ||
                name.EndsWith("t") ||
                name.EndsWith("d") ||
                name.EndsWith("s") ||
                name.EndsWith("l") ||
                name.EndsWith("r") ||
                name.EndsWith("j") ||
                name.EndsWith("z") ||
                name.EndsWith("š"))
            {
                sb.Append("e");
            }
            else if (name.EndsWith("k"))
            {
                sb.Length--;
                sb.Append("če");
            }
            else if (name.EndsWith("g"))
            {
                sb.Length--;
                sb.Append("že");
            }
            else if (name.EndsWith("h"))
            {
                sb.Length--;
                sb.Append("še");
            }

            return sb.ToString();
        }

        public string GetInstrumental(string name)
        {
            StringBuilder sb = new StringBuilder(name);

            if (VoiceHelper.Vowels.Contains(name.ElementAt(name.Length - 1)))
                sb.Length--;

            sb.Append("om");

            return sb.ToString();
        }
    }
}
