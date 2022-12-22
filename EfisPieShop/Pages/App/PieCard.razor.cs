using EfisPieShop.Models;
using Microsoft.AspNetCore.Components;

namespace EfisPieShop.Pages.App
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; }
    }
}
