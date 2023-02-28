using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibVLCTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LibVLC _libvlc;

        public MainWindow()
        {
            InitializeComponent();
            _libvlc= new LibVLC();
/*
            foreach (var videoView in VideoPlayers.Children.OfType<VideoView>())
            {
                videoView.MediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libvlc);
                videoView.MediaPlayer.Play(new Media(_libvlc, uri: new Uri("rtsp://10.10.1.41:8554/abandoned_1")));
            }*/

           
            //VideoView.MediaPlayer.
        }
    }
}
