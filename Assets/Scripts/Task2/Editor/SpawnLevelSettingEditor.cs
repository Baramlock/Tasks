using System;
using System.Linq;
using Spawner;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnLevelSetting))]
public class SpawnLevelSettingEditor : Editor
{
    private SpawnLevelSetting _spawnLevelSetting;
    private UnityEngine.Object _source;

    private void OnEnable()
    {
        _spawnLevelSetting = (SpawnLevelSetting) target;

        if (_spawnLevelSetting.WaveByTime == null || _spawnLevelSetting.WaveByTime.Count == 0)
        {
            _spawnLevelSetting.InitWave();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(GUI.skin.window);
        var waveByTime = _spawnLevelSetting.WaveByTime.ToList();
        _spawnLevelSetting.Duration = EditorGUILayout.FloatField("Duration", _spawnLevelSetting.Duration);
        for (var i = 0; i < waveByTime.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label($"Spawn By Time {i * (_spawnLevelSetting.Duration / (waveByTime.Count - 1))}");
            DrawRemoveButton(waveByTime[i]);
            EditorGUILayout.EndHorizontal();
            DrawWave(waveByTime[i]);
            EditorGUILayout.EndVertical();
        }

        DrawAddButton();
        EditorGUILayout.EndVertical();
    }

    private void DrawAddButton()
    {
        if (GUILayout.Button("Add"))
        {
            Undo.RecordObject(_spawnLevelSetting, "Add Wave");
            var item = new Wave();
            if (_spawnLevelSetting.WaveByTime.Count >= 1)
            {
                var lastWave = _spawnLevelSetting.WaveByTime[^1];
                item.Type = lastWave.Type;
            }

            item.InitWave();
            _spawnLevelSetting.WaveByTime.Add(item);
        }
    }

    private void DrawRemoveButton(Wave wave)
    {
        if (GUILayout.Button("✖", GUILayout.Width(45)))
        {
            Undo.RecordObject(_spawnLevelSetting, "Remove waveByTime");
            _spawnLevelSetting.WaveByTime.Remove(wave);
        }
    }


    private void DrawWave(Wave enemyCount)
    {
        enemyCount.Type = (EnemyType) EditorGUILayout.EnumFlagsField(enemyCount.Type);
        var countType = Enum.GetNames(typeof(EnemyType)).Length;
        for (int i = 0; i < countType; i++)
        {
            var enemyType = (EnemyType) (1 << i);
            if (enemyCount.Type.HasFlag(enemyType))
                enemyCount.Date[i].Count = EditorGUILayout.IntField(enemyType + " Count", enemyCount.Date[i].Count);
        }
    }
}