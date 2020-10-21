using DARTS.Data.DataFactory;
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
            CodeField idField = new CodeField(ThrowFieldNames.Id, true);
            _fieldCollection.Add(ThrowFieldNames.Id, idField);

            _fieldCollection.Add(ThrowFieldNames.TurnId, new CodeField(ThrowFieldNames.TurnId));
            _fieldCollection.Add(ThrowFieldNames.Score, new CodeField(ThrowFieldNames.Score));
            _fieldCollection.Add(ThrowFieldNames.ScoreType, new CodeField(ThrowFieldNames.ScoreType));


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
