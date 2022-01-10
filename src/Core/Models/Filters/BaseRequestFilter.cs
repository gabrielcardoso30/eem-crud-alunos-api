namespace Core.Models.Filters
{

    public class BaseRequestFilter
    {

        public string TableFilter { get; set; }
        public string Text { get; set; }
        public int Take { get; set; } = 100;
        public int? Page { get; set; }
        public int? Offset { get; set; }
        public string SortingProp { get; set; }
        public bool Ascending { get; set; } = true;
        
        internal int Skip
        {

            get
            {

                if (Offset != null)
                    return (int)Offset;
                
                if (Page != null)
                    return ((int)Page-1) * Take;
                
                return 0;

            }

        }

    }

}
