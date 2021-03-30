using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BugAnimations : MonoBehaviour
{
    public int velocity;
    public int maxHealth = 3;
    public bool isInteracting;
    public Slider healthSlider;
    public int currentHealth;
    public GameObject target = null;
    public int damage;
    public int attackDistance;
    public int expEvolve = 5;
    public GameObject nextEvolution;
    public AudioClip attack;
    public AudioClip die;
    public AudioClip evolve;
    
    private AudioSource src;
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField]
    private int currentExp = 0;

    [SerializeField]
    private float sliderOffset = -1f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Mathf.RoundToInt(Vector3.Magnitude(agent.velocity));
        anim.SetInteger("Move", velocity);
        Vector3 sliderPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        sliderPos.y += sliderOffset;
        healthSlider.transform.position = sliderPos;
        if (target != null) {
            if (Vector3.Distance(this.transform.position, target.transform.position) < attackDistance) {
                anim.SetBool("Interacting", true);
            }
            TargetDeathCheck();
        }
        EvolveCheck();
    }
    void TargetDeathCheck() {
        if (target != null) {
            if(target.GetComponent<EnemyAI>().currentHealth <= 0) {
            currentExp += target.GetComponent<EnemyAI>().expYield;
            target = null;
            anim.SetBool("Interacting", false);
            }
        }
    }
    void DeathCheck() {
        if (currentHealth <= 0) {
            src.PlayOneShot(die);
            this.gameObject.SetActive(false);
        }
    }
    void EvolveCheck() {
        if (currentExp >= expEvolve) {
            src.PlayOneShot(evolve);
            Instantiate(nextEvolution, this.gameObject.transform.position, this.transform.rotation);
            currentHealth = 0;
            DeathCheck();
        }
    }
    public void UnInteract() {
        isInteracting = false;
        if (target != null) {
            if (Vector3.Distance(this.transform.position, target.transform.position) < attackDistance) {
                target.GetComponent<EnemyAI>().Damage(damage);
                src.PlayOneShot(attack);
            }
        }
    }
    public void Damage(int damage) {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        DeathCheck();
    }
}
