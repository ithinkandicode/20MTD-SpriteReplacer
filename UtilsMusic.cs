using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer
{
    /**
     * References » Unity:
     *   https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Networking.UnityWebRequest.html
     *   https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Networking.UnityWebRequestMultimedia.GetAudioClip.html
     * 
     * Vanilla music filenames:
     *   Title  = "Pretty Dungeon LOOP.ogg"   (mod target filename: "title.mp3")
     *   Battle = "Wasteland Combat Loop.ogg" (mod target filename: "battle.mp3")
     */
    class UtilsMusic
    {
        //TODO: integrate into FileLoader
        /**
         * Asynchronously load a music track
         * 
         * Example: await LoadMusicFromDisk("title", AudioType.MPEG);
         * Available audio types: https://docs.unity3d.com/2019.4/Documentation/ScriptReference/AudioType.html
         * Based heavily on: https://github.com/TormentedEmu/7DTD-A19-DMTMods/blob/master/TE_MenuMusic/Harmony/Harmony.cs
         */
        public static async Task<AudioClip> LoadMusicFromDisk(string SourceMusicDirectory, string musicFilename, AudioType audioType)
        {
            AudioClip audioClip = null;
            string musicPath = Path.Combine("file:///", SourceMusicDirectory, musicFilename);

            Log.LogDebug($"Loading music (title.mp3) from: {musicPath}");

            using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(musicPath, audioType))
            {
                webRequest.SendWebRequest();

                try
                {
                    while (!webRequest.isDone)
                    {
                        await Task.Delay(5);
                    }

                    if (webRequest.isNetworkError || webRequest.isHttpError)
                    {
                        Log.LogDebug(webRequest.error);
                        Log.LogDebug("Music file retrieval failed (network error)");
                    }
                    else
                    {
                        audioClip = DownloadHandlerAudioClip.GetContent(webRequest);
                        audioClip.name = "Custom " + musicFilename;
                    }
                }
                catch (Exception e)
                {
                    Log.LogError(e.Message);
                    Log.LogError("Failed to retrieve music file (general error).");
                }
            }

            return audioClip;
        }
    }
}
