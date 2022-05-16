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
            AStarPath path = null;
            //AStarPath path = AStarManager.Instance.FindPath(new Vector3(-10, 1, -10), new Vector3(10, 1, 10));
            AStarPathRequest request = new AStarPathRequest(new Vector3(-10, 1, -10), new Vector3(10, 1, 10), (_path, _success) =>
            {
                Debug.Assert(_success, "Failed to find path... Request path returned false on _success.");
                path = _path;
            });
            AStarRequestManager.RequestPath(request);
            
            yield return new WaitForSeconds(1);
            
            Debug.Assert(path != null, "Unable to find path between points.");
        }

        [UnityTearDown]
        public void TearDownTests()
        {
            Debug.LogError("Finished Tests");
        }
    }
}