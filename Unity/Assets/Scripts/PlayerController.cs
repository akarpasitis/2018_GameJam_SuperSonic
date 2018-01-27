using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;


public class PlayerController : MonoBehaviour {

    KeywordRecognizer KeyWordRec;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();



    public float MovementSpeed = 20;
    Rigidbody PlayerRB;
    Vector3 Movement;

    public GameObject ShotR;
    public GameObject ShotL;

    public Transform SpawnR;
    public Transform SpawnL;

    public float fireRate;
    private float nextFire;


    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();

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
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }


    void FireCalled()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(ShotL, SpawnL.position, SpawnL.rotation);
            Instantiate(ShotR, SpawnR.position, SpawnR.rotation);
        }
    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        MoveBat(h, v);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
    }

    void MoveBat(float h, float v)
    {
        Movement.Set(h, v, 0f);
        Movement = Movement.normalized * MovementSpeed * Time.deltaTime;
        PlayerRB.MovePosition(transform.position + Movement);
    }

}
