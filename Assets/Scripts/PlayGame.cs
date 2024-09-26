using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayGame : MonoBehaviour
{
    public void PlayGame_()
    {
        SceneManager.LoadScene("Scenes/Level1");
    }
}
