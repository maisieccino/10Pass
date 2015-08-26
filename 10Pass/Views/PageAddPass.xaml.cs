using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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

        private async void btnLogoImageSet_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.CommitButtonText = "Choose";
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file!=null)
            {
                cardEdit.LogoImage = file;
                lblLogoImage.Text = file.Name;
                btnLogoImageClear.IsEnabled = true;
            }
        }

        private void btnLogoImageClear_Click(object sender, RoutedEventArgs e)
        {
            cardEdit.LogoImage = null;
            lblLogoImage.Text = "";
            btnLogoImageClear.IsEnabled = false;
        }

        private void appbarFlip_Click(object sender, RoutedEventArgs e)
        {
            if (cardEdit.CardState == controls.ctrlCard.CardStateType.Front)
                cardEdit.CardState = controls.ctrlCard.CardStateType.Back;
            else cardEdit.CardState = controls.ctrlCard.CardStateType.Front;
        }
    }
}
