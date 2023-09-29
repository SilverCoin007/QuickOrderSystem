using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOrderSystemClassLibrary.Services.Api
{
    public class PopUpService
    {
        public event Action<RenderFragment> OnShow;
        public event Action OnClose;

        public void Show(RenderFragment content)
        {
            OnShow?.Invoke(content);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}
