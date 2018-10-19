﻿using System.ComponentModel;
using System.Windows;
using Atarashii.GUI;

namespace Atarashii.OpenSauce.GUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Main _main;

        public MainWindow()
        {
            InitializeComponent();
            _main = (Main) DataContext;
            _main.ShowLogWindow(this);
            _main.LogWindow.Height = 480;
            _main.LogWindow.Top = _main.LogWindow.Top - 60; // (new height - old height) / 2
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            BaseModel.Exit();
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            _main.InstallationPath = BaseModel.PickFolder();
        }

        private void Install(object sender, RoutedEventArgs e)
        {
            _main.InstallOpenSauce();
        }
    }
}