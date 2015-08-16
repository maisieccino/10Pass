using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _10Pass.Views;

namespace _10Pass.Models
{
    public class NavItem
    {
        public object Glyph { get; set; }
        public string Text { get; set; }
        public Type Page { get; set; }

        public string ForegroundColor { get; set; }

        public NavItem(object glyph = null, string text = "", Type page = null, string foregroundcolor = "")
        {
            Glyph = glyph;
            Text = text;
            Page = (page == null ? typeof(BlankPage) : page);
            ForegroundColor = foregroundcolor;
        }
    }
}
