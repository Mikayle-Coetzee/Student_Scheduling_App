#region Documentation Header
// File: CalculationClass.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 1
// Description: This class is for performing calculations related to study time.
#endregion

namespace CustomClassLibrary
{
    /// <summary>
    /// This class is for performing calculations related to study time.
    /// </summary>
    public class CalculationClass
    {
        /// <summary>
        /// Default ConstructoR of the CalculationClass
        /// </summary>
        public CalculationClass() 
        { 
        
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method calculates the self-study hours and minutes per week based on the given parameters.
        /// </summary>
        /// <param name="numOfCredits">The number of credits for the module.</param>
        /// <param name="numOfWeeks">The number of weeks in the semester.</param>
        /// <param name="classHrsPerWeek">The class hours per week for the module.</param>
        /// <param name="classMinPerWeek">The class minutes per week for the module.</param>
        /// <returns>A tuple containing the calculated self-study hours and remaining minutes.</returns>
        public int CalculateSelfStudyHrsPerWeek(int numOfCredits, double numOfWeeks, int classHrsPerWeek)
        {
            double totalTime = classHrsPerWeek ;

            // Using the formula provided in the POE
            double selfStudyHrs = ((numOfCredits * 10) / numOfWeeks) - totalTime;

            // Calculate total self-study minutes
            double selfStudyMinutes = selfStudyHrs * 60;

            // Split self-study minutes into hours and remaining minutes
            int selfStudyHours = (int)(selfStudyMinutes / 60);
            int remainingMinutes = (int)(selfStudyMinutes % 60);

            return selfStudyHours;
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
