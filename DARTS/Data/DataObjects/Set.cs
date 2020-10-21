using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Set : DataObjectBase
    {
        private List<Leg> _legs;

        private PlayerEnum _winnningPlayer, _beginnningPlayer;

        private PlayState _setState = PlayState.NotStarted;

        private int _player1LegsWon, _player2LegsWon, _numLegs;

        private const int PlayerPoints = 501;

        public long Id
        {
            get => (int)FieldCollection[SetFieldNames.Id].Value;
            set => FieldCollection[SetFieldNames.Id].Value = value;
        }
        public long MatchId
        {
            get => (int)FieldCollection[SetFieldNames.MatchId].Value;
            set => FieldCollection[SetFieldNames.MatchId].Value = value;
        }
        public BindingList<DataObjectBase> Legs
        {
            get => CollectionFieldCollection[SetFieldNames.Legs].Value;
            set => CollectionFieldCollection[SetFieldNames.Legs].Value = value;
        }

        public PlayerEnum WinningPlayer
        {
            get
            {
                int enumVal = (int)FieldCollection[SetFieldNames.WinningPlayer].Value;
                return (PlayerEnum)enumVal;
            }
            set => FieldCollection[SetFieldNames.WinningPlayer].Value = (int)value;
        }

        public PlayerEnum BeginningPlayer
        {
            get
            {
                int enumVal = (int)FieldCollection[SetFieldNames.BeginningPlayer].Value;
                return (PlayerEnum)enumVal;
            }
            set => FieldCollection[SetFieldNames.BeginningPlayer].Value = (int)value;
        }

        public PlayState SetState
        {
            get
            {
                int enumVal = (int)FieldCollection[SetFieldNames.SetState].Value;
                return (PlayState)enumVal;
            }
            set => FieldCollection[SetFieldNames.SetState].Value = (int)value;
        }

        public int NumLegs
        {
            get => (int)FieldCollection[MatchFieldNames.NumLegs].Value;
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
            get => (int)FieldCollection[SetFieldNames.Player1LegsWon].Value;
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
            get => (int)FieldCollection[SetFieldNames.Player2LegsWon].Value;
            set
            {
                if (value > NumLegs)
                {
                    throw new ArgumentOutOfRangeException("Legs won by a player can not be bigger than the number of legs in the set.");
                }
                else FieldCollection[SetFieldNames.Player2LegsWon].Value = value;
            }
        }

        private LegFactory LegFactory
        {
            get;
            set;
        }

        public Leg CreateNewLeg()
        {
            Leg newLeg = (Leg)LegFactory.Spawn();
            newLeg.Player1LegScore = PlayerPoints;
            newLeg.Player2LegScore = PlayerPoints;
            newLeg.LegState = PlayState.InProgress;

            return newLeg;
        }

        public void Start()
        {
            Legs = new BindingList<DataObjectBase>();
            LegFactory = new LegFactory();
            // TODO: impement factory pattern.
            Leg firstLeg = CreateNewLeg();
            firstLeg.BeginningPlayer = BeginningPlayer;
            Legs.Add(firstLeg);
            Post();
            firstLeg.Start();       
        }

        public PlayerEnum CheckWin()
        {
            PlayerEnum winner = (Legs[Legs.Count - 1] as Leg).CheckWin();

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
                Post();
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
