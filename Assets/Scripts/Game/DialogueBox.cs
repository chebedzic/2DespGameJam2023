using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshPro dialog;

    public IEnumerator EndingText()
    {
        
        gameObject.SetActive(true);
        dialog.text = "Device su kljucne za povracaj novca";
        yield return new WaitForSeconds(2);
        dialog.text = "Device ce ti pomoci u snalazenju  terenu, odnosno u orijentaciji";
        yield return new WaitForSeconds(2);
        dialog.text = "Device ce ti pomoci u snalazenjuterenu, odnosno u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device ce ti pomoci u snalazeenu, odnosno u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device ce ti pomoci u snu, odnosno u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device ce ti pomoci u , odnosno u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device ce ti pomoci odnosno u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device ce ti pomonosno u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device ce ti po u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device ce u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device u orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device orijentaciji";
        yield return new WaitForSeconds(0.2f);
        dialog.text = "Device orijentacija";
        yield return new WaitForSeconds(2);
        dialog.text = "Device orientation";
    }
}
