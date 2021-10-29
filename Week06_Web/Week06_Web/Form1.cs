using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Week06_Web.Entities;
using Week06_Web.MnbServiceReference;

namespace Week06_Web
{
    public partial class Form1 : Form
    {

        BindingList<RateData> rates = new BindingList<RateData>();
        string result;
        string resultCurrency;
        BindingList<string> currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            RefreshData();
            LoadCurrencies();

            comboBox1.DataSource = currencies;
            comboBox1.Text = currencies.First();
            dataGridView1.DataSource = rates;
        }
        public void RefreshData()
        {
            rates.Clear();
            var mnbService = new MNBArfolyamServiceSoapClient();

            var requestCurrency = new GetCurrenciesRequestBody();
            var responseCurrency = mnbService.GetCurrencies(requestCurrency);
            resultCurrency = responseCurrency.GetCurrenciesResult;

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.Text,
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            var response = mnbService.GetExchangeRates(request);
            result = response.GetExchangeRatesResult;

            getXml();
            diagram();
        }

        public void getXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                {
                    rate.Value = value / unit;
                }
            }
        }

        public void diagram()
        {
            chart1.DataSource = rates;

            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void LoadCurrencies()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(resultCurrency);           
            foreach(XmlElement item in xml.DocumentElement.FirstChild.ChildNodes)
            {
                currencies.Add(item.InnerText);
            }
        }
    }
}
