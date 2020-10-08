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
            Random r = new Random();
            Array playerEnumsValues = Enum.GetValues(typeof(PlayerEnum));
            List<Player> dummyPlayers = new List<Player>();

            for (int i = 0; i < 6; i++)
            {
                Player p = new Player();
                p.Name = "player" + Convert.ToString(i);
                p.ID = i;
                dummyPlayers.Add(p);
            }

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
            List<Match> dummyMatches = new List<Match>();

            for (int i = 0; i < 3; i++)
            {
                Match m = new Match();
                m.Player1 = dummyPlayers[i];
                m.Player2 = dummyPlayers[i + 3];
                m.NumSets = i;
                m.NumLegs = i;
                m.WinningPlayer = (PlayerEnum)playerEnumsValues.GetValue(r.Next(1, playerEnumsValues.Length));
                List<Set> dummySetList = new List<Set>();
                for (int j = 0; j < r.Next(1,5); j++)
                {
                    dummySetList.Add(dummySet);
                }
                m.Sets = dummySetList;
                dummyMatches.Add(m);
            }

            return dummyMatches;
        }
    }
}
