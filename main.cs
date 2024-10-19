// To comment a section of code in Visual Studio, you can use the keyboard shortcut Ctrl+K, Ctrl+C.
// To uncomment a section of code, you can use the keyboard shortcut Ctrl+K, Ctrl+U.

using System;
using System.Net.Sockets;
using System.Net;

namespace FitnessNutritionDB
{
    internal class main
    {

        private const int Port = 7025;

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            Console.WriteLine($"A pornit serverul TCP, se asteapta conexiuni pe portul {Port}\n");
            
            using (SqlConnectionManager SQLManager = new SqlConnectionManager())
            {
                try
                {
                    #region TestareFunctionalitati
                    //SQLManager.CrearePlanAlimentar(1500);
                    //SQLManager.CrearePlanAlimentarSaptamanal(1500);
                    //SQLManager.CreareAntrenamentZilnic("Piept", 40);
                    //SQLManager.CreareAntrenamentSaptamanal(5, 60);

                    //SQLManager.AdaugareReteta("Pranz", "Baton de granola (1 buc)", 150, (decimal)20, (decimal)7.5, (decimal)2.5);
                    //SQLManager.StergereReteta("Pranz", "Baton de granola (1 buc)");
                    //SQLManager.AdaugareExercitiu("Fluturari cu gantere", "piept", "Exercitiu pentru piept", 4, 12, 10);
                    //SQLManager.StergereExercitiu("Fluturari cu gantere", "piept");
                    
                    //SQLManager.AdaugareUtilizator("bianca", "andrei", "Nutritionist");
                    //SQLManager.VerificareValiditateUtilizator("bianca", "andrei");

                    //decimal weight = 70m;
                    //decimal height = 175m;
                    //int age = 25;
                    //string sex = "Masculin";
                    //string activityLevel = "moderately active"; 

                    //decimal dailyCalories = NutritionCalculator.CalculateCalories(weight, height, age, sex, activityLevel);
                    //decimal roundedCalories = Math.Round(dailyCalories / 100) * 100;
                    //Console.WriteLine($"The estimated daily calorie needs: {roundedCalories} calories.");

                    //string goal = "muscle gain";
                    //var workoutRecommendations = NutritionCalculator.GetWorkoutRecommendations(goal);
                    //Console.WriteLine($"Workout recommendations for the goal '{goal}':");
                    //foreach (var recommendation in workoutRecommendations)
                    //{
                    //    Console.WriteLine(recommendation);
                    //}
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la executarea operatiunii: " + ex.Message);
                }
            }

            while ("te iubesc" != "")
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");
                ClientHandler.HandleClient(client);
                Console.WriteLine("Client is going to be disconnected.");
            }

        }
    }
}
