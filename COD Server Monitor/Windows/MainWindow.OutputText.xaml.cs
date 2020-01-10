using System;
using System.Windows;

namespace COD_Server_Monitor
{
   public partial class MainWindow : Window
   {
      private void OutputText_ClearOutput (object sender, RoutedEventArgs e)
      {
         this.OutputText.Text = String.Empty;
      }
   }
}