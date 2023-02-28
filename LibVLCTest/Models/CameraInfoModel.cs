using OCode.MVVM.Base;

namespace LibVLCTest.Models
{
    public class CameraInfoModel : ViewModelBase
    {
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }
        private int _id;

        public string NewName
        {
            get => _newName;
            set => Set(ref _newName, value);
        }
        private string _newName;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        private string _name;

        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    Set(ref _address, value);
                }
            }
        }
        private string _address;

        public CameraInfoModel()
        {

        }

        public CameraInfoModel(CameraInfoModel other)
        {
            Id = other.Id;
            Name = other.Name;
            NewName = other.Name;
            Address = other.Address;
        }
    }
}
