using System;

namespace GradeBook
{
    public class Statistics
    {
        public double Avg { get { return Sum / Cnt; } }
        public double Hi;
        public double Lo;
        public char Letter
        {
            get
            {
                switch (Avg)
                {
                    case var d when d >= 90.0:
                        return 'A';
                    case var d when d >= 80.0:
                        return 'B';
                    case var d when d >= 70.0:
                        return 'C';
                    case var d when d >= 60.0:
                        return 'D';
                    default:
                        return 'F';
                }
            }
        }
        public double Sum;
        public int Cnt;
        public Statistics()
        {
            Cnt = 0;
            Sum = 0.0;
            this.Hi = double.MinValue;
            this.Lo = double.MaxValue;

        }

        public void Add(double num)
        {
            Sum += num;
            Cnt++;
            Hi = Math.Max(Hi, num);
            Lo = Math.Min(Lo, num);
        }

    }
}