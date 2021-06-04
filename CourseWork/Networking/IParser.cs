using System.Threading.Tasks;

namespace CourseWork.Networking
{
    public interface IParser<out T>
    {
        T? Parse(string input);
    }
}