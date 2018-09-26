using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Breakout
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Start : Page
	{

        MediaElement element, bttn;
		public Start()
		{
			this.InitializeComponent();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            play_music(); //cue menu music           
          
        }

        public async void play_music() //plays start-up music
        {
            element = new MediaElement();          
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");
            var file = await assets.GetFileAsync("BREAKOUT_START_UP_ (3).mp3");
            element.SetSource(await file.OpenAsync(FileAccessMode.Read), file.ContentType);
            element.AutoPlay = true;
            
        }


        private async void bttn_sound() // allows a button noise to be selected 
        {
            bttn = new MediaElement();
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");
            var file = await assets.GetFileAsync("Button.mp3");
            bttn.SetSource(await file.OpenAsync(FileAccessMode.Read), file.ContentType);
            bttn.Play();
        }


        private void onePlayer_Click( object sender, RoutedEventArgs e )
		{
			this.Frame.Navigate( typeof( MainPage ), 1 );
            element.Stop();
            bttn_sound(); //cue button click sound
            
		}

        private void autoPilot_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), 0);
            element.Stop();
            bttn_sound(); //cue button click sound
        }

        private void QuitButton_Click( object sender, RoutedEventArgs e )
		{
			Application.Current.Exit();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
            Passing pass = e.Parameter as Passing;
            //Removed main title page, and replaced it with a logo.
            if (pass != null)
            {
                mainTitle.Text = pass.text;
                mainTitle.FontSize = (double)pass.size;
                mainTitle.Foreground = pass.color;
            }
        }

        private void options_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), 7);
            bttn_sound(); //cue button click sound
        }
    }
}
