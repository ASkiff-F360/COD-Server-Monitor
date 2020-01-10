using System;
using System.Windows;

namespace COD_Server_Monitor
{
   public partial class MainWindow : Window
   {
      private void Window_Loaded (object sender, RoutedEventArgs e)
      {
         UserStorage.Deserialize (ref AppCollection, ref InterfaceValues);
         
         ApplicationGrid.ItemsSource = AppCollection;
         
         this.MinimizeToTray.IsChecked = InterfaceValues.MinimizeToTray;
         this.Width = InterfaceValues.Width;
         this.Height = InterfaceValues.Height;
      }

      private void Window_Closing (object sender, System.ComponentModel.CancelEventArgs e)
      {
         InterfaceValues.MinimizeToTray = this.MinimizeToTray.IsChecked;
         InterfaceValues.Width = this.Width;
         InterfaceValues.Height = this.Height;

         UserStorage.Serialize (AppCollection, InterfaceValues);
      }
   }
}