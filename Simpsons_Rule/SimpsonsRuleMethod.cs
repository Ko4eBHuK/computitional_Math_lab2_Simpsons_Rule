using System;
using System.Collections.Generic;
using System.Text;

namespace Simpsons_Rule
{
    class SimpsonsRuleMethod
    {
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }
        public double IntegralValueN { get; set; }
        public double IntegralValueTwoN { get; set; }
        public int StepCount { get; set; }
        public double StepValue { get; set; }
        public double CalculatedAccuracy { get; set; }
        private double Accuracy { get; set; }
        private double FunctionIndex { get; set; }


        public SimpsonsRuleMethod(double incomingLowerLimit, double incomingUpperLimit, double accuracy, int functionIndex)
        {
            if(incomingLowerLimit != incomingUpperLimit)
            {
                CheckLimits(incomingLowerLimit, incomingUpperLimit);
                Accuracy = accuracy;
                FunctionIndex = functionIndex;
                CalculateIntegralValue();
            }
            else
            {
                IntegralValueN = 0;
            }
        }

        private void CalculateIntegralValue()
        {
            IntegralValueN = 0;
            IntegralValueTwoN = 0;
            int n = 4;

            do
            {
                StepValue = (UpperLimit - LowerLimit) / n;

                IntegralValueN += GetFunctionValueInPoint(LowerLimit);
                for (int i = 1; i < n; i++)
                {
                    IntegralValueN += 4 * GetFunctionValueInPoint(i * StepValue);
                    i++;
                    IntegralValueN += 2 * GetFunctionValueInPoint(i * StepValue);
                }
                IntegralValueN += GetFunctionValueInPoint(UpperLimit);
                IntegralValueN *= StepValue;
                IntegralValueN /= 3;

                n *= 2;
                StepValue = (UpperLimit - LowerLimit) / n;

                IntegralValueTwoN += GetFunctionValueInPoint(LowerLimit);
                for (int i = 1; i < n; i++)
                {
                    IntegralValueTwoN += 4 * GetFunctionValueInPoint(i * StepValue);
                    i++;
                    IntegralValueTwoN += 2 * GetFunctionValueInPoint(i * StepValue);
                }
                IntegralValueTwoN += GetFunctionValueInPoint(UpperLimit);
                IntegralValueTwoN *= StepValue;
                IntegralValueTwoN /= 3;

                CalculatedAccuracy = Math.Abs(IntegralValueN - IntegralValueTwoN) / 15;
            }
            while (n < 1000000 & CalculatedAccuracy >= Accuracy);

            StepCount = n / 2;
        }

        public double GetFunctionValueInPoint(double x)
        {
            double y = 0;

            switch (FunctionIndex)
            {
                case 0:
                    y = Math.Pow(x, 2) + 14 * x - 4;
                    break;
                case 1:
                    y = Math.Pow(Math.E, 12 * x);
                    break;
                case 2:
                    y = x - 0.158;
                    break;
                case 3:
                    y = 7 * Math.Sin(x - 9);
                    break;
            }

            return y;
        }



        private void CheckLimits(double incomingLowerLimit, double incomingUpperLimit)
        {
            if (incomingLowerLimit < incomingUpperLimit)
            {
                LowerLimit = incomingLowerLimit;
                UpperLimit = incomingUpperLimit;
            }
            else
            {
                UpperLimit = incomingLowerLimit;
                LowerLimit = incomingUpperLimit;
            }
        }
    }
}
