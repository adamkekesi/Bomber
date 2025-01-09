using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Bomber.Model;
using Bomber.Persistence;
using BomberViewAvalonia.ViewModels;
using BomberViewAvalonia.Views;
using System.IO;
using System;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.Threading;

namespace BomberViewAvalonia;

public partial class App : Application
{
    #region Fields
    private BomberViewModel? viewModel;
    private BomberModel? model;

    #endregion

    #region Properites

    private TopLevel? TopLevel
    {
        get
        {
            return ApplicationLifetime switch
            {
                IClassicDesktopStyleApplicationLifetime desktop => TopLevel.GetTopLevel(desktop.MainWindow),
                ISingleViewApplicationLifetime singleViewPlatform => TopLevel.GetTopLevel(singleViewPlatform.MainView),
                _ => null
            };
        }
    }

    #endregion

    #region Application methods
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {

        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            viewModel = new BomberViewModel(false);
            viewModel.LoadMap += ViewModel_LoadGame;
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            viewModel = new BomberViewModel(true);
            viewModel.LoadMap += ViewModel_LoadGame;
            singleViewPlatform.MainView = new MainView
            {
                DataContext = viewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    #endregion

    #region ViewModel event handlers
    private async void ViewModel_LoadGame(object? sender, EventArgs e)
    {
        if (TopLevel == null)
        {
            await MessageBoxManager.GetMessageBoxStandard(
                    "Error",
                    "Can't access files",
                    ButtonEnum.Ok, Icon.Error)
                .ShowAsync();
            return;
        }

        var files = await TopLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Load map",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("Bomberman map")
                {
                    Patterns = new[] { "*.txt" }
                }
            },

        });

        if (files.Count == 0)
        {
            return;
        }

        try
        {
            Stream stream = await files[0].OpenReadAsync();
            model = new BomberModel(await new StreamMapLoader(stream).LoadAsync());
            model.GameOver += Model_GameOver;
            viewModel?.StartGame(model);
        }
        catch (IOException ex)
        {
            await MessageBoxManager.GetMessageBoxStandard(
                "Error",
                "File reading is unsuccessful!\n" + ex.Message,
                ButtonEnum.Ok, Icon.Error)
            .ShowAsync();
        }
    }
    #endregion

    #region Model event handlers
    private async void Model_GameOver(object? sender, EventArgs e)
    {
        if (model == null)
        {
            return;
        }

        if (!Dispatcher.UIThread.CheckAccess())
        {
            await Dispatcher.UIThread.Invoke(async () =>
            {
                await MessageBoxManager.GetMessageBoxStandard(
                            "End",
                           model.Won ? "You won!" : "Game over!",
                            ButtonEnum.Ok, Icon.Info)
                        .ShowAsync();

            });
        }
        else
        {
            await MessageBoxManager.GetMessageBoxStandard(
                               "End",
                              model.Won ? "You won!" : "Game over!",
                               ButtonEnum.Ok, Icon.Info)
                           .ShowAsync();
        }

        model.Dispose();
    }
    #endregion




}
