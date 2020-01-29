using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace COD_Server_Monitor
{
   public partial class Switch : UserControl
   {
      public static readonly DependencyProperty IsOnProperty =
         DependencyProperty.Register ("IsOn", typeof (Boolean), typeof (Switch), new FrameworkPropertyMetadata (false));

      public Boolean IsOn
      {
         get { return (Boolean) GetValue (IsOnProperty); }
         set { SetValue (IsOnProperty, value); }
      }

      public CornerRadius CornerRadius
      {
         get { return this.BackgroundBorder.CornerRadius; }
         set { this.BackgroundBorder.CornerRadius = value; }
      }

      public Switch ()
      {
         InitializeComponent ();
      }

      private void SelectorEllipse_MouseDown (object sender, MouseButtonEventArgs e)
      {
         IsOn = !IsOn;
      }
   }
}
