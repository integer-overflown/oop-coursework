namespace CourseWork.Database
{
    public class DataChangesNotifier
    {
        public delegate void DataAppendedHandler();

        public event DataAppendedHandler DataAppended;

        public void NotifyDataAppended()
        {
            DataAppended.Invoke();
        }
    }
}