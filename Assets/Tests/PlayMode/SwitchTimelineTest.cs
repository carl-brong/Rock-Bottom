using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/*Carl Brong
  Unit Test 1: SwitchTimelines
*/

public class NewTestScript
{
    private bool toggle = false;
    private GameObject gameObject = new GameObject();

    [UnityTest]
    public IEnumerator SwitchTimelines()
    {
        
       
        //gets the "Past" and "Present" children from the "Terrain" parent
        var testPast = new GameObject("Past");
        var testPresent = new GameObject("Present");

        var switchTimelines = gameObject.AddComponent<SwitchTimelines>();
        switchTimelines.Past = testPast;
        switchTimelines.Present = testPresent;
        //switches tileset between the two game objects labeled "Past" and "Present"
        testPast.SetActive(!toggle);
        testPresent.SetActive(toggle);

        //function  from SwitchTimeline script
        switchTimelines.switchTime();

        //checks if the function performed correctly by checking if the true/false comparisons are true
        Assert.IsTrue(testPast.activeSelf, "Past should be active after switch.");
        Assert.IsFalse(testPresent.activeSelf, "Present should be inactive after switch.");

        yield return null;
    }
}


