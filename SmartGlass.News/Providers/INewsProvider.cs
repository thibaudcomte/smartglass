using SmartGlass.News.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGlass.News.Providers
{
    public interface INewsProvider
    {
        string Name { get; }
        Task<IEnumerable<NewsEntry>> GetNewsEntriesAsync();
    }
}
