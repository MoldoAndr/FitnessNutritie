using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace FitnessNutritionDB
{
    public static class ClientHandler
    {
        public static void          HandleClient(TcpClient client)
        {
            Console.WriteLine("Client connected.");
            try
            {
                using (NetworkStream networkStream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(networkStream, Encoding.UTF8, leaveOpen: true))
                using (BinaryWriter writer = new BinaryWriter(networkStream, Encoding.UTF8, leaveOpen: true))
                {
                    string username = null;
                    bool authenticated = false;

                    while (true)
                    {
                        try
                        {
                            string command = reader.ReadString();
                            switch (command)
                            {
                                case "SIGNUP":
                                    HandleSignup(reader, writer);
                                    break;
                                case "LOGIN":
                                    username = HandleLogin(reader, writer, ref authenticated);
                                    break;
                                case "UPLOAD":
                                case "DOWNLOAD":
                                case "DELETE":
                                case "LIST":
                                case "SHARE":
                                case "VIEW":
                                case "RENAME":
                                case "SAVE":
                                case "SEND":
                                    if (authenticated)
                                    {
                                        //CommandHandler.HandleAuthenticatedCommand(command, reader, writer, username);
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        writer.Write("NOT_AUTHENTICATED");
                                        Console.WriteLine("Unauthenticated access attempt.");
                                    }
                                    break;
                                case "DISCONNECT":
                                    Console.WriteLine("Client requested disconnect.");
                                    return;
                                default:
                                    writer.Write("INVALID_COMMAND");
                                    Console.WriteLine($"Invalid command received: {command}");
                                    break;
                            }
                        }
                        catch (IOException ioEx)
                        {
                            Console.WriteLine($"IO Exception: {ioEx.Message}");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected.");
            }
        }

        private static void         HandleSignup(BinaryReader reader, BinaryWriter writer)
        {
            try
            {
                string newUsername = reader.ReadString();           // Username
                string newPassword = reader.ReadString();           // Password
                string userType = reader.ReadString();              // User type (Admin, Nutritionist, Client)
                string sex = reader.ReadString();                   // Sex (Optional for non-admin users)
                decimal height = reader.ReadDecimal();              // Height (Optional for non-admin users)
                decimal weight = reader.ReadDecimal();              // Weight (Optional for non-admin users)
                string physicalCondition = reader.ReadString();     // Physical condition (Optional for non-admin users)

                SqlConnectionManager managerSQL = new SqlConnectionManager();
                
                managerSQL.AdaugareUtilizator(newUsername, newPassword, userType, sex, height, weight, physicalCondition);
                writer.Write("SIGNUP_SUCCESS");
                Console.WriteLine($"User '{newUsername}' signed up successfully.");
            }
            catch (Exception ex)
            {
                writer.Write("SIGNUP_FAILED");
                Console.WriteLine($"Signup failed: {ex.Message}");
            }
        }

        private static string       HandleLogin(BinaryReader reader, BinaryWriter writer, ref bool authenticated)
        {
            string username = reader.ReadString();
            string password = reader.ReadString();
            //authenticated = UserManager.ValidateUser(username, password);
            writer.Write(authenticated);
            Console.WriteLine(authenticated ? $"User authenticated: {username}" : $"Authentication failed for user: {username}");
            return authenticated ? username : null;
        }
    }
}
