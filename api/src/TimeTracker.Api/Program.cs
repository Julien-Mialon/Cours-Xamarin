using Storm.Api.Launchers;

namespace TimeTracker.Api;

public class Program
{
    public static void Main(string[] args)
    {
        DefaultLauncher<Startup>.RunWebHost(args);
    }
}