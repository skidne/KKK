using UnityEngine;
using System.Collections;

public class TestRoll : MonoBehaviour {

    public void Roll()
    {
        GameObject.Find("Astro").GetComponent<Animator>().SetTrigger("Roll");
    }
}
