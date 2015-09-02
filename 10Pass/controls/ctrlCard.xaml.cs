using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Wallet;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ZXing;
using ZXing.Rendering;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace _10Pass.controls
{
    public sealed partial class ctrlCard : UserControl
    {
        public WalletItem walletItem;

        public enum CardStateType {Front,Back}

        public enum SymbologyTypes { UPCA, UPCE, EAN13, EAN8, ITF, CODE39, CODE128, QR, PDF417, AZTEC }

        public WalletBarcode Barcode
        {
            get { return walletItem.Barcode; }
            set
            {
                walletItem.Barcode = value;
               GenBarcode();
            }
        }

        private async void GenBarcode()
        {
            //Awful but neccessary.
            BarcodeFormat format;
            switch(walletItem.Barcode.Symbology)
            {
                case WalletBarcodeSymbology.Aztec: format = BarcodeFormat.AZTEC; break;
                case WalletBarcodeSymbology.Code128: format = BarcodeFormat.CODE_128; break;
                case WalletBarcodeSymbology.Code39: format = BarcodeFormat.CODE_39; break;
                case WalletBarcodeSymbology.Ean13: format = BarcodeFormat.EAN_13; break;
                case WalletBarcodeSymbology.Ean8: format = BarcodeFormat.EAN_8; break;
                case WalletBarcodeSymbology.Itf: format = BarcodeFormat.ITF; break;
                case WalletBarcodeSymbology.Pdf417: format = BarcodeFormat.PDF_417; break;
                case WalletBarcodeSymbology.Qr: format = BarcodeFormat.QR_CODE; break;
                case WalletBarcodeSymbology.Upca: format = BarcodeFormat.UPC_A; break;
                case WalletBarcodeSymbology.Upce: format = BarcodeFormat.UPC_E; break;
                default: format = BarcodeFormat.QR_CODE; break;
            }
            var encOptions = new ZXing.Common.EncodingOptions() { Width = 256, Height = 256, Margin = 5 };
            //BarcodeWriter bcw = new BarcodeWriter
            //{
            //    Format = format,
            //    Options = encOptions,
            //};
            IBarcodeWriter writer = new BarcodeWriter
            {
                Format = format,
                Options = encOptions,
                Renderer = new ZXing.Rendering.PixelDataRenderer() { Foreground = Colors.Black }
            };

            try {
                var result = writer.Write(walletItem.Barcode.Value);
                imgBarcode.Source = result.ToBitmap() as WriteableBitmap;
                imgBarcode.UpdateLayout();
            }
            catch
            {
                MessageDialog msgd = new MessageDialog("There may have been a formatting problem with your chosen format.", "Error");
                await msgd.ShowAsync();
            }
        }

        private CardStateType _cardState;
        public CardStateType CardState
        {
            get { return _cardState; }
            set
            {
                _cardState = value;
                if(_cardState== CardStateType.Front)
                {
                    gridBody.Visibility = Visibility.Visible;
                    stackBack.Visibility = Visibility.Collapsed;
                }
                else
                {
                    gridBody.Visibility = Visibility.Collapsed;
                    stackBack.Visibility = Visibility.Visible;
                }
            }
        }

        public Color BodyColor
        {
            get { return walletItem.BodyColor; }
            set {
                walletItem.BodyColor = value;
                gridBody.Background = new SolidColorBrush(walletItem.BodyColor);
            }
        }

        public Color HeaderColor
        {
            get { return walletItem.HeaderColor; }
            set
            {
                walletItem.HeaderColor = value;
                stackHeader.Background = new SolidColorBrush(walletItem.HeaderColor);
            }
        }

        public Color BodyTextColor
        {
            get { return walletItem.BodyFontColor; }
            set
            {
                walletItem.BodyFontColor = value;
                // Set colors here.
            }
        }

        public Color HeaderTextColor
        {
            get { return walletItem.HeaderFontColor; }
            set
            {
                walletItem.HeaderFontColor = value;
                //set control text color here.
            }
        }

        public StorageFile LogoImage
        {
            get { return (StorageFile)walletItem.LogoImage;  }
            set
            {
                walletItem.LogoImage = walletItem.Logo336x336 = walletItem.Logo159x159 = walletItem.Logo99x99 = value;
            }
        }

        /// <summary>
        /// Creates a visible wallet item card as a control. You can also edit and save it.
        /// </summary>
        /// <param name="item">Wallet item. Can leave blank if null.</param>
        public ctrlCard(WalletItem item = null)
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            walletItem = (item == null ? new WalletItem(WalletItemKind.General,"Wallet Item") : item);
        }

        public ctrlCard()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            walletItem = new WalletItem(WalletItemKind.General, "Wallet Item");
            GenerateControl();
        }

        async void GenerateControl()
        {
            gridBody.Background = new SolidColorBrush(walletItem.BodyColor);

        }

    }
}
