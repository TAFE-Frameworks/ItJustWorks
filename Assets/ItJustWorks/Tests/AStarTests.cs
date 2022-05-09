using ItJustWorks.AI.AStar;

using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ItJustWorks.Tests
{
    public class AStarTests
    {
        [UnitySetUp]
        public IEnumerator SetupTests()
        {
            yield return SceneManager.LoadSceneAsync("PathfindingSamples");
        }
        
        [UnityTest]
        public IEnumerator FindAStarPathTest()
        {
            AStarPath path = AStarManager.Instance.FindPath(new Vector3(-10, 1, -10), new Vector3(10, 1, 10));
            
            yield return null;
            
            Debug.Assert(path != null, "Unable to find path between points.");
        }

        [UnityTearDown]
        public void TearDownTests()
        {
            Debug.LogError("Finished Tests");
        }
    }
}