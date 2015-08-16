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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace _10Pass.controls
{
    public partial class ctrlColorPicker : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            ASlider.Value = SelectedColor.A;
            RSlider.Value = SelectedColor.R;
            GSlider.Value = SelectedColor.G;
            BSlider.Value = SelectedColor.B;

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public ctrlColorPicker()
        {
            this.InitializeComponent();
            SelectedColor = Color.FromArgb(255, 0, 0, 0);
        }

        private Color _SelectedColor;
        public Color SelectedColor { get { return _SelectedColor; }
            set {
                if (value!=_SelectedColor)
                {
                    _SelectedColor = value;
                    OnPropertyChanged("SelectedColor");
                }
            } }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            byte R, G, B, A;

            A = Convert.ToByte(ASlider.Value);
            R = Convert.ToByte(RSlider.Value);
            G = Convert.ToByte(GSlider.Value);
            B = Convert.ToByte(BSlider.Value);
            if (SelectedColor != Color.FromArgb(A, R, G, B))
            {
                SelectedColor = Color.FromArgb(A, R, G, B);
                try
                {
                    txtRGBA.Text = SelectedColor.ToString();
                }
                catch (NullReferenceException)
                {
                    txtRGBA.Text = "#FF000000";
                }
            }

            showColor.Fill = new SolidColorBrush(SelectedColor);
        }
    }
}
