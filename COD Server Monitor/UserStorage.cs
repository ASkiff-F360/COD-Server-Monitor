using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace COD_Server_Monitor
{
   public class UserStorage
   {
      private static string APP_DATA = Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);
      private static string SETTINGS_DIR = APP_DATA + "\\COD Server Monitor";
      private static string SETTINGS_XML = SETTINGS_DIR + "\\settings.xml";

      public List<MonitoredApp> applications;

      public UserStorage ()
      {
         applications = new List<MonitoredApp> ();
      }

      public static void Serialize (ObservableCollection<MonitoredApp> appList)
      {
         if (!Directory.Exists (SETTINGS_DIR))
            Directory.CreateDirectory (SETTINGS_DIR);

         UserStorage storage = new UserStorage ();
         foreach (MonitoredApp app in appList)
            storage.applications.Add (app);

         var serializer = new DataContractSerializer (typeof (UserStorage));
         using (XmlWriter xml = XmlWriter.Create (SETTINGS_XML))
            serializer.WriteObject (xml, storage);
      }

      public static void Deserialize (ref ObservableCollection<MonitoredApp> appList)
      {
         UserStorage storage = null;

         try
         {
            var serializer = new DataContractSerializer (typeof (UserStorage));
            using (XmlReader xml = XmlReader.Create (SETTINGS_XML))
               storage = (UserStorage) serializer.ReadObject (xml);
         }
         catch
         {
            storage = new UserStorage ();
         }

         foreach (MonitoredApp app in storage.applications)
            appList.Add (app);
      }
   }
}
