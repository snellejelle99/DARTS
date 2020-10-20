using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace DARTS.Data
{
    public class DummyData
    {
        public static List<Match> TempAddListItems()
        {
            Random random = new Random();
            Array playerEnumsValues = Enum.GetValues(typeof(PlayerEnum));
            List<Player> dummyPlayers = new List<Player>();

            for (int i = 0; i < 6; i++)
            {
                Player player = new Player();
                player.Name = "player" + Convert.ToString(i);
                player.ID = i;
                dummyPlayers.Add(player);
            }

            Set dummySet = new Set();
            dummySet.BeginningPlayer = PlayerEnum.Player1;
            dummySet.NumLegs = 5;
            dummySet.Player1LegsWon = 2;
            dummySet.Player2LegsWon = 3;
            dummySet.WinningPlayer = PlayerEnum.Player1;

            Leg dummyLeg = new Leg();
            dummyLeg.BeginningPlayer = PlayerEnum.Player1;
            dummyLeg.Player1LegScore = 501;
            dummyLeg.Player2LegScore = 501;
            dummyLeg.WinningPlayer = PlayerEnum.Player1;

            Turn dummyTurn = new Turn();
            dummyTurn.PlayerTurn = PlayerEnum.Player1;
            dummyTurn.ThrownPoints = 20;
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

            List<Match> dummyMatches = new List<Match>();

            for (int i = 0; i < 3; i++)
            {
                Match match = new Match();
                match.Player1 = dummyPlayers[i];
                match.Player2 = dummyPlayers[i + 3];
                match.NumSets = 3;
                match.NumLegs = 5;
                match.Player1SetsWon = 0;
                match.Player2SetsWon = 3;
                match.WinningPlayer = (PlayerEnum)playerEnumsValues.GetValue(random.Next(1, playerEnumsValues.Length));
               
                List<Set> dummySetList = new List<Set>();
                for (int j = 0; j < 3; j++)
                {
                    dummySetList.Add(dummySet);
                }
                match.Sets = dummySetList;
                dummyMatches.Add(match);
            }

            return dummyMatches;
        }
    }
}
