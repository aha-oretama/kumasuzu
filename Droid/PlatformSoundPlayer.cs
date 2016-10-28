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
			mediaPlayer = MediaPlayer.Create(this, Resource.Raw.sei_ge_suzu02).Start();
			mediaPlayer.Prepare();
		}

		public void playSound()
		{
			mediaPlayer.Start();
		}
	}
}
