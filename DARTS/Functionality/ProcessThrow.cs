using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Navigation;

namespace DARTS.Functionality
{
    public class ProcessThrow
    {
     
        /// <summary>
        /// Calculates the Score from a single dart throw 
        /// </summary>
        /// <param name="ThrowScore">the regular score without multiplication</param>
        /// <param name="type">the Scoretype</param>
        /// <returns></returns>
        public static Tuple<int, ScoreType> CalculateThrowScore(int ThrowScore, ScoreType type)
        {
            if (ThrowScore >= 0 && (ThrowScore <= 20 || ThrowScore == 25 || ThrowScore == 50))
            {
                switch (type)
                {
                    case ScoreType.Double:
                        ThrowScore *= 2;
                        break;
                    case ScoreType.Triple:
                        ThrowScore *= 3;
                        break;
                    default:
                        break;
                };
            }
            return new Tuple<int, ScoreType>(ThrowScore, type);         
        }


    }
}
