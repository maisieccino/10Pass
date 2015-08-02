using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Wallet;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _10Pass
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PassProcessor : Page
    {
        Windows.Storage.StorageFile paramPass = null;

        public PassProcessor()
        {
            this.InitializeComponent();
        }        

        //private async Task<bool> generatePass (JObject o)
        //{
        //    //do stuff
        //    if (card != null)
        //    {
        //        //await Task.Run(() => { txtInfo.Text = "Creating .mswallet file..."; });
        //        var passType = "";
        //        switch (card.Kind.ToString())
        //        {
        //            case "BoardingPass": passType = "boardingPass"; break;
        //            case "Deal": passType = "coupon"; break;
        //            case "Ticket": passType = "eventTicket"; break;
        //            case "MembershipCard": passType = "storeCard"; break;
        //            default: passType = "generic"; break;
        //        }
        //        try
        //        {
        //            card.BodyColor = HelperMethods.getColorFromRGBString(o["backgroundColor"].Value<string>());
        //            card.BodyFontColor = HelperMethods.getColorFromRGBString(o["foregroundColor"].Value<string>());
        //            card.HeaderColor = card.BodyColor;
        //            card.HeaderFontColor = HelperMethods.getColorFromRGBString(o["labelColor"].Value<string>());

        //            card.IssuerDisplayName = o["organizationName"].Value<string>();

        //            IReadOnlyList<Windows.Storage.StorageFile> files = await Windows.Storage.ApplicationData.Current.TemporaryFolder.GetFilesAsync();
        //            //txtInfo.Text = "Loading image files...";
        //            card.Logo99x99 = files.Where(x => x.Name == "icon.png").First();
        //            card.Logo159x159 = card.Logo336x336 = card.Logo99x99;
        //            card.HeaderBackgroundImage = files.Where(x => x.Name == "logo.png").First();
        //            try
        //            {
        //                card.BodyBackgroundImage = files.Where(x => x.Name == "background.png").First();
        //            }
        //            catch (Exception) { txtInfo.Text = "No background image found!"; };
        //            int i = 0;

        //            //Primary Fields
        //            if (o[passType]["primaryFields"] != null)
        //            {
        //                foreach (JObject jo in o[passType]["primaryFields"])
        //                {
        //                    WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
        //                    if (i == 0) prop.DetailViewPosition = WalletDetailViewPosition.PrimaryField1;
        //                    else if (i == 1) prop.DetailViewPosition = WalletDetailViewPosition.PrimaryField2;
        //                    else throw new InvalidDataException();
        //                    card.DisplayProperties[jo["key"].Value<string>()] = prop;
        //                    i++;
        //                }
        //            }

        //            //Secondary fields
        //            i = 0;
        //            if (o[passType]["secondaryFields"] != null)
        //            {
        //                foreach (JObject jo in o[passType]["secondaryFields"])
        //                {
        //                    WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
        //                    if (i == 0) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField1;
        //                    else if (i == 1) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField2;
        //                    else if (i == 2) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField3;
        //                    else if (i == 3) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField4;
        //                    else if (i == 4) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField5;
        //                    else throw new InvalidDataException();
        //                    card.DisplayProperties[jo["key"].Value<string>()] = prop;
        //                    i++;
        //                }
        //            }

        //            //Auxiliary fields
        //            i = 0;
        //            if (o[passType]["auxiliaryFields"] != null)
        //            {
        //                foreach (JObject jo in o[passType]["auxiliaryFields"])
        //                {
        //                    WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
        //                    if (i == 0) prop.DetailViewPosition = WalletDetailViewPosition.FooterField1;
        //                    else if (i == 1) prop.DetailViewPosition = WalletDetailViewPosition.FooterField2;
        //                    else if (i == 2) prop.DetailViewPosition = WalletDetailViewPosition.FooterField3;
        //                    else if (i == 3) prop.DetailViewPosition = WalletDetailViewPosition.FooterField4;
        //                    else throw new InvalidDataException();
        //                    card.DisplayProperties[jo["key"].Value<string>()] = prop;
        //                    i++;
        //                }
        //            }

        //            //Header fields
        //            i = 0;
        //            if (o[passType]["headerFields"] != null)
        //            {
        //                foreach (JObject jo in o[passType]["headerFields"])
        //                {
        //                    WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
        //                    if (i == 0) prop.DetailViewPosition = WalletDetailViewPosition.HeaderField1;
        //                    else if (i == 1) prop.DetailViewPosition = WalletDetailViewPosition.HeaderField2;
        //                    else throw new InvalidDataException();
        //                    card.DisplayProperties[jo["key"].Value<string>()] = prop;
        //                    i++;
        //                }
        //            }

        //            //Header fields
        //            if (o[passType]["backFields"] != null)
        //            {
        //                foreach (JObject jo in o[passType]["backFields"])
        //                {
        //                    WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
        //                    prop.SummaryViewPosition = WalletSummaryViewPosition.Hidden;
        //                    card.DisplayProperties[jo["key"].Value<string>()] = prop;
        //                }
        //            }

        //            if (o["barcode"] != null)
        //            {
        //                WalletBarcodeSymbology sym = new WalletBarcodeSymbology();
        //                switch (o["barcode"]["format"].Value<string>())
        //                {
        //                    case "PKBarcodeFormatQR": sym = WalletBarcodeSymbology.Qr; break;
        //                    case "PKBarcodeFormatPDF417": sym = WalletBarcodeSymbology.Pdf417; break;
        //                    case "PKBarcodeFormatAztec": sym = WalletBarcodeSymbology.Aztec; break;
        //                    default: throw new InvalidDataException();
        //                }
        //                card.Barcode = new WalletBarcode(sym, o["barcode"]["message"].Value<string>());
        //            }

        //            if (o["locations"] != null)
        //            {
        //                i = 0;
        //                foreach (JObject jo in o["locations"])
        //                {
        //                    WalletRelevantLocation location = new WalletRelevantLocation();
        //                    location.DisplayMessage = jo["relevantText"].Value<string>();
        //                    var position = new Windows.Devices.Geolocation.BasicGeoposition();
        //                    position.Latitude = jo["latitude"].Value<double>();
        //                    position.Longitude = jo["longitude"].Value<double>();
        //                    try {
        //                        position.Altitude = jo["altitude"].Value<double>();
        //                    }
        //                    catch(Exception)
        //                    {
        //                        System.Diagnostics.Debug.WriteLine("An altitude does not exist for location " + location.DisplayMessage);
        //                    }
        //                    location.Position = position;
        //                    //Check one doesn't already exist.
        //                    if (card.RelevantLocations.Where(x => x.Key == i.ToString()) != null)
        //                        i++;
        //                    else
        //                        card.RelevantLocations.Add(i.ToString(), location);
        //                    i++;
        //                }
        //            }

        //            if (o["relevantDate"] != null)
        //            {
        //                card.RelevantDate = DateTime.Parse(o["relevantDate"].Value<string>());
        //            }

        //            if (o["expirationDate"] != null)
        //            {
        //                card.ExpirationDate = DateTime.Parse(o["expirationDate"].Value<string>());
        //            }

        //            return true;

        //        }
        //        catch (Exception)
        //        {
        //            card = null;
        //            return false;
        //        }
        //    }
        //    else return false;
        //}

        /// <summary>
        /// This is the method that will process the pkpass file.
        /// </summary>
        /// <param name="file"></param>
        //private async Task<bool> processPass(Windows.Storage.StorageFile file)
        //{
        //    bool success = false;
        //    txtInfo.Text = "Preparing for processing...";
        //    Windows.Storage.StorageFolder folder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            
        //    txtInfo.Text = "Extracting the .PKPASS file...";
        //    //ZipFile.ExtractToDirectory(file.Path.ToString(), folder.Path.ToString());
        //    await Task.Run(() => { ZipFile.ExtractToDirectory(file.Path.ToString(), folder.Path.ToString()); });
            
        //    IReadOnlyList<Windows.Storage.StorageFile> files = await Windows.Storage.ApplicationData.Current.TemporaryFolder.GetFilesAsync();

        //    txtInfo.Text = "Reading pass.json...";
            
        //    using (StreamReader reader = File.OpenText(files.Where(x => x.Name == "pass.json").First().Path.ToString()))
        //    {
                
        //        JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
        //        if(o["boardingPass"]!= null)
        //        {
        //            card = new WalletItem(WalletItemKind.BoardingPass, o["description"].Value<string>());
        //        }
        //        else if (o["coupon"] != null)
        //        {
        //            card = new WalletItem(WalletItemKind.Deal, o["description"].Value<string>());
        //        }
        //        else if (o["eventTicket"] != null)
        //        {
        //            card = new WalletItem(WalletItemKind.Ticket, o["description"].Value<string>());
        //        }
        //        else if (o["storeCard"] != null)
        //        {
        //            card = new WalletItem(WalletItemKind.MembershipCard, o["description"].Value<string>());
        //        }
        //        else if (o["generic"] != null)
        //        {
        //            card = new WalletItem(WalletItemKind.General, o["description"].Value<string>());
        //        }

        //        success = await generatePass(o);
        //        if(!success)
        //        {
        //            MessageDialog msgbox = new MessageDialog("Invalid pass.json.", "Error");
        //            await msgbox.ShowAsync();
        //            progRing.IsActive = false;
        //            btnCancel.Visibility = Visibility.Collapsed;
        //            success = false;
        //        }
        //        else
        //        {
        //            WalletItemStore wallet = await WalletManager.RequestStoreAsync();
        //            txtInfo.Text = "Saving to wallet...";
        //            string cardId = o["serialNumber"].Value<string>();
        //            if (await wallet.GetWalletItemAsync(cardId) != null)
        //            {
        //                MessageDialog msgbox = new MessageDialog("Would you like to overwrite this item?", "An item with the same ID exists!");
        //                UICommand uiYes = new UICommand("Yes");
        //                UICommand uiNo = new UICommand("No");
        //                msgbox.Commands.Add(uiYes);
        //                msgbox.Commands.Add(uiNo);
        //                msgbox.DefaultCommandIndex = 0;
        //                msgbox.CancelCommandIndex = 1;
        //                if (await msgbox.ShowAsync() == uiYes)
        //                {
        //                    try
        //                    {
        //                        await wallet.DeleteAsync(cardId);
        //                        await wallet.AddAsync(cardId, card);
        //                        success = true;
        //                        txtInfo.Text = "Saved! Press the button below to return to the home screen.";
        //                    }
        //                    catch (Exception)
        //                    {
        //                        success = false;
        //                        txtInfo.Text = "There was a problem saving the card to the wallet.";
        //                    }
        //                }
        //                else
        //                {
        //                    success = false;
        //                    txtInfo.Text = "You cancelled the creation.";
        //                }
                            
        //            }
        //            else
        //            {
        //                await wallet.AddAsync(cardId, card);
        //                txtInfo.Text = "Saved! Press the button below to return to the home screen.";
        //            }

        //            btnReturn.Visibility = Visibility.Visible;
        //            progRing.IsActive = false;
        //            btnCancel.Visibility = Visibility.Collapsed;
        //            HelperMethods.clearTempFiles();
        //            if (success)
        //                await wallet.ShowAsync(cardId);
                   
        //        }
        //    }
        //    return success;
        //}

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string)
            {
                //paramPass = (e.Parameter as Windows.Storage.StorageFile);
                if ((e.Parameter as String) == "fileToken")
                {
                    paramPass = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync("passFileToken");
                    HelperMethods.clearTempFiles();
                }
                else
                    this.Frame.GoBack();
            }
        }

        private async void CurrentInfoChanged(object sender, string newInfo)
        {
            txtInfo.Text = newInfo;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (paramPass != null)
            {
                btnCancel.Visibility = Visibility.Visible;
                btnReturn.Visibility = Visibility.Collapsed;
                progRing.IsActive = true;
                TitleText.Text = "Please wait...";
                try {
                    HelperMethods.clearTempFiles();
                    pkpassHelpers pkpasshelper = new pkpassHelpers(paramPass);
                    pkpasshelper.CurrentInfoChanged += CurrentInfoChanged;
                    pkPassConversionResult result = await pkpasshelper.createWalletItem();
                    if(result!=null)
                        OnSuccessfulCardCreation(result);
                    else OnFailedCardCreation();
                }
                catch (OperationCanceledException)
                {
                    TitleText.Text = "Cancelled!";
                    txtInfo.Text = "You cancelled the operation.";
                    commonCompleteCommands();
                    MessageDialog msg = new MessageDialog("Operation cancelled.");
                    await msg.ShowAsync();
                }
                catch(Exception ex)
                {
                    MessageDialog msg = new MessageDialog(ex.Message, "There was an error processing the file.");
                    commonCompleteCommands();
                }

            }
            UpdateLayout();
        }

        private void OnFailedCardCreation()
        {
            commonCompleteCommands();
            if(!Regex.IsMatch(txtInfo.Text,@"Exception\:.*"))
                TitleText.Text = "Operation cancelled.";
        }

        private async void OnSuccessfulCardCreation(pkPassConversionResult result)
        {
            if (result != null)
            {
                WalletItem item = result.item;
                string cardId = result.cardId;
                commonCompleteCommands();
                TitleText.Text = "Done!";
                txtInfo.Text = "The .mswallet file has been created.";
                MessageDialog msg = new MessageDialog("Would you like to add this to your wallet?", "Item created!");
                UICommand uiYes = new UICommand("Yes");
                UICommand uiNo = new UICommand("No");
                msg.Commands.Add(uiYes); msg.Commands.Add(uiNo);
                if (await msg.ShowAsync() == uiYes)
                    await SaveItemToWallet(item,cardId);
            }
            else
            {
                commonCompleteCommands();
                TitleText.Text = "Operation failed or cancelled.";
            }
        }

        private async Task SaveItemToWallet(WalletItem item,string cardId)
        {
            WalletItemStore wallet = await WalletManager.RequestStoreAsync();
            txtInfo.Text = "Saving to wallet...";
            if (await wallet.GetWalletItemAsync(cardId) != null)
            {
                MessageDialog msgbox = new MessageDialog("Would you like to overwrite this item?", "An item with the same ID exists!");
                UICommand uiYes = new UICommand("Yes");
                UICommand uiNo = new UICommand("No");
                msgbox.Commands.Add(uiYes);
                msgbox.Commands.Add(uiNo);
                msgbox.DefaultCommandIndex = 0;
                msgbox.CancelCommandIndex = 1;
                if (await msgbox.ShowAsync() == uiYes)
                {
                    try
                    {
                        await wallet.DeleteAsync(cardId);
                        await wallet.AddAsync(cardId, item);
                        txtInfo.Text = "Saved! Press the button below to return to the home screen.";
                        await wallet.ShowAsync(cardId);
                    }
                    catch (Exception)
                    {
                        txtInfo.Text = "There was a problem saving the card to the wallet.";
                    }
                }
                else
                {
                    txtInfo.Text = "You cancelled the creation.";
                }

            }
            else
            {
                await wallet.AddAsync(cardId, item);
                txtInfo.Text = "Saved! Press the button below to return to the home screen.";
                await wallet.ShowAsync(cardId);
            }
        }

        private void commonCompleteCommands()
        {
            btnCancel.Visibility = Visibility.Collapsed;
            btnReturn.Visibility = Visibility.Visible;
            progRing.IsActive = false;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
            else this.Frame.Navigate(typeof(MainPage));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
