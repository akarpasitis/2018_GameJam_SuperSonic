﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;


public class Recognition : MonoBehaviour {
    KeywordRecognizer KeyWordRec;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private void Start()
    {
        keywords.Add("Fire", () =>
        {
            FireCalled();
        });

        KeyWordRec = new KeywordRecognizer(keywords.Keys.ToArray());
        KeyWordRec.OnPhraseRecognized += KeywordRecognizerOnPhraseRecognized;
        KeyWordRec.Start();
    }

    void KeywordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if(keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }


    void FireCalled()
    {
        print("You just said Fire");
    }

}
