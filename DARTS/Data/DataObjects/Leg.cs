﻿using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Leg : DataObjectBase
    {
        #region Properties
        private int PlayerPoints { get; set; }
        public long Id
        {
            get => (long)FieldCollection[LegFieldNames.Id].Value;
            set => FieldCollection[LegFieldNames.Id].Value = value;
        }

        public long SetId
        {
            get => (long)FieldCollection[LegFieldNames.SetId].Value;
            set => FieldCollection[LegFieldNames.SetId].Value = value;
        }
        public BindingList<DataObjectBase> Turns
        {
            get => CollectionFieldCollection[LegFieldNames.Turns].Value;
            set => CollectionFieldCollection[LegFieldNames.Turns].Value = value;
        }

        public PlayerEnum WinningPlayer
        {
            get => (PlayerEnum)FieldCollection[LegFieldNames.WinningPlayer].Value;
            set => FieldCollection[LegFieldNames.WinningPlayer].Value = (int)value;
        }

        public PlayerEnum BeginningPlayer
        {
            get => (PlayerEnum)(int)FieldCollection[LegFieldNames.BeginningPlayer].Value;
            set => FieldCollection[LegFieldNames.BeginningPlayer].Value = (int)value;
        }

        public PlayState LegState
        {
            get => (PlayState)(int)FieldCollection[LegFieldNames.LegState].Value;        
            set => FieldCollection[LegFieldNames.LegState].Value = (int)value;
        }

        public uint Player1LegScore
        {
            get => (uint)FieldCollection[LegFieldNames.Player1LegScore].Value;
            set => FieldCollection[LegFieldNames.Player1LegScore].Value = value;
        }

        public uint Player2LegScore
        {
            get => (uint)FieldCollection[LegFieldNames.Player2LegScore].Value;
            set => FieldCollection[LegFieldNames.Player2LegScore].Value = value;
        }

        public uint CurrentPlayerLegScore
        {
            get
            {
                switch(((Turn)Turns.Last()).PlayerTurn)
                {
                    case PlayerEnum.Player1:
                        return Player1LegScore;
                    default:
                        return Player2LegScore;
                }
            }
            set
            {
                switch (((Turn)Turns.Last()).PlayerTurn)
                {
                    case PlayerEnum.Player1:
                        Player1LegScore = value;
                        break;
                    default:
                        Player2LegScore = value;
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
            Turn firstTurn = (Turn)TurnFactory.Spawn();
            firstTurn.PlayerTurn = BeginningPlayer;
            firstTurn.ThrownPoints = 0;

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
            Turn nextTurn = (Turn)TurnFactory.Spawn();
            nextTurn.ThrownPoints = 0;
            if (Turns.Count > 0)
            {
                Turn currentTurn = (Turn)Turns.Last();
                nextTurn.PlayerTurn = currentTurn.PlayerTurn == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;
            }
            else nextTurn.PlayerTurn = BeginningPlayer;
 
            Turns.Add(nextTurn);
        }

        public void SubtractScore()
        {
            foreach (Throw dart in ((Turn)Turns.Last()).Throws)
            {
                if (CurrentPlayerLegScore > dart.Score) CurrentPlayerLegScore -= (uint)dart.Score;
                else if (CurrentPlayerLegScore == dart.Score)
                {
                    if (dart.ScoreType == ScoreType.Double || dart.ScoreType == ScoreType.Bullseye) CurrentPlayerLegScore = 0;
                }
                else break;
            }
        }

        private Leg() : base()
        {

        }
    }
}
