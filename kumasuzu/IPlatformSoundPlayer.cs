using System;
namespace kumasuzu
{
	public interface IPlatformSoundPlayer
	{
		void PlaySound(int samplingRate, byte[] pcmData);
	}
}
