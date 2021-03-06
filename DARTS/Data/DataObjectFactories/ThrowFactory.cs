﻿using DARTS.Data.DataFactory;
using DARTS.Data.DataObjectFactories.DataObjectFieldTypes;
using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjectFactories
{
    public class ThrowFactory : DataObjectFactoryBase
    {
        protected override void InitializeFields()
        {
            _fieldCollection.Add(ThrowFieldNames.Id, new CodeField(ThrowFieldNames.Id, true));
            _fieldCollection.Add(ThrowFieldNames.TurnId, new CodeField(ThrowFieldNames.TurnId));
            _fieldCollection.Add(ThrowFieldNames.Score, new DataField(ThrowFieldNames.Score, SQLiteType.INTEGER));
            _fieldCollection.Add(ThrowFieldNames.ScoreType, new DataField(ThrowFieldNames.ScoreType, SQLiteType.INTEGER));
        }

        protected override void SetNameAndTarget()
        {
            TableName = "THROW";
            TargetObject = typeof(Throw);
        }
    }

    public static class ThrowFieldNames
    {
        public const string Id = "Id";
        public const string TurnId = "TurnId";
        public const string Score = "Score";
        public const string ScoreType = "ScoreType";
    }
}
