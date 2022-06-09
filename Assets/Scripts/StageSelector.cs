using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    public void SelectLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    
    void LoadLevel(int sceneIndex)
    {
        
    }
  
}
