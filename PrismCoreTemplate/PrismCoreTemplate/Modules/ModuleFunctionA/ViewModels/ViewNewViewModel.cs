using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace ModuleFunctionA.ViewModels
{
    public class ViewNewViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewNewViewModel()
        {
            Message = "View New from your Prism Module";
        }
    }
}
