using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
	[SerializeField] float minvalue;
	[SerializeField] float maxvalue;

    private void Update()
    {
        var bird = GameObject.FindGameObjectWithTag("Player");
        if(bird)
        {
            var campos = transform.position;
            campos.x = bird.transform.position.x;
            campos.x = Mathf.Clamp(campos.x, minvalue, maxvalue);
            transform.position = Vector3.Lerp(transform.position, campos, 0.3f);
        }     
    }
}
