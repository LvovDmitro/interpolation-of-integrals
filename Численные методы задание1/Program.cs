namespace Численные_методы_задание1
{
    using System;

        class Program
        {
            static double eps = 0.000001;//точность вычисления
            static double BuildR(double x)//строим наш ряд для каждого х
            {
                double Sumend = 0, Sum, a;
                int n = 1;//начнём с нуля, нулём и окончим)
                a = ((-1) * x * x) / 4; //формула такова, при подставлении 1 в а(n) член, получим данное выражение
                do
                {
                    Sumend += a;//будем прибавлять а, тем самым брать следующие значения а1, а2 и тд
                    Sum = Sumend;//записываем нашу будущую итоговую сумму
                    a *= ((-1) * x * x * n) / ((n + 1) * (2 * n + 1) * (2 * n + 2));//формула, которая избавляет нас от факториалов, полученная путемя деления следующего на предыдущий член
                    Sum += a;//и к этой сумме прибавляем наши члены
                    n++;//увеличиваем n, тем самым шагаем дальше
                } while (Math.Abs(a) > eps);//доводим до нужной точности
                return Sum;
            }
            static double Lagrang(double xx, double[] x, int n)//строим приближенный полином
            {
                int i, j;
                double[] f = new double[n + 1];
            double[] l = new double[n + 1];
            for (i = 0; i < n; i++)
                {
                    f[i] = BuildR(x[i]); //задаем массив значениями из данного ряда
                l[i] = 0;
            }
                double sum = 0, multiple; //здесь находится произведение скобок(xx-///)
                for (j = 1; j <= n; j++)
                {
                for (i = n; i >= j; i--)//вычисляем полином, идём в обратную стороны так как вычитаем предыдущий член(n-1)
                {
                    f[i] = (f[i] - f[i - 1]) / (x[i] - x[i - j]);//записываем в каждое f[] одно деление(x.../x...)т е формула произведения
                  //  l[i] = (xx - x[i]) / (x[i] - x[j]);формула внутри произведения
                }
                }
                for (i = n - 1; i >= 0; i--)//поли
                {
                multiple = 1;//когда выполняем произведение, начинаем с 1
                for (j = 0; j < i; j++)
                {
                    multiple *= (xx - x[j]);//
                    //multiple *=l[j] перемножаем скобки
                }

                multiple *= f[j];//начинаем в итоге все перемножать и получать итоговый ответ
                    sum += multiple;//каждый прогон суммируем и записываем
                }
                return sum;
            }
            static void Main(string[] args)
            {
                Console.WriteLine("Узлы:");
                Console.WriteLine("х:      Полином Лагранжа :            Сам ряд:		     Погрешность:\n");
                int n = 5;//если увеличить - ничего не меняется, хотя должно...или не должно..? а, вроде меняется
                double a = 0.4, b = 4, h1 = (b - a) / 10, h2 = (b - a) / n;//входные значения, шаг
                double[] nodes = new double[100];//массив для узлов, просто какое-то большое количество
                for (int i = 0; i < n + 1; i++)
                {
                nodes[i] = a + h2 * i;//записываем наши узлы, которые вычисляется по формуле на листочке
                }
                for (double x = 0.04; x <= b; x += h1) // для вывода
                {
                    Console.Write("{0:F2}", x);
                    Console.Write("     {0:F12}      ", Lagrang(x, nodes, n));
                    Console.Write("     {0:F12}      ", BuildR(x));
                    Console.Write("     {0:F12}0      ", Math.Abs(Lagrang(x, nodes, n) - BuildR(x)));
                    Console.WriteLine("\n");
                }
                Console.WriteLine("х:                           Полином Лагранжа:             Сумма ряда:		      Разность:\n");
                double[] nodesch = new double[200];//массив для узлов чебышева

                for (int i = 0; i < n + 1; i++)
                {
                nodesch[i] = (b + a) / 2 + (b - a) / 2 * Math.Cos((2 * i + 1) * Math.PI / ((2 * n + 2)));//формула узлов чебышева
                }
                double r = 0;
                for (r = 0.04; r <= b; r += h1)//выводим тоже самое, только для узлов чебышева
                {
                    Console.Write("{0:F16}          ", r);
                    Console.Write("{0:F16}          ", Lagrang(r, nodesch, n));
                    Console.Write("{0:F16}          ", BuildR(r));
                    Console.Write("{0:F16}          ", Math.Abs(BuildR(r) - Lagrang(r, nodesch, n)));
                    Console.WriteLine("\n");
                }
                int k = 1;
                int w, z=0;
                int[] index = new int[210];//увеличение прогонки
                double max;
                w = 59;// максимальное количество узлов
                double[] maxpogresn = new double[210], maxpogresncheba = new double[210];//просто большие значения
                for (int i = 0; i < w; i += k)
                {
                    if (i % 10 != 0)//убирает то, что кратно 10
                    {
                        double h22 = (b - a) / (5);
                        for (int j = 0; j < i; j++)
                        {
                        nodes[j] = a + h22 * j;
                        }
                        double h11 = (b - a) / (10);
                        max = 0;
                        for (double x = a; x <= b; x += h11)//вычисляем максимальную погрешность по обычным узлам
                        {
                            if (Math.Abs(Lagrang(x, nodes, i) - BuildR(x)) > max)
                            {
                                max = Math.Abs(Lagrang(x, nodes, i) - BuildR(x));
                            }
                        }
                        maxpogresn[z] = max;
                        for (int j = 0; j < i; j++)
                        {
                        nodesch[j] = (b + a) / 2 + (b - a) / 2 * Math.Cos((2 * j + 1) * Math.PI / ((2 * (i - 1) + 2)));
                        }
                        max = 0;
                        for (int j = 0; j < n + 1; j++)// максимальная погрешность по узлам чебышева
                        {
                            r = (b + a) / 2 +((b - a) / 2) * Math.Cos((2 * j + 1) * Math.PI / ((2 * n + 2)));
                            if (Math.Abs(Lagrang(r, nodesch, i) - BuildR(r)) > max)
                            {
                                max = Math.Abs(Lagrang(r, nodesch, i) - BuildR(r));
                            }
                        }
                        maxpogresncheba[z] = max;
                        index[z] = i;
                        z++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Узлы интерполяции:\n");
                for (int i = 0; i < z; i++)
                {
                    Console.WriteLine(index[i].ToString() + "         " + "{0:F16}", maxpogresn[i]);
                }
                Console.WriteLine();
                Console.WriteLine("Чебышевские узлы интерполяции:\n");
                for (int i = 0; i < z; i++)
                {
                    Console.WriteLine(index[i].ToString() + "         " + "{0:F16}", maxpogresncheba[i]);
                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }
}

