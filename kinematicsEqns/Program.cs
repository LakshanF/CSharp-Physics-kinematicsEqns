using System;

namespace kinematicsEqns
{
    /// <summary>
    /// Solving the kinematic eqns programmatically
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string option;
            bool loop = true;

            while (loop)
            {
                bool vertical = true;

                Console.WriteLine("0-time, 1-distance, 2-v_0, 3-v_f, 4-exit");
                option = Console.ReadLine();

                Console.WriteLine("Is the movement horizontal? 1 - yes");
                var s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s) && int.Parse(s) == 1)
                {
                    vertical = false;
                }

                Equations eqn = new Equations(vertical);
                switch (option)
                {
                    case "0":
                        eqn.CalculateTime();
                        break;
                    case "1":
                        eqn.CalculateDistance();
                        break;
                    case "2":
                        eqn.CalculateInitialVelocity();
                        break;
                    case "3":
                        eqn.CalculateFinalVelocity();
                        break;
                    case "4":
                        loop = false; ;
                        break;
                }
                Console.WriteLine($"Result: <{eqn.Result}>");
            }
        }
    }
}
