using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class FirebaseCustomInitialization : MonoBehaviour
{
	DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
	protected bool isFirebaseInitialized = false;
	public static readonly string FIREBASE_REALTIME_DB_ENDPOINT = "https://poker-aparat-u-kafani-default-rtdb.europe-west1.firebasedatabase.app/";
	private DatabaseReference reference;

	private static FirebaseCustomInitialization _instance;

	public List<LeaderboardEntry> LeaderboardEntries = new List<LeaderboardEntry>();

	public List<LeaderboardEntry> LeaderboardEntriesOrdered
	{
		get
		{
			return LeaderboardEntries.OrderByDescending(obd => obd.score).ToList();
		}
	}

	public static FirebaseCustomInitialization Instance
	{
		get
		{
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
		DontDestroyOnLoad(this);
	}

	void Start()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
		{
			dependencyStatus = task.Result;
			if (dependencyStatus == DependencyStatus.Available)
			{
				InitializeFirebase();
			}
			else
			{
				Debug.LogError(
				  "Could not resolve all Firebase dependencies: " + dependencyStatus);
			}
		});
	}

	void Update()
	{
	}

	protected virtual void InitializeFirebase()
	{
		FirebaseDatabase frbDb = FirebaseDatabase.GetInstance(FIREBASE_REALTIME_DB_ENDPOINT);
		reference = frbDb.RootReference;
		HighscoreListener();
		isFirebaseInitialized = true;
	}

	protected void HighscoreListener()
	{
		reference.Database
		  .GetReference("highscores").OrderByChild("score").LimitToLast(100)
		  .ValueChanged += (object sender2, ValueChangedEventArgs e2) =>
		  {
			  LeaderboardEntries.Clear();
			  if (e2.DatabaseError != null)
			  {
				  Debug.LogError(e2.DatabaseError.Message);
				  return;
			  }

			  if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
			  {
				  foreach (var childSnapshot in e2.Snapshot.Children)
				  {

					  Debug.Log(childSnapshot.GetRawJsonValue());
					  LeaderboardEntry entry = StringSerializationAPI.Deserialize(typeof(LeaderboardEntry), childSnapshot.GetRawJsonValue()) as LeaderboardEntry;
					  LeaderboardEntries.Add(entry);
				  }
			  }
		  };
	}

	public void DeleteHighscoreRecord(string username)
    {
		reference.Database.GetReference("highscores").Child(username).SetValueAsync(null);
	}

	public void WriteNewScore(string userId, int score)
	{
		// Create new entry at /user-scores/$userid/$scoreid and at
		// /leaderboard/$scoreid simultaneously
		LeaderboardEntry entry = new LeaderboardEntry(userId, score);
		var entryJson = StringSerializationAPI.Serialize(typeof(LeaderboardEntry), entry);

		reference.Database.GetReference("highscores").Child(userId).SetRawJsonValueAsync(entryJson);
	}

	private void GetScores()
	{
		reference.Database
		  .GetReference("highscores")
		  .GetValueAsync().ContinueWith(task =>
		  {
			  if (task.IsFaulted)
			  {
				  // Handle the error...
			  }
			  else if (task.IsCompleted)
			  {
				  DataSnapshot snapshot = task.Result;
				  // Do something with snapshot...
			  }
		  });
	}
}
