using System;
/// <summary>
/// Custom exception for robot safety
/// </summary>
public class RobotSafetyException : Exception
{
    public RobotSafetyException(string message) : base(message)
    {
    }
}

/// <summary>
/// Robot hazard auditor class
/// </summary>
public class RobotHazardAuditor
{
    /// <summary>
    /// Calculate hazard risk based on arm precision, worker density, and machinery state
    /// </summary>
    public double CalculateHazardRisk(double armPrecision, int workerDensity, string machineryState)
    {
        // Validate armPrecision
        if (armPrecision < 0.0 || armPrecision > 1.0)
        {
            throw new RobotSafetyException("Error: Arm precision must be 0.0-1.0");
        }

        // Validate workerDensity
        if (workerDensity < 1 || workerDensity > 20)
        {
            throw new RobotSafetyException("Error: Worker density must be 1-20");
        }

        // Determine machine risk factor based on machineryState (case-sensitive)
        double machineRiskFactor;
        if (machineryState == "Worn")
        {
            machineRiskFactor = 1.3;
        }
        else if (machineryState == "Faulty")
        {
            machineRiskFactor = 2.0;
        }
        else if (machineryState == "Critical")
        {
            machineRiskFactor = 3.0;
        }
        else
        {
            throw new RobotSafetyException("Error: Unsupported machinery state");
        }

        // Hazard Risk = ((1.0 - armPrecision) * 15.0) + (workerDensity * machineRiskFactor)
        double hazardRisk = ((1.0 - armPrecision) * 15.0) + (workerDensity * machineRiskFactor);
        return hazardRisk;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        /// <summary>
        /// Main method to calculate robot hazard risk
        /// </summary>
        try
        {
            Console.WriteLine("Enter Arm Precision (0.0 - 1.0):");
            double armPrecision = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter Worker Density (1 - 20):");
            int workerDensity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Machinery State (Worn/Faulty/Critical):");
            string machineryState = Console.ReadLine();

            RobotHazardAuditor auditor = new RobotHazardAuditor();
            double risk = auditor.CalculateHazardRisk(armPrecision, workerDensity, machineryState);

            Console.WriteLine("Robot Hazard Risk Score: " + risk);
        }
        catch (RobotSafetyException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

