using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRandomAppear : MonoBehaviour {

    public Transform player;
    public GameObject agent;
    public NotesCollectedScriptableObject nts_obj;
    public float min_spawn_dist = 5f;
    public float max_spawn_dist = 10f;
    
    [SerializeField]
    uint note_count;
    [SerializeField]
    private bool wait;

    void Start() {

        agent.gameObject.SetActive(false);
        wait = false;
        nts_obj.note_event.AddListener(new_note);
    }

    void Update() {

        if (!wait) {
            
            if (Random.Range(1, 100000) * .00001 >= .99 - (float)note_count *.1) {
                Debug.Log("Puk Spawned");
                StartCoroutine(spawn_puk());
            }

        }
    }

    void new_note(uint x) {
        note_count = x;
    }

    IEnumerator spawn_puk() {
        
        transform.position = player.position;
        Debug.Log("HMM");

        float random_dir = Random.Range(0, 360f);
        Vector3 dir = Quaternion.AngleAxis(random_dir, Vector3.up) * player.forward;
        RaycastHit hit;

        Vector3 spawn_pos;

        if (Physics.Raycast(player.position, dir, out hit, max_spawn_dist)) {

            if (hit.distance <= min_spawn_dist) {
                Debug.Log($"New Iter! {hit.transform.position}");
                yield return spawn_puk();
                Debug.Log("End of iter!");
                goto end;
            }

            spawn_pos = player.position + (dir * (max_spawn_dist - Random.Range(min_spawn_dist, hit.distance)));
        }

        else {
            spawn_pos = player.position + (dir * (max_spawn_dist - Random.Range(min_spawn_dist, max_spawn_dist)));
        }

        agent.gameObject.transform.position = spawn_pos;
        agent.gameObject.SetActive(true);
        wait = true;

        Debug.Log("Waiting");
        yield return new WaitForSeconds(5);
        Debug.Log("Not anymore");

        wait = false;
        agent.gameObject.SetActive(false);

        end: Debug.Log("REACHED END!!!!");
    }
}
