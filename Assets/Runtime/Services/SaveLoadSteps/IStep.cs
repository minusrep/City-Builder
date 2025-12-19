using System.Threading.Tasks;

namespace Runtime.Services.SaveLoadSteps
{
    public interface IStep
    {
        Task Run();
    }
}