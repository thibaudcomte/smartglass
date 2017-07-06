using System;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SmartGlass.Core.UI.Converters
{
    public class ItemToItemIndexInCollectionConverter : DependencyObject, IValueConverter
    {
        public IList List
        {
            get { return (IList)GetValue(ListProperty); }
            set { SetValue(ListProperty, value); }
        }

        public static readonly DependencyProperty ListProperty =
            DependencyProperty.Register("List", typeof(IList), 
                typeof(ItemToItemIndexInCollectionConverter), new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return List.IndexOf(value) + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
