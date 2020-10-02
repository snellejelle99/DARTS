using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data
{
    class DummyData
    {
        public static List<Match> TempAddListItems()
        {
            List<Match> dummyMatches = new List<Match>();
            Match dummyMatch = new Match();
            Player playerKoos = new Player();
            playerKoos.ID = 12;
            playerKoos.Name = "Koos";
            playerKoos.PlayerType = PlayerEnum.Player1;
            Player playerJan = new Player();
            playerJan.ID = 11;
            playerJan.Name = "Jan";
            playerJan.PlayerType = PlayerEnum.Player2;
            dummyMatch.Player1 = playerKoos;
            dummyMatch.Player2 = playerJan;
            dummyMatch.NumSets = 10;
            dummyMatch.NumLegs = 5;
            dummyMatch.WinningPlayer = PlayerEnum.Player1;
            Set dummySet = new Set();
            dummySet.BeginningPlayer = PlayerEnum.Player1;
            dummySet.NumLegs = 66;
            dummySet.Player1LegsWon = 66;
            dummySet.Player2LegsWon = 666;
            dummySet.WinningPlayer = PlayerEnum.Player1;
            Leg dummyLeg = new Leg();
            dummyLeg.BeginningPlayer = PlayerEnum.Player1;
            dummyLeg.Player1LegScore = 66;
            dummyLeg.Player2LegScore = 66;
            dummyLeg.WinningPlayer = PlayerEnum.Player1;
            Turn dummyTurn = new Turn();
            dummyTurn.PlayerTurn = PlayerEnum.Player1;
            dummyTurn.ThrownPoints = 66;
            Tuple<int, ScoreType> dummyTuple1 = new Tuple<int, ScoreType>(15, ScoreType.Double);
            Tuple<int, ScoreType> dummyTuple2 = new Tuple<int, ScoreType>(15, ScoreType.Double);
            Tuple<int, ScoreType> dummyTuple3 = new Tuple<int, ScoreType>(15, ScoreType.Double);
            List<Tuple<int, ScoreType>> dummyTuples = new List<Tuple<int, ScoreType>>();
            dummyTuples.Add(dummyTuple1);
            dummyTuples.Add(dummyTuple2);
            dummyTuples.Add(dummyTuple3);
            dummyTurn.Throws = dummyTuples;
            List<Turn> dummyTurnList = new List<Turn>();
            dummyTurnList.Add(dummyTurn);
            dummyLeg.Turns = dummyTurnList;
            List<Leg> dummyLegList = new List<Leg>();
            dummyLegList.Add(dummyLeg);
            dummySet.Legs = dummyLegList;
            List<Set> dummySetList = new List<Set>();
            dummySetList.Add(dummySet);
            dummyMatch.Sets = dummySetList;

            for (int i = 0; i < 5; i++)
            {
                dummyMatches.Add(dummyMatch);
            }

            return dummyMatches;
        }
    }
}
