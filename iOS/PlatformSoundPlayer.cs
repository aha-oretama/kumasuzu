using System;
using AudioToolbox;
using Foundation;
using kumasuzu.iOS;


[assembly: Xamarin.Forms.Dependency(typeof(PlatformSoundPlayer))]

namespace kumasuzu.iOS
{
	public class PlatformSoundPlayer:IPlatformSoundPlayer
	{

		NSUrl url;
		SystemSound systemSound;

		public PlatformSoundPlayer()
		{
			url = NSUrl.FromFilename("Sounds/suzu.mp3");
			systemSound = new SystemSound(url);
		}

		public void playSound()
		{
			systemSound.PlaySystemSound();
		}

	}
}
