using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            audioSource.Play();
            Invoke("loadScene", 2f);
            
        }
    }

    void loadScene(){
        SceneManager.LoadScene("Main");
    }
}
