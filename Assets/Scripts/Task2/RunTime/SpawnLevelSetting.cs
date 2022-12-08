using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    [CreateAssetMenu(menuName = "Enemy/EnemySpawnSettings", order = 51, fileName = "EnemySpawnSettings")]
    public class SpawnLevelSetting : ScriptableObject
    {
        public float Duration;
        public List<Wave> WaveByTime;

        public void InitWave()
        {
            WaveByTime = new List<Wave>();
            foreach (var wave in WaveByTime)
                wave.InitWave();
        }
    }
}