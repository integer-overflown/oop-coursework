namespace CourseWork.Database
{
    public class DataChangesNotifier<T>
    {
        public delegate void DataChangedHandler(DataChangedEventArgs args);

        public event DataChangedHandler? DataAppended;
        public event DataChangedHandler? DataRemoved;
        public event DataChangedHandler? DataEdited;

        public void NotifyDataAppended(T data) => DataAppended?.Invoke(new DataChangedEventArgs(data));
        public void NotifyDataRemoved(T data) => DataRemoved?.Invoke(new DataChangedEventArgs(data));
        public void NotifyDataEdited(T data) => DataEdited?.Invoke(new DataChangedEventArgs(data));

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