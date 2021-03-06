﻿using DARTS.Data.DataObjectFactories;
using System;
using System.ComponentModel;

namespace DARTS.Data.DataObjects
{
    public class Set : DataObjectBase
    {
        public int PlayerPoints { get; set; }

        public long Id
        {
            get => (long)FieldCollection[SetFieldNames.Id].Value;
            set => FieldCollection[SetFieldNames.Id].Value = value;
        }

        public long MatchId
        {
            get => (long)FieldCollection[SetFieldNames.MatchId].Value;
            set => FieldCollection[SetFieldNames.MatchId].Value = value;
        }

        public BindingList<DataObjectBase> Legs
        {
            get => CollectionFieldCollection[SetFieldNames.Legs].Value;
            set => CollectionFieldCollection[SetFieldNames.Legs].Value = value;
        }

        public PlayerEnum WinningPlayer
        {
            get => (PlayerEnum)Convert.ToInt32(FieldCollection[SetFieldNames.WinningPlayer].Value);
            set => FieldCollection[SetFieldNames.WinningPlayer].Value = (int)value;
        }

        public PlayerEnum BeginningPlayer
        {
            get => (PlayerEnum)Convert.ToInt32(FieldCollection[SetFieldNames.BeginningPlayer].Value);
            set => FieldCollection[SetFieldNames.BeginningPlayer].Value = (int)value;
        }

        public PlayState SetState
        {
            get => (PlayState)Convert.ToInt32(FieldCollection[SetFieldNames.SetState].Value);
            set => FieldCollection[SetFieldNames.SetState].Value = (int)value;
        }

        public int NumLegs
        {
            get => Convert.ToInt32(FieldCollection[MatchFieldNames.NumLegs].Value);
            set
            {
                if (value % 2 == 0)
                {
                    throw new ArgumentOutOfRangeException("Number of sets cannot be an even number.");
                }
                else FieldCollection[MatchFieldNames.NumLegs].Value = value;
            }
        }

        public int Player1LegsWon
        {
            get => Convert.ToInt32(FieldCollection[SetFieldNames.Player1LegsWon].Value);
            set
            {
                if (value > NumLegs)
                {
                    throw new ArgumentOutOfRangeException("Legs won by a player can not be bigger than the number of legs in the set.");
                }
                else FieldCollection[SetFieldNames.Player1LegsWon].Value = value;
            }
        }

        public int Player2LegsWon
        {
            get => Convert.ToInt32(FieldCollection[SetFieldNames.Player2LegsWon].Value);
            set
            {
                if (value > NumLegs)
                {
                    throw new ArgumentOutOfRangeException("Legs won by a player can not be bigger than the number of legs in the set.");
                }
                else FieldCollection[SetFieldNames.Player2LegsWon].Value = value;
            }
        }

        public string SetDetails
        {
            get
            {
                int totalThrown = 0, amountOfThrows = 0;
                for (int i = 0; i < Legs.Count; i++)
                {
                    for (int j = 0; j < ((Leg)Legs[i]).Turns.Count; j++)
                    {
                        totalThrown += ((Turn)((Leg)Legs[i]).Turns[j]).ThrownPoints;
                        amountOfThrows += 1;
                    }
                }
                return string.Format("Set: {0}-{1}: avg. thrown {2}", Player1LegsWon, Player2LegsWon, totalThrown / amountOfThrows);
            }
        }

        private LegFactory legFactory = new LegFactory();

        public Leg CreateNewLeg()
        {
            Leg newLeg = (Leg)legFactory.Spawn();
            newLeg.Player1LegScore = (uint)PlayerPoints;
            newLeg.Player2LegScore = (uint)PlayerPoints;
            newLeg.LegState = PlayState.InProgress;

            return newLeg;
        }

        public void Start()
        {
            Leg firstLeg = CreateNewLeg();
            firstLeg.BeginningPlayer = BeginningPlayer;
            Legs.Add(firstLeg);
            firstLeg.Start();       
        }

        public PlayerEnum CheckWin()
        {
            PlayerEnum winner = ((Leg)Legs[Legs.Count - 1]).CheckWin();

            if (winner != PlayerEnum.None)
            {
                if (winner == PlayerEnum.Player1)
                    Player1LegsWon++;
                else if (winner == PlayerEnum.Player2)
                    Player2LegsWon++;


                if (Player1LegsWon > (NumLegs / 2))
                {
                    WinningPlayer = PlayerEnum.Player1;
                    SetState = PlayState.Finished;
                }
                else if (Player2LegsWon > (NumLegs / 2))
                {
                    WinningPlayer = PlayerEnum.Player2;
                    SetState = PlayState.Finished;
                }
                else WinningPlayer = PlayerEnum.None;
            }
            else WinningPlayer = PlayerEnum.None;

            return WinningPlayer;
        }

        public void ChangeTurn()
        {
            //If nobody has won the leg yet change the turn.
            if (GetCurrentLeg().LegState == PlayState.InProgress)
            {
                GetCurrentLeg().ChangeTurn();
            }

            //If someone has won it start a new Leg and let the other player begin. Then start the leg.
            else
            {
                // TODO: impement factory pattern.
                Leg nextLeg = CreateNewLeg();
                nextLeg.BeginningPlayer = GetCurrentLeg().BeginningPlayer == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;
                Legs.Add(nextLeg);
                nextLeg.Start();
            }
        }

        public Leg GetCurrentLeg()
        {
            return (Leg)Legs[Legs.Count - 1];
        }

        private Set() : base()
        {

        }
    }
}
