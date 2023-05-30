using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.Frames
{
    public class IndexViewModel
    {
        public IEnumerable<Frame> Frames { get; set; } = new List<Frame>();
        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Materials { get; set; } = new List<SelectListItem>();
        public Frame CreateViewModel { get; set; } = new Frame();
    }
}
