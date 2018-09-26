using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Gaming.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace Breakout
{
    public class Passing
    {
        public string text { get; set; }
        public int size { get; set; }
       
        public SolidColorBrush color { get; set; }
        public MediaElement Elm { set; get; }
        public Passing()
        {

        }
    }
}
