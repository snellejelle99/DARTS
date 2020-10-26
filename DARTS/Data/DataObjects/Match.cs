using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Media;

namespace DARTS.Data.DataObjects
{
    public class Match : DataObjectBase
    {
        #region Properties
        public long Id
        {
            get => (long)FieldCollection[MatchFieldNames.Id].Value;
            set => FieldCollection[MatchFieldNames.Id].Value = value;
        }

        public long Player1Id
        {
            get => (long)FieldCollection[MatchFieldNames.Player1Id].Value;
            set => FieldCollection[MatchFieldNames.Player1Id].Value = value;
        }
        public long Player2Id
        {
            get => (long)FieldCollection[MatchFieldNames.Player2Id].Value;
            set => FieldCollection[MatchFieldNames.Player2Id].Value = value;
        }
        public int pointsPerLeg
        {
            get => (int)FieldCollection[MatchFieldNames.PointsPerLeg].Value;
            set => FieldCollection[MatchFieldNames.PointsPerLeg].Value = value;
        }
        public DataObjectBase Player1
        {
            get => (DataObjectBase)ObjectFieldCollection[MatchFieldNames.Player1].Value;
            set => ObjectFieldCollection[MatchFieldNames.Player1].Value = value;
        }

        public DataObjectBase Player2
        {
            get => (DataObjectBase)ObjectFieldCollection[MatchFieldNames.Player2].Value;
            set => ObjectFieldCollection[MatchFieldNames.Player2].Value = value;
        }

        public PlayerEnum WinningPlayer
        {
            get => (PlayerEnum)Convert.ToInt32(FieldCollection[MatchFieldNames.WinningPlayer].Value);
            set => FieldCollection[MatchFieldNames.WinningPlayer].Value = (int)value;
        }

        public PlayerEnum BeginningPlayer
        {
            get => (PlayerEnum)((int)FieldCollection[MatchFieldNames.BeginningPlayer].Value);
            set => FieldCollection[MatchFieldNames.BeginningPlayer].Value = (int)value;
        }

        public PlayState MatchState
        {
            get => (PlayState)((int)FieldCollection[MatchFieldNames.MatchState].Value);
            set => FieldCollection[MatchFieldNames.MatchState].Value = (int)value;
        }

        public BindingList<DataObjectBase> Sets
        {
            get => CollectionFieldCollection[MatchFieldNames.Sets].Value;
            set => CollectionFieldCollection[MatchFieldNames.Sets].Value = value;
        }

        public int NumSets
        {
            get => (int)FieldCollection[MatchFieldNames.NumSets].Value;
            set
            {
                if (value % 2 == 0)
                {
                    throw new ArgumentOutOfRangeException("Number of sets cannot be an even number.");
                }
                else FieldCollection[MatchFieldNames.NumSets].Value = value;
            }
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

        public int Player1SetsWon
        {
            get => Convert.ToInt32(FieldCollection[MatchFieldNames.Player1SetsWon].Value);
            set
            {
                if (value > NumSets)
                {
                    throw new ArgumentOutOfRangeException("Sets won by a player can not be bigger than the number of sets in the match.");
                }
                else FieldCollection[MatchFieldNames.Player1SetsWon].Value = value;
            }
        }

        public int Player2SetsWon
        {
            get => Convert.ToInt32(FieldCollection[MatchFieldNames.Player2SetsWon].Value);
            set
            {
                if (value > NumSets)
                {
                    throw new ArgumentOutOfRangeException("Sets won by a player can not be bigger than the number of sets in the match.");
                }
                else FieldCollection[MatchFieldNames.Player2SetsWon].Value = value;
            }
        }

        private SetFactory SetFactory
        {
            get;
            set;
        }

        public Set CreateNewSet()
        {
            Set newSet = (Set)SetFactory.Spawn();
            newSet.NumLegs = NumLegs;
            newSet.SetState = PlayState.InProgress;
            newSet.Player1LegsWon = 0;
            newSet.Player2LegsWon = 0;
            newSet.PlayerPoints = pointsPerLeg;

            return newSet;
        }

        public void Start()
        {
            Player1SetsWon = 0;
            Player2SetsWon = 0;
            Sets = new BindingList<DataObjectBase>();
            SetFactory = new SetFactory();
            if (BeginningPlayer.Equals(PlayerEnum.None))
            {
                BeginningPlayer = ChooseRandomPlayer();
            }

            // TODO: impement factory pattern.
            Set firstSet = CreateNewSet();
            firstSet.BeginningPlayer = BeginningPlayer;
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
            PlayerEnum winner = GetCurrentSet().CheckWin();

            if (winner != PlayerEnum.None)
            {
                if (winner == PlayerEnum.Player1)
                    Player1SetsWon++;

                else if (winner == PlayerEnum.Player2)
                    Player2SetsWon++;

                if (Player1SetsWon > (NumSets / 2))
                {
                    WinningPlayer = PlayerEnum.Player1;
                    MatchState = PlayState.Finished;
                }

                else if (Player2SetsWon > (NumSets / 2))
                {
                    WinningPlayer = PlayerEnum.Player2;
                    MatchState = PlayState.Finished;
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
                if (GetCurrentSet().SetState == PlayState.InProgress) //If newest set still in progress, change the turn.
                {
                    GetCurrentSet().ChangeTurn();
                }

                else //If newest set already finished, start another one 
                {
                    Set nextSet = CreateNewSet();
                    nextSet.BeginningPlayer = GetCurrentSet().BeginningPlayer == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;

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

        public DataObjectBase WinningPlayerObject
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

        #region Helper functions
        public Set GetCurrentSet()
        {
            return (Set)Sets[Sets.Count - 1];
        }

        public Leg GetCurrentLeg()
        {
            return (Leg)GetCurrentSet().Legs[GetCurrentSet().Legs.Count - 1];
        }

        public Turn GetCurrentTurn()
        {
            return (Turn)GetCurrentLeg().Turns[GetCurrentLeg().Turns.Count - 1];
        }
        #endregion

        private Match() : base()
        {
        }
    }
}
