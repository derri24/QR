using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using QRCoder;
using Button = Android.Widget.Button;



namespace QR_code
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var generateQrBtn = (Button) FindViewById(Resource.Id.generateQrBtn);
            if (generateQrBtn != null)
                generateQrBtn.Click += delegate { GenerateQrBtn_Click(); };
            
            var scanBtn = (Button) FindViewById(Resource.Id.scanBtn);
            if (scanBtn != null)
                scanBtn.Click += delegate { ScanBtn_Click();};
        }

        private void GenerateQr(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);

            var imageView = (ImageView) FindViewById(Resource.Id.qrImage);

            using (var ms = new MemoryStream(qrCodeBytes))
            {
                if (imageView != null)
                    imageView.SetImageDrawable(new BitmapDrawable(ms));
            }
        }

        private void GenerateQrBtn_Click()
        {
            var data = ((EditText) FindViewById(Resource.Id.data))?.Text;
            GenerateQr(data);
        }

        private void ScanBtn_Click()
        {
            Intent intent = new Intent(this, typeof(ScanActivity));
            StartActivity(intent);
        }
    }
}