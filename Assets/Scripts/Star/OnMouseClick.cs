using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.collider.gameObject.name.ToLower())
                {
                    case "sun":
                        break;
                    case "water":
                        break;
                    case "venus":
                        break;
                    case "earth":
                        SceneManager.LoadScene("Level1");
                        break;
                    case "mars":
                        break;
                    case "wood":
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
