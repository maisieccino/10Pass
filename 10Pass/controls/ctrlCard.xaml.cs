using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Wallet;
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
        WalletItem walletItem;

        /// <summary>
        /// Creates a visible wallet item card as a control. You can also edit and save it.
        /// </summary>
        /// <param name="item">Wallet item. Can leave blank if null.</param>
        public ctrlCard(WalletItem item = null)
        {
            this.InitializeComponent();
            walletItem = (item == null ? new WalletItem(WalletItemKind.General,"Wallet Item") : item);
        }

    }
}
