using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitCycle.Models
{
    static public class MaxPowerCalculator
    {
        static Dictionary<int, string> rmformulas = new Dictionary<int, string>()
        {
            {1,"w*(36/(37-((10-x)+r)))" },
            {2,"w*(1+(((10-x)+r)/30))" }
        };
        public static double Calculate(int rmID,double weight, double repeats, double rpe)
        {
            Argument w = new Argument("w", weight);
            Argument r = new Argument("r", repeats);
            Argument x = new Argument("x", rpe);

            Expression expression = new Expression(rmformulas[rmID], w, r, x);
            return expression.calculate();

        }
 
    }
}
