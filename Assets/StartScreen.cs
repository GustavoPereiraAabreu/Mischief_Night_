using UnityEngine;
using UnityEngine.SceneManagement;  // Necessário para carregar cenas

public class StartScreen : MonoBehaviour
{
    // Função chamada quando o botão é clicado
    public void StartGame()
    {
        // Carrega a cena do jogo (certifique-se de que a cena "Game" está adicionada nas configurações do build)
        SceneManager.LoadScene("Game");
    }
}