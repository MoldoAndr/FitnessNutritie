using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;


namespace FitnessNutritionDB
{
    public class SqlConnectionManager : IDisposable
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public                  SqlConnectionManager()
        {
            connectionString = "Server=tcp:SPIRIDUSUL,1433;Database=FitnessNutritieDB;User Id=Fitness;Password=FitnessDB;TrustServerCertificate=True;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Conexiune reusita la baza de date.");
        }

        public void             Dispose()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public int              CreareAntrenamentZilnic(string grupaMusculara, int timpMaxim)
        {
            string selectQuery = "SELECT * FROM Exercitii WHERE GrupaMusculara = @GrupaMusculara ORDER BY NEWID()"; // Randomize exercises
            List<string> workoutPlan = new List<string>();
            int totalTimpEstimare = 0;

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@GrupaMusculara", grupaMusculara);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int timpEstimareExercitiu = (int)reader["TimpEstimareExecutie"];

                        if (totalTimpEstimare + timpEstimareExercitiu > timpMaxim)
                        {
                            break;
                        }

                        workoutPlan.Add($"{reader["DenumireExercitiu"]} - {reader["Repetari"]} repetari, {reader["Seturi"]} seturi, {timpEstimareExercitiu} minute estimate");
                        totalTimpEstimare += timpEstimareExercitiu;  // Calculează timpul total estimat
                    }
                }
            }

            if (totalTimpEstimare > timpMaxim && workoutPlan.Count > 0)
            {
                string lastExercise = workoutPlan[workoutPlan.Count - 1];
                workoutPlan.RemoveAt(workoutPlan.Count - 1);

                string[] parts = lastExercise.Split(',');
                int timpEstimareUltimExercitiu = int.Parse(parts[parts.Length - 1].Split(' ')[0]); // Obținem timpul din descrierea exercițiului

                totalTimpEstimare -= timpEstimareUltimExercitiu;
            }

            Console.WriteLine($"Antrenament de o zi pentru {grupaMusculara}:\n");
            foreach (var exercise in workoutPlan)
            {
                Console.WriteLine(exercise);
            }

            Console.WriteLine($"Timp total estimat: {totalTimpEstimare} minute.");
            return totalTimpEstimare;
        }

        public List<string>     GenereazaGrupeMusculare(int numarZile)
        {
            List<string> grupeMusculare = new List<string>();

            switch (numarZile)
            {
                case 3:
                    grupeMusculare.Add("Push");
                    grupeMusculare.Add("Pull");
                    grupeMusculare.Add("Legs");
                    break;
                case 4:
                    grupeMusculare.Add("Biceps-Piept");
                    grupeMusculare.Add("Triceps-Spate");
                    grupeMusculare.Add("Picioare");
                    grupeMusculare.Add("Umeri");
                    break;
                case 5:
                    grupeMusculare.Add("Push");
                    grupeMusculare.Add("Pull");
                    grupeMusculare.Add("Legs");
                    grupeMusculare.Add("Push");
                    grupeMusculare.Add("Pull");
                    break;
                case 6:
                    grupeMusculare.Add("Push");
                    grupeMusculare.Add("Pull");
                    grupeMusculare.Add("Legs");
                    grupeMusculare.Add("Push");
                    grupeMusculare.Add("Pull");
                    grupeMusculare.Add("Legs");
                    break;
                default:
                    throw new ArgumentException("Numar invalid de zile. Alege intre 3, 4, 5 sau 6 zile.");
            }

            return grupeMusculare;
        }

        public List<string>     GenereazaZileAntrenament(int numarZile)
        {
            List<string> zileAntrenament = new List<string>();

            switch (numarZile)
            {
       
                case 3:
                    zileAntrenament.Add("Marti");
                    zileAntrenament.Add("Joi");
                    zileAntrenament.Add("Sambata");
                    break;
                case 4:
                    zileAntrenament.Add("Luni");
                    zileAntrenament.Add("Miercuri");
                    zileAntrenament.Add("Vineri");
                    zileAntrenament.Add("Sambata");
                    break;
                case 5:
                    zileAntrenament.Add("Luni");
                    zileAntrenament.Add("Marti");
                    zileAntrenament.Add("Miercuri");
                    zileAntrenament.Add("Joi");
                    zileAntrenament.Add("Vineri");
                    break;
                case 6:
                    zileAntrenament.Add("Luni");
                    zileAntrenament.Add("Marti");
                    zileAntrenament.Add("Miercuri");
                    zileAntrenament.Add("Joi");
                    zileAntrenament.Add("Vineri");
                    zileAntrenament.Add("Sambata");
                    break;
                default:
                    throw new ArgumentException("Numar invalid de zile. Alege intre 3, 4, 5 sau 6 zile.");
            }

            return zileAntrenament;
        }

        public void             CreareAntrenamentSaptamanal(int numarZile, int numarMinuteZilnicMaxim)
        {
            List<string> grupeMusculare = GenereazaGrupeMusculare(numarZile);
            List<string> zileAntrenament = GenereazaZileAntrenament(numarZile);
            int totalTimpEstimare = 0;

            Console.WriteLine($"\nPlan de antrenament pentru {numarZile} zile:");
            for (int zi = 0; zi < numarZile; zi++)
            {
                Console.WriteLine($"\nZiua {zi + 1} ({zileAntrenament[zi]}): {grupeMusculare[zi]}");

                totalTimpEstimare += CreareAntrenamentZilnic(grupeMusculare[zi], numarMinuteZilnicMaxim);

                Console.WriteLine($"\nTimp total estimat pentru {zileAntrenament[zi]}: {totalTimpEstimare} minute.");

                totalTimpEstimare = 0;
            }
        }

        public void             CrearePlanAlimentarSaptamanal(int numarCalorii)
        {
            Console.WriteLine("\nPlan alimentar saptamanal:");
            for (int zi = 1; zi <= 7; zi++)
            {
                Console.WriteLine($"Ziua {zi}:");
                CrearePlanAlimentar(numarCalorii);
                Console.WriteLine();
            }
        }

        public void             CrearePlanAlimentar(int numarCalorii)
        {
            List<string> mealPlan = new List<string>();
            int calorii_totale = 0;
            int eroare_acceptable = 100;  // Eroare maximă de 100 de calorii

            do
            {
                calorii_totale = 0;
                mealPlan.Clear();  // Resetăm planul alimentar pentru o nouă încercare

                // Mic dejun
                string breakfastQuery = "SELECT TOP 1 * FROM Retete WHERE TipMasa = 'Mic Dejun' AND Calorii <= @NumarCalorii ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(breakfastQuery, connection))
                {
                    command.Parameters.AddWithValue("@NumarCalorii", numarCalorii);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mealPlan.Add($"{reader["TipMasa"]}: {reader["Ingrediente"]} ({reader["Calorii"]} Cal)");
                            calorii_totale += (int)reader["Calorii"];
                        }
                    }
                }

                // Pranz
                string lunchQuery = "SELECT TOP 1 * FROM Retete WHERE TipMasa = 'Pranz' AND Calorii <= @NumarCalorii ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(lunchQuery, connection))
                {
                    command.Parameters.AddWithValue("@NumarCalorii", numarCalorii);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mealPlan.Add($"{reader["TipMasa"]}: {reader["Ingrediente"]} ({reader["Calorii"]} Cal)");
                            calorii_totale += (int)reader["Calorii"];
                        }
                    }
                }

                // Cina
                string dinnerQuery = "SELECT TOP 1 * FROM Retete WHERE TipMasa = 'Cina' AND Calorii <= @NumarCalorii ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(dinnerQuery, connection))
                {
                    command.Parameters.AddWithValue("@NumarCalorii", numarCalorii);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mealPlan.Add($"{reader["TipMasa"]}: {reader["Ingrediente"]} ({reader["Calorii"]} Cal)");
                            calorii_totale += (int)reader["Calorii"];
                        }
                    }
                }

                // Verificăm dacă numărul total de calorii este în intervalul acceptabil
            } while (Math.Abs(calorii_totale - numarCalorii) > eroare_acceptable);

            // Afișăm planul alimentar
            Console.WriteLine("\nPlan alimentar zilnic:");
            Console.WriteLine($"Calorii totale: {calorii_totale}");
            foreach (var meal in mealPlan)
            {
                Console.WriteLine(meal);
            }
        }

        public void             AdaugareUtilizator(string name, string password, string userType, string sex = "Unspecified", decimal height = 0, decimal kilograms = 0, string physicalCondition  = "Unspecified")
        {
            string checkQuery = "SELECT COUNT(*) FROM Utilizatori WHERE Name = @Name";

            using (SqlCommand command = new SqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                int userCount = (int)command.ExecuteScalar();

                if (userCount > 0)
                {
                    Console.WriteLine("\nUtilizatorul exista deja.");
                    return;
                }
            }

            string insertQuery = "INSERT INTO Utilizatori (Name, HashedPassword, Sex, Height, Kilograms, PhysicalCondition, UserType) VALUES (@Name, @HashedPassword, @Sex, @Height, @Kilograms, @PhysicalCondition, @UserType)";

            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@HashedPassword", HashPasswordSHA256(password));
                if (userType == "Admin" || userType == "")
                {
                    command.Parameters.AddWithValue("@Sex", sex);
                    command.Parameters.AddWithValue("@Height", DBNull.Value);
                    command.Parameters.AddWithValue("@Kilograms", DBNull.Value);
                    command.Parameters.AddWithValue("@PhysicalCondition", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Sex", sex);
                    command.Parameters.AddWithValue("@Height", height);
                    command.Parameters.AddWithValue("@Kilograms", kilograms);
                    command.Parameters.AddWithValue("@PhysicalCondition", physicalCondition);
                }
                command.Parameters.AddWithValue("@UserType", userType);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Utilizator adaugat cu succes." : "Eroare la adaugarea utilizatorului.");
            }
        }
    
        public void             AdaugareReteta(string tipMasa, string ingrediente, int calorii, decimal carbohidrati, decimal proteine, decimal grasimi)
        {
            string insertQuery = "INSERT INTO Retete (TipMasa, Ingrediente, Calorii, Carbohidrati, Proteine, Grasimi) VALUES (@TipMasa, @Ingrediente, @Calorii, @Carbohidrati, @Proteine, @Grasimi)";

            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@TipMasa", tipMasa);
                command.Parameters.AddWithValue("@Ingrediente", ingrediente);
                command.Parameters.AddWithValue("@Calorii", calorii);
                command.Parameters.AddWithValue("@Carbohidrati", carbohidrati);
                command.Parameters.AddWithValue("@Proteine", proteine);
                command.Parameters.AddWithValue("@Grasimi", grasimi);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Reteta adaugata cu succes." : "Eroare la adaugarea retetei.");
            }
        }
        
        public bool             VerificareValiditateUtilizator(string username, string password)
        {
            string checkQuery = "SELECT COUNT(*) FROM Utilizatori WHERE Name = @Name";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            if (checkQuery == null)
            {
                Console.WriteLine($"Utilizatorul {username} este invalid\n");
                return false;
            }
            string hashedPassword = HashPasswordSHA256(password);
            if (hashedPassword == null)
            {
                Console.WriteLine($"Utilizatorul {username} este invalid\n");
                return false;
            }
            string check_Query = "SELECT COUNT(*) FROM Utilizatori WHERE Name = @Name AND HashedPassword = @HashedPassword";
            if (check_Query == null)
            {
                Console.WriteLine($"Utilizatorul {username} este invalid\n");
                return false;
            }
            Console.WriteLine($"Utilizatorul {username} este valid\n");
            return true;
        }

        public void             StergereReteta(string tipMasa, string ingrediente)
        {
            string deleteQuery = "DELETE FROM Retete WHERE TipMasa = @TipMasa AND Ingrediente = @Ingrediente";

            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@TipMasa", tipMasa);
                command.Parameters.AddWithValue("@Ingrediente", ingrediente);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Reteta stearsa cu succes." : "Eroare la stergerea retetei.");
            }
        }

        public void             AdaugareExercitiu(string denumireExercitiu, string grupaMusculara, string descriere, int repetari, int seturi, int timpEstimareExecutie)
        {
            string insertQuery = "INSERT INTO Exercitii (DenumireExercitiu, GrupaMusculara, Repetari, Descriere, Seturi, TimpEstimareExecutie) VALUES (@DenumireExercitiu, @GrupaMusculara, @Repetari, @Descriere, @Seturi, @TimpEstimareExecutie)";

            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@DenumireExercitiu", denumireExercitiu);
                command.Parameters.AddWithValue("@GrupaMusculara", grupaMusculara);
                command.Parameters.AddWithValue("@Repetari", repetari);
                command.Parameters.AddWithValue("@Seturi", seturi);
                command.Parameters.AddWithValue("@TimpEstimareExecutie", timpEstimareExecutie);
                command.Parameters.AddWithValue("@Descriere", descriere);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Exercitiu adaugat cu succes." : "Eroare la adaugarea exercitiului.");
            }
        }
        
        public void             StergereExercitiu(string denumireExercitiu, string grupaMusculara)
        {
            string deleteQuery = "DELETE FROM Exercitii WHERE DenumireExercitiu = @DenumireExercitiu AND GrupaMusculara = @GrupaMusculara";

            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@DenumireExercitiu", denumireExercitiu);
                command.Parameters.AddWithValue("@GrupaMusculara", grupaMusculara);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Exercitiu sters cu succes." : "Eroare la stergerea exercitiului.");
            }
        }

        public static string    HashPasswordSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hash)
                {
                    hashString.Append(b.ToString("x2")); // Convert each byte to a hex string
                }
                return hashString.ToString();
            }
        }
    }
}
