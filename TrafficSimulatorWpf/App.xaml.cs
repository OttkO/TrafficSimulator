using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TrafficSimulatorWpf
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var mainWindow = new Tracy();
      var mainWindowViewModel = new TracyViewModel();

      mainWindow.ViewModel = mainWindowViewModel;
      mainWindow.Show();
    }
  }
}
