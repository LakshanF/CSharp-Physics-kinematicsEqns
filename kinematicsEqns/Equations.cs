using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace kinematicsEqns
{
    public class Equations
    {
        const double g = -9.8;

        double? a;
        double? v_0;
        double? v_f;
        double? s;
        double? time;

        bool _vertical;

        MetricType _unassigned = MetricType.None;

        public double? Result { get; internal set; }

        public Equations(bool vertical)
        {
            _vertical = vertical;
            if (_vertical)
            {
                a = g;
            }
            else
            {
                a = 0;
            }
        }

        internal void CalculateTime()
        {
            GetParms(MetricType.Time);
            switch (_unassigned) 
            {
                case MetricType.Distance:
                    // v_f = v_0 + a*t
                    time = (v_f - v_0) / a;
                    break;
                case MetricType.FinalVelocity:
                    // x = v_0*t + 1/2(a*t^2)
                    // A*x^2 + B*x + C=0
                    double A = a.Value / 2;
                    double B = v_0.Value;
                    double C = -s.Value;

                    double t1 = Math.Abs(Math.Sqrt(Math.Pow(B, 2.0) - 4 * A * C));
                    double t2 = (-B + t1)/(2*A);
                    if (t2 > 0)
                    {
                        time = t2;
                    }
                    else
                    {
                        t2 = (-B - t1) / (2 * A);
                        Debug.Assert(t2 >= 0, "Hmmmm... time problems");
                        time = t2;
                    }
                    break;
            }

            Result = time;
        }


        internal void CalculateDistance()
        {
            GetParms(MetricType.Distance);
            switch (_unassigned)
            {
                case MetricType.Time:
                    // 2*a*s = v_f^2 - v_0^2
                    s = (Math.Pow(v_f.Value, 2.0) - Math.Pow(v_0.Value, 2.0)) / (2*a);
                    break;
                case MetricType.FinalVelocity:
                    // s=v_0*t + 1/2*a*t^2
                    s = v_0 * time + 1 / 2 * a * Math.Pow(time.Value, 2.0);
                    break;
            }

            Result = s;
        }

        internal void CalculateInitialVelocity()
        {
            throw new NotImplementedException();
        }

        internal void CalculateFinalVelocity()
        {
            throw new NotImplementedException();
        }


        private void GetParms(MetricType metric)
        {
            int skippedValues = 0;
            if (metric != MetricType.Time)
            {
                Console.WriteLine("Please enter Time: ");
                time = ReadUserInput();
                if (!time.HasValue)
                {
                    skippedValues++;
                    _unassigned = MetricType.Time;
                }
            }
            if (metric != MetricType.Distance)
            {
                Console.WriteLine("Please enter distance: ");
                s  = ReadUserInput();
                if (!s.HasValue)
                {
                    skippedValues++;
                    _unassigned = MetricType.Distance;
                }
            }
            if (metric != MetricType.InitialVelocity)
            {
                Console.WriteLine("Please enter Initial Velocity: ");
                v_0 = ReadUserInput();
                if (!v_0.HasValue)
                {
                    skippedValues++;
                    _unassigned = MetricType.InitialVelocity;
                }
            }
            if (metric != MetricType.FinalVelocity)
            {
                Console.WriteLine("Please enter Final Velocity: ");
                v_f = ReadUserInput();
                if (!v_f.HasValue)
                {
                    skippedValues++;
                    _unassigned = MetricType.FinalVelocity;
                }
            }
            if (skippedValues > 1)
            {
                throw new ArgumentException("Not sufficient Information to proceed");
            }
        }

        /// <summary>
        /// Reads the user input
        /// </summary>
        /// <param name="skippedValues"></param>
        /// <returns></returns>
        private double? ReadUserInput()
        {
            double? ret = null;
            Console.WriteLine("Enter empty string if needed");
            string value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                ret = Double.Parse(value);
            }
            return ret;
        }

        enum MetricType
        {
            None,
            Time,
            Distance,
            InitialVelocity,
            FinalVelocity,
            Acceleration
        }

    }
}
