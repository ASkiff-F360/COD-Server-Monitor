using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace COD_Server_Monitor
{
   public partial class MainWindow : Window
   {
      private ObservableCollection<MonitoredApp> AppCollection;
      private DispatcherTimer AppMonitor;

      public MainWindow ()
      {
         InitializeComponent ();

         AppMonitor = new DispatcherTimer ();
         AppMonitor.Tick += new EventHandler (CheckApplicationStatus);
         AppMonitor.Interval = new TimeSpan (0, 0, 5); //Every 5 seconds

         AppCollection = new ObservableCollection<MonitoredApp> ();
      }

      private void Window_Loaded (object sender, RoutedEventArgs e)
      {
         UserStorage.Deserialize(ref AppCollection);

         ApplicationGrid.ItemsSource = AppCollection;
      }

      private void Window_Closing (object sender, System.ComponentModel.CancelEventArgs e)
      {
         UserStorage.Serialize (AppCollection);
      }

      private void ApplicationGrid_ContextClick (object sender, RoutedEventArgs e)
      {
         AddAppWindow addApp = new AddAppWindow ();
         addApp.ShowDialog ();

         if (addApp.AddApplication)
         {
            AppCollection.Add (new MonitoredApp (addApp.FilePath, addApp.Arguments, addApp.AutoRestart));
         }
      }

      private void ApplicationGrid_StartClick (object sender, RoutedEventArgs e)
      {
         MonitoredApp monitoredApp = ((FrameworkElement) sender).DataContext as MonitoredApp;

         monitoredApp.IsRunning = true;

         //if (!AppMonitor.IsEnabled)
           // AppMonitor.Start ();
      }

      private void ApplicationGrid_RemoveClick (object sender, RoutedEventArgs e)
      {
         MonitoredApp monitoredApp = ((FrameworkElement)sender).DataContext as MonitoredApp;

         AppCollection.Remove(monitoredApp);
      }

      private void OutputText_ClearOutput (object sender, RoutedEventArgs e)
      {
         this.OutputText.Text = String.Empty;
      }

      private void CheckApplicationStatus (object sender, EventArgs e)
      {

      }
   }
}
