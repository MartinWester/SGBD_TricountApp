﻿using prbd_2324_a01.Model;
using PRBD_Framework;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

namespace prbd_2324_a01.ViewModel;

public class ListTricountsViewModel : ViewModelBase<User, PridContext>
{
    private ObservableCollection<TricountCardViewModel> _tricounts;
    public ObservableCollection<TricountCardViewModel> Tricounts {
        get => _tricounts;
        set => SetProperty(ref _tricounts, value);
    }

    private string _filter;
    public string Filter {
        get => _filter;
        set => SetProperty(ref _filter, value, OnRefreshData);
    }

    public ICommand ClearFilter { get; set; }
    public ICommand NewTricount { get; set; }
    public ICommand DisplayTricountDetail { get; set; }

    public ListTricountsViewModel() : base() {
        OnRefreshData();

        ClearFilter = new RelayCommand(() => Filter = "");

        NewTricount = new RelayCommand(() => {
            NotifyColleagues(App.Messages.MSG_EDIT_TRICOUNT, new Tricount());
        });

        DisplayTricountDetail = new RelayCommand<TricountCardViewModel>(vm => {
            NotifyColleagues(App.Messages.MSG_DISPLAY_TRICOUNT, vm.Tricount);
        });
    }

    protected override void OnRefreshData() {
        if (CurrentUser.Id != 5) {
            List<Tricount> tricounts = string.IsNullOrEmpty(Filter) ? Subscription.GetAllTricountByUserId(CurrentUser.Id) : Subscription.GetAllTricountByUserIdFiltered(CurrentUser.Id, Filter);
            Tricounts = new ObservableCollection<TricountCardViewModel>(tricounts.Select(t => new TricountCardViewModel(t)));
        } else {
            IQueryable<Tricount> tricounts = string.IsNullOrEmpty(Filter) ? Tricount.GetAll() : Tricount.GetAllFiltered(Filter);
            Tricounts = new ObservableCollection<TricountCardViewModel>(tricounts.Select(t => new TricountCardViewModel(t)));
        }
    }
}
