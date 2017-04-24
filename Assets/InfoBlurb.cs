using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBlurb : MonoBehaviour {
    [SerializeField] private string infoToDisplay;
    private Text speechBub;
    private GameObject speechParent;
    void Start()
    {
        GameObject playOb = GameObject.FindGameObjectWithTag("Player");
        if (playOb)
        {
            GhostMove GM = playOb.GetComponent<GhostMove>();
            speechBub = GM.GetSpeechBubbleText();
            speechParent = GM.GetSpeechBubbleParent();
        }
    }
    public void OnMouseOver()
    {
        speechParent.gameObject.SetActive(true);
        speechBub.text = infoToDisplay;
    }
    public void OnMouseExit()
    {
        speechParent.gameObject.SetActive(false);
    }
    public void ArtifactUIOver(string text)
    {
        speechParent.gameObject.SetActive(true);
        speechBub.text = text;
    }
    public void ArtifactExit()
    {
        speechParent.gameObject.SetActive(false);
    }
}
