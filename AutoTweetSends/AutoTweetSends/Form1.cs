using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTweetSends
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
        string sendTextLanguage, textboxLanguage;
        private void button1_Click(object sender, EventArgs e)
        {
            string UserUsName = textBox1.Text;
            string UserPass = textBox2.Text;
            if (radioButton1.Checked == true)
            {
                textboxLanguage = "What is happening?!";
                sendTextLanguage = "Tweet";

            }
            if (radioButton2.Checked == true)
            {
                textboxLanguage = "Neler oluyor?";
                sendTextLanguage = "Gönder";

            }
            int how_many_twits = Convert.ToInt32(textBox3.Text);
            string tweetText = richTextBox1.Text;
            if (tweetText.Length > 1 && tweetText.Length < 301 && how_many_twits > 0 && UserUsName.Length > 0 && UserPass.Length > 0)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://twitter.com/i/flow/login");
                System.Threading.Thread.Sleep(10000);
                IWebElement username = driver.FindElement(By.CssSelector("input[name='text']"));
                username.Click();
                System.Threading.Thread.Sleep(10000);
                username.SendKeys(UserUsName);
                System.Threading.Thread.Sleep(100);
                SendKeys.Send("{ENTER}");
                System.Threading.Thread.Sleep(2000);
                Clipboard.SetText(UserPass);
                SendKeys.Send("^v");
                System.Threading.Thread.Sleep(100);
                SendKeys.Send("{ENTER}");
                System.Threading.Thread.Sleep(10000);
                IWebElement tweetClickLocator = driver.FindElement(By.XPath("//span[contains(text(), '" + textboxLanguage + "')]"));
                By tweetButtonClickLocator = By.XPath("//div[@dir='ltr']/span/span[contains(text(), '" + sendTextLanguage + "')]");
                for (int i = 1; i <= how_many_twits; i++)
                {
                    tweetClickLocator.Click();
                    System.Threading.Thread.Sleep(500);
                    SendKeys.Send(tweetText + " " + i);
                    System.Threading.Thread.Sleep(500);
                    IWebElement tweetClick = driver.FindElement(tweetButtonClickLocator);
                    tweetClick.Click();
                    System.Threading.Thread.Sleep(7000);
                }
            }
            else
            {
                MessageBox.Show("Please make sure you have not left any blank fields.");
            }
        }
    }
}
