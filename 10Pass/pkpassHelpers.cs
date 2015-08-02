using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Wallet;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace _10Pass
{
    public delegate void ChangedCurrentInfoHandler(object sender, string NewInfo);

    /// <summary>
    /// Your one stop shop for working with pkpass files.
    /// </summary>
    public class pkpassHelpers
    {
        private string _currentInfo;
        public string CurrentInfo { get { return _currentInfo; } private set
            {
                _currentInfo = value;
                OnCurrentInfoChanged();
            } }
        Windows.Storage.StorageFile sourceFile;
        IReadOnlyList<Windows.Storage.StorageFile> contents = null;
        string pkPassType = "generic";

        public event ChangedCurrentInfoHandler CurrentInfoChanged;

        protected virtual void OnCurrentInfoChanged()
        {
            if (CurrentInfoChanged != null)
                CurrentInfoChanged(this, CurrentInfo);
        }

        /// <summary>
        /// Creates an instance of pkpassHelpers.
        /// </summary>
        /// <param name="file">The .pkpass file to be processed.</param>
        public pkpassHelpers(Windows.Storage.StorageFile file)
        {
            sourceFile = file;
        }



        JObject getJsonData(Windows.Storage.StorageFile file)
        {
            StreamReader reader = File.OpenText(file.Path.ToString());
            return (JObject)JToken.ReadFrom(new JsonTextReader(reader));
        }

        Windows.Storage.StorageFile getImageFromName(string name, IReadOnlyList<Windows.Storage.StorageFile> files)
        {
            try {
                return files.Where(x => x.Name == name).First();
            }
            catch
            {
                return null;
            }
        }

        pkPassConversionResult parsePkpassType(IReadOnlyList<Windows.Storage.StorageFile> files)
        {
            try
            {
                WalletItem item = null;
                CurrentInfo = "Opening pass.json...";
                JObject o = getJsonData(files.Where(x => x.Name == "pass.json").First());
                //Determine the type of the card and return it.
                if (o["boardingPass"] != null)
                {
                    pkPassType = "boardingPass";
                    item = new WalletItem(WalletItemKind.BoardingPass, o["description"].Value<string>());
                }
                else if (o["coupon"] != null)
                {
                    pkPassType = "coupon";
                    item = new WalletItem(WalletItemKind.Deal, o["description"].Value<string>());
                }
                else if (o["eventTicket"] != null)
                {
                    pkPassType = "eventTicket";
                    item = new WalletItem(WalletItemKind.Ticket, o["description"].Value<string>());
                }
                else if (o["storeCard"] != null)
                {
                    pkPassType = "storeCard";
                    item = new WalletItem(WalletItemKind.MembershipCard, o["description"].Value<string>());
                }
                else if (o["generic"] != null)
                {
                    pkPassType = "generic";
                    item = new WalletItem(WalletItemKind.General, o["description"].Value<string>());
                }
                else throw new Exception();

                //Get images
                CurrentInfo = "Fetching images...";
                item.Logo99x99 = item.Logo159x159 = item.Logo336x336 = getImageFromName("icon.png", contents);
                item.HeaderBackgroundImage = getImageFromName("logo.png", contents);
                item.BodyBackgroundImage = getImageFromName("background.png", contents);

                CurrentInfo = "Fetching colors...";
                item.BodyColor = HelperMethods.getColorFromRGBString(o["backgroundColor"].Value<string>());
                item.BodyFontColor = HelperMethods.getColorFromRGBString(o["foregroundColor"].Value<string>());
                item.HeaderColor = item.BodyColor;
                item.HeaderFontColor = HelperMethods.getColorFromRGBString(o["labelColor"].Value<string>());

                int i = 0;
                if (o[pkPassType]["primaryFields"] != null)
                {
                    foreach (JObject jo in o[pkPassType]["primaryFields"])
                    {
                        WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
                        if (i == 0) prop.DetailViewPosition = WalletDetailViewPosition.PrimaryField1;
                        else if (i == 1) prop.DetailViewPosition = WalletDetailViewPosition.PrimaryField2;
                        else throw new InvalidDataException();
                        item.DisplayProperties[jo["key"].Value<string>()] = prop;
                        i++;
                    }
                }

                //Secondary fields
                i = 0;
                if (o[pkPassType]["secondaryFields"] != null)
                {
                    foreach (JObject jo in o[pkPassType]["secondaryFields"])
                    {
                        WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
                        if (i == 0) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField1;
                        else if (i == 1) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField2;
                        else if (i == 2) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField3;
                        else if (i == 3) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField4;
                        else if (i == 4) prop.DetailViewPosition = WalletDetailViewPosition.SecondaryField5;
                        else throw new InvalidDataException();
                        item.DisplayProperties[jo["key"].Value<string>()] = prop;
                        i++;
                    }
                }

                //Auxiliary fields
                i = 0;
                if (o[pkPassType]["auxiliaryFields"] != null)
                {
                    foreach (JObject jo in o[pkPassType]["auxiliaryFields"])
                    {
                        WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
                        if (i == 0) prop.DetailViewPosition = WalletDetailViewPosition.FooterField1;
                        else if (i == 1) prop.DetailViewPosition = WalletDetailViewPosition.FooterField2;
                        else if (i == 2) prop.DetailViewPosition = WalletDetailViewPosition.FooterField3;
                        else if (i == 3) prop.DetailViewPosition = WalletDetailViewPosition.FooterField4;
                        else throw new InvalidDataException();
                        item.DisplayProperties[jo["key"].Value<string>()] = prop;
                        i++;
                    }
                }

                //Header fields
                i = 0;
                if (o[pkPassType]["headerFields"] != null)
                {
                    foreach (JObject jo in o[pkPassType]["headerFields"])
                    {
                        WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
                        if (i == 0) prop.DetailViewPosition = WalletDetailViewPosition.HeaderField1;
                        else if (i == 1) prop.DetailViewPosition = WalletDetailViewPosition.HeaderField2;
                        else throw new InvalidDataException();
                        item.DisplayProperties[jo["key"].Value<string>()] = prop;
                        i++;
                    }
                }

                //Header fields
                if (o[pkPassType]["backFields"] != null)
                {
                    foreach (JObject jo in o[pkPassType]["backFields"])
                    {
                        WalletItemCustomProperty prop = new WalletItemCustomProperty(jo["label"].Value<string>(), jo["value"].Value<string>());
                        prop.SummaryViewPosition = WalletSummaryViewPosition.Hidden;
                        item.DisplayProperties[jo["key"].Value<string>()] = prop;
                    }
                }

                if (o["barcode"] != null)
                {
                    WalletBarcodeSymbology sym = new WalletBarcodeSymbology();
                    switch (o["barcode"]["format"].Value<string>())
                    {
                        case "PKBarcodeFormatQR": sym = WalletBarcodeSymbology.Qr; break;
                        case "PKBarcodeFormatPDF417": sym = WalletBarcodeSymbology.Pdf417; break;
                        case "PKBarcodeFormatAztec": sym = WalletBarcodeSymbology.Aztec; break;
                        default: throw new InvalidDataException();
                    }
                    item.Barcode = new WalletBarcode(sym, o["barcode"]["message"].Value<string>());
                }

                if (o["locations"] != null)
                {
                    i = 0;
                    foreach (JObject jo in o["locations"])
                    {
                        WalletRelevantLocation location = new WalletRelevantLocation();
                        if (jo["relevantText"] != null)
                        {
                            location.DisplayMessage = jo["relevantText"].Value<string>();
                        }
                        var position = new Windows.Devices.Geolocation.BasicGeoposition();
                        position.Latitude = jo["latitude"].Value<double>();
                        position.Longitude = jo["longitude"].Value<double>();
                        try
                        {
                            position.Altitude = jo["altitude"].Value<double>();
                        }
                        catch (Exception)
                        {
                            System.Diagnostics.Debug.WriteLine("An altitude does not exist for location " + location.DisplayMessage);
                        }
                        location.Position = position;
                        //Check one doesn't already exist.
                        if (item.RelevantLocations.Where(x => x.Key == i.ToString()).Count() > 0)
                            i++;
                        else
                            item.RelevantLocations.Add(i.ToString(), location);
                        i++;
                    }
                }

                if (o["relevantDate"] != null)
                {
                    item.RelevantDate = DateTime.Parse(o["relevantDate"].Value<string>());
                }

                if (o["expirationDate"] != null)
                {
                    item.ExpirationDate = DateTime.Parse(o["expirationDate"].Value<string>());
                }

                string cardId = o["serialNumber"].Value<string>();

                pkPassConversionResult result = new pkPassConversionResult();
                result.item = item; result.cardId = cardId;
                return result;
            }
            catch (Exception ex)
            {
                CurrentInfo = "Exception: " + ex.Message;
                return null;
            }
        }

        async Task<IReadOnlyList<Windows.Storage.StorageFile>> extractPKPASSFile(Windows.Storage.StorageFile source, Windows.Storage.StorageFolder destination)
        {
            await Task.Run(() => { ZipFile.ExtractToDirectory(source.Path.ToString(), destination.Path.ToString()); });
            return await Windows.Storage.ApplicationData.Current.TemporaryFolder.GetFilesAsync();
        }

        public async Task<pkPassConversionResult> createWalletItem()
        {
            try {
                Windows.Storage.StorageFolder tempFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;

                CurrentInfo = "Extracting the .PKPASS file...";
                contents = await extractPKPASSFile(sourceFile, tempFolder);
                pkPassConversionResult result = parsePkpassType(contents);
                return result;
            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message, "There was an error processing the file.");
                return null;
            }
        }

    }

    public class pkPassConversionResult
    {
        public WalletItem item;
        public string cardId;
    }
}
