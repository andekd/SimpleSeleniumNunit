using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitSimple
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using System;

    [Ignore("Only for advanced tests")]
    [TestFixture]
    public class GmailTests
    {
        private IWebDriver driver;

        public GmailTests() { }

        [SetUp]
        public void LoadDriver()
        {
            Console.WriteLine("SetUp");
            driver = new FirefoxDriver();
        }

        [Test]
        public void Login()
        {
            Console.WriteLine("Test");

            driver.Navigate().GoToUrl("http://gmail.com");
            driver.FindElement(By.Id("Email"), 10).SendKeys("contactortest");
            driver.FindElement(By.Id("next"), 10).Submit();
            driver.FindElement(By.Id("Passwd"), 10).SendKeys("contactortest#1");
            driver.FindElement(By.Id("signIn"), 10).Submit();

            Assert.True(driver.Title.Contains("Inbox"));
        }

        [TearDown]
        public void UnloadDriver()
        {
            Console.WriteLine("TearDown");
            //driver.Quit();
        }
    }


    [TestFixture, Description("Tests Google Search with String data")]
    public class GoogleTests
    {
        private IWebDriver driver;

        public GoogleTests() { }

        [SetUp]
        public void LoadDriver()
        {
            driver = new FirefoxDriver();
            // getting dummy testdata file to check how a testdata folder is handled in jenkins
            string plainFileName = "dummy1.txt";
            string execPath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine("execPath " + execPath);
            string nameSpacePath = System.IO.Directory.GetParent(System.IO.Directory.GetParent(execPath).FullName).FullName;
            Console.WriteLine("nameSpacePath " + nameSpacePath);
            string testDataPath = nameSpacePath + @"\TestData\";
            string fullFileName = testDataPath + plainFileName;
            Console.WriteLine("fullFileName " + fullFileName);
            //using (System.IO.StreamReader file = new System.IO.StreamReader(fullFileName))
            //{
            //string data = file.ReadToEnd();
            //Console.WriteLine("Content of dummy file: " + data);
            //};
        }
        [TestCase("Google", "Google")]   // searchString = Google
        [TestCase("Bing", "Bing")]     // searchString = Bing
        public void Search(string searchString, string result)
        {
            // execute Search twice with testdata: Google, Bing

            driver.Navigate().GoToUrl("http://google.com");
            IWebElement searchField = driver.FindElement(By.Name("q"), 10);
            searchField.SendKeys(searchString);
            searchField.SendKeys(Keys.Enter);

            driver.FindElement(By.Id("resultStats"), 10);

            string title = driver.Title;
            Console.WriteLine("Title: " + title);
            Console.WriteLine("result: " + result);

            Assert.True(driver.Title.Contains(result));
        }

        [TearDown]
        public void UnloadDriver()
        {
            driver.Quit();
            string savedir = Environment.GetEnvironmentVariable("CrmTestsDir");
            Console.WriteLine("value of env CrmTestsDir: " + savedir);
            String fullFileName = savedir + "\\full.txt";
            String wildFileName = savedir + "\\wild.txt";
            String dummyFileName = savedir + "\\dummy13.txt";
            var date = DateTime.Now;
            int theMinute = date.Minute * 10;
            int theSec = date.Second * 10;
            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullFileName))
            {
                //String line = "YVALUE=" + theMinute + System.Environment.NewLine + "URL=http://foo.bar/";
                String line = "YVALUE=" + theMinute + ",25";
                file.WriteLine(line);
                file.Close();
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(wildFileName))
            {
                String line = "YVALUE=" + theSec + ",25";
                file.WriteLine(line);
                file.Close();
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(dummyFileName))
            {
                String line = "Your dummy text" + theMinute + theSec;
                file.WriteLine(line);
                file.Close();
            }

        }
    }
}

