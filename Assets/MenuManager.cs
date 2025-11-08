using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère l'interface utilisateur du menu principal et ses interactions, comme
/// démarrer une nouvelle partie, charger une sauvegarde et ouvrir le panneau des paramètres.
/// </summary>
public class MenuManager : MonoBehaviour
{

    [Header("UI References")]
    [Tooltip("Le bouton utilisé pour continuer une partie sauvegardée. Doit être désactivé si aucune sauvegarde n'existe.")]
    public GameObject continueButton;
    [Tooltip("Le panneau contenant toutes les options de paramètres.")]
    public GameObject settingsPanel;

    // Référence privée au système de sauvegarde
    private SaveSystem saveSystem;


    /// <summary>
    /// Appelé lorsque l'instance du script est chargée.
    /// </summary>
    void Start()
    {
        // Trouver le composant SaveSystem dans la scène
        saveSystem = FindAnyObjectByType<SaveSystem>();

        // Configurer le bouton 'Continuer' en fonction de la disponibilité du fichier de sauvegarde
        if (continueButton != null)
        {
            // Nous avons besoin d'un système de sauvegarde pour vérifier un fichier de sauvegarde
            if (saveSystem != null)
            {
                bool saveFileExists = saveSystem.CheckHasSave();
                continueButton.SetActive(saveFileExists);
            }
            else
            {
                // S'il n'y a pas de système de sauvegarde, masquer le bouton Continuer
                continueButton.SetActive(false);
            }
        }

        // S'assurer que le panneau des paramètres est masqué au démarrage
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // S'assurer que le jeu tourne à vitesse normale lorsque dans le menu
        Time.timeScale = 1f;
    }


    // --- Gestionnaires de boutons publics ---

    /// <summary>
    /// Appelé lorsque le bouton "Continuer" est pressé.
    /// </summary>
    public void OnContinueClick()
    {
        // Définir un drapeau pour dire au jeu de charger depuis la sauvegarde
        SaveSystem.IsLoadingGame = true;
        // Charger la scène principale du jeu
        SceneNavigator.StartGame();
    }

    /// <summary>
    /// Appelé lorsque le bouton "Nouvelle partie" est pressé.
    /// </summary>
    public void OnNewGameClick()
    {
        // Définir un drapeau pour dire au jeu de *ne pas* charger depuis la sauvegarde
        SaveSystem.IsLoadingGame = false;
        // Charger la scène principale du jeu
        SceneNavigator.StartGame();
    }

    /// <summary>
    /// Appelé lorsque le bouton "Paramètres" est pressé.
    /// </summary>
    public void OnSettingsClick()
    {
        // Basculer la visibilité du panneau des paramètres
        ToggleSettingsPanel();
    }

    /// <summary>
    /// Appelé lorsque le bouton "Quitter" est pressé.
    /// </summary>
    public void OnQuitClick()
    {
        // Fermer l'application
        SceneNavigator.ExitApp();
    }


    /// <summary>
    /// Bascule l'état actif du panneau des paramètres.
    /// S'il est activé, il se désactive. S'il est désactivé, il s'active.
    /// </summary>
    public void ToggleSettingsPanel()
    {
        if (settingsPanel == null)
        {
            Debug.LogWarning("La référence au panneau des paramètres n'est pas définie dans MenuManager.");
            return;
        }

        // Définir le panneau à l'opposé de son état actif actuel
        bool isCurrentlyActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isCurrentlyActive);
    }
}