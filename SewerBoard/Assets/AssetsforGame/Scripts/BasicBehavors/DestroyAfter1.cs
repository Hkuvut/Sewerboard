using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter1 : MonoBehaviour
{
    [Header("Time until gameobject does blckout challenge, no need to change variable timeLeft")]
    public float timeLeft;
    [Header("Time total that gameobject will take to die/destroy")]
    public float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, deathTime);
        timeLeft = deathTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
    }
}
