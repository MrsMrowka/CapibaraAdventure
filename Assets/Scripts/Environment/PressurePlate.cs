using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject[] doorToOpen;

    private Animator animator;
    private Animator[] animatorToOpen;
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorToOpen = new Animator[doorToOpen.Length];
        for(int i = 0; i < doorToOpen.Length; i++)
        {
            animatorToOpen[i] = doorToOpen[i].GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("BoxToPush"))
        {
            GlobalVariables.UsedPressurePlate += 1;
            animator.SetBool("PushDown", true);

            foreach (var item in animatorToOpen)
            {
                item.SetBool("PushDown", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("BoxToPush"))
        {
            animator.SetBool("PushDown", false);
            foreach (var item in animatorToOpen)
            {
                item.SetBool("PushDown", false);
            }
        }
    }
}
