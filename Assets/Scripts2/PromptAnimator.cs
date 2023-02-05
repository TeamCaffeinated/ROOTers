using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptAnimator : MonoBehaviour
{
    private TMP_Text promptText;
    private RectTransform rTransform;
    private bool blinkOn = false;
    private float lastBlinkTimestamp = 0;
    private float lastTypeTimestamp = 0;
    private uint nextLetter = 0;
    private bool doneTyping = false;
    private float doneSpeed = 0f;

    public float blinkFrequency;
    public float typeFrequency;
    public float typeDelay;
    public string commandText;
    public float doneAcceleration;
    public float postTypeDelay;
    public GameObject animatedStarter;

    // Start is called before the first frame update
    void Start()
    {
        promptText = GetComponent<TMP_Text>();
        rTransform = GetComponent<RectTransform>();
        animatedStarter.GetComponent<AnimatedStarterController>().doneAcceleration = doneAcceleration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastBlinkTimestamp > blinkFrequency)
        {
            Blink();
        }
        if ((nextLetter == 0 && Time.time > typeDelay)
            || (Time.time - lastTypeTimestamp > typeFrequency && nextLetter < commandText.Length && nextLetter > 0))
        {
            TypeLetter();
        }
        if (doneTyping && Time.time - lastTypeTimestamp > postTypeDelay) {
            doneSpeed += doneAcceleration * Time.deltaTime * 46;
            rTransform.position += new Vector3(-doneSpeed * Time.deltaTime, 0, 0);
            animatedStarter.SetActive(true);
        }
    }

    void Blink()
    {
        if (blinkOn)
            promptText.text = promptText.text.Substring(0, promptText.text.Length - 1);
        else
            promptText.text = promptText.text + "_";
        blinkOn = !blinkOn;
        lastBlinkTimestamp = Time.time;
    }

    void TypeLetter()
    {
        promptText.text = promptText.text + commandText.Substring((int)nextLetter, 1);
        nextLetter++;
        lastTypeTimestamp = Time.time;
        lastBlinkTimestamp = Time.time;
        if (nextLetter == commandText.Length)
            doneTyping = true;
    }
}
