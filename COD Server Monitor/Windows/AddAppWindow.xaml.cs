﻿using Microsoft.Win32;
using System;
using System.Windows;

namespace COD_Server_Monitor
{
   public partial class AddAppWindow : Window
   {
      public String FilePath;
      public String DisplayName;
      public String Arguments;
      public Boolean AutoRestart;
      public Boolean AddApplication;

      public AddAppWindow ()
      {
         InitializeComponent ();

         AddApplication = false;
      }

      private void Window_Loaded (object sender, RoutedEventArgs e)
      {
         Window mainWindow = Application.Current.MainWindow;
         this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
         this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
      }

      private void AddApplication_BrowseClick (object sender, RoutedEventArgs e)
      {
         OpenFileDialog dialog = new OpenFileDialog ();
         dialog.Filter = "EXE Files|*.exe";
         if (dialog.ShowDialog () == true)
         {
            this.FilePath = dialog.FileName;
            this.FilePathTextBox.Text = this.FilePath;
            this.AddButton.IsEnabled = true;
         }
      }

      private void AddApplication_AddClick (object sender, RoutedEventArgs e)
      {
         this.Arguments = this.ArgumentsTextBox.Text;
         this.DisplayName = this.DisplayNameTextBox.Text;
         this.AutoRestart = this.AutoRestartSwitch.IsOn == true;

         if (this.Arguments.Equals ("Arguments"))
            this.Arguments = String.Empty;

         AddApplication = true;
         this.Close ();
      }

      private void AddApplication_CancelClick (object sender, RoutedEventArgs e)
      {
         this.Close ();
      }

      private void AddApplication_ArgumentsEnterFocus (object sender, RoutedEventArgs e)
      {
         if (this.ArgumentsTextBox.Text.Equals ("Arguments"))
            this.ArgumentsTextBox.Text = String.Empty;
      }

      private void AddApplication_ArgumentsLeaveFocus (object sender, RoutedEventArgs e)
      {
         if (this.ArgumentsTextBox.Text.Equals (String.Empty))
            this.ArgumentsTextBox.Text = "Arguments";
      }
   }
}
