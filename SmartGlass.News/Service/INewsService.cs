using SmartGlass.News.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGlass.News.Service
{
    public interface INewsService
    {
        IEnumerable<NewsSource> NewsSources { get; }
        Task UpdateAsync();

        event EventHandler CategoriesUpdated;
    }
}
