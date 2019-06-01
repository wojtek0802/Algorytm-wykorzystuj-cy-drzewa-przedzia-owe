using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Litery
{
    class Program
    {
        static int SIZE = 20;
        static int[] w = new int[1 << (SIZE + 1)];

        static void insert(int x, int val)
        {
            int v = (1 << SIZE) + x;
            w[v] = val;
            while (v != 1)
            {
                v /= 2;
                w[v] = w[2 * v] + w[2 * v + 1];
            }
        }

        static int query(int a, int b)
        {
            int va = (1 << SIZE) + a;
            int vb = (1 << SIZE) + b;

            int wyn = w[va];
            if (va != vb)
                wyn += w[vb];

            while (va / 2 != vb / 2)
            {
                if (va % 2 == 0) wyn += w[va + 1];
                if (vb % 2 == 1) wyn += w[vb - 1];
                va /= 2; vb /= 2;
            }
            return wyn;
        }

        static int MAXN = 1000005;
        static long wynik;
        static int n, pos, temp;
        static List<int>[] tab = new List<int>[255];
        static char[] s = new char[MAXN];

        static string[] odczytajImiona()
        {
            string[] imiona = new string[2];
            using (StreamReader sr = new StreamReader("lit1.in"))
            {
                sr.ReadLine();
                imiona[0] = sr.ReadLine();
                imiona[1] = sr.ReadLine();
            }
            return imiona;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = new List<int>();
            }
            string[] imiona = odczytajImiona();
            string pierwsze = imiona[0];
            string drugie = imiona[1];
            for (int i = pierwsze.Length-1; i >= 0; --i)
            {
                tab[(int)(pierwsze[i])].Add(i);
            }

            for (int i = 0; i < drugie.Length; ++i)
            {
                pos = tab[drugie[i]].Last();
                tab[drugie[i]].Remove(pos);
                insert(pos, 1);
                pos += query(pos + 1, MAXN);
                wynik += pos - i;
            }

            Console.WriteLine(wynik);
            Console.ReadKey();
        }
    }
}
