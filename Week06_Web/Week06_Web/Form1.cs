using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Week06_Web.Entities;
using Week06_Web.MnbServiceReference;

namespace Week06_Web
{
    public partial class Form1 : Form
    {

        BindingList<RateData> rates = new BindingList<RateData>();

        public Form1()
        {
            InitializeComponent();
            Fuggveny1();

            dataGridView1.DataSource = rates;
        }
        public void Fuggveny1()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

        }
    }
}
