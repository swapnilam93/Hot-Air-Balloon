using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Valve.VR;

// Add this script as a component to the GameObject you want to track
public class TrackedObject : MonoBehaviour {

	private enum TrackedDevice{
		Tracker1,
		Tracker2,
	}

	[Header("Don't change when playing")]
	[SerializeField] private TrackedDevice trackedDevice;

	private static List<uint> trackers = new List<uint>();
    private static List<uint> controllers = new List<uint>();
    
	private static bool initialized = false;

	private uint index;

	private void Start() {
		// PrintTrackers();
		InitailizeViveDeviceInfo();

		switch (trackedDevice) {
			case TrackedDevice.Tracker1:
				if (trackers.Count >= 1) {
					var steamVRTrackedObject = gameObject.AddComponent<SteamVR_TrackedObject>();
					steamVRTrackedObject.SetDeviceIndex((int) trackers[0]);
					index = trackers[0];
				} else {
					Debug.LogError("You are trying to use tracker 1 but there is no tracker connected"
					+ "Make sure the trackers are turned on and show stable green light. If the problem persists, find a TA.");
				}
				break;
			case TrackedDevice.Tracker2:
				if (trackers.Count >= 2) {
					var steamVRTrackedObject = gameObject.AddComponent<SteamVR_TrackedObject>();
					steamVRTrackedObject.SetDeviceIndex((int) trackers[1]);
					index = trackers[1];
				} else {
					Debug.LogError("You are trying to use tracker 2 but there is only one or no tracker connected."
					+ "Make sure the trackers are turned on and show stable green light. If the problem persists, find a TA.");
				}
				break;
		}
	}

	private void Update() {
		if (!OpenVR.System.IsTrackedDeviceConnected(index)) {
			Debug.LogWarning("A tracker (index: " + index + ") is being referenced but not tracked properly." 
			+ "Check if all trackers are turned on and visible to the base stations");
		}
	}

	private static void InitailizeViveDeviceInfo() {
		if (initialized) return;
		
		for (uint i = 0; i < 16; i++) {
			ETrackedDeviceClass deviceClass = OpenVR.System.GetTrackedDeviceClass(i);
			switch (deviceClass) {
				case ETrackedDeviceClass.Controller:
					controllers.Add(i);
					break;
				case ETrackedDeviceClass.GenericTracker:
					trackers.Add(i);
					break;
			}
		}
		initialized = true;
	}

	// print all vive device information to the console. You may not need all information
	// and it's totally ok if you don't understand what's going on here
	private static void PrintTrackers() {
		var error = ETrackedPropertyError.TrackedProp_Success;
		Debug.Log("<color=orange>vvvvvvvvvvvvvvv Vive Trackers vvvvvvvvvvvvvvv</color>");
		for (uint i = 0; i < 16; i++) {
			
			var serialNumber = new StringBuilder();
			var modelLabel = new StringBuilder();
			var modelNumber = new StringBuilder();
			
			var wirelessDongle = new StringBuilder();
			OpenVR.System.GetStringTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_SerialNumber_String, serialNumber, 64, ref error);
			OpenVR.System.GetStringTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_ModeLabel_String, modelLabel, 64, ref error);
			OpenVR.System.GetStringTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_ConnectedWirelessDongle_String,
				wirelessDongle, 64, ref error);
			OpenVR.System.GetStringTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_ModelNumber_String, modelNumber , 64, ref error);
			ETrackedDeviceClass deviceClass = OpenVR.System.GetTrackedDeviceClass(i);
			
			Debug.Log( 
				"(" + i + ") " 
				+ "" + (SteamVR_TrackedObject.EIndex) i + ", "
				+ "<color=orange>" + serialNumber + ", </color>"
				+ "<color=green>" + deviceClass + ", </color>"
				+ "<color=fuchsia>" + modelNumber + ", </color>"
				);
		}
		Debug.Log("<color=orange>^^^^^^^^^^ Vive Trackers ^^^^^^^^^^</color>");
	}
}
