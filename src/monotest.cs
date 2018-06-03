using System;
using System.Net;
using System.Net.Http;
using System.Drawing;
using System.Windows.Forms;

using ServiceStack;
using ServiceStack.Text;

namespace mono_test
{
    public partial class monotest : Form
    {
		string VersionAPI = "https://api.github.com/repos/antwal/mono-test/releases/latest";

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            monotest app = new monotest();
            
            Application.Run(app);
        }
        
        public monotest()
        {
            InitializeComponent();
        }
        
        private void onForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("onForm Load");

			var jsonVersion = JsonObject.Parse (GitHubAPI (VersionAPI));

			string versionNew = jsonVersion.Get<string> ("tag_name").ToString ();
			string versionLink = jsonVersion.Get<string> ("zipball_url").ToString ();

			Console.Out.WriteLine ("Latest Version: {0}", versionNew);
			Console.Out.WriteLine ("Download URL: {0}", versionLink);
        }

        private void onExit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Exit Click");
        }

		string GitHubAPI(string url)
		{
			var httpClient = new HttpClient ();

			httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/json");
			httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "mono-test");

			var response = httpClient.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			using (var responseContent = response.Content) {
				string jsonContent = responseContent.ReadAsStringAsync ().Result;
				return jsonContent;
			}				
		}
    }
}