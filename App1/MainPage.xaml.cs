using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        String filmName;
        TMDbClient client = new TMDbClient("dd74c13e1622f36f0aaa6b03ae4f2e20");

        public MainPage()
        {
            this.InitializeComponent();

        }


        private void searchButon_Click(object sender, RoutedEventArgs e)
        {
            var searchedText = findText.Text;

            if (searchedText == "")
            {
                myListView.Items.Clear();
            }
            else if (filmName != searchedText)
            {
                filmName = searchedText;

                myListView.Items.Clear();

                SearchContainer<SearchMovie> results = client.SearchMovieAsync(filmName).Result;

                foreach (SearchMovie result in results.Results)
                    myListView.Items.Add(result.Title);

                myListView.InvalidateArrange();
            }

        }

        private void findText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(myListView.SelectedItems.Count > 0)
            {
                var item = myListView.SelectedItem.ToString();

                SearchContainer<SearchMovie> results = client.SearchMovieAsync(item).Result;
                TitleText.Text = item;
                YearText.Text = results.Results[0].ReleaseDate.ToString();
                Overview.Text = results.Results[0].Overview;
                Popularity.Text = "Popularity: " + Math.Round(results.Results[0].Popularity, 2).ToString() + " / 10";

            }

            }


        }
    }

