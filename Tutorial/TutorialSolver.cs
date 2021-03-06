using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class contains methods to manipulate the Rubik's Cube during the tutorial.
/// </summary>
public class TutorialSolver : MonoBehaviour {

	#region Properties

	// The bool variable |turning| indicates whether or not any side of the Rubik's Cube is currently rotating.
	// The bool variable |solving| indicates whether or not this class is actively performing a solution.
	private bool turning = false;
	private bool solving = false;

	// The float variable |speed| is the speed with which the animated rotations are made.
	// The float variable |turnWait| is the time in seconds to pause between single rotations. 
	public float speed = 100f;
	private float turnWait = 0.4f;

	// The GameObject variable |rubiksCube| is the entire Rubik's Cube as a whole.
	public GameObject rubiksCube;

	// The following GameObject variables ending with "face" are used for their Bounds to make the correct rotation.
	// They represent the sides of the Rubik's Cube.
	public GameObject f_face;
	public GameObject d_face;
	public GameObject r_face;
	public GameObject u_face;
	public GameObject l_face;
	public GameObject b_face;

	// The following Bounds variables ending in "bounds" are used to determine which pieces to rotate for a
	// given rotation.
	private Bounds f_bounds;
	private Bounds d_bounds;
	private Bounds r_bounds;
	private Bounds u_bounds;
	private Bounds l_bounds;
	private Bounds b_bounds;

	// The following GameObject variables beginning with "cube" are the pieces of the Rubik's Cube.
	public GameObject cube00;
	public GameObject cube01;
	public GameObject cube02;
	public GameObject cube03;
	public GameObject cube04;
	public GameObject cube05;
	public GameObject cube06;
	public GameObject cube07;
	public GameObject cube08;
	public GameObject cube09;
	public GameObject cube10;
	public GameObject cube11;
	public GameObject cube12;
	public GameObject cube13;
	public GameObject cube14;
	public GameObject cube15;
	public GameObject cube16;
	public GameObject cube17;
	public GameObject cube18;
	public GameObject cube19;
	public GameObject cube20;
	public GameObject cube21;
	public GameObject cube22;
	public GameObject cube23;
	public GameObject cube24;
	public GameObject cube25;
	public GameObject cube26;

	// The bool variable |upSwap| is the bool associated with a GetButtonDown method to flip the view of
	// the Rubik's Cube upside down.
	// The bool variable |doUpSwap| becomes true when <upSwap> becomes true. It prevents multiple flips.
	// The int variable |sunnySideUp| is used as a reference to the current side of the Rubik's Cube that
	// is facing up. Its value will either be 1 for the White side up, and -1 for the Yellow side up.
	private bool upSwap;
	private bool doUpSwap = false;
	private int sunnySideUp = 1;

	// The List<GameObject> variable |allCubes| contains all pieces of the Rubik's Cube.
	// The List<GameObject> variable |faceCubes| is populated with the pieces on a given side.
	private List<GameObject> allCubes  = new List<GameObject> ();
	private List<GameObject> allFaces  = new List<GameObject> ();

	// The List<Vector3> variable |allCubesCenters| contains all the piece center positions.
	// The List<Bounds> variable |allFacesBounds| contains all the "bounds" Bounds variables.
	private List<Vector3> allCubesCenters = new List<Vector3> ();
	private List<Bounds> allFacesBounds = new List<Bounds> ();

	// The List<int> variable |moves| contains all the rotations that have been made.
	private List<int> moves = new List<int> ();
	#endregion

	/// <summary>
	/// Start this instance by populating List properties.
	/// </summary>
	void Start () {

		allFaces.Add (f_face);
		allFaces.Add (d_face);
		allFaces.Add (r_face);
		allFaces.Add (u_face);
		allFaces.Add (l_face);
		allFaces.Add (b_face);

		f_bounds = f_face.GetComponent<MeshCollider> ().bounds;
		d_bounds = d_face.GetComponent<MeshCollider> ().bounds;
		r_bounds = r_face.GetComponent<MeshCollider> ().bounds;
		u_bounds = u_face.GetComponent<MeshCollider> ().bounds;
		l_bounds = l_face.GetComponent<MeshCollider> ().bounds;
		b_bounds = b_face.GetComponent<MeshCollider> ().bounds;

		allFacesBounds.Add (f_bounds);
		allFacesBounds.Add (d_bounds);
		allFacesBounds.Add (r_bounds);
		allFacesBounds.Add (u_bounds);
		allFacesBounds.Add (l_bounds);
		allFacesBounds.Add (b_bounds);

		allCubes.Add (cube00);
		allCubes.Add (cube01);
		allCubes.Add (cube02);
		allCubes.Add (cube03);
		allCubes.Add (cube04);
		allCubes.Add (cube05);
		allCubes.Add (cube06);
		allCubes.Add (cube07);
		allCubes.Add (cube08);
		allCubes.Add (cube09);
		allCubes.Add (cube10);
		allCubes.Add (cube11);
		allCubes.Add (cube12);
		allCubes.Add (cube13);
		allCubes.Add (cube14);
		allCubes.Add (cube15);
		allCubes.Add (cube16);
		allCubes.Add (cube17);
		allCubes.Add (cube18);
		allCubes.Add (cube19);
		allCubes.Add (cube20);
		allCubes.Add (cube21);
		allCubes.Add (cube22);
		allCubes.Add (cube23);
		allCubes.Add (cube24);
		allCubes.Add (cube25);
		allCubes.Add (cube26);
	}

	/// <summary>
	/// Update this instance by checking if the user has pressed the Button to flip the Rubik's Cube.
	/// </summary>
	void Update () {
		upSwap = Input.GetButtonDown ("Jump");
		sunnySideUp = rubiksCube.GetComponent<Tutorial> ().GetUpFace ();
		if (upSwap) {
			doUpSwap = true;
		}
		if (doUpSwap) {
			doUpSwap = false;
//			SetFaceTransforms ();
		}
	}

	#region Turn Mechanisms

	/// <summary>
	/// Rotates the F side clockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator F__cw () {

		float angle = 0;
		Vector3 dir;
		Vector3 dir2;
		if (sunnySideUp < 0) {
			dir = Vector3.down;
			dir2 = Vector3.up;
		} else {
			dir = Vector3.up;
			dir2 = Vector3.down;
		}
		List<GameObject> faceCubes = GetFaceCubes (f_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (dir * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (dir2 * error, Space.World);
				}
			}
		}
		moves.Add (0);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the F side counterclockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator F_ccw () {

		float angle = 0;
		Vector3 dir;
		Vector3 dir2;
		if (sunnySideUp < 0) {
			dir = Vector3.up;
			dir2 = Vector3.down;
		} else {
			dir = Vector3.down;
			dir2 = Vector3.up;
		}
		List<GameObject> faceCubes = GetFaceCubes (f_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (dir * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (dir2 * error, Space.World);
				}
			}
		}
		moves.Add (1);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the D side clockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator D__cw () {

		float angle = 0;
		List<GameObject> faceCubes = GetFaceCubes (d_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (Vector3.right * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (Vector3.left * error, Space.World);
				}
			}
		}
		moves.Add (2);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the D side counterclockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator D_ccw () {

		float angle = 0;
		List<GameObject> faceCubes = GetFaceCubes (d_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (Vector3.left * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (Vector3.right * error, Space.World);
				}
			}
		}
		moves.Add (3);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the R side clockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator R__cw () {

		float angle = 0;
		Vector3 dir;
		Vector3 dir2;
		if (sunnySideUp < 0) {
			dir = Vector3.back;
			dir2 = Vector3.forward;
		} else {
			dir = Vector3.forward;
			dir2 = Vector3.back;
		}
		List<GameObject> faceCubes = GetFaceCubes (r_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (dir * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (dir2 * error, Space.World);
				}
			}
		}
		moves.Add (4);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the R side counterclockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator R_ccw () {

		float angle = 0;
		Vector3 dir;
		Vector3 dir2;
		if (sunnySideUp < 0) {
			dir = Vector3.forward;
			dir2 = Vector3.back;
		} else {
			dir = Vector3.back;
			dir2 = Vector3.forward;
		}
		List<GameObject> faceCubes = GetFaceCubes (r_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (dir * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (dir2 * error, Space.World);
				}
			}
		}
		moves.Add (5);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the U side clockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator U__cw () {

		float angle = 0;
		List<GameObject> faceCubes = GetFaceCubes (u_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (Vector3.left * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (Vector3.right * error, Space.World);
				}
			}
		}
		moves.Add (6);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the U side counterclockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator U_ccw () {

		float angle = 0;
		List<GameObject> faceCubes = GetFaceCubes (u_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (Vector3.right * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (Vector3.left * error, Space.World);
				}
			}
		}
		moves.Add (7);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the L side clockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator L__cw () {

		float angle = 0;
		Vector3 dir;
		Vector3 dir2;
		if (sunnySideUp < 0) {
			dir = Vector3.forward;
			dir2 = Vector3.back;
		} else {
			dir = Vector3.back;
			dir2 = Vector3.forward;
		}
		List<GameObject> faceCubes = GetFaceCubes (l_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (dir * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (dir2 * error, Space.World);
				}
			}
		}
		moves.Add (8);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the L side counterclockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator L_ccw () {

		float angle = 0;
		Vector3 dir;
		Vector3 dir2;
		if (sunnySideUp < 0) {
			dir = Vector3.back;
			dir2 = Vector3.forward;
		} else {
			dir = Vector3.forward;
			dir2 = Vector3.back;
		}
		List<GameObject> faceCubes = GetFaceCubes (l_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (dir * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (dir2 * error, Space.World);
				}
			}
		}
		moves.Add (9);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the B side clockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator B__cw () {

		float angle = 0;
		Vector3 dir;
		Vector3 dir2;
		if (sunnySideUp < 0) {
			dir = Vector3.down;
			dir2 = Vector3.up;
		} else {
			dir = Vector3.up;
			dir2 = Vector3.down;
		}
		List<GameObject> faceCubes = GetFaceCubes (b_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (dir * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (dir2 * error, Space.World);
				}
			}
		}
		moves.Add (10);
		turning = false;
		yield return null;
	}

	/// <summary>
	/// Rotates the B side counterclockwise.
	/// </summary>
	/// <remarks>
	/// This is an iterator in order to "animate" the rotation.
	/// </remarks>
	IEnumerator B_ccw () {

		float angle = 0;
		Vector3 dir;
		Vector3 dir2;
		if (sunnySideUp < 0) {
			dir = Vector3.up;
			dir2 = Vector3.down;
		} else {
			dir = Vector3.down;
			dir2 = Vector3.up;
		}
		List<GameObject> faceCubes = GetFaceCubes (b_bounds);
		while (angle < 90) {
			foreach (GameObject cube in faceCubes) {
				cube.transform.Rotate (dir * speed * Time.deltaTime, Space.World);
			}
			angle += speed * Time.deltaTime;
			if (angle < 90) {
				yield return new WaitForSeconds (0.01f);
			} else if (angle > 90) {
				float error = angle - 90;
				foreach (GameObject cube in faceCubes) {
					cube.transform.Rotate (dir2 * error, Space.World);
				}
			}
		}
		moves.Add (11);
		turning = false;
		yield return null;
	}
	#endregion

	/// <summary>
	/// Gets the pieces on a given side.
	/// </summary>
	/// <returns>The pieces on a given side.</returns>
	/// <param name="bound">The side.</param>
	List<GameObject> GetFaceCubes(Bounds bound) {
		List<GameObject> faceCubes = new List<GameObject> ();
		allCubesCenters = GetCubesCenters ();
		foreach (Vector3 center in allCubesCenters) {
			if (bound.Contains(center)) {
				int i = allCubesCenters.IndexOf(center);
				faceCubes.Add(allCubes[i]);
			}
		}
		turning = true;
		return faceCubes;
	}

	/// <summary>
	/// Gets the centers of the pieces.
	/// </summary>
	/// <returns>The centers of the pieces.</returns>
	List<Vector3> GetCubesCenters () {
		allCubesCenters.Clear ();
		foreach (GameObject cube in allCubes) {
			Vector3 cubeCenter = cube.GetComponent<BoxCollider> ().bounds.center;
			allCubesCenters.Add (cubeCenter);
		}
		return allCubesCenters;
	}

	/// <summary>
	/// Sets the face transforms according to the faceConfig.
	/// </summary>
	public void SetFaceTransforms () {

//		Vector3 oldFpos = allFaces [0].transform.position;
//		allFaces [0].transform.position = allFaces [5].transform.position;
//		allFaces [5].transform.position = oldFpos;
//
//		Vector3 oldRpos = allFaces [2].transform.position;
//		allFaces [2].transform.position = allFaces [4].transform.position;
//		allFaces [4].transform.position = oldRpos;
		sunnySideUp = rubiksCube.GetComponent<Tutorial> ().GetUpFace ();

		for (int i = 0; i < allFaces.Count; i++) {
			allFacesBounds [i] = allFaces [i].GetComponent<MeshCollider> ().bounds;
		}

		f_bounds = allFacesBounds [0];
		d_bounds = allFacesBounds [1];
		r_bounds = allFacesBounds [2];
		u_bounds = allFacesBounds [3];
		l_bounds = allFacesBounds [4];
		b_bounds = allFacesBounds [5];
	}

	/// <summary>
	/// Performs the specified algorithm.
	/// </summary>
	/// <param name="algorithm">Algorithm.</param>
	public IEnumerator PerformAlgorithm (List<int> algorithm) {
		solving = true;
		foreach (int move in algorithm) {
//			print (move);
			turning = true;
			switch (move) {
			case 0:
				StartCoroutine (F__cw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 1:
				StartCoroutine (F_ccw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 2:
				StartCoroutine (D__cw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 3:
				StartCoroutine (D_ccw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 4:
				StartCoroutine (R__cw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 5:
				StartCoroutine (R_ccw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 6:
				StartCoroutine (U__cw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 7:
				StartCoroutine (U_ccw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 8:
				StartCoroutine (L__cw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 9:
				StartCoroutine (L_ccw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 10:
				StartCoroutine (B__cw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			case 11:
				StartCoroutine (B_ccw ());
				yield return new WaitForSeconds (turnWait);
				while (turning) {
					yield return null;
				}
				break;
			}
		}
		solving = false;
	}

	/// <summary>
	/// Gets the side facing up.
	/// </summary>
	/// <returns>The side facing up.</returns>
	public int GetUpFace () {
		return sunnySideUp;
	}

	/// <summary>
	/// Determines whether this instance is turning.
	/// </summary>
	/// <returns><c>true</c> if this instance is turning; otherwise, <c>false</c>.</returns>
	public bool IsTurning () {
		return turning;
	}

	/// <summary>
	/// Determines whether this instance is solving.
	/// </summary>
	/// <returns><c>true</c> if this instance is solving; otherwise, <c>false</c>.</returns>
	public bool IsSolving () {
		return solving;
	}
}
