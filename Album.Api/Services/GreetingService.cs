namespace Album.Api.Services
{
    public class GreetingService : IGreetingService
    {
        public string GetGreeting(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Hello World";
            }

            return $"Hello {name}";
        }
    }
}