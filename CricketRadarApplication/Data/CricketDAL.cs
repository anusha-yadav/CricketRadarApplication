using CricketRadarApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketRadarApplication.Data
{
    public class CricketDAL
    {
        private readonly string ConnectionString;

        /// <summary>
        /// Constructor to initialize connection string from configuration.
        /// </summary>
        public CricketDAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["CricketDB"].ConnectionString;
        }

        /// <summary>
        /// Retrieves a collection of all teams from the database.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Teams> GetTeams()
        {
            ObservableCollection<Teams> teams = new ObservableCollection<Teams>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM IPLTeams";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teams.Add(new Teams
                        {
                            TeamId = (int)reader["TeamId"],
                            TeamName = reader["TeamName"].ToString(),
                            City = reader["City"].ToString(),
                            Captain = reader["Captain"].ToString()
                        });
                    }
                }
            }

            return teams;
        }

        /// <summary>
        /// Adds a new cricket team to the database.
        /// </summary>
        /// <param name="team"></param>
        public void AddTeam(Teams team)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO IPLTeams (TeamName, City, Captain) VALUES (@TeamName, @City, @Captain)";
                command.Parameters.AddWithValue("@TeamName", team.TeamName);
                command.Parameters.AddWithValue("@City", team.City);
                command.Parameters.AddWithValue("@Captain", team.Captain);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates an existing cricket team in the database.
        /// </summary>
        /// <param name="team"></param>
        public void UpdateTeam(Teams team)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE IPLTeams SET TeamName = @TeamName, City = @City, Captain = @Captain WHERE TeamId = @TeamId";
                command.Parameters.AddWithValue("@TeamName", team.TeamName);
                command.Parameters.AddWithValue("@City", team.City);
                command.Parameters.AddWithValue("@Captain", team.Captain);
                command.Parameters.AddWithValue("@TeamId", team.TeamId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a cricket team from the database by its ID.
        /// </summary>
        /// <param name="teamId"></param>
        public void DeleteTeam(int teamId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM IPLTeams WHERE TeamId = @TeamId";
                command.Parameters.AddWithValue("@TeamId", teamId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
