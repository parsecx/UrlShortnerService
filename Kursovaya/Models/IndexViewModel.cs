using Kursovaya.Entities;

namespace Kursovaya.Models
{
    public class IndexViewModel
    {
        public List<UrlModel> CurrentUrls { get; } = null!;
        public PageViewModel PageViewModel { get; }
        public IndexViewModel(IEnumerable<UrlModel> links, PageViewModel model)
        {
            CurrentUrls = links != null ? links.ToList() : new List<UrlModel>();
            PageViewModel = model;
        }
    }
}
