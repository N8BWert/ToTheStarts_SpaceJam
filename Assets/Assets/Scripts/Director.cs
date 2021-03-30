using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Director : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public float minionRadius = 5;
    public float attackDistance = 7f;
    public GameObject marker;
    public Canvas pauseScreen;

    private Vector3 newPosition;
    private Vector3 startPosition;
    private float startTime = 0;
    private float journeyLength = 0;
    Camera cam;

    [SerializeField]
    private List<GameObject> Minions = new List<GameObject>();
    [SerializeField]
    private GameObject target = null;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        newPosition = gameObject.transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            pauseScreen.gameObject.SetActive(true);
        }
        FindNewLocation();
        if (target != null) {
            TargetDeathCheck();
        }
        MoveToLocation();
    }
    void FindNewLocation() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.CompareTag("Enemy")) {
                    target = hit.collider.gameObject;
                    startPosition = gameObject.transform.position;
                    newPosition = target.transform.position;
                    Instantiate(marker, newPosition, new Quaternion(0, 0, 0, 0));
                    newPosition.y = gameObject.transform.position.y;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(gameObject.transform.position, newPosition);
                } else {
                    target = null;
                    startPosition = gameObject.transform.position;
                    newPosition = hit.point;
                    Instantiate(marker, newPosition, new Quaternion(0, 0, 0, 0));
                    newPosition.y = gameObject.transform.position.y;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(gameObject.transform.position, newPosition);
                }
            }
            DirectMinions();
        }
    }
    void MoveToLocation() {
        if (startPosition != newPosition && transform.position != newPosition) {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, newPosition, fractionJourney);
        }
    }
    void DirectMinions() {
        if (target == null) {
            for (int i = 0; i < Minions.Count; i++) {
                Vector3 Destination = new Vector3();
                if (i == 0) {
                    Destination = newPosition;
                } else if (i < 7) {
                    Destination = new Vector3(
                        newPosition.x + minionRadius * Mathf.Cos(Mathf.PI / 3 * (i - 1)), 
                        newPosition.y, 
                        newPosition.z + minionRadius * Mathf.Sin(Mathf.PI / 3 * (i - 1))
                    );
                } else if (i < 19) {
                    Destination = new Vector3(
                        newPosition.x + 2 * minionRadius * Mathf.Cos(Mathf.PI / 6 * (i - 1)),
                        newPosition.y,
                        newPosition.z + 2 * minionRadius * Mathf.Sin(Mathf.PI / 6 * (i - 1))
                    );
                } else if (i < 44) {
                    Destination = new Vector3(
                        newPosition.x + 3 * minionRadius * Mathf.Cos(Mathf.PI / 12 * (i - 1)),
                        newPosition.y,
                        newPosition.z + 3 * minionRadius * Mathf.Sin(Mathf.PI / 12 * (i - 1))
                    );
                }
                Minions[i].GetComponent<NavMeshAgent>().stoppingDistance = 0;
                Minions[i].GetComponent<NavMeshAgent>().SetDestination(Destination);
                Minions[i].GetComponent<BugAnimations>().target = null;
            }
        } else if (target != null) {
            for (int i = 0; i < Minions.Count; i++) {
                Minions[i].GetComponent<NavMeshAgent>().stoppingDistance = 7;
                Minions[i].GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                Minions[i].GetComponent<BugAnimations>().target = target;
            }
        }
    }
    void TargetDeathCheck() {
        if (target != null) {
            if(target.GetComponent<EnemyAI>().currentHealth <= 0) {
                target = null;
            }
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bug")) {
            Minions.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Bug")) {
            Minions.Remove(other.gameObject);
        }
    }
}
