﻿using prbd_2324_a01.Model;
using PRBD_Framework;

namespace prbd_2324_a01.ViewModel;

public class MainViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
{
    public string Title => "prbd_2324_a01";
}