using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_3
{
    class LongNum
    {
        public static string MulFrac(string s1, string s2)
        {
            LongNum arrnom1 = new LongNum();
            LongNum arrnom2 = new LongNum();
            LongNum arrdenom1 = ReadArr("1");
            LongNum arrdenom2 = ReadArr("1");
            int d = 0;
            string subs1;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == '/') { d = i; }
            }
            if (d > 0)
            {
                subs1 = s1.Substring(0, d);
                string subs2 = s1.Substring(d + 1, s1.Length - 1);
                arrnom1 = LongNum.ReadArr(subs1);
                arrdenom1 = LongNum.ReadArr(subs2);
            }
            else
            {
                arrnom1 = LongNum.ReadArr(s1);
            }

            d = 0;

            for (int i = 0; i < s2.Length; i++)
            {
                if (s2[i] == '/') { d = i; }
            }
            if (d > 0)
            {
                subs1 = s2.Substring(0, d);
                string subs2 = s2.Substring(d + 1, s2.Length - 1);
                arrnom2 = LongNum.ReadArr(subs1);
                arrdenom2 = LongNum.ReadArr(subs2);
            }
            else
            {
                arrnom2 = LongNum.ReadArr(s2);
            }
            LongNum nom = arrnom1 * arrnom2;
            LongNum denom = arrdenom1 * arrdenom2;
            string sres1="";

            if (denom.Array.GetLength(0) == 1 && denom.Array[0] == 1)
            {
                for (int i = 0; i < nom.Array.GetLength(0); i++)
                {
                    sres1 += Convert.ToString(nom.Array[i]);
                }
            }
            else
            {
                LongNum counter = ReadArr("2");
                if (Compare(nom, denom))
                {
                    while (Compare(nom / ReadArr("2"), counter))
                    {
                        LongNum arres1 = nom / counter;
                        LongNum arres2 = denom / counter;
                        if (arres1.exp == 0 && arres2.exp == 0)
                        {
                            nom = nom / counter;
                            denom = denom / counter;
                            counter = ReadArr("2");
                        }
                        else counter = counter + ReadArr("1");
                    }
                }
                else
                {
                    while (Compare(denom / ReadArr("2"), counter))
                    {
                        LongNum arres1 = nom / counter;
                        LongNum arres2 = denom / counter;
                        if (arres1.exp == 0 && arres2.exp == 0)
                        {
                            nom = nom / counter;
                            denom = denom / counter;
                            counter = ReadArr("2");
                        }
                        else counter = counter + ReadArr("1");
                    }
                }
                
                for (int i = 0; i < nom.Array.GetLength(0); i++)
                {
                    sres1 += Convert.ToString(nom.Array[i]);
                }
                sres1 += "/";
                for (int i = 0; i < denom.Array.GetLength(0); i++)
                {
                    sres1 += Convert.ToString(denom.Array[i]);
                }
            }
            return sres1;
        }

        public static string DivFrac(string s1, string s2)
        {
            int d = 0;
            string subs1;
            for (int i = 0; i < s2.Length; i++)
            {
                if (s2[i] == '/') { d = i; }
            }
            subs1 = s2.Substring(0, d);
            string subs2 = s2.Substring(d + 1, s2.Length - 1);
            if (d + 1 >= s2.Length - 1) subs2 = "1";
            s2 = subs2 + "/" + subs1;
            return MulFrac(s1, s2);
        }

        public static LongNum operator +(LongNum arr1, LongNum arr2)
        {
            return Sum(arr1, arr2);
        }

        public static LongNum operator -(LongNum arr1, LongNum arr2)
        {
            return Subtract(arr1, arr2);
        }

        public static LongNum operator *(LongNum arr1, LongNum arr2)
        {
            return Multiply(arr1, arr2);
        }

        public static LongNum operator /(LongNum arr1, LongNum arr2)
        {
            return Divide(arr1, arr2, 10);
        }

        public static LongNum KaratsubaAlgorythm(LongNum arr1, LongNum arr2)
        {
            LongNum subarr1 = new LongNum();
            LongNum subarr2 = new LongNum();
            LongNum bigarr1 = new LongNum();
            LongNum bigarr2 = new LongNum();
            bigarr1.Array = new int[arr1.Array.GetLength(0) / 2];
            bigarr2.Array = new int[arr2.Array.GetLength(0) / 2];
            subarr1.Array = new int[arr1.Array.GetLength(0) - bigarr1.Array.GetLength(0)];
            subarr2.Array = new int[arr2.Array.GetLength(0) - bigarr2.Array.GetLength(0)];
            int base1 = arr1.Array.GetLength(0) - bigarr1.Array.GetLength(0);
            int base2 = arr2.Array.GetLength(0) - bigarr2.Array.GetLength(0);
            for (int i = 0; i < bigarr1.Array.GetLength(0); i++) bigarr1.Array[i] = arr1.Array[i];
            for (int i = 0; i < bigarr2.Array.GetLength(0); i++) bigarr2.Array[i] = arr2.Array[i];
            for (int i = 0; i < subarr1.Array.GetLength(0); i++) subarr1.Array[i] = arr1.Array[i + bigarr1.Array.GetLength(0)];
            for (int i = 0; i < subarr2.Array.GetLength(0); i++) subarr2.Array[i] = arr2.Array[i + bigarr2.Array.GetLength(0)];
            return (subarr1 * subarr2) + (subarr1 * bigarr2 * Degree(ReadArr("10"), base2)) + (subarr2 * bigarr1 * Degree(ReadArr("10"), base1)) + (bigarr1 * bigarr2 * Degree(ReadArr("10"), base1 + base2));
        }

        public static LongNum Degree(LongNum arr1, int deg)
        {
            arr1 = ToBinary(arr1);
            LongNum arr2 = new LongNum();
            arr2.Array = new int[1];
            arr2.Array[0] = 1;
            for (int i = 0; i < deg; i++) arr2 = MultiplyBinary(arr2, arr1);
            if (deg % 2 == 0) arr2.sign = true;
            else arr2.sign = arr1.sign;
            arr2 = ToBigInt(arr2);
            return arr2;
        }

        public static LongNum BinarySum(LongNum arr1, LongNum arr2)
        {
            LongNum result = new LongNum();
            LongNum arr11 = new LongNum();
            LongNum arr22 = new LongNum();
            if (Compare(arr1, arr2))
            {
                arr11.Array = new int[arr1.Array.GetLength(0)];
                arr22.Array = new int[arr2.Array.GetLength(0)];
                arr11.sign = arr1.sign;
                arr22.sign = arr2.sign;
                for (int i = 0; i < arr11.Array.GetLength(0); i++)
                {
                    arr11.Array[i] = arr1.Array[i];
                }
                for (int i = 0; i < arr22.Array.GetLength(0); i++)
                {
                    arr22.Array[i] = arr2.Array[i];
                }
            }
            else
            {
                arr11.Array = new int[arr2.Array.GetLength(0)];
                arr22.Array = new int[arr1.Array.GetLength(0)];
                arr22.sign = arr1.sign;
                arr11.sign = arr2.sign;
                for (int i = 0; i < arr22.Array.GetLength(0); i++)
                {
                    arr22.Array[i] = arr1.Array[i];
                }
                for (int i = 0; i < arr11.Array.GetLength(0); i++)
                {
                    arr11.Array[i] = arr2.Array[i];
                }
            }
            result.Array = new int[arr11.Array.GetLength(0)+1];
            int n = arr11.Array.GetLength(0);
            int m = arr22.Array.GetLength(0);
            bool log = false;
            for (int i = arr11.Array.GetLength(0) - 1; i >= 0; i--)
            {
                if (n - m - 1 < i)
                {
                    if (log == false)
                    {
                        if (arr11.Array[i] + arr22.Array[i + m - n] > 1)
                        {
                            result.Array[i + 1] = 0;
                            log = true;
                        }
                        else result.Array[i + 1] = arr11.Array[i] + arr22.Array[i + m - n];
                    }
                    else
                    {
                        log = false;

                        if (arr11.Array[i] + arr22.Array[i + m - n] > 0)
                        {
                            result.Array[i + 1] = arr11.Array[i] + arr22.Array[i + m - n] - 1;
                            log = true;
                        }
                        else result.Array[i + 1] = 1;
                    }
                }
                else
                {
                    if (log == false)
                    {
                        result.Array[i + 1] = arr11.Array[i];
                    }
                    else
                    {
                        log = false;
                        if (arr11.Array[i] < 1)
                        {
                            result.Array[i + 1] = arr11.Array[i] + 1;
                        }
                        else
                        {
                            result.Array[i + 1] = 0;
                            log = true;
                        }
                    }
                }
            }
            if (log == true)
            {
                result.Array[0] = 1;
            }
            result.exp = Math.Max(arr1.exp, arr2.exp);
            return result;
        }

        public static LongNum ReadArr(string s)
        {
            LongNum arr1 = new LongNum();
            int n = s.Length;
            int d = n;
            for (int l = 0; l < n; l++)
            {
                if ((s[l] == '.') || (s[l] == ','))
                {
                    d = l;
                    break;
                }
            }
            if ((s[0]) == '-')
            {
                arr1.sign = false;
            }
            else
            {
                arr1.sign = true;
            }

            if (arr1.sign)
            {
                if (d != n)
                {
                    arr1.exp = n - 1 - d;
                    arr1.Array = new int[n - 1];
                }
                else
                {
                    arr1.Array = new int[n];
                }
                for (int i = 0; i < d; i++) arr1.Array[i] = int.Parse(Convert.ToString(s[i]));
                for (int i = d + 1; i < n; i++) arr1.Array[i - 1] = int.Parse(Convert.ToString(s[i]));
            }
            else
            {
                if (d != n)
                {
                    arr1.exp = n - 1 - d;
                    arr1.Array = new int[n - 2];
                }
                else
                {
                    arr1.Array = new int[n - 1];
                }
                for (int i = 0; i < d - 1; i++) arr1.Array[i] = int.Parse(Convert.ToString(s[i + 1]));
                for (int i = d; i < n - 1; i++) arr1.Array[i - 1] = int.Parse(Convert.ToString(s[i + 1]));
            }
            return arr1;
        }

        public static void Write(LongNum arres)
        {
            Console.WriteLine();
            int o = 0;
            while ((arres.Array[arres.Array.GetLength(0) - o - 1] == 0) && (o < arres.exp))
            {
                o++;
            }
            int n = arres.Array.GetLength(0);
            LongNum arres1 = new LongNum();
            arres1.sign = arres.sign;
            arres1.exp = arres.exp - o;
            arres1.Array = new int[n - o];
            for (int i = 0; i < n - o; i++)
            {
                arres1.Array[i] = arres.Array[i];
            }
            if (arres1.sign == false) Console.Write("-");
            for (int i = 0; i < arres.Array.GetLength(0) - arres.exp; i++)
            {
                Console.Write(arres.Array[i]);
            }
            if (arres.exp > 0)
            {
                Console.Write(',');
                for (int i = 0; i < arres.exp; i++)
                    Console.Write(arres.Array[arres.Array.GetLength(0) - arres.exp + i]);
            }
            Console.WriteLine();
        }

        public static bool Compare(LongNum arr1, LongNum arr2)
        {
            bool bigger1;
            int[] arrtemp1;
            int[] arrtemp2;
            if (arr1.exp > arr2.exp) bigger1 = true;
            else bigger1 = false;
            if (bigger1)
            {
                arrtemp2 = new int[arr2.Array.GetLength(0) + arr1.exp - arr2.exp];
                for (int i = 0; i < arr2.Array.GetLength(0); i++)
                {
                    arrtemp2[i] = arr2.Array[i];
                }
                arrtemp1 = new int[arr1.Array.GetLength(0)];
                for (int i = 0; i < arr1.Array.GetLength(0); i++)
                {
                    arrtemp1[i] = arr1.Array[i];
                }
            }
            else
            {
                arrtemp1 = new int[arr1.Array.GetLength(0) + arr2.exp - arr1.exp];
                for (int i = 0; i < arr1.Array.GetLength(0); i++)
                {
                    arrtemp1[i] = arr1.Array[i];
                }
                arrtemp2 = new int[arr2.Array.GetLength(0)];
                for (int i = 0; i < arr2.Array.GetLength(0); i++)
                {
                    arrtemp2[i] = arr2.Array[i];
                }
            }
            if (arrtemp1.GetLength(0) > arrtemp2.GetLength(0))
            {
                return true;
            }
            if (arrtemp1.GetLength(0) < arrtemp2.GetLength(0))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < arrtemp1.GetLength(0); i++)
                {
                    if (arrtemp1[i] < arrtemp2[i])
                    {
                        return false;
                    }
                    else
                    {
                        if (arrtemp1[i] > arrtemp2[i])
                        {
                            return true;
                        }
                    }
                }
                return true;
            }
        }

        public static LongNum Sum(LongNum inarr1, LongNum inarr2)
        {
            string s1 = "", s2 = "";
            for (int j = 0; j < inarr1.Array.GetLength(0); j++)
            {
                s1 += Convert.ToString(inarr1.Array[j]);
            }

            for (int j = 0; j < inarr2.Array.GetLength(0); j++)
            {
                s2 += Convert.ToString(inarr2.Array[j]);
            }

            if (inarr1.exp > inarr2.exp)
            {
                for (int i = 0; i < inarr1.exp - inarr2.exp; i++)
                {
                    s2 += "0";
                }
            }
            else
            {
                for (int i = 0; i < inarr2.exp - inarr1.exp; i++)
                {
                    s1 += "0";
                }
            }


            LongNum arr1 = new LongNum();
            LongNum arr2 = new LongNum();

            if (Compare(inarr1, inarr2))
            {
                arr1.Array = new int[s1.Length];
                arr2.Array = new int[s2.Length];
                arr1.sign = inarr1.sign;
                arr2.exp = inarr2.exp;
                arr1.exp = inarr1.exp;
                arr2.sign = inarr2.sign;
                for (int i = 0; i < s1.Length; i++)
                {
                    arr1.Array[i] = int.Parse(Convert.ToString(s1[i]));
                }
                for (int i = 0; i < s2.Length; i++)
                {
                    arr2.Array[i] = int.Parse(Convert.ToString(s2[i]));
                }
            }
            else
            {
                arr1.Array = new int[s2.Length];
                arr2.Array = new int[s1.Length];
                arr1.sign = inarr2.sign;
                arr2.sign = inarr1.sign;
                arr1.exp = inarr2.exp;
                arr2.exp = inarr1.exp;
                for (int i = 0; i < s2.Length; i++)
                {
                    arr1.Array[i] = int.Parse(Convert.ToString(s2[i]));
                }
                for (int i = 0; i < s1.Length; i++)
                {
                    arr2.Array[i] = int.Parse(Convert.ToString(s1[i]));
                }
            }
            int n = Math.Max(arr1.Array.GetLength(0), arr2.Array.GetLength(0));
            int m = Math.Min(arr1.Array.GetLength(0), arr2.Array.GetLength(0));
            bool log = false;
            LongNum arres = new LongNum();
            arres.Array = new int[n + 1];
            if ((inarr1.exp > 0) || (inarr2.exp > 0))
            {
                arres.exp = Math.Max(inarr1.exp, inarr2.exp);
            }
            arres.sign = true;
            if (((arr1.sign == false) && (arr2.sign == false)) || ((arr1.sign == true) && (arr2.sign == true)))
            {
                for (int i = arr1.Array.GetLength(0) - 1; i >= 0; i--)
                {
                    if (n - m - 1 < i)
                    {
                        if (log == false)
                        {
                            if (arr1.Array[i] + arr2.Array[i + m - n] > 9)
                            {
                                arres.Array[i + 1] = arr1.Array[i] + arr2.Array[i + m - n] - 10;
                                log = true;
                            }
                            else arres.Array[i + 1] = arr1.Array[i] + arr2.Array[i + m - n];
                        }
                        else
                        {
                            log = false;

                            if (arr1.Array[i] + arr2.Array[i + m - n] > 8)
                            {
                                arres.Array[i + 1] = arr1.Array[i] + arr2.Array[i + m - n] - 9;
                                log = true;
                            }
                            else arres.Array[i + 1] = arr1.Array[i] + arr2.Array[i + m - n] + 1;
                        }
                    }
                    else
                    {
                        if (log == false)
                        {
                            arres.Array[i + 1] = arr1.Array[i];
                        }
                        else
                        {
                            log = false;
                            if (arr1.Array[i] < 9)
                            {
                                arres.Array[i + 1] = arr1.Array[i] + 1;
                            }
                            else
                            {
                                arres.Array[i + 1] = 0;
                                log = true;
                            }
                        }
                    }
                }
                if (log == true)
                {
                    arres.Array[0] = 1;
                }
                if ((arr1.sign == false) && (arr2.sign == false))
                {
                    arres.sign = false;
                }
            }
            else
            {
                if (arr1.sign == false) arres.sign = false;
                for (int j = n - 1; j >= 0; j--)
                {
                    if (n - m - 1 < j)
                    {
                        if (arr1.Array[j] >= arr2.Array[j - n + m])
                        {
                            arres.Array[j + 1] = arr1.Array[j] - arr2.Array[j - n + m];
                        }
                        else
                        {
                            int k = 1;
                            while (arr1.Array[j - k] == 0)
                            {
                                k++;
                            }
                            arr1.Array[j - k] -= 1;
                            for (int l = j - k + 1; l < j; l++)
                            {
                                arr1.Array[l] += 9;
                            }
                            arres.Array[j + 1] = arr1.Array[j] - arr2.Array[j + m - n] + 10;
                        }
                    }
                    else
                    {
                        arres.Array[j + 1] = arr1.Array[j];
                    }
                }
            }
            int o = 0;
            while ((arres.Array[0 + o] == 0) && (o < arres.Array.GetLength(0) - arres.exp - 1))
            {
                o++;
            }
            LongNum arres1 = new LongNum();
            arres1.sign = arres.sign;
            arres1.exp = arres.exp;
            if (n - o + 1 > arres.exp)
            {
                arres1.Array = new int[n - o + 1];
                for (int i = 0; i < arres1.Array.GetLength(0); i++)
                {
                    arres1.Array[i] = arres.Array[i + o];
                }
            }
            else
            {
                arres1.Array = new int[n + 2 - o];
                for (int i = 1; i < arres1.Array.GetLength(0); i++)
                {
                    arres1.Array[i] = arres.Array[i + o - 1];
                }
            }

            return arres1;

            //  return arres;
        }

        public static LongNum Subtract(LongNum arr1, LongNum arr2)
        {
            LongNum arrdiv = new LongNum();
            arrdiv.Array = new int[arr2.Array.GetLength(0)];
            arrdiv.exp = arr2.exp;
            for (int i = 0; i < arr2.Array.GetLength(0); i++)
            {
                arrdiv.Array[i] = arr2.Array[i];
            }
            if (arr2.sign)
            {
                arrdiv.sign = false;
            }
            else
            {
                arrdiv.sign = true;
            }
            return Sum(arr1, arrdiv);
        }

        public static LongNum Multiply(LongNum arr1, LongNum arr2)
        {
            LongNum result1 = new LongNum();
            LongNum arres2 = new LongNum();
            LongNum arresfin = new LongNum();
            result1.exp = arr1.exp + arr2.exp;
            result1.Array = new int[arr1.Array.GetLength(0) + arr2.Array.GetLength(0)];
            for (int i = arr2.Array.GetLength(0) - 1; i >= 0; i--)
            {
                arres2.exp = arr1.exp;
                arres2.Array = new int[arr1.Array.GetLength(0)];
                for (int j = 0; j < arr2.Array[i]; j++)
                {
                    arres2 = Sum(arres2, arr1);
                }

                arresfin.Array = new int[arres2.Array.GetLength(0) + (arr2.Array.GetLength(0) - i - 1)];
                for (int j = 0; j < arres2.Array.GetLength(0); j++)
                {
                    arresfin.Array[j] = arres2.Array[j];
                }
                arresfin.exp = arr2.exp + arr1.exp;
                result1 = Sum(result1, arresfin);
            }
            if (arr1.sign != arr2.sign) result1.sign = false;
            else result1.sign = true;
            return result1;
        }

        public static LongNum MultiplyBinary(LongNum arr1, LongNum arr2)
        {
            LongNum result1 = new LongNum();
            LongNum arres2 = new LongNum();
            LongNum arresfin = new LongNum();
            result1.exp = arr1.exp + arr2.exp;
            result1.Array = new int[arr1.Array.GetLength(0) + arr2.Array.GetLength(0)];
            for (int i = arr2.Array.GetLength(0) - 1; i >= 0; i--)
            {
                arres2.exp = arr1.exp;
                arres2.Array = new int[arr1.Array.GetLength(0)];
                for (int j = 0; j < arr2.Array[i]; j++)
                {
                    arres2 = BinarySum(arres2, arr1);
                }

                arresfin.Array = new int[arres2.Array.GetLength(0) + (arr2.Array.GetLength(0) - i - 1)];
                for (int j = 0; j < arres2.Array.GetLength(0); j++)
                {
                    arresfin.Array[j] = arres2.Array[j];
                }
                arresfin.exp = arr2.exp + arr1.exp;
                result1 = BinarySum(result1, arresfin);
            }
            if (arr1.sign != arr2.sign) result1.sign = false;
            else result1.sign = true;
            return result1;
        }

        public static LongNum Divide(LongNum arr1, LongNum arr2, int nu)
        {
            LongNum result = new LongNum();
            LongNum arrtemp1 = new LongNum();
            LongNum arrtemp2 = new LongNum();
            LongNum arrtemp = new LongNum();
            LongNum arrtemptemp = new LongNum();
            string s = "";
            bool log = false;
            if (arr1.exp >arr2.exp)
            {
                arrtemp.Array = new int[arr1.Array.GetLength(0)];
                arrtemp2.Array = new int[arr2.Array.GetLength(0) + arr1.exp - arr2.exp];
                for (int i = 0; i < arr1.Array.GetLength(0); i++) arrtemp.Array[i] = arr1.Array[i];
                for (int i = 0; i < arr2.Array.GetLength(0); i++) arrtemp2.Array[i] = arr2.Array[i];
            }
            else
            {
                arrtemp.Array = new int[arr1.Array.GetLength(0) + arr2.exp - arr1.exp];
                arrtemp2.Array = new int[arr2.Array.GetLength(0)];
                for (int i = 0; i < arr1.Array.GetLength(0); i++) arrtemp.Array[i] = arr1.Array[i];
                for (int i = 0; i < arr2.Array.GetLength(0); i++) arrtemp2.Array[i] = arr2.Array[i];
            }
            while (result.exp < nu)
            {
                if (Compare(arrtemp, arrtemp2) == false)
                {
                    result.exp++;
                    s = "0" + s;
                    arrtemptemp.Array = new int[arrtemp.Array.GetLength(0) + 1];
                    for (int i = 0; i < arrtemp.Array.GetLength(0); i++) arrtemptemp.Array[i] = arrtemp.Array[i];
                    arrtemp.Array = new int[arrtemptemp.Array.GetLength(0)];
                    for (int i = 0; i < arrtemptemp.Array.GetLength(0); i++) arrtemp.Array[i] = arrtemptemp.Array[i];
                }
                else
                {  
                    arrtemp1.Array = new int[arrtemp.Array.GetLength(0)];
                    for (int i = 0; i < arrtemp2.Array.GetLength(0); i++)
                    {
                        arrtemp1.Array[i] = arrtemp2.Array[i];
                    }
                    if (Compare(arrtemp, arrtemp1) == false)
                    {
                        arrtemp1.Array = new int[arrtemp.Array.GetLength(0) - 1];
                        for (int i = 0; i < arrtemp2.Array.GetLength(0); i++)
                        {
                            arrtemp1.Array[i] = arrtemp2.Array[i];
                        }
                    }
                    int n = 0;
                    do
                    {
                        arrtemp = Subtract(arrtemp, arrtemp1);
                        n++;
                        if ((arrtemp.Array.GetLength(0) == 1) && (arrtemp.Array[0] == 0))
                        {
                            log = true;
                            break;
                        }
                    }
                    while (Compare(arrtemp, arrtemp1));
                    s += Convert.ToString(n);
                }
                if (log) break;
            }
            if (s[s.Length - 1] == '0')
            {
                s += "1";
                result.exp++;
            }
            string s1 = s.Substring(s.Length - result.exp, result.exp);
            if (s1 == "") s1 += "0";
            string s2 = s.Substring(0,s.Length - result.exp);
            s = s2 + "," + s1;
            result = ReadArr(s);
            if (arr1.sign != arr2.sign)
            {
                result.sign = false;
            }
            else
            {
                result.sign = true;
            }
            return result;
        }

        public static LongNum ToBinary(LongNum arr1)
        {
            string s = "";
            LongNum arr2 = ReadArr("1");
            while (Compare(arr1, arr2)) arr2 = Multiply(arr2, ReadArr("2"));
            arr2 = Divide(arr2, ReadArr("2"),0);
            while (Compare(ReadArr("0"), arr1) == false)
            {
                if (Compare(arr1, arr2))
                {
                    s += 1;
                    arr1 = Subtract(arr1, arr2);
                    arr2 = Divide(arr2, ReadArr("2"),0);
                }
                else
                {
                    s += "0";
                    arr2 = Divide(arr2, ReadArr("2"),0);
                }
            }
            while (Compare(arr2,ReadArr("1")))
            {
                arr2 = Divide(arr2, ReadArr("2"),0);
                s += 0;
            }
            LongNum result = ReadArr(s);
            result.sign = arr1.sign;
            result.exp = arr1.exp;
            return result;
        }

        public static LongNum ToBigInt(LongNum arr1)
        {
            LongNum result = new LongNum();
            result.Array = new int[1];
            result.sign = arr1.sign;
            result.exp = arr1.exp;
            LongNum counter = ReadArr("1");
            for (int i = arr1.Array.GetLength(0) - 1; i >= 0; i--)
            {
                if (arr1.Array[i]==1)
                {
                    result = Sum(result, counter);
                }
                counter = Multiply(counter, ReadArr("2"));
            }
            return result;
        }


        public bool sign=true;
        public int[] Array;
        public int exp = 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool prog = true;
            bool l1;
            while (prog)
            {
                Console.WriteLine("Выберите программу:\n\n");
                Console.WriteLine("1.Сумма квадратов факториалов по заданному k\n\n2.Возведение вещественного числа в целую степень\n\n3.Возведение вещественного числа в вещественную степень (нет)\n\n4.Умножение / деление рациональных чисел с приведением членов (нет)\n\n5.Перевод целого числа в систему счисления Фибоначчи (нет)\n\n6.Вычисление Пи с точностью до указанного знака (нет)\n\n7.Вычисление числа E с точностью до указанного знака (нет)\n\n8.Умножение целых чисел с помощью алгоритма Карацубы и столбиком (алгоритм Карацубы выдает неправильный результат)\n");
                string s = Console.ReadLine();
                switch (s)
                {

                    #region Сумма квадратов факториалов по заданному k
                    case "1":
                        Console.WriteLine("Введите k");
                        string k = Console.ReadLine();
                        LongNum arr1 = LongNum.ReadArr(k);
                        LongNum counter1 = LongNum.ReadArr("1");
                        LongNum number1 = LongNum.ReadArr("1");
                        LongNum result1 = LongNum.ReadArr("0");
                        while (LongNum.Compare(arr1, counter1))
                        {
                            result1 = LongNum.Sum(result1, LongNum.Multiply(number1,number1));
                            number1 = LongNum.Multiply(number1, LongNum.Sum(counter1, LongNum.ReadArr("1")));
                            counter1 = LongNum.Sum(counter1, LongNum.ReadArr("1"));
                        }
                        LongNum.Write(result1);
                        l1 = true;
                        while (l1)
                        {
                            Console.WriteLine("Хотите продолжить? (y/n, Да/Нет)");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                case "Y":
                                case "да":
                                case "Да":
                                    l1 = false;
                                    break;
                                case "n":
                                case "N":
                                case "нет":
                                case "Нет":
                                    prog = false;
                                    l1 = false;
                                    break;
                                default:
                                    Console.WriteLine("\nПожалуйста, повторите ответ\n");
                                    break;
                            }
                        }
                        break;
                    #endregion

                    #region Возведение вещественного числа в целую степень
                    case "2":
                        Console.WriteLine("\nВведите возводимое число\n");
                        LongNum arr2 = LongNum.ReadArr(Console.ReadLine());
                        Console.WriteLine("\nВведите степень\n");
                        int a = int.Parse(Console.ReadLine());
                        arr2 = LongNum.Degree(arr2, a);
                        LongNum.Write(arr2);
                        l1 = true;
                        while (l1)
                        {
                            Console.WriteLine("Хотите продолжить? (y/n, Да/Нет)");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                case "Y":
                                case "да":
                                case "Да":
                                    l1 = false;
                                    break;
                                case "n":
                                case "N":
                                case "нет":
                                case "Нет":
                                    prog = false;
                                    l1 = false;
                                    break;
                                default:
                                    Console.WriteLine("\nПожалуйста, повторите ответ\n");
                                    break;
                            }
                        }
                        break;
                    #endregion

                    #region Возведение вещественного числа в вещественную степень
                    case "3":
                        break;
                    #endregion

                    #region Умножение / деление рациональных чисел с приведением членов
                    case "4":
                        Console.WriteLine("Введите дробь, операцию и вторую дробь");
                        int d = 0;
                        string subs1 = Console.ReadLine();
                        string oper = Console.ReadLine();
                        string subs2=Console.ReadLine();
                        string result4;
                        if (oper == "/") result4 = LongNum.DivFrac(subs1, subs2);
                        else result4 = LongNum.MulFrac(subs1, subs2);
                        Console.WriteLine(result4);
                        l1 = true;
                        while (l1)
                        {
                            Console.WriteLine("Хотите продолжить? (y/n, Да/Нет)");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                case "Y":
                                case "да":
                                case "Да":
                                    l1 = false;
                                    break;
                                case "n":
                                case "N":
                                case "нет":
                                case "Нет":
                                    prog = false;
                                    l1 = false;
                                    break;
                                default:
                                    Console.WriteLine("\nПожалуйста, повторите ответ\n");
                                    break;
                            }
                        }
                            break;
                    #endregion

                    #region Перевод целого числа в систему счисления Фибоначчи
                    case "5":
                        break;
                    #endregion

                    #region Вычисление Пи с точностью до указанного знака
                    case "6":
                        Console.WriteLine("Введите, до какого знака нужно вычислить Пи");
                        int p = int.Parse(Console.ReadLine());
                        LongNum arrp = new Homework_3.LongNum();
                        arrp.Array = new int[p + 1];
                        arrp.Array[p] = 1;
                        arrp.exp = p;
                        int counter6 = 1;
                        LongNum prearr6 = LongNum.ReadArr("0");
                        LongNum result6 = LongNum.ReadArr("4");
                        while (LongNum.Compare(result6 - prearr6, arrp))
                        {
                            prearr6.Array = new int[result6.Array.GetLength(0)];
                            for (int i = 0; i < result6.Array.GetLength(0); i++)
                            {
                                prearr6.Array[i] = result6.Array[i];
                            }
                            prearr6.sign = result6.sign;
                            prearr6.exp = result6.exp;
                            result6 = result6 + (LongNum.ReadArr("4") * (LongNum.Divide(LongNum.Degree(LongNum.ReadArr("-1"), counter6), (LongNum.ReadArr("2") * LongNum.ReadArr(Convert.ToString(counter6)) + LongNum.ReadArr("1")), p+3)));
                            counter6++;
                        }
                        LongNum.Write(result6);
                        l1 = true;
                        while (l1)
                        {
                            Console.WriteLine("Хотите продолжить? (y/n, Да/Нет)");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                case "Y":
                                case "да":
                                case "Да":
                                    l1 = false;
                                    break;
                                case "n":
                                case "N":
                                case "нет":
                                case "Нет":
                                    prog = false;
                                    l1 = false;
                                    break;
                                default:
                                    Console.WriteLine("\nПожалуйста, повторите ответ\n");
                                    break;
                            }
                        }

                        break;
                    #endregion

                    #region Вычисление числа E с точностью до указанного знака
                    case "7":
                        LongNum.Write(LongNum.ReadArr(Console.ReadLine())/ LongNum.ReadArr(Console.ReadLine()));
                        break;
                    #endregion

                    #region Умножение целых чисел с помощью алгоритма Карацубы и столбиком
                    case "8":
                        Console.WriteLine("Введите число k, факториал которого надо вычислить");
                        LongNum arr8 = LongNum.ReadArr(Console.ReadLine());
                        LongNum counter8 = LongNum.ReadArr("1");
                        LongNum result = LongNum.ReadArr("1");
                        var sw = new Stopwatch();
                        sw.Start();
                        while (LongNum.Compare(arr8, counter8))
                        {
                            result = result * counter8;
                            counter8 = counter8 + LongNum.ReadArr("1");
                        }
                        sw.Stop();
                        Console.Write("\n________________________________\nУмножение столбиком");
                        LongNum.Write(result);
                        Console.WriteLine("Выполнилось за " + sw.ElapsedMilliseconds + " мс\n");
                        result = LongNum.ReadArr("1");
                        counter8 = LongNum.ReadArr("1");
                        sw.Start();
                        while (LongNum.Compare(arr8, counter8))
                        {
                            result = LongNum.KaratsubaAlgorythm(result, counter8);
                            counter8 = counter8 + LongNum.ReadArr("1");
                        }
                        sw.Stop();
                        Console.Write("\n________________________________\nАлгоритм Карацубы");
                        LongNum.Write(result);
                        Console.WriteLine("Выполнилось за " + sw.ElapsedMilliseconds + " мс\n");
                        l1 = true;
                        while (l1)
                        {
                            Console.WriteLine("Хотите продолжить? (y/n, Да/Нет)");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                case "Y":
                                case "да":
                                case "Да":
                                    l1 = false;
                                    break;
                                case "n":
                                case "N":
                                case "нет":
                                case "Нет":
                                    prog = false;
                                    l1 = false;
                                    break;
                                default:
                                    Console.WriteLine("\nПожалуйста, повторите ответ\n");
                                    break;
                            }
                        }
                        break;
                    #endregion

                    #region default
                    default:
                        Console.WriteLine("\nВыбирать можно только числа от 1 до 8. Пожалуйста, выберите снова (нажмите enter для продолжения)\n");
                        Console.ReadLine();
                        break;
                        #endregion
                }
            }
        }
    }
}
