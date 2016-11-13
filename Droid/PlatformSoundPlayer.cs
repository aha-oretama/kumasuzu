using System;
using Android.Media;
using kumasuzu.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformSoundPlayer))]
	
namespace kumasuzu.Droid
{
	public class PlatformSoundPlayer:IPlatformSoundPlayer
	{
		private MediaPlayer mediaPlayer;

		public PlatformSoundPlayer()
		{
			mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.suzu);
		}

		public void playSound()
		{
			mediaPlayer.Start();
		}
	}
}
