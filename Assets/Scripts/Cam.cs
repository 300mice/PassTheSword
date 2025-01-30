using UnityEngine;

public class Cam : MonoBehaviour
{
    private ObjectFade _fader;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        //GameObject PartyMember = GameObject.FindGameObjectWithTag("PartyMember");

        GameObject[] PartyMembers = GameObject.FindGameObjectsWithTag("PartyMember");

        for(int i = 0; i < PartyMembers.Length; i++)
        {
            GameObject member = PartyMembers[i];

            if (member != null)
            {
                Vector3 dir = member.transform.position - transform.position;
                Ray ray = new Ray(transform.position, dir);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawRay(transform.position, hit.point);


                    if (hit.collider == null)
                        return;

                    if (!hit.collider.gameObject == member)
                    {
                        _fader = hit.collider.gameObject.GetComponent<ObjectFade>();

                        if (_fader != null)
                        {
                            _fader.DoFade = false;
                            Debug.Log("huh");
                        }
                        else
                        {
                            _fader = hit.collider.gameObject.GetComponent<ObjectFade>();
                            if (_fader != null)
                            {
                                _fader.DoFade = true;
                            }

                        }


                    }

                }

            }
        }

        /*
        if (PartyMember != null)
        {
            Vector3 dir = PartyMember.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                    return;

                if (hit.collider.gameObject == PartyMember)
                {
                    if(_fader != null)
                    {
                        _fader.DoFade = false;
                    }
                    else
                    {
                        _fader = hit.collider.gameObject.GetComponent<ObjectFade>();
                        if(_fader != null)
                        {
                            _fader.DoFade = true;
                        }

                    }

                }

            }

        }*/
    }
}