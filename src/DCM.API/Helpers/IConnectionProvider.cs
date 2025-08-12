namespace DCM.API.Helpers
{
    public interface IConnectionProvider
    {
        string GetConnectionString(string name = "DefaultConnection");
    }
}
