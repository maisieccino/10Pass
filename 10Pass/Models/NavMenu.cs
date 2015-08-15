using _10Pass.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10Pass.Models
{
    public class NavMenu
    {
        public string BackgroundColor { get; set; }
        public string FG { get; set; }
        public List<NavItem> MenuItems { get; set; }
        public NavMenu()
        {
            BackgroundColor = "#FF764514";
            FG = "#FFD9A648";
            MenuItems = new List<NavItem>()
            {
                new NavItem("\uE10F","Home",typeof(PageHome),FG),
                new NavItem("\uE109","Add Pass",typeof(PageAddPass),FG)
            };
            {

            }
        }
    }
}
