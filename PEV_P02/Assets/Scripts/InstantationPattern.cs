using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantationPattern : MonoBehaviour
{
    public GameObject prefab; // Prefab del game object a instanciar
    public int numInstancies = 10; // Nombre d'instÓncies a crear
    public float radi = 5f; // Radi del cercle on s'instanciaran els game objects

    private void Start()
    {
        // Creem les instÓncies en un cercle al voltant de l'objecte principal
        for (int i = 0; i < numInstancies; i++)
        {
            // Calculem la posiciˇ del game object en el cercle
            float angle = i * Mathf.PI * 2 / numInstancies; // angle de separaciˇ entre les instÓncies
            Vector3 posicio = transform.position + new Vector3(Mathf.Cos(angle) * radi, 0f, Mathf.Sin(angle) * radi);

            // Instanciem el game object a la posiciˇ calculada
            Instantiate(prefab, posicio, Quaternion.identity);
        }
    }
}
