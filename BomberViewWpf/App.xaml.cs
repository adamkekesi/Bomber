using Bomber.Model;
using Bomber.Persistence;
using BomberViewWpf.View;
using BomberViewWpf.ViewModel;
using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace BomberViewWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields
        private BomberViewModel? viewModel;
        private MainWindow? view;
        private BomberModel? model;

        #endregion

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            viewModel = new BomberViewModel();
            viewModel.LoadMap += ViewModel_LoadGame;

            view = new MainWindow();
            view.DataContext = viewModel;
            view.Show();
        }

        #region ViewModel event handlers
        private async void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    model = new BomberModel(await new MapLoader(openFileDialog.FileName).LoadAsync());
                    model.GameOver += Model_GameOver;
                    viewModel?.StartGame(model);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("File reading is unsuccessful!\n" + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        #endregion

        #region Model event handlers
        private void Model_GameOver(object? sender, EventArgs e)
        {
            if (model == null)
            {
                return;
            }

            MessageBox.Show(model.Won ? "You won!" : "Game over!",
                      "End", MessageBoxButton.OK, MessageBoxImage.Information);

            model.Dispose();
        }
        #endregion




    }

}
