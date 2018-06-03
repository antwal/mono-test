using System;
using System.Net;
using System.Net.Http;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using ServiceStack;
using ServiceStack.Text;

namespace mono_test
{
    public partial class monotest : Form
    {
		/// <summary>
		/// Github Latest version API Url
		/// </summary>
		string VersionAPI = "https://api.github.com/repos/antwal/mono-test/releases/latest";

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
        [STAThread]
        static void Main(string[] args)
        {			
			//AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler (AssemblyResolve);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler (AssemblyResolve);

            monotest app = new monotest();
            
            Application.Run(app);
        }
        
        public monotest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ons the form load.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
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

		/// <summary>
		/// Github API Call
		/// </summary>
		/// <param name="url">github api url</param>
		/// <returns>json content</returns>
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

		/// <summary>
		/// Handler Assemblies Resolve
		/// </summary>
		/// <returns>The resolve.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		/// <returns>>assembly data</returns>
		static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
		{
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream ("mono-test.ServiceStack.Text.dll"))
			{
				byte[] assemblyData = new byte[stream.Length];
				stream.Read (assemblyData, 0, assemblyData.Length);
				return Assembly.Load (assemblyData);
			}
		}
    }
}