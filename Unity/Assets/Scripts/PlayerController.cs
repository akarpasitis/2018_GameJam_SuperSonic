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

    public GameObject cloneShotR;
    public GameObject cloneShotL;

    public Transform SpawnR;
    public Transform SpawnL;

    public float fireRate;
    private float nextFire;
    public float LongTimer = 3;
    public float CloseTimer = 1;

    public bool LongFired = false;
    public bool CloseFired = false;



    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();

        keywords.Add("Far", () =>
        {
            FarFireCalled();
            LongFired = true; 
        });


        keywords.Add("Short", () =>
        {
            CloseFireCalled();
            CloseFired = true;
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


    public void FarFireCalled()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            cloneShotL =  (GameObject)Instantiate(ShotL, SpawnL.position, SpawnL.rotation);
            cloneShotR = (GameObject)Instantiate(ShotR, SpawnR.position, SpawnR.rotation);
           
        }
    }


    public void CloseFireCalled()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            cloneShotL = (GameObject)Instantiate(ShotL, SpawnL.position, SpawnL.rotation);
            cloneShotR = (GameObject)Instantiate(ShotR, SpawnR.position, SpawnR.rotation);

        }
    }



    void Update()
    {
        transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
        if(LongFired == true)
        {

            LongTimer -= Time.deltaTime;
            if (LongTimer < 0)
            {
                Destroy(cloneShotL);
                Destroy(cloneShotR);
                LongTimer = 3;
                LongFired = false;
                
            }
        }

        if (CloseFired == true)
        {

            CloseTimer -= Time.deltaTime;
            if (CloseTimer < 0)
            {
                Destroy(cloneShotL);
                Destroy(cloneShotR);
                CloseTimer = 1;
                CloseFired = false;

            }
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        MoveBat(h, v);
    }

 

    void MoveBat(float h, float v)
    {
        Movement.Set(h, v, 0f);
        Movement = Movement.normalized * MovementSpeed * Time.deltaTime;
        PlayerRB.MovePosition(transform.position + Movement);
    }

}
