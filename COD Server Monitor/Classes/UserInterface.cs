using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace COD_Server_Monitor
{
   [DataContract]
   public class UserInterface
   {
      [DataMember]
      public Boolean MinimizeToTray;

      [DataMember]
      public Double Width;

      [DataMember]
      public Double Height;

      [DataMember]
      public Skin Skin;
   }
}
