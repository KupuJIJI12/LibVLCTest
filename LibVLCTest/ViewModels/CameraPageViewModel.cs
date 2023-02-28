using LibVLCTest.MVVM.Base;
using OCode.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibVLCTest.ViewModels
{
    internal class CameraPageViewModel : ViewModelPageBase
    {
        private const int _camerasPerPage = 6;

        //public ObservableCollection<CameraWidgetViewModel> VisibleCameraWidgets { get; set; }
        public Dictionary<int, CameraWidgetViewModel> CameraWidgetVMs { get; } = new Dictionary<int, CameraWidgetViewModel>()
        {
            {1, new CameraWidgetViewModel(1, "https://streamer.camera.rt.ru/public/master.m3u8?sid=3093e499-e57b-4445-8189-cfca33810bfb", "test1") },
            {2, new CameraWidgetViewModel(2, "https://msk.rtsp.me/fFh2hyaOt4hJQvDlPX47vg/1677579402/hls/2DRNsQ56.m3u8?ip=46.29.77.209", "test2") },
            {3, new CameraWidgetViewModel(3, "https://itl.rtsp.me/7rVGH77gd2416Ga-uwfTSw/1677579435/hls/3D7Gbh5F.m3u8?ip=46.29.77.209", "test3") },
            {4, new CameraWidgetViewModel(4, "https://streamer.camera.rt.ru/public/master.m3u8?sid=3093e499-e57b-4445-8189-cfca33810bfb", "test4") },
            {5, new CameraWidgetViewModel(5, "https://streamer.camera.rt.ru/public/master.m3u8?sid=3093e499-e57b-4445-8189-cfca33810bfb", "test5") },
            {6, new CameraWidgetViewModel(6, "https://streamer.camera.rt.ru/public/master.m3u8?sid=3093e499-e57b-4445-8189-cfca33810bfb", "test6") },
            {7, new CameraWidgetViewModel(7, "https://streamer.camera.rt.ru/public/master.m3u8?sid=3093e499-e57b-4445-8189-cfca33810bfb", "test7") },
            {8, new CameraWidgetViewModel(8, "https://streamer.camera.rt.ru/public/master.m3u8?sid=3093e499-e57b-4445-8189-cfca33810bfb", "test8") }
        };

        public ObservableCollection<CameraWidgetViewModel> VisibleCameraWidgets { get; set; } = new ObservableCollection<CameraWidgetViewModel>();

        public int PagesCount
        {
            get
            {
                var result = CameraWidgetVMs.Count / _camerasPerPage;
                return result == 0 || CameraWidgetVMs.Count % _camerasPerPage != 0
                    ? ++result : result;
            }
        }

        public bool IsNextEnabled
        {
            get => _isNextEnabled;
            set => Set(ref _isNextEnabled, value);
        }
        private bool _isNextEnabled;

        public bool IsPreviousEnabled
        {
            get => _isPreviousEnabled;
            set => Set(ref _isPreviousEnabled, value);
        }
        private bool _isPreviousEnabled;

        public int CurrentPageNumber
        {
            get => _currentPageNumber;
            set
            {
                Set(ref _currentPageNumber, value);
                UpdateNavigationButtonsEnabledState();
            }
        }
        private int _currentPageNumber = 1;


        public RelayCommand FirstCameraPageCommand =>
            _firstCameraPageCommand ??= new RelayCommand(_ =>
            {
                CurrentPageNumber = 1;
                RefreshDetailCollection();
            });
        private RelayCommand _firstCameraPageCommand;

        public RelayCommand NextCameraPageCommand =>
            _nextCameraPageCommand ??= new RelayCommand(_ =>
            {
                if (IsNextEnabled)
                {
                    CurrentPageNumber++;
                    RefreshDetailCollection();
                }
            });
        private RelayCommand _nextCameraPageCommand;

        public RelayCommand PreviousCameraPageCommand =>
            _previousCameraPageCommand ??= new RelayCommand(_ =>
            {
                if (IsPreviousEnabled)
                {
                    CurrentPageNumber--;
                    RefreshDetailCollection();
                }
            });
        private RelayCommand _previousCameraPageCommand;

        public RelayCommand LastCameraPageCommand =>
            _lastCameraPageCommand ??= new RelayCommand(_ =>
            {
                CurrentPageNumber = PagesCount;
                RefreshDetailCollection();
            });
        private RelayCommand _lastCameraPageCommand;

        public override RelayCommand OnPageLoadedCommand =>
            _onPageLoadedCommand ??= new RelayCommand(_ =>
            {
                NotifyPropertyChanged(nameof(PagesCount));
                if (CurrentPageNumber > PagesCount)
                {
                    CurrentPageNumber = PagesCount;
                }
                RefreshDetailCollection();
            });

        public CameraPageViewModel()
        {
            RefreshDetailCollection();
            //CameraWidgetVMs = Application.Current.Properties["CameraWidgetVMs"] as Dictionary<int, CameraWidgetViewModel>;
        }

        private void RefreshDetailCollection()
        {
            VisibleCameraWidgets.Clear();
            VisibleCameraWidgets = new ObservableCollection<CameraWidgetViewModel>(CameraWidgetVMs
                    .Skip(_camerasPerPage * (CurrentPageNumber - 1))
                    .Take(_camerasPerPage)
                    .Select(kvp => kvp.Value));

            NotifyPropertyChanged(nameof(VisibleCameraWidgets));

            UpdateNavigationButtonsEnabledState();
        }

        private void UpdateNavigationButtonsEnabledState()
        {
            IsNextEnabled = _currentPageNumber < PagesCount;
            IsPreviousEnabled = _currentPageNumber > 1;
        }
    }
}
