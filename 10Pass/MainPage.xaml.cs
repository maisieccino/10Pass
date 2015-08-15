using _10Pass.Models;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _10Pass
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }



        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            splitMain.IsPaneOpen = !splitMain.IsPaneOpen;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavMenu.SelectedIndex = 0;
        }

        private void NavMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            if (list.SelectedIndex == -1) { return; }
            Frame current = splitMain.Content as Frame;
            current.Navigate(((NavItem)list.SelectedItem).Page);
            splitMain.IsPaneOpen = false;
            PageDesc.Text = ((NavItem)list.SelectedItem).Text;
        }
    }
}
