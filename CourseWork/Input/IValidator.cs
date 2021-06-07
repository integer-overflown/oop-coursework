namespace CourseWork.Input
{
    public interface IValidator<in T>
    {
        bool IsValid(T value);
    }
}