using MySql.Data.MySqlClient;


namespace sqlPro
{
    public class AgentsDal
    {
        //====================================
        private const string ConnectionString = "server=127.0.0.1;user id=root;password=;database=EagleEye;port=3306;";
        private readonly MySqlConnection _connection = new MySqlConnection(ConnectionString);

        //====================================
        public AgentsDal()
        {
            OpenConnection();
        }

        //------------------------------------------------------
        public void OpenConnection()
        {
            try
            {
                _connection.Open();
                Console.WriteLine("Connection opened");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to open connection:");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //------------------------------------------------------
        public void CloseConnection()
        {
            _connection.Close();
            Console.WriteLine("Connection closed"); 
        }

        //------------------------------------------------------
        public void ExecuteCommand(string query, Dictionary<string, object> parameters)
        {
            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                foreach (KeyValuePair<string, object> param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                command.ExecuteNonQuery();
            }
        }

        //------------------------------------------------------
        public void UpdateAgentLocationById()
        {
            Console.WriteLine("Enter agent Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter agent new location");
            string newLocation = Console.ReadLine()!;
            string query = "UPDATE agent SET location = @location WHERE id = @id;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@location", newLocation },
                { "@id", id }
            };

            try
            {
                ExecuteCommand(query, parameters);
                Console.WriteLine($"Agent (ID: {id}) location updated to '{newLocation}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to update location:");
                Console.WriteLine(ex.Message);
            }
        }
        
        //------------------------------------------------------
        public void UpdateAgentStatusById()
        {
            Console.WriteLine("Enter agent Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter agent new status");
            string newStatus = Console.ReadLine()!;
            string query = "UPDATE agent SET status= @status WHERE id = @id;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@status", newStatus },
                { "@id", id }
            };

            try
            {
                ExecuteCommand(query, parameters);
                Console.WriteLine($"Agent (ID: {id}) status updated to '{newStatus}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to update status:");
                Console.WriteLine(ex.Message);
            }
        }

        //------------------------------------------------------
        public void AddAgent()
        {
            Console.Write("Enter code name: ");
            string codeName = Console.ReadLine();

            Console.Write("Enter real name: ");
            string realName = Console.ReadLine();

            Console.Write("Enter location: ");
            string location = Console.ReadLine();

            Console.Write("Enter status: ");
            string status = Console.ReadLine();

            string query = "INSERT INTO agent (codeName, realName, location, status, missionsCompleted) " +
                           "VALUES (@codeName, @realName, @location, @status, 0);";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@codeName", codeName },
                { "@realName", realName },
                { "@location", location },
                { "@status", status }
            };

            try
            {
                ExecuteCommand(query, parameters);
                Console.WriteLine("Agent added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add agent:");
                Console.WriteLine(ex.Message);
            }
        }
        //------------------------------------------------------
        public void DeleteAgentById()
        {
            Console.WriteLine("Enter agent Id");
            int id = Convert.ToInt32(Console.ReadLine());
            string query = "DELETE FROM agent WHERE id = @id;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };

            try
            {
                ExecuteCommand(query, parameters);
                Console.WriteLine($"Agent with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to delete agent:");
                Console.WriteLine(ex.Message);
            }
        }
        
        //--------------------------------------------------------------
        public List<Agent> GetAllAgents()
        {
            List<Agent> agents = new List<Agent>();
            string query = "SELECT * FROM agent;";
            MySqlCommand command = new MySqlCommand(query, _connection);
            MySqlDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                {
                    while (reader.Read())
                    {
                        Agent agent = new Agent
                        {
                            Id = reader.GetInt32("id"),
                            CodeName = reader.GetString("codeName"),
                            RealName = reader.GetString("realName"),
                            Location = reader.GetString("location"),
                            Status = reader.GetString("status"),
                            MissionsCompleted = reader.GetInt32("missionsCompleted")
                        };

                        agents.Add(agent);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to retrieve agents:");
                Console.WriteLine(ex.Message);
            }
            return agents;
        }
    }
}
    

