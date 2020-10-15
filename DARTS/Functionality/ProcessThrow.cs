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
        public ProcessThrow()
        { 
        
        }

        public int ValidateScore(int ThrowScore, ScoreType type, int LegScore)
        {
            if(ThrowScore >= 0 && (ThrowScore <= 20 || ThrowScore == 25 || ThrowScore== 50))
            {
                switch (type)
                {
                    case ScoreType.Bull:
                    case ScoreType.Bullseye:
                    case ScoreType.Single:
                        LegScore -= ThrowScore;
                        break;
                    case ScoreType.Double:
                        LegScore -= ThrowScore * 2;
                        break;
                    case ScoreType.Triple:
                        LegScore -= ThrowScore * 3;
                        break;
                };

                if (LegScore == 0)
                {
                    if (type == ScoreType.Double || type == ScoreType.Bullseye)
                    {
                        return LegScore;
                    }
                    else
                    {
                        if (type == ScoreType.Single || type == ScoreType.Bull)
                        {
                            LegScore += ThrowScore;
                        }
                        else if (type == ScoreType.Triple)
                        {
                            LegScore += ThrowScore * 3;
                        }    
                        

                    }
                }

                
            }
            return LegScore;
        }
        

    }
}
