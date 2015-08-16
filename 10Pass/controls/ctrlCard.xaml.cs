using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace _10Pass.controls
{
    public sealed partial class ctrlCard : UserControl
    {


        public ctrlCard()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void SetValueDp(DependencyProperty prop, object value,
            [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        {
            SetValue(prop, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
}
