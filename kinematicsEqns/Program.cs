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
            bool vertical = true;
            
            Console.WriteLine("0-time, 1-distance, 2-v_0, 3-v_f");
            option = Console.ReadLine();

            Console.WriteLine("Is the movement horizontal? 1 - yes");
            var s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s) && int.Parse(s)==1)
            {
                vertical = false;
            }

            Equations eqn = new Equations(vertical);
            switch (option)
            {
                case "0":
                    eqn.GetTime();
                    break;
                case "1":
                    eqn.GetDistance();
                    break;
                case "2":
                    eqn.GetInitialVelocity();
                    break;
                case "3":
                    eqn.GetFinalVelocity();
                    break;
            }
            Console.WriteLine($"Result: <{eqn.Result}>");
        }
    }
}
