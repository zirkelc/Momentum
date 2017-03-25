//using Android.Content;
//using Android.Views;
//using Android.Widget;
//using BottomBar.Droid.Helpers;
//using BottomNavigationBar;
//using BottomNavigationBar.Listeners;
//using Momentum.Controls;
//using Momentum.Droid.Renderers;
//using Momentum.Extensions;
//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
//using Xamarin.Forms.Platform.Android.AppCompat;

////[assembly: ExportRenderer(typeof(BottomBarPage), typeof(BottomBarPageRenderer))]
////namespace Momentum.Droid.Renderers
////{
////    public class BottomBarPageRenderer : VisualElementRenderer<BottomBarPage>, IOnTabClickListener
////    {
////        bool _disposed;
////        BottomNavigationBar.BottomBar _bottomBar;
////        FrameLayout _frameLayout;
////        BottomBar.Droid.Helpers.IPageController _pageController;

////        public BottomBarPageRenderer()
////        {
////            AutoPackage = false;
////        }

////        #region IOnTabClickListener
////        public void OnTabSelected(int position)
////        {
////            SwitchContent(Element.Children[position]);
////        }

////        public void OnTabReSelected(int position)
////        {
////        }
////        #endregion

////        protected override void Dispose(bool disposing)
////        {
////            if (disposing && !_disposed)
////            {
////                _disposed = true;

////                RemoveAllViews();

////                foreach (Page pageToRemove in Element.Children)
////                {
////                    IVisualElementRenderer pageRenderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer(pageToRemove);

////                    if (pageRenderer != null)
////                    {
////                        pageRenderer.ViewGroup.RemoveFromParent();
////                        pageRenderer.Dispose();
////                    }

////                    // pageToRemove.ClearValue (Platform.RendererProperty);
////                }

////                if (_bottomBar != null)
////                {
////                    _bottomBar.SetOnTabClickListener(null);
////                    _bottomBar.Dispose();
////                    _bottomBar = null;
////                }

////                if (_frameLayout != null)
////                {
////                    _frameLayout.Dispose();
////                    _frameLayout = null;
////                }

////                /*if (Element != null) {
////					PageController.InternalChildren.CollectionChanged -= OnChildrenCollectionChanged;
////				}*/
////            }

////            base.Dispose(disposing);
////        }

////        protected override void OnAttachedToWindow()
////        {
////            base.OnAttachedToWindow();
////            _pageController.SendAppearing();
////        }

////        protected override void OnDetachedFromWindow()
////        {
////            base.OnDetachedFromWindow();
////            _pageController.SendDisappearing();
////        }


////        protected override void OnElementChanged(ElementChangedEventArgs<BottomBarPage> e)
////        {
////            base.OnElementChanged(e);

////            if (e.NewElement != null)
////            {

////                BottomBarPage bottomBarPage = e.NewElement;

////                if (_bottomBar == null)
////                {
////                    _pageController = PageController.Create(bottomBarPage);

////                    // create a view which will act as container for Page's
////                    _frameLayout = new FrameLayout(Forms.Context);
////                    _frameLayout.LayoutParameters = new FrameLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent, GravityFlags.Fill);
////                    AddView(_frameLayout, 0);

////                    // create bottomBar control
////                    _bottomBar = BottomNavigationBar.BottomBar.Attach(_frameLayout, null);
////                    _bottomBar.NoTabletGoodness();
////                    _bottomBar.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
////                    _bottomBar.SetOnTabClickListener(this);

////                    UpdateTabs();
////                    UpdateBarBackgroundColor();
////                    UpdateBarTextColor();
////                }

////                if (bottomBarPage.CurrentPage != null)
////                {
////                    SwitchContent(bottomBarPage.CurrentPage);
////                }
////            }
////        }

////        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
////        {
////            base.OnElementPropertyChanged(sender, e);

////            if (e.PropertyName == nameof(TabbedPage.CurrentPage))
////            {
////                SwitchContent(Element.CurrentPage);
////            }
////            else if (e.PropertyName == NavigationPage.BarBackgroundColorProperty.PropertyName)
////            {
////                UpdateBarBackgroundColor();
////            }
////            else if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName)
////            {
////                UpdateBarTextColor();
////            }
////        }

////        protected virtual void SwitchContent(Page view)
////        {
////            Context.HideKeyboard(this);

////            _frameLayout.RemoveAllViews();

////            if (view == null)
////            {
////                return;
////            }

////            if (Xamarin.Forms.Platform.Android.Platform.GetRenderer(view) == null)
////            {
////                Xamarin.Forms.Platform.Android.Platform.SetRenderer(view, Xamarin.Forms.Platform.Android.Platform.CreateRenderer(view));
////            }

////            _frameLayout.AddView(Xamarin.Forms.Platform.Android.Platform.GetRenderer(view).ViewGroup);
////        }

////        protected override void OnLayout(bool changed, int l, int t, int r, int b)
////        {
////            int width = r - l;
////            int height = b - t;

////            var context = Context;

////            _bottomBar.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(height, MeasureSpecMode.AtMost));
////            int tabsHeight = Math.Min(height, Math.Max(_bottomBar.MeasuredHeight, _bottomBar.MinimumHeight));

////            if (width > 0 && height > 0)
////            {
////                _pageController.ContainerArea = new Rectangle(0, 0, context.FromPixels(width), context.FromPixels(height - 168));
////                //_pageController.ContainerArea = new Rectangle(0, context.FromPixels (tabsHeight), context.FromPixels (width), context.FromPixels (height - tabsHeight)));

////                ObservableCollection<Element> internalChildren = _pageController.InternalChildren;

////                for (var i = 0; i < internalChildren.Count; i++)
////                {
////                    var child = internalChildren[i] as VisualElement;

////                    if (child == null)
////                    {
////                        continue;
////                    }

////                    IVisualElementRenderer renderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer(child);
////                    var navigationRenderer = renderer as NavigationPageRenderer;
////                    if (navigationRenderer != null)
////                    {
////                        // navigationRenderer.ContainerPadding = tabsHeight;
////                    }
////                }

////                _bottomBar.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(tabsHeight, MeasureSpecMode.Exactly));
////                _bottomBar.Layout(0, 0, width, tabsHeight);
////            }

////            base.OnLayout(changed, l, t, r, b);
////        }

////        void UpdateBarBackgroundColor()
////        {
////            if (_disposed || _bottomBar == null)
////            {
////                return;
////            }

////            _bottomBar.SetBackgroundColor(Element.BarBackgroundColor.ToAndroid());
////        }

////        void UpdateBarTextColor()
////        {
////            if (_disposed || _bottomBar == null)
////            {
////                return;
////            }

////            // haven't found yet how to set text color for tab items on_bottomBar, doesn't seem to have a direct way
////        }

////        void UpdateTabs()
////        {
////            // create tab items
////            SetTabItems();

////            // set tab colors
////            SetTabColors();
////        }

////        void SetTabItems()
////        {
////            BottomBarTab[] tabs = Element.Children.Select(page => {
////                var tabIconId = ResourceManagerEx.IdFromTitle(page.Icon, ResourceManager.DrawableClass);
////                return new BottomBarTab(tabIconId, page.Title);
////            }).ToArray();

////            _bottomBar.SetItems(tabs);
////        }

////        void SetTabColors()
////        {
////            for (int i = 0; i < Element.Children.Count; ++i)
////            {
////                Page page = Element.Children[i];

////                Color? tabColor = page.GetTabColor();

////                if (tabColor != null)
////                {
////                    _bottomBar.MapColorForTab(i, tabColor.Value.ToAndroid());
////                }
////            }
////        }
////    }
//}