

•	Place the files and folders on a server (mya be IIS). 
•	Enable webgl on the server and browser. 
•	Add MIME files, make sure all below MIME files available on sever.

File Extension	MIME Type	Description
.wasm	application/wasm	WebAssembly binary containing game logic
.data	application/octet-stream	Asset data, textures, and scene environments
.unityweb	application/octet-stream	Compressed asset format (used in some Unity versions)
.json	application/json	Project configuration and debug data

•	Then run the URL as in server.



Programming Explanation:
Folder Structure:
All animations, Characters, Materials, Scenes, Scripts and Textures available in the relevant folders as below.
 
I have used below two c# files here:
•	GameController.cs
•	ViewCone.cs
“GameController.cs” contains logic to move the character.
“ViewCone.cs” contains logic to produce view cone, ray passing and shooting and other game logics. Find below the detailed code explanation.
ViewCone.cs: 
•	Viewdistance, view Angle, Raycount are variables, we can change as per our requirement.
•	public float viewDistance = 6f;
•	    public float viewAngle = 70f;
•	    public int rayCount = 40;

•	In start, I have created mesh

void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

•	“void GenerateMesh()” will generate a view cone with meshes. 
•	Inside late update I called that “generateMesh()” function.
•	In generateMesh, we are creating triangle with raycast and meshs.
•	Initially will create a single triangle and keep increasing those triangles to form a circle as shown below.
 
•	We have 2 list arrays, vertices and triangles to store those vector3 coordinates.
•	Calculations for angle increment and initial start angle.
•	
•	        float angleStep = viewAngle / rayCount;
•	        float startAngle = -viewAngle * 0.5f;

•	Forming triangle and keep adding triangles as many raycounts.

•	We added  if condition “if (Physics.Raycast(transform.position, dir, out hit, viewDistance))” to restrict the end point of line, if any collider hits, example wall.

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
•	For forming the trianlges with fill 
•	          if (i > 0)
•	            {
•	                int index = vertices.Count - 1;
•	
•	                triangles.Add(0);
•	                triangles.Add(index - 1);
•	                triangles.Add(index);
•	            }

mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();

•	All asset lists are :
 

My publish Settings:
•	Publishing Settings → Compression Format:.
•	Managed Stripping Level: High. Enable Strip Engine Code.
•	Code Optimization: Disk Size with LTO.
•	Exception Support: None (or “Explicitly Thrown Exceptions Only”).
•	Data Caching: off, unless you need it.
------------------------------------END------------------------------------------------








