using FitCycle.Services;
using FitCycle.UWP;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(UWPSoundManager))]
namespace FitCycle.UWP
{
    public class UWPSoundManager : ISoundManager
    {
        public UWPSoundManager()
        {

        }

        public async void PlayAudioFile(string fileName)
        {
            var element = new Windows.UI.Xaml.Controls.MediaElement();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("MyFolder");
            var file = await folder.GetFileAsync(fileName);
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }
    }
}
