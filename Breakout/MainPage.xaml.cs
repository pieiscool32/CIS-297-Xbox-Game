using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Gaming.Input;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Breakout
{
	/// <summary
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private DispatcherTimer timer;
		private bool aKeyDown = false;
		private bool dKeyDown = false;
        private bool paused = false;
        private bool add = true;

		private int players;
        private int accelBeforePause;
        private int count = 0;
        static private int options_paddleSpeed = 10;
        static private int options_ballSpeed = 5;
        static private int options_ballCount = 50;

		private double leftBound;
		private double rightBound;
		private double topBound;
		private double paddleTop;

		private List<Rectangle> bricks;
        private List<Ellipse> balls;
        private List<List<int>> acc;
        // 0 = spd 1 = angle
        private List<int> acceleration;
		private Gamepad controller;
        private Random random;
        private List<Ellipse> power;
        private List<List<int>> poweracc;
        private List<int> powerCount;
        private string[] music_list = new string[] { "Gamply_song (1).mp3.","Bauchamp_-_124_simple_beat.mp3", "Bauchamp_-_128_deep_tec_beat.mp3" };
        private MediaElement bttn, g_ply = new MediaElement();
       


		public MainPage()
		{
			// disable the TV safe display zone to get images to the very edge
			Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetDesiredBoundsMode( Windows.UI.ViewManagement.ApplicationViewBoundsMode.UseCoreWindow );

			this.InitializeComponent();

            // setup game timer
            timer = new DispatcherTimer();
            // add event handler to the Tick event
            timer.Tick += dispatcherTimer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 2);
       
			random = new Random();

			// ensure keypresses are captured no matter what UI element has the focus
			Window.Current.CoreWindow.KeyDown += KeyDown_Handler;
			Window.Current.CoreWindow.KeyUp += KeyUp_Handler;
		}

        private static MediaPlayer _mediaPlayer = new MediaPlayer();

        public static async Task PlayUsingMediaPlayerAsync()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("Zeds Dead x NGHTMRE - Frontlines (ft. GG Magree) [Premiere]_oAmQIDIK8RA_youtube.mp3");
            _mediaPlayer.AutoPlay = false;
            _mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);

            _mediaPlayer.MediaOpened += _mediaPlayer_MediaOpened;

            _mediaPlayer.IsLoopingEnabled = true;

        }

        private static void _mediaPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            sender.Play();
        }

        protected override void OnNavigatedTo( NavigationEventArgs e )
		{
            PlayUsingMediaPlayerAsync();

            players = (int)e.Parameter;
            acceleration = new List<int>();
            acc = new List<List<int>>();
            balls = new List<Ellipse>();
            power = new List<Ellipse>();
            poweracc = new List<List<int>>();
            powerCount = new List<int>();
            acceleration.Add(1);
            acceleration.Add(230 + random.Next(-20, 20));

            if (players == 7)
            {
                timer.Stop();

                displayOptions();
            }
            else
            {
                HideOptions();
                this.LeftWall.Visibility = Visibility.Visible;
                this.RightWall.Visibility = Visibility.Visible;
                this.topWall.Visibility = Visibility.Visible;
                this.Ball.Visibility = Visibility.Visible;
                this.BottomPaddle.Visibility = Visibility.Visible;
                setSizes();
                fillBricks();
                acceleration[0] = options_ballSpeed;
                timer.Start();
               
            }    
		}

        private void displayOptions()
        {
            this.Ball.Visibility = Visibility.Collapsed;
            this.BottomPaddle.Visibility = Visibility.Collapsed;
            this.LeftWall.Visibility = Visibility.Collapsed;
            this.RightWall.Visibility = Visibility.Collapsed;
            this.topWall.Visibility = Visibility.Collapsed;
            _mediaPlayer.Pause(); //pause song during option menu 
            this.ReturnGame.Visibility = Visibility.Collapsed;

            if (bricks != null)
            {
                foreach(var brick in bricks)
                {
                    brick.Visibility = Visibility.Collapsed;
                }
                for(int index = 0; index < acc.Count; index++)
                {
                    acc[index][0] = 0;
                    balls[index].Visibility = Visibility.Collapsed;
                }
                ReturnGame.Visibility = Visibility.Visible;
            }

            this.PaddleSpeedOptions.Visibility = Visibility.Visible;
            this.paddle_slow.Visibility = Visibility.Visible;
            this.paddle_medium.Visibility = Visibility.Visible;
            this.paddle_fast.Visibility = Visibility.Visible;
            this.ReturnMenu.Visibility = Visibility.Visible;
            this.BallSpeed.Visibility = Visibility.Visible;
            this.ball_fast.Visibility = Visibility.Visible;
            this.ball_medium.Visibility = Visibility.Visible;
            this.ball_slow.Visibility = Visibility.Visible;
            this.ball_one.Visibility = Visibility.Visible;
            this.ball_few.Visibility = Visibility.Visible;
            this.ball_many.Visibility = Visibility.Visible;
            this.BallCountOptions.Visibility = Visibility.Visible;
        }

        private void HideOptions()
        {
            this.Ball.Visibility = Visibility.Visible;
            this.BottomPaddle.Visibility = Visibility.Visible;
            this.LeftWall.Visibility = Visibility.Visible;
            this.RightWall.Visibility = Visibility.Visible;
            this.topWall.Visibility = Visibility.Visible;
            _mediaPlayer.Play() ; // resume playlist for song 
            if (bricks != null)
            {
                foreach (var brick in bricks)
                {
                    brick.Visibility = Visibility.Visible;
                }
                for (int index = 0; index < acc.Count; index++)
                {
                    acc[index][0] = 5;
                    balls[index].Visibility = Visibility.Visible;
                }
                acceleration[0] = options_ballSpeed;
                ReturnMenu.Content = "Return to Menu";
            }

            this.ReturnGame.Visibility = Visibility.Collapsed;
            this.PaddleSpeedOptions.Visibility = Visibility.Collapsed;
            this.paddle_slow.Visibility = Visibility.Collapsed;
            this.paddle_medium.Visibility = Visibility.Collapsed;
            this.paddle_fast.Visibility = Visibility.Collapsed;
            this.ReturnMenu.Visibility = Visibility.Collapsed;
            this.BallSpeed.Visibility = Visibility.Collapsed;
            this.ball_fast.Visibility = Visibility.Collapsed;
            this.ball_medium.Visibility = Visibility.Collapsed;
            this.ball_slow.Visibility = Visibility.Collapsed;
            this.ball_one.Visibility = Visibility.Collapsed;
            this.ball_few.Visibility = Visibility.Collapsed;
            this.ball_many.Visibility = Visibility.Collapsed;
            this.BallCountOptions.Visibility = Visibility.Collapsed;
        }

		private void fillBricks()
		{
			int ycap = (int)Math.Round(topBound * 0.5);
			bricks = new List<Rectangle>();

			double count = Math.Floor((rightBound - leftBound) / 100);
			int xoffset = (((int)rightBound - (int)leftBound) - ((int)count*100))/ 2;

			for (int row = (int)topBound - 60; row > (int)topBound - ycap; row -= 20)
			{
				SolidColorBrush color = getRandomColor();
                //Color randomColor = Color.FromArgb(255, (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
                for (int col = (int)leftBound + xoffset; col + 50 < (int)rightBound; col += 50)
                {
                    if(random.Next(1,15) != 10)
                    {
                        addBrick(row, col, color);
                    }
                }
            }
		}

		private void addBrick(double ypos, double xpos, SolidColorBrush color)
		{
			var brick = new Rectangle();
            brick.Fill = color;
            if (random.Next(1,30) == 10)
            {
                brick.Tag = "special";
                brick.Fill = getRandomColor();
            }
			brick.Name = $"Brick @ row: {ypos} col: {xpos}";
            brick.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            brick.HorizontalAlignment = HorizontalAlignment.Left;
			brick.VerticalAlignment = VerticalAlignment.Bottom;
			brick.Margin = new Thickness(xpos, 0, 0, ypos);
			brick.Height = 20;
			brick.Width = 50;
			Grid.Children.Add(brick);
			bricks.Add(brick);
		}

        private void addBall(double ypos, double xpos, SolidColorBrush color)
        {
            var ball = new Ellipse();
            ball.Name = $"Ball @ row: {ypos} col: {xpos}";
            ball.Fill = color;
            ball.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            ball.HorizontalAlignment = HorizontalAlignment.Left;
            ball.VerticalAlignment = VerticalAlignment.Bottom;
            ball.Margin = new Thickness(xpos, 0, 0, ypos);
            ball.Height = 10;
            ball.Width = 10;
            Grid.Children.Add(ball);
            balls.Add(ball);
        }

        private void addUp(double ypos, double xpos, SolidColorBrush color)
        {
            var ball = new Ellipse();
            ball.Name = $"Ball @ row: {ypos} col: {xpos}";
            ball.Fill = color;
            ball.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            ball.HorizontalAlignment = HorizontalAlignment.Left;
            ball.VerticalAlignment = VerticalAlignment.Bottom;
            ball.Margin = new Thickness(xpos, 0, 0, ypos);
            ball.Height = 20;
            ball.Width = 20;
            Grid.Children.Add(ball);
            power.Add(ball);
        }

        public void hitBrickCheck()
		{
            bool hit = false;
            List<bool> hits = new List<bool>();
            foreach(var ball in balls)
            {
                hits.Add(false);
            }
            List<Rectangle> delete = new List<Rectangle>();
			foreach(var brick in bricks)
			{
				if(Collide.hit(Ball, acceleration, brick) && !hit)
				{
                    if((string)brick.Tag == "special")
                    {
                        addUp(brick.Margin.Bottom, brick.Margin.Left, getRandomColor());
                        poweracc.Add(new List<int>() { 8, 270 });
                    }
                    delete.Add(brick);
                    hit = true;
                }
                for (int index = 0; index < balls.Count; index++)
                {
                    if (Collide.hit(balls[index], acc[index], brick) && !hits[index])
                    {
                        delete.Add(brick);
                        hits[index] = true;
                    }
                }
			}
            foreach(var brick in delete)
            {
                Grid.Children.Remove(brick);
                bricks.Remove(brick);
                count++;
                for (int index = 0; index < powerCount.Count; index++)
                {
                    powerCount[index]--;
                }
            }
		}

		private void setSizes()
		{
			double height = ((Frame)Window.Current.Content).ActualHeight;
			double width = ((Frame)Window.Current.Content).ActualWidth;

			topWall.Width = width - 60;
			LeftWall.Height = height - 60;
			RightWall.Height = height - 60;

			topBound = height - 31;
			leftBound = 31;
			rightBound = width - 31;
			paddleTop = BottomPaddle.Margin.Bottom + BottomPaddle.Height;
		}

        private void moveBalls()
        {
            for(int index = 0; index < balls.Count; index++)
            {
                Collide.wall(balls[index], acc[index], leftBound, 'l');
                Collide.wall(balls[index], acc[index], rightBound, 'r');
                Collide.wall(balls[index], acc[index], topBound, 't');

                double xmov = acc[index][0] * Math.Cos((acc[index][1] * Math.PI) / 180);
                double ymov = acc[index][0] * Math.Sin((acc[index][1] * Math.PI) / 180);
                balls[index].Margin = new Thickness(balls[index].Margin.Left + xmov, 0, 0, balls[index].Margin.Bottom + ymov);
            }
        }

        private void BallFall()
        {
            List<int> fall = new List<int>();
            for(int index = 0; index < balls.Count; index++)
            {
                if(balls[index].Margin.Bottom < paddleTop - 5)
                {
                    fall.Add(index);
                }
            }
            foreach(int delete in fall)
            {
                Grid.Children.Remove(balls[delete]);
                balls.RemoveAt(delete);
                acc.RemoveAt(delete);
            }
        }

		private void dispatcherTimer_Tick( object sender, object e )
		{
            MovePaddles();
            acceleration[0] = acceleration[0] < 0 ? 0 : acceleration[0];
            acceleration[0] = acceleration[0] > 10 ? 10 : acceleration[0];
            if (paused)
            {
                acceleration[0] = 0;
            }
			EnsurePaddleIsInBounds(BottomPaddle);
			hitBrickCheck();

            if(power != null)
            {
                int rem = -1;
                for(int index = 0; index < power.Count; index++)
                {
                    if (Collide.hit(power[index], poweracc[index], BottomPaddle))
                    {
                        Grid.Children.Remove(power[index]);
                        poweracc.RemoveAt(index);
                        power.RemoveAt(index);
                        powerCount.Add(10);
                        BottomPaddle.Width = BottomPaddle.Width * 1.5;
                    }
                    else if (power[index].Margin.Bottom < paddleTop - 5)
                    {
                        Grid.Children.Remove(power[index]);
                        poweracc.RemoveAt(index);
                        power.RemoveAt(index);
                    }
                    else
                    {
                        double mov = poweracc[index][0] * Math.Sin((poweracc[index][1] * Math.PI) / 180);
                        power[index].Margin = new Thickness(power[index].Margin.Left, 0, 0, power[index].Margin.Bottom + mov);
                    }
                }
                for(int index = 0; index < powerCount.Count; index++)
                {
                    if(powerCount[index] <= 0)
                    {
                        rem = index;
                    }
                }
                if (rem != -1)
                {
                    powerCount.RemoveAt(rem);
                    BottomPaddle.Width = (BottomPaddle.Width * 2) / 3;
                }
            }

            if(count >= options_ballCount && add)
            {
                count = 0;
                addBall(paddleTop + 20, leftBound + 20, getRandomColor());
                acc.Add(new List<int>() { 5, 40 + random.Next(-10, 10) });
            }

            BallFall();
			double bottom = Ball.Margin.Bottom;

			if (bottom < paddleTop - 5)
			{
				timer.Stop();
				ElementSoundPlayer.Play(ElementSoundKind.GoBack);
				Frame.Navigate(typeof(Start), new Passing
				{

					color = getRandomColor(),
					text = "Game Over",
					size = 48
				});

                g_ply.Stop(); //end music 
            }

			if (bricks.Count == 0)
			{
				timer.Stop();
				ElementSoundPlayer.Play( ElementSoundKind.GoBack );
				Frame.Navigate(typeof(Start), new Passing {
					color = getRandomColor(),
					text = "Game Won",
					size = 48
				});
			}

            moveBalls();
            for (int index = 0; index < balls.Count; index++)
            {
                Collide.hit(balls[index], acc[index], BottomPaddle);
            }

            Collide.wall(Ball, acceleration, leftBound, 'l');
            Collide.wall(Ball, acceleration, rightBound, 'r');
            Collide.wall(Ball, acceleration, topBound, 't');

			Collide.hit(Ball, acceleration, BottomPaddle);
			// x left-right: cos y up-down: sin
			// !! remeber, 0 deg is --> NOT NORTH !!
			double xmov = acceleration[0] * Math.Cos((acceleration[1] * Math.PI) / 180);
			double ymov = acceleration[0] * Math.Sin((acceleration[1] * Math.PI) / 180);
			Ball.Margin = new Thickness(Ball.Margin.Left + xmov, 0, 0, Ball.Margin.Bottom + ymov);
		}

		private SolidColorBrush getRandomColor()
		{
			return new SolidColorBrush( Color.FromArgb( 255, (byte)random.Next( 100, 250 ), (byte)random.Next( 100, 250 ), (byte)random.Next( 100, 250 ) ) );
		}

		private void MovePaddles()
		{
            if (Gamepad.Gamepads.Count > 0)
            {
                controller = Gamepad.Gamepads.First();
                var reading = controller.GetCurrentReading();

                if (reading.Buttons.HasFlag(GamepadButtons.A))
                {
                    acceleration[0] += 1;
                }
                if (reading.Buttons.HasFlag(GamepadButtons.B))
                {
                    acceleration[0] -= 1;
                }
                if(reading.Buttons.HasFlag(GamepadButtons.Y))
                {
                    addBall(paddleTop + 20, leftBound + 20, getRandomColor());
                    acc.Add(new List<int>() { 5, 40 + random.Next(-10, 10) });
                }
                if (reading.Buttons.HasFlag(GamepadButtons.Menu))
                {
                    if (!paused)
                    {
                        accelBeforePause = acceleration[0];
                        acceleration[0] = 0;
                        paused = true;
                        displayOptions();
                    }
                }
            }

            if (players == 0)
            {
                ComputerMovePaddle(BottomPaddle);
            }
            else if (Gamepad.Gamepads.Count > 0)
            {
                controller = Gamepad.Gamepads.First();
                var reading = controller.GetCurrentReading();
                if (reading.LeftThumbstickX < -.1 || reading.LeftThumbstickX > 0.1)
                {
                    if (!paused)
                    {
                        BottomPaddle.Margin = new Thickness(BottomPaddle.Margin.Left + options_paddleSpeed * reading.LeftThumbstickX, 0, 0, 25);
                    }
                }
            }
            else
            {

                if(!paused)
                {
                    if (aKeyDown)
                    {
                        BottomPaddle.Margin = new Thickness(BottomPaddle.Margin.Left - options_paddleSpeed, 0, 0, 25);
                    }
                    if (dKeyDown)
                    {
                        BottomPaddle.Margin = new Thickness(BottomPaddle.Margin.Left + options_paddleSpeed, 0, 0, 25);
                    }
                }
            }
        }

        private int lowestBall()
        {
            int height = (int)topBound, pos = (int)rightBound;
            foreach(var ball in balls)
            {
                if (ball.Margin.Bottom < height)
                {
                    height = (int)ball.Margin.Bottom;
                    pos = (int)ball.Margin.Left;
                }
            }
            if(Ball.Margin.Bottom < height)
            {
                height = (int)Ball.Margin.Bottom;
                pos = (int)Ball.Margin.Left;
            }
            return pos;
        }

		private void ComputerMovePaddle( Rectangle paddle )
		{
			double top, bottom;
			if(paddle.VerticalAlignment == VerticalAlignment.Top)
			{
				top = 25;
				bottom = 0;
			}
			else
			{
				top = 0;
				bottom = 25;
			}
			paddle.Margin = new Thickness( lowestBall() - ( paddle.Width / 2 ), top, 0, bottom );
		}

		private void EnsurePaddleIsInBounds(Rectangle paddle)
		{
			double top, bottom;
			if (paddle.VerticalAlignment == VerticalAlignment.Top)
			{
				top = 25;
				bottom = 0;
			}
			else
			{
				top = 0;
				bottom = 25;
                
			}
			if ( paddle.Margin.Left < LeftWall.Margin.Left )
			{
				paddle.Margin = new Thickness( leftBound, top, 0, bottom );
			}
			else if ( paddle.Margin.Left + paddle.Width > rightBound )
			{
				paddle.Margin = new Thickness( rightBound - paddle.Width, top, 0, bottom );
			}
		}

		private void KeyDown_Handler( Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e )
		{
			if ( e.VirtualKey == Windows.System.VirtualKey.A )
			{
				aKeyDown = true;
			}
			else if ( e.VirtualKey == Windows.System.VirtualKey.D )
			{
				dKeyDown = true;
			}
            else if (e.VirtualKey == Windows.System.VirtualKey.W)
            {
                if (paused != true)
                {
                    acceleration[0] += 1;
                }
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.S)
            {
                if (paused != true)
                {
                    acceleration[0] -= 1;
                }
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.Q || e.VirtualKey == Windows.System.VirtualKey.Escape)
            {
                if (!paused)
                {
                    accelBeforePause = acceleration[0];
                    acceleration[0] = 0;
                    paused = true;
                    displayOptions();
                }
                
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.B)
            {
                addBall(paddleTop + 20, leftBound + 20, getRandomColor());
                acc.Add(new List<int>() { 5, 40 + random.Next(-10, 10) });
            }
        }

		private void KeyUp_Handler( Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e )
		{
			if ( e.VirtualKey == Windows.System.VirtualKey.A )
			{
				aKeyDown = false;
			}
			else if ( e.VirtualKey == Windows.System.VirtualKey.D )
			{
				dKeyDown = false;
			}

		}
        
        private void ReturnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.PaddleSpeedOptions.Visibility = Visibility.Collapsed;
            this.paddle_slow.Visibility = Visibility.Collapsed;
            this.paddle_medium.Visibility = Visibility.Collapsed;
            this.paddle_fast.Visibility = Visibility.Collapsed;
            this.ReturnMenu.Visibility = Visibility.Collapsed;
            this.BallSpeed.Visibility = Visibility.Collapsed;
            this.ball_fast.Visibility = Visibility.Collapsed;
            this.ball_medium.Visibility = Visibility.Collapsed;
            this.ball_slow.Visibility = Visibility.Collapsed;

            timer.Stop();

            g_ply.Stop(); //stop game_play music 


            Frame.Navigate(typeof(Start));
        }

        private void ReturnGame_Click(object sender, RoutedEventArgs e)
        {
            acceleration[0] = accelBeforePause;
            paused = false;
            HideOptions();
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

        private void paddle_fast_Click(object sender, RoutedEventArgs e)
        {
            options_paddleSpeed = 20;
            bttn_sound(); // cue button click

        }

        private void paddle_medium_Click(object sender, RoutedEventArgs e)
        {
            options_paddleSpeed = 10;
            bttn_sound(); // cue button click
        }

        private void paddle_slow_Click(object sender, RoutedEventArgs e)
        {
            options_paddleSpeed = 5;
            bttn_sound(); // cue button click
        }

        private void ball_fast_Click(object sender, RoutedEventArgs e)
        {
            options_ballSpeed = 7;
            bttn_sound(); // cue button click
        }

        private void ball_medium_Click(object sender, RoutedEventArgs e)
        {
            options_ballSpeed = 5;
            bttn_sound(); // cue button click
        }

        private void ball_one_Click(object sender, RoutedEventArgs e)
        {
            add = false;
            bttn_sound(); // cue button click
        }

        private void ball_few_Click(object sender, RoutedEventArgs e)
        {
            options_ballCount = 80;
            bttn_sound(); // cue button click
        }

        private void ball_many_Click(object sender, RoutedEventArgs e)
        {
            options_ballCount = 40;
            bttn_sound(); // cue button click
        }

        private void ball_slow_Click(object sender, RoutedEventArgs e)
        {
            options_ballSpeed = 2;
            bttn_sound(); // cue button click
        }
    }
}
