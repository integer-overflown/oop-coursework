using System.Threading.Tasks;

namespace CourseWork.ViewModels
{
    public interface IAsyncInitialization
    {
        public Task Initialization { get; }
    }
}