using UnityEngine;

/// <summary>
/// Contrôle le panneau du menu pause dans la scène de jeu.
/// Gère la pause, la reprise, la sauvegarde et le retour au menu.
/// </summary>
public class PausePanel : MonoBehaviour
{
    // Référence au contrôleur principal du jeu
    private GameManager gameManager;

    /// <summary>
    /// Logique d'initialisation.
    /// </summary>
    void Start()
    {
        // Trouver le GameManager actif
        gameManager = FindAnyObjectByType<GameManager>();

        // Le panneau de pause doit toujours commencer masqué
        gameObject.SetActive(false);
    }


    // --- Contrôle de l'état du panneau ---

    /// <summary>
    /// Affiche le panneau de pause et gèle le temps de jeu.
    /// </summary>
    public void OpenPanel()
    {
        gameObject.SetActive(true);
        // Mettre le jeu en pause en mettant l'échelle de temps à 0
        Time.timeScale = 0f;
    }


    /// <summary>
    /// Masque le panneau de pause et reprend le temps de jeu.
    /// </summary>
    public void ClosePanel()
    {
        gameObject.SetActive(false);
        // Reprendre le jeu en mettant l'échelle de temps à 1
        Time.timeScale = 1f;
    }

    // --- Gestionnaires d'événements de bouton ---


    /// <summary>
    /// Appelé par le bouton "Retour au jeu".
    /// </summary>
    public void OnReturnToGameClick()
    {
        ClosePanel();
    }


    /// <summary>
    /// Appelé par le bouton "Sauvegarder la partie".
    /// </summary>
    public void OnSaveGameClick()
    {
        // S'assurer que nous avons un GameManager pour obtenir les données
        if (gameManager != null)
        {
            // Créer un nouvel objet GameState avec les données actuelles
            GameState state = new GameState
            {
                score = gameManager.score,
                lives = gameManager.nLives,
                difficulty = gameManager.spawnRate
            };

            // Passer l'état au SaveSystem pour être écrit sur le disque
            SaveSystem.SaveGame(state);
            Debug.Log("Partie sauvegardée avec succès !");
        }
        else
        {
            Debug.LogWarning("Impossible de sauvegarder : référence au GameManager manquante.");
        }
    }


    /// <summary>
    /// Appelé par le bouton "Retour au menu".
    /// </summary>
    public void OnReturnToMenuClick()
    {
        // IMPORTANT : Toujours sortir de la pause avant de changer de scène
        Time.timeScale = 1f;
        SceneNavigator.GoToMenu();
    }


    /// <summary>
    /// Appelé par le bouton "Quitter le jeu" (depuis le menu pause).
    /// </summary>
    public void OnQuitGameClick()
    {
        SceneNavigator.ExitApp();
    }
}