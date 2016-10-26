using System;
using kumasuzu.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformSoundPlayer))]
namespace kumasuzu.iOS
{
	public class PlatformSoundPlayer:IPlatformSoundPlayer
	{
		public PlatformSoundPlayer()
		{
		}

		public void PlaySound(int samplingRate, byte[] pcmData)
		{
			throw new NotImplementedException();
		}
	}
}
