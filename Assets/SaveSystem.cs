using UnityEngine;
using System.IO; // Utilisé pour les opérations de fichiers (Path, File)
using Newtonsoft.Json; // Utilisation de la bibliothèque Json.NET pour la sérialisation
using System; // Pour la gestion des exceptions (Exception)

/// <summary>
/// Définit la structure de données de ce qui est sauvegardé.
/// Doit être [System.Serializable] si on utilise JsonUtility d'Unity,
/// mais c'est une bonne pratique même pour Json.NET.
/// </summary>
[System.Serializable]
public class GameState
{
    public int score;
    public int lives;
    public float difficulty;
}

/// <summary>
/// Une classe utilitaire statique pour gérer les opérations de sauvegarde et de chargement
/// en sérialisant un objet GameState dans un fichier JSON.
/// </summary>
public class SaveSystem : MonoBehaviour
{

    /// <summary>
    /// Un drapeau global qui indique au GameManager s'il doit
    /// charger depuis la sauvegarde ou démarrer une nouvelle partie.
    /// </summary>
    public static bool IsLoadingGame = false;


    /// <summary>
    /// Obtient le chemin complet et indépendant de la plateforme vers le fichier de sauvegarde.
    /// </summary>
    private static string SavePath
    {
        get
        {
            // Application.persistentDataPath est un répertoire sûr et accessible en écriture
            // sur toutes les plateformes (Windows, macOS, Android, iOS, etc.)
            return Path.Combine(Application.persistentDataPath, "gamesave.json");
        }
    }


    /// <summary>
    /// Sérialise le GameState donné en JSON et l'écrit dans le fichier de sauvegarde.
    /// </summary>
    /// <param name="state">Les données de jeu à sauvegarder.</param>
    public static void SaveGame(GameState state)
    {
        try
        {
            // Convertir l'objet GameState en une chaîne JSON formatée
            string json = JsonConvert.SerializeObject(state, Formatting.Indented);

            // Écrire la chaîne JSON dans le fichier, en écrasant s'il existe
            File.WriteAllText(SavePath, json);

            Debug.Log("État du jeu sauvegardé dans : " + SavePath);
        }
        catch (Exception ex)
        {
            // Enregistrer les erreurs qui se produisent lors de l'E/S de fichier
            Debug.LogError($"Échec de la sauvegarde du jeu : {ex.Message}");
        }
    }


    /// <summary>
    /// Vérifie si un fichier de sauvegarde existe déjà.
    /// </summary>
    /// <returns>Vrai si le fichier existe, faux sinon.</returns>
    public bool CheckHasSave()
    {
        return File.Exists(SavePath);
    }


    /// <summary>
    /// Charge le JSON depuis le fichier de sauvegarde et le désérialise
    /// en un objet GameState.
    /// </summary>
    /// <returns>Le GameState chargé, ou null si le chargement échoue.</returns>
    public static GameState LoadStateFromSave()
    {
        // D'abord, vérifier si le fichier existe
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("Aucun fichier de sauvegarde trouvé à : " + SavePath);
            return null;
        }

        try
        {
            // Lire tout le texte du fichier de sauvegarde
            string json = File.ReadAllText(SavePath);

            // Reconvertir la chaîne JSON en objet GameState
            return JsonConvert.DeserializeObject<GameState>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Échec du chargement de l'état du jeu : {ex.Message}");
            return null;
        }
    }
}