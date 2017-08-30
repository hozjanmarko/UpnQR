using System;
using System.Text;
using UpnQR;

namespace QrCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //potrebno zaradi kodiranj
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //Navidezni podatki
            var qrdata = UpnQRData.CreateUpnQRData(
                "UTROŠA MARJAN",
                "VELENJSKA CESTA 8/B",
                "LENDAVA",
                38.76m,
                "NOWS",
                "Komunalne storitve 05/2017",
                "SI56555555554489785",
                "SI124597854236541",
                "Podjetje1",
                "nekje ob ledavi 6",
                "Selgrad",
                new DateTime(2017,6,30)
                );

            var qrImage = UpnQRBuilder.GenerateUpnQR(qrdata);

            qrImage.Save("testQr.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
