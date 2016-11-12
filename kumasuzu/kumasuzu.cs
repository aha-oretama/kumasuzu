using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace kumasuzu
{

	public class App : Application
	{
		private Boolean started = false;
		private static string START_IMAGE_PATH = "kumasuzu.images.kumasuzu_startButton.png";
		private static string STOP_IMAGE_PATH = "kumasuzu.images.kumasuzu_stopButton.png";

		public App()
		{
			var soundPlayer = DependencyService.Get<IPlatformSoundPlayer>();

			CancellationTokenSource cancelTokenSource = null;
			Timer timer;

			var image = new Image
			{
				Aspect = Aspect.AspectFit,
				Source = ImageSource.FromResource(START_IMAGE_PATH)
			};

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += async (sender, e) =>
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

					image.Opacity = 0.5;
					await Task.Delay(150);
					image.Opacity = 1.0;
					image.Source = ImageSource.FromResource(STOP_IMAGE_PATH);
				}
				else {
					cancelTokenSource.Cancel();

					image.Opacity = 0.5;
					await Task.Delay(150);
					image.Opacity = 1.0;
					image.Source = ImageSource.FromResource(START_IMAGE_PATH);
				}
			};

			image.GestureRecognizers.Add(tapGestureRecognizer);

			// The root page of your application
			var content = new ContentPage
			{
				Title = "クマ鈴",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						image
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
	}
}
