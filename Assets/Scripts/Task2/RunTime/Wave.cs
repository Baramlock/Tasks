using System;
using System.Collections.Generic;

namespace Spawner
{
    [Serializable]
    public class Wave
    {
        public EnemyType Type;
        public List<EnemyCount> Date;
        
        public void InitWave()
        {
            Date = new List<EnemyCount>();
            var countType = Enum.GetNames(typeof(EnemyType)).Length;
            for (int i = 0; i < countType; i++)
                Date.Add(new EnemyCount((EnemyType) (1 << i)));
        }
    }
}