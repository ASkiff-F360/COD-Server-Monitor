using System.Windows;

namespace COD_Server_Monitor
{
   public enum Skin
   {
      Light,
      Dark
   }

   public partial class App : Application
   {
      public static Skin Skin
      {
         get;
         set;
      } = Skin.Dark;

      public void ChangeSkin (Skin skin)
      {
         Skin = skin;

         foreach (ResourceDictionary dict in Resources.MergedDictionaries)
         {
            if (dict is SkinResourceDictionary skinDict)
               skinDict.UpdateSource ();
            else
               dict.Source = dict.Source;
         }
      }
   }
}
