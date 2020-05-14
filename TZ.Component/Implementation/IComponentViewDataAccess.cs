using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.CompExtention
{
    public interface IComponentViewDataAccess
    {
        string Connection { get; }
        bool SaveView(IComponentView view);
        bool UpdateView(IComponentView view);
        bool RemoveView(string viewID);
        List<ComponentView> GetViews();
        ComponentView GetView(string viewid);
        void LoadViewComponents(IComponentView view);

    }
}
