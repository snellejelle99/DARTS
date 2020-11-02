using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DARTS_UnitTests
{
    public static class Mock_DatabaseTestData
    {
        public static void AddDatabaseTestData()
        {
            PlayerFactory playerFactory = new PlayerFactory();
            LegFactory legFactory = new LegFactory();
            TurnFactory turnFactory = new TurnFactory();
            MatchFactory matchFactory = new MatchFactory();
            SetFactory setFactory = new SetFactory();
            ThrowFactory throwFactory = new ThrowFactory();

            Random random = new Random();
            Array playerEnumsValues = Enum.GetValues(typeof(PlayerEnum));
            List<Player> dummyPlayers = new List<Player>();

            for (int i = 0; i < 6; i++)
            {
                Player player = (Player)playerFactory.Spawn();
                player.Name = "player" + Convert.ToString(i);
                player.Id = i;
                dummyPlayers.Add(player);
            }

            Set dummySet = (Set)setFactory.Spawn();
            dummySet.BeginningPlayer = PlayerEnum.Player1;
            dummySet.NumLegs = 5;
            dummySet.Player1LegsWon = 2;
            dummySet.Player2LegsWon = 3;
            dummySet.WinningPlayer = PlayerEnum.Player1;

            Leg dummyLeg = (Leg)legFactory.Spawn();
            dummyLeg.BeginningPlayer = PlayerEnum.Player1;
            dummyLeg.Player1LegScore = 501;
            dummyLeg.Player2LegScore = 501;
            dummyLeg.WinningPlayer = PlayerEnum.Player1;

            Turn dummyTurn = (Turn)turnFactory.Spawn();
            dummyTurn.PlayerTurn = PlayerEnum.Player1;
            dummyTurn.ThrownPoints = 20;

            List<DataObjectBase> dummyThrows = throwFactory.SpawnAmount(3);
            ((Throw)dummyThrows[0]).Score = 15;
            ((Throw)dummyThrows[0]).ScoreType = ScoreType.Double;
            ((Throw)dummyThrows[1]).Score = 15;
            ((Throw)dummyThrows[1]).ScoreType = ScoreType.Double;
            ((Throw)dummyThrows[2]).Score = 15;
            ((Throw)dummyThrows[2]).ScoreType = ScoreType.Double;

            dummyTurn.Throws = new BindingList<DataObjectBase>(dummyThrows);

            BindingList<DataObjectBase> dummyTurnList = new BindingList<DataObjectBase>();
            dummyTurnList.Add(dummyTurn);
            dummyLeg.Turns = dummyTurnList;

            BindingList<DataObjectBase> dummyLegList = new BindingList<DataObjectBase>();
            dummyLegList.Add(dummyLeg);
            dummySet.Legs = dummyLegList;

            List<Match> dummyMatches = new List<Match>();

            for (int i = 0; i < 3; i++)
            {
                Match match = (Match)matchFactory.Spawn();
                match.Player1 = dummyPlayers[i];
                match.Player2 = dummyPlayers[i + 3];
                match.NumSets = 3;
                match.NumLegs = 5;
                match.Player1SetsWon = 0;
                match.Player2SetsWon = 3;
                match.PointsPerLeg = 301;
                match.WinningPlayer = (PlayerEnum)playerEnumsValues.GetValue(random.Next(1, playerEnumsValues.Length));
                BindingList<DataObjectBase> dummySetList = new BindingList<DataObjectBase>();

                for (int j = 0; j < 3; j++)
                {
                    dummySetList.Add(dummySet);
                }
                match.Sets = dummySetList;
                dummyMatches.Add(match);
                match.Post();
            }
        }
    }
}
