using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : ExtendedBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Wait(2f, () => {
            SceneManager.LoadScene("Level11Scene");
        }); 
    }
}
