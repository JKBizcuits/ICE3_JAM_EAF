using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICE3StarterCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {

            if (txtConvert.Text == "" || (rdoLinear.IsChecked == false && rdoDB.IsChecked == false))
            {
                MessageBox.Show("No Radio Button selected or no text in box. Try again dummy.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (rdoLinear.IsChecked == true)
            {
                DBToLinear(txtConvert.Text);
            }
            else if (rdoDB.IsChecked == true)
            {

                LinearToDB(txtConvert.Text);
            }
        }

        private void LinearToDB(string x)
        {
            txtConvertOutput.Text = Convert.ToString(Math.Round(10 * Math.Log10(Convert.ToDouble(x)), 4)) + " SNR";
        }

        private void DBToLinear(string y)
        {
            txtConvertOutput.Text = Convert.ToString(Math.Round(Math.Pow(10, (Convert.ToDouble(y) / 10)), 4)) + " dB";
        }

        private void btnComputeNoise_Click(object sender, RoutedEventArgs e)
        {
            if (cboTemperatureUnit.Text == "" || txtTemperature.Text ==  "" || txtBandwidth.Text == "")
            {
                MessageBox.Show("Missing input information. Give up on life. No offense.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cboTemperatureUnit.Text == "Celsius")
            {
                calcNoiseInDB(CTOK(Convert.ToDouble(txtTemperature.Text)));
            }
            else if (cboTemperatureUnit.Text == "Fahrenheit")
            {
                calcNoiseInDB(FTOK(Convert.ToDouble(txtTemperature.Text)));
            }
            else if (cboTemperatureUnit.Text == "Kelvin")
            {
                calcNoiseInDB(Convert.ToDouble(txtTemperature.Text));
            }
        }

        private double CTOK(double temp)
        {
            return temp + 273.15;
        }

        private double FTOK(double temp)
        {
            return (temp - 32) * (5 / 9) + 273.15;
        }

        private void calcNoiseInDB(double t)
        {
            double result = Math.Round((-228.6) + 10 * Math.Log10(t) + 10 * Math.Log10(Convert.ToDouble(txtBandwidth.Text)* (Math.Pow(10,6))),4);
            txtNoiseOutput.Text = Convert.ToString(result) + " dBW";
        }
    }
}
