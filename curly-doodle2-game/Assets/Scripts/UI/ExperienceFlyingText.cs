using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceFlyingText : MonoBehaviour
{
    public Text text;
    public GameObject textPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SetFlyingText(float experience)
    {
        text.text = "+ " + experience.ToString() + " exp";
        text.enabled = true;
        yield return new WaitForSeconds(2f);
        text.enabled = false;
    }
}
