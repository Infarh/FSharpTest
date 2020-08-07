using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace WpfTest.Infrastructure.Behaviors
{
    internal class ScrollToAddedItem : Behavior<ListBox>
    {
        #region SelectNewItem : bool - Выбрать новое значение

        /// <summary>Выбрать новое значение</summary>
        public static readonly DependencyProperty SelectNewItemProperty =
            DependencyProperty.Register(
                nameof(SelectNewItem),
                typeof(bool),
                typeof(ScrollToAddedItem),
                new PropertyMetadata(default(bool)));

        /// <summary>Выбрать новое значение</summary>
        //[Category("")]
        [Description("Выбрать новое значение")]
        public bool SelectNewItem { get => (bool)GetValue(SelectNewItemProperty); set => SetValue(SelectNewItemProperty, value); }

        #endregion

        #region LeaveSelection : bool - Оставлять выделение

        /// <summary>Оставлять выделение</summary>
        public static readonly DependencyProperty LeaveSelectionProperty =
            DependencyProperty.Register(
                nameof(LeaveSelection),
                typeof(bool),
                typeof(ScrollToAddedItem),
                new PropertyMetadata(default(bool)));

        /// <summary>Оставлять выделение</summary>
        //[Category("")]
        [Description("Оставлять выделение")]
        public bool LeaveSelection { get => (bool)GetValue(LeaveSelectionProperty); set => SetValue(LeaveSelectionProperty, value); }

        #endregion

        #region Enable : bool - Показывать новый элемент

        /// <summary>Показывать новый элемент</summary>
        public static readonly DependencyProperty EnableProperty =
            DependencyProperty.Register(
                nameof(Enable),
                typeof(bool),
                typeof(ScrollToAddedItem),
                new PropertyMetadata(true, OnEnableChanged));

        private static void OnEnableChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
        {
            var selector = (ScrollToAddedItem)D;
            if ((bool)E.NewValue)
                ((INotifyCollectionChanged)selector.AssociatedObject.Items).CollectionChanged += selector.OnCollectionChanged;
            else
                ((INotifyCollectionChanged)selector.AssociatedObject.Items).CollectionChanged -= selector.OnCollectionChanged;
        }

        /// <summary>Показывать новый элемент</summary>
        //[Category("")]
        [Description("Показывать новый элемент")]
        public bool Enable { get => (bool)GetValue(EnableProperty); set => SetValue(EnableProperty, value); }

        #endregion

        protected override void OnAttached() => ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged += OnCollectionChanged;

        protected override void OnDetaching() => ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged -= OnCollectionChanged;

        private void OnCollectionChanged(object Sender, NotifyCollectionChangedEventArgs E)
        {
            if (E.Action != NotifyCollectionChangedAction.Add) return;
            var added_item = E.NewItems[0];
            if (added_item is null) return;
            AssociatedObject.ScrollIntoView(added_item);
            if (!SelectNewItem) return;
            if (LeaveSelection && AssociatedObject.SelectionMode == SelectionMode.Multiple)
                AssociatedObject.SelectedItems.Add(added_item);
            else
                AssociatedObject.SelectedItem = added_item;
        }
    }
}
