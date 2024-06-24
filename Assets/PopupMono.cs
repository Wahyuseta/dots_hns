using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMono : MonoBehaviour
{
    float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.Translate(Vector3.up * Time.deltaTime * 10f);

        if(timer > 2f)
            Destroy(gameObject);
    }
}
