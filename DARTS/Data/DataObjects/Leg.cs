using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Leg : DataObjectBase
    {
        #region BackingStores

        private PlayerEnum _winnningPlayer, _beginningPlayer;

        private PlayState _legState = PlayState.NotStarted;

        private uint _player1LegScore, _player2LegScore;
        #endregion

        #region Properties
        public long Id
        {
            get => (int)FieldCollection[LegFieldNames.Id].Value;
            set => FieldCollection[LegFieldNames.Id].Value = value;
        }

        public BindingList<DataObjectBase> Turns
        {
            get => CollectionFieldCollection[LegFieldNames.Turns].Value;
            set => CollectionFieldCollection[LegFieldNames.Turns].Value = value;
        }

        public PlayerEnum WinningPlayer
        {
            get
            {
                int enumVal = (int)FieldCollection[LegFieldNames.WinningPlayer].Value;
                return (PlayerEnum)enumVal;
            }
            set => FieldCollection[LegFieldNames.WinningPlayer].Value = (int)value;
        }

        public PlayerEnum BeginningPlayer
        {
            get
            {
                int enumVal = (int)FieldCollection[LegFieldNames.BeginningPlayer].Value;
                return (PlayerEnum)enumVal;
            }
            set => FieldCollection[LegFieldNames.BeginningPlayer].Value = (int)value;
        }

        public PlayState LegState
        {
            get
            {
                int enumVal = (int)FieldCollection[LegFieldNames.LegState].Value;
                return (PlayState)enumVal;
            }
            set => FieldCollection[LegFieldNames.LegState].Value = (int)value;
        }

        public uint Player1LegScore
        {
            get => (uint)FieldCollection[LegFieldNames.Player1LegScore].Value;
            set => FieldCollection[LegFieldNames.Player1LegScore].Value = value;
        }

        public uint Player2LegScore
        {
            get => (uint)FieldCollection[LegFieldNames.Player1LegScore].Value;
            set => FieldCollection[LegFieldNames.Player1LegScore].Value = value;
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
        private TurnFactory TurnFactory
        {
            get;
            set;
        }
        public void Start()
        {
            Turns = new BindingList<DataObjectBase>();
            TurnFactory = new TurnFactory();
            // TODO: impement factory pattern.
            Turn firstTurn = (Turn)TurnFactory.Spawn();
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
            Turn nextTurn = (Turn)TurnFactory.Spawn();
            Turn currentTurn = Turns[Turns.Count - 1] as Turn;
            nextTurn.PlayerTurn = currentTurn.PlayerTurn == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;

            

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
