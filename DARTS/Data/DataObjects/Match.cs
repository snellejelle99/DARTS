using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Media;

namespace DARTS.Data.DataObjects
{
    public class Match
    {
        #region BackingStores
        private PlayerEnum _winnningPlayer, _beginningPlayer;

        private ObjectState _matchState = ObjectState.NotStarted;

        private Player _player1;

        private Player _player2;

        private List<Set> _sets;

        private int _numSets, _numLegs;

        private int _player1SetsWon, _player2SetsWon;
        #endregion


        #region Properties
        public Player Player1
        {
            get => _player1;
            set => _player1 = value;
        }

        public Player Player2
        {
            get => _player2;
            set => _player2 = value;
        }

        public PlayerEnum WinningPlayer
        {
            get => _winnningPlayer;
            set => _winnningPlayer = value;
        }

        public PlayerEnum BeginningPlayer
        {
            get => _beginningPlayer;
            set => _beginningPlayer = value;
        }

        public ObjectState MatchState
        {
            get => _matchState;
            set => _matchState = value;
        }

        public List<Set> Sets
        {
            get => _sets;
            set => _sets = value;
        }

        public int NumSets
        {
            get => _numSets;
            set
            {
                if (value % 2 == 0)
                {
                    throw new ArgumentOutOfRangeException("Number of sets cannot be an even number.");
                }
                else _numSets = value;
            }
        }

        public int NumLegs
        {
            get => _numLegs;
            set
            {
                if (value % 2 == 0)
                {
                    throw new ArgumentOutOfRangeException("Number of legs cannot be an even number.");
                }
                else _numLegs = value;
            }
        }

        public int Player1SetsWon
        {
            get => _player1SetsWon;
            set
            {
                if (value > NumSets)
                {
                    throw new ArgumentOutOfRangeException("Sets won by a player can not be bigger than the number of sets in the match.");
                }
                else _player1SetsWon = value;
            }
        }

        public int Player2SetsWon
        {
            get => _player2SetsWon;
            set
            {
                if (value > NumSets)
                {
                    throw new ArgumentOutOfRangeException("Sets won by a player can not be bigger than the number of sets in the match.");
                }
                else _player2SetsWon = value;
            }
        }

        public void Start()
        {
            Sets = new List<Set>();

            if (BeginningPlayer.Equals(PlayerEnum.None))
            {
                BeginningPlayer = ChooseRandomPlayer();
            }

            // TODO: impement factory pattern.
            Set firstSet = new Set();
            firstSet.BeginningPlayer = BeginningPlayer;
            firstSet.NumLegs = NumLegs;
            firstSet.SetState = ObjectState.InProgress;

            Sets.Add(firstSet);
            firstSet.Start();
        }
        /// <summary>
        /// Called in ChangeTurn()
        /// Checks if the math has been won.
        /// Calls Set and Leg CheckWin() method to check that the last turn won the Leg/Set/Match.
        /// If needed also sets the winner of that set/leg.
        /// </summary>
        /// <returns>PlayerEnum.Player1 or Player2 if someone won, else returns PlayerEnum.None</returns>
        public PlayerEnum CheckWin()
        {
            PlayerEnum winner = Sets[Sets.Count - 1].CheckWin();

            if (winner != PlayerEnum.None)
            {
                if (winner == PlayerEnum.Player1)
                    Player1SetsWon++;

                else if (winner == PlayerEnum.Player2)
                    Player2SetsWon++;

                if (Player1SetsWon > (NumSets / 2))
                {
                    WinningPlayer = PlayerEnum.Player1;
                    MatchState = ObjectState.Finished;
                }

                else if (Player2SetsWon > (NumSets / 2))
                {
                    WinningPlayer = PlayerEnum.Player2;
                    MatchState = ObjectState.Finished;
                }

                else WinningPlayer = PlayerEnum.None;
            }

            else WinningPlayer = PlayerEnum.None;

            return WinningPlayer;
        }
        /// <summary>
        /// Starts the next turn and assigns the right player..
        /// Uses CheckWin methods to see if a new Leg or Set must be created and creates them if needed.
        /// </summary>
        public void ChangeTurn()
        {
            if (CheckWin() == PlayerEnum.None)
            {
                if (Sets[Sets.Count - 1].SetState == ObjectState.InProgress) //If newest set still in progress, change the turn.
                {
                    Sets[Sets.Count - 1].ChangeTurn();
                }

                else //If newest set already finished, start another one 
                {
                    Set nextSet = new Set();
                    nextSet.BeginningPlayer = Sets[Sets.Count - 1].BeginningPlayer == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;
                    nextSet.NumLegs = NumLegs;
                    nextSet.SetState = ObjectState.InProgress;

                    Sets.Add(nextSet);
                    nextSet.Start();
                }
            }
        }

        private PlayerEnum ChooseRandomPlayer()
        {
            // Choose randomly between PlayerEnum.Player1 and PlayerEnum.Player2.
            Random random = new Random();
            return (PlayerEnum)random.Next((int)PlayerEnum.Player1, (int)PlayerEnum.Player2 + 1);
        }

        public Player WinningPlayerObject
        {
            get
            {
                switch (WinningPlayer)
                {
                    case PlayerEnum.Player1:
                        return Player1;
                    case PlayerEnum.Player2:
                        return Player2;
                    default:
                        return null;
                }
            }
        }
        #endregion

        public Match()
        {  
        }
    }
}
