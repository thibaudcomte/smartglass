using SmartGlass.Video.Service;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SmartGlass.Video.Controls
{
    public sealed partial class YoutubePlayer : UserControl
    {
        public YoutubePlayer()
        {
            InitializeComponent();
            webView.Navigate(new Uri("ms-appx-web:///SmartGlass.Video/Controls/videostream.master.html"));
        }

        #region VideoId

        public string VideoId
        {
            get { return (string)GetValue(VideoIdProperty); }
            set { SetValue(VideoIdProperty, value); }
        }

        public static readonly DependencyProperty VideoIdProperty =
            DependencyProperty.Register("VideoId", typeof(string),
                typeof(YoutubePlayer), new PropertyMetadata("", OnVideoIdPropertyChanged));

        private async static void OnVideoIdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as YoutubePlayer;
            if (me == null) return;

            await me.SetVideoIdAsync(e.NewValue.ToString());
        }

        private async Task SetVideoIdAsync(string id)
        {
            await webView?.InvokeScriptAsync("setVideoId", new string[] { id });
        }

        #endregion // VideoId

        #region PlaybackState

        public VideoPlaybackState PlaybackState
        {
            get { return (VideoPlaybackState)GetValue(PlaybackStateProperty); }
            set { SetValue(PlaybackStateProperty, value); }
        }

        public static readonly DependencyProperty PlaybackStateProperty =
            DependencyProperty.Register("PlaybackState", typeof(VideoPlaybackState),
                typeof(YoutubePlayer), new PropertyMetadata(VideoPlaybackState.Stopped, OnPlaybackStatePropertyChanged));

        private async static void OnPlaybackStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as YoutubePlayer;
            if (me == null) return;

            await me.SetPlaybackStateAsync((VideoPlaybackState)e.NewValue);
        }

        private async Task SetPlaybackStateAsync(VideoPlaybackState state)
        {
            string function = string.Empty;

            switch (state)
            {
                case VideoPlaybackState.Stopped:
                    function = "stop";
                    break;
                case VideoPlaybackState.Playing:
                    function = "play";
                    break;
                case VideoPlaybackState.Paused:
                    function = "pause";
                    break;
            }

            await webView?.InvokeScriptAsync(function, null);
        }

        #endregion // PlaybackState

        #region Volume

        public int Volume
        {
            get { return (int)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        public static readonly DependencyProperty VolumeProperty =
            DependencyProperty.Register("Volume", typeof(int),
                typeof(YoutubePlayer), new PropertyMetadata(50, OnVolumePropertyChanged));

        private async static void OnVolumePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as YoutubePlayer;
            if (me == null) return;

            await me.SetVolumeAsync((int)e.NewValue);
        }

        private async Task SetVolumeAsync(int volume)
        {
            await webView?.InvokeScriptAsync("setVideoVolume", new string[] { volume.ToString() });
        }

        #endregion // Volume

        #region IsMuted

        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        public static readonly DependencyProperty IsMutedProperty =
            DependencyProperty.Register("IsMuted", typeof(bool),
                typeof(YoutubePlayer), new PropertyMetadata(false, OnIsMutedPropertyChanged));

        private async static void OnIsMutedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as YoutubePlayer;
            if (me == null) return;

            await me.Mute((bool)e.NewValue);
        }

        private async Task Mute(bool mute = true)
        {
            await webView?.InvokeScriptAsync(mute ? "mute" : "unmute", null);
        }

        #endregion // IsMuted
    }
}
