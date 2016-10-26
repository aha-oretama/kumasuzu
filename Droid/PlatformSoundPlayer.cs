using System;
using kumasuzu.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformSoundPlayer))]

namespace kumasuzu.Droid
{
	public class PlatformSoundPlayer:IPlatformSoundPlayer
	{
		public void PlaySound(int samplingRate, byte[] pcmData)
		{
			throw new NotImplementedException();
		}
	}
}
