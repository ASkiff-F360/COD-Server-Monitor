using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace COD_Server_Monitor
{
   public partial class Switch : UserControl
   {
      public static readonly DependencyProperty IsOnProperty = 
         DependencyProperty.Register("IsOn", typeof(Boolean), typeof(Switch), new FrameworkPropertyMetadata(false));

      public Boolean IsOn
      {
         get { return (Boolean)GetValue(IsOnProperty); }
         set { SetValue(IsOnProperty, value); BindingGroup.SetValue(IsOnProperty, value); }
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
