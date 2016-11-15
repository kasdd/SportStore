using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.Domain
{
    public enum Availability
    {
        [Display(Name = "Shop")]
        ShopOnly,
        [Display(Name = "Online")]
        OnlineOnly,
        [Display(Name = "Shop and Online")]
        ShopAndOnline
    }
}