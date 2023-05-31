using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    public NotesCollectedScriptableObject nts_obj;
    [SerializeField]
    private uint note_count;

    void Start() {

        text.text = $"note count: {note_count}";
        nts_obj.note_event.AddListener(set_count);
    }

    // Update is called once per frame
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.E)) {
            nts_obj.add_note();
        }

    }

    void set_count(uint x) {
        this.note_count = x;
        text.text = $"note count: {note_count}";
    }
}
