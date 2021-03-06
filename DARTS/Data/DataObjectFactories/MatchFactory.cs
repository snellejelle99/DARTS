﻿using System;
using System.Collections.Generic;
using System.Text;
using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS.Data.DataObjects;

namespace DARTS.Data.DataObjectFactories
{
    public class MatchFactory : DataObjectFactoryBase
    {
        protected override void InitializeFields()
        {
            CodeField idField = new CodeField(MatchFieldNames.Id, true);
            CodeField player1IdField = new CodeField(MatchFieldNames.Player1Id);
            CodeField player2IdField = new CodeField(MatchFieldNames.Player2Id);
            _fieldCollection.Add(MatchFieldNames.Id, idField);
            _fieldCollection.Add(MatchFieldNames.Player1Id, player1IdField);
            _fieldCollection.Add(MatchFieldNames.Player2Id, player2IdField);

            _fieldCollection.Add(MatchFieldNames.WinningPlayer, new DataField(MatchFieldNames.WinningPlayer, SQLiteType.INTEGER));
            _fieldCollection.Add(MatchFieldNames.BeginningPlayer, new DataField(MatchFieldNames.BeginningPlayer, SQLiteType.INTEGER));
            _fieldCollection.Add(MatchFieldNames.MatchState, new DataField(MatchFieldNames.MatchState, SQLiteType.INTEGER));
            _fieldCollection.Add(MatchFieldNames.NumSets, new DataField(MatchFieldNames.NumSets, SQLiteType.INTEGER));
            _fieldCollection.Add(MatchFieldNames.NumLegs, new DataField(MatchFieldNames.NumLegs, SQLiteType.INTEGER));
            _fieldCollection.Add(MatchFieldNames.Player1SetsWon, new DataField(MatchFieldNames.Player1SetsWon, SQLiteType.INTEGER));
            _fieldCollection.Add(MatchFieldNames.Player2SetsWon, new DataField(MatchFieldNames.Player2SetsWon, SQLiteType.INTEGER));
            _fieldCollection.Add(MatchFieldNames.PointsPerLeg, new DataField(MatchFieldNames.PointsPerLeg, SQLiteType.INTEGER));

            _objectFieldCollection.Add(MatchFieldNames.Player1, new ObjectField(MatchFieldNames.Player1, typeof(PlayerFactory), player1IdField));
            _objectFieldCollection.Add(MatchFieldNames.Player2, new ObjectField(MatchFieldNames.Player2, typeof(PlayerFactory), player2IdField));

            _collectionFieldCollection.Add(MatchFieldNames.Sets, new CollectionField(MatchFieldNames.Sets, typeof(SetFactory), SetFieldNames.MatchId, idField));
        }

        protected override void SetNameAndTarget()
        {
            TableName = "MATCH";
            TargetObject = typeof(Match);
        }

        public override void Delete(DataObjectBase objectBase)
        {
            Match match = (Match)objectBase;
            match.CollectionFieldCollection[MatchFieldNames.Sets].DeleteValues();
            base.Delete(objectBase);
        }
    }

    public static class MatchFieldNames
    {
        public const string Id = "Id";
        public const string Player1Id = "Player1Id";
        public const string Player2Id = "Player2Id";
        public const string Player1 = "Player1";
        public const string Player2 = "Player2";
        public const string WinningPlayer = "WinningPlayer";
        public const string BeginningPlayer = "BeginningPlayer";
        public const string MatchState = "MatchState";
        public const string Sets = "Sets";
        public const string NumSets = "NumSets";
        public const string NumLegs = "NumLegs";
        public const string Player1SetsWon = "Player1SetsWon";
        public const string Player2SetsWon = "Player2SetsWon";
        public const string PointsPerLeg = "PointsPerLeg";
    }
}
