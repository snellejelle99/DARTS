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
        public static List<Match> TempAddListItems()
        {
            Random random = new Random();
            Array playerEnumsValues = Enum.GetValues(typeof(PlayerEnum));
            List<Player> dummyPlayers = new List<Player>();

            for (int i = 0; i < 6; i++)
            {
                Player player = new Player();
                player.Name = "player" + Convert.ToString(i);
                player.Id = i;
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

            BindingList<DataObjectBase> dummyTurnList = new BindingList<DataObjectBase>();
            dummyTurnList.Add(dummyTurn);
            dummyLeg.Turns = dummyTurnList;

            BindingList<DataObjectBase> dummyLegList = new BindingList<DataObjectBase>();
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
                match.WinningPlayer = (PlayerEnum)playerEnumsValues.GetValue(random.Next(1, playerEnumsValues.Length));
                BindingList<DataObjectBase> dummySetList = new BindingList<DataObjectBase>();
                for (int j = 0; j < 3; j++)
                {
                    dummySetList.Add(dummySet);
                }
                match.Sets = dummySetList;
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
            SetFactory setFactory = new SetFactory(
                );
            Player player1 = (Player)playerFactory.Spawn();
            player1.Name = "Klaas";

            Player player2 = (Player)playerFactory.Spawn();
            player2.Name = "Pieter";

            int numSets = 11;
            int numLegs = 3;

            PlayerEnum beginningPlayer = PlayerEnum.Player1;

            BindingList<DataObjectBase> turns = new BindingList<DataObjectBase>();
            Turn turn = (Turn)turnFactory.Spawn();
            turn.PlayerTurn = PlayerEnum.Player2;
            turns.Add(turn);

            BindingList<DataObjectBase> legs = new BindingList<DataObjectBase>();
            Leg leg = (Leg)legFactory.Spawn();
            leg.Player1LegScore = 441;
            leg.Player2LegScore = 501;
            leg.BeginningPlayer = beginningPlayer;
            leg.LegState = PlayState.InProgress;
            leg.Turns = turns;
            legs.Add(leg);

            BindingList<DataObjectBase> sets = new BindingList<DataObjectBase>();
            Set set = (Set)setFactory.Spawn();
            set.NumLegs = numLegs;
            set.Player1LegsWon = 1;
            set.Player2LegsWon = 2;            
            set.Legs = legs;
            sets.Add(set);

            Match match = (Match)matchFactory.Spawn();
            match.Player1 = player1;
            match.Player2 = player2;
            match.NumSets = numSets;
            match.NumLegs = numLegs;
            match.Player1SetsWon = 5;
            match.Player2SetsWon = 4;
            match.Sets = sets;
            match.BeginningPlayer = beginningPlayer;
            match.MatchState = PlayState.InProgress;

            return match;
        }
    }
}
