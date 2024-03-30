using CricketRadarApplication.Models;
using CricketRadarApplication.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CricketRadarApplication.Views;

namespace CricketRadarApplication.ViewModels
{
        public class TeamViewModel : INotifyPropertyChanged
        {
            private readonly CricketDAL CricketData;

            // Collection of employees bound to the UI.
            public ObservableCollection<Teams> Teams { get; set; } = new ObservableCollection<Teams>();
            // Selected employee in the UI.
            public Teams SelectedTeam { get; set; }

            public RelayCommand AddCommand { get; }
            public RelayCommand UpdateCommand { get; }
            public RelayCommand DeleteCommand { get; }

            public TeamViewModel(CricketDAL dataAccessLayer)
            {
                CricketData = dataAccessLayer;

                AddCommand = new RelayCommand(AddTeam);
                UpdateCommand = new RelayCommand(UpdateTeam, CanUpdateDelete);
                DeleteCommand = new RelayCommand(DeleteTeam, CanUpdateDelete);

                LoadData();
            }

            /// <summary>
            /// Loads employee data from the database.
            /// </summary>
            private void LoadData()
            {
                Teams.Clear();
                foreach (var employee in CricketData.GetTeams())
                {
                    Teams.Add(employee);
                }
            }

            /// <summary>
            /// Determines if updating or deleting an employee is possible.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            private bool CanUpdateDelete(object obj)
            {
                return SelectedTeam != null;
            }

            /// <summary>
            /// Adds a new Team.
            /// </summary>
            /// <param name="obj"></param>
            private void AddTeam(object obj)
            {
                var teamForm = new AddTeamForm();
                teamForm.SaveClicked += (teamName, city, captain) =>
                {
                    Teams team = new Teams { TeamName = teamName, City = city, Captain = captain};
                    CricketData.AddTeam(team);
                    LoadData();
                };

                ShowDialog(teamForm, "Add Team");
            }

            //// <summary>
            /// Updates the selected cricket team.
            /// </summary>
            /// <param name="obj"></param>
            private void UpdateTeam(object obj)
            {
                var teamForm = new AddTeamForm();
                teamForm.txtTeamName.Text = SelectedTeam.TeamName;
                teamForm.txtCity.Text = SelectedTeam.City;
                teamForm.txtCaptain.Text = SelectedTeam.Captain;

                teamForm.SaveClicked += (teamName, city, captain) =>
                {
                    SelectedTeam.TeamName = teamName;
                    SelectedTeam.City = city;
                    SelectedTeam.Captain = captain;
                    CricketData.UpdateTeam(SelectedTeam);
                    LoadData();
                };

                ShowDialog(teamForm, "Update Team");
            }


        /// <summary>
        /// Deletes the selected employee
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteTeam(object obj)
            {
                if (SelectedTeam != null)
                {
                    CricketData.DeleteTeam(SelectedTeam.TeamId);
                    LoadData();
                }
            }

            /// <summary>
            /// Displays a dialog window.
            /// </summary>
            /// <param name="teamForm"></param>
            /// <param name="title"></param>
            private void ShowDialog(AddTeamForm teamForm, string title)
            {
                var window = new Window
                {
                    Title = title,
                    Content = teamForm,
                    SizeToContent = SizeToContent.WidthAndHeight
                };
                window.ShowDialog();
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
}
