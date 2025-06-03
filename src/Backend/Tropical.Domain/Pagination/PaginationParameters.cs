﻿namespace Tropical.Comunication.Pagination
{
    public class PaginationParameters
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Direction { get; set; } = "ASC";
    }
}
