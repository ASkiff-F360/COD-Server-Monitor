using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace COD_Server_Monitor
{
   [DataContract]
   public class MonitoredApp : INotifyPropertyChanged
   {
      private String name;
      private String path;
      private String args;
      private Boolean restart;
      private Int32 pid;
      private Boolean running;

      /// <summary>
      /// The name of the application being monitored
      /// </summary>
      [DataMember(Name="FileName")]
      public String Name {
         get { return name; }
         set {
            name = value;
            OnPropertyChanged("Name");
         }
      }

      /// <summary>
      /// The file path to the executable of the application being monitored
      /// </summary>
      [DataMember(Name="FilePath")]
      public String Path
      {
         get { return path; }
         set
         {
            path = value;
            OnPropertyChanged ("Path");
         }
      }

      /// <summary>
      /// The command line arguments used when starting the application
      /// </summary>
      [DataMember(Name="Arguments")]
      public String Arguments
      {
         get { return args; }
         set
         {
            args = value;
            OnPropertyChanged ("Arguments");
         }
      }

      /// <summary>
      /// Whether or not to restart the application when crashed or closed
      /// </summary>
      [DataMember(Name="AutoRestart")]
      public Boolean AutoRestart
      {
         get { return restart; }
         set
         {
            restart = value;
            OnPropertyChanged ("AutoRestart");
         }
      }

      /// <summary>
      /// The process ID of the application being monitored
      /// </summary>
      public Int32 ProcessID
      {
         get { return pid; }
         set
         {
            pid = value;
            OnPropertyChanged ("ProcessID");
         }
      }

      /// <summary>
      /// Whether or not the application is currently running
      /// </summary>
      public Boolean IsRunning
      {
         get { return running; }
         set
         {
            running = value;
            OnPropertyChanged ("IsRunning");
         }
      }

      public MonitoredApp(String filePath, String arguments = "", bool autoRestart = false)
      {
         this.Name = System.IO.Path.GetFileName(filePath);
         this.Path = filePath;
         this.Arguments = arguments;
         this.AutoRestart = autoRestart;
         this.ProcessID = 0;
         this.IsRunning = false;
      }

      public event PropertyChangedEventHandler PropertyChanged;
      protected void OnPropertyChanged(string PropertyName)
      {
         Console.WriteLine("Property Changed: {0}", PropertyName);
         PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (PropertyName));
      }
   }
}
