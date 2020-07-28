using System.Windows;
using WpfTest.Infrastructure.Commands.Base;

namespace WpfTest.Infrastructure.Commands
{
    class ClosedWindow : Command
    {
        protected override void Execute(object parameter) => (parameter as Window ?? App.ActiveWindow ?? App.FocusedWindow)?.Close();
    }
}
