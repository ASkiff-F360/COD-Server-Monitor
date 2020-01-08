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
      private ObservableCollection<ApplicationRow> AppCollection;
      private DispatcherTimer AppMonitor;

      public MainWindow ()
      {
         InitializeComponent ();

         AppMonitor = new DispatcherTimer ();
         AppMonitor.Tick += new EventHandler (CheckApplicationStatus);
         AppMonitor.Interval = new TimeSpan (0, 0, 5); //Every 5 seconds

         AppCollection = new ObservableCollection<ApplicationRow> ();

         ApplicationGrid.ItemsSource = AppCollection;
      }

      private void ApplicationGrid_ContextClick (object sender, RoutedEventArgs e)
      {
         AddAppWindow addApp = new AddAppWindow ();
         addApp.ShowDialog ();

         if (addApp.AddApplication)
         {
            String FileName = System.IO.Path.GetFileName (addApp.FilePath);

            AppCollection.Add (new ApplicationRow ()
            {
               Name = FileName,
               Path = addApp.FilePath,
               Arguments = addApp.Arguments,
               AutoRestart = addApp.AutoRestart
            });
         }
      }

      private void ApplicationGrid_StartClick (object sender, RoutedEventArgs e)
      {
         if (!AppMonitor.IsEnabled)
            AppMonitor.Start ();
      }

      private void ApplicationGrid_RemoveClick (object sender, RoutedEventArgs e)
      {

      }

      private void OutputText_ClearOutput (object sender, RoutedEventArgs e)
      {
         this.OutputText.Text = String.Empty;
      }

      private void CheckApplicationStatus (object sender, EventArgs e)
      {

      }
   }

   public class ApplicationRow
   {
      public String Name { get; set; }
      public String Path { get; set; }
      public String Arguments { get; set; }
      public Boolean AutoRestart { get; set; }
   }
}
