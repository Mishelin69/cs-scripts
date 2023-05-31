using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NotesCollectedScriptableObject", menuName = "ScriptableObjects/NotesCollectedScriptableObject", order = 1)]
public class NotesCollectedScriptableObject : ScriptableObject {

    public UnityEvent<uint> note_event;
    private uint note_count = 0;

    void OnEnable() {

        Debug.Log("Initializing Event!");

        if (note_event == null)
            note_event = new UnityEvent<uint>();

        note_count = 0;
        note_event.Invoke(note_count);
    }

    public void add_note() {
        ++note_count;
        note_event.Invoke(note_count);
    }
}
