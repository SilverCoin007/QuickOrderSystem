using Microsoft.AspNetCore.Components;

namespace QuickOrderSystemAdminApp.Data
{
    public class PopUpService
    {
        public event Action<RenderFragment>? OnShow;
        public event Action? OnClose;

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
