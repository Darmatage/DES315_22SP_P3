using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A04_LaserEyeAttack : MonoBehaviour
{
    public GameObject leftEyePrefab;
    public GameObject rightEyePrefab;

    private GameObject curLeft;
    private GameObject curRight;

    public string button;

    private bool isWeaponOut = false;

    public float lengthOfAttack = 4f;

    private Vector3 leftOGpos;
    private Vector3 rightOGpos;


    private bool isDone = false;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        leftOGpos = leftEyePrefab.transform.localPosition;
        rightOGpos = rightEyePrefab.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown(button)) && !isWeaponOut)
        {

            curLeft = Instantiate(leftEyePrefab, leftOGpos, Quaternion.identity);
            curRight = Instantiate(rightEyePrefab, rightOGpos, Quaternion.identity);

            Vector3 tarLeft = new Vector3(lengthOfAttack, - lengthOfAttack, 0);
            Vector3 tarRight = new Vector3(lengthOfAttack, - lengthOfAttack, 0);

            StartCoroutine(LerpPosition(tarLeft, tarRight, 1.5f));

            isWeaponOut = true;
            
        }

        if(isWeaponOut && isDone)
        {
            isDone = false;
            StartCoroutine(WithdrawWeapon());
        }
    }


    IEnumerator LerpPosition(Vector3 tarLeft, Vector3 tarRight, float dur)
    {
        float time = 0;

        Vector3 leftStart = curLeft.transform.position;
        Vector3 rightStart = curRight.transform.position;

        while (time < dur)
        {
            curLeft.transform.position = Vector3.Lerp(leftStart, tarLeft, time / dur);
            curRight.transform.position = Vector3.Lerp(rightStart, tarRight, time / dur);

            time += Time.deltaTime;
  
            yield return null;
        }

        isDone = true;
    }

    IEnumerator WithdrawWeapon()
    {
        yield return new WaitForSeconds(0.6f);

        if(curLeft)
        {
            Destroy(curLeft);
        }

        if(curRight)
        {
            Destroy(curRight);
        }

        isWeaponOut = false;
    }
}
