using System.Collections.Generic;
using System.IO;
using Exiled.API.Features;
using MEC;

namespace Cruxifix
{
    public static class AudioPlayerManager
    {
        private static bool _isAudioPlaying;

        public static void PlaySpatialAudio(this Player player, float maxDistance, float volume, float duration)
        {
            string globalPlayerName = GenerateRandomString(6);
            string speakerName = GenerateRandomString(6);
            string clipName = GenerateRandomString(6);

            string clipFullPath = Plugin.Singleton.Config.AudioPath;
            AudioClipStorage.LoadClip(clipFullPath, clipName);

            var audioSource = AudioPlayer.Create(globalPlayerName);
            var speaker = audioSource.AddSpeaker(speakerName, 1f, true, 1, maxDistance);

            speaker.Position = player.Position;
            _isAudioPlaying = true;

            Timing.RunCoroutine(UpdateSpeakerPositionRoutine(speaker, player, duration));

            audioSource.AddClip(clipName, volume, false, true);

            Timing.CallDelayed(duration, () =>
            {
                if (!Round.IsEnded)
                {
                    StopAudio(audioSource, speaker, speakerName, clipName);
                }
            });

            if (Round.IsEnded)
            {
                StopAudio(audioSource, speaker, speakerName, clipName);
            }
        }

        private static void StopAudio(AudioPlayer source, Speaker speaker, string speakerName, string clipName)
        {
            _isAudioPlaying = false;
            AudioClipStorage.DestroyClip(clipName);
            speaker.Destroy();
            source.RemoveSpeaker(speakerName);
            source.Destroy();
        }

        private static IEnumerator<float> UpdateSpeakerPositionRoutine(Speaker speaker, Player player, float duration)
        {
            while (_isAudioPlaying)
            {
                speaker.transform.position = player.Transform.position;
                yield return Timing.WaitForOneFrame;
            }
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new System.Random();
            var result = new char[length];

            for (int i = 0; i < length; i++)
                result[i] = chars[random.Next(chars.Length)];

            return new string(result);
        }
    }
}
