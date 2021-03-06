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
            get => (PlayerEnum)Convert.ToInt32(FieldCollection[LegFieldNames.WinningPlayer].Value);
            set => FieldCollection[LegFieldNames.WinningPlayer].Value = (int)value;
        }

        public PlayerEnum BeginningPlayer
        {
            get => (PlayerEnum)Convert.ToInt32(FieldCollection[LegFieldNames.BeginningPlayer].Value);
            set => FieldCollection[LegFieldNames.BeginningPlayer].Value = (int)value;
        }

        public PlayState LegState
        {
            get => (PlayState)Convert.ToInt32(FieldCollection[LegFieldNames.LegState].Value);        
            set => FieldCollection[LegFieldNames.LegState].Value = (int)value;
        }

        public uint Player1LegScore
        {
            get => Convert.ToUInt32(FieldCollection[LegFieldNames.Player1LegScore].Value);
            set => FieldCollection[LegFieldNames.Player1LegScore].Value = value;
        }

        public uint Player2LegScore
        {
            get => Convert.ToUInt32(FieldCollection[LegFieldNames.Player2LegScore].Value);
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
        
        public string LegDetails
        {
            get
            {
                int totalThrown = 0, amountOfThrows = 0;
                for (int i = 0; i < Turns.Count; i++)
                {
                    totalThrown += ((Turn)Turns[i]).ThrownPoints;
                    amountOfThrows += 1;
                }
                return string.Format("Leg: {0}-{1}: avg. thrown {2}", Player1LegScore, Player2LegScore, totalThrown / amountOfThrows);
            }
        }
        #endregion

        private TurnFactory turnFactory = new TurnFactory();

        public void Start()
        {
            Turn firstTurn = (Turn)turnFactory.Spawn();
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
            Turn nextTurn = (Turn)turnFactory.Spawn();
            Turn currentTurn = (Turn)Turns[Turns.Count - 1];
            nextTurn.PlayerTurn = currentTurn.PlayerTurn == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;
            nextTurn.ThrownPoints = 0;
            
            Turns.Add(nextTurn);
        }

        public void SubtractScore()
        {
            if (CurrentPlayerLegScore > ((Turn)Turns.Last()).ThrownPoints && CurrentPlayerLegScore - ((Turn)Turns.Last()).ThrownPoints != 1)
            {
                CurrentPlayerLegScore -= (uint)((Turn)Turns.Last()).ThrownPoints;
            }
            else if (CurrentPlayerLegScore - (uint)((Turn)Turns.Last()).ThrownPoints == 0)
            {
                foreach (Throw dart in ((Turn)Turns.Last()).Throws.Reverse())
                {
                    if (dart.ScoreType != ScoreType.Miss)
                    {
                        if (dart.ScoreType == ScoreType.Double || dart.ScoreType == ScoreType.Bullseye) CurrentPlayerLegScore = 0;
                    }
                }
            }
            else ((Turn)Turns.Last()).ThrownPoints = 0;
        }

        private Leg() : base()
        {

        }
    }
}
