using Bomber.Model;
using Bomber.Persistence;
using BomberViewAvalonia.ViewModels;
using System;

namespace BomberViewAvalonia.Views
{
    public static class DesignData
    {
        public static BomberViewModel ViewModel
        {
            get
            {
                var model = new BomberModel(new CellContent[,]
                {
                    {CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty },
                    {CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty },
                    {CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty },
                    {CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty },
                    {CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty,CellContent.Empty },
                
                });
                model.PauseToggle();

                BomberViewModel vm = new BomberViewModel(true);
                vm.StartGame(model);
                return vm;
            }
        }
    }
}
