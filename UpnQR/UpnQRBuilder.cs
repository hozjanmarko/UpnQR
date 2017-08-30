using System;
using System.Drawing;
using ZXing;
using ZXing.CoreCompat.Rendering;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace UpnQR
{
    public static class UpnQRBuilder
    {
        const string QR_ENCODING = "ISO-8859-2";
        const int QR_VERSION = 15;

        public static Bitmap GenerateUpnQR(UpnQRData qRData)
        {
            if (qRData == null)
                throw new ArgumentException("QrData niz ni določen");

            var barcodeWriter = new BarcodeWriter<Bitmap>();

            //barcodeWriter.Renderer = new IBarcodeRenderer<Bitmap>()

            barcodeWriter.Format = BarcodeFormat.QR_CODE; 

            barcodeWriter.Options = new QrCodeEncodingOptions()
            {
                DisableECI = false,
                CharacterSet = QR_ENCODING,
                QrVersion = QR_VERSION,
                Height = 300,
                Width = 300,
                ErrorCorrection = ErrorCorrectionLevel.M
            };

            barcodeWriter.Renderer = new BitmapRenderer();


            return barcodeWriter.Write(qRData.GetQrDataString());
        }
    }
}
