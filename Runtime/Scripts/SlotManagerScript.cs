using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManagerScript : MonoBehaviour
{

    //public ResponsivePosition responsivePosition;

    public List<GameObject> Slots = new List<GameObject>();

    public Transform EndPosRight;
    public Transform EndPosLeft;


    public bool MovingRight;

    public float Space;
    public float Speed;


    public void RemoveObject(GameObject Target)
    {
        Slots.Remove(Target);

        if (Slots.Count > 1)
        {
            Space += Target.GetComponent<SpriteRenderer>().bounds.size.x / (Slots.Count - 1);
        } 
    }



    // Start is called before the first frame update
    void Start()
    {
        // Speed *= responsivePosition.GetScreenRatio();
        // Space *= responsivePosition.GetScreenRatio();

    }


    // Update is called once per frame
    void Update()
    {

        float CurrentSpeed = Speed * Time.deltaTime;

        if (MovingRight)
        {
            // Moving Right
            for (int i = 0; i < Slots.Count; i++)
            {
                if (i + 1 < Slots.Count)
                {
                    // move behind standing items to right

                    if (Slots[i + 1].transform.position.x - Slots[i].transform.position.x > Space)
                    {
                        Slots[i].transform.position += Vector3.right * CurrentSpeed;
                    }
                    //else if (Slots[i + 1].transform.position.x - Slots[i].transform.position.x < Space - 0.15f)
                    //{
                    //    //Slots[i].transform.Translate(-CurrentSpeed * 2, 0, 0);
                    //}
                }
                else
                {
                    // move leading item to right and handle its position switching

                    Slots[i].transform.position += Vector3.right * CurrentSpeed;


                    if (Slots[i].transform.position.x > EndPosRight.transform.position.x + Slots[i].transform.localScale.x)
                    {
                        Slots[i].transform.position = new Vector2(EndPosLeft.transform.position.x - Slots[i].transform.localScale.x,
                            Slots[i].transform.position.y);

                        Slots.Insert(0, Slots[i]);
                        Slots.RemoveAt(Slots.Count - 1);
                    }
                }
            }
        }
        else
        {
            // Moving left
            for (int i = Slots.Count - 1; i > -1; i--)
            {
                if (i - 1 >= 0)
                {
                    // move behind standing items to left

                    if (Slots[i - 1].transform.position.x - Slots[i].transform.position.x < -Space)
                    {
                        Slots[i].transform.position += Vector3.left * CurrentSpeed;
                    }
                    //else if (Slots[i - 1].transform.position.x - Slots[i].transform.position.x > -Space + 0.15f)
                    //{
                    //    //Slots[i].transform.Translate(CurrentSpeed * 2, 0, 0);
                    //}
                }
                else
                {
                    // move leading item to left and handle its position switching

                    Slots[i].transform.position += Vector3.left * CurrentSpeed;


                    if (Slots[i].transform.position.x < EndPosLeft.transform.position.x - Slots[i].transform.localScale.x)
                    {
                        Slots[i].transform.position = new Vector2(EndPosRight.transform.position.x + Slots[i].transform.localScale.x,
                            Slots[i].transform.position.y);

                        Slots.Insert(Slots.Count, Slots[i]);
                       Slots.RemoveAt(0);
                    }
                }
            }
        }
    }



}
