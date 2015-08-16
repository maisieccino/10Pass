using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace _10Pass.controls
{
    public sealed partial class ctrlColorPicker : UserControl
    {


        public ctrlColorPicker()
        {
            this.InitializeComponent();
            SelectedColor = Color.FromArgb(255, 0, 0, 0);
        }

        public Color SelectedColor { get; set; }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            byte R, G, B, A;

            A = Convert.ToByte(ASlider.Value);
            R = Convert.ToByte(RSlider.Value);
            G = Convert.ToByte(GSlider.Value);
            B = Convert.ToByte(BSlider.Value);

            SelectedColor = Color.FromArgb(A, R, G, B);
            txtRGBA.Text = SelectedColor.ToString();

            showColor.Fill = new SolidColorBrush(SelectedColor);
        }
    }
}
