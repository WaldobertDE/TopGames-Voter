using System.Diagnostics;
using TopGames_Voter.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TopGames_Voter
{
    public partial class Form1 : Form
    {

        DateTime lastVoteTime = Settings.Default.lastVoteTime;
        string server = Settings.Default.server;
       
        public Form1()
        {
            InitializeComponent();
            Icon = Resources.Icon1;
        }

        private void InitializeNotifyIcon()//Setup of NotifyIcon
        {
            niTrayIcon.Text = "Voter";
            niTrayIcon.Icon = Resources.Icon1;
            niTrayIcon.Visible = true;
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("Settings", null, SettingsMenu_Click);
            cms.Items.Add("Exit", null, Exit_Click);
            niTrayIcon.ContextMenuStrip = cms;
        }

        private void SaveData()//Saves user data
        {
            Settings.Default.lastVoteTime = lastVoteTime;
            Settings.Default.server = tbServerUrl.Text;
            Settings.Default.Save();
        }

        void Exit_Click(object sender, EventArgs e)//Exit button on NotifyIcon
        {
            niTrayIcon.Dispose();
            this.Close();
        }

        void SettingsMenu_Click(object sender, EventArgs e)//Settings button on NotifyIcon
        {
            Show();
            ShowInTaskbar = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeNotifyIcon();
            tbServerUrl.Text = server;
            StartupCheck();
        }

        private void StartupCheck()
        {
            CheckAndGetDriver();

            if (!CheckUrl(tbServerUrl.Text))
            {
                niTrayIcon.Text = "Enter a top-games.net url.";
                MessageBox.Show("Enter a top-games.net url.\nE.g.: https://top-games.net/dayz/vote/ServerName");
            }
            else
            {

                if (lastVoteTime == DateTime.MinValue)
                {
                    Start();
                }
                else
                {
                    var timeSinceLastVote = DateTime.Now - lastVoteTime;

                    if (timeSinceLastVote.TotalMilliseconds > 7500000)
                    {
                        Start();
                    }
                    else
                    {
                        var remaining = (int)(TimeSpan.FromMilliseconds(7500000) - timeSinceLastVote).TotalMilliseconds;

                        Debug.WriteLine($"Top-Games-Voter: Next vote in: {remaining}ms. ({DateTime.Now + TimeSpan.FromMilliseconds(remaining)})");
                        niTrayIcon.Text = $"Next vote: {DateTime.Now + TimeSpan.FromMilliseconds(remaining)}";
                        Start(remaining);
                    }
                }

            }
        }
        
        private bool CheckUrl(string url)//Check if its a valid top-games url
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (url.Contains("top-games.net") && url.Contains("vote") && result == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnCloseToTray_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Hide();
            ShowInTaskbar = false;
            StartupCheck();
        }

        private void Form1_Shown(object sender, EventArgs e)//Checks if program already set up
        {
            if (lastVoteTime != DateTime.MinValue) Hide();
        }

        private void Start(int delay = 1)
        {
            Debug.WriteLine($"Top-Games-Voter: Timer Started! Delay: {delay}ms");
            timerVote.Interval = delay;
            timerVote.Enabled = true;
            timerVote.Start();
        }

        private void timerVote_Tick(object sender, EventArgs e)
        {
            timerVote.Stop();
            timerVote.Enabled = false;
            Debug.WriteLine("Top-Games-Voter: Timer Elapsed!");
            Vote();
        }

        private void PrintException(Exception ex)
        {
            Debug.WriteLine("Top-Games-Voter: Exception!");
            Debug.WriteLine("Source:");
            Debug.WriteLine(ex.Source);
            Debug.WriteLine("-----------------------------------");
            Debug.WriteLine("Message:");
            Debug.WriteLine(ex.Message);
            Debug.WriteLine("-----------------------------------");
            Debug.WriteLine("Type:");
            Debug.WriteLine(ex.GetType());
            Debug.WriteLine("-----------------------------------");
            Debug.WriteLine("Data:");
            Debug.WriteLine(ex.Data.Count);
            foreach (var item in ex.Data)
            {
                Debug.WriteLine(item);
            }
            Debug.WriteLine("-----------------------------------");
        }

        private void CheckAndGetDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
        }

        private void Vote()
        {
            //XPaths to find the elements.
            string cookieButtonXPath = "//button[contains(@class,'fc-button fc-cta-consent')]//p[1]";
            string yesButtonCSS = "html>body>div:nth-of-type(6)>div:nth-of-type(2)>div:nth-of-type(2)>div>div>form>div:nth-of-type(3)>div:nth-of-type(2)>div>button";
            string timeRemainingXPath = "//div[contains(@class,'alert alert-warning')]//strong[1]";


            EdgeOptions edgeOptions = new EdgeOptions();
            //edgeOptions.AddArgument("--headless");
            //edgeOptions.AddArgument("--disable-gpu");
            var edgeService = EdgeDriverService.CreateDefaultService();
            edgeService.HideCommandPromptWindow = true;
            var edgeDriver = new EdgeDriver(edgeService, edgeOptions);
            WebDriverWait wait = new WebDriverWait(edgeDriver, TimeSpan.FromSeconds(10));

            try
            {
                Debug.WriteLine("Goto URL");
                edgeDriver.Navigate().GoToUrl(Settings.Default.server);
                Debug.WriteLine("Clicking Cookies");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(cookieButtonXPath))).Click();
                Debug.WriteLine("SendKeys Vote");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(yesButtonCSS))).SendKeys(OpenQA.Selenium.Keys.Enter);

                Debug.WriteLine("Top-Games-Voter: Voted!");

                lastVoteTime = DateTime.Now;
                StartupCheck();
            }
            catch (Exception ex)
            {
                PrintException(ex);
                var waitTimeSite = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(timeRemainingXPath))).Text;
                Debug.WriteLine($"Site tells to wait {waitTimeSite}!");
            }

            SaveData();
            edgeDriver.Quit();

        }

    }
}