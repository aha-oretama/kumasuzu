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

		private static int DEFAULT_DISPLAY_INTERVAL = 5;

		private double STEP_VALUE = 1.0;

		public App()
		{
			var soundPlayer = DependencyService.Get<IPlatformSoundPlayer>();

			CancellationTokenSource cancelTokenSource = null;
			Timer timer = null;

			// スタート、ストップボタン
			var image = new Image
			{
				VerticalOptions = LayoutOptions.Center,
				HeightRequest = 150,
				Aspect = Aspect.AspectFit,
				Source = ImageSource.FromResource(START_IMAGE_PATH)
			};

			// Labelを生成する
			var label = new Label
			{
				Text = DEFAULT_DISPLAY_INTERVAL.ToString(),
				HorizontalOptions = LayoutOptions.Center,//中央に配置する（横方向）
			};

			// スライダー
			var slider = new Slider
			{
				WidthRequest = 300, // サイズ
				Minimum = 0.0, // 最小値
				Maximum = 10.0, // 最大値
				Value = (double)SliderUtils.convertToValue(DEFAULT_DISPLAY_INTERVAL), // デフォルト
				HorizontalOptions = LayoutOptions.Center,//中央に配置する（横方向）
			};

			// スライダーの値が変化したときのイベント処理
			slider.ValueChanged += (s, e) =>
			{
				// スライダーは整数値のみ
				var newStep = (int)(Math.Round(e.NewValue / STEP_VALUE));
				slider.Value = newStep * STEP_VALUE;

				// ラベルに変化した値を表示する
				var displayStep = SliderUtils.convertToDisplay(newStep);
				label.Text = displayStep.ToString();

				// スライダーの変化い合わせて、再生時間を調整
				if (started)
				{
					cancelTokenSource.Cancel();

					cancelTokenSource = new CancellationTokenSource();
					timer = new Timer((obj) => soundPlayer.playSound(),
									  null,
									  (int)(displayStep * 1000),
									  (int)(displayStep * 1000),
									  cancelTokenSource);
				}
			};


			// ボタンに対するタップ処理
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
					                  (int)(SliderUtils.convertToDisplay(slider.Value) * 1000),
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
				Title = "くますず",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						label,
						slider,
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
