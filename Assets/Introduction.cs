using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    public string bugText;
    public string evolvedBugText;
    public string enemyText;
    public string spaceshipText;

    public GameObject[] show = new GameObject[4];

    public Text displayText;

    public string nextLevel;

    private int part = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            part++;
        }
        if (part == 0) {
            displayText.text = bugText;
            show[0].SetActive(true);
        } else if (part == 1) {
            displayText.text = evolvedBugText;
            show[0].SetActive(false);
            show[1].SetActive(true);
        } else if (part == 2) {
            displayText.text = enemyText;
            show[1].SetActive(false);
            show[2].SetActive(true);
        } else if (part == 3) {
            displayText.text = spaceshipText;
            show[2].SetActive(false);
            show[3].SetActive(true);
        } else {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
