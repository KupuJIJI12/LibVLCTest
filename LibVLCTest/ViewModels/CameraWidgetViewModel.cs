using LibVLCSharp.Shared;
using LibVLCTest.Models;
using LibVLCTest.MVVM.Base;
using OCode.MVVM.Base;
using System;
using System.Threading;
using System.Windows;

namespace LibVLCTest.ViewModels
{
    internal class CameraWidgetViewModel : ViewModelBase
    {
        public static LibVLC _libVlc;

        public MediaPlayer MediaPlayer { get; private set; }

        public bool IsWorking => MediaPlayer is not null
            && MediaPlayer.IsPlaying;
        public bool IsConnected => Status == Status.Receiving;

        public Status Status
        {
            get => _status;
            set
            {
                Set(ref _status, value);
            }
        }
        private Status _status = Status.Stopped;

        public bool AreNotificationsOn
        {
            get => _areNotificationsOn;
            set
            {
                Set(ref _areNotificationsOn, value);
            }
        }
        private bool _areNotificationsOn = false;

        public CameraInfoModel CameraInfo
        {
            get => _cameraInfo;
            set
            {
                Set(ref _cameraInfo, value);
            }
        }
        private CameraInfoModel _cameraInfo;

        public RelayCommand OnLoad =>
             _onLoad ??= new RelayCommand(args =>
             {
                 NotifyPropertyChanged(nameof(MediaPlayer));
                 MediaPlayer?.Play();
             });
        private RelayCommand _onLoad;

        public RelayCommand OnUnload =>
            _onUnload ??= new RelayCommand(_ =>
            {
                Stop();
            });

        private RelayCommand _stopVideo;

        public RelayCommand StopVideo =>
           _stopVideo ??= new RelayCommand(_ =>
           {
               Stop();
           });
        private RelayCommand _onUnload;

        public CameraWidgetViewModel(int id, string deviceAddress, string name)
        {
            CameraInfo = new()
            {
                Id = id,
                Address = deviceAddress,
                Name = name,
                NewName = name,
            };

            _libVlc ??= new LibVLC();

            MediaPlayer = new MediaPlayer(_libVlc);
            using var media = new Media(_libVlc, new Uri(CameraInfo.Address));
            media.AddOption(":no-audio");
            media.AddOption(":network-caching=150");
            media.AddOption(":sout-mux-caching=150");
            MediaPlayer.Media = media;
        }

        public void Start()
        {
            if (!Uri.TryCreate(CameraInfo.Address, UriKind.Absolute, out Uri deviceUri))
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (MediaPlayer?.Media == null)
                {
                    using var media = new Media(_libVlc, deviceUri);
                    media.AddOption(":no-audio");
                    media.AddOption(":preferred-resolution=240");
                    media.AddOption(":sout-avcodec-hurry-up");
                    media.AddOption(":swscale-mode=0");
                    media.AddOption(":sout-chromecast-conversion-quality=3");
                    media.AddOption(":postproc-q=0");
                    media.AddOption(":sout-x264-psnr");
                    MediaPlayer = new MediaPlayer(media);

                    NotifyPropertyChanged(nameof(MediaPlayer));
                }
                MediaPlayer.Play();

            });
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        public void Close()
        {
            MediaPlayer.Stop();
        }

        private void MainWindowModelOnStatusChanged(object sender, Status status)
        {
            Status = status;
            NotifyPropertyChanged(nameof(IsWorking));
            NotifyPropertyChanged(nameof(MediaPlayer));
            NotifyPropertyChanged(nameof(IsConnected));
        }
    }
}
