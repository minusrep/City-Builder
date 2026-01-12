using System.Threading.Tasks;

namespace Runtime.LoadSteps
{
    public interface IStep
    {
        Task Run();
    }
}