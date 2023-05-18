using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoleDetection : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;
    private BotBehaviour prefabManager;

    private TextMeshProUGUI childTransform;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1UI");
        player2 = GameObject.FindGameObjectWithTag("Player2UI");
        prefabManager = FindObjectOfType<BotBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("BlackCoin"))
        {
            childTransform = player2.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>();
            int x = int.Parse(childTransform.text);
            x = x + 1;

            childTransform.text = x.ToString();
        }
        else if (gameObject.CompareTag("WhiteCoin"))
        {
            childTransform = player1.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>();
            int x = int.Parse(childTransform.text);
            x = x + 1;

            childTransform.text = x.ToString();
        }
        else if (gameObject.CompareTag("Queen"))
        {
            if (prefabManager.isBotPlayerActive == true)
            {
                childTransform = player2.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>();
                int x = int.Parse(childTransform.text);
                x = x + 2;

                childTransform.text = x.ToString();
            }
            else if (prefabManager.isBotPlayerActive == false)
            {
                childTransform = player1.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>();
                int x = int.Parse(childTransform.text);
                x = x + 2;

                childTransform.text = x.ToString();
            }
        }
        Destroy(gameObject);
    }
}
