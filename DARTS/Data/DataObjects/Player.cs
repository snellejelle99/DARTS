using DARTS.Data.DataObjectFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public class Player : DataObjectBase
    {
        #region Properties
        public string Name
        {
            get => (string)FieldCollection["Name"].Value;
            set => FieldCollection["Name"].Value = value;
        }

        public long Id
        {
            get => (long)FieldCollection["Id"].Value;
            set => FieldCollection["Id"].Value = value;
        }

        #endregion

        private Player() : base()
        {

        }

        public List<DataObjectBase> GetMatches()
        {
            MatchFactory matchFactory = new MatchFactory();
            List<DataObjectBase> result = matchFactory.Get(MatchFieldNames.Player1Id, this.Id);
            result = result.Concat(matchFactory.Get(MatchFieldNames.Player2Id, this.Id)).ToList();
            return result;
        }
    }
}
