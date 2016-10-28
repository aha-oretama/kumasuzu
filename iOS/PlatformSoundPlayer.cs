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
			url = NSUrl.FromFilename("Sounds/sei_ge_suzu02.mp3");
			systemSound = new SystemSound(url);
		}

		public void playSound()
		{
			systemSound.PlaySystemSound();
		}

	}
}
