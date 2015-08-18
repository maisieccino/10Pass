using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _10Pass.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAddPass : Page
    {
        public PageAddPass()
        {
            this.InitializeComponent();
            clrpckBackgroundColor.PropertyChanged += clrpckBackgroundColor_PropertyChanged;
            clrpckHeaderColor.PropertyChanged += clrpckHeaderColor_PropertyChanged;
        }

        private void clrpckBackgroundColor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedColor")
            {
                cardEdit.BodyColor = clrpckBackgroundColor.SelectedColor;
            }
        }

        private void UpdateControls()
        {
            clrpckBackgroundColor.SelectedColor = cardEdit.BodyColor;
            
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window.Current.Bounds.Width<800)
            {
                Grid.SetColumn(paneCard, 0);
                Grid.SetColumnSpan(paneCard, 2);
                Grid.SetRow(paneCard, 0);
                Grid.SetRowSpan(paneCard, 1);

                Grid.SetColumn(paneSettings, 0);
                Grid.SetColumnSpan(paneSettings, 2);
                Grid.SetRow(paneSettings, 1);
                Grid.SetRowSpan(paneSettings, 1);

                //paneCard.Stretch = Stretch.UniformToFill;
            }
            else
            {
                Grid.SetColumn(paneCard, 0);
                Grid.SetColumnSpan(paneCard, 1);
                Grid.SetRow(paneCard, 0);
                Grid.SetRowSpan(paneCard, 2);

                Grid.SetColumn(paneSettings, 1);
                Grid.SetColumnSpan(paneSettings, 1);
                Grid.SetRow(paneSettings, 0);
                Grid.SetRowSpan(paneSettings, 2);

                //paneCard.Stretch = Stretch.Uniform;
            }
        }

        private void clrpckHeaderColor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            cardEdit.HeaderColor = clrpckHeaderColor.SelectedColor;
        }
    }
}
