namespace CourseWork.Views.MenuScreens
{
    public interface ISearchAgent<T>
    {
        delegate void SearchFailedHandler(SearchFailedEventArgs args);

        delegate void SearchResultIsEmptyHandler();

        delegate void SearchSucceededHandler(SearchSucceededEventArgs args);

        event SearchSucceededHandler SearchSucceeded;
        event SearchFailedHandler SearchFailed;
        event SearchResultIsEmptyHandler SearchResultIsEmpty;

        public readonly struct SearchSucceededEventArgs
        {
            public T Result { get; }

            public SearchSucceededEventArgs(T result)
            {
                Result = result;
            }
        }

        public readonly struct SearchFailedEventArgs
        {
            public string Reason { get; }

            public SearchFailedEventArgs(string reason)
            {
                Reason = reason;
            }
        }
    }
}