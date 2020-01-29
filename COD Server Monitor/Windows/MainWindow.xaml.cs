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
      private UserInterface InterfaceValues;

      public MainWindow ()
      {
         InitializeComponent ();

         AppMonitor = new DispatcherTimer ();
         AppMonitor.Tick += new EventHandler (CheckApplicationStatus);
         AppMonitor.Interval = new TimeSpan (0, 0, 10); //Every 10 seconds

         AppCollection = new ObservableCollection<MonitoredApp> ();
         InterfaceValues = new UserInterface ();
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

               OutputText.Text += String.Format ("{0} [{1}] is no longer running\n", app.DisplayName, app.Name);

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
               OutputText.Text += String.Format ("{0} [{1}] is no longer responding\n", app.DisplayName, app.Name);

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
            String.Format ("{0} [{1}] started with args: {2}\n", app.DisplayName, app.Name, app.Arguments) :
            String.Format ("Unable to start {0} [{1}]\n", app.DisplayName, app.Path);
      }
   }
}
