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

namespace CricketRadarApplication.Views
{
    /// <summary>
    /// Interaction logic for AddTeamForm.xaml
    /// </summary>
    public partial class AddTeamForm : UserControl
    {
        public event Action<string, string,string> SaveClicked;

        public AddTeamForm() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string teamName = txtTeamName.Text;
            string city = txtCity.Text;
            string captain = txtCaptain.Text;
            SaveClicked?.Invoke(teamName, city, captain);

            // Close the window to hide the popup
            Window.GetWindow(this).Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
