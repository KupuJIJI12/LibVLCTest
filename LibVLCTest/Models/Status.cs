using System.ComponentModel;

namespace LibVLCTest.Models
{
    public enum Status
    {
        [Description("Остановлен")]
        Stopped = 0,
        [Description("Подключение")]
        Connecting = 1,
        [Description("Получение видеопотока")]
        Receiving = 2,
        [Description("Ошибка авторизации")]
        BadCredintails = 3,
        [Description("Ошибка RTSP Потока")]
        RTSPError = 4,
    }
}
