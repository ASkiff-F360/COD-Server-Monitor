using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace COD_Server_Monitor
{
   public partial class MainWindow : Window
   {
      private DispatcherTimer AppMonitor;
      private ObservableCollection<MonitoredApp> AppCollection;

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
         Double width = 0, height = 0;
         Boolean TrayMinimize = false;
         UserStorage.Deserialize (ref AppCollection, ref width, ref height, ref TrayMinimize);

         this.Width = width;
         this.Height = height;
         this.MinimizeToTray.IsChecked = TrayMinimize;

         ApplicationGrid.ItemsSource = AppCollection;
      }

      private void Window_Closing (object sender, System.ComponentModel.CancelEventArgs e)
      {
         UserStorage.Serialize (AppCollection, this.Width, this.Height, this.MinimizeToTray.IsChecked);
      }

      private void Window_StateChanged (object sender, EventArgs e)
      {
         if (this.WindowState == WindowState.Minimized && this.MinimizeToTray.IsChecked)
         {
            this.ShowInTaskbar = false;
         }
         else if (this.WindowState == WindowState.Normal)
         {
            this.ShowInTaskbar = true;
            this.Activate ();
         }
      }

      private void CheckApplicationStatus (object sender, EventArgs e)
      {
         foreach (MonitoredApp app in AppCollection)
         {
            if (!app.IsRunning || app.ProcessID == 0)
               continue;

            // Check to see if the process has exited
            Process process = null;
            try
            {
               process = Process.GetProcessById (app.ProcessID);
            }
            catch
            {
               app.ProcessID = 0;
               app.IsRunning = false;

               OutputText.Text += String.Format ("{0} is no longer running\n", app.Name);

               if (app.AutoRestart)
                  StartApplication (app);

               continue;
            }

            // Check to see if process is no longer responding
            process.Refresh ();
            if (!process.Responding || process.MainWindowTitle.Contains ("ERROR"))
            {
               app.ProcessID = 0;
               app.IsRunning = false;
               OutputText.Text += String.Format ("{0} is no longer responding\n", app.Name);

               if (app.AutoRestart)
               {
                  process.Kill ();

                  StartApplication (app);
               }
            }
         }
      }

      private void StartApplication (MonitoredApp app)
      {
         bool success = app.StartApp ();

         OutputText.Text += success ?
            String.Format ("{0} started with args: {1}\n", app.Name, app.Arguments) :
            String.Format ("Unable to start {0}\n", app.Path);
      }
   }
}
