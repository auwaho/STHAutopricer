using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace STH_Autopricer
{
    public partial class Form1 : Form
    {
        IWebDriver chrome;

        public Form1()
        {
            InitializeComponent();

            if (Directory.Exists(@"\STH_v2_min")) { } else
            {
                string zipPath = Application.StartupPath + @"\STH_v2_min.zip";
                string extractPath = @"\STH_v2_min";

                ZipFile.ExtractToDirectory(zipPath, extractPath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true; //Скрываем консоль (если хром скрыт, то лучше оставить)
            var options = new ChromeOptions();
            options.AddArguments("start-maximized", "--disable-infobars", "--pinned-tab-count=x", "--load-extension=" + @"\STH_v2_min");
            //options.AddExtension(Application.StartupPath + @"\" + "sth.crx"); //для старых версий стх
            chrome = new ChromeDriver(service, options);
            chrome.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            //Подгружаем настройки приложения
            AutologinCheckBox.Checked = Properties.Settings.Default.sth_autologin;
            EmailTextBox.Text  = Properties.Settings.Default.sth_email;
            PasswordTextBox.Text = Properties.Settings.Default.sth_password;

            MinProfitNUD.Value = Properties.Settings.Default.SettingMinProfitNUD;
            MaxProfitNUD.Value = Properties.Settings.Default.SettingMaxProfitNUD;
            RegressionValue.Value  = Properties.Settings.Default.SettingRegressionValue;
            OrderQuantityNUD.Value = Properties.Settings.Default.SettingOrderQuantityNUD;
            OrderMaxPriceNUD.Value = Properties.Settings.Default.SettingOrderMaxPriceNUD;
            ProxyCount.Value = Properties.Settings.Default.SettingProxyCount;

            LinearRegression.Checked = Properties.Settings.Default.SettingLinearRegression;
            QuantityCheckBox.Checked = Properties.Settings.Default.SettingQuantityCheckBox;
            MaxOrderPriceCheckBox.Checked = Properties.Settings.Default.SettingMaxOrderPriceCheckBox;
            CheckOnlySellPrice.Checked = Properties.Settings.Default.SettingCheckOnlySellPrice;
            ChangeOnlyUp.Checked = Properties.Settings.Default.SettingChangeOnlyUp;
            ProxyCheck.Checked = Properties.Settings.Default.SettingProxyCheck;

            if (AutologinCheckBox.Checked == true)
            {
                try
                {
                    chrome.Navigate().GoToUrl("http://steamtraderhelper.com/user/account/login/");

                    IWebElement email = chrome.FindElement(By.Id("LoginForm_email"));
                    email.Clear();
                    email.SendKeys(EmailTextBox.Text);

                    IWebElement password = chrome.FindElement(By.Id("LoginForm_password"));
                    password.Clear();
                    password.SendKeys(PasswordTextBox.Text);
                    password.SendKeys(OpenQA.Selenium.Keys.Enter);

                    System.Threading.Thread.Sleep(1000);
                    chrome.Navigate().GoToUrl("chrome-extension://lekekhdkpekmbgpcanfldeekpjbdieic/page/index.html");
                }
                catch { chrome.Navigate().GoToUrl("chrome-extension://lekekhdkpekmbgpcanfldeekpjbdieic/page/index.html"); }
            }
            else
            {
                chrome.Navigate().GoToUrl("chrome-extension://lekekhdkpekmbgpcanfldeekpjbdieic/page/index.html");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            chrome.Quit();
            Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
            foreach (var chromeDriverProcess in chromeDriverProcesses) { chromeDriverProcess.Kill(); }
        }

        //Для поиска между строк
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        //////////////////////////////////////////Собственно сам процесс
        private void BGAutoprice_DoWork(object sender, DoWorkEventArgs e)
        {
            //Дата ровно месяц назад для оптимизации
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            DateTime monthago = DateTime.Now.AddMonths(-1);
            var fromwhereineedtoparseprices = monthago.ToString("MMM dd yyyy");
            Console.Write(fromwhereineedtoparseprices);

            HttpClient client = new HttpClient();

            //Прокси

            List<string> proxys = new List<string>();

            if (ProxyCheck.Checked == true)
            {
                string sourceHTML = client.GetStringAsync("https://www.proxy-list.download/api/v1/get?type=http").Result;
                List<string> allproxys = sourceHTML.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                proxys = allproxys.GetRange(0, Decimal.ToInt16(ProxyCount.Value));

                //System.IO.File.WriteAllLines(@"/IPs.txt", proxys);
            }

            bool proxy = false;
            
            IWebElement group = chrome.FindElement(By.ClassName("t-s"));
            List<IWebElement> items = group.FindElements(By.CssSelector("tr[class*='item']")).ToList();

            foreach (IWebElement item in items)
            {
            restart:
                if (BGAutoprice.CancellationPending) return;

                bool success = false;
                int retry = 0;

                //Зацикливаем с паузой при микробане
                while (!success && retry < (proxys.Count + 5))
                {
                    try
                    {
                        double budget = 0;
                        decimal quantity = 0;
                        string volume = "";

                        //Берем дивец предмета, получаем его название и айдишник игры
                        IWebElement itemdiv = item.FindElement(By.ClassName("item_name"));
                        string gameid = GetBetween(itemdiv.GetAttribute("href"), "listings/", "/");
                        string itemnamehtml = itemdiv.GetAttribute("href").Substring(itemdiv.GetAttribute("href").LastIndexOf('/') + 1);

                        //Получаем историю покупок
                        string itempagesource = client.GetStringAsync(itemdiv.GetAttribute("href")).Result;
                        string salehistory = GetBetween(itempagesource, fromwhereineedtoparseprices, "];");
                        string itemnameid = GetBetween(itempagesource, "Market_LoadOrderSpread( ", " );");

                        if (CheckOnlySellPrice.Checked == false)
                        {
                            //Получаем суточный оборот и определяем кол-во в одном ордере
                            string jsonpriceoverview = client.GetStringAsync("http://steamcommunity.com/market/priceoverview/?currency=1&appid=" + gameid + "&market_hash_name=" + itemnamehtml).Result;
                            JObject priceoveriewdetails = JObject.Parse(jsonpriceoverview);
                            volume = string.Concat("" + priceoveriewdetails["volume"]).Replace(",", "");
                            double qnt = Double.Parse(volume) * (double)OrderQuantityNUD.Value / 100;
                            quantity = Math.Round(System.Convert.ToDecimal(qnt));
                        }

                        //Определяем цену продажи
                        string[] parts = salehistory.Split('[');
                        List<double> prices = new List<double>();
                        foreach (string part in parts)
                        {
                            double price = Convert.ToDouble(GetBetween(part, ",", ","), CultureInfo.InvariantCulture);
                            prices.Add(price);
                        }
                        var lastsales = prices.Skip(Math.Max(0, prices.Count() - 24)).OrderBy(d => d).Take(22);
                        var peaksales = lastsales.Skip(Math.Max(0, lastsales.Count() - 3));
                        double averageprice = peaksales.Sum(x => x) / peaksales.Count();
                        averageprice = Math.Round(averageprice, 2, MidpointRounding.AwayFromZero);

                        if (CheckOnlySellPrice.Checked == false)
                        {
                            //Получаем список ордеров и определяем самый актуальный (мозгоебка)
                            string jsonitemordershistogram = client.GetStringAsync("http://steamcommunity.com/market/itemordershistogram?country=US&language=english&currency=1&item_nameid=" + itemnameid + "&two_factor=0).Result").Result;
                            JObject ordershistogramdetails = JObject.Parse(jsonitemordershistogram);
                            var buyordergraph = ordershistogramdetails["buy_order_graph"];

                            bool succes2 = false;

                            for (int i = (int)MaxProfitNUD.Value; i >= (int)MinProfitNUD.Value; i--)
                            {
                                if (LinearRegression.Checked == true)
                                {
                                    budget = (averageprice * 0.87) - (averageprice * 0.87 * (i + (averageprice * (double)RegressionValue.Value)) / 100);
                                }
                                else
                                {
                                    budget = (averageprice * 0.87) - (averageprice * 0.87 * i / 100);
                                }
                                
                                budget = Math.Round(budget, 2, MidpointRounding.AwayFromZero);

                                foreach (var order in buyordergraph.Reverse())
                                {
                                    double orderprice = order[0].Value<double>();
                                    int qntinorder = order[1].Value<int>();

                                    if (orderprice >= budget)
                                    {
                                        if (qntinorder < (Double.Parse(volume) * 2))
                                        {
                                            succes2 = true;
                                        }

                                        break;
                                    }

                                    else if (orderprice < budget && qntinorder < (Double.Parse(volume) * 2))
                                    {
                                        succes2 = true;
                                        break;
                                    }
                                }

                                if (succes2 == true)
                                {
                                    //budget += 0.01;
                                    break;
                                }
                            }
                        }

                        StatusDot.ForeColor = System.Drawing.Color.Green;

                        //Пихаем все данные в СТХ
                        if (CheckOnlySellPrice.Checked == false)
                        {
                            IWebElement sthbuyprice = item.FindElement(By.CssSelector("input[class='inp b_Budget']"));
                            sthbuyprice.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                            sthbuyprice.SendKeys(budget.ToString());
                        }

                        if (ChangeOnlyUp.Checked == false)
                        {
                            IWebElement sthsellprice = item.FindElement(By.CssSelector("input[class*='inp inpLeftTab s_Pays']"));
                            sthsellprice.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                            sthsellprice.SendKeys(averageprice.ToString());
                        }
                        else
                        {
                            IWebElement sthsellprice = item.FindElement(By.CssSelector("input[class*='inp inpLeftTab s_Pays']"));
                            sthsellprice.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                            double currentprice = System.Convert.ToDouble(item.FindElement(By.CssSelector("input[class*='inp inpLeftTab s_Pays']")).GetAttribute("value"));

                            if (currentprice < averageprice)
                            {
                                sthsellprice.SendKeys(averageprice.ToString());
                            }
                        }

                        if (QuantityCheckBox.Checked == true)
                        {
                            if (MaxOrderPriceCheckBox.Checked == true)
                            {
                                while (budget * decimal.ToDouble(quantity) > decimal.ToDouble(OrderMaxPriceNUD.Value))
                                {
                                    quantity -= 1;
                                }
                            }

                            IWebElement sthquantity = item.FindElement(By.CssSelector("input[class='inp b_Count']"));
                            sthquantity.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                            sthquantity.SendKeys(quantity.ToString());
                        }

                        success = true;
                    }

                    catch (Exception)
                    {
                        if (BGAutoprice.CancellationPending)
                        {
                            e.Cancel = true;
                        }

                        StatusDot.ForeColor = System.Drawing.Color.Orange;

                        if (ProxyCheck.Checked == true)
                        {
                            if (retry == 0)
                            {
                                if (proxy == false)
                                {
                                    HttpClientHandler handler = new HttpClientHandler()
                                    {
                                        Proxy = new WebProxy(proxys[retry]),
                                        UseProxy = true,
                                    };

                                    client = new HttpClient(handler);
                                    proxy = true;
                                }
                                else
                                {
                                    client = new HttpClient();
                                    proxy = false;
                                }
                            }
                            else if (retry < proxys.Count)
                            {
                                HttpClientHandler handler = new HttpClientHandler()
                                {
                                    Proxy = new WebProxy(proxys[retry]),
                                    UseProxy = true,
                                };

                                client = new HttpClient(handler);
                                proxy = true;
                            }
                            else
                            {
                                client = new HttpClient();
                                proxy = false;

                                for (int i = 0; i < 30; i++)
                                {
                                    if (BGAutoprice.CancellationPending) return;
                                    System.Threading.Thread.Sleep(1000 * retry);
                                }
                            }
                        }

                        else
                        {
                            for (int i = 0; i < 30; i++)
                            {
                                if (BGAutoprice.CancellationPending) return;
                                System.Threading.Thread.Sleep(1000 * retry);
                            }
                        }

                        retry++;

                    }
                }

                if (success == false)
                {

                    DialogResult dialogResult = MessageBox.Show("Ты словил микробан. Попробовать снова?", "Неудача", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        StatusDot.ForeColor = System.Drawing.Color.Green;
                        goto restart;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        try { ParsePricesButton.Text = "Обновить список"; } catch { }
                        StatusDot.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                }

                //StatusDot.ForeColor = System.Drawing.Color.Green;
            }

            try { ParsePricesButton.Text = "Обновить список"; } catch { }
            StatusDot.ForeColor = System.Drawing.Color.Red;
            MessageBox.Show("Дело сделано :)");
        }

        private void ParsePricesButton_Click(object sender, EventArgs e)
        {
            if (ParsePricesButton.Text == "Обновить список")
            {
                BGAutoprice.RunWorkerAsync();
                StatusDot.ForeColor = System.Drawing.Color.Green;
                ParsePricesButton.Text = "Отменить";
            }
            else
            {
                BGAutoprice.CancelAsync();
                StatusDot.ForeColor = System.Drawing.Color.Red;
                ParsePricesButton.Text = "Обновить список";
            }
        }

        private void MinProfitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (MinProfitNUD.Value > MaxProfitNUD.Value)
            {
                MinProfitNUD.Value = MaxProfitNUD.Value;
            }
            Properties.Settings.Default.SettingMinProfitNUD = MinProfitNUD.Value;
        }

        private void MaxProfitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (MaxProfitNUD.Value < MinProfitNUD.Value)
            {
                MaxProfitNUD.Value = MinProfitNUD.Value;
            }
            Properties.Settings.Default.SettingMaxProfitNUD = MaxProfitNUD.Value;
        }

        private void CheckOnlySellPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckOnlySellPrice.Checked == true)
            {
                label1.Enabled = false;
                MinProfitNUD.Enabled = false;
                MaxProfitNUD.Enabled = false;
                QuantityCheckBox.Enabled = false;
                QuantityCheckBox.Checked = false;
                MaxOrderPriceCheckBox.Checked = false;
                ChangeOnlyUp.Enabled = true;
                ChangeOnlyUp.Checked = false;
                LinearRegression.Enabled = false;
                RegressionValue.Enabled = false;
            }
            else
            {
                label1.Enabled = true;
                MinProfitNUD.Enabled = true;
                MaxProfitNUD.Enabled = true;
                QuantityCheckBox.Enabled = true;
                ChangeOnlyUp.Enabled = false;
                ChangeOnlyUp.Checked = false;
                LinearRegression.Enabled = true;
                RegressionValue.Enabled = true;
            }
            Properties.Settings.Default.SettingCheckOnlySellPrice = CheckOnlySellPrice.Checked;
        }

        private void QuantityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (QuantityCheckBox.Checked == true)
            {
                OrderQuantityNUD.Enabled = true;
                MaxOrderPriceCheckBox.Enabled = true;
            }
            else
            {
                OrderQuantityNUD.Enabled = false;
                OrderMaxPriceNUD.Enabled = false;
                MaxOrderPriceCheckBox.Enabled = false;
                MaxOrderPriceCheckBox.Checked = false;
            }
            Properties.Settings.Default.SettingQuantityCheckBox = QuantityCheckBox.Checked;
        }

        private void MaxOrderPriceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MaxOrderPriceCheckBox.Checked == true)
            {
                OrderMaxPriceNUD.Enabled = true;
            }
            else
            {
                OrderMaxPriceNUD.Enabled = false;
            }
            Properties.Settings.Default.SettingMaxOrderPriceCheckBox = MaxOrderPriceCheckBox.Checked;
        }

        private void LinearRegression_CheckedChanged(object sender, EventArgs e)
        {
            if (LinearRegression.Checked == true)
            {
                RegressionValue.Enabled = true;
            }
            else
            {
                RegressionValue.Enabled = false;
            }
            Properties.Settings.Default.SettingLinearRegression = LinearRegression.Checked;
        }

        private void AutologinBox_CheckedChanged(object sender, EventArgs e)
        {
            if(AutologinCheckBox.Checked == true)
            {
                Properties.Settings.Default.sth_autologin = AutologinCheckBox.Checked;

            }
            else
            {
                Properties.Settings.Default.sth_autologin = AutologinCheckBox.Checked;
            }
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.sth_email = EmailTextBox.Text;
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.sth_password = PasswordTextBox.Text;
        }

        private void RegressionValue_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SettingRegressionValue = RegressionValue.Value;
        }

        private void OrderQuantityNUD_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SettingOrderQuantityNUD = OrderQuantityNUD.Value;
        }

        private void OrderMaxPriceNUD_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SettingOrderMaxPriceNUD = OrderMaxPriceNUD.Value;
        }

        private void ProxyCount_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SettingProxyCount = ProxyCount.Value;
        }

        private void ChangeOnlyUp_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SettingChangeOnlyUp = ChangeOnlyUp.Checked;
        }

        private void ProxyCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SettingProxyCheck = ProxyCheck.Checked;
        }
    }
}