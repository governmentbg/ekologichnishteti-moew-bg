﻿namespace Zopoesht.Application.Common.Dtos
{
    public class SearchResultItemDto<T>
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; } = new List<T>();
    }
}
