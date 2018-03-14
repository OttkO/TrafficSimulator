using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace TrafficSimulatorWpf
{
    public class MainWindowViewModel : BindableBase
    {
        private DelegateCommand _playCommand;
        public ICommand PlayCommand => _playCommand ?? (_playCommand = new DelegateCommand(Play));

        private DelegateCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete));

        /// <summary>
        /// Starts the simulation
        /// </summary>
        private void Play()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the selected Road
        /// </summary>
        private void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
