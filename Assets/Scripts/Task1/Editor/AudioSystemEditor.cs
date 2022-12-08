using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(AudioSystem))]
public class AudioSystemEditor : Editor
{
    [SerializeField] private GUISkin _skin;
    private AudioSystem _audioSystem;
    private AudioSystemData _currentPlay = new();
    private bool _isOpen;

    private void OnEnable()
    {
        _audioSystem = (AudioSystem) target;

        if (ReferenceEquals(_audioSystem.AudioSource, null))
            _audioSystem.Init();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawAudioSystemDates();
        DrawAddNewClipButton();
        EditorGUILayout.EndVertical();

        if (GUI.changed)
        {
            UpgradeDate();
            EditorUtility.SetDirty(_audioSystem);
            EditorSceneManager.MarkSceneDirty(_audioSystem.gameObject.scene);
        }
    }

    private void UpgradeDate() => _audioSystem.UpgradeData(_currentPlay);

    private void DrawAudioSystemDates()
    {
        var dataList = _audioSystem.Date;
        for (var i = 0; i < dataList.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            var date = _audioSystem.Date[i];
            DrawAudioSystemDate(date, i);
            EditorGUILayout.EndVertical();
        }
    }

    private void DrawAudioSystemDate(AudioSystemData date, int index)
    {
        EditorGUILayout.BeginHorizontal();
        date.AudioClip =
            (AudioClip) EditorGUILayout.ObjectField($"Clip {index} ", date.AudioClip, typeof(AudioClip), true);
        DrawStopAllButton();
        DrawPlayButton(date);
        DrawRemoveButton(index);
        EditorGUILayout.EndHorizontal();
        date.Pitch = EditorGUILayout.Slider("Pitch", date.Pitch, 0, 1);
        date.Volume = EditorGUILayout.Slider("Volume", date.Volume, 0, 1);
    }

    private void DrawStopAllButton()
    {
        if (GUILayout.Button("❚❚", _skin.button, GUILayout.Width(25)))
            _audioSystem.Pause();
    }

    private void DrawPlayButton(AudioSystemData data)
    {
        if (GUILayout.Button("▶", _skin.button, GUILayout.Width(25)))
        {
            _audioSystem.Play(data);
            _currentPlay = data;
        }
    }

    private void DrawRemoveButton(int index)
    {
        if (GUILayout.Button("✖", _skin.button,GUILayout.Width(25)))
        {
            Undo.RecordObject(_audioSystem, "Remove Date");
            _audioSystem.Date.RemoveAt(index);
        }
    }

    private void DrawAddNewClipButton()
    {
        if (GUILayout.Button("Add"))
        {
            Undo.RecordObject(_audioSystem, "Add Date");
            _audioSystem.Date.Add(new AudioSystemData());
        }
    }
}