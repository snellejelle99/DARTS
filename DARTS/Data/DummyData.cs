using DARTS.Data.DataObjectFactories;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media;

namespace DARTS.Data
{
    public class DummyData
    {
        public static BindingList<DataObjectBase> TempAddListItems()
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
            dummySet.SetState = PlayState.NotStarted;

            Leg dummyLeg = (Leg)legFactory.Spawn();
            dummyLeg.BeginningPlayer = PlayerEnum.Player1;
            dummyLeg.Player1LegScore = 501;
            dummyLeg.Player2LegScore = 501;
            dummyLeg.WinningPlayer = PlayerEnum.Player1;
            dummyLeg.LegState = PlayState.NotStarted;

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

            dummyTurn.Throws.Add(dummyThrows[0]);
            dummyTurn.Throws.Add(dummyThrows[1]);
            dummyTurn.Throws.Add(dummyThrows[2]);

            dummyLeg.Turns.Add(dummyTurn);
            
            dummySet.Legs.Add(dummyLeg);

            BindingList<DataObjectBase> dummyMatches = new BindingList<DataObjectBase>();

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
                match.MatchState = PlayState.Finished;
                match.BeginningPlayer = (PlayerEnum)playerEnumsValues.GetValue(random.Next(1, playerEnumsValues.Length));
                match.WinningPlayer = (PlayerEnum)playerEnumsValues.GetValue(random.Next(1, playerEnumsValues.Length));
                
                for (int j = 0; j < 3; j++)
                {
                    match.Sets.Add(dummySet);
                }
                match.Post();
                dummyMatches.Add(match);
            }

            return dummyMatches;
        }

        public static Match GetDummyMatch()
        {

            PlayerFactory playerFactory = new PlayerFactory();
            LegFactory legFactory = new LegFactory();
            TurnFactory turnFactory = new TurnFactory();
            MatchFactory matchFactory = new MatchFactory();
            SetFactory setFactory = new SetFactory();

            Player player1 = (Player)playerFactory.Spawn();
            player1.Name = "Klaas";

            Player player2 = (Player)playerFactory.Spawn();
            player2.Name = "Pieter";

            int numSets = 3;
            int numLegs = 3;

            PlayerEnum beginningPlayer = PlayerEnum.Player1;

            Match match = (Match)matchFactory.Spawn();
            match.Player1 = player1;
            match.Player2 = player2;
            match.NumSets = numSets;
            match.NumLegs = numLegs;
            match.Player1SetsWon = 0;
            match.Player2SetsWon = 0;
            match.WinningPlayer = PlayerEnum.None;
            match.BeginningPlayer = beginningPlayer;
            match.MatchState = PlayState.InProgress;
            match.Post();
            match.Start();

            return match;
        }
    }
}
