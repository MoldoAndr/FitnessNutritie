using System;
using System.Collections.Generic;

namespace FitnessNutritionDB
{
    internal class NutritionCalculator
    {
        public static decimal           CalculateCalories(decimal weight, decimal height, int age, string sex, string activityLevel)
        {
            decimal bmr;

            if (sex.ToLower() == "masculin" || sex.ToLower() == "male")
            {
                // Mifflin-St Jeor formula for men
                bmr = 10 * weight + 6.25m * height - 5 * age + 5;
            }
            else if (sex.ToLower() == "feminin" || sex.ToLower() == "female")
            {
                // Mifflin-St Jeor formula for women
                bmr = 10 * weight + 6.25m * height - 5 * age - 161;
            }
            else
            {
                throw new ArgumentException("Invalid sex value. Use 'Masculin' or 'Feminin'.");
            }

            decimal activityMultiplier = 1.2m;
            switch (activityLevel.ToLower())
            {
                case "sedentary":
                    activityMultiplier = 1.2m;
                    break;
                case "lightly active":
                    activityMultiplier = 1.375m;
                    break;
                case "moderately active":
                    activityMultiplier = 1.55m;
                    break;
                case "very active":
                    activityMultiplier = 1.725m;
                    break;
                case "super active":
                    activityMultiplier = 1.9m;
                    break;
                default:
                    throw new ArgumentException("Invalid activity level. Use one of: sedentary, lightly active, moderately active, very active, super active.");
            }

            decimal tdee = bmr * activityMultiplier;

            return Math.Round(tdee, 2);
        }

        public static List<string>      GetWorkoutRecommendations(string goal)
        {
            List<string> recommendations = new List<string>();

            switch (goal.ToLower())
            {
                case "muscle gain":
                    recommendations.Add("push");
                    recommendations.Add("pull");
                    recommendations.Add("legs");
                    recommendations.Add("push");
                    recommendations.Add("pull");
                    recommendations.Add("legs");
                    break;
                case "weight loss":
                    recommendations.Add("cardio");
                    recommendations.Add("push");
                    recommendations.Add("pull");
                    recommendations.Add("legs");
                    break;
                case "maintenance":
                    recommendations.Add("push");
                    recommendations.Add("pull");
                    recommendations.Add("legs");
                    break;
                default:
                    throw new ArgumentException("Invalid goal value. Use 'muscle gain', 'weight loss', or 'maintenance'.");
            }

            return recommendations;
        }
    }
}
