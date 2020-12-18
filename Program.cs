using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA_star
{
     public struct Tnode
    {
        public int stepCost;
        public int huristic;
        public int fcost;

        public List<int> stat;
        public Tnode (int a ,List<int> b,int c)
        {
            stepCost = a;
            stat = b;
            huristic = c;
            fcost = stepCost+huristic;
        }
    }


    class Program
    {
        
 
        static bool constraint(int first, int findex, int second, int sindex)
        {
            if (findex == sindex)
            {
                return false;
            }
            if (first == second)
            {
                return true;
            }
            else if (Math.Abs(first - second) == Math.Abs(findex - sindex))
            {
                return true;
            }
            return false;
        }
        static int gards(List<int> a)
        {
            int count = 0;
            for (int i = 0; i < a.Count() ; i++)
            {
                for (int j = 0; j < a.Count(); j++)
                {
                    if (constraint(a[i],i,a[j],j))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        static List<Tnode> expand(Tnode initial, int epsilon)
         {
            List<Tnode> fring = new List<Tnode>();
            int[] temp = new int[initial.stat.Count] ;
            for (int i = 0; i < initial.stat.Count; i++)
            {
                for (int j = 0; j < initial.stat.Count; j++)
                {
                    if (initial.stat[i] == j+1)
                    {
                        continue;
                    }
                    initial.stat.CopyTo(temp, 0);
                    temp[i] = j+1;
                    Tnode t = new Tnode(initial.stepCost+1, temp.ToList(), gards(temp.ToList()));
                    if (epsilon >= t.fcost)
                    {
                        fring.Add(t);
                    }
                }
            }
            return fring;
        }
        static List<Tnode> bsort(List<Tnode> a)
        {
            Tnode temp = new Tnode();
            for (int j = 0; j <= a.Count() - 2; j++)
            {
                for (int i = 0; i <= a.Count - 2; i++)
                {
                    if (a[i].fcost > a[i + 1].fcost)
                    {
                        temp = a[i + 1];
                        a[i + 1] = a[i];
                        a[i] = temp;
                    }
                }
            }
            return a;
        } 

        static List<int> IDA_star(Tnode initial , int epsilon)
        {
            int iter = 1;
            int thershould = epsilon;
            List<Tnode> q;
            int number = 0;
            while (true)
            {
                if (gards(initial.stat) == 0)
                {
                    return initial.stat;
                }
                List<Tnode> fring = new List<Tnode>();
                q = expand(initial, thershould);
                number += q.Count;
                fring.AddRange(q);
                fring = bsort(fring);
                Tnode temp = new Tnode();


                while (fring.Count != 0)
                {
                    temp = fring[0];
                    fring.RemoveAt(0);
                    if (gards(temp.stat) == 0)
                    {

                        Console.WriteLine("nodes :"+number);
                        Console.WriteLine("itertion :" + iter);
                        return temp.stat;
                    }
                    q = expand(temp, thershould);
                    number += q.Count;
                    fring.AddRange(q);
                    fring = bsort(fring);
                }

                thershould = epsilon + thershould;
                iter++;
            }
            return null;
        }




        static void Main(string[] args)
        {
            Console.WriteLine("hello  world this is IDA* algorithm");
            Console.WriteLine("enter the number of desire queens");
            int x =Int16.Parse( Console.ReadLine());
            List<int> problem = new List<int>();
            Random a = new Random();
            for (int i = 0; i < x; i++)
            {
                //a.Next(1,x+1)
                problem.Add(1);
            }
            Tnode initial = new Tnode(0, problem,gards(problem));
            problem = IDA_star(initial,1);
            Console.WriteLine("the solution of this problem is :");
            for (int i = 0; i < problem.Count; i++)
            {
                Console.Write(problem[i] +" ");
            }
            Console.ReadKey();


        }
    }
}
