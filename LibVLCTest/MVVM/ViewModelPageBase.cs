using LibVLCTest.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCode.MVVM.Base
{
    public class ViewModelPageBase : ViewModelBase
    {
        protected RelayCommand _onPageLoadedCommand;
        public virtual RelayCommand OnPageLoadedCommand =>
            _onPageLoadedCommand ??= new RelayCommand(async _ => await OnPageInit());

        protected async virtual Task OnPageInit()
        {
        }

        public async virtual Task OnPageLeaving()
        {
        }


        public ViewModelPageBase()
        {
        }
    }
}
