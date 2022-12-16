namespace FindATrade.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPrevious => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int EntitiesCount { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.EntitiesCount / this.ItemsPerPage);

        public int ItemsPerPage { get; set; }
    }
}
