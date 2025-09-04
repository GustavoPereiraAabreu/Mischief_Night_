using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    
    public void QuitGame()
    {
        
        SceneManager.LoadScene("Menu");
    }

}

