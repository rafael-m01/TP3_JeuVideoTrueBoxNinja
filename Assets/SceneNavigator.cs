using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Une classe utilitaire statique simple pour gérer toutes les transitions de scène
/// et la fermeture de l'application. Cela centralise la gestion des scènes.
/// </summary>
public class SceneNavigator : MonoBehaviour
{
    // L'utilisation de constantes pour les noms de scènes évite les fautes de frappe ("magic strings")
    private const string MENU_SCENE_NAME = "Menu";
    private const string GAME_SCENE_NAME = "Game";

    /// <summary>
    /// Charge la scène du menu principal.
    /// </summary>
    public static void GoToMenu()
    {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }


    /// <summary>
    /// Charge la scène principale du jeu.
    /// </summary>
    public static void StartGame()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }


    /// <summary>
    /// Quitte l'application.
    /// (Note : Cela ne fonctionne que dans un jeu "buildé", pas dans l'éditeur Unity.)
    /// </summary>
    public static void ExitApp()
    {
#if UNITY_EDITOR
        // Si dans l'éditeur, arrêter le mode de jeu
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si dans un jeu "buildé", quitter
        Application.Quit();
#endif
    }
}