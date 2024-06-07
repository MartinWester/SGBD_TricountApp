﻿using prbd_2324_a01.Model;
using PRBD_Framework;
using prbd_2324_a01.ViewModel;

namespace prbd_2324_a01.View
{
    public partial class ViewOperationView : DialogWindowBase
    {
        public ViewOperationView(Operation operation) {
            InitializeComponent();
            DataContext = new ViewOperationViewModel(operation);
        }

        public ViewOperationView(Model.Tricount tricount) {
            InitializeComponent();
            DataContext = new ViewOperationViewModel(tricount);
        }
    }
}
