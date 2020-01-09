using System;
using System.Windows;

namespace COD_Server_Monitor
{
   public class SkinResourceDictionary : ResourceDictionary
   {
      private Uri _lightSource;
      private Uri _darkSource;

      public Uri LightSource
      {
         get { return _lightSource; }
         set
         {
            _lightSource = value;
            base.Source = _lightSource;

         }
      }

      public Uri DarkSource
      {
         get { return _darkSource; }
         set
         {
            _darkSource = value;
            base.Source = _darkSource;
         }
      }

      public void UpdateSource ()
      {
         Uri source = null;

         switch (App.Skin)
         {
            case Skin.Light: source = _lightSource; break;
            case Skin.Dark: source = _darkSource; break;
         }

         if (source != null && base.Source != source)
            base.Source = source;
      }
   }
}
