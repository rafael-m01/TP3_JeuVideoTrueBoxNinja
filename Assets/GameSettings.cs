using UnityEngine;

/// <summary>
/// Une classe statique qui contient et gère les paramètres de jeu persistants
/// en utilisant PlayerPrefs.
/// </summary>
public class GameSettings : MonoBehaviour
{

    // Clés utilisées pour stocker et récupérer les données de PlayerPrefs
    private const string VOLUME_KEY = "GlobalMusicVolume";
    private const string PARTICLES_KEY = "ParticleEffectsEnabled";

    // Propriétés publiques accessibles par d'autres scripts (ex: AudioMixer, ParticleSystems)
    public static float MusicVolume { get; private set; }
    public static bool ShowParticles { get; private set; }


    /// <summary>
    /// Awake est appelé lorsque l'instance du script est chargée,
    /// avant même Start. Idéal pour charger les paramètres.
    /// </summary>
    void Awake()
    {
        // Charger le volume sauvegardé, avec 1.0 (max) par défaut s'il n'est pas trouvé
        MusicVolume = PlayerPrefs.GetFloat(VOLUME_KEY, 1.0f);

        // Charger le paramètre des particules, avec 1 (vrai) par défaut s'il n'est pas trouvé
        int particlesValue = PlayerPrefs.GetInt(PARTICLES_KEY, 1);
        ShowParticles = (particlesValue == 1);
    }


    /// <summary>
    /// Met à jour le volume de la musique et le sauvegarde dans PlayerPrefs.
    /// </summary>
    /// <param name="volume">Le nouveau niveau de volume (0.0 à 1.0).</param>
    public static void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save(); // Persister les changements immédiatement
    }


    /// <summary>
    /// Met à jour le paramètre des particules et le sauvegarde dans PlayerPrefs.
    /// </summary>
    /// <param name="show">Vrai pour afficher les particules, faux pour masquer.</param>
    public static void SetShowParticles(bool show)
    {
        ShowParticles = show;

        // Convertir le booléen en entier (1 pour vrai, 0 pour faux)
        int valueToSave;
        if (show)
        {
            valueToSave = 1;
        }
        else
        {
            valueToSave = 0;
        }

        PlayerPrefs.SetInt(PARTICLES_KEY, valueToSave);
        PlayerPrefs.Save(); // Persister les changements immédiatement
    }
}