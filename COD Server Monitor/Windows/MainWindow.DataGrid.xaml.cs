using System.Windows;

namespace COD_Server_Monitor
{
   public partial class MainWindow : Window
   {
      private void ApplicationGrid_ContextClick (object sender, RoutedEventArgs e)
      {
         AddAppWindow addApp = new AddAppWindow ();
         addApp.ShowDialog ();

         if (addApp.AddApplication)
         {
            AppCollection.Add (new MonitoredApp (addApp.FilePath, addApp.DisplayName, addApp.Arguments, addApp.AutoRestart));
         }
      }

      private void ApplicationGrid_StartClick (object sender, RoutedEventArgs e)
      {
         MonitoredApp monitoredApp = ((FrameworkElement) sender).DataContext as MonitoredApp;

         StartApplication (monitoredApp);

         if (!AppMonitor.IsEnabled)
            AppMonitor.Start ();
      }

      private void ApplicationGrid_RemoveClick (object sender, RoutedEventArgs e)
      {
         MonitoredApp monitoredApp = ((FrameworkElement) sender).DataContext as MonitoredApp;

         AppCollection.Remove (monitoredApp);
      }
   }
}