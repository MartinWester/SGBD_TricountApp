﻿using prbd_2324_a01.Model;
using PRBD_Framework;
using System.Windows.Input;

namespace prbd_2324_a01.ViewModel;

public class MainViewModel : ViewModelBase<User, PridContext>
{
    public static string Title {
        get => $"My Tricount ({CurrentUser?.Mail})";
    }

    public ICommand ReloadDataCommand { get; set; }
    public ICommand ResetDataCommand { get; set; }

    public MainViewModel() : base() {
        ReloadDataCommand = new RelayCommand(() => {
            if (Context.ChangeTracker.HasChanges())
                return;
            App.ClearContext();
            NotifyColleagues(ApplicationBaseMessages.MSG_REFRESH_DATA);
        });
        ResetDataCommand = new RelayCommand(() => {
            Console.Write("Reset Database...");
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            NotifyColleagues(ApplicationBaseMessages.MSG_REFRESH_DATA);
            NotifyColleagues(App.Messages.MSG_LOGIN, CurrentUser);
            Console.Write(" done");
        });
    }
}