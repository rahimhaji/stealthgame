using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class ViewCone : MonoBehaviour
{
    public GameObject infoMsg;
    public float viewDistance = 6f;
    public float viewAngle = 70f;
    public int rayCount = 40;

    private Mesh mesh;
    //[SerializeField] private LayerMask targetLayer;
    
    void Start()
    {
        infoMsg.SetActive(false);
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray ray1 = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray1.origin, ray1.direction * viewDistance, Color.red, 1f);
            if (Physics.Raycast(ray1, out RaycastHit hitInfo, viewDistance))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);
                
                if (hitInfo.collider.GetComponent<Collider>().name == "guard")
                {
                    ParticleSystem ps = hitInfo.collider.GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        ps.Play();
                    }
            
                    Animator ani1 = hitInfo.collider.GetComponent<Animator>();
                    if (ani1 != null)
                    {
                        ani1.Play("shot_clip1", 0, 0f);
                    }
                    
                }

                if (hitInfo.collider.GetComponent<Collider>().name == "missionwall")
                {
                    infoMsg.SetActive(true);
                }
            }
        }
        

        // if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, viewDistance))
        // {
        //     Debug.Log("Hit: " + hit.collider.name);
        //     Debug.DrawLine(transform.position, hit.point, Color.blue);
        //     ParticleSystem ps = hit.collider.GetComponent<ParticleSystem>();
        //     if (ps != null)
        //     {
        //         ps.Play();
        //     }
            
        //     Animator ani1 = hit.collider.GetComponent<Animator>();
        //     if (ani1 != null)
        //     {
        //         ani1.Play("shot_clip1", 0, 0f);
        //     }
        // }
    }
    void LateUpdate()
    {
        GenerateMesh();
    }



    void GenerateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices.Add(Vector3.zero);

        float angleStep = viewAngle / rayCount;
        float startAngle = -viewAngle * 0.5f;

        for (int i = 0; i <= rayCount; i++)
        {
            float currentAngle = startAngle + angleStep * i;

            Vector3 dir = Quaternion.Euler(0, currentAngle, 0) * transform.forward;

            Vector3 endPoint;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, viewDistance))
            {
                endPoint = transform.InverseTransformPoint(hit.point);
                //Debug.DrawLine(transform.position, hit.point, Color.blue);
            }
            else
            {
                endPoint = transform.InverseTransformPoint(transform.position + dir * viewDistance);
            }

            vertices.Add(endPoint);

            if (i > 0)
            {
                int index = vertices.Count - 1;

                triangles.Add(0);
                triangles.Add(index - 1);
                triangles.Add(index);
            }
        }

        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
    }
}