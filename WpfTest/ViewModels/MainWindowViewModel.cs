using System;
using System.Windows.Documents;
using System.Windows.Input;
using WpfTest.ViewModels.Base;

namespace WpfTest.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Тестовое окно";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов";

        /// <summary>Статус</summary>
        public string Status { get => _Status; private set => Set(ref _Status, value); }

        #endregion

        #region Document : FlowDocument - Документ

        /// <summary>Документ</summary>
        private FlowDocument _Document;

        /// <summary>Документ</summary>
        public FlowDocument Document
        {
            get => _Document;
            set
            {
                if (!Set(ref _Document, value, out var old)) return;
                value.IfNotNull(v => v.KeyUp += OnDocumentKeyPressed);
                old.IfNotNull(v => v.KeyUp -= OnDocumentKeyPressed);
            }
        }


        #endregion

        public MainWindowViewModel()
        {
            Document = new FlowDocument
            {
                Blocks =
                {
                    new Paragraph
                    {
                        Inlines =
                        {
                            "123"
                        }
                    }
                }
            };
        }

        private void OnDocumentKeyPressed(object Sender, KeyEventArgs E)
        {
            if(!(Sender is FlowDocument document)) return;
        }
    }
}
