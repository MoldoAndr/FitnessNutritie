using System;
using System.Collections.Generic;
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

                    CrearePlanAlimentar(connection, 1500);
                    CreareAntrenament(connection, 4, 3);
                    AdaugareUtilizator(connection, "testUser", "hashedPassword123", "Masculin", 1.75m, 75m, "Incepator", "Utilizator");
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

        static void CrearePlanAlimentar(SqlConnection connection, int numarCalorii)
        {
            List<string> mealPlan = new List<string>();

            // Interogare pentru Mic Dejun
            string breakfastQuery = "SELECT TOP 1 * FROM Retete WHERE TipMasa = 'Mic Dejun' AND Calorii <= @NumarCalorii ORDER BY NEWID()";
            using (SqlCommand command = new SqlCommand(breakfastQuery, connection))
            {
                command.Parameters.AddWithValue("@NumarCalorii", numarCalorii);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        mealPlan.Add($"{reader["TipMasa"]}: {reader["Ingrediente"]} ({reader["Calorii"]} Cal)");
                    }
                }
            }

            // Interogare pentru Pranz
            string lunchQuery = "SELECT TOP 1 * FROM Retete WHERE TipMasa = 'Pranz' AND Calorii <= @NumarCalorii ORDER BY NEWID()";
            using (SqlCommand command = new SqlCommand(lunchQuery, connection))
            {
                command.Parameters.AddWithValue("@NumarCalorii", numarCalorii);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        mealPlan.Add($"{reader["TipMasa"]}: {reader["Ingrediente"]} ({reader["Calorii"]} Cal)");
                    }
                }
            }

            // Interogare pentru Cina
            string dinnerQuery = "SELECT TOP 1 * FROM Retete WHERE TipMasa = 'Cina' AND Calorii <= @NumarCalorii ORDER BY NEWID()";
            using (SqlCommand command = new SqlCommand(dinnerQuery, connection))
            {
                command.Parameters.AddWithValue("@NumarCalorii", numarCalorii);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        mealPlan.Add($"{reader["TipMasa"]}: {reader["Ingrediente"]} ({reader["Calorii"]} Cal)");
                    }
                }
            }

            Console.WriteLine("Plan alimentar:");
            foreach (var meal in mealPlan)
            {
                Console.WriteLine(meal);
            }
        }

        static void CreareAntrenament(SqlConnection connection, int numarZile, int numarExercitii)
        {
            string selectQuery = "SELECT TOP(@NumarExercitii) * FROM Exercitii ORDER BY NEWID()"; // Random selection of exercises
            List<string> workoutPlan = new List<string>();

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@NumarExercitii", numarExercitii);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workoutPlan.Add($"{reader["DenumireExercitiu"]} - {reader["Repetari"]} repetari, {reader["Seturi"]} seturi");
                    }
                }
            }

            Console.WriteLine($"Plan de antrenament pentru {numarZile} zile:");
            foreach (var exercise in workoutPlan)
            {
                Console.WriteLine(exercise);
            }
        }

        static void AdaugareUtilizator(SqlConnection connection, string name, string hashedPassword, string sex, decimal height, decimal weight, string physicalCondition, string userType)
        {
            // Check if user already exists
            string checkQuery = "SELECT COUNT(*) FROM Utilizatori WHERE Name = @Name";

            using (SqlCommand command = new SqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                int userCount = (int)command.ExecuteScalar();

                if (userCount > 0)
                {
                    Console.WriteLine("Utilizatorul exista deja.");
                    return;
                }
            }

            // If not exists, add the user
            string insertQuery = "INSERT INTO Utilizatori (Name, HashedPassword, Sex, Height, Weight, PhysicalCondition, UserType) VALUES (@Name, @HashedPassword, @Sex, @Height, @Weight, @PhysicalCondition, @UserType)";

            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                command.Parameters.AddWithValue("@Sex", sex);
                command.Parameters.AddWithValue("@Height", height);
                command.Parameters.AddWithValue("@Weight", weight);
                command.Parameters.AddWithValue("@PhysicalCondition", physicalCondition);
                command.Parameters.AddWithValue("@UserType", userType);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Utilizator adaugat cu succes." : "Eroare la adaugarea utilizatorului.");
            }
        }
    }
}
