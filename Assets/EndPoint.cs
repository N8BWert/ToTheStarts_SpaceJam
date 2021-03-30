using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public Text countdownText;
    public string nextLevel;
    public Image fadeImage;
    public int safeBugs = 0;
    public AudioClip blastOff;

    [SerializeField]
    private float countdownTime;
    [SerializeField]
    private bool countingDown = false;
    private float startCountdownTime;
    private AudioSource src;

    [SerializeField]
    private float endTime = 3f;

    void Start() {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countingDown) {
            countdownTime = (Time.time - startCountdownTime);
            countdownText.text = countdownTime.ToString();
            if (countdownTime > endTime) {
                src.PlayOneShot(blastOff);
                if (safeBugs > Score.highScore) {
                    Score.highScore = safeBugs;
                    Score.SaveScore();
                }
                SceneManager.LoadScene(nextLevel);
            }
            fadeImage.color = new Color(0, 0, 0, countdownTime / endTime);
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bug")) {
            if(!countingDown) {
                startCountdownTime = Time.time;
                countingDown = true;
                safeBugs++;
                other.gameObject.SetActive(false);
            } else {
                safeBugs++;
                other.gameObject.SetActive(false);
            }
        }
    }
}
