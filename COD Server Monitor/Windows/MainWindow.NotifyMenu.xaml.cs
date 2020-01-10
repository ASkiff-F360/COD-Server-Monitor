using System.Windows;

namespace COD_Server_Monitor
{
   public partial class MainWindow : Window
   {
      private void NotifyMenu_ShowApp (object sender, RoutedEventArgs e)
      {
         this.WindowState = WindowState.Normal;
      }

      private void NotifyMenu_ExitApp (object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown ();
      }

      private void NotifyMenu_SwapTheme (object sender, RoutedEventArgs e)
      {
         (App.Current as App).ChangeSkin (App.Skin == Skin.Dark ? Skin.Light : Skin.Dark);

      }
   }
}