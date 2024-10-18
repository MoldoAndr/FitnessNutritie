using System;
using System.Data.SqlClient;

namespace FitnessNutritionDB
{
    internal class ConexiuneDB
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=tcp:SPIRIDUSUL,1433;Database=FitnessNutritieDB;User Id=Fitness;Password=FitnessDB;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexiune reusita la baza de date.");

                    // Citire alimente
                    CitireRetete(connection);

                    // Citire exercitii
                    CitireExercitii(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la conectare: " + ex.Message);
                }
            }

            Console.WriteLine("Apasa orice tasta pentru a inchide aplicatia.");
            Console.ReadKey();
        }

        static void CitireRetete(SqlConnection connection)
        {
            string selectQuery = "SELECT * FROM Retete";

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Retete:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["ID"]}, Calorii: {reader["Calorii"]}, Carbohidrati: {reader["Carbohidrati"]}, Proteine: {reader["Proteine"]}, Grasimi: {reader["Grasimi"]}, Ingrediente: {reader["Ingrediente"]}, Tip Masa: {reader["TipMasa"]}");
                    }
                }
            }
        }

        static void CitireExercitii(SqlConnection connection)
        {
            string selectQuery = "SELECT * FROM Exercitii";

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Exercitii:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["ID"]}, Denumire: {reader["DenumireExercitiu"]}, Repetari: {reader["Repetari"]}, Grupa Musculara: {reader["GrupaMusculara"]}, Seturi: {reader["Seturi"]}, Descriere: {reader["Descriere"]}, Timp Estimat: {reader["TimpEstimareExecutie"]} minute");
                    }
                }
            }
        }
    }
}
