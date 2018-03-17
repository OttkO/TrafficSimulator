using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;
using TrafficSimulator;

namespace TrafficSimulatorWpf
{
    public class TracyViewModel : BindableBase
    {
        private readonly Simulator _simulator;

        public TracyViewModel()
        {
            _simulator = new Simulator(30);
        }

        private DelegateCommand _playCommand;
        public ICommand PlayCommand => _playCommand ?? (_playCommand = new DelegateCommand(Play));

        private DelegateCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete));

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        /// <summary>
        /// Starts the simulation
        /// </summary>
        private void Play()
        {
            //if (!IsRunning)
            //{
            //    _simulator.StartSim();
            //}
            //else
            //{
            //    _simulator.StopSim();
            //}

            IsRunning = !IsRunning;
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
