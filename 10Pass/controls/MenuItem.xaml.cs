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
    public sealed partial class MenuItem : UserControl
    {
        /// <summary>
        /// The icon to display 
        /// </summary>
        //public string Icon
        //{
        //    get
        //    {
        //        return txtLogo.Text;
        //    }
        //    set
        //    {
        //        txtLogo.Text = value;
        //    }
        //}
        //public string Label
        //{
        //    get
        //    {
        //        return txtLabel.Text;
        //    }
        //    set
        //    {
        //        txtLabel.Text = Label;
        //    }
        //}

        //private Brush _backgroundColor = null;
        //public Brush BackgroundColor
        //{
        //    get
        //    {
        //        return _backgroundColor;
        //    }
        //    set
        //    {
        //        _backgroundColor = root.Background = value;
        //    }
        //}

        //private Brush _foregroundColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        //public Brush ForegroundColor
        //{
        //    get
        //    {
        //        return _foregroundColor;
        //    }
        //    set
        //    {
        //        _foregroundColor = txtLabel.Foreground = txtLogo.Foreground = value;
        //    }
        //}

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValueDp(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string),
            typeof(MenuItem),null);


        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValueDp(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
            typeof(MenuItem), null);


        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValueDp(BackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush),
            typeof(MenuItem), null);


        public Brush ForegroundColor
        {
            get { return (Brush)GetValue(ForegroundColorProperty); }
            set { SetValueDp(ForegroundColorProperty, value); }
        }
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register("ForegroundColor", typeof(Brush),
            typeof(MenuItem), null);


        public Brush BackgroundHoverColor
        {
            get { return (Brush)GetValue(BackgroundHoverColorProperty); }
            set { SetValueDp(BackgroundHoverColorProperty, value); }
        }
        public static readonly DependencyProperty BackgroundHoverColorProperty =
            DependencyProperty.Register("BackgroundHoverColor", typeof(Brush),
            typeof(MenuItem), null);


        public Brush ForegroundHoverColor
        {
            get { return (Brush)GetValue(ForegroundHoverColorProperty); }
            set { SetValueDp(ForegroundHoverColorProperty, value); }
        }
        public static readonly DependencyProperty ForegroundHoverColorProperty =
            DependencyProperty.Register("ForegroundHoverColor", typeof(Brush),
            typeof(MenuItem), null);


        public event PropertyChangedEventHandler PropertyChanged;
        void SetValueDp(DependencyProperty prop, object value,
            [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        {
            SetValue(prop, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        public MenuItem()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

    }
}
