namespace CourseWork.Database
{
    public class DataChangesNotifier<T>
    {
        public delegate void DataChangedHandler(DataChangedEventArgs args);

        public event DataChangedHandler? DataRemoved;
        public event DataChangedHandler? DataUpdated;

        public void NotifyDataRemoved(T data) => DataRemoved?.Invoke(new DataChangedEventArgs(data));
        public void NotifyDataUpdated(T data) => DataUpdated?.Invoke(new DataChangedEventArgs(data));

        public readonly struct DataChangedEventArgs
        {
            public T Data { get; }

            public DataChangedEventArgs(T data)
            {
                Data = data;
            }
        }
    }
}