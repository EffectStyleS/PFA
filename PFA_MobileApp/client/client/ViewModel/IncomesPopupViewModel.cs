using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class IncomesPopupViewModel : BaseViewModel
    {
        public IncomesPopupViewModel(IncomeModel income = null) 
        {
            PageTitle = income == null ? "Add Income" : "Edit Income";

            Id = income?.Id;
            Name = income?.Name;
            Value = income?.Value;
            Date = income?.Date;
            IncomeTypeId = income == null ? 0 : income.IncomeTypeId;

            // TODO: из сервиса подгружать
            IncomeTypes = new ObservableCollection<IncomeTypeModel>()
            {
                new IncomeTypeModel() { Name = "Type 1" },
                new IncomeTypeModel() { Name = "Type 2" },
                new IncomeTypeModel() { Name = "Type 3" },
                new IncomeTypeModel() { Name = "Type 4" },
            };
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        int? _id;

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        decimal? _value;

        [ObservableProperty]
        DateTime? _date;

        [ObservableProperty]
        int _incomeTypeId;

        [ObservableProperty]
        ObservableCollection<IncomeTypeModel> _incomeTypes;

        [RelayCommand]
        async Task Save()
        {
            // TODO: реализовать сохранение
            await MopupService.Instance.PopAsync();
            
        }

        [RelayCommand]
        async Task Cancel()
        {
            // TODO: реализовать отмену
            await MopupService.Instance.PopAsync();
        }
    }
}
