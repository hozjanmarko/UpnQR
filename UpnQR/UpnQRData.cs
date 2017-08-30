using System;
using System.Collections.Generic;
using System.Text;

namespace UpnQR
{
    public class UpnQRData
    {
        protected const char LF = (char)10;
        protected const int MAX_ImePlacnika = 33;
        protected const int MAX_NaslovPlacnika = 33;
        protected const int MAX_KrajPlacnika = 33;
        protected const decimal MAX_ZnesekZaPlacilo = 100000000;
        protected const int MAX_KodaNamena = 4;
        protected const int MAX_NamenPlacila = 42;
        protected const int MAX_IbanPrejemnika = 34;
        protected const int MAX_ReferencaPrejemnika = 26;
        protected const int MAX_ImePrejemnika = 33;
        protected const int MAX_NaslovPrejemnika = 33;
        protected const int MAX_KrajPrejemnika = 33;
        protected const string CONST_UPNQR = "UPNQR";
        protected const int MIN_KONTROLNA_VSOTA = 100;
        protected const int MIN_DOLZINA_VSEBIN = 411;

        public string ImePlacnika { get; protected set; }
        public string NaslovPlacnika { get; protected set; }
        public string KrajPlacnika { get; protected set; }
        public decimal ZnesekZaPlacilo { get; protected set; } = 0;
        public string KodaNamena { get; protected set; }
        public string NamenPlacila { get; protected set; }
        public DateTime? RokPlacila { get; protected set; }
        public string IbanPrejemnika { get; protected set; }
        public string ReferencaPrejemnika { get; protected set; }
        public string ImePrejemnika { get; protected set; }
        public string NaslovPrejemnika { get; protected set; }
        public string KrajPrejemnika { get; protected set; }

        protected UpnQRData()
        {

        }

        public static UpnQRData CreateUpnQRData(
            string _ImePlacnika,
            string _NaslovPlacnika,
            string _KrajPlacnika,
            decimal _ZnesekZaPlacilo,
            string _KodaNamena,
            string _NamenPlacila,
            string _IbanPrejemnika,
            string _ReferencaPrejemnika,
            string _ImePrejemnika,
            string _NaslovPrejemnika,
            string _KrajPrejemnika,
            DateTime? _RokPlacila)
        {
            var errorList = new List<string>();

            if (String.IsNullOrWhiteSpace(_ImePlacnika))
                errorList.Add("Ime placnika je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_NaslovPlacnika))
                errorList.Add("Naslov placnika je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_KrajPlacnika))
                errorList.Add("Kraj lacnika je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_KodaNamena))
                errorList.Add("Koda namena je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_NamenPlacila))
                errorList.Add("Namen placila je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_IbanPrejemnika))
                errorList.Add("Iban prejemnika je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_ReferencaPrejemnika))
                errorList.Add("Referenca prejemnika je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_ImePrejemnika))
                errorList.Add("Ime prejemnika je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_NaslovPrejemnika))
                errorList.Add("Naslov prejemnika je obvezen podatek");
            if (String.IsNullOrWhiteSpace(_KrajPrejemnika))
                errorList.Add("Kraj prejemnika je obvezen podatek");

            if (errorList.Count > 0)
            {
                var _error = "";
                foreach (string err in errorList)
                {
                    _error += err + LF;
                }
                throw new ArgumentException(_error);
            }

            return new UpnQRData() {
                ImePlacnika = _ImePlacnika,
                NaslovPlacnika = _NaslovPlacnika,
                KrajPlacnika = _KrajPlacnika,
                ZnesekZaPlacilo = _ZnesekZaPlacilo,
                KodaNamena = _KodaNamena,
                NamenPlacila = _NamenPlacila,
                RokPlacila = _RokPlacila,
                IbanPrejemnika = _IbanPrejemnika,
                ReferencaPrejemnika = _ReferencaPrejemnika,
                ImePrejemnika = _ImePrejemnika,
                NaslovPrejemnika = _NaslovPrejemnika,
                KrajPrejemnika = _KrajPrejemnika
            };
        }

        public string GetQrDataString()
        {
            var strZnesek = "00000000000" + ((int)(Math.Round(ZnesekZaPlacilo, 2) * 100)).ToString();
            strZnesek = strZnesek.Substring(strZnesek.Length - 11);

            var tempQr = CONST_UPNQR + LF + LF + LF + LF + LF;
            //6
            tempQr += ImePlacnika + LF;
            //7
            tempQr += NaslovPlacnika + LF;
            //8
            tempQr += KrajPlacnika + LF;
            //9
            tempQr += strZnesek + LF;
            //10
            tempQr += LF;
            //11
            tempQr += LF;
            //12
            tempQr += KodaNamena + LF;
            //13
            tempQr += NamenPlacila + LF;
            //14
            tempQr += (RokPlacila != null ? String.Format("{0:dd.MM.yyyy}", RokPlacila) : "") + LF;
            //15
            tempQr += IbanPrejemnika + LF;
            //16
            tempQr += ReferencaPrejemnika + LF;
            //17
            tempQr += ImePrejemnika + LF;
            //18
            tempQr += NaslovPrejemnika + LF;
            //19
            tempQr += KrajPrejemnika + LF;
            //20
            tempQr += (tempQr.Length < MIN_KONTROLNA_VSOTA ? "0" : "") + tempQr.Length.ToString() + LF;

            var qrLen = MIN_DOLZINA_VSEBIN - tempQr.Length;
            if (qrLen > 0)
            {
                for (int i = 0; i < qrLen; i++)
                    tempQr += " ";
            }

            return EncodeFromUtf8ToIso88592(tempQr);
        }

        protected string EncodeFromUtf8ToIso88592(string _input)
        {
            Encoding iso = Encoding.GetEncoding("ISO-8859-2");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(_input);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            return iso.GetString(isoBytes);
        }
    }
}
