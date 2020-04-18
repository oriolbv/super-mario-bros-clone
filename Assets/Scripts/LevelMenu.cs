using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : ExtendedBehaviour
{

    public GameObject LivesText;


    // Start is called before the first frame update
    void Start()
    {
        LivesText.GetComponent<Text>().text = "x " + GameScore.Instance.Lives.ToString();
        Wait(2f, () => {
            SceneManager.LoadScene("Level11Scene");
        }); 
    }
}
