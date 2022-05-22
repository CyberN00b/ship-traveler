using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventText : MonoBehaviour
{
    private Animation _animation = null;
    private InterfaceGenerator generator = null;
    public bool is_showed = false;
    public string text = "";
    public string txt_name = "unidentified";
    void Awake() 
    {
        _animation = this.GetComponent<Animation>();
        generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();

    }
    public void changeText(string new_text) 
    {
        this.GetComponent<TextMeshProUGUI>().text = new_text;
        text = new_text;
    }
    public void show() 
    {
        if (!is_showed)
            StartCoroutine("Show");

    }
    public void hide()
    {
        if (is_showed)
            StartCoroutine("Hide");
    }
    public void hideAndDisable()
    {
        StartCoroutine("HideAndDisable");
    }
    public void disableAfterSec(float seconds) {
        StartCoroutine(DisableAfterSec(seconds));
    }
    IEnumerator DisableAfterSec(float seconds) {
        yield return new WaitForSeconds(seconds);
        hideAndDisable();
    }
    IEnumerator Hide()
    {
        _animation.Play("Hide");
        yield return new WaitForSeconds(1f);
        is_showed = false;
    }
    IEnumerator Show()
    {
        _animation.Play("Show");
        yield return new WaitForSeconds(1f);
        is_showed = true;
    }
    IEnumerator HideAndDisable()
    {
        if (is_showed) {
            yield return StartCoroutine("Hide");
        }
        generator.removeEventText(this);
    }
}
