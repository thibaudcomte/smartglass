using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SmartGlass.Core.UI.Regions
{
    internal class Region
    {
        public ERegionLocation RegionLocation { get; }

        public ContentControl ContentControl { get; }

        public IDictionary<string, FrameworkElement> RegisteredViews { get; }

        public Region(ERegionLocation location, ContentControl control)
        {
            RegionLocation = location;
            ContentControl = control;
            RegisteredViews = new Dictionary<string, FrameworkElement>();
        }
    }

    public class RegionManager : IRegionManager
    {
        private IList<Region> _Regions = new List<Region>();
        private Panel _LayoutRoot;

        public void CreateRegionLocation(ERegionLocation regionLocation, ContentControl contentControl)
        {
            if (_Regions.FirstOrDefault(r => r.RegionLocation == regionLocation) != null)
                throw new Exception($"Region location {regionLocation} already exists.");

            _Regions.Add(new Region(regionLocation, contentControl));
        }

        public string RegisterRegionView(ERegionLocation regionLocation, FrameworkElement view)
        {
            var region = _Regions.FirstOrDefault(r => r.RegionLocation == regionLocation);

            if (region == null)
                throw new Exception($"Region location {regionLocation} doesn't exist.");

            string viewKey = $"{regionLocation}@{view.GetType().FullName}";

            if (region.RegisteredViews.ContainsKey(viewKey))
                throw new Exception($"View with key {viewKey} is already registered with region location {regionLocation}.");

            region.RegisteredViews.Add(viewKey, view);

            return viewKey;
        }

        public async Task ActivateRegionViewAsync(ERegionLocation regionLocation, string viewKey)
        {
            var region = _Regions.FirstOrDefault(r => r.RegionLocation == regionLocation);

            if (region == null)
                throw new Exception($"Region location {regionLocation} doesn't exist.");

            if (!region.RegisteredViews.ContainsKey(viewKey))
                throw new Exception($"View with key {viewKey} doesn't exist in region location {regionLocation}.");

            await region.ContentControl.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    var view = region.RegisteredViews[viewKey];

                    if (Equals(region.ContentControl.Content, view))
                        return;

                    await DeactivateRegionAsync(regionLocation);

                    var activationAwareView = view as IRegionActivationAware;
                    var activationAwareViewModel = view.DataContext as IRegionActivationAware;

                    if (activationAwareView != null)
                        await activationAwareView.OnBeforeActivatedAsync();

                    if (activationAwareViewModel != null)
                        await activationAwareViewModel?.OnBeforeActivatedAsync();

                    region.ContentControl.Content = view;

                    if (activationAwareView != null)
                        await activationAwareView.OnAfterActivatedAsync();

                    if (activationAwareViewModel != null)
                        await activationAwareViewModel.OnAfterActivatedAsync();
                });
        }

        public async Task DeactivateRegionAsync(ERegionLocation regionLocation)
        {
            var region = _Regions.FirstOrDefault(r => r.RegionLocation == regionLocation);

            if (region == null)
                throw new Exception($"Region location {regionLocation} doesn't exist.");

            await region.ContentControl.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    var view = region.ContentControl.Content as FrameworkElement;
                    if (view == null)
                        return;

                    var activationAwareView = view as IRegionActivationAware;
                    var activationAwareViewModel = view.DataContext as IRegionActivationAware;

                    if (activationAwareView != null)
                        await activationAwareView?.OnBeforeDeactivatedAsync();

                    if (activationAwareViewModel != null)
                        await activationAwareViewModel?.OnBeforeDeactivatedAsync();

                    region.ContentControl.Content = null;

                    if (activationAwareView != null)
                        await activationAwareView?.OnAfterDeactivatedAsync();

                    if (activationAwareViewModel != null)
                        await activationAwareViewModel?.OnAfterDeactivatedAsync();
                });
        }

        public void SetLayoutRoot(Panel layoutRoot)
        {
            _LayoutRoot = layoutRoot;
        }

        public async void SetLayoutRootVisibility(bool visible)
        {
            await _LayoutRoot.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    _LayoutRoot.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
                });
        }
    }
}
