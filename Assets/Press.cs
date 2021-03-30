using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Press : MonoBehaviour
{
    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextLevel() {
        SceneManager.LoadScene(nextLevel);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
