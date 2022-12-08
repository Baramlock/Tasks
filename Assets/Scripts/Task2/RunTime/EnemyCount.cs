using System;

namespace Spawner
{
    [Serializable]
    public class EnemyCount
    {
        public EnemyType EnemyType;
        public int Count;

        public EnemyCount(EnemyType enemyType, int count = 0)
        {
            EnemyType = enemyType;
            Count = count;
        }
    }
}