using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Ce composant s'assure que le GameObject auquel il est attaché
/// persistera à travers les chargements de scène. Il agit aussi comme un "bootstrapper"
/// pour charger le menu principal depuis une scène initiale (ex: "Boot" ou "Init").
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{

    // Un drapeau statique pour s'assurer qu'une seule instance de cet objet existe
    private static bool instanceInitialized = false;

    /// <summary>
    /// Appelé lorsque l'objet est créé pour la première fois.
    /// </summary>
    void Awake()
    {

        // Vérifier si c'est la première fois que cet objet est créé
        if (!instanceInitialized)
        {
            // Si c'est la première fois, le marquer pour ne pas être détruit
            DontDestroyOnLoad(gameObject);
            instanceInitialized = true;

            // Ce script suppose qu'il démarre dans une scène "Boot"
            // et est responsable du chargement de la scène "Menu"
            SceneManager.LoadScene("Menu");
        }
        else
        {
            // Si une instance existe *déjà* (ex: retour à la scène Boot),
            // détruire ce nouvel objet en double.
            Destroy(gameObject);
        }
    }
}