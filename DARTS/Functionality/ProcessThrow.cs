using DARTS.Data.DataObjectFactories;
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
        public static Throw CalculateThrowScore(int ThrowScore, ScoreType type)
        {
            if ((ThrowScore >= 0 && ThrowScore <= 20) || ThrowScore == 25 || ThrowScore == 50)
            {
                switch (type)
                {
                    case ScoreType.Miss:
                        if (ThrowScore != 0) throw new ArgumentException("ScoreType Miss requires a ThrowScore of 0.");
                        break;
                    case ScoreType.Single:
                        if (!(ThrowScore >= 0 && ThrowScore <= 20)) throw new ArgumentException("ScoreType Single requires a ThrowScore between 0 and 20.");
                        break;
                    case ScoreType.Double:
                        if (!(ThrowScore >= 0 && ThrowScore <= 20)) throw new ArgumentException("ScoreType Single requires a ThrowScore between 0 and 20.");
                        ThrowScore *= 2;
                        break;
                    case ScoreType.Triple:
                        if (!(ThrowScore >= 0 && ThrowScore <= 20)) throw new ArgumentException("ScoreType Single requires a ThrowScore between 0 and 20.");
                        ThrowScore *= 3;
                        break;
                    case ScoreType.Bull:
                        if (ThrowScore != 25) throw new ArgumentException("ScoreType Bull requires a ThrowScore of 25.");
                        break;
                    case ScoreType.Bullseye:
                        if (ThrowScore != 50) throw new ArgumentException("ScoreType Bullseye requires a ThrowScore of 50.");
                        break;
                };
            }
            else throw new ArgumentOutOfRangeException("Given ThrowScore isn't between 0 and 20 or isn't 25 or 50.");

            ThrowFactory throwFactory = new ThrowFactory();
            Throw _throw = (Throw)throwFactory.Spawn();
            _throw.Score = ThrowScore;
            _throw.ScoreType = type;

            return _throw;
        }


    }
}
