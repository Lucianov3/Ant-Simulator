using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainScript : MonoBehaviour
{
    Image image;
    [SerializeField]Sprite[] sprites;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(ChangeImage());
    }

    IEnumerator ChangeImage()
    {
        image.sprite = sprites[Random.Range(0, sprites.Length - 1)];
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(ChangeImage());
    }
}
