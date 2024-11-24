using 

namespace SampleCleanArchitecture.Shared
{
    public class PagedList<T>
    {
        private List<T> _list { get; set; }
        public List<T> List { get => _list; set => _list = value; }

        private int _total { get; set; }
        public int Total { get => _total; set => _total = value; }
        private int _currentOffset { get; set; }
        public int CurrentOffset { get => _currentOffset; set => _currentOffset = value; }

        private int _pageSize { get; set; }
        public int PageSize { get => _pageSize; set => _pageSize = value; }

    }

}
