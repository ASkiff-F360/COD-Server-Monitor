using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace COD_Server_Monitor
{
   [DataContract]
   public class MonitoredApp : INotifyPropertyChanged
   {
      private String name;
      private String display;
      private String path;
      private String args;
      private Boolean restart;
      private Boolean inject;
      private String dllPath;
      private Int32 pid;
      private Boolean running;

      /// <summary>
      /// The name of the application being monitored
      /// </summary>
      [DataMember (Name = "FileName")]
      public String Name
      {
         get { return name; }
         set
         {
            if (name != null && name.Equals (value))
               return;

            name = value;
            OnPropertyChanged ("Name");
         }
      }

      [DataMember (Name = "DisplayName")]
      public String DisplayName
      {
         get { return display; }
         set
         {
            if (display != null && display.Equals (value))
               return;

            display = value;
            OnPropertyChanged ("DisplayName");
         }
      }

      /// <summary>
      /// The file path to the executable of the application being monitored
      /// </summary>
      [DataMember (Name = "FilePath")]
      public String Path
      {
         get { return path; }
         set
         {
            if (path != null && path.Equals (value))
               return;

            path = value;
            OnPropertyChanged ("Path");
         }
      }

      /// <summary>
      /// The command line arguments used when starting the application
      /// </summary>
      [DataMember (Name = "Arguments")]
      public String Arguments
      {
         get { return args; }
         set
         {
            if (args != null && args.Equals (value))
               return;

            args = value;
            OnPropertyChanged ("Arguments");
         }
      }

      /// <summary>
      /// Whether or not to restart the application when crashed or closed
      /// </summary>
      [DataMember (Name = "AutoRestart")]
      public Boolean AutoRestart
      {
         get { return restart; }
         set
         {
            if (restart == value)
               return;

            restart = value;
            OnPropertyChanged ("AutoRestart");
         }
      }

      /// <summary>
      /// Whether or not there is a DLL that should be injected on startup
      /// </summary>
      [DataMember (Name = "AutoInject", EmitDefaultValue = false)]
      public Boolean AutoInject
      {
         get { return inject; }
         set
         {
            if (inject == value)
               return;

            inject = value;
            OnPropertyChanged ("AutoInject");
         }
      }

      /// <summary>
      /// Path to the DLL to be auto injected on application startup
      /// </summary>
      [DataMember(Name = "DllPath")]
      public String DllPath
      {
         get { return dllPath; }
         set
         {
            if (dllPath != null && dllPath.Equals(value))
               return;

            dllPath = value;
            OnPropertyChanged("DllPath");
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
            if (pid == value)
               return;

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
            if (running == value)
               return;

            running = value;
            OnPropertyChanged ("IsRunning");
         }
      }

      public MonitoredApp (String filePath, String displayName = "", String arguments = "", bool autoRestart = false, bool autoInject = false, String injectPath = null)
      {
         this.Name = System.IO.Path.GetFileName (filePath);
         this.DisplayName = displayName;
         this.Path = filePath;
         this.Arguments = arguments;
         this.AutoRestart = autoRestart;
         this.AutoInject = autoInject;
         this.DllPath = injectPath;
         this.ProcessID = 0;
         this.IsRunning = false;
      }

      public event PropertyChangedEventHandler PropertyChanged;
      protected void OnPropertyChanged (string PropertyName)
      {
         PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (PropertyName));
      }

      public bool StartApp ()
      {
         try
         {
            Process process = new Process ();
            process.StartInfo.FileName = this.Path;
            process.StartInfo.Arguments = this.Arguments;

            process.Start ();

            if(this.AutoInject && this.DllPath != null)
            {
               System.Threading.Thread.Sleep(1000);

               Memory.InjectDLL(process, this.DllPath);
            }

            this.ProcessID = process.Id;
            this.IsRunning = true;
         }
         catch
         {
            this.ProcessID = 0;
            this.IsRunning = false;
         }

         return this.IsRunning;
      }
   }
}
