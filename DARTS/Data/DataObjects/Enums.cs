using System;
using System.Collections.Generic;
using System.Text;

namespace DARTS.Data.DataObjects
{
    public enum PlayState
    {
        NotStarted,
        InProgress,
        Finished
    }

    public enum PlayerEnum
    {
        None,
        Player1,
        Player2
    }

    public enum ScoreType
    {
        Miss,
        Single,
        Double,
        Triple,
        Bull,
        Bullseye
    }
}
