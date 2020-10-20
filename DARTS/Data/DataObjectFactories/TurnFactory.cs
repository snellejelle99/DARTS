using DARTS.Data.DataFactory;
using DARTS.Data.DataObjects;

namespace DARTS.Data.DataObjectFactories
{
    public class TurnFactory : DataObjectFactoryBase
    {
        protected override void InitializeFields()
        {
         
        }

        protected override void SetNameAndTarget()
        {
            TableName = "TURN";
            TargetObject = typeof(Turn);
        }
    }
}