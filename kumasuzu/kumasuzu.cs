using System;
using System.Threading;
using Xamarin.Forms;

namespace kumasuzu
{

	public class App : Application
	{
		private Boolean started = false;
		private static string START = "開始";
		private static string STOP = "停止";

		public App()
		{
			var soundPlayer = DependencyService.Get<IPlatformSoundPlayer>();

			CancellationTokenSource cancelTokenSource = null;
			Timer timer;

			var button = createCircularButton();
			button.Text = START;

			button.Clicked += (sender, e) =>
			{
				started = !started;
				if (started)
				{
					cancelTokenSource = new CancellationTokenSource();
					timer = new Timer((obj) => soundPlayer.playSound(),
					                  null,
					                  0,
					                  5000,
					                  cancelTokenSource);
					button.Text = STOP;
					button.BackgroundColor = Color.White;
					button.TextColor = Color.Accent;
				}
				else {
					cancelTokenSource.Cancel();

					button.Text = START;
					button.BackgroundColor = Color.Accent;
					button.TextColor = Color.White;
				}
			};

			// The root page of your application
			var content = new ContentPage
			{
				Title = "クマ鈴",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						button
					}
				}
			};

			MainPage = new NavigationPage(content);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		private Button createCircularButton()
		{
			const int BUTTON_BORDER_WIDTH = 1;
			const int BUTTON_HEIGHT = 132;
			const int BUTTON_HEIGHT_WP = 216;
			const int BUTTON_HALF_HEIGHT = 66;
			const int BUTTON_HALF_HEIGHT_WP = 108;
			const int BUTTON_WIDTH = 132;
			const int BUTTON_WIDTH_WP = 216;
			const int FONT_SIZE = 27;
			const int FONT_SIZE_WP = 36;

			return new Button
			{
				HorizontalOptions = LayoutOptions.Center,
		        BackgroundColor = Color.Accent,
		        BorderColor = Color.Black,
		        TextColor = Color.White,
				FontSize = Device.OnPlatform(FONT_SIZE,FONT_SIZE,FONT_SIZE_WP),
				FontAttributes = FontAttributes.Bold,
		        BorderWidth = BUTTON_BORDER_WIDTH,
		        BorderRadius = Device.OnPlatform(BUTTON_HALF_HEIGHT, BUTTON_HALF_HEIGHT, BUTTON_HALF_HEIGHT_WP),
		        HeightRequest = Device.OnPlatform(BUTTON_HEIGHT, BUTTON_HEIGHT, BUTTON_HEIGHT_WP),
		        MinimumHeightRequest = Device.OnPlatform(BUTTON_HEIGHT, BUTTON_HEIGHT, BUTTON_HEIGHT_WP),
		        WidthRequest = Device.OnPlatform(BUTTON_WIDTH, BUTTON_WIDTH, BUTTON_WIDTH_WP),
		        MinimumWidthRequest = Device.OnPlatform(BUTTON_WIDTH, BUTTON_WIDTH, BUTTON_WIDTH_WP)
			};
		}
	}
}
