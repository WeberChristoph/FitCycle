using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FitCycle.Behaviors
{
    public class ResizeListView : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView bindable)
        {
            bindable.BindingContextChanged += Bindable_Resize;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            bindable.BindingContextChanged -= Bindable_Resize;
        }

        void Bindable_Resize(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            if (!(sender is ListView listview))
                return;
            var elements= listview.ItemsSource;
            if (elements == null)
                return;
            var count = 0;
            foreach (var element in elements)
            {
                count++;
            }
            if (listview.RowHeight > 0)
                listview.HeightRequest = count * listview.RowHeight;
        }
    }
}
