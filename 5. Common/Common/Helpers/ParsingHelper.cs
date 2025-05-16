using Amazon;
using Common.Exceptions;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace Common.Helpers
{
    public static class ParsingHelper
    {
        public static int ParseInt(this string value)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            throw new InvalidArgumentException();
        }

        public static string TranslateToJapanese(this string value)
        {
            string outValue = string.Empty;

            IConfigurationRoot languageConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("japaneseConfigure.json")
                .Build();

            var japaneseArray = languageConfiguration.GetSection("JapaneseConfig").GetChildren();
            foreach(ConfigurationSection section in japaneseArray)
            {
                if(value == section.Key)
                {
                    outValue = section.Value;
                    break;
                }    
            }    

            return outValue;
        }

        public static string GenerateQRCode(string data)
        {
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return Convert.ToBase64String(BitmapToBytesCode(qrCodeImage));
        }

        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public static RegionEndpoint ToAWSRegion (this string value)
        {
            switch (value)
            {
                case "ap-northeast-1":
                    return RegionEndpoint.APNortheast1;
                case "us-east-2":
                    return RegionEndpoint.USEast2;
                default:
                    return RegionEndpoint.APNortheast1;
            }
        }
    }
}
