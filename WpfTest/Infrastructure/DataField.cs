﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfTest.Infrastructure
{
    internal class DataField : Freezable
    {
        #region Source : object - Источник

        /// <summary>Источник</summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                nameof(Source),
                typeof(object),
                typeof(DataField),
                new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSourceChanged) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

        private static void OnSourceChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
        {

        }

        /// <summary>Источник</summary>
        //[Category("")]
        [Description("Источник")]
        public object Source { get => (object) GetValue(SourceProperty); set => SetValue(SourceProperty, value); }

        #endregion

        #region Destination : object - Приёмник

        /// <summary>Приёмник</summary>
        public static readonly DependencyProperty DestinationProperty =
            DependencyProperty.Register(
                nameof(Destination),
                typeof(object),
                typeof(DataField),
                new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDestinationChanged) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

        private static void OnDestinationChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
        {

        }

        /// <summary>Приёмник</summary>
        //[Category("")]
        [Description("Приёмник")]
        public object Destination { get => (object) GetValue(DestinationProperty); set => SetValue(DestinationProperty, value); }

        #endregion

        //public DataField() => BindingOperations.SetBinding(this, DestinationProperty, new Binding(nameof(Source)) { Source = this });

        protected override Freezable CreateInstanceCore() => new DataField();
    }
}
