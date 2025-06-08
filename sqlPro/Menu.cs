using ZstdSharp.Unsafe;

namespace sqlPro;

public class Controller
{
    private AgentsDal _dal = new AgentsDal();

    public Controller()
    {
        RunMenu();
    }
    public void RunMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Add agent");
            Console.WriteLine("2. Delete agent");
            Console.WriteLine("3. change agent location");
            Console.WriteLine("4. change agent status");
            Console.WriteLine("5. get all agents");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1-6): ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    _dal.AddAgent();
                    break;
                case "2":
                    _dal.DeleteAgentById();
                    break;
                case "3":
                    _dal.UpdateAgentLocationById();
                    break;
                case "4":
                    _dal.UpdateAgentStatusById();
                    break;
                case "5":
                    List<Agent> agents = _dal.GetAllAgents();
                    foreach (Agent agent in agents)
                        agent.PrintInfo();
                    break;
                case "6":
                    _dal.CloseConnection();
                    break;
                default:
                    break;

            }
        }
    }
}