using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class contains methods to provide a step-by-step approach to solving a Rubik's Cube.
/// </summary>
public class Tutorial : MonoBehaviour {

	#region Properties

	// The GameObject variable |solver| contains the script to scramble and solve the Rubik's Cube.
	public GameObject solver;

	// The Text variable |tutor| is the main feature of the tutorial. It displays the instructions.
	public Text tutor;

	public Camera cam;

	// The GameObject variable |keyboard| is used to display sprites of the buttons required to
	// manipulate the Rubik's Cube.
	public GameObject keyboard;

	// The following Material variables are used to emphasize groups of pieces of the Rubik's Cube
	// to facilitate pattern recognition.
	public Material plastic;
	public Material glow;
	public Material any;

	public Sprite key_none;
	public Sprite key_f;
	public Sprite key_fi;
	public Sprite key_d;
	public Sprite key_di;
	public Sprite key_r;
	public Sprite key_ri;
	public Sprite key_u;
	public Sprite key_ui;
	public Sprite key_l;
	public Sprite key_li;
	public Sprite key_b;
	public Sprite key_bi;

	// The following GameObject variables are used to display the sprites showing key configurations of
	// the Rubik's Cube.
	public GameObject summaryDone;
	public GameObject summary1;
	public GameObject summary2;
	public GameObject summary3;
	public GameObject summary4;
	public GameObject config1of3;
	public GameObject config2of3;
	public GameObject config3of3;

	// The following Text variables are used to display the algorithms necessary to solve the Rubik's
	// Cube for corresponding configurations.
	public Text summaryDoneText;
	public Text summary1Text;
	public Text summary2Text;
	public Text summary3Text;
	public Text summary4Text;

	// The following Sprite variables are used with the summary variables above.
	public Sprite state1_1a;
	public Sprite state1_1b;
	public Sprite state1done;
	public Sprite state2_1a;
	public Sprite state2_1b;
	public Sprite state2_1c;
	public Sprite state2_2a;
	public Sprite state2done;
	public Sprite state3_1a;
	public Sprite state3_2a;
	public Sprite state3done;
	public Sprite state4_1a;
	public Sprite state4_2a;
	public Sprite state4_3a;
	public Sprite state4done;
	public Sprite state5_1a;
	public Sprite state5_2a;
	public Sprite state5_3a;
	public Sprite state5done;
	public Sprite state6_1a;
	public Sprite state6_2a;
	public Sprite state6_3a;
	public Sprite state6_3b;
	public Sprite state6done;

	// The following GameObject variables ending with "face" are used for their Bounds to make the correct rotation.
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

	private Material[] original00;
	private Material[] original01;
	private Material[] original02;
	private Material[] original03;
	private Material[] original04;
	private Material[] original05;
	private Material[] original06;
	private Material[] original07;
	private Material[] original08;
	private Material[] original09;
	private Material[] original10;
	private Material[] original11;
	private Material[] original12;
	private Material[] original13;
	private Material[] original14;
	private Material[] original15;
	private Material[] original16;
	private Material[] original17;
	private Material[] original18;
	private Material[] original19;
	private Material[] original20;
	private Material[] original21;
	private Material[] original22;
	private Material[] original23;
	private Material[] original24;
	private Material[] original25;
	private Material[] original26;

	// The following int variables represent the rotations of the Rubik's Cube.
	private int f;
	private int fi;
	private int d;
	private int di;
	private int r;
	private int ri;
	private int u;
	private int ui;
	private int l;
	private int li;
	private int b;
	private int bi;

	// The int variable |tutorialStep| is used to display the correct instructions and animations
	// during the tutorial.
	// The bool variable |stepDone| is used to ensure the animations of a particular step are not done
	// more than once.
	private int tutorialStep = 0;
	private bool stepDone = false;

	// The bool variable |upSwap| is the bool associated with a GetButtonDown method to flip the view of
	// the Rubik's Cube upside down.
	// The bool variable |doUpSwap| becomes true when <upSwap> becomes true. It prevents multiple flips.
	// The int variable |sunnySideUp| is used as a reference to the current side of the Rubik's Cube that
	// is facing up. Its value will either be 1 for the White side up, and -1 for the Yellow side up.
	// The Quaternion variable |whiteSideUp| contains the rotation of the Rubik's Cube's solved
	// configuration, with the White side facing up.
	private bool upSwap;
	private bool doUpSwap = false;
	private int sunnySideUp = 1;
	private Quaternion whiteSideUp;

	// The int variable |faceConfig| represents the current F side of the Rubik's Cube according to
	// the camera view.
	private int faceConfig = 0;

	// The following List<GameObject> variables contain groups of pieces.
	private List<GameObject> allFaces = new List<GameObject> ();
	private List<GameObject> sortedFacesBlue = new List<GameObject> ();
	private List<GameObject> sortedFacesOrange = new List<GameObject> ();
	private List<GameObject> sortedFacesGreen = new List<GameObject> ();
	private List<GameObject> sortedFacesRed = new List<GameObject> ();

	private List<GameObject> allCubes = new List<GameObject> ();
	private List<GameObject> edgeCubes = new List<GameObject> ();
	private List<GameObject> cornerCubes = new List<GameObject> ();
	private List<GameObject> centerCubes = new List<GameObject> ();

	private List<GameObject> whiteCross = new List<GameObject> ();
	private List<GameObject> whiteCorners = new List<GameObject> ();
	private List<GameObject> middleLayer = new List<GameObject> ();
	private List<GameObject> yellowCross = new List<GameObject> ();
	private List<GameObject> yellowCorners = new List<GameObject> ();
	private List<GameObject> topLayer = new List<GameObject> ();

	// The following List variables are used to manipulate the sides of the Rubik's Cube if the
	// the Cube is turned upside down, or rotated to a different camera view.
	private static List<Vector3> refFacePositions = new List<Vector3> ();
	private List<Bounds> allFacesBounds = new List<Bounds> ();
	private List<Bounds> sortedBounds = new List<Bounds> ();

	#endregion

	/// <summary>
	/// Start this instance by populating the List properties.
	/// </summary>
	void Start () {
		allFaces.Add (f_face);
		allFaces.Add (d_face);
		allFaces.Add (r_face);
		allFaces.Add (u_face);
		allFaces.Add (l_face);
		allFaces.Add (b_face);

		foreach (GameObject face in allFaces) {
			Vector3 pos = face.transform.position;
			refFacePositions.Add (pos);
		}
			
		keyboard.SetActive (false);

		sortedFacesBlue.Add (u_face);
		sortedFacesBlue.Add (b_face);
		sortedFacesBlue.Add (l_face);
		sortedFacesBlue.Add (f_face);
		sortedFacesBlue.Add (r_face);
		sortedFacesBlue.Add (d_face);

		sortedFacesOrange.Add (l_face);
		sortedFacesOrange.Add (b_face);
		sortedFacesOrange.Add (d_face);
		sortedFacesOrange.Add (f_face);
		sortedFacesOrange.Add (u_face);
		sortedFacesOrange.Add (r_face);

		sortedFacesGreen.Add (d_face);
		sortedFacesGreen.Add (b_face);
		sortedFacesGreen.Add (r_face);
		sortedFacesGreen.Add (f_face);
		sortedFacesGreen.Add (l_face);
		sortedFacesGreen.Add (u_face);

		sortedFacesRed.Add (r_face);
		sortedFacesRed.Add (b_face);
		sortedFacesRed.Add (u_face);
		sortedFacesRed.Add (f_face);
		sortedFacesRed.Add (d_face);
		sortedFacesRed.Add (l_face);

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

		sortedBounds = allFacesBounds;
		SortBounds ();

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

		whiteSideUp = cube00.transform.rotation;

		original00 = cube00.GetComponent<Renderer> ().materials;
		original01 = cube01.GetComponent<Renderer> ().materials;
		original02 = cube02.GetComponent<Renderer> ().materials;
		original03 = cube03.GetComponent<Renderer> ().materials;
		original04 = cube04.GetComponent<Renderer> ().materials;
		original05 = cube05.GetComponent<Renderer> ().materials;
		original06 = cube06.GetComponent<Renderer> ().materials;
		original07 = cube07.GetComponent<Renderer> ().materials;
		original08 = cube08.GetComponent<Renderer> ().materials;
		original09 = cube09.GetComponent<Renderer> ().materials;
		original10 = cube10.GetComponent<Renderer> ().materials;
		original11 = cube11.GetComponent<Renderer> ().materials;
		original12 = cube12.GetComponent<Renderer> ().materials;
		original13 = cube13.GetComponent<Renderer> ().materials;
		original14 = cube14.GetComponent<Renderer> ().materials;
		original15 = cube15.GetComponent<Renderer> ().materials;
		original16 = cube16.GetComponent<Renderer> ().materials;
		original17 = cube17.GetComponent<Renderer> ().materials;
		original18 = cube18.GetComponent<Renderer> ().materials;
		original19 = cube19.GetComponent<Renderer> ().materials;
		original20 = cube20.GetComponent<Renderer> ().materials;
		original21 = cube21.GetComponent<Renderer> ().materials;
		original22 = cube22.GetComponent<Renderer> ().materials;
		original23 = cube23.GetComponent<Renderer> ().materials;
		original24 = cube24.GetComponent<Renderer> ().materials;
		original25 = cube25.GetComponent<Renderer> ().materials;
		original26 = cube26.GetComponent<Renderer> ().materials;

		edgeCubes.Add (cube02);
		edgeCubes.Add (cube04);
		edgeCubes.Add (cube06);
		edgeCubes.Add (cube08);
		edgeCubes.Add (cube10);
		edgeCubes.Add (cube12);
		edgeCubes.Add (cube16);
		edgeCubes.Add (cube18);
		edgeCubes.Add (cube20);
		edgeCubes.Add (cube22);
		edgeCubes.Add (cube24);
		edgeCubes.Add (cube26);

		cornerCubes.Add (cube00);
		cornerCubes.Add (cube01);
		cornerCubes.Add (cube03);
		cornerCubes.Add (cube07);
		cornerCubes.Add (cube09);
		cornerCubes.Add (cube19);
		cornerCubes.Add (cube21);
		cornerCubes.Add (cube25);

		centerCubes.Add (cube05);
		centerCubes.Add (cube11);
		centerCubes.Add (cube13);
		centerCubes.Add (cube15);
		centerCubes.Add (cube17);
		centerCubes.Add (cube23);

		whiteCross.Add (cube20);
		whiteCross.Add (cube24);
		whiteCross.Add (cube26);
		whiteCross.Add (cube22);

		whiteCorners.Add (cube21);
		whiteCorners.Add (cube00);
		whiteCorners.Add (cube25);
		whiteCorners.Add (cube19);

		middleLayer.Add (cube12);
		middleLayer.Add (cube18);
		middleLayer.Add (cube16);
		middleLayer.Add (cube10);

		yellowCross.Add (cube04);
		yellowCross.Add (cube08);
		yellowCross.Add (cube02);
		yellowCross.Add (cube06);

		yellowCorners.Add (cube07);
		yellowCorners.Add (cube01);
		yellowCorners.Add (cube09);
		yellowCorners.Add (cube03);

		topLayer.Add (cube04);
		topLayer.Add (cube08);
		topLayer.Add (cube02);
		topLayer.Add (cube06);
		topLayer.Add (cube07);
		topLayer.Add (cube01);
		topLayer.Add (cube09);
		topLayer.Add (cube03);
	}

	/// <summary>
	/// Go back a step in the tutorial.
	/// </summary>
	public void Previous () {
		if (!solver.GetComponent<TutorialSolver> ().IsSolving ()) {
			if (tutorialStep > 0) {
				tutorialStep -= 1;
			}
			stepDone = false;
		}
	}

	/// <summary>
	/// Go forward a step in the tutorial.
	/// </summary>
	public void Next () {
		if (!solver.GetComponent<TutorialSolver> ().IsSolving ()) {
			tutorialStep += 1;
			stepDone = false;
		}
	}

	/// <summary>
	/// Update this instance. Flips the Rubik's Cube if the user presses the spacebar.
	/// Configures the rotation int variables according to the camera view. Displays the summary
	/// of a phase upon completion.
	/// </summary>
	void Update () {
		doUpSwap = Input.GetButtonDown ("Jump");
		if (doUpSwap) {
			StartCoroutine (SwapTopView ());
		}
		faceConfig = cam.GetComponent<TutorialCamera> ().GetFaceConfig ();
		StartCoroutine (FaceSwap ());
		ConfigureMoves ();
		ShowSideConfigs ();
		Summary ();
		StartCoroutine (Steps ());
	}

	/// <summary>
	/// Starts a coroutine to reconfigure the sorted lists of sides.
	/// </summary>
	IEnumerator FaceSwap () {
		while (solver.GetComponent<TutorialSolver> ().IsTurning ()) {
			yield return null;
		}
		//UserFace (y);
		yield return null;
	}

	/// <summary>
	/// Resorts the List variables containing the pieces of each side, according to the camera view.
	/// </summary>
	/// <param name="y">The y coordinate of the camera.</param>
	void UserFace (float y) {
		if ((y >= 45f) && (y < 135f)) {
			faceConfig = (sunnySideUp > 0) ? 0 : 1;
			if (!allFaces.Equals (sortedFacesBlue)) {
				allFaces = sortedFacesBlue;
				SortBounds ();
			}
		} else if ((y >= -135f) && (y < -45f)) {
			faceConfig = (sunnySideUp > 0) ? 4 : 5;
			if (!allFaces.Equals (sortedFacesGreen)) {
				allFaces = sortedFacesGreen;
				SortBounds ();
			}
		} else if ((y >= -45f) && (y < 45f)) {
			if (sunnySideUp > 0) {
				faceConfig = 2;
				if (!allFaces.Equals (sortedFacesOrange)) {
					allFaces = sortedFacesOrange;
					SortBounds ();
				}
			} else if (sunnySideUp < 0) {
				faceConfig = 3;
				if (!allFaces.Equals (sortedFacesRed)) {
					allFaces = sortedFacesRed;
					SortBounds ();
				}
			}
		} else {
			if (sunnySideUp > 0) {
				faceConfig = 6;
				if (!allFaces.Equals (sortedFacesRed)) {
					allFaces = sortedFacesRed;
					SortBounds ();
				}
			} else if (sunnySideUp < 0) {
				faceConfig = 7;
				if (!allFaces.Equals (sortedFacesOrange)) {
					allFaces = sortedFacesOrange;
					SortBounds ();
				}
			}
		}
	}

	/// <summary>
	/// Sorts the bounds according to the camera view. The property |sortedBounds| contains the Bounds
	/// of sides in the following order: F D R U L B
	/// </summary>
	void SortBounds () {

		sortedBounds.Clear ();
		foreach (GameObject face in allFaces) {
			Bounds faceBound = face.GetComponent<MeshCollider> ().bounds;
			sortedBounds.Add (faceBound);
		}
	}

	/// <summary>
	/// Configures the moves according to the camera view.
	/// </summary>
	void ConfigureMoves () {
		switch (faceConfig) {
		case 0:
			f  = 6;
			fi = 7;
			d  = 11;
			di = 10;
			r  = 8;
			ri = 9;
			u  = 0;
			ui = 1;
			l  = 4;
			li = 5;
			b  = 2;
			bi = 3;
			break;
		case 1:
			f  = 6;
			fi = 7;
			d  = 0;
			di = 1;
			r  = 4;
			ri = 5;
			u  = 11;
			ui = 10;
			l  = 8;
			li = 9;
			b  = 2;
			bi = 3;
			break;
		case 2:
			f  = 8;
			fi = 9;
			d  = 11;
			di = 10;
			r  = 2;
			ri = 3;
			u  = 0;
			ui = 1;
			l  = 6;
			li = 7;
			b  = 4;
			bi = 5;
			break;
		case 3:
			f  = 4;
			fi = 5;
			d  = 0;
			di = 1;
			r  = 2;
			ri = 3;
			u  = 11;
			ui = 10;
			l  = 6;
			li = 7;
			b  = 8;
			bi = 9;
			break;
		case 4:
			f  = 2;
			fi = 3;
			d  = 11;
			di = 10;
			r  = 4;
			ri = 5;
			u  = 0;
			ui = 1;
			l  = 8;
			li = 9;
			b  = 6;
			bi = 7;
			break;
		case 5:
			f  = 2;
			fi = 3;
			d  = 0;
			di = 1;
			r  = 8;
			ri = 9;
			u  = 11;
			ui = 10;
			l  = 4;
			li = 5;
			b  = 6;
			bi = 7;
			break;
		case 6:
			f  = 4;
			fi = 5;
			d  = 11;
			di = 10;
			r  = 6;
			ri = 7;
			u  = 0;
			ui = 1;
			l  = 2;
			li = 3;
			b  = 8;
			bi = 9;
			break;
		case 7:
			f  = 8;
			fi = 9;
			d  = 0;
			di = 1;
			r  = 6;
			ri = 7;
			u  = 11;
			ui = 10;
			l  = 2;
			li = 3;
			b  = 4;
			bi = 5;
			break;
		}
	}

	/// <summary>
	/// Displays a summary at the end of each phase.
	/// </summary>
	void Summary () {
		switch (tutorialStep) {
		case 54:
			foreach (GameObject cube in allCubes) {
				cube.SetActive (false);
			}
			summaryDone.SetActive (true);
			summaryDoneText.gameObject.SetActive (true);

			summary1.SetActive (true);
			summary1Text.gameObject.SetActive (true);

			summaryDone.GetComponent<SpriteRenderer> ().sprite = state1done;

			summary1.GetComponent<SpriteRenderer> ().sprite = state1_1b;
			summary1Text.text = "[R' U F' U']";
			break;
		case 93:
			foreach (GameObject cube in allCubes) {
				cube.SetActive (false);
			}
			summaryDone.SetActive (true);
			summaryDoneText.gameObject.SetActive (true);

			summary1.SetActive (true);
			summary1Text.gameObject.SetActive (true);

			summary2.SetActive (true);
			summary2Text.gameObject.SetActive (true);

			summary3.SetActive (true);
			summary3Text.gameObject.SetActive (true);

			summary4.SetActive (true);
			summary4Text.gameObject.SetActive (true);

			summaryDone.GetComponent<SpriteRenderer> ().sprite = state2done;

			summary1.GetComponent<SpriteRenderer> ().sprite = state2_1a;
			summary1Text.text = "[R' D' R D]";

			summary2.GetComponent<SpriteRenderer> ().sprite = state2_1b;
			summary2Text.text = "[R' D' R D]";

			summary3.GetComponent<SpriteRenderer> ().sprite = state2_1c;
			summary3Text.text = "[R' D' R D]";

			summary4.GetComponent<SpriteRenderer> ().sprite = state2_2a;
			summary4Text.text = "[R' D' R]";

			break;
		case 126:
			foreach (GameObject cube in allCubes) {
				cube.SetActive (false);
			}
			summaryDone.SetActive (true);
			summaryDoneText.gameObject.SetActive (true);

			summary1.SetActive (true);
			summary1Text.gameObject.SetActive (true);

			summary2.SetActive (true);
			summary2Text.gameObject.SetActive (true);

			summaryDone.GetComponent<SpriteRenderer> ().sprite = state3done;

			summary1.GetComponent<SpriteRenderer> ().sprite = state3_1a;
			summary1Text.text = "[U R U' R' U' F' U F]";

			summary2.GetComponent<SpriteRenderer> ().sprite = state3_2a;
			summary2Text.text = "[U' L' U L U F U' F']";
			break;
		
		case 150:
			foreach (GameObject cube in allCubes) {
				cube.SetActive (false);
			}
			summaryDone.SetActive (true);
			summaryDoneText.gameObject.SetActive (true);

			summary1.SetActive (true);
			summary1Text.gameObject.SetActive (true);

			summary2.SetActive (true);
			summary2Text.gameObject.SetActive (true);

			summary3.SetActive (true);
			summary3Text.gameObject.SetActive (true);

			summaryDone.GetComponent<SpriteRenderer> ().sprite = state4done;

			summary1.GetComponent<SpriteRenderer> ().sprite = state4_1a;
			summary1Text.text = "[F U R U' R' F']";

			summary2.GetComponent<SpriteRenderer> ().sprite = state4_2a;
			summary2Text.text = "[F U R U' R' F']";

			summary3.GetComponent<SpriteRenderer> ().sprite = state4_3a;
			summary3Text.text = "[F R U R' U' F']";
			break;

		case 191:
			foreach (GameObject cube in allCubes) {
				cube.SetActive (false);
			}
			summaryDone.SetActive (true);
			summaryDoneText.gameObject.SetActive (true);

			summary1.SetActive (true);
			summary1Text.gameObject.SetActive (true);

			summary2.SetActive (true);
			summary2Text.gameObject.SetActive (true);

			summary3.SetActive (true);
			summary3Text.gameObject.SetActive (true);

			summaryDone.GetComponent<SpriteRenderer> ().sprite = state5done;

			summary1.GetComponent<SpriteRenderer> ().sprite = state5_1a;
			summary1Text.text = "[R U R' U R U U R']";

			summary2.GetComponent<SpriteRenderer> ().sprite = state5_2a;
			summary2Text.text = "[R U R' U R U U R']";

			summary3.GetComponent<SpriteRenderer> ().sprite = state5_3a;
			summary3Text.text = "[R U R' U R U U R']";
			break;

		case 227:
			foreach (GameObject cube in allCubes) {
				cube.SetActive (false);
			}
			summaryDone.SetActive (true);
			summaryDoneText.gameObject.SetActive (true);

			summary1.SetActive (true);
			summary1Text.gameObject.SetActive (true);

			summary2.SetActive (true);
			summary2Text.gameObject.SetActive (true);

			summary3.SetActive (true);
			summary3Text.gameObject.SetActive (true);

			summary4.SetActive (true);
			summary4Text.gameObject.SetActive (true);

			summaryDone.GetComponent<SpriteRenderer> ().sprite = state6done;

			summary1.GetComponent<SpriteRenderer> ().sprite = state6_1a;
			summary1Text.text = "[R' F R' B B R F' R' B B R R U']";

			summary2.GetComponent<SpriteRenderer> ().sprite = state6_2a;
			summary2Text.text= "[R' F R' B B R F' R' B B R R U']";

			summary3.GetComponent<SpriteRenderer> ().sprite = state6_3a;
			summary3Text.text = "[F F U L R' F F L' R U F F]";

			summary4.GetComponent<SpriteRenderer> ().sprite = state6_3b;
			summary4Text.text = "[F F U' L R' F F L' R U' F F]";
			break;

		default:
			if (!cube00.gameObject.activeInHierarchy) {
				foreach (GameObject cube in allCubes) {
					cube.SetActive (true);
				}
			}
			summaryDone.SetActive (false);
			summaryDoneText.gameObject.SetActive (false);

			summary1.SetActive (false);
			summary1Text.gameObject.SetActive (false);

			summary2.SetActive (false);
			summary2Text.gameObject.SetActive (false);

			summary3.SetActive (false);
			summary3Text.gameObject.SetActive (false);

			summary4.SetActive (false);
			summary4Text.gameObject.SetActive (false);
			break;
		}
	}

	/// <summary>
	/// Displays/Hides side configurations.
	/// </summary>
	void ShowSideConfigs () {
		int phase = 0;

		// The following if control structure handles all tutorial steps where side configurations should be hidden.
		if ((tutorialStep < 36) || ((tutorialStep > 52) && (tutorialStep < 63)) || ((tutorialStep > 92) && (tutorialStep < 103))
			|| ((tutorialStep > 121) && (tutorialStep < 132)) || ((tutorialStep > 148) && (tutorialStep < 154))
			|| ((tutorialStep > 189) && (tutorialStep < 197)) || (tutorialStep > 225)) {
			phase = 0;
		}

		// The following if control structure handles all tutorial steps where side configurations should be shown.
		if ((tutorialStep >= 36) && (tutorialStep <= 52)) {
			phase = 1;
		} else if ((tutorialStep >= 63) && (tutorialStep <= 92)) {
			phase = 2;
		} else if ((tutorialStep >= 103) && (tutorialStep <= 121)) {
			phase = 3;
		} else if ((tutorialStep >= 132) && (tutorialStep <= 148)) {
			phase = 4;
		} else if ((tutorialStep >= 154) && (tutorialStep <= 189)) {
			phase = 5;
		} else if ((tutorialStep >= 197) && (tutorialStep <= 225)) {
			phase = 6;
		}

		// The following switch statement changes the sprites of the side configurations depending on the phase.
		switch (phase) {
		case 0:
			config1of3.gameObject.SetActive (false);
			config2of3.gameObject.SetActive (false);
			config3of3.gameObject.SetActive (false);
			break;
		case 1:
			config1of3.gameObject.SetActive (true);
			config2of3.gameObject.SetActive (true);
			config3of3.gameObject.SetActive (false);
			config1of3.GetComponent<SpriteRenderer> ().sprite = state1_1a;
			config2of3.GetComponent<SpriteRenderer> ().sprite = state1_1b;
			break;
		case 2:
			if ((tutorialStep == 91) || (tutorialStep == 92)) {
				config1of3.gameObject.SetActive (true);
				config2of3.gameObject.SetActive (false);
				config3of3.gameObject.SetActive (false);
				config1of3.GetComponent<SpriteRenderer> ().sprite = state2_2a;
			} else {
				config1of3.gameObject.SetActive (true);
				config2of3.gameObject.SetActive (true);
				config3of3.gameObject.SetActive (true);
				config1of3.GetComponent<SpriteRenderer> ().sprite = state2_1a;
				config2of3.GetComponent<SpriteRenderer> ().sprite = state2_1b;
				config3of3.GetComponent<SpriteRenderer> ().sprite = state2_1c;
			}
			break;
		case 3:
			config1of3.gameObject.SetActive (true);
			config2of3.gameObject.SetActive (true);
			config3of3.gameObject.SetActive (false);
			config1of3.GetComponent<SpriteRenderer> ().sprite = state3_1a;
			config2of3.GetComponent<SpriteRenderer> ().sprite = state3_2a;
			break;
		case 4:
			config1of3.gameObject.SetActive (true);
			config2of3.gameObject.SetActive (true);
			config3of3.gameObject.SetActive (true);
			config1of3.GetComponent<SpriteRenderer> ().sprite = state4_1a;
			config2of3.GetComponent<SpriteRenderer> ().sprite = state4_2a;
			config3of3.GetComponent<SpriteRenderer> ().sprite = state4_3a;
			break;
		case 5:
			config1of3.gameObject.SetActive (true);
			config2of3.gameObject.SetActive (true);
			config3of3.gameObject.SetActive (true);
			config1of3.GetComponent<SpriteRenderer> ().sprite = state5_1a;
			config2of3.GetComponent<SpriteRenderer> ().sprite = state5_2a;
			config3of3.GetComponent<SpriteRenderer> ().sprite = state5_3a;
			break;
		case 6:
			if (tutorialStep < 214) {
				config1of3.gameObject.SetActive (true);
				config2of3.gameObject.SetActive (true);
				config3of3.gameObject.SetActive (false);
				config1of3.GetComponent<SpriteRenderer> ().sprite = state6_1a;
				config2of3.GetComponent<SpriteRenderer> ().sprite = state6_2a;
			} else {
				config1of3.gameObject.SetActive (true);
				config2of3.gameObject.SetActive (true);
				config3of3.gameObject.SetActive (false);
				config1of3.GetComponent<SpriteRenderer> ().sprite = state6_3a;
				config2of3.GetComponent<SpriteRenderer> ().sprite = state6_3b;
			}
			break;
		}

	}

	/// <summary>
	/// Gets the instruction to display for the current tutorial step.
	/// </summary>
	/// <returns>The instruction.</returns>
	string GetInstruction () {
		switch (tutorialStep) {
		case 0:
			return "Part I: Anatomy of the Rubik's Cube";
		case 1:
			return "Use the arrow keys to change your view of the Rubik's Cube.";
		case 2:
			return "Press the spacebar to flip the Rubik's Cube upside down.";
		case 3:
			return "There are six sides of the Rubik's Cube, which we will assign the names F (Front), B (Back), U (Up), D (Down), R (Right), and L (Left). Each side has a Center piece, which defines the color of that side.";
		case 4:
			return "These are twelve Edge pieces. Edges have two colors.";
		case 5:
			return "These are eight Corner pieces. Corners have three colors.";
		case 6:
			return "Now let's assign the names of each side according to our view of the Rubik's Cube and name each possible move we can make.";
		case 7:
			return "The side directly facing you is assigned the name F. The rest of the sides are assigned names relative to F. Here the Blue side is F, the Green side is B, the White side is U, the Yellow side is D, the Purple side is R, and the Red side is L.";
		case 8:
			return "[F] Front Clockwise";
		case 9:
			return "[F'] Front Counterclockwise";
		case 10:
			return "[B] Back Clockwise";
		case 11:
			return "[B'] Back Counterclockwise";
		case 12:
			return "[U] Up Clockwise";
		case 13:
			return "[U'] Up Counterclockwise";
		case 14:
			return "[D] Down Clockwise";
		case 15:
			return "[D'] Down Counterclockwise";
		case 16:
			return "[R] Right Clockwise";
		case 17:
			return "[R'] Right Counterclockwise";
		case 18:
			return "[L] Left Clockwise";
		case 19:
			return "[L'] Left Counterclockwise";
		case 20:
			return "It's important to stick to one orientation while completing a sequence of turns. After finishing a sequence, take care to mentally reassign names whenever you change your view of the Rubik's Cube.";
		case 21:
			return "Part II: Solving the Rubik's Cube";
		case 22:
			return "An algorithm is given by a series of turns enclosed in brackets.";
		case 23:
			return "For example, this is the algorithm [U U U U]. Any turn done four times in a row will bring you back to the initial configuration.";
		case 24:
			return "The key to learning how to solve a Rubik's Cube is identifying which algorithms to use.";
		case 25:
			return "Now we'll go over which algorithms to use according to the Rubik's Cube's configurations.";
		case 26:
			return "There are six phases to solve a Rubik's Cube.";
		case 27:
			return "Phase 1: White Cross";
		case 28:
			return "There are four Edge pieces that make up the White Cross. We will move each Edge to its correct place one at a time.";
		case 29:
			return "Take note that each of these Edge pieces matches their adjacent Center pieces. Before moving on to the next phase, make sure all the Edges match.";
		case 31:
			return "The first thing we need to do is assign names to the sides. Let's choose the Blue side as F and the White side as U.";
		case 30:
			return "We can choose any Edge to start. Let's go with the Purple-White Edge.";
		case 32:
			return "Now, find the Purple-White Edge and move it so that it is on the D side.";
		case 33:
			return "Next, rotate the D side until the Purple-White Edge is directly under the Purple Center piece.";
		case 34:
			return "Double check to make sure the side name assignment we have still matches our view of the Rubik's Cube. The Purple side is still the R side.";
		case 35:
			return "Perform the algorithm [R R] to get the Purple-White Edge on the U side.";
		case 36:
			return "Your Rubik's Cube should now have either of these configurations. The top configuration has the correct Purple-White Edge position."; 
		case 37:
			return "If we have the bottom configuration, perform the algorithm [R' U F' U'] to obtain the top configuration.";
		case 38:
			return "Now the Purple-White Edge is in its proper place. We must pick the next White Cross Edge with the color that matches the Center piece of the side that is counterclockwise to the Purple side.";
		case 39:
			return "In this case, that will be the Green-White Edge. After that will be Red-White, then Blue-White. We solve the White Cross Edges in a counterclockwise order to avoid accidentally unsolving a previous White Cross Edge.";
		case 40:
			return "Let's find the Green-White Edge and move it to the D side.";
		case 41:
			return "With the Purple side as F, perform the algorithm [R R]. Now the Green-White Edge is in its proper place.";
		case 42:
			return "Next let's find the Red-White Edge.";
		case 43:
			return "Move it down to the D side.";
		case 44:
			return "Rotate the D side until the Red-White Edge is directly under the Red Center piece.";
		case 45:
			return "With the Green side as F, perform the algorithm [R R]. Now we have this configuration again.";
		case 46:
			return "Perform the algorithm [R' U F' U']. Now the Red-White Edge is in its proper place.";
		case 47:
			return "Lastly, let's find the Blue-White Edge.";
		case 48:
			return "It's in the middle row. When we bring it down to the D side, we need to be careful with the White Cross Edges we have already solved.";
		case 49:
			return "Let's assign the Green side as F. We have this configuration.";
		case 50:
			return "Perform the algorithm [R' D D R].";
		case 51:
			return "Now the Blue-White Edge is on the D side, and the other White Cross Edges are still solved.";
		case 52:
			return "Rotate the D side until the Blue-White Edge is directly under the Blue Center piece.";
		case 53:
			return "With the Red side as F, perform the algorithm [R R]. Now the Blue-White Edge is in its proper place. The White Cross is now complete.";
		case 54:
			return "Summary of Phase 1:";
		
		
		case 55:
			return "Phase 2: White Corners";
		case 56:
			return "There are four Corner pieces that make up the White Corners. We will solve each Corner one at a time.";
		case 57:
			return "Take note that each of these Corner pieces matches the colors of all three of its sides.";
		case 58:
			return "We can choose any White Corner to start, but there's a smart way to decide which one is best to save us from doing extra work.";
		case 59:
			return "With the White side as U, look on the bottom row of pieces. Are there any White Corner pieces with their White faces NOT on the Yellow side?";
		case 60:
			return "Yes, one. The Purple-Green-White Corner is on the bottom row, with its White face on the Purple side.";
		case 61:
			return "Look at the other face that is NOT on the Yellow side. It's Green.";
		case 62:
			return "We need to move the Purple-Green-White Corner so that the Green face is on the Green side. Do this by rotating the D side.";
		case 63:
			return "Now the Rubik's Cube matches this configuration.";
		case 64:
			return "Let's assign the Purple side as F. The White side is U.";
		case 65:
			return "Perform the algorithm [D' R' D R].";
		case 66:
			return "Now the Purple-Green-White Corner is in its proper place.";
		case 67:
			return "Look on the bottom row of pieces again. Are there any more White Corner pieces with their White faces NOT on the Yellow side?";
		case 68:
			return "This time, there aren't any. However there are two White Corners on the bottom row with White faces on the Yellow side: the Green-Red-White and Blue-Purple-White Corners.";
		case 69:
			return "We can choose either of these. Let's go with Blue-Purple-White.";
		case 70:
			return "Move the Blue-Purple-White Corner until it is directly below its correct spot.";
		case 71:
			return "Now we have this configuration.";
		case 72:
			return "With the Blue side as F, perform the algorithm [D' R' D D R].";
		case 73:
			return "Now the Blue-Purple-White Corner is on the bottom row, with its White face NOT on the Yellow side. Its Purple face is also NOT on the Yellow side.";
		case 74:
			return "We need to move the Blue-Purple-White Corner by rotating the D side until its Purple face is on the Purple side. In this case, it's already on the Purple side.";
		case 75:
			return "Let's assign the Blue side as F, keeping the White side as U.";
		case 76:
			return "Perform the algorithm [D' R' D R]";
		case 77:
			return "Now the Blue-Purple-White Corner is in its proper place.";
		case 78:
			return "Once more, let's look at the bottom row for any White Corner with its White face NOT on the Yellow side.";
		case 79:
			return "There is one: the Green-Red-White Corner.";
		case 80:
			return "The other face that is not on the Yellow side is its Green face.";
		case 81:
			return "If necessary, rotate the D side so that the Green face is on the Green side. We got lucky again, because it's already there.";
		case 82:
			return "Now we have this configuration.";
		case 83:
			return "Let's assign the Green side as F, keeping the White side as U.";
		case 84:
			return "Perform the algorithm [R' D' R].";
		case 85:
			return "Now the Green-Red-White Corner is in its proper place.";
		case 86:
			return "Let's look on the bottom row for the last White Corner: the Red-Blue-White Corner.";
		case 87:
			return "Its White and Blue faces are NOT on the Yellow side.";
		case 88:
			return "Move the Red-Blue-White Corner by rotating D so that its Blue face is on the Blue side.";
		case 89:
			return "With the Red side as F, perform the algorithm [D' R' D R]";
		case 90:
			return "Now all of our White Corners are in the correct places.";
		case 91:
			return "Sometimes when initially looking for a White Corner to solve, we may find it on the top row rather than the bottom row, with its White face on the wrong side.";
		case 92:
			return "To get it down to the D side, perform the algorithm [R' D' R].";
		case 93:
			return "Summary of Phase 2:";
		
		case 94:
			return "Phase 3: Middle Layer";
		case 95:
			return "There are four Edge pieces that make up the Middle Layer. We will solve each Edge one at a time.";
		case 96:
			return "Take note that each of these Edge pieces matches the colors of both of its adjacent Center pieces.";
		case 97:
			return "We can choose any Edge to start, but there's a smart way to decide which one is best to save us from doing extra work.";
		case 98:
			return "For this phase, let's assign the Yellow side as U.";
		case 99:
			return "Look on the U side. Check out the Edge pieces. Are any of them one of the Middle Layer Edges?";
		case 100:
			return "Here, there are two Middle Layer Edges already on the U side: the Blue-Purple Edge and the Purple-Green Edge.";
		case 101:
			return "Pick any of the Middle Layer Edges already on the U side. Let's go with the Blue-Purple Edge.";
		case 102:
			return "First we need to get Blue-Purple Edge ready. Notice that the Blue color of this piece is on the U side. The Purple color is on the Blue side.";
		case 103:
			return "We need to move the Blue-Purple Edge so that the color of the piece that is NOT on the U side (in this case Purple) aligns with its matching Center piece, like either of these configurations.";
		case 104:
			return "Notice now there is a vertical stack of three pieces with one matching color (Purple here). This is the condition we must meet before applying our algorithm.";
		case 105:
			return "Reorient the Rubik's Cube so that the Purple side is now F. The Blue side is R and the Green side is L.";
		case 106:
			return "Since the Blue side is R, we need to apply the following algorithm to bring the Blue-Purple Edge to the RIGHT: [U, R, U', R', U', F', U, F]";
		case 107:
			return "Now the Blue-Purple Edge is in the right place. Let's look again on the U side for any other Middle Layer Edges.";
		case 108:
			return "We have two options: the Purple-Green Edge and the Red-Blue Edge.";
		case 109:
			return "Let's go with the Red-Blue Edge.";
		case 110:
			return "Again we align the Red-Blue Edge to make the vertical stack of three matching pieces (Blue here).";
		case 111:
			return "Reorient the Rubik's Cube so that the Blue side is now F. The Red side is R and the Purple side is L.";
		case 112:
			return "The Red side is R. Like last time, this Middle-Layer Edge piece needs to move to the RIGHT. We apply the same algorithm: [U, R, U', R', U', F', U, F]";
		case 113:
			return "Now the Red-Blue side is in the right place. Let's look on the U side for any other Middle Layer Edges.";
		case 114:
			return "This time we only have one option: the Purple-Green Edge.";
		case 115:
			return "Again we align the Purple-Green Edge to make the vertical stack of three matching pieces (Green here).";
		case 116:
			return "Reorient the Rubik's Cube so that the Green side is F. The Purple side is R and the Red side is L.";
		case 117:
			return "The Purple-Green Edge needs to move to the Right, like the two previous Middle Layer Edges. Again we apply the same algoritm: [U, R, U', R', U', F', U, F]";
		case 118:
			return "Now the Purple-Green Edge is in the right place. Let's look on the U side for the last Middle Layer Edge.";
		case 119:
			return "Luckily, the Green-Red Edge is on the U side.";
		case 120:
			return "Let's bring it to the proper starting position to make a vertical stack of three Green pieces.";
		case 121:
			return "Let's set the Green side as F. Unlike the three previous Middle Layer Edges, the Green-Red Edge needs to move to the LEFT. In this case we use a different algorithm: [U', L', U, L, U, F, U', F']";
		case 122:
			return "Now all our Middle Layer Edges are in their correct spots.";
		case 123:
			return "Sometimes, we can get stuck with no Middle Layer Edges left on the U side. In this case, we can apply either of the two algorithms from before, using any side as F.";
		case 124:
			return "Let's choose the Blue side as F and apply the first algorithm we learned in this phase: [U, R, U', R', U', F', U, F]";
		case 125:
			return "Now the Blue-Purple Edge is on the U side.";
		case 126:
			return "Summary of Phase 3:";
		case 127:
			return "Phase 4: Yellow Cross";
		case 128:
			return "This phase and the ones after it are different than the previous phases.";
		case 129:
			return "There are four Edge pieces that make up the Yellow Cross.";
		case 130:
			return "Unlike the White Cross Edges, the Yellow Cross Edge pieces do not have to match both of their adjacent Center pieces. They only have to match the Yellow Center piece.";
		case 131:
			return "Instead of solving this phase one piece at a time, we will perform algorithms based on the configuration of all the pieces.";
		case 132:
			return "These are the three configurations we need to watch out for. Keep in mind these configurations only show the Yellow Cross Edges. There may be Yellow Corners on the Yellow side as well, but we won't worry about those yet.";
		case 133:
			return "Let's look at an example of the top configuration.";
		case 134:
			return "For this pattern, we can pick any side to be the F side. Let's go with the Blue side.";
		case 135:
			return "Perform the algorithm [F U R U' R' F'].";
		case 136:
			return "Now the current pattern looks very similar to the third configuration.";
		case 137:
			return "It is important that the line of Yellow pieces is horizontal. If it's vertical with respect to our F side, we have two options: change the F side OR rotate the U side.";
		case 138:
			return "Let's change the F side to the Purple side. Now we have exactly the third configuration.";
		case 139:
			return "Perfrom the algorithm [F R U R' U' F'].";
		case 140:
			return "Now we have the Yellow Cross.";
		case 141:
			return "Let's look at another example.";
		case 142:
			return "Here we have a pattern that looks very similar to the second configuration.";
		case 143:
			return "Like the third configuration, it is important that we match this pattern exactly to the configuration shown.";
		case 144:
			return "We can either change the F side OR rotate the U side.";
		case 145:
			return "Let's rotate the U side so we have the correct configuration using Blue as the F side.";
		case 146:
			return "Now we have the second configuration exactly.";
		case 147:
			return "This pattern calls for the same algorithm as the first configuration.";
		case 148:
			return "Perform the algorithm [F U R U' R' F'].";
		case 149:
			return "Now we have the Yellow Cross.";
		case 150:
			return "Phase 4 Summary";
		

		case 151:
			return "Phase 5: Yellow Corners";
		case 152:
			return "There are four Corner pieces that make up the Yellow Corners.";
		case 153:
			return "Unlike the White Corners, the Yellow Corner pieces do not have to match all of their adjacent side colors. They only have to match the Yellow side.";
		case 154:
			return "There is only one algorithm for this phase. The important part is making sure the pattern on the Yellow side has exactly the same orientation as any one of these three configurations.";
		case 155:
			return "The first configuration is used when there are NO Yellow Corners with Yellow on the U side.";
		case 156:
			return "Let's look at an example.";
		case 157:
			return "Change the F side until we get an exact match of the first configuration.";
		case 158:
			return "There is only one possible choice for an F side that will give us the correct orientation: the Red side.";
		case 159:
			return "With Red as F, we have a Yellow Corner with its Yellow color facing the left.";
		case 160:
			return "Perform the algorithm [R U R' U R U U R'].";
		case 161:
			return "Now we have something that almost looks like the second configuration. We have exactly one Yellow Corner with its Yellow color on the U side.";
		case 162:
			return "We must orient the Rubik's Cube so that it matches the second configuration exactly. We can do this by changing our F side to Green.";
		case 163:
			return "Now we have exactly the second configuration. The second configuration applies when there is exactly ONE Yellow Corner with Yellow on the U side.";
		case 164:
			return "Perform the algorithm [R U R' U R U U R'].";
		case 165:
			return "Now we have all of the Yellow Corners on the U side.";
		case 166:
			return "Before moving on, we must review something important about the third configuration.";
		case 167:
			return "Even though the bottom configuration shown appears only to have five Yellow pieces on the U side, there must be at least TWO Yellow Corners with their Yellow color on the U side for this configuration to apply.";
		case 168:
			return "These are not shown in the bottom configuration because ANY two of the four Yellow Corners can have their Yellow color on the U side.";
		case 169:
			return "If there are three Yellow Corners with their Yellow Corners on the U side, this configuration should be used.";
		case 170:
			return "Let's look at an example.";
		case 171:
			return "We need to find the correct F side to match the bottom configuration. There should be a Yellow Corner with its Yellow color on the F side.";
		case 172:
			return "The Green side is the correct F side here.";
		case 173:
			return "Perform the algorithm [R U R' U R U U R'].";
		case 174:
			return "We are back to the same pattern as the middle configuration.";
		case 175:
			return "Let's find the correct F side to match the middle configuration.";
		case 176:
			return "The Red side is the correct F side here.";
		case 177:
			return "Perform the algorithm [R U R' U R U U R'].";
		case 178:
			return "Once again we have the same pattern as the middle configuration. Let's repeat the last step we did.";
		case 179:
			return "The Purple side is the correct F side here.";
		case 180:
			return "Perform the algorithm [R U R' U R U U R'].";
		case 181:
			return "Now we have all of the Yellow Corners on the U side.";
		case 182:
			return "Let's look at one more example using the bottom configuration.";
		case 183:
			return "Let's find the correct F side to match the bottom configuration.";
		case 184:
			return "The Red side is the correct F side here.";
		case 185:
			return "Perform the algorithm [R U R' U R U U R'].";
		case 186:
			return "Now we have the middle configuration, and we have the correct F side already: the Red side.";
		case 187:
			return "Perform the algorithm [R U R' U R U U R'].";
		case 188:
			return "We got the middle configuration again. Let's move to the correct F side, which is the Purple side.";
		case 189:
			return "Perform the algorithm [R U R' U R U U R'].";
		case 190:
			return "Now we have all of the Yellow Corners on the U side.";
		case 191:
			return "Phase 5 Summary";
		case 192:
			return "Phase 6: Top Layer";
		case 193:
			return "There are four Edge pieces and four Corner pieces that make up the Top Layer. We will start with the Corner pieces.";
		case 194:
			return "Take a look at the Yellow Corners of the U side. Rotate the U side until at least TWO Corners are in the right spots, meaning they match their colors on all three of their sides.";
		case 195:
			return "The two correct Top Layer Corners can be next to each other, like in this case.";
		case 196:
			return "They can also be across from each other diagonally, which we will see later on.";
		case 197:
			return "The top configuration shown on the right applies when the two correct Top Layer Corners are next to each other. The bottom configuration applies when the Corners are across from each other diagonally.";
		case 198:
			return "Let's look at this example of the top configuration.";
		case 199:
			return "Here the two correct Top Layer Corners are the Green-Red-Yellow and the Purple-Green-Yellow Corners.";
		case 200:
			return "We need to choose the F side so that the two correct Top Layer Edges are on the B side.";
		case 201:
			return "The Blue side is the correct F side here.";
		case 202:
			return "Perform the algorithm [R' F R' B B R F' R' B B R R U'].";
		case 203:
			return "In this case, we've already solved the Rubik's Cube without having to deal with the Top Layer Edges! Sometimes we won't be so lucky.";
		case 204:
			return "Let's look at an example of the bottom configuration. Here the Red-Blue-Yellow and the Purple-Green-Yellow Corners are the two correct Top Layer Corners.";
		case 205:
			return "The two correct Top Layer Corners are already on the right sides. We need to perform the same algorithm as before in order to obtain the top configuration.";
		case 206:
			return "It does not matter which side we choose as F whenever we have the bottom configuration. Let's go with the Blue side as F.";
		case 207:
			return "Perform the algorithm [R' F R' B B R F' R' B B R R U'].";
		case 208:
			return "Now we almost have the top configuration. The two correct Top Layer Corners are the Red-Blue-Yellow and the Green-Red-Yellow Corners. They are next to each other, not across from each other.";
		case 209:
			return "We need to rotate the U side until the two correct Top Layer Corners are on the correct side.";
		case 210:
			return "Let's find the correct F side to apply for the first configuration.";
		case 211:
			return "The Purple side is the correct F side here, since the two adjacent correct Top Layer Corners are on the B side.";
		case 212:
			return "Perform the algorithm [R' F R' B B R F' R' B B R R U'].";
		case 213:
			return "Once again we have solved the Rubik's Cube without having to deal with the Top Layer Edges.";
		case 214:
			return "Let's practice for situations where we will have to deal with the Top Layer Edges.";
		case 215:
			return "Take a look at the Yellow Edges of the U side. There are two possibilities. Either all the Top Layer Edges will be mismatched, or there will be a single Top Layer Edge in the correct spot.";
		case 216:
			return "Here, all the Top Layer Edges are mismatched.";
		case 217:
			return "Pick any Top Layer Edge. Let's go with the Purple-Yellow Edge.";
		case 218:
			return "Since this Top Layer Edge is on the Red side, we choose the Red side to be F.";
		case 219:
			return "There are three possible configurations now. Either we need to move the Edge clockwise, counterclockwise, or to the B side.";
		case 220:
			return "We need to move the Purple-Yellow Edge to the Purple side. Notice that this is the B side. When we have this configuration, we apply either of the algorithms for the clockwise or counterclockwise cases.";
		case 221:
			return "Perform the algorithm [F F U L R' F F L' R U F F].";
		case 222:
			return "Now the Green-Yellow Edge is in the correct spot. We need to set a new F side so that this correct Top Layer Edge is on the B side.";
		case 223:
			return "The Blue side is the correct F side here.";
		case 224:
			return "Take a look at the Top Layer Edge on the F side. It is the Purple-Yellow Edge. Since the Purple side is clockwise to the F side, we need to perform the same algorithm as last time.";
		case 225:
			return "Perform the algorithm [F F U L R' F F L' R U F F].";
		case 226:
			return "Now all the Top Layer Edges are in the correct spots, and we've solved the Rubik's Cube!";
		case 227:
			return "Phase 6 Summary";
		}
		return null;
	}

	/// <summary>
	/// Performs the current tutorial step. The main method of this script.
	/// </summary>
	IEnumerator Steps () {
		List<GameObject> faceCubes;
		string instruction = GetInstruction ();
		tutor.text = instruction;
		switch (tutorialStep) {
		#region Part I
		case 2:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
			}
			break;
		case 3:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CenterCubes ();
			}
			break;
		case 4:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				EdgeCubes ();
			}
			break;
		case 5:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CornerCubes ();
			}
			break;
		case 6:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
			}
			break;
		case 7:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				keyboard.SetActive (false);
			}
			break;
		case 8:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				keyboard.SetActive (true);
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_f;
				List<int> moves = new List<int> ();
				moves.Add (f);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (u_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
				break;
		case 9:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_fi;
				List<int> moves = new List<int> ();
				moves.Add (fi);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (u_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 10:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_b;
				List<int> moves = new List<int> ();
				moves.Add (b);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (d_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 11:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_bi;
				List<int> moves = new List<int> ();
				moves.Add (bi);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (d_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 12:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_u;
				List<int> moves = new List<int> ();
				moves.Add (u);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (f_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 13:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_ui;
				List<int> moves = new List<int> ();
				moves.Add (ui);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (f_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 14:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_d;
				List<int> moves = new List<int> ();
				moves.Add (d);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (b_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 15:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_di;
				List<int> moves = new List<int> ();
				moves.Add (di);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (b_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 16:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_r;
				List<int> moves = new List<int> ();
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (l_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 17:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_ri;
				List<int> moves = new List<int> ();
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (l_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 18:
			if (!stepDone) {
				stepDone = true;
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_l;
				List<int> moves = new List<int> ();
				moves.Add (l);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (r_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}

			break;
		case 19:
			if (!stepDone) {
				stepDone = true;
				keyboard.SetActive (true);
				keyboard.GetComponent<SpriteRenderer> ().sprite = key_li;
				List<int> moves = new List<int> ();
				moves.Add (li);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			AnyColors ();
			faceCubes = GetFaceCubes (r_bounds);
			foreach (GameObject cube in faceCubes) {
				ResetColors (cube);
			}
			break;
		case 20:
			if (!stepDone) {
				stepDone = true;
				ResetColors ();
				keyboard.SetActive (false);
			}
			break;
			#endregion

		#region White Cross
		case 21:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
			}
			break;
		case 23:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				List<int> moves = new List<int> ();
				moves.Add (u);
				moves.Add (u);
				moves.Add (u);
				moves.Add (u);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 27:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
			}
			break;
		case 28:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
			}
			break;
		case 29:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
			}
			break;
		case 30:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross1 ();
			}
			break;
		case 31:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross1 ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
			}
			break;
		case 32:
			//special camera view
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross1 ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				List<int> moves = new List<int> ();
				moves.Add (fi);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 33:
			//special camera view
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross2 ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				List<int> moves = new List<int> ();
				moves.Add (d);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 34:
			//special camera view
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross3 ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
			}
			break;
		case 35:
			//special camera view
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				config1of3.SetActive (false);
				config2of3.SetActive (false);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross3 ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 36:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				config1of3.SetActive (true);
				config2of3.SetActive (true);
				config1of3.GetComponent<SpriteRenderer> ().sprite = state1_1a;
				config2of3.GetComponent<SpriteRenderer> ().sprite = state1_1b;
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross4 ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
			}
			break;
		case 37:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				config1of3.SetActive (true);
				config2of3.SetActive (true);
				config1of3.GetComponent<SpriteRenderer> ().sprite = state1_1a;
				config2of3.GetComponent<SpriteRenderer> ().sprite = state1_1b;
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross4 ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				List<int> moves = new List<int> ();
				moves.Add (ri);
				moves.Add (u);
				moves.Add (fi);
				moves.Add (ui);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 38:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				config1of3.SetActive (false);
				config2of3.SetActive (false);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross5 ();
//				List<int> moves = new List<int> ();
//				moves.Add (b);
//				moves.Add (f);
//				moves.Add (f);
//				moves.Add (di);
//				moves.Add (l);
//				moves.Add (l);
//				moves.Add (li);
//				moves.Add (u);
//				moves.Add (bi);
//				moves.Add (ui);
//				moves.Add (li);
//				moves.Add (d);
//				moves.Add (l);
//				moves.Add (f);
//				moves.Add (f);
//				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
//				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
//					yield return null;
//				}
			}
			break;
		case 39:
			//wc6
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				ResetColors (cube22);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross5 ();
			}
			break;
		case 40:
			//wc6
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				ResetColors (cube22);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross5 ();
				List<int> moves = new List<int> ();
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 41:
			//wc7
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				ResetColors (cube22);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross6 ();
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 42:
			//wc8
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
//				AnyColors ();
//				CenterCubes ();
//				ResetColors (cube26);
//				ResetColors (cube22);
//				ResetColors (cube20);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross7 ();
			}
			break;
		case 43:
			//wc8
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				ResetColors (cube22);
				ResetColors (cube20);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross7 ();
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 44:
			//wc9
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				ResetColors (cube22);
				ResetColors (cube20);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross8 ();
				List<int> moves = new List<int> ();
				moves.Add (di);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 45:
			//wc10
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				ResetColors (cube22);
				ResetColors (cube20);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross9 ();
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 46:
			//wc11
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				CenterCubes ();
				ResetColors (cube26);
				ResetColors (cube22);
				ResetColors (cube20);
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross10 ();
				List<int> moves = new List<int> ();
				moves.Add (ri);
				moves.Add (u);
				moves.Add (fi);
				moves.Add (ui);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 47:
			//wc12
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross11 ();
			}
			break;
		case 48:
			//wc12
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross11 ();
			}
			break;
		case 49:
			//wc12
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross11 ();
			}
			break;
		case 50:
			//wc12
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross11 ();
				List<int> moves = new List<int> ();
				moves.Add (ri);
				moves.Add (d);
				moves.Add (d);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 51:
			//wc13
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross12 ();
			}
			break;
		case 52:
			//wc13
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross12 ();
				List<int> moves = new List<int> ();
				moves.Add (di);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 53:
			//wc14
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCross13 ();
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;

		
		#endregion

		#region White Corners
		case 55:
			if (!stepDone) {
				stepDone = true;
				config1of3.SetActive (false);
				ResetCube ();
				AnyColors ();
				WhiteCorners ();
			}
			break;
		case 56:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCorners ();
			}
			break;
		case 57:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				WhiteCorners ();
			}
			break;
		case 58:
			//special camera
			//wc1
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners1 ();
			}
			break;
		case 59:
			//special camera
			//wc1
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners1 ();
			}
			break;
		case 60:
			//special camera
			//wc1
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners1 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
			}
			break;
		case 61:
			//special camera
			//wc1
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners1 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
			}
			break;
		case 62:
			//wc1
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners1 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
			}
			break;
		case 63:
			//wc1
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners1 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
			}
			break;
		case 64:
			//wc1
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners1 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
			}
			break;
		case 65:
			//wc1
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners1 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				List<int> moves = new List<int> ();
				moves.Add (di);
				moves.Add (ri);
				moves.Add (d);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 66:
			//wc2
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners2 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
			}
			break;
		case 67:
			//wc2
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners2 ();
				ResetColors ();
			}
			break;
		case 68:
			//wc2
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners2 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
			}
			break;
		case 69:
			//wc2
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners2 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
			}
			break;
		case 70:
			//wc2
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners2 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				List<int> moves = new List<int> ();
				moves.Add (d);
				moves.Add (d);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 71:
			//wc3
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners3 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
			}
			break;
		case 72:
			//wc3
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners3 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				List<int> moves = new List<int> ();
				moves.Add (di);
				moves.Add (ri);
				moves.Add (d);
				moves.Add (d);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 73:
			//wc4
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners4 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
			}
			break;
		case 74:
			//wc4
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners4 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
			}
			break;
		case 75:
			//wc4
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners4 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
			}
			break;
		case 76:
			//wc4
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners4 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				List<int> moves = new List<int> ();
				moves.Add (di);
				moves.Add (ri);
				moves.Add (d);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 77:
			//wc5
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners5 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
			}
			break;
		case 78:
			//wc5
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners5 ();
			}
			break;
		case 79:
			//wc5
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners5 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
			}
			break;
		case 80:
			//wc5
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners5 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
			}
			break;
		case 81:
			//wc5
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners5 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
			}
			break;
		case 82:
			//wc5
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners5 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
			}
			break;
		case 83:
			//wc5
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners5 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
			}
			break;
		case 84:
			//wc5
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners5 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
				List<int> moves = new List<int> ();
				moves.Add (ri);
				moves.Add (di);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 85:
			//wc6
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners6 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
			}
			break;
		case 86:
			//wc6
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners6 ();
			}
			break;
		case 87:
			//wc6
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners6 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
				ResetColors (cube21);
			}
			break;
		case 88:
			//wc6
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners6 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
				ResetColors (cube21);
				List<int> moves = new List<int> ();
				moves.Add (d);
				moves.Add (d);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 89:
			//wc7
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners7 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube25);
				ResetColors (cube00);
				ResetColors (cube19);
				ResetColors (cube21);
				List<int> moves = new List<int> ();
				moves.Add (di);
				moves.Add (ri);
				moves.Add (d);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 90:
			//wc8
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners8 ();
			}
			break;
		case 91:
			//wc9*
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners9 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube00);
			}
			break;
		case 92:
			//wc9*
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners9 ();
				AnyColors ();
				CenterCubes ();
				WhiteCross ();
				ResetColors (cube00);
				List<int> moves = new List<int> ();
				moves.Add (ri);
				moves.Add (di);
				moves.Add (r);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;


		#endregion

		#region Middle Layer
		case 94:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				MiddleLayer ();
			}
			break;
		case 95:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				MiddleLayer ();
			}
			break;
		case 96:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				MiddleLayer ();
			}
			break;
		case 97:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners10 ();
			}
			break;
		case 98:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners10 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 99:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners10 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 100:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners10 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube16);
				ResetColors (cube18);
			}
			break;
		case 101:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners10 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
			}
			break;
		case 102:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners10 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
			}
			break;
		case 103:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_WhiteCorners10 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				List<int> moves = new List<int> ();
				moves.Add (u);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 104:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer1 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
			}
			break;
		case 105:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer1 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
			}
			break;
		case 106:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer1 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				List<int> moves = new List<int> ();
				moves.Add (u);
				moves.Add (r);
				moves.Add (ui);
				moves.Add (ri);
				moves.Add (ui);
				moves.Add (fi);
				moves.Add (u);
				moves.Add (f);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 107:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer2 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 108:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer2 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube16);
				ResetColors (cube12);
			}
			break;
		case 109:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer2 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube12);
			}
			break;
		case 110:
			//md2
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer2 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube12);
				List<int> moves = new List<int> ();
				moves.Add (u);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 111:
			//md3
			//special camera
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer3 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube12);
			}
			break;
		case 112:
			//md3
			//moves
			//special camera
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer3 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube12);
				List<int> moves = new List<int> ();
				moves.Add (u);
				moves.Add (r);
				moves.Add (ui);
				moves.Add (ri);
				moves.Add (ui);
				moves.Add (fi);
				moves.Add (u);
				moves.Add (f);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 113:
			//md4
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 114:
			//md4
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer4 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube12);
				ResetColors (cube16);
			}
			break;
		case 115:
			//md4
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer4 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube12);
				ResetColors (cube16);
				List<int> moves = new List<int> ();
				moves.Add (ui);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 116:
			//md5
			//special camera
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer5 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube12);
				ResetColors (cube16);
			}
			break;
		case 117:
			//md5
			//moves
			//special camera
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer5 ();
				StartCoroutine (SwapTopView ());
				WhiteCorners ();
				ResetColors (cube18);
				ResetColors (cube12);
				ResetColors (cube16);
				List<int> moves = new List<int> ();
				moves.Add (u);
				moves.Add (r);
				moves.Add (ui);
				moves.Add (ri);
				moves.Add (ui);
				moves.Add (fi);
				moves.Add (u);
				moves.Add (f);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 118:
			//md6
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer6 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 119:
			//md6
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer6 ();
				StartCoroutine (SwapTopView ());
				MiddleLayer ();
			}
			break;
		case 120:
			//md6
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer6 ();
				StartCoroutine (SwapTopView ());
				MiddleLayer ();
				List<int> moves = new List<int> ();
				moves.Add (u);
				moves.Add (u);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 121:
			//md7
			//moves
			//special camera
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer7 ();
				StartCoroutine (SwapTopView ());
				MiddleLayer ();
				List<int> moves = new List<int> ();
				moves.Add (ui);
				moves.Add (li);
				moves.Add (u);
				moves.Add (l);
				moves.Add (u);
				moves.Add (f);
				moves.Add (ui);
				moves.Add (fi);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 122:
			//md8
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer8 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 123:
			//md9
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer9();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 124:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer9 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (u);
				moves.Add (r);
				moves.Add (ui);
				moves.Add (ri);
				moves.Add (ui);
				moves.Add (fi);
				moves.Add (u);
				moves.Add (f);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 125:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer10 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		#endregion

		#region Yellow Cross
		case 127:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 128:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 129:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 130:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 131:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 132:
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 133:
			//ml8
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer8 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 134:
			//ml8
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer8 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 135:
			//ml8
			//moves
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_MiddleLayer8 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (f);
				moves.Add (u);
				moves.Add (r);
				moves.Add (ui);
				moves.Add (ri);
				moves.Add (fi);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 136:
			//yc1
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 137:
			//yc1
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 138:
			//yc1
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 139:
			//yc1
			//moves
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross1 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (f);
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (ui);
				moves.Add (fi);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 140:
			//yc2
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross2 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 141:
			//yc3
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross3 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 142:
			//yc3
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross3 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 143:
			//yc3
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross3 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 144:
			//yc3
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross3 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 145:
			//yc3
			//moves
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross3 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 146:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross3 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (u);
				moves.Add (u);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 147:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 148:
			//yc4
			//moves
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCross ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross4 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (f);
				moves.Add (u);
				moves.Add (r);
				moves.Add (ui);
				moves.Add (ri);
				moves.Add (fi);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 149:
			//yc5
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCross5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 150:
			//SUMMARY
			break;
		#endregion

		#region Yellow Corners
		case 151:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 152:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 153:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 154:
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 155:
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 156:
			//yc1
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 157:
			//yc1
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 158:
			//yc1
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 159:
			//yc1
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 160:
			//yc1
			//moves
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners1 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (u);
				moves.Add (r);
				moves.Add (u);
				moves.Add (u);
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 161:
			//yc2
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners2 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 162:
			//yc2
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners2 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 163:
			//yc2
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners2 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 164:
			//yc2
			//moves
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners2 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (u);
				moves.Add (r);
				moves.Add (u);
				moves.Add (u);
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 165:
			//yc3
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners3 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 166:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 167:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 168:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 169:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 170:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 171:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 172:
			//yc4
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 173:
			//yc4
			//moves
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners4 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (u);
				moves.Add (r);
				moves.Add (u);
				moves.Add (u);
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 174:
			//yc5
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 175:
			//yc5
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 176:
			//yc5
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 177:
			//yc5
			//configs
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners5 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (u);
				moves.Add (r);
				moves.Add (u);
				moves.Add (u);
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 178:
			//yc6
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners6 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 179:
			//yc6
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners6 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 180:
			//yc6
			//configs
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners6 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (u);
				moves.Add (r);
				moves.Add (u);
				moves.Add (u);
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 181:
			//yc7
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
//				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners7 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 182:
			//yc8 new
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners8 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 183:
			//yc8
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners8 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 184:
			//yc8
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners8 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 185:
			//yc8
			//configs
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners8 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (u);
				moves.Add (r);
				moves.Add (u);
				moves.Add (u);
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 186:
			//yc9
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners9 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 187:
			//yc9
			//configs
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners9 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (u);
				moves.Add (r);
				moves.Add (u);
				moves.Add (u);
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 188:
			//yc10
			//configs
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners10 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 189:
			//yc10
			//configs
			//moves
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners10 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (r);
				moves.Add (u);
				moves.Add (ri);
				moves.Add (u);
				moves.Add (r);
				moves.Add (u);
				moves.Add (u);
				moves.Add (ri);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 190:
			//yc11
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				AnyColors ();
				YellowCorners ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners11 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 191:
			//summary
			break;

		#endregion

		#region Top Layer
		case 192:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 193:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners11 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 194:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_YellowCorners11 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (u);
				moves.Add (u);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 195:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 196:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 197:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 198:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 199:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 200:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 201:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer1 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 202:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer1 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (ri);
				moves.Add (f);
				moves.Add (ri);
				moves.Add (b);
				moves.Add (b);
				moves.Add (r);
				moves.Add (fi);
				moves.Add (ri);
				moves.Add (b);
				moves.Add (b);
				moves.Add (r);
				moves.Add (r);
				moves.Add (ui);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 203:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 204:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer2 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 205:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer2 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 206:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer2 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 207:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer2 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (ri);
				moves.Add (f);
				moves.Add (ri);
				moves.Add (b);
				moves.Add (b);
				moves.Add (r);
				moves.Add (fi);
				moves.Add (ri);
				moves.Add (b);
				moves.Add (b);
				moves.Add (r);
				moves.Add (r);
				moves.Add (ui);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 208:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer3 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 209:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer3 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (ui);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 210:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 211:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer4 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 212:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer4 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (ri);
				moves.Add (f);
				moves.Add (ri);
				moves.Add (b);
				moves.Add (b);
				moves.Add (r);
				moves.Add (fi);
				moves.Add (ri);
				moves.Add (b);
				moves.Add (b);
				moves.Add (r);
				moves.Add (r);
				moves.Add (ui);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 213:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 214:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 215:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 216:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 217:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 218:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 219:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 220:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer5 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 221:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer5 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (f);
				moves.Add (f);
				moves.Add (u);
				moves.Add (l);
				moves.Add (ri);
				moves.Add (f);
				moves.Add (f);
				moves.Add (li);
				moves.Add (r);
				moves.Add (u);
				moves.Add (f);
				moves.Add (f);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 222:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer6 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 223:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer6 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 224:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer6 ();
				StartCoroutine (SwapTopView ());
			}
			break;
		case 225:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				solver.GetComponent<TutorialScrambler> ().Tutor_TopLayer6 ();
				StartCoroutine (SwapTopView ());
				List<int> moves = new List<int> ();
				moves.Add (f);
				moves.Add (f);
				moves.Add (u);
				moves.Add (l);
				moves.Add (ri);
				moves.Add (f);
				moves.Add (f);
				moves.Add (li);
				moves.Add (r);
				moves.Add (u);
				moves.Add (f);
				moves.Add (f);
				StartCoroutine (solver.GetComponent<TutorialSolver> ().PerformAlgorithm (moves));
				while (solver.GetComponent<TutorialSolver> ().IsSolving ()) {
					yield return null;
				}
			}
			break;
		case 226:
			if (!stepDone) {
				stepDone = true;
				ResetCube ();
				ResetColors ();
				StartCoroutine (SwapTopView ());
			}
			break;
		#endregion
		}
		yield return null;
	}


	#region Groups

	/// <summary>
	/// Shows only the Front side pieces.
	/// </summary>
	void Front () {
		AnyColors ();
		List<GameObject> faceCubes = GetFaceCubes (sortedBounds [0]);
		foreach (GameObject cube in faceCubes) {
			ResetColors (cube);
		}
	}

	/// <summary>
	/// Shows only the Down side pieces.
	/// </summary>
	void Down () {
		AnyColors ();
		List<GameObject> faceCubes = GetFaceCubes (sortedBounds [1]);
		foreach (GameObject cube in faceCubes) {
			ResetColors (cube);
		}
	}

	/// <summary>
	/// Shows only the Right side pieces.
	/// </summary>
	void Right () {
		AnyColors ();
		List<GameObject> faceCubes = GetFaceCubes (sortedBounds [2]);
		foreach (GameObject cube in faceCubes) {
			ResetColors (cube);
		}
	}

	/// <summary>
	/// Shows only the Up side pieces.
	/// </summary>
	void Up () {
		AnyColors ();
		List<GameObject> faceCubes = GetFaceCubes (sortedBounds [3]);
		foreach (GameObject cube in faceCubes) {
			ResetColors (cube);
		}
	}
		
	/// <summary>
	/// Shows only the Left side pieces.
	/// </summary>
	void Left () {
		AnyColors ();
		List<GameObject> faceCubes = GetFaceCubes (sortedBounds [4]);
		foreach (GameObject cube in faceCubes) {
			ResetColors (cube);
		}
	}

	/// <summary>
	/// Shows only the Back side pieces.
	/// </summary>
	void Back () {
		AnyColors ();
		List<GameObject> faceCubes = GetFaceCubes (sortedBounds [5]);
		foreach (GameObject cube in faceCubes) {
			ResetColors (cube);
		}
	}

	/// <summary>
	/// Shows the Center pieces.
	/// </summary>
	void CenterCubes () {
		ResetColors (cube05);
		ResetColors (cube11);
		ResetColors (cube13);
		ResetColors (cube15);
		ResetColors (cube17);
		ResetColors (cube23);
	}

	/// <summary>
	/// Shows the Edge pieces.
	/// </summary>
	void EdgeCubes () {
		ResetColors (cube02);
		ResetColors (cube04);
		ResetColors (cube06);
		ResetColors (cube08);
		ResetColors (cube10);
		ResetColors (cube12);
		ResetColors (cube16);
		ResetColors (cube18);
		ResetColors (cube20);
		ResetColors (cube22);
		ResetColors (cube24);
		ResetColors (cube26);
	}

	/// <summary>
	/// Shows the Corner pieces.
	/// </summary>
	void CornerCubes () {
		ResetColors (cube00);
		ResetColors (cube01);
		ResetColors (cube03);
		ResetColors (cube07);
		ResetColors (cube09);
		ResetColors (cube19);
		ResetColors (cube21);
		ResetColors (cube25);
	}

	/// <summary>
	/// Shows the White Cross pieces.
	/// </summary>
	void WhiteCross () {
		ResetColors (cube23);
		ResetColors (cube11);
		ResetColors (cube15);
		ResetColors (cube17);
		ResetColors (cube13);
		ResetColors (cube20);
		ResetColors (cube24);
		ResetColors (cube26);
		ResetColors (cube22);
	}

	/// <summary>
	/// Shows the White Corners pieces.
	/// </summary>
	void WhiteCorners () {
		ResetColors (cube23);
		ResetColors (cube11);
		ResetColors (cube15);
		ResetColors (cube17);
		ResetColors (cube13);
		ResetColors (cube20);
		ResetColors (cube24);
		ResetColors (cube26);
		ResetColors (cube22);
		ResetColors (cube21);
		ResetColors (cube00);
		ResetColors (cube25);
		ResetColors (cube19);
	}

	/// <summary>
	/// Shows the Middle Layer pieces.
	/// </summary>
	void MiddleLayer () {
		ResetColors (cube23);
		ResetColors (cube11);
		ResetColors (cube15);
		ResetColors (cube17);
		ResetColors (cube13);
		ResetColors (cube20);
		ResetColors (cube24);
		ResetColors (cube26);
		ResetColors (cube22);
		ResetColors (cube21);
		ResetColors (cube00);
		ResetColors (cube25);
		ResetColors (cube19);
		ResetColors (cube12);
		ResetColors (cube18);
		ResetColors (cube16);
		ResetColors (cube10);
	}

	/// <summary>
	/// Shows the Yellow Cross pieces.
	/// </summary>
	void YellowCross () {
		MiddleLayer ();
		ResetColors (cube05);
		ResetColors (cube04);
		ResetColors (cube08);
		ResetColors (cube02);
		ResetColors (cube06);
		foreach (GameObject cube in yellowCross) {
			Material[] mats = cube.GetComponent<Renderer> ().materials;
			int i = 0;
			foreach (Material mat in mats) {
				if (mat.name != "Yellow (Instance)") {
					if (mat.name != "Plastic (Instance)") {
						mats [i] = any;
					}
				}
				i++;
			}
			cube.GetComponent<Renderer> ().materials = mats;
		}

	}

	/// <summary>
	/// Shows the Yellow Corners pieces.
	/// </summary>
	void YellowCorners () {
		YellowCross ();
		ResetColors (cube07);
		ResetColors (cube01);
		ResetColors (cube09);
		ResetColors (cube03);
		foreach (GameObject cube in yellowCorners) {
			Material[] mats = cube.GetComponent<Renderer> ().materials;
			int i = 0;
			foreach (Material mat in mats) {
				if (mat.name != "Yellow (Instance)") {
					if (mat.name != "Plastic (Instance)") {
						mats [i] = any;
					}
				}
				i++;
			}
			cube.GetComponent<Renderer> ().materials = mats;
		}
	}

	/// <summary>
	/// Shows the Top Layer pieces.
	/// </summary>
	void TopLayer () {
		YellowCross ();
		ResetColors (cube07);
		ResetColors (cube01);
		ResetColors (cube09);
		ResetColors (cube03);
	}
	#endregion

	/// <summary>
	/// Changes all colors of the Rubik's Cube to gray.
	/// </summary>
	void AnyColors () {
		ResetColors ();
		foreach (GameObject cube in allCubes) {
			Material[] mats = cube.GetComponent<Renderer> ().materials;
			int i = 0;
			foreach (Material mat in mats) {
				if (mat.name != "Plastic (Instance)") {
					mats [i] = any;
				}
				i++;
			}
			cube.GetComponent<Renderer> ().materials = mats;
		}
	}

	/// <summary>
	/// Resets the colors of the entire Rubik's Cube.
	/// </summary>
	void ResetColors () {
		foreach (GameObject cube in allCubes) {
			string cubeNum = cube.name.Substring (5);
			Material[] mats = GetOriginalColors (cubeNum);
			cube.GetComponent<Renderer> ().materials = mats;
		}
	}

	/// <summary>
	/// Resets the colors of one piece.
	/// </summary>
	/// <param name="cube">Cube.</param>
	void ResetColors (GameObject cube) {
		string cubeNum = cube.name.Substring (5);
		Material[] mats = GetOriginalColors (cubeNum);
		cube.GetComponent<Renderer> ().materials = mats;
	}

	/// <summary>
	/// Gets the original colors of a piece. Used to reset the colors.
	/// </summary>
	/// <returns>The original colors of a piece.</returns>
	/// <param name="cubeIndex">Piece index.</param>
	Material[] GetOriginalColors (string cubeIndex) {
		switch (cubeIndex) {
		case "00":
			return original00;
			
		case "01":
			return original01;
			
		case "02":
			return original02;
			
		case "03":
			return original03;
			
		case "04":
			return original04;
			
		case "05":
			return original05;
			
		case "06":
			return original06;
			
		case "07":
			return original07;
			
		case "08":
			return original08;
			
		case "09":
			return original09;
			
		case "10":
			return original10;
			
		case "11":
			return original11;
			
		case "12":
			return original12;
			
		case "13":
			return original13;
			
		case "14":
			return original14;
			
		case "15":
			return original15;
			
		case "16":
			return original16;
			
		case "17":
			return original17;
			
		case "18":
			return original18;
			
		case "19":
			return original19;
			
		case "20":
			return original20;
			
		case "21":
			return original21;
			
		case "22":
			return original22;
			
		case "23":
			return original23;
			
		case "24":
			return original24;
			
		case "25":
			return original25;
			
		case "26":
			return original26;
			
		}
		return original00;
	}

	/// <summary>
	/// Turns glow off for all pieces.
	/// </summary>
	void GlowOff () {
		foreach (GameObject cube in allCubes) {
			Material[] mats = cube.GetComponent<Renderer> ().materials;
			int i = 0;
			foreach (Material mat in mats) {
				if (mat.name == "Glow (Instance)") {
					mats [i] = plastic;
				}
				i++;
			}
			cube.GetComponent<Renderer> ().materials = mats;
		}
	}

	/// <summary>
	/// Turns glow on for pieces of a given side.
	/// </summary>
	/// <param name="bound">Bounds of the desired side.</param>
	void GlowOn (Bounds bound) {
		List<GameObject> faceCubes = GetFaceCubes (bound);
		foreach (GameObject cube in faceCubes) {
			Material[] mats = cube.GetComponent<Renderer> ().materials;
			int i = 0;
			foreach (Material mat in mats) {
				if (mat.name == "Plastic (Instance)") {
					mats [i] = glow;
				}
				i++;
			}
			cube.GetComponent<Renderer> ().materials = mats;
		}
	}

	/// <summary>
	/// Turns glow on for one piece.
	/// </summary>
	/// <param name="cube">Cube.</param>
	void GlowOn (GameObject cube) {
		Material[] mats = cube.GetComponent<Renderer> ().materials;
		int i = 0;
		foreach (Material mat in mats) {
			if (mat.name == "Plastic (Instance)") {
				mats [i] = glow;
			}
			i++;
		}
		cube.GetComponent<Renderer> ().materials = mats;
	}

	/// <summary>
	/// Gets the pieces on a given side.
	/// </summary>
	/// <returns>The pieces on a given side.</returns>
	/// <param name="bound">Bounds of the desired side.</param>
	List<GameObject> GetFaceCubes(Bounds bound) {
		List<GameObject> faceCubes = new List<GameObject> ();
		List<Vector3> allCubesCenters = GetCubesCenters ();
		foreach (Vector3 center in allCubesCenters) {
			if (bound.Contains(center)) {
				int i = allCubesCenters.IndexOf(center);
				faceCubes.Add(allCubes[i]);
			}
		}
		return faceCubes;
	}

	/// <summary>
	/// Gets the pieces' center positions. Used to determine which pieces are on which sides.
	/// </summary>
	/// <returns>The pieces' centers.</returns>
	List<Vector3> GetCubesCenters () {
		List<Vector3> allCubesCenters = new List<Vector3> ();
		foreach (GameObject cube in allCubes) {
			Vector3 cubeCenter = cube.GetComponent<BoxCollider> ().bounds.center;
			allCubesCenters.Add (cubeCenter);
		}
		return allCubesCenters;
	}

	/// <summary>
	/// Reverts the Rubik's Cube to the completely solved configuration. 
	/// Flips the Rubik's Cube back to the White side up, if it isn't already.
	/// </summary>
	void ResetCube () {
		if (sunnySideUp < 1) {
			StartCoroutine (SwapTopView ());
		}
		foreach (GameObject cube in allCubes) {
			cube.transform.rotation = whiteSideUp;
		}
	}

	/// <summary>
	/// Used to update the variables describing the top side of the Rubik's Cube.
	/// </summary>
	void SetSideUp () {
		doUpSwap = false;
		sunnySideUp *= -1;
	}

	/// <summary>
	/// Swaps the top view of the Rubik's Cube.
	/// Reconfigures the rotation int variables according to the new camera view.
	/// Changes the transforms of the sides to match the new camera view.
	/// </summary>
	IEnumerator SwapTopView () {
		while (solver.GetComponent<TutorialSolver> ().IsTurning ()) {
			yield return null;
		}
		doUpSwap = false;
		sunnySideUp *= -1;
		Vector3 sun = new Vector3 (180, 0, 0);
		this.transform.rotation *= Quaternion.Euler (sun);
		faceConfig = cam.GetComponent<TutorialCamera> ().GetFaceConfig ();
		ConfigureMoves ();
		SetFaceTransforms ();
		solver.GetComponent<TutorialSolver> ().SetFaceTransforms ();
		yield return null;
	}

	/// <summary>
	/// Sets the face transforms whenever the Rubik's Cube is flipped upside down.
	/// </summary>
	void SetFaceTransforms () {
		Vector3 oldFpos = allFaces [0].transform.position;
		allFaces [0].transform.position = allFaces [5].transform.position;
		allFaces [5].transform.position = oldFpos;

		Vector3 oldRpos = allFaces [2].transform.position;
		allFaces [2].transform.position = allFaces [4].transform.position;
		allFaces [4].transform.position = oldRpos;

		for (int i = 0; i < allFaces.Count; i++) {
			allFacesBounds [i] = allFaces [i].GetComponent<MeshCollider> ().bounds;
		}

		f_bounds = allFacesBounds [5];
		d_bounds = allFacesBounds [1];
		r_bounds = allFacesBounds [4];
		u_bounds = allFacesBounds [3];
		l_bounds = allFacesBounds [2];
		b_bounds = allFacesBounds [0];
	}

	/// <summary>
	/// Prints the current tutorial step. Used for debugging.
	/// </summary>
	public void Test () {
		print (tutorialStep);
	}

	/// <summary>
	/// Skips to a particular phase. Used for debugging.
	/// </summary>
	public void SkipToPhaseII () {
		tutorialStep = 175;
	}

	/// <summary>
	/// Gets the face configuration according to the camera view.
	/// </summary>
	/// <returns>The face configuration.</returns>
	public int GetFaceConfig () {
		return faceConfig;
	}

	/// <summary>
	/// Gets the side facing up.
	/// </summary>
	/// <returns>The up side.</returns>
	public int GetUpFace () {
		return sunnySideUp;
	}

	/// <summary>
	/// Gets the tutorial step.
	/// </summary>
	/// <returns>The tutorial step.</returns>
	public int GetTutorialStep () {
		return tutorialStep;
	}

	/// <summary>
	/// Ends the tutorial. Changes the scene to the main scene.
	/// </summary>
	public void EndTutorial () {
		SceneManager.LoadScene ("_main");
	}

}
