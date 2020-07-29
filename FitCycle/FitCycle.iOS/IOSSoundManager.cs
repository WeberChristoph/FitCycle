using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AVFoundation;
using FitCycle.iOS;
using FitCycle.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSSoundManager))]
namespace FitCycle.iOS
{
    public class IOSSoundManager : ISoundManager
    {
        public IOSSoundManager()
        {
        }

        public void PlayAudioFile(string fileName)
        {
            string sFilePath = NSBundle.MainBundle.PathForResource
            (Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            var url = NSUrl.FromString(sFilePath);
            var _player = AVAudioPlayer.FromUrl(url);
            _player.FinishedPlaying += (object sender, AVStatusEventArgs e) => {
                _player = null;
            };
            _player.Play();
        }
    }
}