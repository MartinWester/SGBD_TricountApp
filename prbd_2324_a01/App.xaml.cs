﻿using prbd_2324_a01.Model;
using prbd_2324_a01.ViewModel;
using System.Windows;
using System.Globalization;
using PRBD_Framework;

namespace prbd_2324_a01;

public partial class App : ApplicationBase<User, PridContext> {
    public enum Messages
    {
        MSG_LOGIN,
        MSG_LOGOUT,
        MSG_EDIT_TRICOUNT,
        MSG_DISPLAY_TRICOUNT,
        MSG_CLOSE_TAB,
        MSG_CLEAR_TAB,
        MSG_SIGNUP,
        MSG_DO_SIGNUP,
        MSG_TRICOUNT_CHANGED,
        MSG_NEW_TRICOUNT,
        MSG_PARTICIPANTS_CHANGED,
        MSG_TITLE_CHANGED,
        MSG_EDIT_OPERATION,
        MSG_ADD_OPERATION,
        MSG_DELETE_TRICOUNT,
        MSG_DELETE_OPERATION,
        MSG_OPERATION_CHANGED,
        MSG_OPERATION_AMOUNT_CHANGED
    }

    public App() {
        var ci = new CultureInfo("fr-BE") {
            DateTimeFormat = {
                ShortDatePattern = "dd/MM/yyyy",
                DateSeparator = "/"
            }
        };
        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;
        CultureInfo.CurrentCulture = ci;
        CultureInfo.CurrentUICulture = ci;
    }

    protected override void OnStartup(StartupEventArgs e) {
        PrepareDatabase();
        TestQueries();

        Register<User>(this, Messages.MSG_LOGIN, user => {
            Login(user);
            NavigateTo<MainViewModel, User, PridContext>();
        });

        Register(this, Messages.MSG_LOGOUT, () => {
            Logout();
            NavigateTo<LoginViewModel, User, PridContext>();
        });

        Register(this, Messages.MSG_SIGNUP, () => {
            NavigateTo<SignupViewModel, User, PridContext>();
        });

        Register<User>(this, Messages.MSG_DO_SIGNUP, user => {
            Login(user);
            NavigateTo<MainViewModel, User, PridContext>();
        });

    }

    private static void PrepareDatabase() {
        // Clear database and seed data
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();

        // Cold start
        Console.Write("Cold starting database... ");
        Context.Users.Find(1);
        Console.WriteLine("done");        
    }

    protected override void OnRefreshData() {
        if (CurrentUser?.Id != null)
            CurrentUser = User.GetUserById(CurrentUser.Id);
    }

    private static void TestQueries() {
        // Un endroit pour tester vos requêtes LINQ
    }
}