using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DARTS
{
    class StartscreenValidation : ObservableObject
    {
        private string _player1;
        private string _player2;
        public string Player1
        {
            get { return _player1; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");

                OnPropertyChanged(ref _player1, value);
            }
        }

        public string Player2
        {
            get { return _player2; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");

                OnPropertyChanged(ref _player2, value);
            }
        }
    }
}
