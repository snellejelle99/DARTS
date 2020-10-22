using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Leg
    {
        #region BackingStores
        private List<Turn> _turns;

        private PlayerEnum _winnningPlayer, _beginningPlayer;

        private PlayState _legState = PlayState.NotStarted;

        private uint _player1LegScore, _player2LegScore;
        #endregion

        #region Properties
        public List<Turn> Turns
        {
            get => _turns;
            set => _turns = value;
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

        public PlayState LegState
        {
            get => _legState;
            set => _legState = value;
        }

        public uint Player1LegScore
        {
            get => _player1LegScore;
            set => _player1LegScore = value;
        }

        public uint Player2LegScore
        {
            get => _player2LegScore;
            set => _player2LegScore = value;
        }

        public uint CurrentPlayerLegScore
        {
            get
            {
                switch(Turns.Last().PlayerTurn)
                {
                    case PlayerEnum.Player1:
                        return _player1LegScore;
                    default:
                        return _player2LegScore;
                }
            }
            set
            {
                switch (Turns.Last().PlayerTurn)
                {
                    case PlayerEnum.Player1:
                        _player1LegScore = value;
                        break;
                    default:
                        _player2LegScore = value;
                        break;
                }
            }            
        }
        #endregion

        public void Start()
        {
            Turns = new List<Turn>();

            // TODO: impement factory pattern.
            Turn firstTurn = new Turn();
            firstTurn.PlayerTurn = BeginningPlayer;
            firstTurn.Throws = new List<Tuple<int, ScoreType>>();

            Turns.Add(firstTurn);
        }

        public PlayerEnum CheckWin()
        {
            if (Player1LegScore == 0)
            {
                WinningPlayer = PlayerEnum.Player1;
                LegState = PlayState.Finished;
            }
            else if (Player2LegScore == 0)
            {
                WinningPlayer = PlayerEnum.Player2;
                LegState = PlayState.Finished;
            }
            else
            {
                WinningPlayer = PlayerEnum.None;
            }

            return WinningPlayer;
        }

        public void ChangeTurn()
        {
            // TODO: impement factory pattern.(?)
            // Create the next turn and assign the other player.
            Turn nextTurn = new Turn();
            nextTurn.PlayerTurn = Turns[Turns.Count - 1].PlayerTurn == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;
            nextTurn.Throws = new List<Tuple<int, ScoreType>>();
            Turns.Add(nextTurn);
        }

        public void SubtractScore()
        {
            foreach (Tuple<int, ScoreType> dart in Turns.Last().Throws)
            {
                if (CurrentPlayerLegScore > dart.Item1) CurrentPlayerLegScore -= (uint)dart.Item1;
                else if (CurrentPlayerLegScore == dart.Item1)
                {
                    if (dart.Item2 == ScoreType.Double || dart.Item2 == ScoreType.Bullseye) CurrentPlayerLegScore = 0;
                }
                else break;
            }
        }

        public Leg()
        {

        }
    }
}
