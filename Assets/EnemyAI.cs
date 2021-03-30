using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public int damage;
    public int maxHealth;
    public Slider healthSlider;
    public float sliderOffset;
    public int currentHealth;
    public int expYield = 1;
    public AudioClip die;
    public AudioClip attack;

    private Animator anim;
    private NavMeshAgent agent;
    private Collider col;
    private AudioSource src;
    
    [SerializeField]
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            float distTarget = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
            if (distTarget > agent.stoppingDistance) {
                agent.SetDestination(target.transform.position);
                anim.SetInteger("Move", Mathf.RoundToInt(Vector3.Magnitude(agent.velocity)));
            } else {
                anim.SetInteger("Move", 0);
                anim.SetBool("Attacking", true);
            }
            TargetDeathCheck();
        }
        Vector3 sliderPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        sliderPos.y += sliderOffset;
        healthSlider.transform.position = sliderPos;
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bug")) {
            target = other.gameObject;
            col.enabled = !col.enabled;
        }
    }
    void TargetDeathCheck() {
        if(target.GetComponent<BugAnimations>().currentHealth <= 0) {
            target = null;
            col.enabled = !col.enabled;
            anim.SetBool("Attacking", false);
        }
    }
    void DeathCheck() {
        if (currentHealth <= 0) {
            src.PlayOneShot(die);
            this.gameObject.SetActive(false);
        }
    }
    public void Damage(int damage) {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        DeathCheck();
    }
    public void HIT() {
        if (Vector3.Distance(this.transform.position, target.transform.position) < agent.stoppingDistance) {
            target.GetComponent<BugAnimations>().Damage(damage);
            src.PlayOneShot(attack);
        }
        anim.SetBool("Attacking", false);
    }
}
