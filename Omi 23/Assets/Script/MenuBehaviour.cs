using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

    public void triggerMenuBehavior(int i)
    {
        switch (i)
        {

            case (0):
                SceneManager.LoadScene("Level1");
                break;
            case (1):
                Application.Quit();
                break;
            default:
                break;
        }
    }


    


}
