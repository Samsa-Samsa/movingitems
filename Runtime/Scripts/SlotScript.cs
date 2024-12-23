using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;


public enum Types
{
    type1, type2, type3, type4, type5, type6, type7, type8, type9, type10, type11, type12, type13, type14, type15
}

public class SlotScript : MonoBehaviour
{

    public GameManagerScript gameManagerScript;
    public SlotManagerScript slotManagerScript;
    public AudioManagerScript audioManagerScript;

    public float ScalingFactor;

    public float RandomXMin;
    public float RandomXMax;
    public float RandomYMin;
    public float RandomYMax;

    [SerializeField] private Types type;
    AnimationCurve TrajectoryCurve;

    public Types SlotTypes
    {
        get => type;
        set => type = value;
    }
    GameObject Carier;

    Vector2 CartPos;
    Vector2 OriginalScale;

    float TimeToReachBasket;
    float Angle;
    float InitialDistance;
    float TrajCurveFirstKeyT;
    float TrajCurveLastKeyT;
    float HeightMultiplier;

    int RockingSpeed;
    int MaxRockingAngle;

    bool Rocking;
    bool LeftFinished;
    bool RightFinished;
    bool GetCollected;
    bool SlotArrived;
    bool NewIsVisible;

    [HideInInspector]
    public bool IsVisible;




    //IEnumerator DelayItemChangeSound()
    //{
    //    yield return new WaitForSeconds(0.5f );

    //    print("change");
    //    audioManagerScript.PlayItemChangeSound();
    //    gameManagerScript.SetSampleSlot();

    //}


    bool CheckVisibility()
    {
        bool visible = transform.position.x < slotManagerScript.EndPosRight.transform.position.x
          - GetComponent<SpriteRenderer>().bounds.size.x / 2 && transform.position.x > slotManagerScript.EndPosLeft.transform.position.x
          + GetComponent<SpriteRenderer>().bounds.size.x / 2;

        return visible;
    }


    void HandleRocking()
    {
        if (Rocking)
        {
            if (Angle < MaxRockingAngle && !LeftFinished)
            {
                Angle = Mathf.Lerp(Angle, Angle + 5, RockingSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, Angle);
            }
            else if (Angle >= MaxRockingAngle)
            {
                LeftFinished = true;
            }


            if (Angle > -MaxRockingAngle && !RightFinished && LeftFinished)
            {
                Angle = Mathf.Lerp(Angle, Angle - 5, RockingSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, Angle);
            }
            else if (Angle <= -MaxRockingAngle)
            {
                RightFinished = true;
            }


            if (Angle < 0 && LeftFinished && RightFinished)
            {
                Angle = Mathf.Lerp(Angle, Angle + 5, RockingSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, Angle);
            }
            else if (LeftFinished && RightFinished)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                LeftFinished = false;
                RightFinished = false;
                Rocking = false;
            }
        }
    }


   


    void OnMouseDown()
    {

        if (GameManagerScript.CurrentSlotTag == type.ToString() && IsVisible)
        {
            // collection of item is executing here
            // item collection sound can be played here

            transform.localScale = new Vector2(transform.localScale.x + (transform.localScale.x * ScalingFactor / 100),
               transform.localScale.y + (transform.localScale.y * ScalingFactor / 100));

            audioManagerScript.PlayTapSound();

            slotManagerScript.RemoveObject(gameObject);

            gameManagerScript.SlotsCollected++;
            gameManagerScript.DeactivateGuiding();

            Carier = new GameObject("Slot " + gameManagerScript.SlotsCollected);

            Carier.transform.position = transform.position;

            transform.parent = Carier.transform;

            transform.localPosition = new Vector2(0, 0);

   
            gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder += gameManagerScript.SlotsCollected;

            GetCollected = true;

            float RandomX = Random.Range(RandomXMin, RandomXMax);
            float RandomY = Random.Range(RandomYMin, RandomYMax);

            if (gameManagerScript.SlotsCollected % 2 == 0)
            {
                RandomX *= 1;
                RandomY *= 1;
            }
            else
            {
                RandomX *= -1;
                RandomY *= -1;
            }

            CartPos = new Vector2(gameManagerScript.SlotInCartPos.position.x + RandomX,
              gameManagerScript.SlotInCartPos.position.y + RandomY);


            InitialDistance = Mathf.Abs(CartPos.x - Carier.transform.position.x);

        }
        else if(!GetCollected && IsVisible)
        {
            // wrong item touching executing here
            // wrong item touching sound can be played here

            audioManagerScript.PlayWrongTapSound();

            Rocking = true;
        }
    }



    void Start()
    {

        TimeToReachBasket = gameManagerScript.ItemTimeToReachBasket;
        HeightMultiplier = gameManagerScript.ItemHeightMultiplier;

        RockingSpeed = gameManagerScript.ItemRockingSpeed;
        MaxRockingAngle = gameManagerScript.ItemMaxRockingAngle;

        TrajectoryCurve = gameManagerScript.TrajectoryCurve;

        TrajCurveFirstKeyT = TrajectoryCurve.keys[0].time;
        TrajCurveLastKeyT = TrajectoryCurve.keys[TrajectoryCurve.length - 1].time;

        OriginalScale = transform.localScale;

    }

    void Update()
    {

        IsVisible = CheckVisibility();


        if (GetCollected)
        {

            if(transform.localScale.x > OriginalScale.x + 0.001f)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, OriginalScale, 4 * Time.deltaTime);
                //print("test");
            }


            if (Vector2.Distance(Carier.transform.position, CartPos) > 0.02f)
            {
                // moving item to basket after its collected

                Carier.transform.position = Vector2.MoveTowards(Carier.transform.position, CartPos, 
                    InitialDistance / TimeToReachBasket * Time.deltaTime);

                float DistanceToTarget = Mathf.Abs( CartPos.x - Carier.transform.position.x );

                float CurveProgress = math.remap(InitialDistance, 0, TrajCurveFirstKeyT, TrajCurveLastKeyT, DistanceToTarget);

                float TrajectoryCurveVal = TrajectoryCurve.Evaluate(CurveProgress);

                transform.localPosition = new Vector3(0, TrajectoryCurveVal * HeightMultiplier, 0);

            }
            else if(!SlotArrived)
            {
                // changing target item on basket and play geraffe animation executing here

                audioManagerScript.PlayGettingIntoBasketSound();

                gameManagerScript.ChangeAnimation();

                StartCoroutine(gameManagerScript.DelayItemChange());
                SlotArrived = true;
                
            }
        }


        HandleRocking();

    }
    
    
}


