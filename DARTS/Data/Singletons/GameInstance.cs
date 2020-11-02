using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.Singletons
{
    public sealed class GameInstance
    {
        private static GameInstance instance = null;
        private static readonly object padlock = new object();
        
        private MainWindow _mainWindow { get; }
        public MainWindow MainWindow
        {
            get => _mainWindow;
        }

        private GameInstance()
        {
            _mainWindow = new MainWindow()
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
        }

        public static GameInstance Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GameInstance();
                    }
                    return instance;
                }
            }
        }
    }
}
