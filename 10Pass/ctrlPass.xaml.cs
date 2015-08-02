using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace _10Pass
{
    [DefaultValue("#FFFFFFFF")]
    public sealed partial class ctrlPass : UserControl
    {
        public SolidColorBrush BackgroundColor
        {
            get { return (passRoot.Background as SolidColorBrush); }
            set { passRoot.Background = value; }
        }

        public ctrlPass()
        {
            this.InitializeComponent();
        }
    }
}
