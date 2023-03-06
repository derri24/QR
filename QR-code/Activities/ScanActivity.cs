using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using ZXing.Mobile;

namespace QR_code
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class ScanActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_scan);

            var generateBtn = (Button) FindViewById(Resource.Id.generateBtn);
            if (generateBtn != null)
                generateBtn.Click += delegate { GenerateBtn_Click(); };

            var scanQrBtn = (Button) FindViewById(Resource.Id.scanQrBtn);
            if (scanQrBtn != null)
                scanQrBtn.Click += async delegate { await ScanQrBtn_Click(); };
        }

        private async Task ScanQrBtn_Click()
        {
            var textView = (TextView) FindViewById(Resource.Id.result);
            MobileBarcodeScanner.Initialize(Application);
            var scanner = new MobileBarcodeScanner();

            var result = await scanner.Scan();
            if (result != null && textView != null)
            {
                textView.Text = result.Text;
            }
        }

        private void GenerateBtn_Click()
        {
            FinishActivity(0);
        }
    }
}