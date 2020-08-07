﻿using System;
using System.Threading.Tasks;
using WpfTest.Infrastructure.Commands.Base;

namespace WpfTest.Infrastructure.Commands
{
    internal class LambdaCommandAsync : CommandAsync
    {
        private readonly ActionAsync<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public LambdaCommandAsync(ActionAsync Execute, Func<bool> CanExecute) : this(async p => await Execute(), CanExecute is null ? (Func<object, bool>)null : p => CanExecute()) { }

        public LambdaCommandAsync(ActionAsync<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        protected override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        protected override async Task ExecuteAsync(object parameter) => await _Execute(parameter);
    }
}