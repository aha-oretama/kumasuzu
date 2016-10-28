using System;
using Android.Media;
using Java.IO;
using kumasuzu.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformSoundPlayer))]

namespace kumasuzu.Droid
{
	public class PlatformSoundPlayer:IPlatformSoundPlayer
	{
		MediaPlayer mediaPlayer;

		public PlatformSoundPlayer(String uri)
		{
			mediaPlayer = new MediaPlayer();
			mediaPlayer.SetDataSource("Sounds/sei_ge_suzu02.mp3");
			mediaPlayer.Prepare();
		}

		public void playSound()
		{
			mediaPlayer.Start();
		}
	}
}
